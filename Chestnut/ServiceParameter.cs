using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chestnut
{
    class ServiceParameter : IInjectionParameter
    {
        public string id { get; set; }

        public ServiceParameter(string id)
        {
            this.id = id;
        }
    }
}
