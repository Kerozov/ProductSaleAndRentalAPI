using Application.Models.LocationModels.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers.Profiles;

public class LocationModel:Profile
{
    public LocationModel()
    {
        CreateMap<Location, GetLocationsDto>();
    }
}