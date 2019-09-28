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
        public DbSet<TinkerJems.Core.Models.JewelryItem> JewelryItems { get; set; }

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
