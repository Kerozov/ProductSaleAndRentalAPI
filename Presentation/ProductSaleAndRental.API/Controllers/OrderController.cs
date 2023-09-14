using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductSaleAndRental.API.Controllers;

[Route("api/Orders")]
[ApiController]
public class OrderController :ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IValidator<CreateOrderDto> _createOrderValidator;


    public OrderController(IOrderService orderService, IValidator<CreateOrderDto> createOrderValidator)
    {
        _orderService = orderService;
        _createOrderValidator = createOrderValidator;
    }

    [HttpGet]
    [Route("Pending")]
    public async Task<List<GetOrdersDto>> GetPendingOrders()
    {
        return await _orderService.GetPendingsOrders();
    }
    [HttpGet]
    [Route("My-Orders")]
    public async Task<List<GetOrdersDto>> GetMyOrders()
    {
        string email =  this.User.Claims.First(claim => claim.Type == "preferred_username").Value;

        return await _orderService.GetMyOrders(email);
    }

    [HttpPost]
    [Route("Add")]
    
    public async Task CreateOrder(CreateOrderDto dto)
    {
        await _createOrderValidator.ValidateAndThrowAsync(dto); 
      string email =  this.User.Claims.First(claim => claim.Type == "preferred_username").Value;
      await  _orderService.CreateOrder(dto, email);
    }

    [HttpPut]
    [Route("Orders/Status/{id}")]
    public async Task ChangeStatus(int id)
    {
       await _orderService.ChangeStatus(id);
    }
    
    [HttpDelete]
    [Route("Reject/{id}")]
    public async Task RejectOrder(int id)
    {
       await _orderService.RejectOrder( id);
    }
}