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

        private readonly LandingPage _landingPage;
        private readonly RegisterPage _registerPage;

        public RegisterStepDefinitions()
        {
            _faker = FakeFieldGenerator.CreateInstance();
            _webDriver = webDriver;
            _landingPage = new LandingPage(_webDriver);
            _registerPage = new RegisterPage(_webDriver);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _landingPage.Load();
        }

        [Given(@"User choose to Register")]
        public void GivenUserChooseToRegister()
        {
            _landingPage.ClickRegister();
        }

        [When(@"User fills Registration Form as follows")]
        public void WhenUserFillsRegistrationFormAsFollows(Table table)
        {
            var data = _faker.FakeTable(table).CreateSet<RegisterData>();
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