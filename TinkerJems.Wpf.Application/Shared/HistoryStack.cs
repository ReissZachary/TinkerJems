using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;

namespace TinkerJems.Wpf.Application.Shared
{
    public static class HistoryStack
    {
        public static Stack<History> ViewStack = new Stack<History>();
    }

    public class History
    {
        public string PageName { get; set; }
        public JewelryItem Item { get; set; }
        public string Category { get; set; }
    }
}
