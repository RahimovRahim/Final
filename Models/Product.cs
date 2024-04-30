using System;
namespace FinalProject.Models;

public class Product
{
	public int Id{ get; set; }
    public double SKU { get; set; }
    public string Features { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double DiscountPrice { get; set; }
    public double Price { get; set; }
    public int Rating { get; set; }
    public bool IsDeleted { get; set; }
    public int Discount { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsStock { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public ICollection<ProductImage>? Images { get; set; }
    public string Material { get; set; }
    public string ClaimedSize { get; set; }
    public string RecommendedUse { get; set; }
    public string Manufacturer { get; set; }
    public string PosterImage { get; set; }

}

