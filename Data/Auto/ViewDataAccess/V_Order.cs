using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class V_Order
    {
        private const string VIEW_NAME = "V_Order";
        private const string ALL_FIELDS = "OrderNum,OrderType,OrderStatus,GroupID,OrderHistory,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,LineName,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,InnerPrice,OuterPrice,Deposit,GruopLeader,TravelGuide,PromotionName,CanChangeNum,TravelDays";
        private const string SQL_SELECT = "Select OrderNum,OrderType,OrderStatus,GroupID,OrderHistory,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,LineName,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,InnerPrice,OuterPrice,Deposit,GruopLeader,TravelGuide,PromotionName,CanChangeNum,TravelDays From V_Order Where ";
        private const string SQL_TOP="Select Top {0} OrderNum,OrderType,OrderStatus,GroupID,OrderHistory,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,LineName,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,InnerPrice,OuterPrice,Deposit,GruopLeader,TravelGuide,PromotionName,CanChangeNum,TravelDays From V_Order Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From V_Order Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";

        #region Ë½ÓÐ·½·¨
        private void fillModel(DbDataReader dr, V_OrderInfo model)
        {
                    model.OrderNum = dr.GetString(0);
                    model.OrderType = dr.GetString(1);
                    model.OrderStatus = dr.GetString(2);
                    model.GroupID = dr.GetInt32(3);
                    model.OrderHistory = dr.GetString(4);
                    model.LineID = dr.GetInt32(5);
                    model.TotalMoney = dr.GetDecimal(6);
                    model.MoneyPayed = dr.GetDecimal(7);
                    model.MoneyReturn = dr.GetDecimal(8);
                    model.BuyNum = dr.GetInt32(9);
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
                    if (!dr.IsDBNull(23))
                        model.LineName = dr.GetString(23);
                    if (!dr.IsDBNull(24))
                        model.GoDate = dr.GetDateTime(24);
                    if (!dr.IsDBNull(25))
                        model.BackDate = dr.GetDateTime(25);
                    if (!dr.IsDBNull(26))
                        model.GatheringTime = dr.GetString(26);
                    if (!dr.IsDBNull(27))
                        model.GatheringPlace = dr.GetString(27);
                    if (!dr.IsDBNull(28))
                        model.TransferPlace = dr.GetString(28);
                    if (!dr.IsDBNull(29))
                        model.InnerPrice = dr.GetDecimal(29);
                    if (!dr.IsDBNull(30))
                        model.OuterPrice = dr.GetDecimal(30);
                    if (!dr.IsDBNull(31))
                        model.Deposit = dr.GetDecimal(31);
                    if (!dr.IsDBNull(32))
                        model.GruopLeader = dr.GetString(32);
                    if (!dr.IsDBNull(33))
                        model.TravelGuide = dr.GetString(33);
                    if (!dr.IsDBNull(34))
                        model.PromotionName = dr.GetString(34);
                    model.CanChangeNum = dr.GetInt32(35);
                    if (!dr.IsDBNull(36))
                        model.TravelDays = dr.GetString(36);
        }

        #endregion

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public List<V_OrderInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_OrderInfo>(fillModel, strSql, paras);
        }

        public List<V_OrderInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<V_OrderInfo>(fillModel, strSql, paras);
        }

        public List<V_OrderInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<V_OrderInfo>(fillModel,strSql, paras);
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

        public List<V_OrderInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_OrderInfo>(fillModel, strSql, paras, true, tran);
        }

    }
}
