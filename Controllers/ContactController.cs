using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Context;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Controllers;
public class ContactController : Controller
{
    private readonly RedioDbContext _context;

    public ContactController(RedioDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Index(Comment comment)
    {
        if (!ModelState.IsValid)
            return View();

        Comment comments = new()
        {
            Name = comment.Name,
            Comments = comment.Comments,
            Email = comment.Email,
        };
        await _context.Comments.AddAsync(comments);
        await _context.SaveChangesAsync();

        if (ModelState.IsValid)
        {
            TempData["Success"] = "Your comment sent successfully";
            return RedirectToAction("Index");
        }
        return View();

    }
}


