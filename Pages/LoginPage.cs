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
        private IWebElement UsernameInput => _wait.Until(d => d.FindElement(By.Name("username")));
        private IWebElement PasswordInput => _driver.FindElement(By.Name("password"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector("input[value='Log In']"));

        private IWebElement RegisterLink => _driver.FindElement(By.LinkText("Register"));
        private IWebElement ForgotLoginInfoLink => _driver.FindElement(By.LinkText("Forgot login info?"));

        // ========================
        // Actions (Fluent)
        // ========================

        public LoginPage Load()
        {
            Navigate(_baseUrlOptions.Login);
            return this;
        }

        public LoginPage EnterUsername(string username)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
            return this;
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }

        public LoginPage LoginAs(string username, string password)
        {
            return this
                .EnterUsername(username)
                .EnterPassword(password);
        }

        public void SubmitLogin(string username, string password)
        {
            LoginAs(username, password);
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
    }
}