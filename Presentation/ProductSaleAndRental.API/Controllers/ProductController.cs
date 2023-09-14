using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductSaleAndRental.API.Controllers;

[Route("api/Products")]
[ApiController]
public class ProductController : ControllerBase
{
   private readonly IProductService _productService;
   private readonly IValidator<AddProductDto> _productValidator;
   private readonly IValidator<ProductEditDto> _editValidator;

   public   ProductController(IProductService productService, IValidator<AddProductDto> productValidator, IValidator<ProductEditDto> editValidator)
   {
      _productService = productService;
      _productValidator = productValidator;
      _editValidator = editValidator;
   }
   
   [HttpGet]
   [Route("All")]

   public async Task<List<GetProductsDto>> GetAllProducts()
   {
      return await _productService.GetAllProductForSale();
   }
   
   [HttpGet ("{productId}")]
   public async Task<ProductDetailsDto> ProductDetails(int productId)
   {
       return await _productService.GetById(productId);
   }


   [HttpPost]
   [Route("Inventory/Add")]
   public async Task<int> AddProduct(AddProductDto productDto)
   {
      await  _productValidator.ValidateAndThrowAsync(productDto);
      return await _productService.AddProduct(productDto);
   }


   [HttpPut]
   [Route("Edit/{id}")]

   public async Task<ProductEditDto>  EditProducts(int id, ProductEditDto product)
   {
      await _editValidator.ValidateAndThrowAsync(product);
    return  await _productService.EditProducts(id, product);
      
   }
   
   [HttpDelete]
   [Route("Inventory/Delete/{id}")]
   public async Task DeleteProduct(int id)
   {
      await _productService.DeleteProduct(id);
   }
}