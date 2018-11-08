using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace Arrow.Framework.Extensions.DotNetEnum
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 枚举转换成字典类型，枚举名转成Key，枚举描述转成Value
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this Type enumType)
        {
            //key 不区分大小写
            Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            if (!enumType.IsEnum)
                return dict;

            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                FieldInfo fi = enumType.GetField(name);
                DescriptionAttribute[] arrDescriptionAttributes = (DescriptionAttribute[])fi.
                            GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (arrDescriptionAttributes.Length > 0)
                    dict.Add(fi.Name, arrDescriptionAttributes[0].Description);
                else
                    dict.Add(fi.Name, fi.Name);
            }
            return dict;
        }

        /// <summary>
        /// 将枚举绑定到下拉列表，若非枚举，则返回空列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="ddl"></param>
        public static void BindToDropDownList(this Type enumType , DropDownList ddl )
        {
            ddl.DataSource = enumType.ToDictionary();
            ddl.DataTextField = "value";
            ddl.DataValueField = "key";
            ddl.DataBind();
        }

        /// <summary>
        /// 根据枚举名，获得枚举描述，不区分大小写。如[Description("红色")] Red，根据枚举名：Red，获得其描述：红色。
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum enumType)
        {
            return enumType.GetType().ToDictionary()[enumType.ToString()];
        }

        /// <summary>
        /// 根据枚举名，获得枚举描述，不区分大小写。如[Description("红色")] Red，根据枚举名：Red，获得其描述：红色。
        /// </summary>
        /// <param name="enumName"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string ToDescription(this string enumName, Type enumType)
        {
            return enumType.ToDictionary()[enumName];
        }

    }
}
