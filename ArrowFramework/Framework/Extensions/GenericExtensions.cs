using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Arrow.Framework.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// 类属性（简单类型）的ToString()的扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToDebugString<T>(this T t) where T : new()
        {
            var query = from prop in t.GetType().GetProperties(
                        BindingFlags.Instance | BindingFlags.Public)
                        where prop.CanRead
                        select string.Format("{0}: {1}\r\n",
                        prop.Name,
                        prop.GetValue(t, null));

            string[] fields = query.ToArray();
            StringBuilder sb = new StringBuilder();

            foreach (string field in fields)
            {
                sb.Append(field);
            }

            return sb.ToString();
        }
    }
}
