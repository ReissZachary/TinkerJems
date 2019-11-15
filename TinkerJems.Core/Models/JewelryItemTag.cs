using System;
using System.Collections.Generic;
using System.Text;

namespace TinkerJems.Core.Models
{
    public class JewelryItemTag
    {
        public int Id { get; set; }
        public int JewelryItemId { get; set; }
        public int TagId { get; set; }        
        public JewelryItem JewelryItem { get; set; }
        public Tag  Tag { get; set; }
    }
}
