using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Arrow.Framework.Extensions
{
    /// <summary>
    /// 对象扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 判断对象是否为空，若对象为null或DBNull.Value，则返回true，否则返回false
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsNull(this object obj)
        {
            return (obj == null || obj == DBNull.Value) ? true : false;
        }


        #region 类型转换
        /// <summary>
        /// obj类型转换为字符串，
        /// 对象为null，或为DBNull.Value，则返回""
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToArrowString(this object obj)
        {
            return obj.IsNull() ? "" : obj.ToString();
        }

        /// <summary>
        /// 转换成整数，不能转换则返回默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dValue">不能转换返回的默认值</param>
        /// <returns></returns>
        public static int ToArrowInt(this object obj, int dValue)
        {
            return obj.IsNull() ? dValue : obj.ToString().AsInt();
        }

        /// <summary>
        /// 转换成整数，不能转换则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToArrowInt(this object obj)
        {
            return obj.IsNull() ? 0 : obj.ToString().AsInt();
        }

        /// <summary>
        /// 转换成货币型，不能转换则返回默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dValue">不能转换返回的默认值</param>
        /// <returns></returns>
        public static decimal ToArrowDecimal(this object obj, decimal dValue)
        {
            return obj.IsNull() ? dValue : obj.ToString().AsDecimal();
        }

        /// <summary>
        /// 字符串转换成货币型，不能转换则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToArrowDecimal(this object obj)
        {
            return obj.IsNull() ? 0M : obj.ToString().AsDecimal();
        }

        /// <summary>
        /// 转换为日期形式，转换失败，返回默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dValue">转换失败，返回的默认值</param>
        /// <returns></returns>
        public static DateTime ToArrowDateTime(this object obj, DateTime dValue)
        {
            return obj.IsNull() ? dValue : obj.ToString().AsDateTime();
        }

        /// <summary>
        /// 转换成日期类型，若无法转换，则返回DateTime.MinValue
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToArrowDateTime(this object obj)
        {
            return obj.IsNull() ? DateTime.MinValue : obj.ToString().AsDateTime();
        }

        /// <summary>
        /// 转换成浮点类型，若无法转换，则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ToArrowFloat(this object obj)
        {
            return obj.IsNull() ? 0 : obj.ToString().AsFloat();
        }

        /// <summary>
        /// 转换成浮点类型，若无法转换，则返回默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dValue">>转换失败，返回的默认值</param>
        /// <returns></returns>
        public static float ToArrowFloat(this object obj, float dValue)
        {
            return obj.IsNull() ? dValue : obj.ToString().AsFloat();
        }

        /// <summary>
        /// 转换成布尔值，若无法转换，则返回false
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToArrowBool(this object obj)
        {
            return obj.IsNull() ? false : obj.ToString().AsBool();
        }

        /// <summary>
        /// 转换成布尔值，若无法转换，则返回默认值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dValue">转换失败，返回的默认值</param>
        /// <returns></returns>
        public static bool ToArrowBool(this object obj, bool dValue)
        {
            return obj.IsNull() ? dValue : obj.ToString().AsBool();
        }

        #endregion

        /// <summary>
        /// 对象ToString后输出，最后加br标签
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static void ResponseLine(this object obj)
        {
            HttpContext.Current.Response.Write(obj.ToString() + "<br/>");
        }

    }
}
