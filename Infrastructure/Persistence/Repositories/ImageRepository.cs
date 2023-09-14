using Application.Models.GenericRepository;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interface;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class ImageRepository :GenericRepository<Image>, IImageRepository
{
    
    public ImageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async  Task<GetImageDto> GetImageByProductId(int productId)
    {
        var query = @"SELECT i.Id, i.img   FROM Product AS p   JOIN Image AS i on p.Id = i.ProductId WHERE p.Id= @productId";
        var result =  await Connection.QueryFirstOrDefaultAsync<GetImageDto>(query, new {productId}, Transaction);
        return  result;
    }
}