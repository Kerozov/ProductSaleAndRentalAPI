﻿using System.Data.SqlClient;
using Application.Models.GenericRepository;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Persistence.Repositories;

public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
{
    public OrdersRepository(IUnitOfWork unitOfWork) :base(unitOfWork)
    {
    }

    public async Task<List<GetOrdersDto>> GetPendingsOrders()
    {

        var query =
            "SELECT [Order].Code, [Order].Price, [Order].Quantity, OrderDate, [Order].Status, [Order].Id, [Order].Email  FROM [Order]  Where [Order].Status = '0'";

        var result = await Connection.QueryAsync<GetOrdersDto>(query,null,Transaction);
        return  (List<GetOrdersDto>)result;
    }

    public async Task<List<GetOrdersDto>> GetMyOrders(string email)
    {
        var query = @"SELECT * FROM [Order]  Where Email= @email";

        var result = await Connection.QueryAsync<GetOrdersDto>(query, new{email}, Transaction);
        return  (List<GetOrdersDto>)result;
    }

    public async Task<GetOrderByProductIdDto> GetPendingOrderByProductId(int productId)
    {
        var query = @"SELECT Id, Quantity, Status, OrderDate, ProductId, Email, Code, Price, Name FROM [Order] WHERE ProductId = @productId AND Status = '0'";

        var result = await Connection.QueryFirstOrDefaultAsync<GetOrderByProductIdDto>(query,new{ productId},Transaction);

        return result;
    }
}