using System;
using FinalProject.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.ViewComponents
{

    public class ProductViewComponent : ViewComponent
    {
        private readonly RedioDbContext _context;

        public ProductViewComponent(RedioDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _context.Products.Where(p => !p.IsDeleted).Include(p => p.Category).ToListAsync();
            return View(products);
        }
    }
}

