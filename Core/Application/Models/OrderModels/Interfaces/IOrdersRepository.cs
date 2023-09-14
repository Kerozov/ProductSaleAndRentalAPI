using Application.Models.GenericRepository;
using Application.Models.OrderModels.Dtos;
using Domain.Entities;

namespace Application.Models.OrderModels.Interfaces;

public interface IOrdersRepository :IGenericRepository<Order>
{
     Task<List<GetOrdersDto>> GetPendingsOrders();
     
     Task<List<GetOrdersDto>> GetMyOrders(string email);
     
     Task<GetOrderByProductIdDto> GetPendingOrderByProductId(int productId);
}