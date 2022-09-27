using CarsServiceAPI.Protos;
using Grpc.Net.Client;

namespace CarsServiceAPI.Services
{
    public class GrpcClient
    {
        private static readonly string RPC_ADDRESS = "http://defects-service:85";
        public static async Task<DefectsRpcResponse> GetDefects(int carId)
        {
            try
            {
                var channel = GrpcChannel.ForAddress(RPC_ADDRESS);
                var client = new DefectsRpcService.DefectsRpcServiceClient(channel);
                Console.WriteLine("Client creation successfull... ");
                var request = new DefectsRpcRequest { CarId = carId };
                var defects = await client.GetDefectsForCarAsync(request);

                return defects;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
