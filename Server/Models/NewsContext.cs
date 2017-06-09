using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Author> Authors { get; set; }

        public NewsContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
