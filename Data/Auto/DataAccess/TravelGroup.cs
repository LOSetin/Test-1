using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class TravelGroup
    {
        private const string TABLE_NAME = "TravelGroup";
        private const string ALL_FIELDS = "ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields";
        private const string SQL_ADD = "Insert Into TravelGroup (LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields)Values(@LineID,@Name,@TotalNum,@RemainNum,@JoinNum,@PromotionNum,@GroupNum,@GoDate,@BackDate,@GatheringTime,@GatheringPlace,@TransferPlace,@GoTravel,@BackTravel,@OuterPrice,@InnerPrice,@Deposit,@GruopLeader,@TravelGuide,@IsDel,@IsPublish,@Remarks,@AddUserName,@AddUserRealName,@AddTime,@ExtraFields)";
        private const string SQL_UPDATE = "Update TravelGroup Set LineID=@LineID,Name=@Name,TotalNum=@TotalNum,RemainNum=@RemainNum,JoinNum=@JoinNum,PromotionNum=@PromotionNum,GroupNum=@GroupNum,GoDate=@GoDate,BackDate=@BackDate,GatheringTime=@GatheringTime,GatheringPlace=@GatheringPlace,TransferPlace=@TransferPlace,GoTravel=@GoTravel,BackTravel=@BackTravel,OuterPrice=@OuterPrice,InnerPrice=@InnerPrice,Deposit=@Deposit,GruopLeader=@GruopLeader,TravelGuide=@TravelGuide,IsDel=@IsDel,IsPublish=@IsPublish,Remarks=@Remarks,AddUserName=@AddUserName,AddUserRealName=@AddUserRealName,AddTime=@AddTime,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields From TravelGroup Where ";
        private const string SQL_DELETE = "Delete From TravelGroup Where ";
        private const string SQL_TOP="Select Top {0} ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields From TravelGroup Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From TravelGroup Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(TravelGroupInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@TotalNum",model.TotalNum),
                    Db.Helper.MakeInParameter("@RemainNum",model.RemainNum),
                    Db.Helper.MakeInParameter("@JoinNum",model.JoinNum),
                    Db.Helper.MakeInParameter("@PromotionNum",model.PromotionNum),
                    Db.Helper.MakeInParameter("@GroupNum",model.GroupNum),
                    Db.Helper.MakeInParameter("@GoDate",model.GoDate),
                    Db.Helper.MakeInParameter("@BackDate",model.BackDate),
                    Db.Helper.MakeInParameter("@GatheringTime",model.GatheringTime),
                    Db.Helper.MakeInParameter("@GatheringPlace",model.GatheringPlace),
                    Db.Helper.MakeInParameter("@TransferPlace",model.TransferPlace),
                    Db.Helper.MakeInParameter("@GoTravel",model.GoTravel),
                    Db.Helper.MakeInParameter("@BackTravel",model.BackTravel),
                    Db.Helper.MakeInParameter("@OuterPrice",model.OuterPrice),
                    Db.Helper.MakeInParameter("@InnerPrice",model.InnerPrice),
                    Db.Helper.MakeInParameter("@Deposit",model.Deposit),
                    Db.Helper.MakeInParameter("@GruopLeader",model.GruopLeader),
                    Db.Helper.MakeInParameter("@TravelGuide",model.TravelGuide),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@IsPublish",model.IsPublish),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(TravelGroupInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@LineID",model.LineID),
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@TotalNum",model.TotalNum),
                    Db.Helper.MakeInParameter("@RemainNum",model.RemainNum),
                    Db.Helper.MakeInParameter("@JoinNum",model.JoinNum),
                    Db.Helper.MakeInParameter("@PromotionNum",model.PromotionNum),
                    Db.Helper.MakeInParameter("@GroupNum",model.GroupNum),
                    Db.Helper.MakeInParameter("@GoDate",model.GoDate),
                    Db.Helper.MakeInParameter("@BackDate",model.BackDate),
                    Db.Helper.MakeInParameter("@GatheringTime",model.GatheringTime),
                    Db.Helper.MakeInParameter("@GatheringPlace",model.GatheringPlace),
                    Db.Helper.MakeInParameter("@TransferPlace",model.TransferPlace),
                    Db.Helper.MakeInParameter("@GoTravel",model.GoTravel),
                    Db.Helper.MakeInParameter("@BackTravel",model.BackTravel),
                    Db.Helper.MakeInParameter("@OuterPrice",model.OuterPrice),
                    Db.Helper.MakeInParameter("@InnerPrice",model.InnerPrice),
                    Db.Helper.MakeInParameter("@Deposit",model.Deposit),
                    Db.Helper.MakeInParameter("@GruopLeader",model.GruopLeader),
                    Db.Helper.MakeInParameter("@TravelGuide",model.TravelGuide),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@IsPublish",model.IsPublish),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, TravelGroupInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.LineID = dr.GetInt32(1);
                    model.Name = dr.GetString(2);
                    model.TotalNum = dr.GetInt32(3);
                    model.RemainNum = dr.GetInt32(4);
                    model.JoinNum = dr.GetInt32(5);
                    model.PromotionNum = dr.GetInt32(6);
                    model.GroupNum = dr.GetString(7);
                    model.GoDate = dr.GetDateTime(8);
                    model.BackDate = dr.GetDateTime(9);
                    model.GatheringTime = dr.GetString(10);
                    model.GatheringPlace = dr.GetString(11);
                    model.TransferPlace = dr.GetString(12);
                    model.GoTravel = dr.GetString(13);
                    model.BackTravel = dr.GetString(14);
                    model.OuterPrice = dr.GetDecimal(15);
                    model.InnerPrice = dr.GetDecimal(16);
                    model.Deposit = dr.GetDecimal(17);
                    model.GruopLeader = dr.GetString(18);
                    model.TravelGuide = dr.GetString(19);
                    model.IsDel = dr.GetInt32(20);
                    model.IsPublish = dr.GetInt32(21);
                    model.Remarks = dr.GetString(22);
                    model.AddUserName = dr.GetString(23);
                    model.AddUserRealName = dr.GetString(24);
                    model.AddTime = dr.GetDateTime(25);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(26));
        }

        #endregion

        #region 基本操作
        public int Add(TravelGroupInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(TravelGroupInfo model)
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

        public TravelGroupInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<TravelGroupInfo>(fillModel, strSql, paras);
        }

        public List<TravelGroupInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<TravelGroupInfo>(fillModel, strSql, paras);
        }

        public List<TravelGroupInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<TravelGroupInfo>(fillModel, strSql, paras);
        }

        public List<TravelGroupInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<TravelGroupInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(TravelGroupInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(TravelGroupInfo model, DbTransaction tran)
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

        public TravelGroupInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<TravelGroupInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<TravelGroupInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<TravelGroupInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
