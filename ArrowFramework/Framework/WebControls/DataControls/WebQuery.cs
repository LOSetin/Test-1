using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arrow.Framework.Extensions;
using Arrow.Framework.DataAccess;

namespace Arrow.Framework.WebControls
{
    public class WebQuery
    {
        #region 字段
        private string tableName = ""; //要查询的数据表。
        private string fields = "*"; //要查询的字段，如id,title,content
        private string primaryKey = ""; //主键字段，用来排序，保证唯一。
        private bool ascending = false; //是否为升序排列。
        private string condition = ""; //查询的筛选条件，不包含where。
        private ControlSqlCreateType sqlCreateType = ControlSqlCreateType.RowNum;
        private string orderBy = "";
        #endregion

        #region 属性
        /// <summary>
        /// 要查询的表名或视图
        /// </summary>
        public string TableName { set { this.tableName = value; }      get { return this.tableName; } }

        /// <summary>
        /// 要查询的字段，如id,title,content
        /// </summary>
        public string Fields { set { this.fields = value; }   get { return this.fields; } }

        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey { set { this.primaryKey = value; } get { return this.primaryKey; } }

        /// <summary>
        /// 是否为升序排列
        /// </summary>
        public bool Ascending { set { this.ascending = value; } get { return this.ascending; } }

        /// <summary>
        /// 查询的筛选条件，不包含where
        /// </summary>
        public string Condition { set { this.condition = value; } get { return this.condition; } }

        /// <summary>
        /// 使用Top还是RowNum
        /// </summary>
        public ControlSqlCreateType SqlCreateType { set { this.sqlCreateType = value; } get { return this.sqlCreateType; } }

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { set { this.orderBy = value; } get { return this.orderBy; } }
        #endregion

        #region 公有方法
        /// <summary>
        /// 生成获取记录总数的sql语句
        /// </summary>
        /// <returns></returns>
        public string CreateGetRecordCountSql()
        {
            string strsql = "SELECT Count(1) FROM " + tableName;
            if (!string.IsNullOrEmpty(condition))
            {
                strsql += " WHERE " + condition;
            }
            return strsql;
        }

        /// <summary>
        /// 生成分页语句
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public string CreatePagerSql(string providerName, WebPager pager)
        {
            string sql = "";
            if (SqlCreateType == ControlSqlCreateType.RowNum)
            {
                if (providerName.IsEqualTo(DBProvider.SqlClient))
                {
                    sql = CreateSqlByRowNum(pager.PageIndex, pager.PageSize);
                }
                else if (providerName.IsEqualTo(DBProvider.OracleClient))
                {
                    sql = CreatePLSqlByRowNum(pager.PageIndex, pager.PageSize);
                }
            }
            else if (sqlCreateType == ControlSqlCreateType.TopN)
            {
                if (providerName.IsEqualTo(DBProvider.SqlClient))
                {
                    sql = CreatePagerSql(pager.RecordCount, pager.PageCount, pager.PageIndex, pager.PageSize);
                }
                else if (providerName.IsEqualTo(DBProvider.OracleClient))
                {
                    string orderBy = "";
                    string sort = " desc";
                    if (ascending) sort = " asc";
                    orderBy = primaryKey + sort;
                    sql = CreatePLSqlByRowNum(pager.PageIndex, pager.PageSize);
                }
            }
            return sql;
        }

        #endregion

        #region 私有方法
        private string GetSortType(bool ascending)
        {
            string strSort = "";
            if (ascending)
            {
                strSort = "ASC";
            }
            else
            {
                strSort = "DESC";
            }
            return strSort;
        }

        /// <summary>
        /// 生成高效分页sql语句，使用Top N
        /// </summary>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageCount">分页总数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        private string CreatePagerSql(int recordCount, int pageCount, int pageIndex, int pageSize)
        {
            StringBuilder sb = new StringBuilder();

            int middleIndex = 1;
            middleIndex = pageCount / 2;//中间页的索引

            int firstIndex = 1; //第一页的索引
            int lastIndex = pageCount; //最后一页的索引
            if (pageIndex <= firstIndex)
            {

                sb.Append("SELECT TOP ").Append(pageSize).Append(" ")
                .Append(fields).Append(" FROM ").Append(tableName);
                if (condition != String.Empty)
                    sb.Append(" WHERE ").Append(condition);
                sb.Append(" ORDER BY ").Append(primaryKey).Append(" ")
                .Append(GetSortType(ascending));

            }
            else if (pageIndex > firstIndex && pageIndex <= middleIndex)
            {
                sb.Append("SELECT TOP ").Append(pageSize).Append(" ")
                .Append(fields).Append(" FROM ").Append(tableName)
                .Append(" WHERE ").Append(primaryKey);
                if (ascending)
                    sb.Append(" > (").Append(" SELECT MAX(");
                else
                    sb.Append(" < (").Append(" SELECT MIN(");
                sb.Append(primaryKey).Append(") FROM ( SELECT TOP ")
                .Append(pageSize * (pageIndex - 1)).Append(" ").Append(primaryKey)
                .Append(" FROM ").Append(tableName);
                if (condition != String.Empty)
                    sb.Append(" WHERE ").Append(condition);
                sb.Append(" ORDER BY ").Append(primaryKey).Append(" ")
                .Append(GetSortType(ascending)).Append(" ) TableA )");
                if (condition != String.Empty)
                    sb.Append(" AND ").Append(condition);
                sb.Append(" ORDER BY ").Append(primaryKey).Append(" ")
                .Append(GetSortType(ascending));

            }

            else if (pageIndex > middleIndex && pageIndex < lastIndex)
            {
                sb.Append(" SELECT * FROM ( ")
                .Append("SELECT TOP ").Append(pageSize).Append(" ")
                .Append(fields).Append(" FROM ").Append(tableName)
                .Append(" WHERE ").Append(primaryKey);
                if (ascending)
                    sb.Append(" < (").Append(" SELECT MIN(");
                else
                    sb.Append(" > (").Append(" SELECT MAX(");
                sb.Append(primaryKey).Append(") FROM ( SELECT TOP ")
                .Append(recordCount - pageSize * pageIndex).Append(" ").Append(primaryKey)
                .Append(" FROM ").Append(tableName);
                if (condition != String.Empty)
                    sb.Append(" WHERE ").Append(condition);
                sb.Append(" ORDER BY ").Append(primaryKey).Append(" ")
                .Append(GetSortType(!ascending)).Append(" ) TableA )");
                if (condition != String.Empty)
                    sb.Append(" AND ").Append(condition);
                sb.Append(" ORDER BY ").Append(primaryKey).Append(" ").Append(GetSortType(!ascending));
                sb.Append(" ) TableB ORDER BY ").Append(primaryKey).Append(" ").Append(GetSortType(ascending));
            }
            else if (pageIndex >= lastIndex)
            {
                sb.Append("SELECT * FROM ( ")
                .Append(" SELECT TOP ").Append(recordCount - pageSize * (lastIndex - 1)).Append(" ").Append(fields)
                .Append(" FROM ").Append(tableName);
                if (condition != String.Empty)
                    sb.Append(" WHERE ").Append(condition);
                sb.Append(" ORDER BY ").Append(primaryKey).Append(" ").Append(GetSortType(!ascending))
                .Append(" ) TableA ORDER BY ").Append(primaryKey).Append(" ").Append(GetSortType(ascending));
            }

            return sb.ToString();

        }


        /// <summary>
        /// 生成RowNum分页Sql
        /// </summary>
        /// <param name="pageIndex">分页索引，从1开始</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        private string CreateSqlByRowNum(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (condition.IsNullOrEmpty()) condition = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string val = @"SELECT RowNum, {0} FROM (SELECT {0},ROW_NUMBER() OVER(ORDER BY {1}) as RowNum FROM {2} Where {3}) t where RowNum>{4} and RowNum<={5} ";
            val = string.Format(val, fields, orderBy, tableName, condition, min, max);
            return val;
        }

        /// <summary>
        /// 生成RowNum分页PLSql
        /// </summary>
        /// <param name="pageIndex">分页索引，从1开始</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        private string CreatePLSqlByRowNum(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(condition))
            {
                condition = "1=1";
            }

            string sql = "SELECT * FROM(SELECT A.*, ROWNUM RN FROM (SELECT {0} FROM {1} WHERE {2} ORDER BY {3}) A WHERE ROWNUM <= {4})WHERE RN >= {5}";
            string max = (pageIndex * pageSize).ToString();
            string min = ((pageIndex - 1) * pageSize + 1).ToString();
            sql = string.Format(sql, fields, tableName, condition, orderBy, max, min);

            return sql;
        }
        #endregion

    }
}
