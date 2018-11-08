using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using Arrow.Framework.Extensions;
using Arrow.Framework.DataAccess;

[assembly: TagPrefix("ArrowDataList", "ArrowDataList")]
namespace Arrow.Framework.WebControls
{
    /// <summary>
    /// 带分页DataList控件
    /// </summary>
    public class DataList : System.Web.UI.WebControls.DataList 
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
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!DesignMode)
            {
                pager.ShowPager(writer);
            }
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
