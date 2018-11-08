using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.CodeFactory
{
    public class FieldInfo
    {

        /// <summary>
        /// 数据库中的原始字段名
        /// </summary>
        public string FieldName { set; get; }

        /// <summary>
        /// 数据库中的字段类型
        /// </summary>
        public string FieldType { set; get; }

        /// <summary>
        /// 字段长度，如果字段类型是小数，则表示整数部分位数
        /// </summary>
        public int FieldLength { set; get; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int FieldScale { set; get; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { set; get; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { set; get; }

        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool IsUnique { set; get; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsNullable { set; get; }

        /// <summary>
        /// 是否自增，Oracle数据库，当主键为Thread_ID时，表示自增，使用序列SEQ_TABLE的值作为主键值
        /// </summary>
        public bool IsAutoIncrease { set; get; }

        /// <summary>
        /// 字段在c#中的属性名，例如字段名为User_ID，转换为UserID，去掉下划线，首字母大写，缩写不变
        /// </summary>
        public string PropertyName { set; get; }

        /// <summary>
        /// C#中的变量类型
        /// </summary>
        public string VarType { set; get; }

        /// <summary>
        /// 例如，dr.GetString(0)中的String
        /// </summary>
        public string ReaderGetType { set; get; }

    }
}
