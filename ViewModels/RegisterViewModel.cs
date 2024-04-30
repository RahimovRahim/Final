﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Fullname { get; set; }
    [Required]
    public string Username { get; set; }
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    [Required, DataType(DataType.Password), Compare(nameof(Password))]
    public string PasswordConfirm { get; set; }
}

