using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class V_Goods
    {
        private const string VIEW_NAME = "V_Goods";
        private const string ALL_FIELDS = "ID,Name,CatID,Num,Points,CoverPath,BigPicPath,IsOut,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields,CatName";
        private const string SQL_SELECT = "Select ID,Name,CatID,Num,Points,CoverPath,BigPicPath,IsOut,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields,CatName From V_Goods Where ";
        private const string SQL_TOP="Select Top {0} ID,Name,CatID,Num,Points,CoverPath,BigPicPath,IsOut,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields,CatName From V_Goods Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From V_Goods Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";

        #region Ë½ÓÐ·½·¨
        private void fillModel(DbDataReader dr, V_GoodsInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.CatID = dr.GetInt32(2);
                    model.Num = dr.GetInt32(3);
                    model.Points = dr.GetInt32(4);
                    model.CoverPath = dr.GetString(5);
                    model.BigPicPath = dr.GetString(6);
                    model.IsOut = dr.GetInt32(7);
                    model.Remarks = dr.GetString(8);
                    model.AddUserName = dr.GetString(9);
                    model.AddUserRealName = dr.GetString(10);
                    model.AddTime = dr.GetDateTime(11);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(12));
                    model.CatName = dr.GetString(13);
        }

        #endregion

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public List<V_GoodsInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_GoodsInfo>(fillModel, strSql, paras);
        }

        public List<V_GoodsInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<V_GoodsInfo>(fillModel, strSql, paras);
        }

        public List<V_GoodsInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<V_GoodsInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }

        public int GetCount(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(tran, strSql, paras));
            return sum;
        }

        public List<V_GoodsInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_GoodsInfo>(fillModel, strSql, paras, true, tran);
        }

    }
}
