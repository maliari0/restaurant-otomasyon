using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Galeri
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
    }
}
