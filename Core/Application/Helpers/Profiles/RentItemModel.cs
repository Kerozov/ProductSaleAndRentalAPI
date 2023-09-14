using Application.Models.RentItemsModels.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers.Profiles;

public class RentItemModel:Profile
{
    public RentItemModel()
    {
        CreateMap<AddItemForRentDto, RentItems>();
        CreateMap<RentItems, RentItemsDto>();
    }   
}