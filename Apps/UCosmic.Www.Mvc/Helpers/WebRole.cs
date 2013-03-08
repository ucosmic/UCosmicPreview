using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Web.Administration;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace UCosmic.Www.Mvc
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            //var settingAsString = RoleEnvironment.GetConfigurationSettingValue("Full.Setting.Path");

            if (!RoleEnvironment.IsEmulated)
            {
                #region Machine Key Reconfiguration
                // http://msdn.microsoft.com/en-us/library/gg494983.aspx

                _logger = new WebRoleLogger();
                _logger.Log("RoleEntryPoint.OnStart() has been invoked.");
                try
                {
                    // locate the encrypted web.config file
                    var webConfigPath = GetEncryptedWebConfigFilePath();
                    var webConfigFileExists = File.Exists(webConfigPath);
                    if (!webConfigFileExists)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate web.config file at '{0}'.", webConfigPath);

                    // get web.config file contents
                    var webConfigContent = File.ReadAllText(webConfigPath);

                    // construct an XML configuration document
                    var webConfigXmlDocument = new ConfigXmlDocument { InnerXml = webConfigContent, };
                    if (webConfigXmlDocument.DocumentElement == null)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate configProtectedData node in web.config file.");

                    // find the configProtectedData node
                    var configProtectedDataNode = webConfigXmlDocument.DocumentElement.ChildNodes.Cast<XmlNode>()
                        .SingleOrDefault(x => x.Name == "configProtectedData");
                    if (configProtectedDataNode == null)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate configProtectedData node in web.config file.");

                    // find the configProtectedData/provider child node
                    var configProtectionProviderNode = configProtectedDataNode;
                    while (configProtectionProviderNode != null && configProtectionProviderNode.Attributes != null &&
                        (configProtectionProviderNode.Attributes["name"] == null || configProtectionProviderNode.Attributes["thumbprint"] == null))
                    {
                        configProtectionProviderNode = configProtectionProviderNode.ChildNodes.Cast<XmlNode>().FirstOrDefault();
                    }
                    if (configProtectionProviderNode == null || configProtectionProviderNode.Attributes == null)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate configProtectedData/provider child node in web.config file.");

                    // get the configProtectedData/provider node attributes (name & thumbprint)
                    var configProtectionProviderName = configProtectionProviderNode.Attributes["name"].Value;
                    var configProtectionProviderThumbprint = configProtectionProviderNode.Attributes["thumbprint"].Value;

                    // construct & initialize a ProtectedConfigurationProvider
                    var configProtectionProviderAssembly = Assembly.Load("Pkcs12ProtectedConfigurationProvider");
                    var configProtectionProviderType = configProtectionProviderAssembly.GetTypes()
                        .First(t => typeof(ProtectedConfigurationProvider).IsAssignableFrom(t));
                    var protectedConfigurationProvider = Activator.CreateInstance(configProtectionProviderType) as ProtectedConfigurationProvider;
                    if (protectedConfigurationProvider == null)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to construct a ProtectedConfigurationProvider.");

                    protectedConfigurationProvider.Initialize(configProtectionProviderName, new NameValueCollection
                    {
                        { "thumbprint", configProtectionProviderThumbprint },
                    });

                    // get encrypted appSettings XML node
                    var encryptedAppSettingsNode = webConfigXmlDocument.DocumentElement.ChildNodes
                        .Cast<XmlNode>().SingleOrDefault(x => x.Name == "appSettings");
                    if (encryptedAppSettingsNode == null)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate encrypted appSettings node.");

                    // decrypt appSettings XML
                    var decryptedAppSettingsNode = protectedConfigurationProvider.Decrypt(encryptedAppSettingsNode).ChildNodes
                        .Cast<XmlNode>().SingleOrDefault(x => x.Name == "appSettings");
                    if (decryptedAppSettingsNode == null)
                        return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate decrypted appSettings node.");

                    // extract machineConfig values from decrypted appSettings XML
                    var validationKey = GetDecryptedAppSetting(decryptedAppSettingsNode, "MachineValidationKey");
                    var validationAlgorithm = GetDecryptedAppSetting(decryptedAppSettingsNode, "MachineValidationAlgorithm");
                    var decryptionKey = GetDecryptedAppSetting(decryptedAppSettingsNode, "MachineDecryptionKey");
                    var decryptionAlgorithm = GetDecryptedAppSetting(decryptedAppSettingsNode, "MachineDecryptionAlgorithm");
                    if (string.IsNullOrWhiteSpace(validationKey) || string.IsNullOrWhiteSpace(validationAlgorithm) ||
                        string.IsNullOrWhiteSpace(decryptionKey) || string.IsNullOrWhiteSpace(decryptionAlgorithm))
                        return FailBecauseMachineConfigCannotBeReconfigured("A machineKey attribute value could not be found in decrypted appSettings.");

                    using (var server = new ServerManager())
                    {
                        // load IIS site's web configuration
                        var siteName = string.Format("{0}_Web", RoleEnvironment.CurrentRoleInstance.Id);
                        var site = RoleEnvironment.IsEmulated ? server.Sites.First() : server.Sites[siteName];
                        if (site == null)
                            return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate site '{0}'.", siteName);

                        var siteWebConfiguration = site.GetWebConfiguration();
                        if (siteWebConfiguration == null)
                            return FailBecauseMachineConfigCannotBeReconfigured("Unable to load web configuration for site '{0}'.", siteName);

                        var machineKeySection = siteWebConfiguration.GetSection("system.web/machineKey");
                        if (machineKeySection == null)
                            return FailBecauseMachineConfigCannotBeReconfigured("Unable to locate machineConfig section in site '{0}' web configuration.", siteName);

                        // overwrite machineKey values
                        machineKeySection.SetAttributeValue("validationKey", validationKey);
                        machineKeySection.SetAttributeValue("validation", validationAlgorithm);
                        machineKeySection.SetAttributeValue("decryptionKey", decryptionKey);
                        machineKeySection.SetAttributeValue("decryption", decryptionAlgorithm);
                        server.CommitChanges();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == FailBecauseMachineConfigCannotBeReconfiguredMessage) throw;

                    _logger.Log("A(n) {0} exception was encountered while trying to set the machineConfig.", ex.GetType().Name);
                    _logger.Log(ex.Message);
                    _logger.Log(ex.StackTrace);
                    _logger.Log(ex.Source);
                }
                _logger.Dispose();

                #endregion
                #region Diagnostics Trace Logging

                var config = DiagnosticMonitor.GetDefaultInitialConfiguration();

                // Change the polling interval for all logs.
                config.ConfigurationChangePollInterval = TimeSpan.FromSeconds(30.0);

                // Set the transfer interval for all logs.
                config.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1.0);

                // Add performance counter monitoring for configured counters
                var counters = new List<string>
                {
                    @"\Processor(_Total)\% Processor Time",
                    @"\Memory\Available Mbytes",
                    @"\TCPv4\Connections Established",
                    @"\ASP.NET Applications(__Total__)\Requests/Sec",
                    @"\Network Interface(*)\Bytes Received/sec",
                    @"\Network Interface(*)\Bytes Sent/sec"
                };
                foreach (var counterConfig in counters.Select(counter =>
                    new PerformanceCounterConfiguration
                    {
                        CounterSpecifier = counter,
                        SampleRate = TimeSpan.FromMinutes(1)
                    })
                )
                {
                    config.PerformanceCounters.DataSources.Add(counterConfig);
                }
                config.PerformanceCounters.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);

                //Diagnostics Infrastructure logs
                config.DiagnosticInfrastructureLogs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
                config.DiagnosticInfrastructureLogs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;//.error

                //Windows Event Logs
                config.WindowsEventLog.DataSources.Add("System!*");
                config.WindowsEventLog.DataSources.Add("Application!*");
                config.WindowsEventLog.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
                config.WindowsEventLog.ScheduledTransferLogLevelFilter = LogLevel.Warning;

                //Azure Trace Logs
                config.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
                config.Logs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;

                //Crash Dumps
                CrashDumps.EnableCollection(true);

                //IIS Logs
                config.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);

                // start the diagnostics monitor
                DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config);

                #endregion
                #region IIS Domain Binding

                // By default, the website name is "[ Current Role Instance id]_Web"
                var siteName1 = string.Format("{0}_Web", RoleEnvironment.CurrentRoleInstance.Id);

                // In future, if you need add more endpoint(HTTP or HTTPS),
                // please create new bindingEntry and append to the cmd string,
                // separate with ','. For how to use AppCmd to config IIS site,
                // please refer to this article
                // http://learn.iis.net/page.aspx/114/getting-started-with-appcmdexe

                var command = string.Format("set site \"{0}\" /bindings:{1}", siteName1, GetAppCmdBindings());

                const string appCmdPath = @"d:\Windows\System32\inetsrv\appcmd.exe";

                try
                {
                    Process.Start(new ProcessStartInfo(appCmdPath, command));
                    Trace.TraceInformation("Initialize IIS binding succeed.");
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }

                #endregion
            }

            var baseOnStart = base.OnStart();
            return baseOnStart;
        }

        private const string FailBecauseMachineConfigCannotBeReconfiguredMessage =
            "Web Role could not be started because the machineConfig secrets could not be changed.";

        private string GetEncryptedWebConfigFilePath()
        {
            var domainBaseDirectoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            _logger.Log("Base directory of app domain is '{0}'.", domainBaseDirectoryInfo.FullName);
            var webConfigPath = domainBaseDirectoryInfo.FullName;
            if (domainBaseDirectoryInfo.Name.Equals("bin", StringComparison.OrdinalIgnoreCase) && domainBaseDirectoryInfo.Parent != null)
            {
                domainBaseDirectoryInfo = domainBaseDirectoryInfo.Parent;
                webConfigPath = domainBaseDirectoryInfo.FullName;
            }
            if (RoleEnvironment.IsEmulated)
            {
                webConfigPath = _logger.GetSiteRoot();
            }
            webConfigPath = Path.Combine(webConfigPath, "web.config");
            return webConfigPath;
        }

        private string GetDecryptedAppSetting(XmlNode decryptedAppSettingsNode, string key)
        {
            var entryNode = decryptedAppSettingsNode.ChildNodes
                .Cast<XmlNode>().SingleOrDefault(x => x.Name == "add" && x.Attributes != null
                    && x.Attributes["key"] != null && x.Attributes["key"].Value == key);
            if (entryNode != null && entryNode.Attributes != null)
            {
                return entryNode.Attributes["value"].Value;
            }
            _logger.Log("Decrypted appSetting key '{0}' could not be found.");
            throw new InvalidOperationException("Web Role could not be started because the machineConfig secrets could not be changed.");
        }

        private bool FailBecauseMachineConfigCannotBeReconfigured(string format, params object[] args)
        {
            _logger.Log(format, args);
            throw new InvalidOperationException(FailBecauseMachineConfigCannotBeReconfiguredMessage);
        }

        private static string GetAppCmdBindings()
        {
            var config = string.Format("preview.ucosmic.com;ucosmic-preview.cloudapp.net;{0}.cloudapp.net", RoleEnvironment.DeploymentId);
            var domains = config.Split(';').ToList();
            while (domains.FirstOrDefault(string.IsNullOrWhiteSpace) != null)
                domains.Remove(domains.FirstOrDefault(string.IsNullOrWhiteSpace));

            var bindings = new StringBuilder();
            domains.ForEach(domain =>
            {
                if (bindings.Length > 0)
                {
                    bindings.Append(',');
                }
                bindings.Append(string.Format("http/*:80:{0},", domain));
                bindings.Append(string.Format("https/*:443:{0}", domain));
            });

            return bindings.ToString();
        }

        private WebRoleLogger _logger;

        internal class WebRoleLogger : IDisposable
        {
            private readonly StreamWriter _logger;
            private readonly string _logFile;

            internal WebRoleLogger()
            {
                var siteRoot = GetSiteRoot();
                if (siteRoot != null)
                {
                    var appDataDir = Path.Combine(siteRoot, "App_Data\\web-role-logs");
                    Directory.CreateDirectory(appDataDir);
                    var utcNow = DateTime.UtcNow;
                    var fileName = string.Format("log_{0}.{1}.{2}.{3}.{4}.{5}.{6}.txt",
                        utcNow.Year, utcNow.Month, utcNow.Day,
                        utcNow.Hour, utcNow.Minute, utcNow.Second, utcNow.Millisecond);
                    _logFile = Path.Combine(appDataDir, fileName);
                    _logger = File.CreateText(_logFile);
                    _logger.AutoFlush = true;
                    var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    var act = sid.Translate(typeof(NTAccount));
                    var sec = File.GetAccessControl(_logFile);
                    sec.AddAccessRule(new FileSystemAccessRule(act, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(_logFile, sec);
                }
            }

            internal string GetSiteRoot()
            {
                var roleRootDir = Environment.GetEnvironmentVariable("RdRoleRoot") ?? Environment.GetEnvironmentVariable("RoleRoot");
                var appRootDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                if (roleRootDir != null && appRootDir != null)
                {
                    var roleModelDoc = XDocument.Load(Path.Combine(roleRootDir, "RoleModel.xml"));
                    if (roleModelDoc.Root != null)
                    {
                        XNamespace roleModelNs = roleModelDoc.Root.Attribute("xmlns").Value;
                        var sitesElement = roleModelDoc.Root.Element(roleModelNs + "Sites");
                        if (sitesElement != null)
                        {
                            var siteElements = sitesElement.Elements(roleModelNs + "Site");
                            var siteRoot = siteElements.Where(x => x.Attribute("name") != null && x.Attribute("physicalDirectory") != null)
                                            .Select(x => Path.Combine(appRootDir, x.Attribute("physicalDirectory").Value)).First();
                            return siteRoot;
                        }
                    }
                }
                return null;
            }

            internal void Log(string format, params object[] args)
            {
                if (_logger == null) return;

                if (args != null && args.Length > 0) _logger.WriteLine(format, args);
                else _logger.WriteLine(format);
            }

            internal void Dispose()
            {
                if (_logger != null) _logger.Dispose();
            }

            void IDisposable.Dispose()
            {
                Dispose();
            }
        }
    }
}
