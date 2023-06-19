using System;
using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Rezervasyon
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TelefonNo{ get; set; }
        [Required]
        public int Sayi { get; set;}
        [Required]

        public string Saat{ get; set;}

        public DateTime Tarih { get; set;}


    }
}
