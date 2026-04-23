using MyFirstReqnroll.Configurations.Options;
using MyFirstReqnroll.Helpers;
using MyFirstReqnroll.Pages;
using OpenQA.Selenium;
using Reqnroll;

namespace MyFirstReqnroll.StepDefinitions
{
    [Binding]
    public class RegisterStepDefinitions : BaseStep
    {
        private readonly FakeFieldGenerator _faker;
        protected readonly IWebDriver _webDriver;

        private readonly LoginPage _loginPage;
        private readonly RegisterPage _registerPage;

        public RegisterStepDefinitions(IWebDriver webDriver, BaseUrlOptions baseUrlOptions) : base(webDriver, baseUrlOptions)
        {
            _faker = FakeFieldGenerator.CreateInstance();
            _webDriver = webDriver;
            _loginPage = new LoginPage(_webDriver, baseUrlOptions);
            _registerPage = new RegisterPage(_webDriver, baseUrlOptions);
        }

        [Given(@"User choose to Register")]
        public void GivenUserChooseToRegister()
        {
            _loginPage.ClickRegister();
        }

        [When(@"User fills Registration Form as follows")]
        public void WhenUserFillsRegistrationFormAsFollows(Table table)
        {
            var data = _faker.FakeTable(table).CreateInstance<RegisterData>();
            _registerPage.SubmitRegistration(data);
        }

        [Then(@"User should get success message ""(.*)""")]
        public void ThenUserShouldGetSuccessMessage(string message)
        {
            Assert.IsTrue(_registerPage.IsSuccessMessageDisplayed(), "Expected success message not found.");
            string successMessage = _registerPage.GetSuccessMessage();
            Assert.That(successMessage, Does.Contain(message));
        }

        [Then(@"User should get error message ""(.*)""")]
        public void ThenUserShouldGetErrorMessage(string message)
        {
            Assert.IsTrue(_registerPage.IsUsernameErrorDisplayed(), "Expected username error message not found.");
            string errorMessage = _registerPage.GetUsernameError();
            Assert.That(errorMessage, Does.Contain(message));
        }

    }
}