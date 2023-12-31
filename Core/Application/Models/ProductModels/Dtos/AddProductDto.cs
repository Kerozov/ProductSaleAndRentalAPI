﻿namespace Application.Models.ProductModels.Dtos;

public class AddProductDto
{
    public string Name { get; set; }

    public int Quantity { get; set; }

    public int QuantityForRent { get; set; }

    
    public string Description { get; set; }
    
    public string Code { get; set; }
    
    public int QuantityForSale { get; set; }

    public int CategoryId { get; set; }
    
    public int LocationId { get; set; }

    public decimal Price { get; set; }
}