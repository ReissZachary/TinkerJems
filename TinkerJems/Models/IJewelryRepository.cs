using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkerJems.Models
{
    public interface IJewelryRepository
    {
        IEnumerable<JewelryItem> GetAllJewelryItems();
        JewelryItem GetJewelryItemById(int jewelryItemId);
    }
}
