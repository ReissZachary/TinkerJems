using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerJems.Wpf
{
    public interface IImagePicker
    {
        Task GetImageStreamAsync();
    }
}
