using System;
using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Hakkında
    {
        [Key]
        public int Id { get; set; }
     
        public string Title { get; set; }
       
    }
}
