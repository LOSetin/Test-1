using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class V_Group_Line
    {
        private const string VIEW_NAME = "V_Group_Line";
        private const string ALL_FIELDS = "ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields,LineName,FirstCatID,SecondCatID,TravelDays";
        private const string SQL_SELECT = "Select ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields,LineName,FirstCatID,SecondCatID,TravelDays From V_Group_Line Where ";
        private const string SQL_TOP="Select Top {0} ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields,LineName,FirstCatID,SecondCatID,TravelDays From V_Group_Line Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From V_Group_Line Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";

        #region Ë½ÓÐ·½·¨
        private void fillModel(DbDataReader dr, V_Group_LineInfo model)
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
                    model.LineName = dr.GetString(27);
                    model.FirstCatID = dr.GetInt32(28);
                    model.SecondCatID = dr.GetInt32(29);
                    model.TravelDays = dr.GetString(30);
        }

        #endregion

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public List<V_Group_LineInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_Group_LineInfo>(fillModel, strSql, paras);
        }

        public List<V_Group_LineInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<V_Group_LineInfo>(fillModel, strSql, paras);
        }

        public List<V_Group_LineInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<V_Group_LineInfo>(fillModel,strSql, paras);
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

        public List<V_Group_LineInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_Group_LineInfo>(fillModel, strSql, paras, true, tran);
        }

    }
}
