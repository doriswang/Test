using System;

namespace Test.Framework.Extensibility
{
    public enum ObjectLifeSpans
    {
        Singleton = 1,
        Transient = 2,
        WebRequest = 3,
        Thread = 4,
        Session = 5,
        Cached = 6
    }
}
