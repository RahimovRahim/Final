using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Areas.Admin.ViewModels;
using FinalProject.Context;
using FinalProject.Helpers.Extensions;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin,Moderator")]
public class ProductController : Controller
{
    private readonly RedioDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(RedioDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();


        return View(products);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        if (!ModelState.IsValid)
        {
            return View();
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel products)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        if (!ModelState.IsValid)
        {
            return View();
        }
        Product newproduct = new()
        {
            Name = products.Name,
            Price = products.Price,
            DiscountPrice = products.DiscountPrice,
            Rating = products.Rating,
            SKU = products.SKU,
            Description = products.Description,
            Features = products.Features,
            Material = products.Material,
            Manufacturer = products.Manufacturer,
            ClaimedSize = products.ClaimedSize,
            RecommendedUse = products.RecommendedUse,
            CategoryId = products.CategoryId,
            BrandId = products.BrandId,
            IsStock = true,
            IsDeleted = false

        };
        await _context.Products.AddAsync(newproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        return View(products);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        
        _context.Remove(products);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Detail(int id)
    {
        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        return View(products);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (products == null)
        {
            return NotFound();
        }
        ProductUpdateViewModel model = new()
        {
            Name = products.Name,
            Description = products.Description,
            DiscountPrice = products.DiscountPrice,
            Price = products.Price,
            Rating = products.Rating,
            Discount = products.Discount,
            CreatedDate = products.CreatedDate,
            UpdatedDate = products.UpdatedDate,
            CategoryName = products.Category.Name,
            BrandName = products.Brand.Name,
            Material = products.Material,
            ClaimedSize = products.ClaimedSize,
            RecommendedUse = products.RecommendedUse,
            Manufacturer = products.Manufacturer,
            SKU = products.SKU,
            Features = products.Features,
        };
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, ProductUpdateViewModel products)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        var updateProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (products == null)
        {
            return NotFound();
        }

        if (products.PosterImage != null)
        {
            if (products.PosterImage.CheckFileSize(3000))
            {
                ModelState.AddModelError("Image", "Image size is too big");
                return View();
            }

            if (!products.PosterImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Only images are allowed");
                return View();
            }

            string basePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "shop");
            string path = Path.Combine(basePath, updateProduct.PosterImage);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            string fileName = $"{Guid.NewGuid()}-{products.PosterImage.FileName}";
            path = Path.Combine(basePath, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await products.PosterImage.CopyToAsync(stream);
            }
            updateProduct.PosterImage = fileName;
        }

        updateProduct.Name = products.Name;
        updateProduct.Price = products.Price;
        updateProduct.DiscountPrice = products.DiscountPrice;
        updateProduct.Rating = products.Rating;
        updateProduct.SKU = products.SKU;
        updateProduct.Description = products.Description;
        updateProduct.Features = products.Features;
        updateProduct.Material = products.Material;
        updateProduct.Manufacturer = products.Manufacturer;
        updateProduct.ClaimedSize = products.ClaimedSize;
        updateProduct.RecommendedUse = products.RecommendedUse;
        updateProduct.CategoryId = products.CategoryId;
        updateProduct.BrandId = products.BrandId;


        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
