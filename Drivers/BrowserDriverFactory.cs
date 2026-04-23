using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DriverOptions = MyFirstReqnroll.Configurations.Options.DriverOptions;
using BasePathOptions = MyFirstReqnroll.Configurations.Options.BasePathOptions;

namespace MyFirstReqnroll.Drivers
{
    public class BrowserDriverFactory
    {
        private readonly DriverOptions _driverOptions;
        private readonly BasePathOptions _basePathOptions;

        public BrowserDriverFactory(DriverOptions driverOptions, BasePathOptions basePathOptions)
        {
            _driverOptions = driverOptions;
            _basePathOptions = basePathOptions;
        }

        public IWebDriver GetDefaultDriver()
        {

            return GetByName(_driverOptions.Name);
        }

        public IWebDriver GetByName(string name)
        {
            switch (name.ToLower())
            {
                case "chrome":
                    return GetChromeDriver();
                default:
                    throw new ArgumentException($"Unsupported browser: {name}");
            }
        }

        private IWebDriver GetChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            if (_driverOptions.Headless)
            {
                options.AddArgument("--headless");
            }
            foreach (var arg in _driverOptions.arguments)
            {
                options.AddArgument(arg);
            }
            var driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_driverOptions.ImplicitWaitSeconds);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_driverOptions.PageLoadTimeoutSeconds);
            return driver;
        }
    }
}
