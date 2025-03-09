using System.ComponentModel.DataAnnotations;

namespace RP1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Range(0, 100, ErrorMessage = "Dispaly Order Must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        public List<Product>? Products { get; set; } = new();
    }
}
