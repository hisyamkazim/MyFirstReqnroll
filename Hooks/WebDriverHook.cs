using MyFirstReqnroll.Drivers;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;
using DriverOptions = MyFirstReqnroll.Configurations.Options.DriverOptions;
using BasePathOptions = MyFirstReqnroll.Configurations.Options.BasePathOptions;
using MyFirstReqnroll.Helpers;
using Allure.Net.Commons;
using System.Text;
using System.Text.RegularExpressions;

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
        private readonly BasePathOptions _basePathOptions;
        private IWebDriver _webDriver;

        public WebDriverHook(
            IObjectContainer container,
            IReqnrollOutputHelper reqnrollOutputHelper,
            ScenarioContext scenarioContext,
            FeatureContext featureContext,
            DriverOptions driverOptions,
            BasePathOptions basePathOptions
            )
        {
            _container = container;
            _reqnrollOutputHelper = reqnrollOutputHelper;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _driverOptions = driverOptions;
            _basePathOptions = basePathOptions;
        }

        [BeforeScenario]
        public void CreateWebDriver()
        {
            // Create WebDriver using BasePathOptions with updated Downloads path
            var driverFactory = new BrowserDriverFactory(_driverOptions, _basePathOptions);
            _webDriver = driverFactory.GetDefaultDriver();

            // Register the WebDriver in the container for dependency resolution
            _container.RegisterInstanceAs(_webDriver);

            _reqnrollOutputHelper.WriteLine("Starting scenario: " + _scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep(Order = 1)]
        public void TakeScreenshotAndGetCurrentUrlAfterEachStep()
        {
            AllureApi.Step("Details", () =>
            {
                TakeScreenshotForEveryStep();
                GetCurrentURL();
            });
        }

        public void GetCurrentURL()
        {
            //Add current URL to the report on livingdoc and allure
            try
            {
                string currentURL = _webDriver.Url;
                _reqnrollOutputHelper.WriteLine($"Current URL: " + currentURL);

                var status = _scenarioContext.ScenarioExecutionStatus;

                //Add the current URL as a link to the Allure report
                if (status == ScenarioExecutionStatus.TestError ||
                    status == ScenarioExecutionStatus.BindingError ||
                    status == ScenarioExecutionStatus.UndefinedStep)
                {
                    AllureApi.AddLink($"Current URL: {currentURL}", currentURL);
                }
                AllureApi.AddAttachment("Link", "text/plain", Encoding.UTF8.GetBytes(currentURL));
            }
            catch (Exception)
            {
            }
        }

        public void TakeScreenshotForEveryStep(string screenshotName = "")
        {
            if (_webDriver != null)
            {
                try
                {
                    var wibTime = GetWIBTime();
                    var folderPath = BuildScreenshotFolderPath(wibTime);
                    var timestamp = wibTime.ToString("HH_mm_ss_fff"); //Hour_Minute_Second_Milliseconds
                    var stepName = string.IsNullOrEmpty(screenshotName) ? _scenarioContext.StepContext.StepInfo.Text : screenshotName; // if screenshotName is empty then use StepInfo.Text
                    var baseFilename = $"{timestamp}_{stepName}";
                    string sanitizedFilename = SanitizeAndTrim(baseFilename, CalculateMaxFilenameLength(folderPath));
                    var filePath = Path.Combine(folderPath, sanitizedFilename + ".png");

                    var screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();
                    screenshot.SaveAsFile(filePath);

                    // Add the screenshot to the Allure report
                    AllureApi.AddAttachment(baseFilename, "image/png", filePath);
                    _reqnrollOutputHelper.AddAttachment(filePath);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error capturing screenshot: {ex.Message}");
                }
            }
        }

        private DateTime GetWIBTime()
        {
            var wibZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, wibZone);
        }

        private string BuildScreenshotFolderPath(DateTime wibTime)
        {
            var todayDate = wibTime.ToString("yyyy-MM-dd") + "/";
            var featureName = SanitizeAndTrim(_featureContext.FeatureInfo.Title, 30);
            var scenarioName = SanitizeAndTrim(_scenarioContext.ScenarioInfo.Title, 30);

            var directory = Path.Combine(_basePathOptions.Screenshots, featureName, scenarioName, todayDate);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }

        private int CalculateMaxFilenameLength(string folderPath)
        {
            var fullFolderPath = Path.GetFullPath(folderPath);
            return 250 - fullFolderPath.Length;
        }

        private string SanitizeAndTrim(string input, int maxFilenameLength)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var sanitized = Regex.Replace(input, @"[^\w]+", "_");
            sanitized = Regex.Replace(sanitized, @"_+", "_").Trim('_');
            return sanitized.Length > maxFilenameLength ? sanitized.Substring(0, maxFilenameLength) : sanitized;
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