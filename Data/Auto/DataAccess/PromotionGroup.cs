using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class PromotionGroup
    {
        private const string TABLE_NAME = "PromotionGroup";
        private const string ALL_FIELDS = "ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,RemainNum,RawInnerPrice,RawOuterPrice,BeginTime,AddTime,ExtraFields";
        private const string SQL_ADD = "Insert Into PromotionGroup (PromotionID,GroupID,LineID,TotalNum,SelledNum,RemainNum,RawInnerPrice,RawOuterPrice,BeginTime,AddTime,ExtraFields)Values(@PromotionID,@GroupID,@LineID,@TotalNum,@SelledNum,@RemainNum,@RawInnerPrice,@RawOuterPrice,@BeginTime,@AddTime,@ExtraFields)";
        private const string SQL_UPDATE = "Update PromotionGroup Set PromotionID=@PromotionID,GroupID=@GroupID,LineID=@LineID,TotalNum=@TotalNum,SelledNum=@SelledNum,RemainNum=@RemainNum,RawInnerPrice=@RawInnerPrice,RawOuterPrice=@RawOuterPrice,BeginTime=@BeginTime,AddTime=@AddTime,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,RemainNum,RawInnerPrice,RawOuterPrice,BeginTime,AddTime,ExtraFields From PromotionGroup Where ";
        private const string SQL_DELETE = "Delete From PromotionGroup Where ";
        private const string SQL_TOP="Select Top {0} ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,RemainNum,RawInnerPrice,RawOuterPrice,BeginTime,AddTime,ExtraFields From PromotionGroup Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From PromotionGroup Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(PromotionGroupInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@PromotionID",model.PromotionID),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@TotalNum",model.TotalNum),
                    Db.Helper.MakeInParameter("@SelledNum",model.SelledNum),
                    Db.Helper.MakeInParameter("@RemainNum",model.RemainNum),
                    Db.Helper.MakeInParameter("@RawInnerPrice",model.RawInnerPrice),
                    Db.Helper.MakeInParameter("@RawOuterPrice",model.RawOuterPrice),
                    Db.Helper.MakeInParameter("@BeginTime",model.BeginTime),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(PromotionGroupInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@PromotionID",model.PromotionID),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@TotalNum",model.TotalNum),
                    Db.Helper.MakeInParameter("@SelledNum",model.SelledNum),
                    Db.Helper.MakeInParameter("@RemainNum",model.RemainNum),
                    Db.Helper.MakeInParameter("@RawInnerPrice",model.RawInnerPrice),
                    Db.Helper.MakeInParameter("@RawOuterPrice",model.RawOuterPrice),
                    Db.Helper.MakeInParameter("@BeginTime",model.BeginTime),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, PromotionGroupInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.PromotionID = dr.GetInt32(1);
                    model.GroupID = dr.GetInt32(2);
                    model.LineID = dr.GetInt32(3);
                    model.TotalNum = dr.GetInt32(4);
                    model.SelledNum = dr.GetInt32(5);
                    model.RemainNum = dr.GetInt32(6);
                    model.RawInnerPrice = dr.GetDecimal(7);
                    model.RawOuterPrice = dr.GetDecimal(8);
                    model.BeginTime = dr.GetDateTime(9);
                    model.AddTime = dr.GetDateTime(10);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(11));
        }

        #endregion

        #region 基本操作
        public int Add(PromotionGroupInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(PromotionGroupInfo model)
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

        public PromotionGroupInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<PromotionGroupInfo>(fillModel, strSql, paras);
        }

        public List<PromotionGroupInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<PromotionGroupInfo>(fillModel, strSql, paras);
        }

        public List<PromotionGroupInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<PromotionGroupInfo>(fillModel, strSql, paras);
        }

        public List<PromotionGroupInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<PromotionGroupInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(PromotionGroupInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(PromotionGroupInfo model, DbTransaction tran)
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

        public PromotionGroupInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<PromotionGroupInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<PromotionGroupInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<PromotionGroupInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
