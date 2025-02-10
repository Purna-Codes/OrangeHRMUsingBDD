using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;

namespace OrangeHRMUsingBDD.StepDefinitions
{
    [Binding]
    public sealed class DataDrivenTestingLoginStepDefinitions
    {
        private IWebDriver driver;

        public DataDrivenTestingLoginStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given("Navigate to the Login Page using URL {string}")]
        public void GivenNavigateToTheLoginPageUsingURL(string url)
        {
            driver.Url = url;
            Thread.Sleep(5000);
            Console.WriteLine("Navigated to the Login Page");
        }

        [When("username {string} and password as {string}")]
        public void WhenUsernameAndPasswordAs(string username, string password)
        {
            
            driver.FindElement(By.Name("username")).SendKeys(username);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
        }

        [When("Enter password as {string} and username as {string}")]
        public void WhenEnterPasswordAsAndUsernameAs(string password1, string username1)
        {
            driver.FindElement(By.Name("username")).SendKeys(username1);
            driver.FindElement(By.Name("password")).SendKeys(password1);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
        }


    }
}