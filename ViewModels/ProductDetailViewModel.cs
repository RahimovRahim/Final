using System;
using FinalProject.Models;

namespace FinalProject.ViewModels;

public class ProductDetailViewModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double DiscountPrice { get; set; }
    public double Price { get; set; }
    public int Rating { get; set; }
    public double SKU { get; set; }
    public string Features { get; set; } = null!;
    public int Discount { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsStocked { get; set; }
   
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public int BrandId { get; set; }
    public string BrandName { get; set; } = null!;
    public ICollection<ProductImage>? Images { get; set; }
    public string Material { get; set; } = null!;
    public string ClaimedSize { get; set; } = null!;
    public string RecommendedUse { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
}

