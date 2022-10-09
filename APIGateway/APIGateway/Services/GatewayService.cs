using APIGateway.DataClasses;
using APIGateway.IServices;
using Newtonsoft.Json;

namespace APIGateway.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IHttpClientFactory _HttpClientFactory;
        public GatewayService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }

        public async Task<List<CarDTO>> GetCars()
        {
            var httpClient = _HttpClientFactory.CreateClient("CarsHttpClient");

            using(var response = await httpClient.GetAsync(httpClient.BaseAddress + "Cars"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;

                Console.WriteLine(responseContent);
                return JsonConvert.DeserializeObject<List<CarDTO>>(responseContent);
            }
        }
    }
}
