using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProductSaleAndRental.API.Controllers;

[Route("api/Location")]
[ApiController]
public  class LocationController :ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<List<GetLocationsDto>> GetLocations()
    {
      return await _locationService.GetLocations();
    }
}