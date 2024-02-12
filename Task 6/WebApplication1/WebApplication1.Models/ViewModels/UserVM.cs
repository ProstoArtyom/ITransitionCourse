using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class UserVM
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Zа-яА-яёЁ0-9_]+$", ErrorMessage = "Only alphanumeric characters and underscores are allowed.")]
        public string Name { get; set; }
    }
}
