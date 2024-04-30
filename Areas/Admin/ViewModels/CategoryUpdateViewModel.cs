using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.Controllers
{
	public class CategoryUpdateViewModel
	{
        [Required]
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}

