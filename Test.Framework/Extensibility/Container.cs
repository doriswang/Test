using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Test.Framework.Validation;
using Test.Framework.Extensions;

namespace Test.Framework.Extensibility
{
    public static class Container
    {
        public static ITypeResolver resolver;

        [DebuggerStepThrough]
        public static void InitializeWith(ITypeResolver resolver)
        {
            Check.Argument.IsNotNull(resolver, "resolver");

            Container.resolver = resolver;
        }

        //[DebuggerStepThrough]
        public static void Register<I, T>()
            where I : class
            where T : class, I
        {
            resolver.Register<I, T>();
        }

        //[DebuggerStepThrough]
        public static void Register<I, T>(string name)
            where I : class
            where T : class, I
        {
            resolver.Register<I, T>(name);
        }

        //[DebuggerStepThrough]
        public static void Register<I, T>(ObjectLifeSpans lifeSpan)
            where I : class
            where T : class, I
        {
            resolver.Register<I, T>(lifeSpan);
        }

        //[DebuggerStepThrough]
        public static void Register<I, T>(string name, ObjectLifeSpans lifeSpan)
            where I : class
            where T : class, I
        {
            resolver.Register<I, T>(name, lifeSpan);
        }

        public static void RegisterInstance<I, T>(string name, T instance, ObjectLifeSpans lifeSpan)
            where I : class
            where T : class, I
        {
            resolver.RegisterInstance<I, T>(name, instance, lifeSpan);
        }

        //[DebuggerStepThrough]
        public static void RegisterAll<T>() where T : class
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
        public static void Inject<T>(T existing) where T : class
        {
            Check.Argument.IsNotNull(existing, "existing");

            resolver.Inject<T>(existing.GetType().Name, existing, ObjectLifeSpans.Transient);
        }

        //[DebuggerStepThrough]
        public static void Inject<T>(string name, T existing) where T : class
        {
            Check.Argument.IsNotEmpty(name, "name");
            Check.Argument.IsNotNull(existing, "existing");

            resolver.Inject<T>(name, existing, ObjectLifeSpans.Transient);
        }

        //[DebuggerStepThrough]
        public static void Inject<T>(string name, T existing, ObjectLifeSpans lifeSpan) where T : class
        {
            Check.Argument.IsNotEmpty(name, "name");
            Check.Argument.IsNotNull(existing, "existing");

            resolver.Inject<T>(name, existing, lifeSpan);
        }

        [DebuggerStepThrough]
        public static object Resolve(Type type)
        {
            Check.Argument.IsNotNull(type, "type");

            return resolver.Resolve(type);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type) where T : class
        {
            Check.Argument.IsNotNull(type, "type");

            return resolver.Resolve<T>(type);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type, string name) where T : class
        {
            Check.Argument.IsNotNull(type, "type");
            Check.Argument.IsNotEmpty(name, "name");

            return resolver.Resolve<T>(type, name);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>() where T : class
        {
            return resolver.Resolve<T>();
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(string name) where T : class
        {
            Check.Argument.IsNotEmpty(name, "name");

            return resolver.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ResolveAll<T>() where T : class
        {
            return resolver.ResolveAll<T>();
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ResolveAll<T>(Type type) where T : class
        {
            return resolver.ResolveAll<T>(type);
        }

        [DebuggerStepThrough]
        public static void Reset()
        {
            if (resolver != null)
            {
                resolver.Dispose();
            }
        }
    }
}
