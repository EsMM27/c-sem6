using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public float? Price { get; set; }
        public string? Image { get; set; }

        //forign key
        public int CategoryID { get; set; }

        public Category? Category { get; set; }

    }
}
