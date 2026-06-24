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

        By FirstNameLocator => By.Name("customer.firstName");
        By LastNameLocator => By.Name("customer.lastName");
        By AddressLocator => By.Name("customer.address.street");
        By CityLocator => By.Name("customer.address.city");
        By StateLocator => By.Name("customer.address.state");
        By ZipCodeLocator => By.Name("customer.address.zipCode");
        By PhoneLocator => By.Name("customer.phoneNumber");
        By SSNLocator => By.Name("customer.ssn");
        By UsernameLocator => By.Name("customer.username");
        By PasswordLocator => By.Name("customer.password");
        By ConfirmPasswordLocator => By.Name("repeatedPassword");
        By RegisterButtonLocator => By.CssSelector("input[value='Register']");
        By UsernameErrorLocator => By.Id("customer.username.errors");
        By WelcomeTitleLocator => By.CssSelector("#rightPanel h1.title");
        By SuccessMessageLocator => By.XPath("//p[contains(text(),'Your account was created successfully')]");
        
        private IWebElement FirstName => WaitAndFindElement(FirstNameLocator);
        private IWebElement LastName => WaitAndFindElement(LastNameLocator);
        private IWebElement Address => WaitAndFindElement(AddressLocator);
        private IWebElement City => WaitAndFindElement(CityLocator);
        private IWebElement State => WaitAndFindElement(StateLocator);
        private IWebElement ZipCode => WaitAndFindElement(ZipCodeLocator);
        private IWebElement Phone => WaitAndFindElement(PhoneLocator);
        private IWebElement SSN => WaitAndFindElement(SSNLocator);

        private IWebElement Username => WaitAndFindElement(UsernameLocator);
        private IWebElement Password => WaitAndFindElement(PasswordLocator);
        private IWebElement ConfirmPassword => WaitAndFindElement(ConfirmPasswordLocator);

        private IWebElement RegisterButton => WaitAndFindElement(RegisterButtonLocator);

        // Error
        private IWebElement UsernameError => WaitAndFindElement(UsernameErrorLocator);


        private IWebElement WelcomeTitle => WaitAndFindElement(WelcomeTitleLocator);

        private IWebElement SuccessMessage => WaitAndFindElement(SuccessMessageLocator);


        // ========================
        // Actions (Fluent)
        // ========================

        public void Load()
        {
            Navigate(_baseUrlOptions.Register);
        }

        public void EnterFirstName(string value)
        {
            FirstName.Clear();
            FirstName.SendKeys(value);
        }

        public void EnterLastName(string value)
        {
            LastName.Clear();
            LastName.SendKeys(value);
        }

        public void EnterAddress(string value)
        {
            Address.Clear();
            Address.SendKeys(value);
        }

        public void EnterCity(string value)
        {
            City.Clear();
            City.SendKeys(value);
        }

        public void EnterState(string value)
        {
            State.Clear();
            State.SendKeys(value);
        }

        public void EnterZipCode(string value)
        {
            ZipCode.Clear();
            ZipCode.SendKeys(value);
        }

        public void EnterPhone(string value)
        {
            Phone.Clear();
            Phone.SendKeys(value);
        }

        public void EnterSSN(string value)
        {
            SSN.Clear();
            SSN.SendKeys(value);
        }

        public void EnterUsername(string value)
        {
            Username.Clear();
            Username.SendKeys(value);
        }

        public void EnterPassword(string value)
        {
            Password.Clear();
            Password.SendKeys(value);
        }

        public void EnterConfirmPassword(string value)
        {
            ConfirmPassword.Clear();
            ConfirmPassword.SendKeys(value);
        }

        public void ClickRegister()
        {
            RegisterButton.Click();
        }

        // ========================
        // Composite Actions
        // ========================

        public void FillBasicInfo(
            string firstName,
            string lastName,
            string address,
            string city,
            string state,
            string zipCode,
            string phone,
            string ssn)
        {

                EnterFirstName(firstName);
                EnterLastName(lastName);
                EnterAddress(address);
                EnterCity(city);
                EnterState(state);
                EnterZipCode(zipCode);
                EnterPhone(phone);
                EnterSSN(ssn);
        }

        public void FillCredentials(
            string username,
            string password,
            string confirmPassword)
        {
                
                EnterUsername(username);
                EnterPassword(password);
                EnterConfirmPassword(confirmPassword);
        }

        public void SubmitRegistration(RegisterData data)
        {
            FillBasicInfo(data.FirstName, data.LastName, data.Address, data.City, data.State, data.ZipCode, data.Phone, data.SSN);
            FillCredentials(data.Username, data.Password, data.ConfirmPassword);
            ClickRegister();
        }

        // ========================
        // Response Texts
        // ========================
        public string GetSuccessMessage() => SuccessMessage.Text;
        public string GetUsernameError() => UsernameError.Text;


        // ========================
        // Response Checks
        // ========================
        public bool IsSuccessMessageDisplayed()
        {
            try
            {
                WaitForElementDisplayed(SuccessMessageLocator);
                return SuccessMessage.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsUsernameErrorDisplayed()
        {
            try
            {
                WaitForElementDisplayed(UsernameErrorLocator);
                return UsernameError.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }

        }
    }
}