using Grpc.Core;
using InventoryService.Protos;
using InventoryService.RepositoryService.Interface;

namespace InventoryService.Services
{
    public class InventoryGrpcService:Inventory.InventoryBase
    {
        private readonly ILogger<InventoryGrpcService> _logger;
        private readonly IInvertoryRepository _invertoryRepository;

        public InventoryGrpcService(ILogger<InventoryGrpcService> logger, IInvertoryRepository invertoryRepository)
        {
            _logger = logger;
            _invertoryRepository = invertoryRepository;
        }

        public override async Task<InventoryAddResponseModel> AddNewInventoryDetails(InventoryAddModel request, ServerCallContext context)
        {
            InventoryAddResponseModel response = new InventoryAddResponseModel();
            try
            {
                var data= await _invertoryRepository.Add(new Domain.Inventory()
                {
                    InventoryId=Guid.NewGuid(),
                    ProductId=Guid.Parse(request.ProductId),
                    ProductName=request.ProductName,
                    Quantity=request.Quantity,
                });
                await _invertoryRepository.Save();
                response.Response = true;
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Response=false;
            }
            return await Task.FromResult(response);
        }

        public override async Task<InventoryGetResponseModel> GetInventoryDetailsById(InventoryGetModel request, ServerCallContext context)
        {
            var response = new InventoryGetResponseModel();
            var res = await _invertoryRepository.GetInventoryDetailsByProductId(Guid.Parse(request.ProductId));
            if (request.ProductId != null && res != null)
            {
                response.ProductId = request.ProductId;
                response.Quantity = res.Quantity;
            }
            return await Task.FromResult(response);
        }
    }
}
