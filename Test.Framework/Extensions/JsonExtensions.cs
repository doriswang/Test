using System;
using System.Diagnostics;
using Test.Framework.Validation;
using Test.Framework.Serialization;

namespace Test.Framework.Extensions
{
    public static class JsonExtension
    {
        [DebuggerStepThrough]
        public static T FromJson<T>(this string json)
        {
            Check.Argument.IsNotEmpty(json, "json");

            return JSerializer.Deserialize<T>(json);
        }

        [DebuggerStepThrough]
        public static string ToJson<T>(this T instance)
        {
            Check.Argument.IsNotNull(instance, "instance");

            return JSerializer.Serialize(instance);
        }
    }
}
