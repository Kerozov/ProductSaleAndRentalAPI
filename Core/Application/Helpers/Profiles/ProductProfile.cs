using Application.Models.ProductModels.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers.Profiles;

public class ProductProfile :Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductDto, Product>();
        CreateMap<ProductEditDto, Product>();

    }
}