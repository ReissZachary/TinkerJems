using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinkerJems.Core.Models;

namespace TinkerJems.Web2.Data
{
    public class ApplicationDbContext : IdentityDbContext, IJewelryRepository
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TinkerJemsBlogPost>()
                .HasAlternateKey(p => p.Slug)
                .HasName("Unique_Slug");
            builder.Entity<JewelryItemTag>()
                .HasOne<JewelryItem>(t => t.JewelryItem)
                .WithMany(i => i.Tags)
                .HasForeignKey(t => t.JewelryItemId);

            builder.Entity<JewelryItemTag>()
                .HasOne<Tag>(t => t.Tag)
                .WithMany(t => t.TaggedItems)
                .HasForeignKey(t => t.TagId);
            
        }
        
        public DbSet<JewelryItem> JewelryItems { get; set; }

        public DbSet<TinkerJemsBlogPost> TinkerJemsBlogPost { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<JewelryItemTag> ItemTags { get; set; }

        public IEnumerable<JewelryItem> GetAllJewelryItems()
        {
            return JewelryItems;
        }

        public JewelryItem GetJewelryItemById(int jewelryItemId)
        {
            return JewelryItems.Find(jewelryItemId);
        }

        public async Task<JewelryItem> GetJewelryItemByNameAsync(string jewelryItemName)
        {
            return await JewelryItems.FirstOrDefaultAsync(j => j.Name == jewelryItemName);
        }
    }
}
