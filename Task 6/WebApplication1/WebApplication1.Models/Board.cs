using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Board
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
