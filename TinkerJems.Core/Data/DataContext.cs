using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TinkerJems.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Models.JewelryItem> JewelryItem { get; set; }

        public DbSet<Models.TinkerJemsBlogPost> TinkerJemsBlogPost { get; set; }
    }
}
