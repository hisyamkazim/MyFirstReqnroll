using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MyFirstReqnroll.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void WaitForElement(By locator)
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
    }
}