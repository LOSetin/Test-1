using Arrow.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Arrow.Framework.CodeFactory
{
    public class SqlServerFactory : IFactory
    {
        private ProjectInfo pro;
        private DataAccess.Database SqlHelper;

        public SqlServerFactory(ProjectInfo pro)
        {
            this.pro = pro;
            SqlHelper = new DataAccess.Database(pro.ConnectionName);
        }

        #region Sql语句
        public string SqlAllFields(List<FieldInfo> fields)
        {
            return FactoryHelper.CreateFieldList(fields, true, true, "");
        }

        public string SqlInsert(string tableName, List<FieldInfo> fields)
        {
            FieldInfo pk = fields.Find(s => s.IsPrimaryKey);
            if (pk == null) return "";
            string sql = "Insert Into " + tableName + " (";
            //如果主键是自增，则不包括主键和自增
            if (pk.IsAutoIncrease)
            {
                sql = sql + FactoryHelper.CreateFieldList(fields, false, false, "");
                sql = sql + ")Values(" + FactoryHelper.CreateFieldList(fields, false, false, ParaSymbol) + ")";
            }
            else
            {
                //如果主键不是自增，则必须手动输入主键，因此包括主键字段
                sql = sql + FactoryHelper.CreateFieldList(fields, true, false, "");
                sql = sql + ")Values(" + FactoryHelper.CreateFieldList(fields, true, false, ParaSymbol) + ")";
            }
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
            string sql = "Select " + SqlAllFields(fields) + " From " + tableName + " Where ";
            return sql;
        }

        public string SqlTop(string tableName, List<FieldInfo> fields)
        {
            string sql = "Select Top {0} " + SqlAllFields(fields) + " From " + tableName + " Where {1} Order By {2}";
            return sql;
        }

        public string SqlPage(string tableName, List<FieldInfo> fields)
        {
            string sql = @"Select {0} From(Select {0},Row_Number() Over(Order By {3}) as RowNum From {1} Where {2}) t Where RowNum>{4} and RowNum<={5} ";
            return sql;
        }

        public string SqlCount(string tableName)
        {
            string val = "Select Count(*) From " + tableName + " Where ";
            return val;
        }

        #endregion

        #region 基本方法与属性

        public string ParaSymbol
        {
            get { return "@"; }
        }

        public string ExtraFieldName
        {
            get
            {
                return "ExtraFields";
            }
        }

        public string NameFormat(string name)
        {
            return FactoryHelper.NameFormat(name, pro.IsNameSplitWithUnderLine); 
        }

        public List<string> GetTables()
        {
            List<string> lstTables = new List<string>();
            string strsql = "";
            if (pro != null)
            {
                strsql = "select name from dbo.sysobjects where xtype='u' and (not name like 'dtproperties')";
                using (DbDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strsql, null))
                {
                    while (sdr.Read())
                    {
                        if (!sdr.IsDBNull(0))
                        {
                            string s = sdr.GetString(0);
                            lstTables.Add(s);
                        }
                    }
                }
            }
            return lstTables;
        }

        public List<string> GetViews()
        {
            List<string> lstViews = new List<string>();
            string strsql = "";
            if (pro != null)
            {
                strsql = "select name from sys.all_objects where type='v' and  is_ms_shipped =0";
                using (DbDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, strsql, null))
                {
                    while (sdr.Read())
                    {
                        if (!sdr.IsDBNull(0))
                        {
                            string s = sdr.GetString(0);
                            lstViews.Add(s);
                        }
                    }
                }
            }
            return lstViews;
        }

        public List<FieldInfo> GetTableFields(string tbName)
        {
            List<FieldInfo> lstField = new List<FieldInfo>();
            if (pro != null && !string.IsNullOrEmpty(tbName))
            {
                //获取所有字段
                string sqlGetFields = @"select sys.columns.name, sys.types.name as ctype, sys.columns.max_length, sys.columns.is_nullable, (select count(*) from sys.identity_columns where sys.identity_columns.object_id = sys.columns.object_id and sys.columns.column_id = sys.identity_columns.column_id) as is_identity , (select value from sys.extended_properties where sys.extended_properties.major_id = sys.columns.object_id and sys.extended_properties.minor_id = sys.columns.column_id) as description from sys.columns, sys.tables, sys.types where sys.columns.object_id = sys.tables.object_id and sys.columns.system_type_id=sys.types.system_type_id and sys.tables.name='" + tbName + "'  order by sys.columns.column_id ";
                string sqlGetPrimary = "Select name From SysColumns Where id=Object_Id('" + tbName + "') and ColID in (select keyno from sysindexkeys where id=Object_Id('" + tbName + "'))";

                //获得主键
                object o = SqlHelper.ExecuteScalar(CommandType.Text, sqlGetPrimary, null);
                string pk = "";
                if (o != DBNull.Value && o != null)
                {
                    pk = o.ToString();
                }
                //获得unique
                List<string> uniqueFields = GetUniqueFields(tbName);

                using (DbDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sqlGetFields, null))
                {
                    while (sdr.Read())
                    {
                        FieldInfo fi = new FieldInfo();
                        //字段名
                        fi.FieldName = sdr.GetString(0);

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
                        fi.FieldType = sdr.GetString(1);
                        //字段长度
                        fi.FieldLength = sdr.GetInt16(2);
                        //是否可空
                        if (!sdr.IsDBNull(3))
                            fi.IsNullable = sdr.GetBoolean(3);
                        else
                            fi.IsNullable = false;
                        //是否自增
                        if (!sdr.IsDBNull(4))
                            fi.IsAutoIncrease = sdr.GetInt32(4) == 1 ? true : false;
                        else
                            fi.IsAutoIncrease = false;
                        //显示名和描述
                        if (!sdr.IsDBNull(5))
                        {
                            fi.Desc = sdr.GetString(5);
                            string desc = fi.Desc.Replace("，", ",");
                            string[] arr = desc.Split(',');
                            fi.DisplayName = arr[0];
                        }
                        else
                        {
                            fi.DisplayName = "";
                            fi.Desc = "";
                        }
                        //小数位数
                        fi.FieldScale = 0;
                        //C#类型
                        fi.VarType = ToCSType(fi.FieldType, fi.FieldLength, fi.FieldScale);
                        //ReaderGetType
                        fi.ReaderGetType = ToReaderGetType(fi.FieldType, fi.FieldLength, fi.FieldScale);

                        if (fi.FieldType != "sysname")
                            lstField.Add(fi);
                    }
                }
            }

            return lstField;
        }

        public List<FieldInfo> GetViewFields(string viewName)
        {
            string sql = " select sys.columns.name, sys.types.name as ctype, sys.columns.max_length, sys.columns.is_nullable from sys.columns, sys.views, sys.types where sys.columns.object_id = sys.views.object_id and sys.columns.system_type_id=sys.types.system_type_id and sys.views.name='" + viewName + "'  order by sys.columns.column_id";
            List<FieldInfo> lstField = new List<FieldInfo>();

            using (DbDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    FieldInfo fi = new FieldInfo();
                    //字段名
                    fi.FieldName = sdr.GetString(0);

                    //是否主键，视图没有主键
                    fi.IsPrimaryKey = false;

                    //是否唯一，不处理视图的唯一索引
                    fi.IsUnique = false;

                    //c#变量名
                    fi.PropertyName = NameFormat(fi.FieldName);
                    //字段类型
                    fi.FieldType = sdr.GetString(1);
                    //字段长度
                    fi.FieldLength = sdr.GetInt16(2);
                    //是否可空
                    if (!sdr.IsDBNull(3))
                        fi.IsNullable = sdr.GetBoolean(3);
                    else
                        fi.IsNullable = false;
                    //是否自增，不处理自增
                    fi.IsAutoIncrease = false;
                    //显示名和描述，视图没有描述，显示名用原始字段名代替
                    fi.Desc = "";
                    fi.DisplayName = fi.FieldName;
                    
                    //小数位数
                    fi.FieldScale = 0;
                    //C#类型
                    fi.VarType = ToCSType(fi.FieldType, fi.FieldLength, fi.FieldScale);
                    //ReaderGetType
                    fi.ReaderGetType = ToReaderGetType(fi.FieldType, fi.FieldLength, fi.FieldScale);

                    if (fi.FieldType != "sysname")
                        lstField.Add(fi);
                }
            }
            return lstField;
        }

        /// <summary>
        /// 获取唯一键
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> GetUniqueFields(string tableName)
        {
            List<string> lst = new List<string>();
            string sql = @"Select  tab.name as TableName,  idx.name as IdxName,  col.name as ColumnName From  sys.indexes idx    
                                        JOIN sys.index_columns idxCol       
	                                        ON (idx.object_id = idxCol.object_id           
		                                        AND idx.index_id = idxCol.index_id           
		                                        AND idx.is_unique_constraint = 1)    
                                        JOIN sys.tables tab      
	                                        ON (idx.object_id = tab.object_id and OBJECT_NAME(tab.object_id)='{0}')    
                                        JOIN sys.columns col      
	                                        ON (idx.object_id = col.object_id  AND idxCol.column_id = col.column_id)";
            sql = string.Format(sql, tableName);
            using (DbDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    if (!sdr.IsDBNull(2))
                        lst.Add(sdr.GetString(2));
                }
            }
            return lst;
        }

        /// <summary>
        /// 获取该表下的所有外键
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<ForeignKeyInfo> GetForeignKeys(string tableName)
        {
            List<ForeignKeyInfo> fks = new List<ForeignKeyInfo>();
            string sql = @"select fk.name as ConstraintName,
		                                        oMain.name  as  MainTableName,  
		                                        MainCol.name as MainTableColumnName,
		                                        oSub.name as SubTableName, 
		                                        SubCol.name as SubTableColumnName from sys.foreign_keys fk      
                                    JOIN sys.all_objects oSub          
		                                        ON (fk.parent_object_id = oSub.object_id)    
                                    JOIN sys.all_objects oMain 
		                                        ON (fk.referenced_object_id = oMain.object_id)    
                                    JOIN sys.foreign_key_columns fkCols         
                                                ON (fk.object_id = fkCols.constraint_object_id)    
                                    JOIN sys.columns SubCol         
                                                ON (oSub.object_id = SubCol.object_id  AND fkCols.parent_column_id = SubCol.column_id)    
                                    JOIN sys.columns MainCol         
                                                ON (oMain.object_id = MainCol.object_id   AND fkCols.referenced_column_id = MainCol.column_id) Where oSub.name='" + tableName + "'";
            using (DbDataReader sdr = SqlHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (sdr.Read())
                {
                    ForeignKeyInfo model = new ForeignKeyInfo();
                    if (!sdr.IsDBNull(0))
                        model.ConstraintName = sdr.GetString(0);
                    if (!sdr.IsDBNull(1))
                        model.MainTableName = sdr.GetString(1);
                    if (!sdr.IsDBNull(2))
                        model.MainTableColumnName = sdr.GetString(2);
                    if (!sdr.IsDBNull(3))
                        model.SubTableName = sdr.GetString(3);
                    if (!sdr.IsDBNull(4))
                        model.SubTableColumnName = sdr.GetString(4);
                    fks.Add(model);
                }
            }

            return fks;
        }

        public string ToCSType(string type, int length, int scale)
        {
             if (string.IsNullOrEmpty(type)) return "object";
            type = type.ToLower();
            string csType = "object";
            switch (type)
            {
                case "int":
                    csType = "int";
                    break;
                case "varchar":
                    csType = "string";
                    break;
                case "bit":
                    csType = "bool";
                    break;
                case "datetime":
                    csType = "DateTime";
                    break;
                case "decimal":
                    csType = "decimal";
                    break;
                case "float":
                    csType = "double";
                    break;
                case "image":
                    csType = "byte[]";
                    break;
                case "money":
                    csType = "decimal";
                    break;
                case "ntext":
                    csType = "string";
                    break;
                case "nvarchar":
                    csType = "string";
                    break;
                case "smalldatetime":
                    csType = "DateTime";
                    break;
                case "smallint":
                    csType = "int16";
                    break;
                case "text":
                    csType = "string";
                    break;
                case "bigint":
                    csType = "long";
                    break;
                case "binary":
                    csType = "byte[]";
                    break;
                case "char":
                    csType = "string";
                    break;
                case "nchar":
                    csType = "string";
                    break;
                case "numeric":
                    csType = "decimal";
                    break;
                case "real":
                    csType = "Single";
                    break;
                case "smallmoney":
                    csType = "decimal";
                    break;
                case "timestamp":
                    csType = "byte[]";
                    break;
                case "tinyint":
                    csType = "byte";
                    break;
                case "uniqueidentifier":
                    csType = "Guid";
                    break;
                case "varbinary":
                    csType = "byte[]";
                    break;
                default:
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
