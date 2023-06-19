using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Category
    {
        [Key] public int Id { get; set; } //ID attribute ile benzersiz yaptık
        [Required] public string Name { get; set; } //name boş geçilmemesi için attribute tanımladık
    }
}
