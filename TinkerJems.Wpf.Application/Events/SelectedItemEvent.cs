using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TinkerJems.Core.Models;

namespace TinkerJems.Wpf.Application.Events
{
    public class SelectedItemEvent : PubSubEvent<JewelryItem>
    {
    }
}
