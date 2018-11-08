using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class Slider
    {
        private const string TABLE_NAME = "Slider";
        private const string ALL_FIELDS = "ID,Name,PicPath,Url,AddTime,AddUserName,SortOrder,IsTop,IsShow";
        private const string SQL_ADD = "Insert Into Slider (Name,PicPath,Url,AddTime,AddUserName,SortOrder,IsTop,IsShow)Values(@Name,@PicPath,@Url,@AddTime,@AddUserName,@SortOrder,@IsTop,@IsShow)";
        private const string SQL_UPDATE = "Update Slider Set Name=@Name,PicPath=@PicPath,Url=@Url,AddTime=@AddTime,AddUserName=@AddUserName,SortOrder=@SortOrder,IsTop=@IsTop,IsShow=@IsShow Where ";
        private const string SQL_SELECT = "Select ID,Name,PicPath,Url,AddTime,AddUserName,SortOrder,IsTop,IsShow From Slider Where ";
        private const string SQL_DELETE = "Delete From Slider Where ";
        private const string SQL_TOP="Select Top {0} ID,Name,PicPath,Url,AddTime,AddUserName,SortOrder,IsTop,IsShow From Slider Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From Slider Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(SliderInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@PicPath",model.PicPath),
                    Db.Helper.MakeInParameter("@Url",model.Url),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@SortOrder",model.SortOrder),
                    Db.Helper.MakeInParameter("@IsTop",model.IsTop),
                    Db.Helper.MakeInParameter("@IsShow",model.IsShow)
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(SliderInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@PicPath",model.PicPath),
                    Db.Helper.MakeInParameter("@Url",model.Url),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@SortOrder",model.SortOrder),
                    Db.Helper.MakeInParameter("@IsTop",model.IsTop),
                    Db.Helper.MakeInParameter("@IsShow",model.IsShow),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, SliderInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.PicPath = dr.GetString(2);
                    model.Url = dr.GetString(3);
                    model.AddTime = dr.GetDateTime(4);
                    model.AddUserName = dr.GetString(5);
                    model.SortOrder = dr.GetInt32(6);
                    model.IsTop = dr.GetInt32(7);
                    model.IsShow = dr.GetInt32(8);
        }

        #endregion

        #region 基本操作
        public int Add(SliderInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(SliderInfo model)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] paras = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(strSql, paras);
        }

        public int Delete(int iD)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.ExecuteNonQuery(strSql, paras);
        }

        public int DeleteList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_DELETE + strWhere;
            return Db.Helper.ExecuteNonQuery(strSql, paras);
        }

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public SliderInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<SliderInfo>(fillModel, strSql, paras);
        }

        public List<SliderInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<SliderInfo>(fillModel, strSql, paras);
        }

        public List<SliderInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<SliderInfo>(fillModel, strSql, paras);
        }

        public List<SliderInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<SliderInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(SliderInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(SliderInfo model, DbTransaction tran)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] para = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(tran, strSql, para);
        }

        public int Delete(int iD, DbTransaction tran)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.ExecuteNonQuery(tran, strSql, paras);
        }

        public int DeleteList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_DELETE + strWhere;
            return Db.Helper.ExecuteNonQuery(tran, strSql, paras);
        }

        public int GetCount(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(tran, strSql, paras));
            return sum;
        }

        public SliderInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<SliderInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<SliderInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<SliderInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
