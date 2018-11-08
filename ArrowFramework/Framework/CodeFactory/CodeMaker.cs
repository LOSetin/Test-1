using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Arrow.Framework.Extensions;
using System.Data.Common;

namespace Arrow.Framework.CodeFactory
{
    /// <summary>
    /// 单数据库三层代码生成，从数据库生成代码
    /// </summary>
    public class CodeMaker
    {
        private ProjectInfo pro;
        private IFactory factory;

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="pro">项目实例</param>
        public CodeMaker(ProjectInfo pro)
        {
            this.pro = pro;
            this.factory = FactoryHelper.CreateFactory(pro);
        }

        /// <summary>
        /// 生成普通三层代码，文档说明
        /// </summary>
        public void Work()
        {
            List<string> tables = factory.GetTables();
            List<string> views = factory.GetViews();
            string baseDir = GetBasePath();
            string modelDir = baseDir + "\\Model";
            string dalDir = baseDir + "\\DataAccess";
            string viewDir = baseDir + "\\View";
            string viewDALDir = baseDir + "\\ViewDataAccess";
            string docDir = baseDir + "\\Doc";
            try
            {
                if (Directory.Exists(baseDir))
                {
                    Directory.Delete(baseDir, true);
                }
                Directory.CreateDirectory(baseDir);
                Directory.CreateDirectory(modelDir);
                Directory.CreateDirectory(dalDir);
                Directory.CreateDirectory(viewDir);
                Directory.CreateDirectory(viewDALDir);
                Directory.CreateDirectory(docDir);

                //开始生成表
                foreach (string table in tables)
                {
                    string className = factory.NameFormat(table);
                    File.WriteAllText(modelDir + "\\" + className + "Info.cs", CreateTableOrViewModel(table, true), Encoding.Default);
                    File.WriteAllText(dalDir + "\\" + className + ".cs", CreateTableDAL(table), Encoding.Default);
                }

                File.WriteAllText(docDir+"\\database.html", CreateDbDoc(tables), Encoding.Default);
                File.WriteAllText(baseDir + "\\Db.cs", CreateDbHelper(), Encoding.Default);

                //开始生成视图
                foreach (string view in views)
                {
                    string viewName = factory.NameFormat(view);
                    File.WriteAllText(viewDir + "\\" + viewName + "Info.cs", CreateTableOrViewModel(view, false), Encoding.Default);
                    File.WriteAllText(viewDALDir + "\\" + viewName + ".cs", CreateViewDAL(view), Encoding.Default);
                }

            }
            catch
            {
                throw;
            }
        }

        #region 私有方法
        /// <summary>
        /// 获取生成代码根目录
        /// </summary>
        /// <returns></returns>
        private string GetBasePath()
        {
            string basePath = pro.CodeRootPath;
            if(basePath.IsNullOrEmpty())
            {
                basePath = AppDomain.CurrentDomain.BaseDirectory + pro.TopNameSpace;
            }

            return basePath; 
        }

        /// <summary>
        /// 生成数据库操作类
        /// </summary>
        /// <returns></returns>
        private string CreateDbHelper()
        {
            string connName = pro.ConnectionName;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Arrow.Framework.DataAccess;");
            sb.AppendLine();

            sb.AppendLine("namespace " + pro.TopNameSpace);
            sb.AppendLine("{");
            sb.AppendLine(Pad(1) + "public class Db");
            sb.AppendLine(Pad(1) + "{");
            sb.AppendLine(Pad(2) + "public static readonly Database Helper = new Database(\""+pro.ConnectionName+"\");");
            sb.AppendLine(Pad(1) + "}");
            sb.AppendLine("}");


            return sb.ToString();
        }

        /// <summary>
        /// 生成表实体类
        /// </summary>
        /// <param name="tableOrViewName">表或视图名</param>
        /// <param name="isTable">true表示表，false表示视图</param>
        /// <returns></returns>
        private string CreateTableOrViewModel(string tableOrViewName, bool isTable)
        {
            FieldInfo PK = new FieldInfo();
            List<FieldInfo> fields;
            if (isTable)
            {
                fields = factory.GetTableFields(tableOrViewName);
                PK = fields.Find(s => s.IsPrimaryKey);
            }
            else
            {
                fields = factory.GetViewFields(tableOrViewName);
            }

            if (PK == null) return "";
            
            StringBuilder sb = new StringBuilder();
          
            string className = factory.NameFormat(tableOrViewName) + "Info";

            sb.AppendLine("using System;");
            sb.AppendLine("using Arrow.Framework;");
            sb.AppendLine();
            if(isTable)
                sb.AppendLine("namespace " + pro.TopNameSpace );
            else
                sb.AppendLine("namespace " + pro.TopNameSpace);
            sb.AppendLine("{");
            sb.AppendLine(Pad(1) + "public partial class " + className + " : EntityBase");
            sb.AppendLine(Pad(1) + "{");
            sb.AppendLine(Pad(2) + "public " + className + " (){}");
            sb.AppendLine();

            //定义属性
            foreach (FieldInfo fi in fields)
            {
                if (!fi.FieldName.IsEqualTo(factory.ExtraFieldName))
                {
                    sb.AppendLine(Pad(2) + "/// <summary>");
                    sb.AppendLine(Pad(2) + "/// " + fi.Desc);
                    sb.AppendLine(Pad(2) + "/// </summary>");
                    sb.AppendLine(Pad(2) + "public " + fi.VarType + " " + fi.PropertyName+" { set; get; }");
                    sb.AppendLine();
                }
            }

            sb.AppendLine(Pad(2) + "public static readonly string TableOrViewName = \"" + tableOrViewName + "\";");
            sb.AppendLine(Pad(2) + "public static readonly string AllFields = \"" + factory.SqlAllFields(fields) + "\";");
            if (isTable)
                sb.AppendLine(Pad(2) + "public static readonly string TablePrimaryKey = \"" + PK.FieldName.ToArrowString() + "\";");

            sb.AppendLine(Pad(1) + "}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// 生成DAL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string CreateTableDAL(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            List<FieldInfo> fields = factory.GetTableFields(tableName);
            if (fields.Count == 0) return "";
            FieldInfo pkInfo = fields.Find(s => s.IsPrimaryKey);
            //找不到主键，则不生成
            if (pkInfo == null) return "缺少主键！";
            //UserID=@UserID的形式
            string pKeyFormat = pkInfo.FieldName + " = " + factory.ParaSymbol + pkInfo.FieldName;
            string pKeyType = pkInfo.VarType;
            string pkName = pkInfo.PropertyName;
            //首字母小写的主键名
            string varPKName = pkName.Substring(0, 1).ToLower() + pkName.Substring(1);
            string nameSpace = pro.TopNameSpace;
            string ModelName = factory.NameFormat(tableName) + "Info";

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Data.Common;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Arrow.Framework;");
            sb.AppendLine();

            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{"); 
            sb.AppendLine(Pad(1) + "public partial class " + factory.NameFormat(tableName));
            sb.AppendLine(Pad(1) + "{");
            sb.AppendLine(Pad(2) + "private const string TABLE_NAME = \""+tableName+"\";");
            sb.AppendLine(Pad(2) + "private const string ALL_FIELDS = \"" + factory.SqlAllFields(fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_ADD = \"" + factory.SqlInsert(tableName,fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_UPDATE = \"" + factory.SqlUpdate(tableName,fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_SELECT = \"" + factory.SqlSelect(tableName,fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_DELETE = \"" + factory.SqlDelete(tableName) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_TOP=\"" + factory.SqlTop(tableName, fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_COUNT=\"" + factory.SqlCount(tableName) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_PAGE=\"" + factory.SqlPage(tableName, fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string PK_PARA_SET= \"" + pKeyFormat + "\";");
            sb.AppendLine(Pad(2) + "private const string PK_PARA = \"" + factory.ParaSymbol + pkName + "\";");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "#region 私有方法");
            sb.AppendLine(Pad(2) + "private DbParameter[] makeParameterForAdd(" + ModelName + " model)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = {");
            sb.AppendLine(CreateParaForAdd(fields));
            sb.AppendLine(Pad(3) + "};");
            sb.AppendLine(Pad(3) + "return paras;");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "private DbParameter[] makeParameterForUpdate(" + ModelName + " model)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = {");
            sb.AppendLine(CreateParaForUpdate(fields));
            sb.AppendLine(Pad(3) + "};");
            sb.AppendLine(Pad(3) + "return paras;");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "private void fillModel(DbDataReader dr, " + ModelName + " model)");
            sb.AppendLine(Pad(2) + "{");
            sb.Append(ReaderGetValList(fields));
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "#endregion");
            sb.AppendLine();



            sb.AppendLine(Pad(2) + "#region 基本操作");
            //添加
            sb.AppendLine(Pad(2) + "public int Add(" + ModelName + " model)");
            sb.AppendLine(Pad(2) + "{"); 
            sb.AppendLine(Pad(3) + "DbParameter[] paras = makeParameterForAdd(model);");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(SQL_ADD, paras); ");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //更新一条数据
            sb.AppendLine(Pad(2) + "public int Update(" + ModelName + " model)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_UPDATE + PK_PARA_SET;");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = makeParameterForUpdate(model);");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //删除一条数据
            sb.AppendLine(Pad(2) + "public int Delete(" + pKeyType + " " + varPKName + ")");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_DELETE + PK_PARA_SET;");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, " + varPKName + ") };");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //批量删除数据
            sb.AppendLine(Pad(2) + "public int DeleteList(string strWhere, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_DELETE + strWhere;");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //获得记录总数
            sb.AppendLine(Pad(2) + "public int GetCount(string strWhere, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_COUNT + strWhere;");
            sb.AppendLine(Pad(3) + "int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));");
            sb.AppendLine(Pad(3) + "return sum;");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回一个对象
            sb.AppendLine(Pad(2) + "public " + ModelName + " Select(" + pKeyType + " " + varPKName + ")");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + PK_PARA_SET;");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, " + varPKName + ") };");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetModel<" + ModelName + ">(fillModel, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回对象列表
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(string strWhere, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + strWhere;");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回Top对象列表
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回分页列表
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "if (pageIndex < 1) pageIndex = 1;");
            sb.AppendLine(Pad(3) + "if (string.IsNullOrEmpty(strWhere)) strWhere = \"1=1\";");
            sb.AppendLine(Pad(3) + "int min = pageSize * (pageIndex - 1);");
            sb.AppendLine(Pad(3) + "int max = pageSize * pageIndex;");
            sb.AppendLine(Pad(3) + "string strSql = string.Format(SQL_PAGE, ALL_FIELDS, TABLE_NAME, strWhere, orderBy, min, max);");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel,strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回DataSet
            sb.AppendLine(Pad(2) + "public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + strWhere;");
            sb.AppendLine(Pad(3) + " return Db.Helper.ExecuteDataSet(strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine(Pad(2) + "#endregion");
            sb.AppendLine();
            
            sb.AppendLine(Pad(2) + "#region  事务操作");
            //添加一条数据，使用事务
            sb.AppendLine(Pad(2) + "public int Add(" + ModelName + " model, DbTransaction tran)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "DbParameter[] para = makeParameterForAdd(model);");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(tran, SQL_ADD, para);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //更新一条数据，使用事务
            sb.AppendLine(Pad(2) + "public int Update(" + ModelName + " model, DbTransaction tran)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_UPDATE + PK_PARA_SET;");
            sb.AppendLine(Pad(3) + "DbParameter[] para = makeParameterForUpdate(model);");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(tran, strSql, para);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //删除一条数据，使用事务
            sb.AppendLine(Pad(2) + "public int Delete(" + pKeyType + " " + varPKName + ", DbTransaction tran)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_DELETE + PK_PARA_SET;");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, " + varPKName + ") };");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(tran, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //批量删除数据，使用事务
            sb.AppendLine(Pad(2) + "public int DeleteList(string strWhere, DbTransaction tran, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_DELETE + strWhere;");
            sb.AppendLine(Pad(3) + "return Db.Helper.ExecuteNonQuery(tran, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //获得记录总数，使用事务
            sb.AppendLine(Pad(2) + "public int GetCount(string strWhere, DbTransaction tran, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_COUNT + strWhere;");
            sb.AppendLine(Pad(3) + "int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(tran, strSql, paras));");
            sb.AppendLine(Pad(3) + "return sum;");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回一个对象，使用事务
            sb.AppendLine(Pad(2) + "public " + ModelName + " Select(" + pKeyType + " " + varPKName + ", DbTransaction tran)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + PK_PARA_SET;");
            sb.AppendLine(Pad(3) + "DbParameter[] paras = { Db.Helper.MakeInParameter(PK_PARA, " + varPKName + ") };");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetModel<" + ModelName + ">(fillModel, strSql, paras, true, tran);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回对象列表，使用事务
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + strWhere;");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel, strSql, paras, true, tran);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "#endregion");

            sb.AppendLine(Pad(1) + "}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// 生成DAL
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        private string CreateViewDAL(string viewName)
        {
            StringBuilder sb = new StringBuilder();
            List<FieldInfo> fields = factory.GetViewFields(viewName);
            if (fields.Count == 0) return "";
   
            string nameSpace = pro.TopNameSpace;
            string ModelName = factory.NameFormat(viewName) + "Info";

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using System.Data.Common;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using Arrow.Framework;");
            sb.AppendLine();

            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{");
            sb.AppendLine(Pad(1) + "public partial class " + factory.NameFormat(viewName));
            sb.AppendLine(Pad(1) + "{");
            sb.AppendLine(Pad(2) + "private const string VIEW_NAME = \"" + viewName + "\";");
            sb.AppendLine(Pad(2) + "private const string ALL_FIELDS = \"" + factory.SqlAllFields(fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_SELECT = \"" + factory.SqlSelect(viewName, fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_TOP=\"" + factory.SqlTop(viewName, fields) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_COUNT=\"" + factory.SqlCount(viewName) + "\";");
            sb.AppendLine(Pad(2) + "private const string SQL_PAGE=\"" + factory.SqlPage(viewName, fields) + "\";");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "#region 私有方法");
            sb.AppendLine(Pad(2) + "private void fillModel(DbDataReader dr, " + ModelName + " model)");
            sb.AppendLine(Pad(2) + "{");
            sb.Append(ReaderGetValList(fields));
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            sb.AppendLine(Pad(2) + "#endregion");
            sb.AppendLine();


            //获得记录总数
            sb.AppendLine(Pad(2) + "public int GetCount(string strWhere, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_COUNT + strWhere;");
            sb.AppendLine(Pad(3) + "int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(strSql, paras));");
            sb.AppendLine(Pad(3) + "return sum;");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回对象列表
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(string strWhere, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + strWhere;");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回Top对象列表
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(int topNum, string strWhere, string strOrderBy, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = string.Format(SQL_TOP, topNum, strWhere, strOrderBy);");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel, strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回分页列表
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(string strWhere, string orderBy, int pageIndex, int pageSize, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "if (pageIndex < 1) pageIndex = 1;");
            sb.AppendLine(Pad(3) + "if (string.IsNullOrEmpty(strWhere)) strWhere = \"1=1\";");
            sb.AppendLine(Pad(3) + "int min = pageSize * (pageIndex - 1);");
            sb.AppendLine(Pad(3) + "int max = pageSize * pageIndex;");
            sb.AppendLine(Pad(3) + "string strSql = string.Format(SQL_PAGE, ALL_FIELDS, VIEW_NAME, strWhere, orderBy, min, max);");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel,strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回DataSet
            sb.AppendLine(Pad(2) + "public DataSet SelectDataSet(string strWhere, DbParameter[] paras = null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + strWhere;");
            sb.AppendLine(Pad(3) + " return Db.Helper.ExecuteDataSet(strSql, paras);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //获得记录总数，使用事务
            sb.AppendLine(Pad(2) + "public int GetCount(string strWhere, DbTransaction tran, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_COUNT + strWhere;");
            sb.AppendLine(Pad(3) + "int sum = Convert.ToInt32(Db.Helper.ExecuteScalar(tran, strSql, paras));");
            sb.AppendLine(Pad(3) + "return sum;");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            //返回对象列表，使用事务
            sb.AppendLine(Pad(2) + "public List<" + ModelName + "> SelectList(string strWhere, DbTransaction tran, DbParameter[] paras=null)");
            sb.AppendLine(Pad(2) + "{");
            sb.AppendLine(Pad(3) + "string strSql = SQL_SELECT + strWhere;");
            sb.AppendLine(Pad(3) + "return Db.Helper.GetList<" + ModelName + ">(fillModel, strSql, paras, true, tran);");
            sb.AppendLine(Pad(2) + "}");
            sb.AppendLine();

            sb.AppendLine(Pad(1) + "}");
            sb.AppendLine("}");

            return sb.ToString();
        }


        private string CreateTableDoc(string tableName)
        {
            List<FieldInfo> fields = factory.GetTableFields(tableName);
            StringBuilder sb = new StringBuilder();

            string tbDesc = "";
            sb.AppendLine("<div style=\"width: 90%\">表名：").Append(tableName).Append(tbDesc).Append("</div>");
            sb.AppendLine("<table width=\"90%\" border=\"1\" class=\"t1\">");
            sb.AppendLine(@"<thead>
            <th width=""8%"">序号</th>
           <th width=""20%"">字段名称</th>
        <th width=""20%"">字段类型</th>
        <th width=""8%"">非空</th>
            <th width=""8%"">主键</th>
        <th width=""36%"">说明</th>
        </thead>");
            int i = 0;
            foreach (FieldInfo model in fields)
            {
                string fieldType = model.FieldType;
                string desc = model.DisplayName;
                string fieldName = model.FieldName;

                string len = "";
                if (fieldType.ToLower() == "number")
                {
                    len = "(" + model.FieldLength + "," + model.FieldScale + ")";
                }
                else if (fieldType.ToLower().Contains("char"))
                {
                    if (model.FieldLength == -1)
                        len = "(max)";
                    else
                        len = "(" + model.FieldLength + ")";
                }
                else
                {
                    len = "";
                }

                fieldType = fieldType + len;

                string cls = "";
                if (i % 2 == 0) cls = "class=\"a1\"";
                sb.Append("<tr ").Append(cls).AppendLine(">");
                sb.Append("<td>").Append(i + 1).AppendLine("</td>");
                sb.Append("<td>").Append(fieldName).AppendLine("</td>");
                sb.Append("<td>").Append(fieldType).AppendLine("</td>");
                sb.Append("<td>").Append(model.IsNullable ? "" : "是").AppendLine("</td>");
                sb.Append("<td>").Append(model.IsPrimaryKey ? "是" : "").AppendLine("</td>");
                sb.Append("<td>").Append(desc).AppendLine("</td>");
                sb.AppendLine("</tr>");
                i = i + 1;
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        private string CreateDbDoc(List<string> tables)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html >
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=gb2312"" />
    <title>数据库</title>
    <style type=""text/css"">
        body, table { font-size: 14px; font-family:宋体,Verdana;}
        div { margin: 0 auto; height: 20px; line-height: 20px; padding-top: 20px; }
        table { table-layout: fixed; empty-cells: show; border-collapse: collapse; margin: 0 auto; }
        td { height: 25px; }
        h1, h2, h3 { font-size: 14x; margin: 0; padding: 0; }

        table.t1 { border: 1px solid #cad9ea; color: #333; }
        table.t1 th { background-color: #2F589C; background-repeat: repeat-x; height: 30px; color: #fff; }
        table.t1 td, table.t1 th { border: 1px solid #cad9ea;  text-align:center; }
        table.t1 tr.a1 { background-color: #f5fafe; }
    </style>
</head>
<body>");

          
            foreach (string table in tables)
            {
                string doc = CreateTableDoc(table);
                sb.AppendLine(doc);
            }


            sb.Append("<br/><br/></body></html>");
            return sb.ToString();
        }


        /// <summary>
        /// 返回左侧的空格
        /// </summary>
        /// <param name="num">单位1代表4个空格，相当于Tab</param>
        /// <returns></returns>
        private string Pad(int num)
        {
            return FactoryHelper.Pad(num);
        }

        private string CreateParaForAdd(List<FieldInfo> fields)
        {
            return FactoryHelper.CreateParaForAdd(fields, factory);
        }

        private string CreateParaForUpdate(List<FieldInfo> fields)
        {
            return FactoryHelper.CreateParaForUpdate(fields, factory);
        }

        /// <summary>
        /// SqlDataReader 获得值，生成形如model.ID=sdr.GetInt32(0);model.Name=sdr.getString(1);这种语句
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private string ReaderGetValList(List<FieldInfo> fields)
        {
            return FactoryHelper.ReaderGetValList(fields, factory); 
        }

        #endregion

    }
}
