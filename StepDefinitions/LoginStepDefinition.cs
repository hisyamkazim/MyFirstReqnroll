using MyFirstReqnroll.Configurations.Options;
using MyFirstReqnroll.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace MyFirstReqnroll.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions : BaseStep
    {
        protected readonly IWebDriver _webDriver;

        private readonly LoginPage _loginPage;

        public LoginStepDefinitions(IWebDriver webDriver, BaseUrlOptions baseUrlOptions) : base(webDriver, baseUrlOptions)
        {
            _webDriver = webDriver;
            _loginPage = new LoginPage(_webDriver, baseUrlOptions);
        }

        [Given(@"User goes to Login page")]
        public void GivenUserGoesToLoginPage()
        {
            _loginPage.Load();
        }

        [When(@"User enters username ""(.*)"" and password ""(.*)""")]
        public void WhenUserEntersUsernameAndPassword(string username, string password)
        {            _loginPage.EnterUsername(username)
                      .EnterPassword(password)
                      .ClickLogin();
        }

        [Then(@"User should be logged in successfully")]
        public void ThenUserShouldBeLoggedInSuccessfully()
        {
            _loginPage.WaitAndFindElement(By.ClassName("title"));       
            Assert.IsTrue(_loginPage.CurrentUrl.Contains(_baseUrlOptions.AccountOverview), "Expected to be on account overview page after login.");
        }

        [Then(@"User should see welcome message ""(.*)""")]
        public void ThenUserShouldSeeWelcomeMessage(string name)
        {
            string welcomeMessage = _loginPage.WaitAndFindElement(By.XPath("//*[@id='leftPanel']/p")).Text;
            Assert.That(welcomeMessage, Does.Contain(name));
        }

        [Then(@"User should see error message ""(.*)""")]
        public void ThenUserShouldSeeErrorMessage(string message)
        {
            Assert.IsTrue(_webDriver.FindElement(By.Id("errorMessage")).Displayed, "Error message not displayed, expected an error.");
            string errorMessage = _webDriver.FindElement(By.Id("errorMessage")).Text;
            Assert.That(errorMessage, Does.Contain(message));
        }
    }
}