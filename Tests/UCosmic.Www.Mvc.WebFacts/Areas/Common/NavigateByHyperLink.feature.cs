﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.17379
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UCosmic.Www.Mvc.Areas.Common
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class NavigateUsingHyperlinksFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "NavigateByHyperLink.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Navigate using hyperlinks", "  In order to access UCosmic content\r\n  As someone browsing the web\r\n  I want to " +
                    "navigate the site", ProgrammingLanguage.CSharp, new string[] {
                        "Nav"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((TechTalk.SpecFlow.FeatureContext.Current != null) 
                        && (TechTalk.SpecFlow.FeatureContext.Current.FeatureInfo.Title != "Navigate using hyperlinks")))
            {
                UCosmic.Www.Mvc.Areas.Common.NavigateUsingHyperlinksFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn(string startAt, string linkTextToClick, string arriveAt, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Browse from one url to another using hyperlinks without signing in", exampleTags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 9
    testRunner.Given("I am not signed on");
#line 10
    testRunner.And(string.Format("I am starting from the {0} page", startAt));
#line 11
    testRunner.Then(string.Format("I should see a \"{0}\" link", linkTextToClick));
#line 13
    testRunner.When(string.Format("I click the \"{0}\" link", linkTextToClick));
#line 14
    testRunner.Then(string.Format("I should see the {0} page", arriveAt));
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks without signing in")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Sign On")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Sign On")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn_Home()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn("Home", "Sign On", "Sign On", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks without signing in")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Sign Out")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Sign Out")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Sign On")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Sign On")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn_SignOut()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn("Sign Out", "Sign On", "Sign On", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks without signing in")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Sign On")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Sign On")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Home")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn_SignOn()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksWithoutSigningIn("Sign On", "Home", "Home", ((string[])(null)));
        }
        
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu(string startAt, string linkTextToClick, string arriveAt, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Browse from one url to another using hyperlinks after signing in as any1@uc.edu", exampleTags);
#line 22
this.ScenarioSetup(scenarioInfo);
#line 24
    testRunner.Given("I am signed in as any1@uc.edu");
#line 25
    testRunner.And(string.Format("I am starting from the {0} page", startAt));
#line 26
    testRunner.Then(string.Format("I should see a \"{0}\" link", linkTextToClick));
#line 28
    testRunner.When(string.Format("I click the \"{0}\" link", linkTextToClick));
#line 29
    testRunner.Then(string.Format("I should see the {0} page", arriveAt));
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as any1@uc.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 0")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "any1@uc.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Personal Home")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu_Variant0()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu("Home", "any1@uc.edu", "Personal Home", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as any1@uc.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Personal Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Home")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu_Variant1()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu("Personal Home", "Home", "Home", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as any1@uc.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Personal Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "any1@uc.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Personal Home")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu_Variant2()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsAny1Uc_Edu("Personal Home", "any1@uc.edu", "Personal Home", ((string[])(null)));
        }
        
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu(string startAt, string linkTextToClick, string arriveAt, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Browse from one url to another using hyperlinks after signing in as supervisor@su" +
                    "ny.edu", exampleTags);
#line 37
this.ScenarioSetup(scenarioInfo);
#line 39
    testRunner.Given("I am signed in as manager1@suny.edu");
#line 40
    testRunner.And(string.Format("I am starting from the {0} page", startAt));
#line 41
    testRunner.Then(string.Format("I should see a \"{0}\" link", linkTextToClick));
#line 43
    testRunner.When(string.Format("I click the \"{0}\" link", linkTextToClick));
#line 44
    testRunner.Then(string.Format("I should see the {0} page", arriveAt));
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as supervisor@su" +
            "ny.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 0")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Agreements")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Institutional Agreement Public Search")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu_Variant0()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu("Home", "Agreements", "Institutional Agreement Public Search", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as supervisor@su" +
            "ny.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Personal Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Agreements")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Institutional Agreement Public Search")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu_Variant1()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu("Personal Home", "Agreements", "Institutional Agreement Public Search", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as supervisor@su" +
            "ny.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 2")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Institutional Agreement Management")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Home")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Home")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu_Variant2()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu("Institutional Agreement Management", "Home", "Home", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Browse from one url to another using hyperlinks after signing in as supervisor@su" +
            "ny.edu")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Navigate using hyperlinks")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("Nav")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Variant 3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:StartAt", "Institutional Agreement Management")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:LinkTextToClick", "Add a new agreement")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ArriveAt", "Institutional Agreement Add")]
        public virtual void BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu_Variant3()
        {
            this.BrowseFromOneUrlToAnotherUsingHyperlinksAfterSigningInAsSupervisorSuny_Edu("Institutional Agreement Management", "Add a new agreement", "Institutional Agreement Add", ((string[])(null)));
        }
    }
}
#pragma warning restore
#endregion