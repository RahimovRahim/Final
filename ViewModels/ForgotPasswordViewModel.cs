using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels;

public class ForgotPasswordViewModel
{
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }

}

