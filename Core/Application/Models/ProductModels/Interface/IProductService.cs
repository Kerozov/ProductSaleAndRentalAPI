using Application.Models.ProductModels.Dtos;

namespace Application.Models.ProductModels.Interface;

public interface IProductService
{
    public Task<List<GetProductsDto>> GetAllProductForSale();

    Task<ProductDetailsDto> GetById(int productId);


    public Task<int> AddProduct(AddProductDto productDto);

    Task DeleteProduct(int id);
    
    Task<ProductEditDto> EditProducts(int id, ProductEditDto product);
}