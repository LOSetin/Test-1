using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class V_CostHistory
    {
        private const string VIEW_NAME = "V_CostHistory";
        private const string ALL_FIELDS = "ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,LineID,GroupID,BuyNum,PromotionID,LineName,GoDate,PromotionName,TotalMoney,MoneyPayed,ExchangeStatus";
        private const string SQL_SELECT = "Select ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,LineID,GroupID,BuyNum,PromotionID,LineName,GoDate,PromotionName,TotalMoney,MoneyPayed,ExchangeStatus From V_CostHistory Where ";
        private const string SQL_TOP="Select Top {0} ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,LineID,GroupID,BuyNum,PromotionID,LineName,GoDate,PromotionName,TotalMoney,MoneyPayed,ExchangeStatus From V_CostHistory Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From V_CostHistory Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";

        #region Ë½ÓÐ·½·¨
        private void fillModel(DbDataReader dr, V_CostHistoryInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.UserName = dr.GetString(1);
                    model.PointsBefore = dr.GetInt32(2);
                    model.PointsAfter = dr.GetInt32(3);
                    model.PointsCost = dr.GetInt32(4);
                    model.GoodsID = dr.GetInt32(5);
                    model.GoodsNum = dr.GetInt32(6);
                    model.GoodsName = dr.GetString(7);
                    model.OrderNum = dr.GetString(8);
                    model.MoneyCost = dr.GetDecimal(9);
                    model.MoneyBefore = dr.GetDecimal(10);
                    model.MoneyAfter = dr.GetDecimal(11);
                    model.CostType = dr.GetString(12);
                    model.Remarks = dr.GetString(13);
                    model.AddTime = dr.GetDateTime(14);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(15));
                    if (!dr.IsDBNull(16))
                        model.LineID = dr.GetInt32(16);
                    if (!dr.IsDBNull(17))
                        model.GroupID = dr.GetInt32(17);
                    if (!dr.IsDBNull(18))
                        model.BuyNum = dr.GetInt32(18);
                    if (!dr.IsDBNull(19))
                        model.PromotionID = dr.GetInt32(19);
                    if (!dr.IsDBNull(20))
                        model.LineName = dr.GetString(20);
                    if (!dr.IsDBNull(21))
                        model.GoDate = dr.GetDateTime(21);
                    if (!dr.IsDBNull(22))
                        model.PromotionName = dr.GetString(22);
                    if (!dr.IsDBNull(23))
                        model.TotalMoney = dr.GetDecimal(23);
                    if (!dr.IsDBNull(24))
                        model.MoneyPayed = dr.GetDecimal(24);
                    model.ExchangeStatus = dr.GetString(25);
        }

        #endregion

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public List<V_CostHistoryInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_CostHistoryInfo>(fillModel, strSql, paras);
        }

        public List<V_CostHistoryInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<V_CostHistoryInfo>(fillModel, strSql, paras);
        }

        public List<V_CostHistoryInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<V_CostHistoryInfo>(fillModel,strSql, paras);
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

        public List<V_CostHistoryInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_CostHistoryInfo>(fillModel, strSql, paras, true, tran);
        }

    }
}
