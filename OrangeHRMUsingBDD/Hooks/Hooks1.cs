using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OrangeHRMUsingBDD.Utility;
using Reqnroll;
using Reqnroll.BoDi;

namespace OrangeHRMUsingBDD.Hooks
{
    [Binding]
    public sealed class Hooks1:ExtentReport
    {
        private readonly IObjectContainer _container;

        public Hooks1(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario("@Login")]
        public void BeforeScenarioWithTag()
        {
            Console.WriteLine("Running inside Tagged Before Scenario Hook");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioConText)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            _container.RegisterInstanceAs<IWebDriver>(driver);

            Console.WriteLine("Running inside Proritized Before Scenario");

            _scenario = _feature.CreateNode<Scenario>(scenarioConText.ScenarioInfo.Title);
        }

        //[BeforeScenario("@ValidLogin", Order = 0)]
        //public void BeforeScenarioWithValidLoginTag(ScenarioContext scenarioContext)
        //{
        //    Console.WriteLine("Running inside Tagged Before Scenario Hook for Valid Login");
        //    IWebDriver driver = new ChromeDriver();
        //    driver.Manage().Window.Maximize();
        //    _container.RegisterInstanceAs<IWebDriver>(driver);
        //    _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        //}

        //[BeforeScenario("@InValidLogin", Order = 1)]
        //public void BeforeScenarioWithInValidLoginTag(ScenarioContext scenarioContext)
        //{
        //    Console.WriteLine("Running inside Tagged Before Scenario Hook for Invalid Login");
        //    IWebDriver driver = new ChromeDriver();
        //    driver.Manage().Window.Maximize();
        //    _container.RegisterInstanceAs<IWebDriver>(driver);
        //    _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        //}

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }

            Console.WriteLine("Running inside After Scenario");
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Running Before test Run");
            InitExtentReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running After test Run");
            TearDownExtentReport();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running Before Feature");
            _feature=_extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("Running After Feature");
            
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running After Step");

            string stepType=scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            var driver = _container.Resolve<IWebDriver>();

            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName);
                }
            }

            // When test is Failed
            if (scenarioContext.TestError != null)
            {
                string strPath=AddScreenshot(driver, scenarioContext);

                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(strPath).Build());
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(strPath).Build());
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(strPath).Build());
                }
            }


        }
    }
}