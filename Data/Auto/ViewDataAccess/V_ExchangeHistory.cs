using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class V_ExchangeHistory
    {
        private const string VIEW_NAME = "V_ExchangeHistory";
        private const string ALL_FIELDS = "ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,CoverPath,Points,Num,GoodsName,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone";
        private const string SQL_SELECT = "Select ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,CoverPath,Points,Num,GoodsName,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone From V_ExchangeHistory Where ";
        private const string SQL_TOP="Select Top {0} ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,CoverPath,Points,Num,GoodsName,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone From V_ExchangeHistory Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From V_ExchangeHistory Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";

        #region Ë½ÓÐ·½·¨
        private void fillModel(DbDataReader dr, V_ExchangeHistoryInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.UserName = dr.GetString(1);
                    model.PointsBefore = dr.GetInt32(2);
                    model.PointsAfter = dr.GetInt32(3);
                    model.PointsCost = dr.GetInt32(4);
                    model.GoodsID = dr.GetInt32(5);
                    model.GoodsNum = dr.GetInt32(6);
                    model.OrderNum = dr.GetString(7);
                    model.MoneyCost = dr.GetDecimal(8);
                    model.MoneyBefore = dr.GetDecimal(9);
                    model.MoneyAfter = dr.GetDecimal(10);
                    model.CostType = dr.GetString(11);
                    model.Remarks = dr.GetString(12);
                    model.AddTime = dr.GetDateTime(13);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(14));
                    model.CoverPath = dr.GetString(15);
                    model.Points = dr.GetInt32(16);
                    model.Num = dr.GetInt32(17);
                    model.GoodsName = dr.GetString(18);
                    model.ExchangeStatus = dr.GetString(19);
                    model.ExpressName = dr.GetString(20);
                    model.ExpressNum = dr.GetString(21);
                    model.SendTime = dr.GetDateTime(22);
                    model.FinishTime = dr.GetDateTime(23);
                    model.LinkMan = dr.GetString(24);
                    model.LinkAddress = dr.GetString(25);
                    model.LinkPhone = dr.GetString(26);
        }

        #endregion

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public List<V_ExchangeHistoryInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_ExchangeHistoryInfo>(fillModel, strSql, paras);
        }

        public List<V_ExchangeHistoryInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<V_ExchangeHistoryInfo>(fillModel, strSql, paras);
        }

        public List<V_ExchangeHistoryInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<V_ExchangeHistoryInfo>(fillModel,strSql, paras);
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

        public List<V_ExchangeHistoryInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_ExchangeHistoryInfo>(fillModel, strSql, paras, true, tran);
        }

    }
}
