using System;

namespace Chestnut
{
    public class ContainerException : Exception
    {
        public ContainerException(string msg)
            : base(msg)
        {
        }

        public ContainerException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }

}
