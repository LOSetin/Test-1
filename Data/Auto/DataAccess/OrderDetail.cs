using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class OrderDetail
    {
        private const string TABLE_NAME = "OrderDetail";
        private const string ALL_FIELDS = "ID,OrderNum,GroupID,LineID,AddMemberName,RealName,Sex,IDNum,MobileNum,AddTime,Remarks,ExtraFields";
        private const string SQL_ADD = "Insert Into OrderDetail (OrderNum,GroupID,LineID,AddMemberName,RealName,Sex,IDNum,MobileNum,AddTime,Remarks,ExtraFields)Values(@OrderNum,@GroupID,@LineID,@AddMemberName,@RealName,@Sex,@IDNum,@MobileNum,@AddTime,@Remarks,@ExtraFields)";
        private const string SQL_UPDATE = "Update OrderDetail Set OrderNum=@OrderNum,GroupID=@GroupID,LineID=@LineID,AddMemberName=@AddMemberName,RealName=@RealName,Sex=@Sex,IDNum=@IDNum,MobileNum=@MobileNum,AddTime=@AddTime,Remarks=@Remarks,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select ID,OrderNum,GroupID,LineID,AddMemberName,RealName,Sex,IDNum,MobileNum,AddTime,Remarks,ExtraFields From OrderDetail Where ";
        private const string SQL_DELETE = "Delete From OrderDetail Where ";
        private const string SQL_TOP="Select Top {0} ID,OrderNum,GroupID,LineID,AddMemberName,RealName,Sex,IDNum,MobileNum,AddTime,Remarks,ExtraFields From OrderDetail Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From OrderDetail Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(OrderDetailInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@OrderNum",model.OrderNum),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@AddMemberName",model.AddMemberName),
                    Db.Helper.MakeInParameter("@RealName",model.RealName),
                    Db.Helper.MakeInParameter("@Sex",model.Sex),
                    Db.Helper.MakeInParameter("@IDNum",model.IDNum),
                    Db.Helper.MakeInParameter("@MobileNum",model.MobileNum),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(OrderDetailInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@OrderNum",model.OrderNum),
                    Db.Helper.MakeInParameter("@GroupID",model.GroupID),
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@AddMemberName",model.AddMemberName),
                    Db.Helper.MakeInParameter("@RealName",model.RealName),
                    Db.Helper.MakeInParameter("@Sex",model.Sex),
                    Db.Helper.MakeInParameter("@IDNum",model.IDNum),
                    Db.Helper.MakeInParameter("@MobileNum",model.MobileNum),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, OrderDetailInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.OrderNum = dr.GetString(1);
                    model.GroupID = dr.GetInt32(2);
                    model.LineID = dr.GetInt32(3);
                    model.AddMemberName = dr.GetString(4);
                    model.RealName = dr.GetString(5);
                    model.Sex = dr.GetString(6);
                    model.IDNum = dr.GetString(7);
                    model.MobileNum = dr.GetString(8);
                    model.AddTime = dr.GetDateTime(9);
                    model.Remarks = dr.GetString(10);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(11));
        }

        #endregion

        #region 基本操作
        public int Add(OrderDetailInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(OrderDetailInfo model)
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

        public OrderDetailInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<OrderDetailInfo>(fillModel, strSql, paras);
        }

        public List<OrderDetailInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<OrderDetailInfo>(fillModel, strSql, paras);
        }

        public List<OrderDetailInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<OrderDetailInfo>(fillModel, strSql, paras);
        }

        public List<OrderDetailInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<OrderDetailInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(OrderDetailInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(OrderDetailInfo model, DbTransaction tran)
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

        public OrderDetailInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<OrderDetailInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<OrderDetailInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<OrderDetailInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
