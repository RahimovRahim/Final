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
public class HomeController : Controller
{
    private readonly RedioDbContext _context;

    public HomeController(RedioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Where(p => p.IsStock).Where(p => !p.IsDeleted).Include(p => p.Category).ToListAsync();
        var categories = await _context.Categories.Where(p => !p.IsDeleted).Take(4).ToListAsync();
        HomePageViewModel model = new()
        {
            Products = products,
            Categories = categories

        };
        return View(model);
    }
}




