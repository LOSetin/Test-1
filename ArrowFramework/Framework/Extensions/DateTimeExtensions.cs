using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.Extensions
{
    /// <summary>
    /// 日期扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 返回yyyy-MM-dd 0:00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToStartString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd 0:00:00");
        }

        /// <summary>
        /// 返回yyyy-MM-dd 23:59:59
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToEndString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd 23:59:59");
        }

        /// <summary>
        /// 返回yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToLocalString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回yyyyMMddHHmmssfff
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToSeqString(this DateTime dt)
        {
            return dt.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// 返回yyyy-MM-dd
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateOnlyString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

    }
}
