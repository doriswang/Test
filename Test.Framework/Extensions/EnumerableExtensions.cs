namespace Test.Framework.Extensions
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;

    using Validation;

    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null ||
                !enumerable.Any())
                return true;

            return false;
        }

        [DebuggerStepThrough]
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null ||
                !enumerable.Any())
                return false;

            return true;
        }

        [DebuggerStepThrough]
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            Check.Argument.IsNotNull(source, "source");
            Check.Argument.IsNotNegativeOrZero(size, "size");

            int index = 1;
            IEnumerable<T> partition = source.Take(size).AsEnumerable();

            while (partition.Any())
            {
                yield return partition;
                partition = source.Skip(index++ * size).Take(size).AsEnumerable();
            }
        }
    }
}
