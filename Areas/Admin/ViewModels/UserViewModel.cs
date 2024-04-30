using System;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.ViewModels;

	public class UserViewModel
	{
    public AppUser User { get; set; }
    public IList<string> Roles { get; set; } = null!;
}

