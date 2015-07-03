using System.Collections.Generic;

namespace LucAdm.Tests
{
    public abstract class ObjectBuilder<T>
        where T : class
    {
        protected T _instance { get; set; }

        public static implicit operator T(ObjectBuilder<T> builder)
        {
            return builder._instance;
        }

        public T Build()
        {
            return _instance;
        }

        public IList<T> ToList()
        {
            return new List<T> { Build() };
        }
    }
}