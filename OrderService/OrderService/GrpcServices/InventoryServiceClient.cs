using Grpc.Net.Client;
using InventoryService.Protos;
using Polly;
using Polly.Retry;
using System;

namespace OrderService.ServiceLayer.Services
{
    public class InventoryServiceClient
    {
        private static InventoryServiceClient _instance;
        private readonly Inventory.InventoryClient _client;
        private readonly AsyncRetryPolicy _retryPolicy;

        public InventoryServiceClient(IConfiguration configuration)
        {
            var channel = GrpcChannel.ForAddress(configuration.GetSection("Grpc:Server:url").Value);
            _client = new Inventory.InventoryClient(channel);
            _retryPolicy = Policy
                            .Handle<Exception>()
                            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static InventoryServiceClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("Instance not initialized. Call Initialize first.");
                }
                return _instance;
            }
        }

        public static void Initialize(IConfiguration configuration)
        {
            _instance = new InventoryServiceClient(configuration);
        }

        public async Task<bool> AddNewInventoryDetailsAsync(Guid productId, string productName, int quantity)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var reply = await _client.AddNewInventoryDetailsAsync(new InventoryAddModel()
                {
                    ProductId = productId.ToString(),
                    ProductName = productName,
                    Quantity = quantity,
                });
                return reply.Response;
            });
        }
        public async Task<InventoryGetResponseModel> GetInventoryDetailsByIdAsync(Guid id)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                var reply = await _client.GetInventoryDetailsByIdAsync(new InventoryGetModel()
                {
                    ProductId = id.ToString(),
                });
                return reply;
            });
        }
    }
}