using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class ShopingCar
    {
        private const string TABLE_NAME = "ShopingCar";
        private const string ALL_FIELDS = "ID,UserName,LineID,LineName,GroupID,GoDate,BackDate,PromotionID,PromotionName,BuyNum";
        private const string SQL_ADD = "Insert Into ShopingCar (UserName,LineID,LineName,GroupID,GoDate,BackDate,PromotionID,PromotionName,BuyNum)Values(@UserName,@LineID,@LineName,@GroupID,@GoDate,@BackDate,@PromotionID,@PromotionName,@BuyNum)";
        private const string SQL_UPDATE = "Update ShopingCar Set UserName=@UserName,LineID=@LineID,LineName=@LineName,GroupID=@GroupID,GoDate=@GoDate,BackDate=@BackDate,PromotionID=@PromotionID,PromotionName=@PromotionName,BuyNum=@BuyNum Where ";
        private const string SQL_SELECT = "Select ID,UserName,LineID,LineName,GroupID,GoDate,BackDate,PromotionID,PromotionName,BuyNum From ShopingCar Where ";
        private const string SQL_DELETE = "Delete From ShopingCar Where ";
        private const string SQL_TOP="Select Top {0} ID,UserName,LineID,LineName,GroupID,GoDate,BackDate,PromotionID,PromotionName,BuyNum From ShopingCar Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From ShopingCar Where ";
        private const string SQL_PAGE="Select RowNum, {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(ShopingCarInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@LineName",model.LineName),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@GoDate",model.GoDate),
                    Db.Helper.MakeInParameter("@BackDate",model.BackDate),
                    Db.Helper.MakeInParameter("@PromotionID",model.PromotionID),
                    Db.Helper.MakeInParameter("@PromotionName",model.PromotionName),
                    Db.Helper.MakeInParameter("@BuyNum",model.BuyNum)
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(ShopingCarInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@LineName",model.LineName),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@GoDate",model.GoDate),
                    Db.Helper.MakeInParameter("@BackDate",model.BackDate),
                    Db.Helper.MakeInParameter("@PromotionID",model.PromotionID),
                    Db.Helper.MakeInParameter("@PromotionName",model.PromotionName),
                    Db.Helper.MakeInParameter("@BuyNum",model.BuyNum),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, ShopingCarInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.UserName = dr.GetString(1);
                    model.LineID = dr.GetInt32(2);
                    model.LineName = dr.GetString(3);
                    model.GroupID = dr.GetInt32(4);
                    model.GoDate = dr.GetDateTime(5);
                    model.BackDate = dr.GetDateTime(6);
                    model.PromotionID = dr.GetInt32(7);
                    model.PromotionName = dr.GetString(8);
                    model.BuyNum = dr.GetInt32(9);
        }

        #endregion

        #region 基本操作
        public int Add(ShopingCarInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(ShopingCarInfo model)
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

        public ShopingCarInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<ShopingCarInfo>(fillModel, strSql, paras);
        }

        public List<ShopingCarInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<ShopingCarInfo>(fillModel, strSql, paras);
        }

        public List<ShopingCarInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<ShopingCarInfo>(fillModel, strSql, paras);
        }

        public List<ShopingCarInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<ShopingCarInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(ShopingCarInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(ShopingCarInfo model, DbTransaction tran)
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

        public ShopingCarInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<ShopingCarInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<ShopingCarInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<ShopingCarInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
