using MyFirstReqnroll.Configurations.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyFirstReqnroll.Pages
{
    public class LoginPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly BaseUrlOptions _baseUrlOptions;


        public LoginPage(IWebDriver driver, BaseUrlOptions baseUrlOptions) : base(driver, baseUrlOptions)
        {
            _driver = driver;
            _wait = Wait;
            _baseUrlOptions = baseUrlOptions;
        }

        // ========================
        // Locators
        // ========================
        By UsernameLocator => By.Name("username");
        By PasswordLocator => By.Name("password");
        By LoginButtonLocator => By.CssSelector("input[value='Log In']");
        By RegisterLinkLocator => By.LinkText("Register");
        By ForgotLoginInfoLinkLocator => By.LinkText("Forgot login info?");
        By ErrorMessageLocator => By.Id("errorMessage");

        private IWebElement UsernameInput => WaitAndFindElement(UsernameLocator);
        private IWebElement PasswordInput => WaitAndFindElement(PasswordLocator);
        private IWebElement LoginButton => WaitAndFindElement(LoginButtonLocator);

        private IWebElement RegisterLink => WaitAndFindElement(RegisterLinkLocator);
        private IWebElement ForgotLoginInfoLink => WaitAndFindElement(ForgotLoginInfoLinkLocator);
        private IWebElement ErrorMessage => WaitAndFindElement(ErrorMessageLocator);

        // ========================
        // Actions (Fluent)
        // ========================

        public void Load()
        {
            Navigate(_baseUrlOptions.Login);
        }

        public void EnterUsername(string username)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }

        public void SubmitLogin(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public void ClickRegister()
        {
            RegisterLink.Click();
        }

        public void ClickForgotLoginInfo()
        {
            ForgotLoginInfoLink.Click();
        }

        public bool IsErrorMessageDisplayed()
        {
            try
            {
                WaitForElementDisplayed(ErrorMessageLocator);
                return ErrorMessage.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
        }

        public string GetErrorMessage()
        {
            return ErrorMessage.Text;
        }
    }
}