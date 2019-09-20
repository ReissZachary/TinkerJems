using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerJems.Web2.graphQL.Types
{
    public class ProductType : ObjectGraphType<TinkerJems.Core.Models.JewelryItem>
    {
        public ProductType()
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.ImageUrl);
            Field(t => t.ImageThumbnailUrl);
            Field(t => t.Price);
            Field(t => t.Description);
            Field(t => t.LongDescription);
        }
    }
}
