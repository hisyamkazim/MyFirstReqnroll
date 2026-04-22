using MyFirstReqnroll.Services;

namespace MyFirstReqnroll.Hooks
{
    [Binding]
    public class CleanUpDatabaseHook
    {
        private readonly ApiService _apiService;

        public CleanUpDatabaseHook()
        {
            _apiService = new ApiService();
        }

        [BeforeScenario("CleanUpDatabase")]
        [AfterScenario("CleanUpDatabase")]
        public async Task CleanUpSpecificScenario()
        {
            await _apiService.CleanDatabaseAsync();
        }

    }
}
