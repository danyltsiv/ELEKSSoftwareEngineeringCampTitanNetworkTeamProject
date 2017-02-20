using System;
using System.Reflection;

namespace TitanWcfService.PatternsTemplates
{
    public class Singleton<T> where T:class 
    {
        /// created to prohibit creating examples implemented classes
        /// it will cause in private constructor  
        protected Singleton() { } 

        private sealed class SingletonCreator<S> where S:class 
        {
            /// uses reflection to create a example of class without constructor
            private static readonly S instance = (S)typeof(S).GetConstructor(
               BindingFlags.Instance | BindingFlags.NonPublic,
               null,
               new Type[0],
               new ParameterModifier[0]).Invoke(null);

            public static S CreatorInstance => instance;
        }

        public static T Instance => SingletonCreator<T>.CreatorInstance;
    }
}