using System;
using System.Diagnostics;

namespace Test.Framework
{
    public abstract class Disposable : IDisposable
    {
        ~Disposable()
        {
            Dispose(false);
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
