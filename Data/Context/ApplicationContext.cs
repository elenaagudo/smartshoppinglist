using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ApplicationContext : DbContext
    {   
        public DbSet<Printer> Printer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<ShoppingListProduct> ShoppingListProduct { get; set; }
        public DbSet<Supermarket> Supermarket { get; set; }
        // esto de momento no se va a usar para nada
        public DbSet<User> User { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
