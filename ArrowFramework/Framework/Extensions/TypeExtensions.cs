using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace Arrow.Framework.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取类中所有public static readonly 的字段的值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<string> ArrowEnumValues(this Type t) 
        {
            List<string> values = new List<string>();
            if (!t.IsClass)
                return values;

            FieldInfo[] fields = t.GetFields(BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].IsInitOnly && fields[i].IsPublic && fields[i].IsStatic)
                    values.Add(fields[i].GetValue(null).ToArrowString());
            }

            return values;
        }

        /// <summary>
        /// 将类中所有public static readonly 的字段的值，形成下拉列表，value和text均为此值
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ddl"></param>
        public static void BindToDropDownList(this Type t, DropDownList ddl)
        {
            ddl.DataSource = t.ArrowEnumValues();
            ddl.DataBind();
        }



    }
}
