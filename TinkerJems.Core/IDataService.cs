using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;

namespace TinkerJems.Core
{
    public interface IDataService
    {
        public Task AddImageAsync(string filePath);
        public void AddItemToDb(JewelryItem item);
    }
}
