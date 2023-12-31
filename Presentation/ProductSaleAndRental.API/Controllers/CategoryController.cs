﻿
using Application.Models.CategorieModels.Dtos;
using Application.Models.CategorieModels.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/Category")]
[ApiController]
public class CategoryController: ControllerBase
{
    private readonly ICategorieService _categorieService;

    public CategoryController(ICategorieService categorieService)
    {
        _categorieService = categorieService;
    }
    [HttpGet]
    [Route("All")]
    public async Task<List<GetAllCategories>> GetAllCategories()
    {
        return await _categorieService.GetCategories();
    }

    [HttpPost]
    [Route("Add/{name}")]
    public async Task AddCategorie(string name)
    {
        await _categorieService.AddCategorie(name);
    }

    [HttpDelete]
    [Route("Delete/{categoryId}")]

    public async Task DeleteCategory(int categoryId)
    {
        await _categorieService.DeleteCategory(categoryId);
    }
}