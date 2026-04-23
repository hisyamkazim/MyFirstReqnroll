using MyFirstReqnroll.Configurations.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyFirstReqnroll.Pages
{
    public class RegisterPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly BaseUrlOptions _baseUrlOptions;

        public RegisterPage(IWebDriver driver, BaseUrlOptions baseUrlOptions) : base(driver, baseUrlOptions)
        {
            _driver = driver;
            _wait = Wait;
            _baseUrlOptions = baseUrlOptions;
        }

        // ========================
        // Locators
        // ========================
        private IWebElement FirstName => _wait.Until(d => d.FindElement(By.Name("customer.firstName")));
        private IWebElement LastName => _wait.Until(d => d.FindElement(By.Name("customer.lastName")));
        private IWebElement Address => _wait.Until(d => d.FindElement(By.Name("customer.address.street")));
        private IWebElement City => _wait.Until(d => d.FindElement(By.Name("customer.address.city")));
        private IWebElement State => _driver.FindElement(By.Name("customer.address.state"));
        private IWebElement ZipCode => _driver.FindElement(By.Name("customer.address.zipCode"));
        private IWebElement Phone => _driver.FindElement(By.Name("customer.phoneNumber"));
        private IWebElement SSN => _driver.FindElement(By.Name("customer.ssn"));

        private IWebElement Username => _driver.FindElement(By.Name("customer.username"));
        private IWebElement Password => _driver.FindElement(By.Name("customer.password"));
        private IWebElement ConfirmPassword => _driver.FindElement(By.Name("repeatedPassword"));

        private IWebElement RegisterButton => _driver.FindElement(By.CssSelector("input[value='Register']"));

        // Error
        private IWebElement UsernameError => _wait.Until(d => d.FindElement(By.Id("customer.username.errors")));


        private IWebElement WelcomeTitle => _wait.Until(d =>
    d.FindElement(By.CssSelector("#rightPanel h1.title")));

        private IWebElement SuccessMessage => _wait.Until(d =>
    d.FindElement(
            By.XPath("//p[contains(text(),'Your account was created successfully')]")));


        // ========================
        // Actions (Fluent)
        // ========================

        public RegisterPage Load()
        {
            Navigate(_baseUrlOptions.Register);
            return this;
        }

        public RegisterPage EnterFirstName(string value)
        {
            FirstName.Clear();
            FirstName.SendKeys(value);
            return this;
        }

        public RegisterPage EnterLastName(string value)
        {
            LastName.Clear();
            LastName.SendKeys(value);
            return this;
        }

        public RegisterPage EnterAddress(string value)
        {
            Address.Clear();
            Address.SendKeys(value);
            return this;
        }

        public RegisterPage EnterCity(string value)
        {
            City.Clear();
            City.SendKeys(value);
            return this;
        }

        public RegisterPage EnterState(string value)
        {
            State.Clear();
            State.SendKeys(value);
            return this;
        }

        public RegisterPage EnterZipCode(string value)
        {
            ZipCode.Clear();
            ZipCode.SendKeys(value);
            return this;
        }

        public RegisterPage EnterPhone(string value)
        {
            Phone.Clear();
            Phone.SendKeys(value);
            return this;
        }

        public RegisterPage EnterSSN(string value)
        {
            SSN.Clear();
            SSN.SendKeys(value);
            return this;
        }

        public RegisterPage EnterUsername(string value)
        {
            Username.Clear();
            Username.SendKeys(value);
            return this;
        }

        public RegisterPage EnterPassword(string value)
        {
            Password.Clear();
            Password.SendKeys(value);
            return this;
        }

        public RegisterPage EnterConfirmPassword(string value)
        {
            ConfirmPassword.Clear();
            ConfirmPassword.SendKeys(value);
            return this;
        }

        public void ClickRegister()
        {
            RegisterButton.Click();
        }

        // ========================
        // Composite Actions
        // ========================

        public RegisterPage FillBasicInfo(
            string firstName,
            string lastName,
            string address,
            string city,
            string state,
            string zipCode,
            string phone,
            string ssn)
        {
            return this
                .EnterFirstName(firstName)
                .EnterLastName(lastName)
                .EnterAddress(address)
                .EnterCity(city)
                .EnterState(state)
                .EnterZipCode(zipCode)
                .EnterPhone(phone)
                .EnterSSN(ssn);
        }

        public RegisterPage FillCredentials(
            string username,
            string password,
            string confirmPassword)
        {
            return this
                .EnterUsername(username)
                .EnterPassword(password)
                .EnterConfirmPassword(confirmPassword);
        }

        public void SubmitRegistration(RegisterData data)
        {
            FillBasicInfo(data.FirstName, data.LastName, data.Address, data.City, data.State, data.ZipCode, data.Phone, data.SSN);
            FillCredentials(data.Username, data.Password, data.ConfirmPassword);
            ClickRegister();
        }

        public string GetSuccessMessage()
        {
            return SuccessMessage.Text;
        }

        public bool IsSuccessMessageDisplayed()
        {
            try
            {
                return SuccessMessage.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
        }

        public string GetUsernameError()
        {
            return UsernameError.Text;
        }

        public bool IsUsernameErrorDisplayed()
        {
            try
            {
                return UsernameError.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }

        }
    }
}