namespace Application.Models.OrderModels.Dtos;

public class CreateOrderDto
{
    public int Quantity { get; set; }
    
    public int ProductId { get; set; }

}