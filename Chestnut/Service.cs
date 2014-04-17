using System;

namespace Chestnut
{
    public enum Scope{ Singleton, Request }
    public enum ResolveType { Eager, Lazy }
       
    public class Service
    {
        public Scope scope { get; set; }

        public Type type { get; set; }

        public ResolveType resolveType { get; set; }

        protected Array parameterInjectByConstructor;

        protected Array parameterInjectBySetter;

        public Service(Type type, Scope scope, ResolveType resolveType)
        {
            this.type = type;
            this.scope = scope;
            this.resolveType = resolveType;
        }

        public Service injectByConstructor(params object[] parameters)
        {
            this.parameterInjectByConstructor = parameters;
            return this;
        }

        public Service injectBySetter(params object[] parameters)
        {
            this.parameterInjectBySetter = parameters;
            return this;
        }
        
        public object CreateInstance()
        {
            object newInstance = null;

            // check if there are parameters for the constructor
            if (this.parameterInjectByConstructor != null && 
                this.parameterInjectByConstructor.Length > 0)
            {
                Type[] types = this.getParameterTypeList(this.parameterInjectByConstructor);
                object[] parameters = this.getParameterList(this.parameterInjectByConstructor);
                newInstance = this.type.GetConstructor(types).Invoke(parameters);
            }
            else
            {
                newInstance = Activator.CreateInstance(this.type);
            }

            // check if there are setters to call
            if (this.parameterInjectBySetter != null &&
                this.parameterInjectBySetter.Length > 0)
            {
                foreach (var parameter in parameterInjectBySetter)
                {
                    
                }
            }

            return newInstance;
        }

        private object[] getParameterList(Array array)
        {
            throw new NotImplementedException();
        }

        protected Type[] getParameterTypeList(Array parameters)
        {
            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                types[i] = parameters.GetValue(i).GetType();
            }

            return types;
        }
    }
}
