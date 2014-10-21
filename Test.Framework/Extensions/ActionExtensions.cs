using System;
using System.Threading;
using System.Diagnostics;

namespace Test.Framework.Extensions
{
    public static class ActionExtensions
    {
        public static Action Execute(this Action action)
        {
            return () =>
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                }
                finally
                {
                }
            };
        }
    }
}
