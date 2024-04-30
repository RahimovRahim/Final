using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels
{
	public class CategoryViewModel
	{
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}

