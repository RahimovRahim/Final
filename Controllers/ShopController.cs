using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers;

public class ShopController : Controller
{
    private readonly RedioDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    public ShopController(RedioDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {

        var products = await _context.Products.Where(p => !p.IsDeleted).Include(p => p.Category).ToListAsync();
        return View(products);

    }
    [HttpPost]
    public async Task<IActionResult> Search(SearchViewModel model)
    {
        if (model != null)
        {
            var searchTerm = model.SearchTerm.ToLower();
            var filteredProducts = await _context.Products.Where(p => p.Name.ToLower().Contains(searchTerm)).Where(p => !p.IsDeleted).Include(p => p.Category).ToListAsync();
            return View(filteredProducts);
        }
        else
        {
            return View(null);
        }
    }
    public async Task<IActionResult> LoadMore(int skip)
    {
        var products = await _context.Products.Where(p => !p.IsDeleted).Skip(skip).Take(3).Include(p => p.Category).ToListAsync();
        

        return PartialView("_ProductPartial", products);
    }
    [Authorize]
    public async Task<IActionResult> AddToBasket(int productId)
    {
        var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (products == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.ProductId == productId && b.AppUserId == user.Id);

        if (basketItem == null)
        {
            BasketItem newBasketItem = new()
            {
                ProductId = productId,
                AppUserId = user.Id,
                Count = 1,

            };

            await _context.BasketItems.AddAsync(newBasketItem);
        }
        else
        {
            basketItem.Count++;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

}




