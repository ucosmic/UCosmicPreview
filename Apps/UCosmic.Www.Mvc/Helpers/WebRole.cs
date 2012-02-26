using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace UCosmic.Www.Mvc
{
    // ReSharper disable UnusedMember.Global
    public class WebRole : RoleEntryPoint
    // ReSharper restore UnusedMember.Global
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            var isDeployedToEmulator = RoleEnvironment.GetConfigurationSettingValue("UCosmic.Www.Mvc.DeployedToEmulator");
            if (isDeployedToEmulator == "true") return base.OnStart();

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
            return base.OnStart();
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
    }
}
