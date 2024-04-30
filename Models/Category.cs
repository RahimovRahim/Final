using System;
namespace FinalProject.Models;

public class Category
{
	public int Id { get; set; }
	public string Name { get; set; }
	public bool IsDeleted { get; set; }
	public ICollection<Product>? Products { get; set; }
	public string Image { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

