using Arrow.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Arrow.Framework.CodeFactory
{  
    public class OracleFactory : IFactory
    {
        class OracleDescInfo
        {
            public string TableName { set; get; }
            public string ColumnName { set; get; }
            public string Comments { set; get; }
        }

        private DataAccess.Database OracleHelper;
        private ProjectInfo pro;
        /// <summary>
        /// Oracle约定的自增字段的命名，使用序列，名称格式为 PK_SEQ_表名
        /// </summary>
        private const string AUTO_INCREASE_FIELD = "AUTO_ID";

        public OracleFactory(ProjectInfo pro)
        {
            this.pro = pro;
            OracleHelper = new DataAccess.Database(pro.ConnectionName);
        }

        #region Sql语句
        public string SqlAllFields(List<FieldInfo> fields)
        {
            return FactoryHelper.CreateFieldList(fields, true, true, "");
        }

        public string SqlInsert(string tableName, List<FieldInfo> fields)
        {
            //Oracle没有自增字段，如果检测到自增字段，则替换
            string currentID = "PK_SEQ_" + tableName + ".NEXTVAL";
            string sql = "Insert Into " + tableName + " (";
            sql = sql + FactoryHelper.CreateFieldList(fields, true, false, "");
            sql = sql + ")Values(" + FactoryHelper.CreateFieldList(fields, true, false, ParaSymbol) + ")";
            sql = sql.Replace(ParaSymbol + AUTO_INCREASE_FIELD, currentID);
            return sql;
        }

        public string SqlUpdate(string tableName, List<FieldInfo> fields)
        {
            string sql = "Update " + tableName + " Set " + FactoryHelper.CreateUpdateFieldList(fields, ParaSymbol) + " Where ";
            return sql;
        }

        public string SqlDelete(string tableName)
        {
            string sql = "Delete From " + tableName + " Where ";
            return sql;
        }

        public string SqlSelect(string tableName, List<FieldInfo> fields)
        {
            string sql = "Select " + FactoryHelper.CreateFieldList(fields, true, true, "") + " From " + tableName + " Where ";
            return sql;
        }

        public string SqlTop(string tableName, List<FieldInfo> fields)
        {
            string sql = "Select * From (Select " + SqlAllFields(fields) + " From " + tableName + " Where {1} Order By {2}) Where RowNum <= {0} Order By RowNum asc";
            return sql;
        }

        public string SqlPage(string tableName, List<FieldInfo> fields)
        {
            string sql = "SELECT * FROM(SELECT A.*, ROWNUM RN FROM (SELECT {0} FROM {1} WHERE {2} ORDER BY {3}) A WHERE ROWNUM <= {5})WHERE RN > {4}";
            return sql;
        }

        public string SqlCount(string tableName)
        {
            string val = "Select Count(*) From " + tableName + " Where ";
            return val;
        }
        #endregion

        #region 基本方法与属性
        public string ExtraFieldName
        {
            get { return "Extra_Fields"; }
        }

        public string ParaSymbol
        {
            get { return ":"; }
        }

        public string NameFormat(string name)
        {
            return FactoryHelper.NameFormat(name, pro.IsNameSplitWithUnderLine);
        }

        public List<string> GetTables()
        {
            List<string> tables = new List<string>();
            string strsql = "select table_name from user_tables";
            using (DbDataReader odr = OracleHelper.ExecuteReader(CommandType.Text, strsql, null))
            {
                while (odr.Read())
                {
                    if (!odr.IsDBNull(0))
                    {
                        string s = odr.GetString(0);
                        tables.Add(s);
                    }
                }
            }

            return tables;
        }

        public List<string> GetViews()
        {
            List<string> views = new List<string>();
            string strsql = "select view_name from user_views";
            using (DbDataReader odr = OracleHelper.ExecuteReader(CommandType.Text, strsql, null))
            {
                while (odr.Read())
                {
                    if (!odr.IsDBNull(0))
                    {
                        string s = odr.GetString(0);
                        views.Add(s);
                    }
                }
            }

            return views;
        }

        public List<FieldInfo> GetTableFields(string tbName)
        {
            List<FieldInfo> lstField = new List<FieldInfo>();
            if (pro != null && !string.IsNullOrEmpty(tbName))
            {
                //获取所有字段
                string sqlFields = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH,NULLABLE,DATA_PRECISION,DATA_SCALE  FROM USER_TAB_COLumnS where TABLE_NAME='" + tbName + "'";
                string sqlPrimary = "select column_name from all_cons_columns cc where table_name='" + tbName + "' and exists (select 'x' from all_constraints c where c.owner = cc.owner and c.constraint_name = cc.constraint_name and c.constraint_type ='P') order by position";
                string sqlDesc = "select TABLE_NAME,COLUMN_NAME,COMMENTS from user_col_comments where TABLE_NAME='" + tbName + "'";

                //获得主键
                object o = OracleHelper.ExecuteScalar(CommandType.Text, sqlPrimary, null);
                string pk = "";
                if (o != DBNull.Value && o != null)
                {
                    pk = o.ToString();
                }
                //获得unique
                List<string> uniqueFields = GetUniqueFields(tbName);
                //获得该表所有字段的注释
                List<OracleDescInfo> descs = new List<OracleDescInfo>();
                using (DbDataReader odr = OracleHelper.ExecuteReader(CommandType.Text, sqlDesc, null))
                {
                    while (odr.Read())
                    {
                        OracleDescInfo model = new OracleDescInfo();
                        model.TableName = odr.GetString(0);
                        model.ColumnName = odr.GetString(1);
                        model.Comments = "";
                        if (!odr.IsDBNull(2))
                        {
                            model.Comments = odr.GetString(2);
                        }
                        descs.Add(model);
                    }
                }


                using (DbDataReader odr = OracleHelper.ExecuteReader(CommandType.Text, sqlFields, null))
                {
                    while (odr.Read())
                    {
                        FieldInfo fi = new FieldInfo();
                        //字段名
                        fi.FieldName = odr.GetString(0);

                        //是否主键
                        if (fi.FieldName.IsEqualTo(pk))
                            fi.IsPrimaryKey = true;
                        else
                            fi.IsPrimaryKey = false;

                        //是否唯一
                        if (uniqueFields.Exists(s => s == fi.FieldName))
                            fi.IsUnique = true;
                        else
                            fi.IsUnique = false;

                        //c#变量名
                        fi.PropertyName = NameFormat(fi.FieldName);
                        //字段类型
                        fi.FieldType = odr.GetString(1);
                        //字段长度
                        fi.FieldLength = odr.GetInt32(2);
                        //是否可空
                         if (!odr.IsDBNull(3))
                            fi.IsNullable = odr.GetString(3) == "Y" ? true : false;
                        else
                            fi.IsNullable = false;
                        //是否自增
                        fi.IsAutoIncrease = false;
                        //如果是Number类型，FieldLength为Number的整数部分
                        if (fi.FieldType.IsEqualTo("NUMBER"))
                        {
                            if (!odr.IsDBNull(4))
                                fi.FieldLength = odr.GetInt32(4);
                        }
                        if (!odr.IsDBNull(5))
                            fi.FieldScale = odr.GetInt32(5);

                        //显示名和描述
                        OracleDescInfo myDesc = descs.Find(s => s.ColumnName.IsEqualTo(fi.FieldName));
                        fi.DisplayName = "";
                        fi.Desc = "";
                        if (myDesc != null) fi.Desc = myDesc.Comments;

                        string desc = fi.Desc.Replace("，", ",");
                        string[] arr = desc.Split(',');
                        fi.DisplayName = arr[0];

                        //C#类型
                        fi.VarType = ToCSType(fi.FieldType, fi.FieldLength, fi.FieldScale);
                        //ReaderGetType
                        fi.ReaderGetType = ToReaderGetType(fi.FieldType, fi.FieldLength, fi.FieldScale);
                        lstField.Add(fi);
                    }
                }
            }

            return lstField;
        }

        public List<FieldInfo> GetViewFields(string viewName)
        {
            return GetTableFields(viewName);
        }

        public List<string> GetUniqueFields(string tableName)
        {
            List<string> lst = new List<string>();
            string sql = @"select column_name from all_cons_columns cc where table_name='{0}' and exists (select 'x' from all_constraints c where c.owner = cc.owner and c.constraint_name = cc.constraint_name and c.constraint_type ='U') order by position";
            sql = string.Format(sql, tableName);
            using (DbDataReader odr = OracleHelper.ExecuteReader( CommandType.Text, sql, null))
            {
                while (odr.Read())
                {
                    if (!odr.IsDBNull(0))
                        lst.Add(odr.GetString(0));
                }
            }
            return lst;
        }

        public List<ForeignKeyInfo> GetForeignKeys(string tableName)
        {
            List<ForeignKeyInfo> fks = new List<ForeignKeyInfo>();
            string sql = @"select
                                        a.r_constraint_name Constraint_Name,
                                        a.table_name Sub_Table_Name,
                                        c.column_name Sub_Table_Column_Name,
                                        b.table_name Main_Table_Name,
                                        d.column_name Main_Table_Column_Name
                                        from
                                        user_constraints a,
                                        user_constraints b,
                                        user_cons_columns c,
                                        user_cons_columns d
                                        where
                                            a.r_constraint_name=b.constraint_name
                                        and a.constraint_type='R'
                                        and b.constraint_type='P'
                                        and a.r_owner=b.owner
                                        and a.constraint_name=c.constraint_name
                                        and b.constraint_name=d.constraint_name
                                        and a.owner=c.owner
                                        and a.table_name=c.table_name
                                        and b.owner=d.owner
                                        and b.table_name=d.table_name and a.table_name='" + tableName + "'";
            using (DbDataReader odr = OracleHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (odr.Read())
                {
                    ForeignKeyInfo model = new ForeignKeyInfo();
                    if (!odr.IsDBNull(0))
                        model.ConstraintName = odr.GetString(0);
                    if (!odr.IsDBNull(1))
                        model.SubTableName = odr.GetString(1);
                    if (!odr.IsDBNull(2))
                        model.SubTableColumnName = odr.GetString(2);
                    if (!odr.IsDBNull(3))
                        model.MainTableName = odr.GetString(3);
                    if (!odr.IsDBNull(4))
                        model.MainTableColumnName = odr.GetString(4);
                    fks.Add(model);
                }
            }

            return fks;
        }

        public string ToCSType(string type, int length, int scale)
        {
            string csType = "object";
            type = type.ToArrowString().ToLower();
            switch (type)
            {
                case "varchar":
                    csType = "string";
                    break;
                case "varchar2":
                    csType = "string";
                    break;
                case "nvarchar":
                    csType = "string";
                    break;
                case "nvarchar2":
                    csType = "string";
                    break;
                case "char":
                    csType = "string";
                    break;
                case "nchar":
                    csType = "string";
                    break;
                case "clob":
                    csType = "string";
                    break;
                case "nclob":
                    csType = "string";
                    break;
                case "blob":
                    csType = "byte[]";
                    break;
                case "date":
                    csType = "DateTime";
                    break;
                case "number":
                    csType = "decimal";
                    if (scale == 0)
                    {
                        if (length > 10)
                            csType = "long";
                        else
                            csType = "int";
                    }
                    break;
                default:
                    csType = "object";
                    break;
            }
            return csType;
        }

        public string ToReaderGetType(string type, int length, int scale)
        {
            string readerType = "Value";
            string csType = ToCSType(type, length, scale);
            switch (csType)
            {
                case "int":
                    readerType = "Int32";
                    break;
                case "short":
                    readerType = "Int16";
                    break;
                case "long":
                    readerType = "Int64";
                    break;
                case "bool":
                    readerType = "Boolean";
                    break;
                case "object":
                    readerType = "Value";
                    break;
                case "byte[]":
                    readerType = "Value";
                    break;
                default:
                    readerType = csType.Substring(0, 1).ToUpper() + csType.Substring(1);
                    break;
            }
            return readerType;
        }
        #endregion
    }
}
