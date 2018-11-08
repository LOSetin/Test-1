using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class PicWall
    {
        private const string TABLE_NAME = "PicWall";
        private const string ALL_FIELDS = "ID,Name,CoverPath,BigPicPath,Remarks,AddUserName,AddUserRealName,AddTime,SortOrder,IsTop,ExtraFields";
        private const string SQL_ADD = "Insert Into PicWall (Name,CoverPath,BigPicPath,Remarks,AddUserName,AddUserRealName,AddTime,SortOrder,IsTop,ExtraFields)Values(@Name,@CoverPath,@BigPicPath,@Remarks,@AddUserName,@AddUserRealName,@AddTime,@SortOrder,@IsTop,@ExtraFields)";
        private const string SQL_UPDATE = "Update PicWall Set Name=@Name,CoverPath=@CoverPath,BigPicPath=@BigPicPath,Remarks=@Remarks,AddUserName=@AddUserName,AddUserRealName=@AddUserRealName,AddTime=@AddTime,SortOrder=@SortOrder,IsTop=@IsTop,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select ID,Name,CoverPath,BigPicPath,Remarks,AddUserName,AddUserRealName,AddTime,SortOrder,IsTop,ExtraFields From PicWall Where ";
        private const string SQL_DELETE = "Delete From PicWall Where ";
        private const string SQL_TOP="Select Top {0} ID,Name,CoverPath,BigPicPath,Remarks,AddUserName,AddUserRealName,AddTime,SortOrder,IsTop,ExtraFields From PicWall Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From PicWall Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(PicWallInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@CoverPath",model.CoverPath),
                    Db.Helper.MakeInParameter("@BigPicPath",model.BigPicPath),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@SortOrder",model.SortOrder),
                    Db.Helper.MakeInParameter("@IsTop",model.IsTop),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(PicWallInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@CoverPath",model.CoverPath),
                    Db.Helper.MakeInParameter("@BigPicPath",model.BigPicPath),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@SortOrder",model.SortOrder),
                    Db.Helper.MakeInParameter("@IsTop",model.IsTop),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, PicWallInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.CoverPath = dr.GetString(2);
                    model.BigPicPath = dr.GetString(3);
                    model.Remarks = dr.GetString(4);
                    model.AddUserName = dr.GetString(5);
                    model.AddUserRealName = dr.GetString(6);
                    model.AddTime = dr.GetDateTime(7);
                    model.SortOrder = dr.GetInt32(8);
                    model.IsTop = dr.GetInt32(9);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(10));
        }

        #endregion

        #region 基本操作
        public int Add(PicWallInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(PicWallInfo model)
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

        public PicWallInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<PicWallInfo>(fillModel, strSql, paras);
        }

        public List<PicWallInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<PicWallInfo>(fillModel, strSql, paras);
        }

        public List<PicWallInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<PicWallInfo>(fillModel, strSql, paras);
        }

        public List<PicWallInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<PicWallInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(PicWallInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(PicWallInfo model, DbTransaction tran)
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

        public PicWallInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<PicWallInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<PicWallInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<PicWallInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
