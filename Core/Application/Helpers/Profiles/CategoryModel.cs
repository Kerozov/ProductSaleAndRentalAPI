using Application.Models.CategorieModels.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers.Profiles;

public class CategoryModel:Profile
{
    public CategoryModel()
    {
        CreateMap<Category, GetAllCategories>();
    }
}