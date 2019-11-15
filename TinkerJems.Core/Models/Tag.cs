using System;
using System.Collections.Generic;
using System.Text;

namespace TinkerJems.Core.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<JewelryItemTag> TaggedItems { get; set; }
    }
}
