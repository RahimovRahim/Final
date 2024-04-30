using System;
using FinalProject.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.ViewComponents;
public class BlogViewComponent : ViewComponent
{

    private readonly RedioDbContext _context;

    public BlogViewComponent(RedioDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var blog = await _context.Blogs.Where(b => !b.isDeleted).Take(3).ToListAsync();
        return View(blog);
    }

}
