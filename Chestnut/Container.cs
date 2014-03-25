using System;
using System.Collections.Generic;

namespace Chestnut
{
    using System.Linq;

    public class Container
    {
        private IDictionary<string, Service> container;

        private IDictionary<string, object> instances;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container()
        {
            this.container = new Dictionary<string, Service>();
            this.instances = new Dictionary<string, object>();
        }

        public void Register(string id, Service service)
        {
            if (!this.Has(id))
            {
                this.container.Add(id, service);
            }
            else
            {
                throw new ContainerException(string.Format("The container already contains the service '{0}'", id));
            }
        }

        /// <summary>
        /// Checks if a given Service ID is available in the container
        /// </summary>
        /// <param name="id">
        /// The Service-ID
        /// </param>
        /// <returns>
        /// Returns true if a service with the given ID was found
        /// </returns>
        public bool Has(string id)
        {
            return this.container.ContainsKey(id);
        }

        /// <summary>
        /// Returns the count of registered services
        /// </summary>
        /// <returns>
        /// The count of registered services
        /// </returns>
        public int Count()
        {
            return this.container.Count;
        }

        /// <summary>
        /// Removes Service from container
        /// </summary>
        /// <param name="id">
        /// The ID of the Service to remove
        /// </param>
        /// <returns>
        /// True if the service was removed successfully otherwise false
        /// </returns>
        public bool Remove(string id)
        {
            return this.container.Remove(id);
        }

        public object Resolve(string id)
        {
            if (!this.Has(id))
            {
                throw new ContainerException(string.Format("The container doesnt contain the service '{0}'", id)); 
            }

            Service service = this.container[id];
            if (service.scope == Scope.Request)
            {
                return service.CreateInstance();
            }
            else
            {
                if (this.instances.ContainsKey(id))
                {
                    object availableInstance = this.instances[id];
                    if (availableInstance != null)
                    {
                        return availableInstance;
                    }
                }

                object newInstance = service.CreateInstance();
                this.instances.Add(id, newInstance);
                return newInstance;
            }    
        }

        /// <summary>
        /// Can be optionally called.
        /// Pre-instantiates eager loading services in container.
        /// </summary>
        public void StartUp()
        {
            foreach (KeyValuePair<string, Service> item in this.container.Where(item => item.Value.resolveType == ResolveType.Eager))
            {
                this.Resolve(item.Key);
            }
        }
    }
}