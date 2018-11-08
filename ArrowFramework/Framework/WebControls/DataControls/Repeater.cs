using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Arrow.Framework.Extensions;
using Arrow.Framework.DataAccess;

[assembly: TagPrefix("ArrowRepeater", "ArrowRepeater")]
namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 带分页Repeater控件
    /// </summary>
    public class Repeater : System.Web.UI.WebControls.Repeater
    {
        #region 定义字段
        private WebQuery query;
        private Database db;
        private WebPager pager;
        #endregion

        #region 属性定义
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

        #endregion

        #region 重写

        /// <summary>
        /// 呈现控件
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            //当水平位置不够时，显示滚动条
            writer.WriteLine("<div style=\"margin-left:10px;margin-right:10px;overflow-x:auto\">");
            base.Render(writer);
            if (!DesignMode)
            {
                pager.ShowPager(writer);
            }
            writer.WriteLine("</div>");
        }

        public override void DataBind()
        {
            if (!DesignMode)
            {
                string recordCountSql = query.CreateGetRecordCountSql();
                this.pager.RecordCount = db.ExecuteScalar(recordCountSql).ToArrowInt();
                pager.CaculatePageCount();
                string pageSql = query.CreatePagerSql(db.ProviderName, pager);
                this.DataSource = db.ExecuteDataSet(pageSql);
            }
            base.DataBind();
        }
        #endregion

    }
}
