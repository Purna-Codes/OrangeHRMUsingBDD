using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;


namespace OrangeHRMUsingBDD.Utility
{
    public class ExtentReport
    {
        //To Generate Html Report   
        public static ExtentReports _extentReports;
       
        //For maintaining the hierarchy: Feature>Scenario
        public static ExtentTest _feature;
        public static ExtentTest _scenario;

        //For fetching The Path
       // public static string dir=AppDomain.CurrentDomain.BaseDirectory;

        
        //For Setting the  Path to the Folder in the WD --> String Manipulation
        //public static string testResultPath = dir.Replace("bin\\Debug\\net8.0","TestResults");
        public static string testResultPath = "C:\\Users\\Lenovo\\source\\repos\\OrangeHRMUsingBDD\\OrangeHRMUsingBDD\\TestResults\\result1.html";

        public static void InitExtentReport()
        {
            // Log the folder path to ensure it's being set correctly
            //Console.WriteLine("Test Dir folder path: " + dir);

            // Log the folder path to ensure it's being set correctly
            Console.WriteLine("Test result folder path: " + testResultPath);

            var sparkReporter = new ExtentSparkReporter(testResultPath);
            sparkReporter.Config.ReportName = "OrangeHRM BDD Report";
            sparkReporter.Config.DocumentTitle = "OrangeHRM Automation";
            sparkReporter.Config.Theme = Theme.Dark;

            // Create an indtance of ExtentReports
            _extentReports = new ExtentReports();

            // Attach Spark Report With Extent Report
            _extentReports.AttachReporter(sparkReporter);

            // Configure The System Information
            _extentReports.AddSystemInfo("DeviceName:", "Lenovo-Purna");
            _extentReports.AddSystemInfo("OperatingSystem:", "WINDOWS 11");
            _extentReports.AddSystemInfo("Browser:", "Chrome");
            _extentReports.AddSystemInfo("BrowserVersion:", "chrome-131.0.6778.265");

        }
        public static void TearDownExtentReport()
        {
            _extentReports.Flush();
        }

        public static string AddScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();
            string screenshotLocation = "C:\\Users\\Lenovo\\source\\repos\\OrangeHRMUsingBDD\\OrangeHRMUsingBDD\\TestResults\\" + scenarioContext.ScenarioInfo.Title + ".png";
            byte[] screenshotBytes = screenshot.AsByteArray;
            File.WriteAllBytes(screenshotLocation, screenshotBytes);
            return screenshotLocation;
        }
    }
}
