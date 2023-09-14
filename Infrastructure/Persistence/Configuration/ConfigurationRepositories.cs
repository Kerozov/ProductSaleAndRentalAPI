using Application.Models.CategorieModels.Interfaces;
using Application.Models.GenericRepository;
using Application.Models.ImageModels.Interface;
using Application.Models.LocationModels.Interfaces;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Interface;
using Application.Models.RentItemsModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence.Configuration;

public static class ConfigurationRepositories
{
    public static IServiceCollection AddConfigurationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ICategorieRepository, CategoryRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IRentItemRepository, RentItemRepository>();

        return services;
    }
}