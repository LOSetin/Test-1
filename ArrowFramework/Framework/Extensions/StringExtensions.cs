using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Arrow.Framework.Extensions
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 忽略大小写比较字符串，与区域和语言惯例无关的比较
        /// </summary>
        /// <param name="value">比较的字符串</param>
        /// <param name="strCompare">比较的字符串</param>
        /// <returns></returns>
        public static bool IsEqualTo(this string value, string strCompare)
        { 
            return string.Equals(value,strCompare,StringComparison.OrdinalIgnoreCase) ? true : false;
        }

        #region 类型转换
        public static int AsInt(this string value)
        {
            return AsInt(value, 0);
        }

        public static int AsInt(this string value, int defaultValue)
        {
            int result;
            return Int32.TryParse(value, out result) ? result : defaultValue;
        }

        public static decimal AsDecimal(this string value)
        {
            // Decimal.TryParse 对于某些语言区域不能正常使用. 例如 lt-LT,  "12.12" 会解析成 1212.
            return As<Decimal>(value, 0);
        }

        public static decimal AsDecimal(this string value, decimal defaultValue)
        {
            return As<Decimal>(value, defaultValue);
        }

        public static float AsFloat(this string value)
        {
            return AsFloat(value, 0);
        }

        public static float AsFloat(this string value, float defaultValue)
        {
            float result;
            return Single.TryParse(value, out result) ? result : defaultValue;
        }

        public static DateTime AsDateTime(this string value)
        {
            return AsDateTime(value, DateTime.MinValue);
        }

        public static DateTime AsDateTime(this string value, DateTime defaultValue)
        {
            DateTime result;
            return DateTime.TryParse(value, out result) ? result : defaultValue;
        }

        public static TValue As<TValue>(this string value)
        {
            return As<TValue>(value, default(TValue));
        }

        public static bool AsBool(this string value)
        {
            return AsBool(value, false);
        }

        public static bool AsBool(this string value, bool defaultValue)
        {
            bool result;
            return Boolean.TryParse(value, out result) ? result : defaultValue;
        }

        public static TValue As<TValue>(this string value, TValue defaultValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter.ConvertFrom(value);
                }
                // 尝试其他转换
                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter.ConvertTo(value, typeof(TValue));
                }
            }
            catch
            {
            }
            return defaultValue;
        }

        public static bool IsBool(this string value)
        {
            bool result;
            return Boolean.TryParse(value, out result);
        }

        public static bool IsInt(this string value)
        {
            int result;
            return Int32.TryParse(value, out result);
        }

        public static bool IsDecimal(this string value)
        {
            return Is<Decimal>(value);
        }

        public static bool IsFloat(this string value)
        {
            float result;
            return Single.TryParse(value, out result);
        }

        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        public static bool Is<TValue>(this string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
            if (converter != null)
            {
                try
                {
                    if ((value == null) || converter.CanConvertFrom(null, value.GetType()))
                    {
                        converter.ConvertFrom(null, CultureInfo.CurrentCulture, value);
                        return true;
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        #endregion

    }
}
