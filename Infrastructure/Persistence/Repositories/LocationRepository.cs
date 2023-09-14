using Application.Models.GenericRepository;
using Application.Models.LocationModels.Interfaces;
using Domain.Entities;

namespace Persistence.Repositories;

public class LocationRepository:GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}