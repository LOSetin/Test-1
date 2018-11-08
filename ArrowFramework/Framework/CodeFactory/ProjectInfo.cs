using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.CodeFactory
{
    public class ProjectInfo
    {
        /// <summary>
        /// 顶级命名空间
        /// </summary>
        public string TopNameSpace { set; get; }

        /// <summary>
        /// 连接名
        /// </summary>
        public string ConnectionName { set; get; }

        /// <summary>
        /// 命名方法，是否以下划线分隔
        /// </summary>
        public bool IsNameSplitWithUnderLine { set; get; }

        /// <summary>
        /// 生成的代码的根目录，如果为空，则使用默认目录（当前程序根目录\\TopNameSpace）作为根目录
        /// </summary>
        public string CodeRootPath { set; get; }

    }
}
