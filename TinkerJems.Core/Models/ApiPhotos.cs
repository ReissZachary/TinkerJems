using System;
using System.Collections.Generic;
using System.Text;

namespace TinkerJems.Core.Models
{
    public class ApiPhotos
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string LargeUrl { get; set; }
        public int SourceId { get; set; }
    }
}
