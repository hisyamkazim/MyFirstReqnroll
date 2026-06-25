using MyFirstReqnroll.Configurations.Options;
using MyFirstReqnroll.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace MyFirstReqnroll.Steps
{
    [Binding]
    public class LoginStepDefinitions : BaseStep
    {
        protected readonly IWebDriver _webDriver;

        private readonly LoginPage _loginPage;

        private readonly AccountOverviewPage _accountOverviewPage;

        public LoginStepDefinitions(IWebDriver webDriver, BaseUrlOptions baseUrlOptions) : base(webDriver, baseUrlOptions)
        {
            _webDriver = webDriver;
            _loginPage = new LoginPage(_webDriver, baseUrlOptions);
            _accountOverviewPage = new AccountOverviewPage(_webDriver, baseUrlOptions);
        }

        [Given(@"User goes to Login page")]
        public void GivenUserGoesToLoginPage()
        {
            _loginPage.Load();
        }

        [When(@"User enters username {string} and password {string}")]
        public void WhenUserEntersUsernameAndPassword(string username, string password)
        {
            _loginPage.SubmitLogin(username, password);
        }

        [Then(@"User should be logged in successfully")]
        public void ThenUserShouldBeLoggedInSuccessfully()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_loginPage.CurrentUrl.Contains(_baseUrlOptions.AccountOverview), "Expected to be on account overview page after login.");
                Assert.IsTrue(_accountOverviewPage.IsLogoutLinkDisplayed(), "Expected logout link not found.");
            });
        }

        [Then(@"User should see welcome message")]
        public void ThenUserShouldSeeWelcomeMessage()
        {
            Assert.IsTrue(_accountOverviewPage.IsWelcomeMessageDisplayed(), "Expected welcome message not found.");
        }

        [Then(@"User should see error message {string}")]
        public void ThenUserShouldSeeErrorMessage(string message)
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_loginPage.IsErrorMessageDisplayed(), "Error message not displayed, expected an error.");
                string errorMessage = _loginPage.GetErrorMessage();
                Assert.That(errorMessage, Does.Contain(message));
            });
        }
    }
}