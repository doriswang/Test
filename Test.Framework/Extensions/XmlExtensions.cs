using System.Diagnostics;
using System.Text;
using Test.Framework.Serialization;

namespace Test.Framework.Extensions
{
    public static class XmlExtensions
    {
        [DebuggerStepThrough]
        public static string ToXml<T>(this T instance)
        {
            if (instance == null)
                return string.Empty;

            return XSerializer.Serialize(instance);
        }

        [DebuggerStepThrough]
        public static string ToXml<T>(this T instance, bool omitXmlDeclaration)
        {
            if (instance == null)
                return string.Empty;

            return XSerializer.Serialize(instance, omitXmlDeclaration);
        }

        [DebuggerStepThrough]
        public static T ToObject<T>(this string text)
        {
            if (text.IsNullOrEmpty())
                return default(T);
            return XSerializer.Deserialize<T>(text);
        }

        [DebuggerStepThrough]
        public static string SanitizeXmlString(this string xml)
        {
            if (xml.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
                else
                {
                    buffer.Append(" ");
                }
            }
            return buffer.ToString();
        }


        [DebuggerStepThrough]
        public static bool IsLegalXmlChar(this int character)
        {
            return
            (
                character == 0x9 ||
                character == 0xA ||
                character == 0xD ||
               (character >= 0x20 && character <= 0xD7FF) ||
               (character >= 0xE000 && character <= 0xFFFD) ||
               (character >= 0x10000 && character <= 0x10FFFF)
            );
        }
    }
}
