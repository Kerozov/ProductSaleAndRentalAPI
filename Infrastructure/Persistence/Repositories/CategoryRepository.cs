using Application.Models.CategorieModels.Interfaces;
using Application.Models.GenericRepository;
using Domain.Entities;

namespace Persistence.Repositories;

public class CategoryRepository :GenericRepository<Category>, ICategorieRepository
{
    public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}