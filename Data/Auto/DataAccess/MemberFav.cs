using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class MemberFav
    {
        private const string TABLE_NAME = "MemberFav";
        private const string ALL_FIELDS = "ID,UserName,FavType,FavObjID,AddTime,ExtraFields";
        private const string SQL_ADD = "Insert Into MemberFav (UserName,FavType,FavObjID,AddTime,ExtraFields)Values(@UserName,@FavType,@FavObjID,@AddTime,@ExtraFields)";
        private const string SQL_UPDATE = "Update MemberFav Set UserName=@UserName,FavType=@FavType,FavObjID=@FavObjID,AddTime=@AddTime,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select ID,UserName,FavType,FavObjID,AddTime,ExtraFields From MemberFav Where ";
        private const string SQL_DELETE = "Delete From MemberFav Where ";
        private const string SQL_TOP="Select Top {0} ID,UserName,FavType,FavObjID,AddTime,ExtraFields From MemberFav Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From MemberFav Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region ˽�з���
        private DbParameter[] makeParameterForAdd(MemberFavInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@FavType",model.FavType),
                    Db.Helper.MakeInParameter("@FavObjID",model.FavObjID),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(MemberFavInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@FavType",model.FavType),
                    Db.Helper.MakeInParameter("@FavObjID",model.FavObjID),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, MemberFavInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.UserName = dr.GetString(1);
                    model.FavType = dr.GetString(2);
                    model.FavObjID = dr.GetString(3);
                    model.AddTime = dr.GetDateTime(4);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(5));
        }

        #endregion

        #region ��������
        public int Add(MemberFavInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(MemberFavInfo model)
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

        public MemberFavInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<MemberFavInfo>(fillModel, strSql, paras);
        }

        public List<MemberFavInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<MemberFavInfo>(fillModel, strSql, paras);
        }

        public List<MemberFavInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<MemberFavInfo>(fillModel, strSql, paras);
        }

        public List<MemberFavInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<MemberFavInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  �������
        public int Add(MemberFavInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(MemberFavInfo model, DbTransaction tran)
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

        public MemberFavInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<MemberFavInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<MemberFavInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<MemberFavInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
