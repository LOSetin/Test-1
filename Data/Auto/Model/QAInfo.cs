using System;
using Arrow.Framework;

namespace TMS
{
    public partial class QAInfo : EntityBase
    {
        public QAInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CatName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string QATitle { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string QAContent { set; get; }

        public static readonly string TableOrViewName = "QA";
        public static readonly string AllFields = "ID,CatName,QATitle,QAContent";
        public static readonly string TablePrimaryKey = "ID";
    }
}
