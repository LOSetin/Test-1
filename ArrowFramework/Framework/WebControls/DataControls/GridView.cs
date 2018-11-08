using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Configuration;
using Arrow.Framework.Extensions;
using Arrow.Framework.DataAccess;

[assembly: TagPrefix("ArrowGridView", "ArrowGridView")]
namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 带分页GridView控件
    /// </summary>
    public class GridView : System.Web.UI.WebControls.GridView
    {

        #region 定义字段

        private WebQuery query;
        private string mouseHoverColor = "#ddf6d4";
        private Database db;
        private WebPager pager;
        private bool showHorizionaScroll = true;
        #endregion

        #region 属性定义
        /// <summary>
        /// 是否显示横向滚动条
        /// </summary>
        public bool ShowHorizionalScroll
        {
            set { showHorizionaScroll = value; }
            get { return showHorizionaScroll; }
        }

        /// <summary>
        /// 数据库操作类实例
        /// </summary>
        public Database Db
        {
            set { this.db = value; }
            get { return this.db; }
        }

        /// <summary>
        /// 分页对象
        /// </summary>
        public WebPager Pager
        {
            set { this.pager = value; }
            get { return this.pager; }
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        public WebQuery Query
        {
            set { this.query = value; }
            get { return this.query; }
        }

    
        /// <summary>
        /// 鼠标悬停颜色，如果为空则悬停无效
        /// </summary>
        [Bindable(true)]
        [Category("Data")]
        [DefaultValue("#ddf6d4")]
        [Description("鼠标悬停颜色，如果为空则取消悬停效果")]
        public string MouseHoverColor
        {
            set { mouseHoverColor = value; }
            get { return mouseHoverColor; }
        }

        #endregion

        #region 生成数据源
        //GridView不能重写DataBind
        /// <summary>
        /// 生成数据源，在任何设置完成后，DataBind()之前执行
        /// </summary>
        public void CreateDataSource()
        {
            string recordCountSql = query.CreateGetRecordCountSql();
            this.pager.RecordCount = db.ExecuteScalar(recordCountSql).ToArrowInt();
            pager.CaculatePageCount();
            string pageSql = query.CreatePagerSql(db.ProviderName, pager);
            this.DataSource = db.ExecuteDataSet(pageSql);
        }

        #endregion

        #region 重写
        /// <summary>
        /// 呈现控件
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            //当水平位置不够时，显示滚动条
            if(showHorizionaScroll) writer.WriteLine("<div style=\"margin-left:10px;margin-right:10px;overflow-x:auto\">");
            base.Render(writer);

            if (DataSource != null)
                pager.ShowPager(writer);

           if(showHorizionaScroll) writer.WriteLine("</div>");
        }

        /// <summary>
        /// 行鼠标悬停效果
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowDataBound(System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                if (!mouseHoverColor.IsNullOrEmpty())
                {
                    e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='" + mouseHoverColor + "';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
                }
            }
        }

        /// <summary>
        /// 禁止字符串分行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.HeaderStyle.Wrap = false;
            this.RowStyle.Wrap = false;
        }

        #endregion
    }
}
