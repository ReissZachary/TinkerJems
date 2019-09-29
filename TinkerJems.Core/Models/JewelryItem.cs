using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TinkerJems.Core.Models
{
    
    public class JewelryItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public decimal Price { get; set; }

        public string LongDescription { get; set; }

        public string Description { get; set; }

    }
}
