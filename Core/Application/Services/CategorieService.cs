using System.Net;
using Application.Models.CategorieModels.Dtos;
using Application.Models.CategorieModels.Interfaces;
using Application.Models.ExceptionModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.Services;

public class CategorieService : ICategorieService
{
    private readonly ICategorieRepository _categorieRepository;
    private  readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly string categoryKey = "category";

   // private static ConnectionMultiplexer redis;

    private readonly IDatabase db;

    public CategorieService(ICategorieRepository categorieRepository, IConfiguration config, IMapper mapper)
    {
        _categorieRepository = categorieRepository;
        _config = config;
        _mapper = mapper;
      //  redis = ConnectionMultiplexer.Connect(_config.GetValue<string>("Redis:Connection"));
       // db = redis.GetDatabase();
    }

    public async Task<List<GetAllCategories>> GetCategories()
    {
        //Implementation with redis
        // await db.KeyDeleteAsync(categoryKey);
        // var value = await db.StringGetAsync(categoryKey);
        // if (!String.IsNullOrEmpty(value))
        // {
        //     return JsonSerializer.Deserialize<List<GetAllCategories>>(value);
        // }
        //
        //
        // var newValue =_mapper.Map<List<GetAllCategories>>( await _categorieRepository.GetAll());
        // await db.StringGetSetAsync(categoryKey, JsonSerializer.Serialize(newValue));
        return _mapper.Map<List<GetAllCategories>>( await _categorieRepository.GetAll());
    }

    public async Task AddCategorie(string name)
    {
        // var keyExist = db.KeyExists(categoryKey);
        // if (keyExist)
        // {
        //     await db.KeyDeleteAsync(categoryKey);
        // }

        var category = new Category
        {
            Name = name,
        };
        await _categorieRepository.Create(category);
    }

    public async Task DeleteCategory(int categoryId)
    {
       var entity = await _categorieRepository.GetById(categoryId);

       if (entity == null)
       {
           throw new HttpException($"Category Id not found!", HttpStatusCode.NotFound);
       }
        await _categorieRepository.Delete(categoryId);
    }
}