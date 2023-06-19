using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace restaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Rezervasyon> Rezervasyons { get; set; }
        public DbSet<Galeri> Galeris { get; set; }
        public DbSet<Hakkında> Hakkındas { get; set; }
        public DbSet<Blog> Blogs { get;set;}
        public DbSet<Iletisim> Iletisims { get; set; }
        public DbSet<Iletisimim> Iletisimims { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
