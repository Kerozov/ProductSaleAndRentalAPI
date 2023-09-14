using Application.Models.GenericRepository;
using Application.Models.ImageModels.Dtos;
using Domain.Entities;

namespace Application.Models.ImageModels.Interface;

public interface IImageRepository :IGenericRepository<Image>
{
    
    Task<GetImageDto> GetImageByProductId(int productId);
}