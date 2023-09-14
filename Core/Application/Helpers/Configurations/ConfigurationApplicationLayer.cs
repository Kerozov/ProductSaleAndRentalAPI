using FluentValidation;
using Application.Helpers.Profiles;
using Application.Helpers.Validators;
using Application.Models.CategorieModels.Interfaces;
using Application.Models.ImageModels.Interface;
using Application.Models.LocationModels.Interfaces;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Interface;
using Application.Models.RentItemsModels.Interfaces;
using Application.Services; 
using Microsoft.Extensions.DependencyInjection;

namespace Application.Helpers.Configurations;

public static class ConfigurationApplicationLayer
{
    public static IServiceCollection AddConfigurationApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductProfile).Assembly);
        services.AddAutoMapper(typeof(RentItemModel).Assembly);
        services.AddAutoMapper(typeof(LocationModel).Assembly);
        services.AddAutoMapper(typeof(OrderProfile).Assembly);
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrdersService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<ICategorieService, CategorieService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IRentItemsService, RentItemService>();


        
        services.AddScoped<IValidator<CreateOrderDto>, CreateOrderValidator>();
        services.AddScoped<IValidator<AddProductDto>, CreateProductValidator>();
        services.AddScoped<IValidator<ProductEditDto>, EditProductValidator>();
        return services;

    }
}