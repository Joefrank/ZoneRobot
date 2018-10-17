using System;
using System.ComponentModel;

namespace toyrobot.utils
{
    /// <summary>
    /// Utility class used to retrieve description attribute for enum
    /// </summary>
    public static class EnumExtensions
    {
        public static string ToDescription<TEnum>(this TEnum enumValue) where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)((TEnum)enumValue));
        }

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}