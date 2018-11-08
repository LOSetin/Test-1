using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SliderInfo : EntityBase
    {
        public SliderInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PicPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddUserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsTop { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsShow { set; get; }

        public static readonly string TableOrViewName = "Slider";
        public static readonly string AllFields = "ID,Name,PicPath,Url,AddTime,AddUserName,SortOrder,IsTop,IsShow";
        public static readonly string TablePrimaryKey = "ID";
    }
}
