using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Arrow.Framework;

namespace TMS
{
    public partial class LineCat
    {
        private const string TABLE_NAME = "LineCat";
        private const string ALL_FIELDS = "ID,Name,ParentID,IsDel,SortOrder,Remarks,ExtraFields,IsShowIndex,IsHotSell,IsHotSort,IsSearchWord,HitTimes";
        private const string SQL_ADD = "Insert Into LineCat (Name,ParentID,IsDel,SortOrder,Remarks,ExtraFields,IsShowIndex,IsHotSell,IsHotSort,IsSearchWord,HitTimes)Values(@Name,@ParentID,@IsDel,@SortOrder,@Remarks,@ExtraFields,@IsShowIndex,@IsHotSell,@IsHotSort,@IsSearchWord,@HitTimes)";
        private const string SQL_UPDATE = "Update LineCat Set Name=@Name,ParentID=@ParentID,IsDel=@IsDel,SortOrder=@SortOrder,Remarks=@Remarks,ExtraFields=@ExtraFields,IsShowIndex=@IsShowIndex,IsHotSell=@IsHotSell,IsHotSort=@IsHotSort,IsSearchWord=@IsSearchWord,HitTimes=@HitTimes Where ";
        private const string SQL_SELECT = "Select ID,Name,ParentID,IsDel,SortOrder,Remarks,ExtraFields,IsShowIndex,IsHotSell,IsHotSort,IsSearchWord,HitTimes From LineCat Where ";
        private const string SQL_DELETE = "Delete From LineCat Where ";
        private const string SQL_TOP="Select Top {0} ID,Name,ParentID,IsDel,SortOrder,Remarks,ExtraFields,IsShowIndex,IsHotSell,IsHotSort,IsSearchWord,HitTimes From LineCat Where {1} Order By {2}";
        private const string SQL_COUNT="Select Count(*) From LineCat Where ";
        private const string SQL_PAGE="Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
        private const string PK_PARA_SET= "ID = @ID";
        private const string PK_PARA = "@ID";

        #region 私有方法
        private DbParameter[] makeParameterForAdd(LineCatInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@ParentID",model.ParentID),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@SortOrder",model.SortOrder),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@IsShowIndex",model.IsShowIndex),
                    Db.Helper.MakeInParameter("@IsHotSell",model.IsHotSell),
                    Db.Helper.MakeInParameter("@IsHotSort",model.IsHotSort),
                    Db.Helper.MakeInParameter("@IsSearchWord",model.IsSearchWord),
                    Db.Helper.MakeInParameter("@HitTimes",model.HitTimes)
            };
            return paras;
        }

        private DbParameter[] makeParameterForUpdate(LineCatInfo model)
        {
            DbParameter[] paras = {
                    Db.Helper.MakeInParameter("@Name",model.Name),
                    Db.Helper.MakeInParameter("@ParentID",model.ParentID),
                    Db.Helper.MakeInParameter("@IsDel",model.IsDel),
                    Db.Helper.MakeInParameter("@SortOrder",model.SortOrder),
                    Db.Helper.MakeInParameter("@Remarks",model.Remarks),
                    Db.Helper.MakeInParameter("@ExtraFields",FieldsHelper.XmlSerialize(model.ExtraFields)),
                    Db.Helper.MakeInParameter("@IsShowIndex",model.IsShowIndex),
                    Db.Helper.MakeInParameter("@IsHotSell",model.IsHotSell),
                    Db.Helper.MakeInParameter("@IsHotSort",model.IsHotSort),
                    Db.Helper.MakeInParameter("@IsSearchWord",model.IsSearchWord),
                    Db.Helper.MakeInParameter("@HitTimes",model.HitTimes),
                    Db.Helper.MakeInParameter("@ID",model.ID)
            };
            return paras;
        }

        private void fillModel(DbDataReader dr, LineCatInfo model)
        {
                    model.ID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.ParentID = dr.GetInt32(2);
                    model.IsDel = dr.GetInt32(3);
                    model.SortOrder = dr.GetInt32(4);
                    if (!dr.IsDBNull(5))
                        model.Remarks = dr.GetString(5);
                    model.ExtraFields = FieldsHelper.XmlDeserialize(dr.GetString(6));
                    model.IsShowIndex = dr.GetInt32(7);
                    model.IsHotSell = dr.GetInt32(8);
                    model.IsHotSort = dr.GetInt32(9);
                    model.IsSearchWord = dr.GetInt32(10);
                    model.HitTimes = dr.GetInt32(11);
        }

        #endregion

        #region 基本操作
        public int Add(LineCatInfo model)
        {
            DbParameter[] paras = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); 
        }

        public int Update(LineCatInfo model)
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

        public LineCatInfo Select(int iD)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<LineCatInfo>(fillModel, strSql, paras);
        }

        public List<LineCatInfo> SelectList(string strWhere, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<LineCatInfo>(fillModel, strSql, paras);
        }

        public List<LineCatInfo> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)
        {
            string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);
            return Db.Helper.GetList<LineCatInfo>(fillModel, strSql, paras);
        }

        public List<LineCatInfo> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            int min = pageSize * (pageIndex - 1);
            int max = pageSize * pageIndex;
            string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);
            return Db.Helper.GetList<LineCatInfo>(fillModel,strSql, paras);
        }

        public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)
        {
            string strSql = SQL_SELECT + strWhere;
             return Db.Helper.ExecuteDataSet(strSql, paras);
        }
        #endregion

        #region  事务操作
        public int Add(LineCatInfo model, DbTransaction tran)
        {
            DbParameter[] para = makeParameterForAdd(model);
            return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);
        }

        public int Update(LineCatInfo model, DbTransaction tran)
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

        public LineCatInfo Select(int iD, DbTransaction tran)
        {
            string strSql = SQL_SELECT + PK_PARA_SET;
            DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, iD) };
            return Db.Helper.GetModel<LineCatInfo>(fillModel, strSql, paras, true, tran);
        }

        public List<LineCatInfo> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)
        {
            string strSql = SQL_SELECT + strWhere;
            return Db.Helper.GetList<LineCatInfo>(fillModel, strSql, paras, true, tran);
        }

        #endregion
    }
}
