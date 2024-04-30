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
public class BlogController : Controller
{
    private readonly RedioDbContext _context;

    public BlogController(RedioDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var blog = await _context.Blogs.Where(b => !b.isDeleted).ToListAsync();
        return View(blog);
    }
    public async Task<IActionResult> BlogDetail(int id)
    {
        var blog = await _context.Blogs.Where(b => !b.isDeleted).Include(b => b.BlogTopic).ThenInclude(b => b.Topic).FirstOrDefaultAsync(b => b.Id == id);

        var topics = blog.BlogTopic.Select(t => t.Topic).Select(t => t.TopicName).ToArray();

        var blogList = await _context.Blogs
        .Where(b => !b.isDeleted)
        .Include(b => b.BlogTopic)
        .ThenInclude(b => b.Topic)
        .Where(b => b.Id != id && b.Topic.Any(t => topics.Contains(t.TopicName)))
        .ToListAsync();

        BlogPageViewModel model = new()
        {
            Blogs = blogList,
            Blog = blog

        };



        return View(model);

    }
}




