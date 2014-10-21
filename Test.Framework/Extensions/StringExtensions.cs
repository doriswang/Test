using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Test.Framework.Extensions
{
    using Validation;

    public static class StringExtensions
    {
        private static readonly Regex HtmlExpression = new Regex(@"<(.|\n)*?>", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex SSNExpression = new Regex(@"^\d{9}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex DigitsOnlyExpression = new Regex(@"[^\d+//]", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex PhoneNumberExpression = new Regex(@"^\d{10}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex ValidDateExpression = new Regex(@"([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d", RegexOptions.Singleline | RegexOptions.Compiled);

        [DebuggerStepThrough]
        public static string ToTitleCase(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                TextInfo textInfo = cultureInfo.TextInfo;

                return textInfo.ToTitleCase(target.Trim().ToLower());
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static string ToDigitsOnly(this string target)
        {
            return target.IsNotNullOrEmpty() ? DigitsOnlyExpression.Replace(target, "") : string.Empty;
        }

        [DebuggerStepThrough]
        public static string ToUpperCase(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                return target.Trim().ToUpper();
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static string ToPhone(this string target)
        {
            if (target.IsNotNullOrEmpty() && target.Length >= 10)
            {
                return string.Format("({0}) {1}-{2}", target.Substring(0, 3), target.Substring(3, 3), target.Substring(6, 4));
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static bool HasValue(this string target)
        {
            return !string.IsNullOrEmpty(target);
        }

        [DebuggerStepThrough]
        public static bool ToBoolean(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            return Convert.ToBoolean(target);
        }

        [DebuggerStepThrough]
        public static string ReplaceAt(this string target, int index, char newChar)
        {
            Check.Argument.IsNotEmpty(target, "target");
            if (target.Length < index)
            {
                return target;
            }
            var builder = new StringBuilder();
            for (var i = 0; i < target.Length; i++)
            {
                if (i == index)
                {
                    builder.Append(newChar);
                }
                else
                {
                    builder.Append(target[i]);
                }
            }
            return builder.ToString();
        }

        public static string RemoveAt(this string target, int index)
        {
            Check.Argument.IsNotEmpty(target, "target");
            if (target.Length < index)
            {
                return target;
            }
            var builder = new StringBuilder();
            for (var i = 0; i < target.Length; i++)
            {
                if (i == index)
                {
                }
                else
                {
                    builder.Append(target[i]);
                }
            }
            return builder.ToString();
        }

        [DebuggerStepThrough]
        public static bool GuidTryParse(this string s, out Guid result)
        {
            if (s == null)
                throw new ArgumentNullException("String is null in GuidTryParse()");

            try
            {
                result = new Guid(s);
                return true;
            }
            catch (FormatException)
            {
                result = Guid.Empty;
                return false;
            }
            catch (OverflowException)
            {
                result = Guid.Empty;
                return false;
            }
        }

        [DebuggerStepThrough]
        public static bool IsGuid(this string s)
        {
            if (s.IsNullOrEmpty())
                return false;
            try
            {
                new Guid(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        [DebuggerStepThrough]
        public static int ToInteger(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            int i;
            if (!int.TryParse(target, out i))
                throw new InvalidCastException(target + " is an invalid integer.");

            return i;
        }

        [DebuggerStepThrough]
        public static ulong ToUlong(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            ulong i;
            if (!ulong.TryParse(target, out i))
                throw new InvalidCastException(target + " is an invalid ulong.");

            return i;
        }

        public static bool IsInteger(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            int i;
            return int.TryParse(target, out i);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target.Trim());
        }

        [DebuggerStepThrough]
        public static bool IsPhone(this string target)
        {
            return !string.IsNullOrEmpty(target) && PhoneNumberExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsSSN(this string target)
        {
            return !string.IsNullOrEmpty(target) && SSNExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsValidDate(this string target)
        {
            DateTime date;
            bool result = DateTime.TryParse(target, out date);
            return result;
        }

        [DebuggerStepThrough]
        public static bool IsValidPHPDate(this string target)
        {
            var result = false;
            if (target.IsNotNullOrEmpty() && target.Length == 8)
            {
                DateTime date;

                result = DateTime.TryParse(string.Format("{0}-{1}-{2}", target.Substring(0, 4), target.Substring(4, 2), target.Substring(6, 2)), out date);
            }
            return result;
        }

        [DebuggerStepThrough]
        public static double ToDouble(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            double d;
            if (!double.TryParse(target, out d))
                throw new InvalidCastException(target + " is an invalid double.");

            return d;
        }

        public static bool IsDouble(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            double d;
            return double.TryParse(target, out d);
        }

        [DebuggerStepThrough]
        public static DateTime ToDateTime(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            DateTime dt;
            if (!DateTime.TryParse(target, out dt))
                throw new InvalidCastException(target + "is an invalid DateTime.");

            return dt;
        }

        [DebuggerStepThrough]
        public static DateTime ToDateTimePHP(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            DateTime dt;
            if (!DateTime.TryParse(string.Format("{0}-{1}-{2}", target.Substring(0, 4), target.Substring(4, 2), target.Substring(6, 2)), out dt))
                throw new InvalidCastException(target + "is an invalid DateTime.");

            return dt;
        }

        [DebuggerStepThrough]
        public static bool IsDate(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                DateTime dt;
                if (DateTime.TryParse(target, out dt))
                {
                    return true;
                }
            }
            return false;
        }

        [DebuggerStepThrough]
        public static string ToDateTimeString(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            string date = string.Empty;
            if (target.Length == 8)
            {
                var dateTime = new DateTime(int.Parse(target.Substring(0, 4)), int.Parse(target.Substring(4, 2)), int.Parse(target.Substring(6, 2)));
                date = dateTime.ToString("MM/dd/yyyy");
            }
            return date;
        }

        [DebuggerStepThrough]
        public static DateTime ToMySqlDate(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");

            DateTime dt;
            if (!DateTime.TryParse(target, out dt))
                throw new InvalidCastException(target + "is an invalid DateTime.");

            return dt;
        }

        [DebuggerStepThrough]
        public static string ToOrderedDate(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");

            DateTime dt;
            if (!DateTime.TryParse(target, out dt))
                throw new InvalidCastException(target + "is an invalid DateTime.");

            return dt.ToString("yyyy/MM/dd");
        }

        [DebuggerStepThrough]
        public static string[] ToArray(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");
            return target.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        [DebuggerStepThrough]
        public static List<int> ToList(this string target)
        {
            Check.Argument.IsNotEmpty(target, "target");

            List<int> list = new List<int>();
            string[] array = target.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            array.ForEach(s => { list.Add(s.ToInteger()); });

            return list;
        }

        [DebuggerStepThrough]
        public static Guid ToGuid(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return Guid.Empty;
            }
            return new Guid(target);
        }

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        [DebuggerStepThrough]
        public static bool IsNotNullOrEmpty(this string target)
        {
            return !string.IsNullOrEmpty(target);
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            Check.Argument.IsNotEmpty(target, "target");

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static bool IsEqual(this string original, string compare)
        {
            if (original != null && compare != null)
            {
                if (original.Trim().Equals(compare.Trim(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        [DebuggerStepThrough]
        public static string PartOfString(this string original, int startIndex, int length)
        {
            if (original != null && original.Length > length)
            {
                return original.Substring(startIndex, length);
            }
            return original;
        }

        [DebuggerStepThrough]
        public static string Join(this string[] values, string delimiter)
        {
            bool first = true;
            StringBuilder sb = new StringBuilder();

            foreach (string item in values)
            {
                if (item == " " || item == "")
                    continue;

                if (!first)
                {
                    sb.Append(delimiter);
                }
                else
                {
                    first = false;
                }

                sb.Append(item);
            }
            return sb.ToString();
        }

        [DebuggerStepThrough]
        public static string Clean(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                return target.Replace("\n", " <br/> ").Replace("\r", "").Replace(" \"", " &#8220;").Replace("\"", "&#8221;").Replace(" '", " &#8216;").Replace("'", "&#8217;").Replace("\\", "\\\\");
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static string Unclean(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                return target.Replace("<br/>", "\n").Replace("\r", "").Replace("&#8220;", "\"").Replace("&#8221;", "\"").Replace("&#8216;", " '").Replace("&#8217;", "'");
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static string EscapeApostrophe(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                return target.Replace("'", "\\'");
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static bool StartsWithAny(this string input, string[] values)
        {
            bool result = false;

            foreach (string item in values)
            {
                if (input.ToLower().StartsWith(item.ToLower()))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        public static bool ContainsAny(this string input, string[] values)
        {
            bool result = false;

            foreach (string item in values)
            {
                if (input.ToLower().Contains(item.ToLower()))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        public static string TrimWithNullable(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                return target.Trim();
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string target)
        {
            if (target.IsNotNullOrEmpty())
            {
                var input = target.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("&nbsp;", " ").Replace("_", "");
                return HtmlExpression.Replace(input, string.Empty);
            }
            return string.Empty;
        }

        [DebuggerStepThrough]
        public static String Pluralize(this String s)
        {
            return PluralizationService.CreateService(CultureInfo.CurrentCulture).Pluralize(s);
        }

        [DebuggerStepThrough]
        public static String Singularize(this String s)
        {
            return PluralizationService.CreateService(CultureInfo.CurrentCulture).Singularize(s);
        }
    }
}
