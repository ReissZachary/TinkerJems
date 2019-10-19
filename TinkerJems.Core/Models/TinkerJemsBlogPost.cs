using System;
using System.Collections.Generic;
using System.Text;

namespace TinkerJems.Core.Models
{
     public class TinkerJemsBlogPost
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Body { get; set; }

        public string Image { get; set; }

        public DateTime Posted { get; set; }

        public string Author { get; set; }
    }
}
