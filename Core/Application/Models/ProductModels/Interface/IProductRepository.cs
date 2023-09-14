using Application.Models.GenericRepository;
using Application.Models.ProductModels.Dtos;
using Domain.Entities;

namespace Application.Models.ProductModels.Interface;

public interface IProductRepository :IGenericRepository<Product>
{
    Task<List<GetProductsDto>> GetAllProductForSale();
    Task<ProductDetailsDto> GetProductById(int productId);

    public Task<Product> GetProductCode(string code, int locationId);
}