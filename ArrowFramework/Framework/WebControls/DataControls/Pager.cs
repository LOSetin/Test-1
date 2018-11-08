using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;

[assembly: TagPrefix("ArrowPager", "ArrowPager")]
namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class Pager : System.Web.UI.WebControls.Literal
    {

        private WebPager webPager;

        public WebPager WebPager { set { this.webPager = value; } get { return this.webPager; } }


        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!DesignMode)
            {
                webPager.ShowPager(writer);
            }

        }

    }
}
