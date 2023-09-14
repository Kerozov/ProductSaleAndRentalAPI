using Application.Models.LocationModels.Dtos;

namespace Application.Models.LocationModels.Interfaces;

public interface ILocationService
{
    public  Task<List<GetLocationsDto>> GetLocations();
}