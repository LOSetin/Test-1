using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteMember
    {
        private const string TABLE_NAME = "SiteMember";
        private const string ALL_FIELDS = "UserName,UserPwd,RealName,HeadPicPath,Sex,MobileNum,IDNum,Email,QQ,WeChat,TotalCost,TotalPoints,UsedPoints,AddTime,Remarks,InviteNum,InviterUserName,InviterRealName,ExtraFields";
        private const string SQL_ADD = "Insert Into SiteMember (UserName,UserPwd,RealName,HeadPicPath,Sex,MobileNum,IDNum,Email,QQ,WeChat,TotalCost,TotalPoints,UsedPoints,AddTime,Remarks,InviteNum,InviterUserName,InviterRealName,ExtraFields)Values(@UserName,@UserPwd,@RealName,@HeadPicPath,@Sex,@MobileNum,@IDNum,@Email,@QQ,@WeChat,@TotalCost,@TotalPoints,@UsedPoints,@AddTime,@Remarks,@InviteNum,@InviterUserName,@InviterRealName,@ExtraFields)";
        private const string SQL_UPDATE = "Update SiteMember Set UserPwd=@UserPwd,RealName=@RealName,HeadPicPath=@HeadPicPath,Sex=@Sex,MobileNum=@MobileNum,IDNum=@IDNum,Email=@Email,QQ=@QQ,WeChat=@WeChat,TotalCost=@TotalCost,TotalPoints=@TotalPoints,UsedPoints=@UsedPoints,AddTime=@AddTime,Remarks=@Remarks,InviteNum=@InviteNum,InviterUserName=@InviterUserName,InviterRealName=@InviterRealName,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select UserName,UserPwd,RealName,HeadPicPath,Sex,MobileNum,IDNum,Email,QQ,WeChat,TotalCost,TotalPoints,UsedPoints,AddTime,Remarks,InviteNum,InviterUserName,InviterRealName,ExtraFields From SiteMember Where ";
        private const string SQL_DELETE = "Delete From SiteMember Where ";
        private const string SQL_TOP="Select Top {0} UserName,UserPwd,RealName,HeadPicPath,Sex,MobileNum,IDNum,Email,QQ,WeChat,TotalCost,TotalPoints,UsedPoints,AddTime,Remarks,InviteNum,InviterUserName,InviterRealName,ExtraFields From SiteMember Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From SiteMember Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "UserName = @UserName";
        private const string PK_PARA = "@UserName";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(SiteMemberInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserName",model.UserName),
                    Db.Helper.MakeInParameter("@UserPwd",model.UserPwd),
                    Db.Helper.MakeInParameter("@RealName",model.RealName),
                    Db.Helper.MakeInParameter("@HeadPicPath",model.HeadPicPath),
                    Db.Helper.MakeInParameter("@Sex",model.Sex),
                    Db.Helper.MakeInParameter("@MobileNum",model.MobileNum),
                    Db.Helper.MakeInParameter("@IDNum",model.IDNum),
                    Db.Helper.MakeInParameter("@Email",model.Email),
                    Db.Helper.MakeInParameter("@QQ",model.QQ),
                    Db.Helper.MakeInParameter("@WeChat",model.WeChat),
                    Db.Helper.MakeInParameter("@TotalCost",model.TotalCost),
                    Db.Helper.MakeInParameter("@TotalPoints",model.TotalPoints),
                    Db.Helper.MakeInParameter("@UsedPoints",model.UsedPoints),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@InviteNum",model.InviteNum),
                    Db.Helper.MakeInParameter("@InviterUserName",model.InviterUserName),
                    Db.Helper.MakeInParameter("@InviterRealName",model.InviterRealName),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(SiteMemberInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@UserPwd",model.UserPwd),
                    Db.Helper.MakeInParameter("@RealName",model.RealName),
                    Db.Helper.MakeInParameter("@HeadPicPath",model.HeadPicPath),
                    Db.Helper.MakeInParameter("@Sex",model.Sex),
                    Db.Helper.MakeInParameter("@MobileNum",model.MobileNum),
                    Db.Helper.MakeInParameter("@IDNum",model.IDNum),
                    Db.Helper.MakeInParameter("@Email",model.Email),
                    Db.Helper.MakeInParameter("@QQ",model.QQ),
                    Db.Helper.MakeInParameter("@WeChat",model.WeChat),
                    Db.Helper.MakeInParameter("@TotalCost",model.TotalCost),
                    Db.Helper.MakeInParameter("@TotalPoints",model.TotalPoints),
                    Db.Helper.MakeInParameter("@UsedPoints",model.UsedPoints),
                    Db.Helper.MakeInParameter("@AddTime",model.AddTime),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@InviteNum",model.InviteNum),
                    Db.Helper.MakeInParameter("@InviterUserName",model.InviterUserName),
                    Db.Helper.MakeInParameter("@InviterRealName",model.InviterRealName),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@UserName",model.UserName)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, SiteMemberInfo model)
        {
                    model.UserName = dr.GetString(0);
                    model.UserPwd = dr.GetString(1);
                    model.RealName = dr.GetString(2);
                    model.HeadPicPath = dr.GetString(3);
                    model.Sex = dr.GetString(4);
                    model.MobileNum = dr.GetString(5);
                    model.IDNum = dr.GetString(6);
                    model.Email = dr.GetString(7);
                    model.QQ = dr.GetString(8);
                    model.WeChat = dr.GetString(9);
                    model.TotalCost = dr.GetDecimal(10);
                    model.TotalPoints = dr.GetInt32(11);
                    model.UsedPoints = dr.GetInt32(12);
                    model.AddTime = dr.GetDateTime(13);
                    model.Remarks = dr.GetString(14);
                    model.InviteNum = dr.GetString(15);
                    model.InviterUserName = dr.GetString(16);
                    model.InviterRealName = dr.GetString(17);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(18));
        }

        #endregion

        #region 基本操作
        public int Add(SiteMemberInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(SiteMemberInfo model)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] paras = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(strSql, paras);
        }

        public int Delete(string userName)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, userName) };
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

        public SiteMemberInfo Select(string userName)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, userName) };
            return Db.Helper.GetModel<SiteMemberInfo>(fillModel, strSql, paras);
        }

        public List<SiteMemberInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<SiteMemberInfo>(fillModel, strSql, paras);
        }

        public List<SiteMemberInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<SiteMemberInfo>(fillModel, strSql, paras);
        }

        public List<SiteMemberInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<SiteMemberInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(SiteMemberInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(SiteMemberInfo model, DbTransaction tran)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] para = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(tran, strSql, para);
        }

        public int Delete(string userName, DbTransaction tran)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, userName) };
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

        public SiteMemberInfo Select(string userName, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, userName) };
            return Db.Helper.GetModel<SiteMemberInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<SiteMemberInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<SiteMemberInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
