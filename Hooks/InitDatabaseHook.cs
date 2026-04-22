using MyFirstReqnroll.Services;

namespace MyFirstReqnroll.Hooks
{
    [Binding]
    public class InitDatabaseHook
    {
        private readonly ApiService _apiService;

        public InitDatabaseHook()
        {
            _apiService = new ApiService();
        }

        [BeforeScenario("InitDatabase")]
        public async Task InitSpecificScenario()
        {
            await _apiService.InitDatabaseAsync();
        }

    }
}
