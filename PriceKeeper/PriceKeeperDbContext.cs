using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PriceKeeper
{
    public class PriceKeeperDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Measurement> Measurement { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-VAI9KCU\\SQLEXPRESS;Initial Catalog=PriceKeeperDB;Integrated Security=True");
        }
    }
}
