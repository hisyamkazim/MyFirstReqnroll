using MyFirstReqnroll.Drivers;
using OpenQA.Selenium;
using Reqnroll;

namespace MyFirstReqnroll.StepDefinitions
{
    [Binding]
    public class BaseStep : IDisposable
    {
        protected readonly IWebDriver webDriver;
        public BaseStep()
        {            
            webDriver = WebDriverFactory.CreateDriver();
        }

        public void Dispose()
        {
            webDriver.Quit();
            webDriver.Dispose();
        }

        // Common step definitions can be added here
    }
}