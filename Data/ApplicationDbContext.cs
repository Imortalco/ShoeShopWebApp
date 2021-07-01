using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ShoeShopWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ShoeShopWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Shoe> Shoe { get; set; }
        public DbSet<Review> Review { get; set; }

    }
}
