using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class V_Promotion_Group
    {
        private const string VIEW_NAME = "V_Promotion_Group";
        private const string ALL_FIELDS = "ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,AddTime,ExtraFields,PromotionName,LineName,GoDate,BackDate,GruopLeader,TravelGuide,GatheringTime,GatheringPlace,TransferPlace,CoverPath,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,WarmTips,OtherNotice,ProductNum,TravelDays,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,RawInnerPrice,RawOuterPrice,RemainNum";
        private const string SQL_SELECT = "Select ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,AddTime,ExtraFields,PromotionName,LineName,GoDate,BackDate,GruopLeader,TravelGuide,GatheringTime,GatheringPlace,TransferPlace,CoverPath,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,WarmTips,OtherNotice,ProductNum,TravelDays,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,RawInnerPrice,RawOuterPrice,RemainNum From V_Promotion_Group Where ";
        private const string SQL_TOP="Select Top {0} ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,AddTime,ExtraFields,PromotionName,LineName,GoDate,BackDate,GruopLeader,TravelGuide,GatheringTime,GatheringPlace,TransferPlace,CoverPath,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,WarmTips,OtherNotice,ProductNum,TravelDays,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,RawInnerPrice,RawOuterPrice,RemainNum From V_Promotion_Group Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From V_Promotion_Group Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";

        #region Ë½ÓÐ·½·¨
        private void fillModel(DbDataReader dr, V_Promotion_GroupInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.PromotionID = dr.GetInt32(1);
                    model.GroupID = dr.GetInt32(2);
                    model.LineID = dr.GetInt32(3);
                    model.TotalNum = dr.GetInt32(4);
                    model.SelledNum = dr.GetInt32(5);
                    model.AddTime = dr.GetDateTime(6);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(7));
                    if (!dr.IsDBNull(8))
                        model.PromotionName = dr.GetString(8);
                    if (!dr.IsDBNull(9))
                        model.LineName = dr.GetString(9);
                    if (!dr.IsDBNull(10))
                        model.GoDate = dr.GetDateTime(10);
                    if (!dr.IsDBNull(11))
                        model.BackDate = dr.GetDateTime(11);
                    if (!dr.IsDBNull(12))
                        model.GruopLeader = dr.GetString(12);
                    if (!dr.IsDBNull(13))
                        model.TravelGuide = dr.GetString(13);
                    if (!dr.IsDBNull(14))
                        model.GatheringTime = dr.GetString(14);
                    if (!dr.IsDBNull(15))
                        model.GatheringPlace = dr.GetString(15);
                    if (!dr.IsDBNull(16))
                        model.TransferPlace = dr.GetString(16);
                    if (!dr.IsDBNull(17))
                        model.CoverPath = dr.GetString(17);
                    if (!dr.IsDBNull(18))
                        model.StartAddress = dr.GetString(18);
                    if (!dr.IsDBNull(19))
                        model.TargetAddress = dr.GetString(19);
                    if (!dr.IsDBNull(20))
                        model.GoTravel = dr.GetString(20);
                    if (!dr.IsDBNull(21))
                        model.BackTravel = dr.GetString(21);
                    if (!dr.IsDBNull(22))
                        model.SignUpNotice = dr.GetString(22);
                    if (!dr.IsDBNull(23))
                        model.LineDesc = dr.GetString(23);
                    if (!dr.IsDBNull(24))
                        model.WarmTips = dr.GetString(24);
                    if (!dr.IsDBNull(25))
                        model.OtherNotice = dr.GetString(25);
                    if (!dr.IsDBNull(26))
                        model.ProductNum = dr.GetString(26);
                    if (!dr.IsDBNull(27))
                        model.TravelDays = dr.GetString(27);
                    if (!dr.IsDBNull(28))
                        model.PromotionType = dr.GetString(28);
                    if (!dr.IsDBNull(29))
                        model.Discount = dr.GetDecimal(29);
                    if (!dr.IsDBNull(30))
                        model.TotalPayOneTime = dr.GetDecimal(30);
                    if (!dr.IsDBNull(31))
                        model.TotalPayOneTimeJoinNum = dr.GetInt32(31);
                    if (!dr.IsDBNull(32))
                        model.FullCutTotal = dr.GetDecimal(32);
                    if (!dr.IsDBNull(33))
                        model.FullCutMinus = dr.GetDecimal(33);
                    if (!dr.IsDBNull(34))
                        model.SecondKillPrice = dr.GetDecimal(34);
                    if (!dr.IsDBNull(35))
                        model.StartTime = dr.GetDateTime(35);
                    if (!dr.IsDBNull(36))
                        model.EndTime = dr.GetDateTime(36);
                    if (!dr.IsDBNull(37))
                        model.IsDel = dr.GetInt32(37);
                    model.RawInnerPrice = dr.GetDecimal(38);
                    model.RawOuterPrice = dr.GetDecimal(39);
                    model.RemainNum = dr.GetInt32(40);
        }

        #endregion

        public int GetCount(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_COUNT + strWhere;
            int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));
            return sum;
        }

        public List<V_Promotion_GroupInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_Promotion_GroupInfo>(fillModel, strSql, paras);
        }

        public List<V_Promotion_GroupInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<V_Promotion_GroupInfo>(fillModel, strSql, paras);
        }

        public List<V_Promotion_GroupInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<V_Promotion_GroupInfo>(fillModel,strSql, paras);
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

        public List<V_Promotion_GroupInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<V_Promotion_GroupInfo>(fillModel, strSql, paras, true, tran);
        }

    }
}
