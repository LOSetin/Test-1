using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 分页样式
    /// </summary>
    public enum ControlPageStyle
    {
        /// <summary>
        /// 完整的分页，共12343条  第1/10页  首页 上页 1 2 3  4 下页 末页
        /// </summary>
        Full=1,
        /// <summary>
        /// 简单的分页，首页 上页 1 2 3 4 5 下页 末页
        /// </summary>
        Simple=2,
        /// <summary>
        /// 精简的分页，首页 1/20 下页
        /// </summary>
        VerySimple=3
    }
}
