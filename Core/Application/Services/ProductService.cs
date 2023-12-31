﻿using System.Net;
using Application.Helpers.Constants;
using Application.Models.ExceptionModel;
using Application.Models.ImageModels.Interface;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Interface;
using Application.Models.RentItemsModels.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly IOrderService _orderService;
    private readonly IImageRepository _imageRepository;
    private readonly IRentItemsService _rentItemsService;

    public ProductService(IProductRepository productRepository, IMapper mapper, IImageService imageService,
        IOrderService orderService, IImageRepository imageRepository, IRentItemsService rentItemsService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _imageService = imageService;
        _orderService = orderService;
        _imageRepository = imageRepository;
        _rentItemsService = rentItemsService;
        _imageService = imageService;
    }

    public async Task<List<GetProductsDto>> GetAllProductForSale()
    {
        var result = await _productRepository.GetAllProductForSale();

        foreach (var product in result)
        {
            if (product.img != null)
            {
                product.img = CreateURL(product.img);
            }
        }

        return result;
    }


    public async Task<ProductDetailsDto> GetById(int productId)
    {
        var entity = await _productRepository.GetById(productId);

        if (entity == null)
        {
            throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
        }

        var result = _productRepository.GetProductById(productId);
        return await result;
    }


    public async Task<int> AddProduct(AddProductDto productDto)
    {
        var product = await _productRepository.GetProductCode(productDto.Code, productDto.LocationId);
        if (product == null)
        {
            var quantity = productDto.QuantityForRent + productDto.QuantityForSale;
            if (quantity > productDto.Quantity)
            {
                throw new HttpException("Quantity for rent and quantity for sale is too big!", HttpStatusCode.BadRequest);
            }
            var productId = await _productRepository.Create(_mapper.Map<Product>(productDto));
            return productId;
        }
            throw new HttpException("Code exist!", HttpStatusCode.BadRequest);
        
    }


    public async Task DeleteProduct(int id)
    {
        var entity = await _productRepository.GetById(id);

        if (entity == null)
        {
            throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
        }

        var order = await _orderService.GetPendingOrderByProductId(id);
        if (order != null)
        {
            throw new HttpException("Product can't be delete, because order is not complete!",
                HttpStatusCode.BadRequest);
        }

        var rentItem = await _rentItemsService.GetItemByProductId(entity.Id);
        if (rentItem != null)
        {
            throw new HttpException("Product can't be delete, because have rent item!",
                HttpStatusCode.BadRequest);
        }
        await _imageService.DeleteImages(id);
        await _productRepository.Delete(id);
    }

    public async Task<ProductEditDto> EditProducts(int id, ProductEditDto product)
    {
        var entity = await _productRepository.GetById(id);
        if (entity == null)
        {
            throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
        }

        var p = await _productRepository.GetProductCode(product.Code, product.LocationId);
        if ((p != null && p.Id != id))
        {
            throw new HttpException("Code exist!", HttpStatusCode.BadRequest);
        }
        var quantity = product.QuantityForRent + product.QuantityForSale;
        if (quantity  > product.Quantity)
        {
            throw new HttpException("Quantity for rent and quantity for sale is too big!", HttpStatusCode.BadRequest);
        }
        var productForEdit = _mapper.Map<Product>(product);
        productForEdit.Id = id;
        await _productRepository.Update(productForEdit);
        return product;
    }

    private static string CreateURL(string url)
    {
        return CloudinaryConstants.baseUrl + url;
    }
}