using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Fix
{
    public static class StaticExtensions
    {
        public static V ConvertValue<V>(this object x)
        {
            return (V)Convert.ChangeType(x, typeof(V));
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static string ToJSONNull(this string value)
        {
            return value.Replace(":\"\"", ":null");
        }

        public static bool IsJSONNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || value == "[]";
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool ToBoolean(this object value)
        {
            return Convert.ToBoolean(value);
        }

        public static int ToInteger(this object value)
        {
            return Convert.ToInt32(value);
        }

        public static long ToLong(this object value)
        {
            return Convert.ToInt64(value);
        }

        public static DateTime? ToNullableDateTime(this DateTime value)
        {
            DateTime? nullValue = value;
            return nullValue;
        }

        public static DateTime ToDateTime(this DateTime? value)
        {
            return value.Value;
        }


        public static string NameToCamelCase(this string name)
        {

            if (string.IsNullOrEmpty(name))
                return string.Empty;

            name = name.Trim();

            StringBuilder _name = new StringBuilder();
            for (int i = 0; i < name.Split(' ').Count(); i++)
            {
                var subName = name.Split(' ')[i].Trim();
                _name.Append(char.ToUpperInvariant(subName[0]) + subName.Substring(1).ToLower());
                if (name.Split(' ').Any()) _name.Append(" ");
            }
            return _name.ToString().Trim();
        }

        public static string ToEnglishCharacter(this string text)
        {
            var result = String.Join("",
                text
                    .Normalize(NormalizationForm.FormD)
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ı", "i");
            return result;
        }

        public static string GetFirstWord(this string text)
        {

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = text.Trim();
            var subName = text.Split(' ')[0].Trim();

            return subName;
        }


        public static T GetValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }

        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumerationValue.ToString();
        }
    }
}
