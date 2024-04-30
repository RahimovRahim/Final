using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels;

public class BrandUpdateViewModel
{
    [Required]
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
}

