using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyFirstReqnroll.Pages
{
    public class LandingPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LandingPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _wait = Wait;
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

        public LandingPage Load()
        {
            _driver.Navigate().GoToUrl("https://parabank.parasoft.com/parabank/index.htm");
            return this;
        }

        public LandingPage EnterUsername(string username)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);
            return this;
        }

        public LandingPage EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
            return this;
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }

        public LandingPage LoginAs(string username, string password)
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