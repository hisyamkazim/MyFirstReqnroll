using MyFirstReqnroll.Configurations.Options;
using OpenQA.Selenium;
using Reqnroll;

namespace MyFirstReqnroll.Steps
{
    [Binding]
    public class BaseStep : IDisposable
    {
        protected IWebDriver _driver;
        protected readonly BaseUrlOptions _baseUrlOptions;
        public BaseStep(IWebDriver webDriver, BaseUrlOptions baseUrlOptions)
        {
            _baseUrlOptions = baseUrlOptions;
            _driver = webDriver;
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        // Common step definitions can be added here
    }
}