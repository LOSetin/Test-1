using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteUser
    {
        private const string TABLE_NAME = "SiteUser";
        private const string ALL_FIELDS = "Name,Pwd,RealName,RoleIDs,LastLoginTime,LastLoginIP,ThisLoginTime,ThisLoginIP,InviteNum,Remarks,UserStatus,ExtraFields";
        private const string SQL_ADD = "Insert Into SiteUser (Name,Pwd,RealName,RoleIDs,LastLoginTime,LastLoginIP,ThisLoginTime,ThisLoginIP,InviteNum,Remarks,UserStatus,ExtraFields)Values(@Name,@Pwd,@RealName,@RoleIDs,@LastLoginTime,@LastLoginIP,@ThisLoginTime,@ThisLoginIP,@InviteNum,@Remarks,@UserStatus,@ExtraFields)";
        private const string SQL_UPDATE = "Update SiteUser Set Pwd=@Pwd,RealName=@RealName,RoleIDs=@RoleIDs,LastLoginTime=@LastLoginTime,LastLoginIP=@LastLoginIP,ThisLoginTime=@ThisLoginTime,ThisLoginIP=@ThisLoginIP,InviteNum=@InviteNum,Remarks=@Remarks,UserStatus=@UserStatus,ExtraFields=@ExtraFields Where ";
        private const string SQL_SELECT = "Select Name,Pwd,RealName,RoleIDs,LastLoginTime,LastLoginIP,ThisLoginTime,ThisLoginIP,InviteNum,Remarks,UserStatus,ExtraFields From SiteUser Where ";
        private const string SQL_DELETE = "Delete From SiteUser Where ";
        private const string SQL_TOP="Select Top {0} Name,Pwd,RealName,RoleIDs,LastLoginTime,LastLoginIP,ThisLoginTime,ThisLoginIP,InviteNum,Remarks,UserStatus,ExtraFields From SiteUser Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From SiteUser Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "Name = @Name";
        private const string PK_PARA = "@Name";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(SiteUserInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@Pwd",model.Pwd),
                    Db.Helper.MakeInParameter("@RealName",model.RealName),
                    Db.Helper.MakeInParameter("@RoleIDs",model.RoleIDs),
                    Db.Helper.MakeInParameter("@LastLoginTime",model.LastLoginTime),
                    Db.Helper.MakeInParameter("@LastLoginIP",model.LastLoginIP),
                    Db.Helper.MakeInParameter("@ThisLoginTime",model.ThisLoginTime),
                    Db.Helper.MakeInParameter("@ThisLoginIP",model.ThisLoginIP),
                    Db.Helper.MakeInParameter("@InviteNum",model.InviteNum),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@UserStatus",model.UserStatus),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields))
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(SiteUserInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Pwd",model.Pwd),
                    Db.Helper.MakeInParameter("@RealName",model.RealName),
                    Db.Helper.MakeInParameter("@RoleIDs",model.RoleIDs),
                    Db.Helper.MakeInParameter("@LastLoginTime",model.LastLoginTime),
                    Db.Helper.MakeInParameter("@LastLoginIP",model.LastLoginIP),
                    Db.Helper.MakeInParameter("@ThisLoginTime",model.ThisLoginTime),
                    Db.Helper.MakeInParameter("@ThisLoginIP",model.ThisLoginIP),
                    Db.Helper.MakeInParameter("@InviteNum",model.InviteNum),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@UserStatus",model.UserStatus),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@Name",model.Name)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, SiteUserInfo model)
        {
                    model.Name = dr.GetString(0);
                    model.Pwd = dr.GetString(1);
                    model.RealName = dr.GetString(2);
                    model.RoleIDs = dr.GetString(3);
                    model.LastLoginTime = dr.GetDateTime(4);
                    model.LastLoginIP = dr.GetString(5);
                    model.ThisLoginTime = dr.GetDateTime(6);
                    model.ThisLoginIP = dr.GetString(7);
                    model.InviteNum = dr.GetString(8);
                    model.Remarks = dr.GetString(9);
                    model.UserStatus = dr.GetString(10);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(11));
        }

        #endregion

        #region 基本操作
        public int Add(SiteUserInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(SiteUserInfo model)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] paras = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(strSql, paras);
        }

        public int Delete(string name)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, name) };
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

        public SiteUserInfo Select(string name)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, name) };
            return Db.Helper.GetModel<SiteUserInfo>(fillModel, strSql, paras);
        }

        public List<SiteUserInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<SiteUserInfo>(fillModel, strSql, paras);
        }

        public List<SiteUserInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<SiteUserInfo>(fillModel, strSql, paras);
        }

        public List<SiteUserInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<SiteUserInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(SiteUserInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(SiteUserInfo model, DbTransaction tran)
        {
            string strSql = SQL_UPDATE + PK_PARA_SET;
            DbParameter[] para = makeParameterForUpdate(model);
            return Db.Helper.ExecuteNonQuery(tran, strSql, para);
        }

        public int Delete(string name, DbTransaction tran)
        {
            string strSql = SQL_DELETE + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, name) };
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

        public SiteUserInfo Select(string name, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, name) };
            return Db.Helper.GetModel<SiteUserInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<SiteUserInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<SiteUserInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
