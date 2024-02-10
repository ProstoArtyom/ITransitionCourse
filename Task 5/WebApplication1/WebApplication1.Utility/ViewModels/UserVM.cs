using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WebApplication1.Models;

namespace WebApplication1.Utility.ViewModels;

public class UserVM
{
    [ValidateNever]
    public IEnumerable<User> Users { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Region { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int Seed { get; set; }
}