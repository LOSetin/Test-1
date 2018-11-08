using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class CostHistory
    {
        private const string TABLE_NAME = "CostHistory";
        private const string ALL_FIELDS = "ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone";
        private const string SQL_ADD = "Insert Into CostHistory (UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone)Values(@UserName,@PointsBefore,@PointsAfter,@PointsCost,@GoodsID,@GoodsNum,@GoodsName,@OrderNum,@MoneyCost,@MoneyBefore,@MoneyAfter,@CostType,@Remarks,@AddTime,@ExtraFields,@ExchangeStatus,@ExpressName,@ExpressNum,@SendTime,@FinishTime,@LinkMan,@LinkAddress,@LinkPhone)";
        private const string SQL_UPDATE = "Update CostHistory Set UserName=@UserName,PointsBefore=@PointsBefore,PointsAfter=@PointsAfter,PointsCost=@PointsCost,GoodsID=@GoodsID,GoodsNum=@GoodsNum,GoodsName=@GoodsName,OrderNum=@OrderNum,MoneyCost=@MoneyCost,MoneyBefore=@MoneyBefore,MoneyAfter=@MoneyAfter,CostType=@CostType,Remarks=@Remarks,AddTime=@AddTime,ExtraFields=@ExtraFields,ExchangeStatus=@ExchangeStatus,ExpressName=@ExpressName,ExpressNum=@ExpressNum,SendTime=@SendTime,FinishTime=@FinishTime,LinkMan=@LinkMan,LinkAddress=@LinkAddress,LinkPhone=@LinkPhone Where ";
        private const string SQL_SELECT = "Select ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone From CostHistory Where ";
        private const string SQL_DELETE = "Delete From CostHistory Where ";
        private const string SQL_TOP="Select Top {0} ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone From CostHistory Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From CostHistory Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(CostHistoryInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@PointsBefore",model.PointsBefore),
                    Db.Helper.MakeInParameter("@PointsAfter",model.PointsAfter),
                    Db.Helper.MakeInParameter("@PointsCost",model.PointsCost),
                    Db.Helper.MakeInParameter("@GoodsID",model.GoodsID),
                    Db.Helper.MakeInParameter("@GoodsNum",model.GoodsNum),
                    Db.Helper.MakeInParameter("@GoodsName",model.GoodsName),
                    Db.Helper.MakeInParameter("@OrderNum",model.OrderNum),
                    Db.Helper.MakeInParameter("@MoneyCost",model.MoneyCost),
                    Db.Helper.MakeInParameter("@MoneyBefore",model.MoneyBefore),
                    Db.Helper.MakeInParameter("@MoneyAfter",model.MoneyAfter),
                    Db.Helper.MakeInParameter("@CostType",model.CostType),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ExchangeStatus",model.ExchangeStatus),
                    Db.Helper.MakeInParameter("@ExpressName",model.ExpressName),
                    Db.Helper.MakeInParameter("@ExpressNum",model.ExpressNum),
                    Db.Helper.MakeInParameter("@SendTime",model.SendTime),
                    Db.Helper.MakeInParameter("@FinishTime",model.FinishTime),
                    Db.Helper.MakeInParameter("@LinkMan",model.LinkMan),
                    Db.Helper.MakeInParameter("@LinkAddress",model.LinkAddress),
                    Db.Helper.MakeInParameter("@LinkPhone",model.LinkPhone)
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(CostHistoryInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@PointsBefore",model.PointsBefore),
                    Db.Helper.MakeInParameter("@PointsAfter",model.PointsAfter),
                    Db.Helper.MakeInParameter("@PointsCost",model.PointsCost),
                    Db.Helper.MakeInParameter("@GoodsID",model.GoodsID),
                    Db.Helper.MakeInParameter("@GoodsNum",model.GoodsNum),
                    Db.Helper.MakeInParameter("@GoodsName",model.GoodsName),
                    Db.Helper.MakeInParameter("@OrderNum",model.OrderNum),
                    Db.Helper.MakeInParameter("@MoneyCost",model.MoneyCost),
                    Db.Helper.MakeInParameter("@MoneyBefore",model.MoneyBefore),
                    Db.Helper.MakeInParameter("@MoneyAfter",model.MoneyAfter),
                    Db.Helper.MakeInParameter("@CostType",model.CostType),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ExchangeStatus",model.ExchangeStatus),
                    Db.Helper.MakeInParameter("@ExpressName",model.ExpressName),
                    Db.Helper.MakeInParameter("@ExpressNum",model.ExpressNum),
                    Db.Helper.MakeInParameter("@SendTime",model.SendTime),
                    Db.Helper.MakeInParameter("@FinishTime",model.FinishTime),
                    Db.Helper.MakeInParameter("@LinkMan",model.LinkMan),
                    Db.Helper.MakeInParameter("@LinkAddress",model.LinkAddress),
                    Db.Helper.MakeInParameter("@LinkPhone",model.LinkPhone),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, CostHistoryInfo model)
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
                    model.ExchangeStatus = dr.GetString(16);
                    model.ExpressName = dr.GetString(17);
                    model.ExpressNum = dr.GetString(18);
                    model.SendTime = dr.GetDateTime(19);
                    model.FinishTime = dr.GetDateTime(20);
                    model.LinkMan = dr.GetString(21);
                    model.LinkAddress = dr.GetString(22);
                    model.LinkPhone = dr.GetString(23);
        }

        #endregion

        #region 基本操作
        public int Add(CostHistoryInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(CostHistoryInfo model)
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

        public CostHistoryInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<CostHistoryInfo>(fillModel, strSql, paras);
        }

        public List<CostHistoryInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<CostHistoryInfo>(fillModel, strSql, paras);
        }

        public List<CostHistoryInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<CostHistoryInfo>(fillModel, strSql, paras);
        }

        public List<CostHistoryInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<CostHistoryInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(CostHistoryInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(CostHistoryInfo model, DbTransaction tran)
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

        public CostHistoryInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<CostHistoryInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<CostHistoryInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<CostHistoryInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
