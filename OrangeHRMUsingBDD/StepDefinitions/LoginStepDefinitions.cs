using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;

namespace OrangeHRMUsingBDD.StepDefinitions
{
    [Binding]
    public sealed class LoginStepDefinitions
    {
        private IWebDriver driver;

        public LoginStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given("Navigate to the Login Page URL")]
        public void GivenNavigateToTheLoginPageURL()
        {
            driver.Url = "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login";
            Thread.Sleep(5000);
            Console.WriteLine("Navigated to the Login Page");
        }

        [When("Enter Valid Credentials")]
        public void WhenEnterValidCredentials()
        {
            driver.FindElement(By.Name("username")).SendKeys("Admin");
            driver.FindElement(By.Name("password")).SendKeys("admin123");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
        }

        [When("Enter Invalid Credentials")]
        public void WhenEnterInvalidCredentials()
        {
            driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("Admin");
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin123");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            Thread.Sleep(5000);
        }

        [Then("HomePage must be displayed")]
        public void ThenHomePageMustBeDisplayed()
        {
            Console.WriteLine("Navigated to the " +driver.Title+" Page");
        }

        [Then("Error Message must be displayed")]
        public void ThenErrorMessageMustBeDisplayed()
        {
                var errorMessage = driver.FindElement(By.XPath("//div[@class='orangehrm-login-form']//p[contains(@class,'alert-content-text')]")).Text;
                Console.WriteLine(errorMessage);
            
        }



    }
}