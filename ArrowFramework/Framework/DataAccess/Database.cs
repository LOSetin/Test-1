using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

namespace Arrow.Framework.DataAccess
{
    /// <summary>
    /// 数据库操作类，可能在中等信任权限的IIS上面，无法创建Sqlserver和OLEDB之外的数据库连接对象。
    /// 因为使用了DbProviderFactory，所以若重视性能，应使用static readonly 缓存该对象。可参考PetShop中的BLL，缓存反射得来的DAL对象。
    /// </summary>
    public class Database
    {
        private string connectionString;
        private string providerName;
        private DbProviderFactory factory;

        #region 构造函数
        /// <summary>
        /// config文件里的数据库连接名
        /// </summary>
        /// <param name="dbConnName"></param>
        public Database(string dbConnName)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[dbConnName].ConnectionString;
            this.providerName = ConfigurationManager.ConnectionStrings[dbConnName].ProviderName;
            this.factory = DbProviderFactories.GetFactory(this.providerName);
        }

        /// <summary>
        /// 初始化数据库对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="providerName">数据提供名，例如System.Data.SqlClient</param>
        public Database(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.providerName = providerName;
            this.factory = DbProviderFactories.GetFactory(this.providerName);
        }
        #endregion

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
        }

        /// <summary>
        /// 数据提供者名称
        /// </summary>
        public string ProviderName
        {
            get { return this.providerName; }
        }

        #region 填充实体类

        /// <summary>
        /// 从DataReader填充实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fillModel">填充的方法</param>
        /// <param name="strSql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <param name="useTran">是否使用事务处理</param>
        /// <param name="tran">事务对象</param>
        /// <returns></returns>
        public virtual T GetModel<T>(Action<DbDataReader, T> fillModel, string strSql, DbParameter[] paras = null, bool useTran = false, DbTransaction tran = null) where T : class, new()
        {
            T model = new T();
            using (DbDataReader dr = useTran ? ExecuteReader(tran, strSql, paras) : ExecuteReader(strSql, paras))
            {
                if (dr.Read())
                {
                    fillModel(dr, model);
                }
                else
                {
                    model = null;
                }
            }
            return model;
        }


        /// <summary>
        /// 从DataReader填充实体类列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fillModel">填充的方法</param>
        /// <param name="strSql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <param name="useTran">是否使用事务处理</param>
        /// <param name="tran">事务对象</param>
        /// <returns></returns>
        public virtual List<T> GetList<T>(Action<DbDataReader, T> fillModel, string strSql, DbParameter[] paras = null, bool useTran = false, DbTransaction tran = null) where T : class, new()
        {
            List<T> modelList = new List<T>();
            using (DbDataReader dr = useTran ? ExecuteReader(tran, strSql, paras) : ExecuteReader(strSql, paras))
            {
                while (dr.Read())
                {
                    T model = new T();
                    fillModel(dr, model);
                    modelList.Add(model);
                }
            }
            return modelList;
        }


        #endregion

        #region 生成基本对象
        public DbCommand CreateCommand()
        {
            return factory.CreateCommand();
        }

        public DbConnection CreateConnection()
        {
            return factory.CreateConnection();
        }

        public DbDataAdapter CreateAdapter()
        {
            return factory.CreateDataAdapter();
        }

        public DbParameter CreateParameter()
        {
            return factory.CreateParameter();
        }

        public DbParameter MakeInParameter(string name, object value)
        {
            DbParameter para = factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = ParameterDirection.Input;
            return para;
        }

        public DbParameter MakeInParameter(string name, object value, DbType dbType)
        {
            return MakeParameter(name, value, dbType, ParameterDirection.Input);
        }

        public DbParameter MakeParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            DbParameter para = factory.CreateParameter();
            para.ParameterName = name;
            para.Value = value;
            para.Direction = direction;
            para.DbType = dbType;
            return para;
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 使用连接字符串执行SQL
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            DbCommand cmd = CreateCommand();
            using (DbConnection conn = CreateConnection())
            {
                conn.ConnectionString = connectionString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 使用默认连接字符串执行SQL语句，使用参数方式CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 使用事务执行SQL
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            if (trans == null)
                throw new ArgumentNullException("transaction");
            if (trans != null && trans.Connection == null)
                throw new ArgumentException("事务已回滚或提交，请提供一个可用的事务。", "transaction");

            DbCommand cmd = CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 事务处理，使用参数方式CommandType.Text
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(DbTransaction trans, string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteNonQuery(trans, CommandType.Text, cmdText, commandParameters);
        }

        #endregion

        #region ExecuteReader
        /// <summary>
        /// 返回DataReader
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            DbCommand cmd = CreateCommand();
            DbConnection conn = CreateConnection();
            conn.ConnectionString = connectionString;
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 返回DataReader，使用默认连接字符串，使用CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteReader(CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 执行查询，返回DataReader，使用事务，需手动关闭DataReader或对应的数据库连接
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">SQL命令或存储过程名</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            if (trans == null)
                throw new ArgumentNullException("transaction");
            if (trans != null && trans.Connection == null)
                throw new ArgumentException("事务已回滚或提交，请提供一个可用的事务。", "transaction");

            DbCommand cmd = CreateCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                //需手动关闭DataReader或对应的数据库连接
                DbDataReader sdr = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return sdr;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 执行查询，返回DataReader，使用事务，需手动关闭DataReader或对应的数据库连接
        /// </summary>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(DbTransaction trans, string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteReader(trans, CommandType.Text, cmdText, commandParameters);
        }


        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            DbCommand cmd = CreateCommand();
            using (DbConnection conn = CreateConnection())
            {
                conn.ConnectionString = connectionString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 返回首行首列，使用默认连接字符串，使用CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteScalar(CommandType.Text, cmdText, commandParameters);
        }

        ///	<summary>
        ///	执行查询，返回首行首列，使用事务
        ///	</summary>
        ///	<param name="transaction">有效的事务对象</param>
        ///	<param name="commandType">命令类型</param>
        ///	<param name="commandText">SQL命令或存储过程名</param>
        ///	<param name="commandParameters">命令参数</param>
        ///	<returns></returns>
        public virtual object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters = null)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("事务已回滚或提交，请提供一个可用的事务。", "transaction");

            DbCommand cmd = CreateCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            object retval = cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            return retval;
        }

        ///	<summary>
        ///	执行查询，返回首行首列，使用事务，SQL语句
        ///	</summary>
        ///	<param name="transaction">有效的事务对象</param>
        ///	<param name="commandText">SQL命令或存储过程名</param>
        ///	<param name="commandParameters">命令参数</param>
        ///	<returns></returns>
        public virtual object ExecuteScalar(DbTransaction transaction, string commandText, DbParameter[] commandParameters = null)
        {
            return ExecuteScalar(transaction, CommandType.Text, commandText, commandParameters);
        }


        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 返回DataSet，使用指定连接字符串
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            DbCommand cmd = CreateCommand();
            using (DbConnection conn = CreateConnection())
            {
                conn.ConnectionString = connectionString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DbDataAdapter da = CreateAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return (ds);
            }
        }

        /// <summary>
        /// 返回DataSet，使用默认连接字符串， CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteDataSet(CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回DataSet，使用事务
        /// </summary>
        /// <param name="transaction">有效的事务</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">SQL语句或存储过程名</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters = null)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("事务已回滚或提交，请提供一个可用的事务。", "transaction");

            DbCommand cmd = CreateCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            using (DbDataAdapter da = CreateAdapter())
            {
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 返回DataSet，使用事务
        /// </summary>
        /// <param name="transaction">有效的事务</param>
        /// <param name="commandText">SQL语句</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(DbTransaction transaction, string commandText, DbParameter[] commandParameters = null)
        {
            return ExecuteDataSet(transaction, CommandType.Text, commandText, commandParameters);
        }


        #endregion

        #region PrepareCommand
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

    }
}