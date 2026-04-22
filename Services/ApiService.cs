using RestSharp;
namespace MyFirstReqnroll.Services
{
    public class ApiService
    {
        private readonly RestClient _client;

        public ApiService()
        {
            // Base URL Parabank
            var options = new RestClientOptions("https://parabank.parasoft.com/parabank/services/bank");
            _client = new RestClient(options);
        }

        public async Task<RestResponse> CleanDatabaseAsync()
        {
            // Resource endpoint: cleanDB
            var request = new RestRequest("cleanDB", Method.Post);

            // Menambahkan header 'accept: application/xml' sesuai curl
            request.AddHeader("accept", "application/xml");

            // Mengirim request (body kosong sesuai -X POST di curl)
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> InitDatabaseAsync()
        {
            // Resource endpoint: initDB
            var request = new RestRequest("initDB", Method.Post);

            // Menambahkan header 'accept: application/xml' sesuai curl
            request.AddHeader("accept", "application/xml");

            // Mengirim request (body kosong sesuai -X POST di curl)
            return await _client.ExecuteAsync(request);
        }

    }
}
