using Application.Models.GenericRepository;
using Application.Models.RentItemsModels.Dtos;
using Domain.Entities;

namespace Application.Models.RentItemsModels.Interfaces;

public interface IRentItemRepository :IGenericRepository<RentItems>
{
  public Task<List<RentItems>> GetAllItemsForRentByEmail();

  public Task<List<GetMyItems>> GetMyItems(string email);

  public Task<RentItems> GetItemByProductId(int productId);
}