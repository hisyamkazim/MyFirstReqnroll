using MyFirstReqnroll.Configurations.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyFirstReqnroll.Pages
{
    public class AccountOverviewPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly BaseUrlOptions _baseUrlOptions;


        public AccountOverviewPage(IWebDriver driver, BaseUrlOptions baseUrlOptions) : base(driver, baseUrlOptions)
        {
            _driver = driver;
            _wait = Wait;
            _baseUrlOptions = baseUrlOptions;
        }

        // ========================
        // Locators
        // ========================
        By WelcomeTextLocator => By.CssSelector("#leftPanel > p");
        By AccountsOverviewLocator => By.Id("accountTable");
        By HeaderPanelLocator => By.Id("headerPanel");
        By SidePanelLocator => By.CssSelector("#leftPanel > ul");
        By LogoutLinkLocator => By.LinkText("Log Out");

        private IWebElement WelcomeText => WaitAndFindElement(WelcomeTextLocator);
        private IWebElement AccountsOverview => WaitAndFindElement(AccountsOverviewLocator);
        private IWebElement HeaderPanel => WaitAndFindElement(HeaderPanelLocator);
        private IWebElement SidePanel => WaitAndFindElement(SidePanelLocator);
        private IWebElement LogoutLink => WaitAndFindElement(LogoutLinkLocator);

        // ========================
        // Actions (Fluent)
        // ========================

        public void Load()
        {
            Navigate(_baseUrlOptions.AccountOverview);
        }

        public string GetWelcomeMessage()
        {
            return WelcomeText.Text;
        }

        public bool IsWelcomeMessageDisplayed()
        {
            try
            {
                WaitForElementDisplayed(WelcomeTextLocator);
                return WelcomeText.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsAccountsOverviewDisplayed()
        {
            try
            {
                WaitForElementDisplayed(AccountsOverviewLocator);
                return AccountsOverview.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsLogoutLinkDisplayed()
        {
            try
            {
                WaitForElementDisplayed(LogoutLinkLocator);
                return LogoutLink.Displayed;
            }
            catch (Exception ex) when (ex is NoSuchElementException || ex is WebDriverTimeoutException)
            {
                return false;
            }
        }

    }
}