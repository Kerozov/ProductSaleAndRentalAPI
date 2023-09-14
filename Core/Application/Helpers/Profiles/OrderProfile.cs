using Application.Models.OrderModels.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Helpers.Profiles;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
    CreateMap<CreateOrderDto, Order>();
    CreateMap<Order, GetOrdersDto>();
    }

}