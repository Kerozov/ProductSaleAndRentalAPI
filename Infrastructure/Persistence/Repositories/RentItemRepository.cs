﻿using Application.Models.GenericRepository;
using Application.Models.RentItemsModels.Dtos;
using Application.Models.RentItemsModels.Interfaces;
using Dapper;
using Domain.Entities;

namespace Persistence.Repositories;

public class RentItemRepository : GenericRepository<RentItems>, IRentItemRepository
{
    public RentItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<List<RentItems>> GetAllItemsForRentByEmail()
    {
        var query = @"SELECT *  FROM RentItems";
        var result = await Connection.QueryAsync<RentItems>(query,null, Transaction);
        return (List<RentItems>)result;
    }

    public async Task<List<GetMyItems>> GetMyItems(string email)
    {
        string query = "SELECT Quantity, OrderDate ,EndDate, Code, Name FROM RentItems WHERE Email = @email";
        return (List<GetMyItems>)await Connection.QueryAsync<GetMyItems>(query,new {email },Transaction);
    }

    public async Task<RentItems> GetItemByProductId(int productId)
    {
        string query = @"SELECT * FROM RentItems WHERE @productId = ProductId AND EndDate IS NULL";
        return await Connection.QueryFirstOrDefaultAsync<RentItems>(query, new { productId }, Transaction);
    }
}