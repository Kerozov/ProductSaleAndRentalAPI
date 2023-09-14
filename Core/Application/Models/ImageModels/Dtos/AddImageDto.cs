using Microsoft.AspNetCore.Http;

namespace Application.Models.ImageModels.Dtos;

public class AddImageDto
{
    public IFormFile Image { get; set; }
}