﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.8.0.0
//      SpecFlow Generator Version:3.8.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace CarRentalServices.Specs
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.8.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class CarBrowsingFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "CarBrowsing.feature"
#line hidden
        
        public virtual Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext
        {
            get
            {
                return this._testContext;
            }
            set
            {
                this._testContext = value;
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "CarBrowsing", "\tAs a car rental customer\r\n\tI want to be able to choose what car to rent\r\n\tBy bro" +
                    "wsing a selection of available cars for", ProgrammingLanguage.CSharp, ((string[])(null)));
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
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "CarBrowsing")))
            {
                global::CarRentalServices.Specs.CarBrowsingFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>(_testContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Customer can display list of all available cars")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CarBrowsing")]
        public virtual void CustomerCanDisplayListOfAllAvailableCars()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customer can display list of all available cars", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given("a logged in customer \'JohnDoe\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table1.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe",
                            "true"});
                table1.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin",
                            "true"});
                table1.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo",
                            "true"});
#line 9
 testRunner.And("a list of available cars", ((string)(null)), table1, "And ");
#line hidden
#line 14
 testRunner.When("the customer requests available cars", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table2.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe",
                            "true"});
                table2.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin",
                            "true"});
                table2.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo",
                            "true"});
#line 15
 testRunner.Then("the result should be", ((string)(null)), table2, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Customer should not see unavaible cars")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CarBrowsing")]
        public virtual void CustomerShouldNotSeeUnavaibleCars()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customer should not see unavaible cars", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 21
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 22
 testRunner.Given("a logged in customer \'JohnDoe\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table3.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe",
                            "true"});
                table3.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin",
                            "true"});
                table3.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo",
                            "true"});
#line 23
 testRunner.And("a list of available cars", ((string)(null)), table3, "And ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table4.AddRow(new string[] {
                            "4",
                            "Merecedes",
                            "A1",
                            "https://localhost/",
                            "10",
                            "Dusseldorf",
                            "false"});
                table4.AddRow(new string[] {
                            "5",
                            "Skoda",
                            "Octavia",
                            "https://localhost/",
                            "10",
                            "Skopje",
                            "false"});
#line 28
 testRunner.But("these cars are taken", ((string)(null)), table4, "But ");
#line hidden
#line 32
 testRunner.When("the customer requests available cars", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table5.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe",
                            "true"});
                table5.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin",
                            "true"});
                table5.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo",
                            "true"});
#line 33
    testRunner.Then("the result should be", ((string)(null)), table5, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Customers can filter cars by location")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CarBrowsing")]
        public virtual void CustomersCanFilterCarsByLocation()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customers can filter cars by location", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 39
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 40
 testRunner.Given("a logged in customer \'JohnDoe\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location"});
                table6.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe"});
                table6.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin"});
                table6.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo"});
#line 41
 testRunner.And("a list of available cars", ((string)(null)), table6, "And ");
#line hidden
#line 46
 testRunner.When("the customer requests available cars in \'Berlin\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location"});
                table7.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin"});
#line 47
 testRunner.Then("the result should be", ((string)(null)), table7, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Customers can filter cars by price range")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CarBrowsing")]
        public virtual void CustomersCanFilterCarsByPriceRange()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customers can filter cars by price range", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 52
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 53
 testRunner.Given("a logged in customer \'JohnDoe\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location"});
                table8.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe"});
                table8.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin"});
                table8.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo"});
#line 54
 testRunner.And("a list of available cars", ((string)(null)), table8, "And ");
#line hidden
#line 59
 testRunner.When("the customer requests car within price range of 15", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location"});
                table9.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe"});
                table9.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo"});
#line 60
 testRunner.Then("the result should be", ((string)(null)), table9, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Car prices rising on weekends")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CarBrowsing")]
        public virtual void CarPricesRisingOnWeekends()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Car prices rising on weekends", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 66
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table10.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "10",
                            "Kobe",
                            "true"});
                table10.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "20",
                            "Berlin",
                            "true"});
                table10.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "15",
                            "Tokyo",
                            "true"});
                table10.AddRow(new string[] {
                            "4",
                            "Merecedes",
                            "A1",
                            "https://localhost/",
                            "10",
                            "Dusseldorf",
                            "false"});
                table10.AddRow(new string[] {
                            "5",
                            "Skoda",
                            "Octavia",
                            "https://localhost/",
                            "10",
                            "Skopje",
                            "false"});
#line 67
 testRunner.Given("a list of all cars", ((string)(null)), table10, "Given ");
#line hidden
#line 74
 testRunner.When("today \'06/11/2021\' is friday, saturday or sunday", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "Id",
                            "Brand",
                            "Model",
                            "ImageUrl",
                            "PricePerDay",
                            "Location",
                            "IsAvailable"});
                table11.AddRow(new string[] {
                            "1",
                            "Toyota",
                            "Auris",
                            "https://localhost/",
                            "11.5",
                            "Kobe",
                            "true"});
                table11.AddRow(new string[] {
                            "2",
                            "Audi",
                            "A6",
                            "https://localhost/",
                            "23",
                            "Berlin",
                            "true"});
                table11.AddRow(new string[] {
                            "3",
                            "Nissan",
                            "X-Trail",
                            "https://localhost/",
                            "17.25",
                            "Tokyo",
                            "true"});
                table11.AddRow(new string[] {
                            "4",
                            "Merecedes",
                            "A1",
                            "https://localhost/",
                            "11.5",
                            "Dusseldorf",
                            "false"});
                table11.AddRow(new string[] {
                            "5",
                            "Skoda",
                            "Octavia",
                            "https://localhost/",
                            "11.5",
                            "Skopje",
                            "false"});
#line 75
 testRunner.Then("the prices for all cars are increased for 15%", ((string)(null)), table11, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion