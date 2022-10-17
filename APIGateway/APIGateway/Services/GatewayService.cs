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

        public async Task<CarDTO> GetCarWithDefects(int id)
        {
            var httpClient = _HttpClientFactory.CreateClient("CarsHttpClient");

            using(var response = await httpClient.GetAsync(httpClient.BaseAddress + $"Cars/Defects/{id}"))
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                if (!response.IsSuccessStatusCode)
                    return null;

                Console.WriteLine(responseContent);
                return JsonConvert.DeserializeObject<CarDTO>(responseContent);
            }
        }

        public async Task<bool> AddCar(CarWriteDTO car)
        {
            var httpClient = _HttpClientFactory.CreateClient("CarsHttpClient");
            var content = JsonContent.Create<CarWriteDTO>(car);
            Console.WriteLine(car);

            using (var response = await httpClient.PostAsync(httpClient.BaseAddress + "Cars", content))
            {
                return response.IsSuccessStatusCode;
            }
        }
    }
}
