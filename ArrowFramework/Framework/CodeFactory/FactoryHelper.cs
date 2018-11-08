using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arrow.Framework.Extensions;
using System.Configuration;

namespace Arrow.Framework.CodeFactory
{
    public static class FactoryHelper
    {
        /// <summary>
        /// 返回左侧的空格
        /// </summary>
        /// <param name="num">单位1代表4个空格，相当于Tab</param>
        /// <returns></returns>
        public static string Pad(int num)
        {
            return "".PadLeft(num * 4);
        }

        public static string NameFormat(string name,bool isNameSplitWithUnderline)
        {
            if (!isNameSplitWithUnderline)  return name;

            string newName = "";
            string[] arr = name.Split('_');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].IsEqualTo("id"))
                    newName = newName + "ID";
                else
                    newName = newName + arr[i].Substring(0, 1).ToUpper() + arr[i].Substring(1);
            }
            return newName;
            
        }

        /// <summary>
        /// 判断字段类型是否字符串
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public static bool IsStringType(string fieldType)
        {
            return fieldType.ToLower().Contains("char") || fieldType.ToLower().Contains("lob");
        }

        /// <summary>
        /// 生成字段列表，例如UserName,Age
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="includePrimaryKey"></param>
        /// <param name="includeAutoIncreaseField"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string CreateFieldList(List<FieldInfo> fields, bool includePrimaryKey, bool includeAutoIncreaseField, string prefix)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fields.Count; i++)
            {
                if ((fields[i].IsPrimaryKey && includePrimaryKey) || (fields[i].IsAutoIncrease && includeAutoIncreaseField) || (!fields[i].IsPrimaryKey && !fields[i].IsAutoIncrease))
                {
                    sb.Append(prefix).Append(fields[i].FieldName).Append(",");
                }
            }
            string result = sb.ToString();
            if (result.EndsWith(","))
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        /// <summary>
        /// 生成update语句的带参数的字段列表，形如ID=@ID
        /// </summary>
        /// <param name="lstField">字段集合</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static string CreateUpdateFieldList(List<FieldInfo> lstField, string prefix)
        {
            string val = "";

            for (int i = 0; i < lstField.Count; i++)
            {
                if (!lstField[i].IsAutoIncrease && !lstField[i].IsPrimaryKey)
                    val += lstField[i].FieldName + "=" + prefix + lstField[i].FieldName + ",";
            }

            if (val.EndsWith(",")) val = val.Substring(0, val.Length - 1);
            return val;
        }

        public static string CreateParaForAdd(List<FieldInfo> fields, IFactory factory)
        {
            StringBuilder sb = new StringBuilder();
            string prefix = factory.ParaSymbol;
            for (int i = 0; i < fields.Count; i++)
            {
                string fieldName = fields[i].FieldName;

                if (!fields[i].IsAutoIncrease)
                {
                    if (fields[i].FieldName.ToLower() == factory.ExtraFieldName.ToLower())
                    { 
                        //扩展字段
                        sb.AppendLine(Pad(5) + "Db.Helper.MakeInParameter(\"" + prefix + fieldName + "\",FieldsHelper.XmlSerialize(model." + fields[i].PropertyName + ")),");
                    }
                    else
                    {
                        sb.AppendLine(Pad(5) + "Db.Helper.MakeInParameter(\"" + prefix + fieldName + "\",model." + fields[i].PropertyName+"),");
                    }
                }
            }

            string val = sb.ToString().TrimEnd();
            if (val.EndsWith(","))
                val = val.Substring(0, val.Length - 1);
            return val;
        }

        public static string CreateParaForUpdate(List<FieldInfo> fields, IFactory factory)
        {
            StringBuilder sb = new StringBuilder();
            string prefix = factory.ParaSymbol;

            for (int i = 0; i < fields.Count; i++)
            {
                string fieldName = fields[i].FieldName;

                if (!fields[i].IsAutoIncrease && !fields[i].IsPrimaryKey)
                {
                    if (fields[i].FieldName.ToLower() == factory.ExtraFieldName.ToLower())
                    {
                        //扩展字段
                        sb.AppendLine(Pad(5) + "Db.Helper.MakeInParameter(\"" + prefix + fieldName + "\",FieldsHelper.XmlSerialize(model." + fields[i].PropertyName + ")),");
                    }
                    else
                    {
                        sb.AppendLine(Pad(5) + "Db.Helper.MakeInParameter(\"" + prefix + fieldName + "\",model." + fields[i].PropertyName + "),");
                    }
                }
            }

            FieldInfo pkInfo = fields.Find(s => s.IsPrimaryKey);
            sb.AppendLine(Pad(5) + "Db.Helper.MakeInParameter(\"" + prefix + pkInfo.FieldName + "\",model." + pkInfo.PropertyName + ")");


            string val = sb.ToString().TrimEnd();
            if (val.EndsWith(","))
                val = val.Substring(0, val.Length - 1);
            return val;
        }

        /// <summary>
        /// SqlDataReader 获得值，生成形如model.ID=sdr.GetInt32(0);model.Name=sdr.getString(1);这种语句
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string ReaderGetValList(List<FieldInfo> fields, IFactory factory)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < fields.Count; i++)
            {
        
                if (fields[i].FieldName.ToLower() == factory.ExtraFieldName.ToLower())
                {
                    //读取扩展字段
                    sb.AppendLine(Pad(5) + "model." + fields[i].PropertyName + " = FieldsHelper.XmlDeserialize(dr.Get" + fields[i].ReaderGetType + "(" + i.ToString() + "));");
                }
                else
                {
                    if (fields[i].IsNullable)
                    {
                        sb.AppendLine(Pad(5) + "if (!dr.IsDBNull(" + i.ToString() + "))");
                        sb.AppendLine(Pad(6) + "model." + fields[i].PropertyName + " = dr.Get" + fields[i].ReaderGetType + "(" + i.ToString() + ");");
                    }
                    else
                    {
                        sb.AppendLine(Pad(5) + "model." + fields[i].PropertyName + " = dr.Get" + fields[i].ReaderGetType + "(" + i.ToString() + ")" + ";");
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 生成Factory实例
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static IFactory CreateFactory(ProjectInfo pro)
        {
            string providerName = ConfigurationManager.ConnectionStrings[pro.ConnectionName].ProviderName;

            if (providerName.IsEqualTo(DataAccess.DBProvider.SqlClient))
            {
                return new SqlServerFactory(pro);
            }
            else if (providerName.IsEqualTo(DataAccess.DBProvider.OracleClient))
            {
                return new OracleFactory(pro);
            }
            else
            {
                throw new NullReferenceException("数据库类型不支持，实例化失败！");
            }
        }
    }
}
