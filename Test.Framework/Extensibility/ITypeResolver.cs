using LightInject;
using System;
using System.Collections.Generic;

namespace Test.Framework.Extensibility
{
    public interface ITypeResolver : IDisposable
    {
        object GetUnderlyingContainer();
        object Resolve(Type type);
        T Resolve<T>() where T : class;
        T Resolve<T>(Type type) where T : class;
        T Resolve<T>(string name) where T : class;
        T Resolve<T>(Type type, string name) where T : class;
        IEnumerable<T> ResolveAll<T>() where T : class;
        IEnumerable<T> ResolveAll<T>(Type type) where T : class;

        void Inject<T>(T instance) where T : class;
        void Inject<T>(string name, T instance) where T : class;
        void Inject<T>(string name, T instance, ObjectLifeSpans lifeSpan) where T : class;

        void Register<I, T>()
            where I : class
            where T : class, I;
        void Register<I, T>(string name)
            where I : class
            where T : class, I;
        void Register<I, T>(ObjectLifeSpans lifeSpan)
            where I : class
            where T : class, I;
        void Register<I, T>(string name, ObjectLifeSpans lifeSpan)
            where I : class
            where T : class, I;

        void RegisterInstance<T>(string name, T instance)
            where T : class;

        void RegisterInstance<T>(string name, T instance, ObjectLifeSpans lifeSpan)
        where T : class;

        void RegisterInstance<I, T>(string name, T instance, ObjectLifeSpans lifeSpan)
            where I : class
            where T : class, I;

    }
}
