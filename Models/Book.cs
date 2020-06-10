using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Book
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string planId { get; set; }
        public string name { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public double price { get; set; }

    }
}
