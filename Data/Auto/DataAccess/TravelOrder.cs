using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class TravelOrder
    {
        private const string TABLE_NAME = "TravelOrder";
        private const string ALL_FIELDS = "OrderNum,OrderType,OrderStatus,GroupID,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,CanChangeNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,OrderHistory";
        private const string SQL_ADD = "Insert Into TravelOrder (OrderNum,OrderType,OrderStatus,GroupID,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,CanChangeNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,OrderHistory)Values(@OrderNum,@OrderType,@OrderStatus,@GroupID,@LineID,@TotalMoney,@MoneyPayed,@MoneyReturn,@BuyNum,@CanChangeNum,@PromotionID,@CompanyRemarks,@AddTime,@AddMemberName,@AddMemberMobile,@AddMemberRealName,@AddMemberRemarks,@InviteNum,@InviterUserName,@InviterRealName,@OperatorUserName,@OperatorRealName,@ExtraFields,@OrderHistory)";
        private const string SQL_UPDATE = "Update TravelOrder Set OrderType=@OrderType,OrderStatus=@OrderStatus,GroupID=@GroupID,LineID=@LineID,TotalMoney=@TotalMoney,MoneyPayed=@MoneyPayed,MoneyReturn=@MoneyReturn,BuyNum=@BuyNum,CanChangeNum=@CanChangeNum,PromotionID=@PromotionID,CompanyRemarks=@CompanyRemarks,AddTime=@AddTime,AddMemberName=@AddMemberName,AddMemberMobile=@AddMemberMobile,AddMemberRealName=@AddMemberRealName,AddMemberRemarks=@AddMemberRemarks,InviteNum=@InviteNum,InviterUserName=@InviterUserName,InviterRealName=@InviterRealName,OperatorUserName=@OperatorUserName,OperatorRealName=@OperatorRealName,ExtraFields=@ExtraFields,OrderHistory=@OrderHistory Where ";
        private const string SQL_SELECT = "Select OrderNum,OrderType,OrderStatus,GroupID,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,CanChangeNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,OrderHistory From TravelOrder Where ";
        private const string SQL_DELETE = "Delete From TravelOrder Where ";
        private const string SQL_TOP="Select Top {0} OrderNum,OrderType,OrderStatus,GroupID,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,CanChangeNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,OrderHistory From TravelOrder Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From TravelOrder Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "OrderNum = @OrderNum";
        private const string PK_PARA = "@OrderNum";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(TravelOrderInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@OrderNum",model.OrderNum),
                    Db.Helper.MakeInParameter("@OrderType",model.OrderType),
                    Db.Helper.MakeInParameter("@OrderStatus",model.OrderStatus),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@TotalMoney",model.TotalMoney),
                    Db.Helper.MakeInParameter("@MoneyPayed",model.MoneyPayed),
                    Db.Helper.MakeInParameter("@MoneyReturn",model.MoneyReturn),
                    Db.Helper.MakeInParameter("@BuyNum",model.BuyNum),
                    Db.Helper.MakeInParameter("@CanChangeNum",model.CanChangeNum),
                    Db.Helper.MakeInParameter("@PromotionID",model.PromotionID),
                    Db.Helper.MakeInParameter("@CompanyRemarks",model.CompanyRemarks),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@AddMemberName",model.AddMemberName),
                    Db.Helper.MakeInParameter("@AddMemberMobile",model.AddMemberMobile),
                    Db.Helper.MakeInParameter("@AddMemberRealName",model.AddMemberRealName),
                    Db.Helper.MakeInParameter("@AddMemberRemarks",model.AddMemberRemarks),
                    Db.Helper.MakeInParameter("@InviteNum",model.InviteNum),
                    Db.Helper.MakeInParameter("@InviterUserName",model.InviterUserName),
                    Db.Helper.MakeInParameter("@InviterRealName",model.InviterRealName),
                    Db.Helper.MakeInParameter("@OperatorUserName",model.OperatorUserName),
                    Db.Helper.MakeInParameter("@OperatorRealName",model.OperatorRealName),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@OrderHistory",model.OrderHistory)
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(TravelOrderInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@OrderType",model.OrderType),
                    Db.Helper.MakeInParameter("@OrderStatus",model.OrderStatus),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@TotalMoney",model.TotalMoney),
                    Db.Helper.MakeInParameter("@MoneyPayed",model.MoneyPayed),
                    Db.Helper.MakeInParameter("@MoneyReturn",model.MoneyReturn),
                    Db.Helper.MakeInParameter("@BuyNum",model.BuyNum),
                    Db.Helper.MakeInParameter("@CanChangeNum",model.CanChangeNum),
                    Db.Helper.MakeInParameter("@PromotionID",model.PromotionID),
                    Db.Helper.MakeInParameter("@CompanyRemarks",model.CompanyRemarks),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@AddMemberName",model.AddMemberName),
                    Db.Helper.MakeInParameter("@AddMemberMobile",model.AddMemberMobile),
                    Db.Helper.MakeInParameter("@AddMemberRealName",model.AddMemberRealName),
                    Db.Helper.MakeInParameter("@AddMemberRemarks",model.AddMemberRemarks),
                    Db.Helper.MakeInParameter("@InviteNum",model.InviteNum),
                    Db.Helper.MakeInParameter("@InviterUserName",model.InviterUserName),
                    Db.Helper.MakeInParameter("@InviterRealName",model.InviterRealName),
                    Db.Helper.MakeInParameter("@OperatorUserName",model.OperatorUserName),
                    Db.Helper.MakeInParameter("@OperatorRealName",model.OperatorRealName),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@OrderHistory",model.OrderHistory),
                    Db.Helper.MakeInParameter("@OrderNum",model.OrderNum)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, TravelOrderInfo model)
        {
                    model.OrderNum = dr.GetString(0);
                    model.OrderType = dr.GetString(1);
                    model.OrderStatus = dr.GetString(2);
                    model.GroupID = dr.GetInt32(3);
                    model.LineID = dr.GetInt32(4);
                    model.TotalMoney = dr.GetDecimal(5);
                    model.MoneyPayed = dr.GetDecimal(6);
                    model.MoneyReturn = dr.GetDecimal(7);
                    model.BuyNum = dr.GetInt32(8);
                    model.CanChangeNum = dr.GetInt32(9);
                    model.PromotionID = dr.GetInt32(10);
                    model.CompanyRemarks = dr.GetString(11);
                    model.AddTime = dr.GetDateTime(12);
                    model.AddMemberName = dr.GetString(13);
                    model.AddMemberMobile = dr.GetString(14);
                    model.AddMemberRealName = dr.GetString(15);
                    model.AddMemberRemarks = dr.GetString(16);
                    model.InviteNum = dr.GetString(17);
                    model.InviterUserName = dr.GetString(18);
                    model.InviterRealName = dr.GetString(19);
                    model.OperatorUserName = dr.GetString(20);
                    model.OperatorRealName = dr.GetString(21);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(22));
                    model.OrderHistory = dr.GetString(23);
        }

        #endregion

        #region 基本操作
        public int Add(TravelOrderInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(TravelOrderInfo model)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] paras = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(strSql, paras);
        }

        public int Delete(string orderNum)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, orderNum) };
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

        public TravelOrderInfo Select(string orderNum)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, orderNum) };
            return Db.Helper.GetModel<TravelOrderInfo>(fillModel, strSql, paras);
        }

        public List<TravelOrderInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<TravelOrderInfo>(fillModel, strSql, paras);
        }

        public List<TravelOrderInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<TravelOrderInfo>(fillModel, strSql, paras);
        }

        public List<TravelOrderInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<TravelOrderInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(TravelOrderInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(TravelOrderInfo model, DbTransaction tran)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] para = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(tran, strSql, para);
        }

        public int Delete(string orderNum, DbTransaction tran)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, orderNum) };
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

        public TravelOrderInfo Select(string orderNum, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, orderNum) };
            return Db.Helper.GetModel<TravelOrderInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<TravelOrderInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<TravelOrderInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
