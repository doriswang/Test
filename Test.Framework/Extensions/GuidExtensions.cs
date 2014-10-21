namespace Test.Framework.Extensions
{
    using System;
    using System.Diagnostics;

    public static class GuidExtension
    {
        [DebuggerStepThrough]
        public static bool IsEmpty(this Guid target)
        {
            return target == Guid.Empty;
        }

        [DebuggerStepThrough]
        public static bool IsNotEmpty(this Guid target)
        {
            return target != Guid.Empty;
        }
    }
}
