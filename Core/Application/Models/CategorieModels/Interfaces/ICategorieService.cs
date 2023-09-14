using Application.Models.CategorieModels.Dtos;

namespace Application.Models.CategorieModels.Interfaces;

public interface ICategorieService
{
    Task<List<GetAllCategories>> GetCategories();
    
    Task AddCategorie(string name);

    Task DeleteCategory(int categoryId);
}