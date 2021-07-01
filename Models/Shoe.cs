using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShopWebApp.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }

        [Display(Name="Reviews")]
        [InverseProperty("Shoe")]
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public Shoe()
        {
            
        }
    }
}
