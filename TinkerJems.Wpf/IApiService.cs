using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerJems.Wpf
{
    public interface IApiService
    {
        Task<bool> UploadImageAsync(Stream image, string fileName);
    }
}
