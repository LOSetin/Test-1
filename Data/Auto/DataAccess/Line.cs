using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class Line
    {
        private const string TABLE_NAME = "Line";
        private const string ALL_FIELDS = "ID,Name,FirstCatID,SecondCatID,ProductNum,CoverPath,BigPicPath,TravelDays,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,OtherNotice,WarmTips,MinPrice,Tag,Remarks,AddUserName,AddUserRealName,AddTime,IsDel,ExtraFields,IsTop,IsHot,HitTimes,IsPickup";
        private const string SQL_ADD = "Insert Into Line (Name,FirstCatID,SecondCatID,ProductNum,CoverPath,BigPicPath,TravelDays,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,OtherNotice,WarmTips,MinPrice,Tag,Remarks,AddUserName,AddUserRealName,AddTime,IsDel,ExtraFields,IsTop,IsHot,HitTimes,IsPickup)Values(@Name,@FirstCatID,@SecondCatID,@ProductNum,@CoverPath,@BigPicPath,@TravelDays,@StartAddress,@TargetAddress,@GoTravel,@BackTravel,@SignUpNotice,@LineDesc,@OtherNotice,@WarmTips,@MinPrice,@Tag,@Remarks,@AddUserName,@AddUserRealName,@AddTime,@IsDel,@ExtraFields,@IsTop,@IsHot,@HitTimes,@IsPickup)";
        private const string SQL_UPDATE = "Update Line Set Name=@Name,FirstCatID=@FirstCatID,SecondCatID=@SecondCatID,ProductNum=@ProductNum,CoverPath=@CoverPath,BigPicPath=@BigPicPath,TravelDays=@TravelDays,StartAddress=@StartAddress,TargetAddress=@TargetAddress,GoTravel=@GoTravel,BackTravel=@BackTravel,SignUpNotice=@SignUpNotice,LineDesc=@LineDesc,OtherNotice=@OtherNotice,WarmTips=@WarmTips,MinPrice=@MinPrice,Tag=@Tag,Remarks=@Remarks,AddUserName=@AddUserName,AddUserRealName=@AddUserRealName,AddTime=@AddTime,IsDel=@IsDel,ExtraFields=@ExtraFields,IsTop=@IsTop,IsHot=@IsHot,HitTimes=@HitTimes,IsPickup=@IsPickup Where ";
        private const string SQL_SELECT = "Select ID,Name,FirstCatID,SecondCatID,ProductNum,CoverPath,BigPicPath,TravelDays,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,OtherNotice,WarmTips,MinPrice,Tag,Remarks,AddUserName,AddUserRealName,AddTime,IsDel,ExtraFields,IsTop,IsHot,HitTimes,IsPickup From Line Where ";
        private const string SQL_DELETE = "Delete From Line Where ";
        private const string SQL_TOP="Select Top {0} ID,Name,FirstCatID,SecondCatID,ProductNum,CoverPath,BigPicPath,TravelDays,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,OtherNotice,WarmTips,MinPrice,Tag,Remarks,AddUserName,AddUserRealName,AddTime,IsDel,ExtraFields,IsTop,IsHot,HitTimes,IsPickup From Line Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From Line Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(LineInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@FirstCatID",model.FirstCatID),
                    Db.Helper.MakeInParameter("@SecondCatID",model.SecondCatID),
                    Db.Helper.MakeInParameter("@ProductNum",model.ProductNum),
                    Db.Helper.MakeInParameter("@CoverPath",model.CoverPath),
                    Db.Helper.MakeInParameter("@BigPicPath",model.BigPicPath),
                    Db.Helper.MakeInParameter("@TravelDays",model.TravelDays),
                    Db.Helper.MakeInParameter("@StartAddress",model.StartAddress),
                    Db.Helper.MakeInParameter("@TargetAddress",model.TargetAddress),
                    Db.Helper.MakeInParameter("@GoTravel",model.GoTravel),
                    Db.Helper.MakeInParameter("@BackTravel",model.BackTravel),
                    Db.Helper.MakeInParameter("@SignUpNotice",model.SignUpNotice),
                    Db.Helper.MakeInParameter("@LineDesc",model.LineDesc),
                    Db.Helper.MakeInParameter("@OtherNotice",model.OtherNotice),
                    Db.Helper.MakeInParameter("@WarmTips",model.WarmTips),
                    Db.Helper.MakeInParameter("@MinPrice",model.MinPrice),
                    Db.Helper.MakeInParameter("@Tag",model.Tag),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@IsTop",model.IsTop),
                    Db.Helper.MakeInParameter("@IsHot",model.IsHot),
                    Db.Helper.MakeInParameter("@HitTimes",model.HitTimes),
                    Db.Helper.MakeInParameter("@IsPickup",model.IsPickup)
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(LineInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@FirstCatID",model.FirstCatID),
                    Db.Helper.MakeInParameter("@SecondCatID",model.SecondCatID),
                    Db.Helper.MakeInParameter("@ProductNum",model.ProductNum),
                    Db.Helper.MakeInParameter("@CoverPath",model.CoverPath),
                    Db.Helper.MakeInParameter("@BigPicPath",model.BigPicPath),
                    Db.Helper.MakeInParameter("@TravelDays",model.TravelDays),
                    Db.Helper.MakeInParameter("@StartAddress",model.StartAddress),
                    Db.Helper.MakeInParameter("@TargetAddress",model.TargetAddress),
                    Db.Helper.MakeInParameter("@GoTravel",model.GoTravel),
                    Db.Helper.MakeInParameter("@BackTravel",model.BackTravel),
                    Db.Helper.MakeInParameter("@SignUpNotice",model.SignUpNotice),
                    Db.Helper.MakeInParameter("@LineDesc",model.LineDesc),
                    Db.Helper.MakeInParameter("@OtherNotice",model.OtherNotice),
                    Db.Helper.MakeInParameter("@WarmTips",model.WarmTips),
                    Db.Helper.MakeInParameter("@MinPrice",model.MinPrice),
                    Db.Helper.MakeInParameter("@Tag",model.Tag),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@AddUserName",model.AddUserName),
                    Db.Helper.MakeInParameter("@AddUserRealName",model.AddUserRealName),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@IsTop",model.IsTop),
                    Db.Helper.MakeInParameter("@IsHot",model.IsHot),
                    Db.Helper.MakeInParameter("@HitTimes",model.HitTimes),
                    Db.Helper.MakeInParameter("@IsPickup",model.IsPickup),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, LineInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.FirstCatID = dr.GetInt32(2);
                    model.SecondCatID = dr.GetInt32(3);
                    model.ProductNum = dr.GetString(4);
                    model.CoverPath = dr.GetString(5);
                    model.BigPicPath = dr.GetString(6);
                    model.TravelDays = dr.GetString(7);
                    model.StartAddress = dr.GetString(8);
                    model.TargetAddress = dr.GetString(9);
                    model.GoTravel = dr.GetString(10);
                    model.BackTravel = dr.GetString(11);
                    model.SignUpNotice = dr.GetString(12);
                    model.LineDesc = dr.GetString(13);
                    model.OtherNotice = dr.GetString(14);
                    model.WarmTips = dr.GetString(15);
                    model.MinPrice = dr.GetDecimal(16);
                    model.Tag = dr.GetString(17);
                    if (!dr.IsDBNull(18))
                        model.Remarks = dr.GetString(18);
                    model.AddUserName = dr.GetString(19);
                    model.AddUserRealName = dr.GetString(20);
                    model.AddTime = dr.GetDateTime(21);
                    model.IsDel = dr.GetInt32(22);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(23));
                    model.IsTop = dr.GetInt32(24);
                    model.IsHot = dr.GetInt32(25);
                    model.HitTimes = dr.GetInt32(26);
                    model.IsPickup = dr.GetInt32(27);
        }

        #endregion

        #region 基本操作
        public int Add(LineInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(LineInfo model)
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

        public LineInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<LineInfo>(fillModel, strSql, paras);
        }

        public List<LineInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<LineInfo>(fillModel, strSql, paras);
        }

        public List<LineInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<LineInfo>(fillModel, strSql, paras);
        }

        public List<LineInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<LineInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(LineInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(LineInfo model, DbTransaction tran)
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

        public LineInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<LineInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<LineInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<LineInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
