using MyFirstReqnroll.Configurations.Options;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using Reqnroll.BoDi;

namespace MyFirstReqnroll.Hooks
{
    [Binding]
    public class ConfigurationHook
    {
        private readonly IObjectContainer _container;
        private static IConfiguration _config;
        private static DriverOptions _driverOptions;
        private static BaseUrlOptions _baseUrlOptions;
        
        public ConfigurationHook(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario(Order = 1)]
        public void CreateConfig()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                _driverOptions = new DriverOptions();
                _config.GetSection(DriverOptions.Key).Bind(_driverOptions);
                _baseUrlOptions = new BaseUrlOptions();
                _config.GetSection(BaseUrlOptions.Key).Bind(_baseUrlOptions);
            }

            _container.RegisterInstanceAs(_config);
            _container.RegisterInstanceAs(_driverOptions);
            _container.RegisterInstanceAs(_baseUrlOptions);
        }

    }

}