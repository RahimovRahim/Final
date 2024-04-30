using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
	public class SearchViewModel
	{
        [Required]
        public string SearchTerm { get; set; }
    }
}

