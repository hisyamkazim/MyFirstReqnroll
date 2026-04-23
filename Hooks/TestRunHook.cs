using MyFirstReqnroll.Configurations.Options;
using MyFirstReqnroll.Drivers;
using MyFirstReqnroll.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using Reqnroll.BoDi;
using System;
using System.IO;
using System.Threading;
using DriverOptions = MyFirstReqnroll.Configurations.Options.DriverOptions;

namespace MyFirstReqnroll.Hooks
{
    [Binding]
    public class TestRunHooks
    {
        private static IObjectContainer _globalContainer;
        private static DriverOptions _driverOptions;
        private static IWebDriver _webDriver;
        private static BasePathOptions _basePathOptions;

        [BeforeTestRun]
        public static void GlobalSetup(IObjectContainer container)
        {
            _globalContainer = container;
            var configHook = new ConfigurationHook(_globalContainer);
            configHook.CreateConfig();

            _driverOptions = _globalContainer.Resolve<DriverOptions>();
            _basePathOptions = _globalContainer.Resolve<BasePathOptions>();

            var driverFactory = new BrowserDriverFactory(_driverOptions, _basePathOptions);
            _webDriver = driverFactory.GetDefaultDriver();

            if (_webDriver != null)
            {
                    _webDriver.Close(); 
                    _webDriver.Quit();  
                    _webDriver.Dispose();
            }
        }

        [AfterTestRun]
        public static void GlobalTeardown()
        {
            // Let BoDi handle the disposal of registered instances
            if (_globalContainer != null)
            {
                _globalContainer.Dispose();
            }

            _globalContainer = null;
        }
    }
}
