using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chestnut
{
    interface IServiceResolver
    {
        public IServiceResolver(Container container);
        public void Resolve(string id);
    }
}
