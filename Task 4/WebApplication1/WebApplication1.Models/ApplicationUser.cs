using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace WebApplication1.Models;
public class ApplicationUser : IdentityUser
{
    [DisplayName("Last Login Time")]
    public DateTime LastLoginTime { get; set; }
    [DisplayName("Registration Time")]
    public DateTime RegistrationTime { get; set; }
    public string Status { get; set; }
}