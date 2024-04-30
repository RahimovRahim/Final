using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers;

public class ProductController : Controller
{
    private readonly RedioDbContext _context;

    public ProductController(RedioDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> ProductDetail(int id)
    {
        var products = await _context.Products.Where(p => p.IsStock).Include(p => p.Category).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);
        if (products == null)
        {
            return NotFound();
        }

        ProductDetailViewModel model = new()
        {
           Name=products.Name,
           Description=products.Description,
           DiscountPrice=products.DiscountPrice,
           Price=products.Price,
           Rating=products.Rating,
           Discount=products.Discount,
           CreatedDate=products.CreatedDate,
           UpdatedDate=products.UpdatedDate,
           CategoryName =products.Category.Name,
           BrandName=products.Brand.Name,
           Material=products.Material,
           ClaimedSize=products.ClaimedSize,
           RecommendedUse=products.RecommendedUse,
           Manufacturer=products.Manufacturer,
           SKU=products.SKU,
           Features=products.Features,

        };
        return View(model);
    }
}

