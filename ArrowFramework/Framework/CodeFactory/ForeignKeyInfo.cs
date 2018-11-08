using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.CodeFactory
{
    /// <summary>
    /// 外键约束实体类
    /// </summary>
    public class ForeignKeyInfo
    {
        /// <summary>
        /// 约束名
        /// </summary>
        public string ConstraintName { set; get; }

        /// <summary>
        /// 主表名
        /// </summary>
        public string MainTableName { set; get; }

        /// <summary>
        /// 主表列名
        /// </summary>
        public string MainTableColumnName { set; get; }

        /// <summary>
        /// 从表名
        /// </summary>
        public string SubTableName { set; get; }

        /// <summary>
        /// 从表列名
        /// </summary>
        public string SubTableColumnName { set; get; }

    }
}
