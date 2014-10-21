using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Test.Framework.Extensions;
using Test.Framework.Validation;

namespace Test.Framework.Extensibility
{
    public class ChildContainer : IChildContainer
    {
        private ITypeResolver resolver;

        [DebuggerStepThrough]
        public ChildContainer(ITypeResolver resolver)
        {
            Check.Argument.IsNotNull(resolver, "resolver");
            this.resolver = resolver;
        }

        //[DebuggerStepThrough]
        public void Register<I, T>()
            where I : class
            where T : class, I
        {
            resolver.Register<I, T>(ObjectLifeSpans.Thread);
        }

        //[DebuggerStepThrough]
        public void Register<I, T>(string name)
            where I : class
            where T : class, I
        {
            resolver.Register<I, T>(name, ObjectLifeSpans.Thread);
        }

        //[DebuggerStepThrough]
        public void RegisterAll<T>() where T : class
        {
            var type = typeof(T);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
            types.ForEach(t =>
            {
                var instance = (T)Activator.CreateInstance(t);
                Inject<T>(type.Name, instance);
            });
        }

        [DebuggerStepThrough]
        public void Inject<T>(T existing) where T : class
        {
            Check.Argument.IsNotNull(existing, "existing");

            resolver.Inject<T>(existing.GetType().Name, existing, ObjectLifeSpans.Thread);
        }

        //[DebuggerStepThrough]
        public void Inject<T>(string name, T existing) where T : class
        {
            Check.Argument.IsNotEmpty(name, "name");
            Check.Argument.IsNotNull(existing, "existing");

            resolver.Inject<T>(name, existing, ObjectLifeSpans.Thread);
        }

        [DebuggerStepThrough]
        public object Resolve(Type type)
        {
            Check.Argument.IsNotNull(type, "type");

            return resolver.Resolve(type);
        }

        [DebuggerStepThrough]
        public T Resolve<T>(Type type) where T : class
        {
            Check.Argument.IsNotNull(type, "type");

            return resolver.Resolve<T>(type);
        }

        [DebuggerStepThrough]
        public T Resolve<T>(Type type, string name) where T : class
        {
            Check.Argument.IsNotNull(type, "type");
            Check.Argument.IsNotEmpty(name, "name");

            return resolver.Resolve<T>(type, name);
        }

        [DebuggerStepThrough]
        public T Resolve<T>() where T : class
        {
            return resolver.Resolve<T>();
        }

        [DebuggerStepThrough]
        public T Resolve<T>(string name) where T : class
        {
            Check.Argument.IsNotEmpty(name, "name");

            return resolver.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            return resolver.ResolveAll<T>();
        }

        [DebuggerStepThrough]
        public IEnumerable<T> ResolveAll<T>(Type type) where T : class
        {
            return resolver.ResolveAll<T>(type);
        }

        [DebuggerStepThrough]
        public void Reset()
        {
            if (resolver != null)
            {
                resolver.Dispose();
            }
        }

        [DebuggerStepThrough]
        public IChildContainer CreateChildContainer()
        {
            var childContainer = new ChildContainer(resolver);
            return childContainer;
        }

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
