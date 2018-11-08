using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class Promotion
    {
        private const string TABLE_NAME = "Promotion";
        private const string ALL_FIELDS = "ID,Name,CoverPath,PromotionDesc,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,Tag,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields";
        private const string SQL_ADD = "Insert Into Promotion (Name,CoverPath,PromotionDesc,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,Tag,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields)Values(@Name,@CoverPath,@PromotionDesc,@PromotionType,@Discount,@TotalPayOneTime,@TotalPayOneTimeJoinNum,@FullCutTotal,@FullCutMinus,@SecondKillPrice,@StartTime,@EndTime,@IsDel,@Tag,@Remarks,@AddUserName,@AddUserRealName,@AddTime,@ExtraFields)";
        private const string SQL_UPDATE = "Update Promotion Set Name=@Name,CoverPath=@CoverPath,PromotionDesc=@PromotionDesc,PromotionType=@PromotionType,Discount=@Discount,TotalPayOneTime=@TotalPayOneTime,TotalPayOneTimeJoinNum=@TotalPayOneTimeJoinNum,FullCutTotal=@FullCutTotal,FullCutMinus=@FullCutMinus,SecondKillPrice=@SecondKillPrice,StartTime=@StartTime,EndTime=@EndTime,IsDel=@IsDel,Tag=@Tag,Remarks=@Remarks,AddUserName=@AddUserName,AddUserRealName=@AddUserRealName,AddTime=@AddTime,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select ID,Name,CoverPath,PromotionDesc,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,Tag,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields From Promotion Where ";
        private const string SQL_DELETE = "Delete From Promotion Where ";
        private const string SQL_TOP="Select Top {0} ID,Name,CoverPath,PromotionDesc,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,Tag,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields From Promotion Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From Promotion Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(PromotionInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@CoverPath",model.CoverPath),
                    Db.Helper.MakeInParameter("@PromotionDesc",model.PromotionDesc),
                    Db.Helper.MakeInParameter("@PromotionType",model.PromotionType),
                    Db.Helper.MakeInParameter("@Discount",model.Discount),
                    Db.Helper.MakeInParameter("@TotalPayOneTime",model.TotalPayOneTime),
                    Db.Helper.MakeInParameter("@TotalPayOneTimeJoinNum",model.TotalPayOneTimeJoinNum),
                    Db.Helper.MakeInParameter("@FullCutTotal",model.FullCutTotal),
                    Db.Helper.MakeInParameter("@FullCutMinus",model.FullCutMinus),
                    Db.Helper.MakeInParameter("@SecondKillPrice",model.SecondKillPrice),
                    Db.Helper.MakeInParameter("@StartTime",model.StartTime),
                    Db.Helper.MakeInParameter("@EndTime",model.EndTime),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@Tag",model.Tag),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(PromotionInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@CoverPath",model.CoverPath),
                    Db.Helper.MakeInParameter("@PromotionDesc",model.PromotionDesc),
                    Db.Helper.MakeInParameter("@PromotionType",model.PromotionType),
                    Db.Helper.MakeInParameter("@Discount",model.Discount),
                    Db.Helper.MakeInParameter("@TotalPayOneTime",model.TotalPayOneTime),
                    Db.Helper.MakeInParameter("@TotalPayOneTimeJoinNum",model.TotalPayOneTimeJoinNum),
                    Db.Helper.MakeInParameter("@FullCutTotal",model.FullCutTotal),
                    Db.Helper.MakeInParameter("@FullCutMinus",model.FullCutMinus),
                    Db.Helper.MakeInParameter("@SecondKillPrice",model.SecondKillPrice),
                    Db.Helper.MakeInParameter("@StartTime",model.StartTime),
                    Db.Helper.MakeInParameter("@EndTime",model.EndTime),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@Tag",model.Tag),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, PromotionInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.CoverPath = dr.GetString(2);
                    model.PromotionDesc = dr.GetString(3);
                    model.PromotionType = dr.GetString(4);
                    model.Discount = dr.GetDecimal(5);
                    model.TotalPayOneTime = dr.GetDecimal(6);
                    model.TotalPayOneTimeJoinNum = dr.GetInt32(7);
                    model.FullCutTotal = dr.GetDecimal(8);
                    model.FullCutMinus = dr.GetDecimal(9);
                    model.SecondKillPrice = dr.GetDecimal(10);
                    model.StartTime = dr.GetDateTime(11);
                    model.EndTime = dr.GetDateTime(12);
                    model.IsDel = dr.GetInt32(13);
                    model.Tag = dr.GetString(14);
                    model.Remarks = dr.GetString(15);
                    model.AddUserName = dr.GetString(16);
                    model.AddUserRealName = dr.GetString(17);
                    model.AddTime = dr.GetDateTime(18);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(19));
        }

        #endregion

        #region 基本操作
        public int Add(PromotionInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(PromotionInfo model)
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

        public PromotionInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<PromotionInfo>(fillModel, strSql, paras);
        }

        public List<PromotionInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<PromotionInfo>(fillModel, strSql, paras);
        }

        public List<PromotionInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<PromotionInfo>(fillModel, strSql, paras);
        }

        public List<PromotionInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<PromotionInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(PromotionInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(PromotionInfo model, DbTransaction tran)
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

        public PromotionInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<PromotionInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<PromotionInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<PromotionInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
