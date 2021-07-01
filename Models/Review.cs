using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShopWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime PostDate { get; set; }
        [Display(Name = "Shoe")]
        public int ShoeId { get; set; }
        public virtual Shoe Shoe{get;set;}
        public string UserName{ get; set; }

        public Review()
        {
                
        }
    }
}
