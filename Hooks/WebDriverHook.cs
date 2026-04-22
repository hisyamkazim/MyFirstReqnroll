using MyFirstReqnroll.Drivers;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;
using DriverOptions = MyFirstReqnroll.Configurations.Options.DriverOptions;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(10)]

namespace MyFirstReqnroll.Hooks
{
    [Binding]
    public class WebDriverHook
    {
        private readonly IObjectContainer _container;
        private readonly IReqnrollOutputHelper _reqnrollOutputHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly DriverOptions _driverOptions;

        private IWebDriver _webDriver;

        public WebDriverHook(
            IObjectContainer container,
            IReqnrollOutputHelper reqnrollOutputHelper,
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            DriverOptions driverOptions
            )
        {
            _container = container;
            _reqnrollOutputHelper = reqnrollOutputHelper;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _driverOptions = driverOptions;
        }

        [BeforeScenario]
        public void CreateWebDriver()
        {

            // Create WebDriver using BasePathOptions with updated Downloads path
            var driverFactory = new BrowserDriverFactory(_driverOptions);
            _webDriver = driverFactory.GetDefaultDriver();

            // Register the WebDriver in the container for dependency resolution
            _container.RegisterInstanceAs(_webDriver);

            _reqnrollOutputHelper.WriteLine("Starting scenario: " + _scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario(Order = 1)]
        public void DestroyWebDriver()
        {
            try
            {
                var error = _scenarioContext.TestError;
                if (error != null)
                {
                    _reqnrollOutputHelper.WriteLine($"An error ocurred! \n{error}");
                }

                if (_webDriver != null)
                {
                    try
                    {
                        _webDriver.Close(); // Close the current browser window
                        _webDriver.Quit();  // Close all browser windows and end the WebDriver session
                        _webDriver.Dispose();
                        _reqnrollOutputHelper.WriteLine("Browser closed");
                    }
                    catch (Exception closeEx)
                    {
                        // Log any cleanup-related exceptions
                        _reqnrollOutputHelper.WriteLine($"Error while closing WebDriver: {closeEx.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any general exceptions that might interrupt the AfterScenario completion
                _reqnrollOutputHelper.WriteLine($"Error in AfterScenario hook: {ex.Message}");
                throw new Exception($"Error in AfterScenario hook: {ex.Message}");  // Ensure the scenario fails with this exception
            }
            finally
            {
                // Ensure that the scenario completion is logged
                _reqnrollOutputHelper.WriteLine("End of scenario: " + _scenarioContext.ScenarioInfo.Title);
            }
        }
    }
}