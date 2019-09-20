using GraphQL.Instrumentation;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkerJems.Core.Models;
using TinkerJems.Web2.Data;
using TinkerJems.Web2.graphQL.Types;

namespace TinkerJems.Web2.graphQL
{
    public class TinkerJemsQuery : ObjectGraphType
    {
        public TinkerJemsQuery(ApplicationDbContext context)
        {
            Field<ListGraphType<ProductType>>(
            "products",
            resolve: c => context.JewelryItems); ;

        } 


    }
}
