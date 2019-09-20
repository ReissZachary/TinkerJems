using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkerJems.Web2.graphQL
{
    public class TinkerJemsSchema : Schema
    {
        public TinkerJemsSchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<TinkerJemsQuery>();
        }
        
    }
}
