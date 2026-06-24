using MyFirstReqnroll.Configurations.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MyFirstReqnroll.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected BaseUrlOptions BaseUrlOptions;

        public string CurrentUrl => Driver.Url;

        public BasePage(IWebDriver driver, BaseUrlOptions baseUrlOptions)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            BaseUrlOptions = baseUrlOptions;
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void WaitForElementDisplayed(By locator)
        {
            Wait.Until(d => d.FindElement(locator).Displayed);
        }

        public void WaitForElementToDisappear(By locator)
        {
            Wait.Until(d =>
            {
                try
                {
                    return !d.FindElement(locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true; // Element is not present, so it's considered disappeared
                }
            });
        }

        public IWebElement WaitAndFindElement(By locator)
        {
            return Wait.Until(d => d.FindElement(locator));
        }
    }
}