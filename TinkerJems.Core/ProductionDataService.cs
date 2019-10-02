using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Core
{
    public class ProductionDataService : IDataService
    {
        const string imageUploadUrl = "https://localhost:5001/api/image";
        public async Task AddImageAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Unable to find file", filePath);
            }

            var client = new WebClient();
            var response = await client.UploadFileTaskAsync(new Uri(imageUploadUrl), filePath);
        }

        public void AddItemToDb(JewelryItem item)
        {

        }
    }
}
