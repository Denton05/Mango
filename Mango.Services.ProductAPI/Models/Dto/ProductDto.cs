﻿namespace Mango.Services.ProductAPI.Models.Dto;

public class ProductDto
{
    #region Properties

    public string CategoryName { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public int ProductId { get; set; }

    #endregion
}
