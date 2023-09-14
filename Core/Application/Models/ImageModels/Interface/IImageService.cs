using Application.Models.ImageModels.Dtos;

namespace Application.Models.ImageModels.Interface;

public interface IImageService
{
    Task DeleteImages(int productId);

   Task<ImageDto> UploadImage(int productId, AddImageDto image);

   Task<ImageDto> EditImage(int productId, AddImageDto image);
}