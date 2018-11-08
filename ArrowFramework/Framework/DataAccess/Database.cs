using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

namespace Arrow.Framework.DataAccess
{
    /// <summary>
    /// ���ݿ�����࣬�������е�����Ȩ�޵�IIS���棬�޷�����Sqlserver��OLEDB֮������ݿ����Ӷ���
    /// ��Ϊʹ����DbProviderFactory���������������ܣ�Ӧʹ��static readonly ����ö��󡣿ɲο�PetShop�е�BLL�����淴�������DAL����
    /// </summary>
    public class Database
    {
        private string connectionString;
        private string providerName;
        private DbProviderFactory factory;

        #region ���캯��
        /// <summary>
        /// config�ļ�������ݿ�������
        /// </summary>
        /// <param name="dbConnName"></param>
        public Database(string dbConnName)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[dbConnName].ConnectionString;
            this.providerName = ConfigurationManager.ConnectionStrings[dbConnName].ProviderName;
            this.factory = DbProviderFactories.GetFactory(this.providerName);
        }

        /// <summary>
        /// ��ʼ�����ݿ����
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="providerName">�����ṩ��������System.Data.SqlClient</param>
        public Database(string connectionString, string providerName)
        {
            this.connectionString = connectionString;
            this.providerName = providerName;
            this.factory = DbProviderFactories.GetFactory(this.providerName);
        }
        #endregion

        /// <summary>
        /// �����ַ���
        /// </summary>
        public string ConnectionString
        {
            get { return this.connectionString; }
        }

        /// <summary>
        /// �����ṩ������
        /// </summary>
        public string ProviderName
        {
            get { return this.providerName; }
        }

        #region ���ʵ����

        /// <summary>
        /// ��DataReader���ʵ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fillModel">���ķ���</param>
        /// <param name="strSql">sql���</param>
        /// <param name="paras">����</param>
        /// <param name="useTran">�Ƿ�ʹ��������</param>
        /// <param name="tran">�������</param>
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
        /// ��DataReader���ʵ�����б�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fillModel">���ķ���</param>
        /// <param name="strSql">sql���</param>
        /// <param name="paras">����</param>
        /// <param name="useTran">�Ƿ�ʹ��������</param>
        /// <param name="tran">�������</param>
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

        #region ���ɻ�������
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
        /// ʹ�������ַ���ִ��SQL
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
        /// ʹ��Ĭ�������ַ���ִ��SQL��䣬ʹ�ò�����ʽCommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// ʹ������ִ��SQL
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
                throw new ArgumentException("�����ѻع����ύ�����ṩһ�����õ�����", "transaction");

            DbCommand cmd = CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ������ʹ�ò�����ʽCommandType.Text
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
        /// ����DataReader
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
        /// ����DataReader��ʹ��Ĭ�������ַ�����ʹ��CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteReader(CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// ִ�в�ѯ������DataReader��ʹ���������ֶ��ر�DataReader���Ӧ�����ݿ�����
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">SQL�����洢������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] commandParameters = null)
        {
            if (trans == null)
                throw new ArgumentNullException("transaction");
            if (trans != null && trans.Connection == null)
                throw new ArgumentException("�����ѻع����ύ�����ṩһ�����õ�����", "transaction");

            DbCommand cmd = CreateCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                //���ֶ��ر�DataReader���Ӧ�����ݿ�����
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
        /// ִ�в�ѯ������DataReader��ʹ���������ֶ��ر�DataReader���Ӧ�����ݿ�����
        /// </summary>
        /// <param name="trans">�������</param>
        /// <param name="cmdText">SQL���</param>
        /// <param name="commandParameters">�������</param>
        /// <returns></returns>
        public virtual DbDataReader ExecuteReader(DbTransaction trans, string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteReader(trans, CommandType.Text, cmdText, commandParameters);
        }


        #endregion

        #region ExecuteScalar
        /// <summary>
        /// ������������
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
        /// �����������У�ʹ��Ĭ�������ַ�����ʹ��CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteScalar(CommandType.Text, cmdText, commandParameters);
        }

        ///	<summary>
        ///	ִ�в�ѯ�������������У�ʹ������
        ///	</summary>
        ///	<param name="transaction">��Ч���������</param>
        ///	<param name="commandType">��������</param>
        ///	<param name="commandText">SQL�����洢������</param>
        ///	<param name="commandParameters">�������</param>
        ///	<returns></returns>
        public virtual object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters = null)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("�����ѻع����ύ�����ṩһ�����õ�����", "transaction");

            DbCommand cmd = CreateCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            object retval = cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            return retval;
        }

        ///	<summary>
        ///	ִ�в�ѯ�������������У�ʹ������SQL���
        ///	</summary>
        ///	<param name="transaction">��Ч���������</param>
        ///	<param name="commandText">SQL�����洢������</param>
        ///	<param name="commandParameters">�������</param>
        ///	<returns></returns>
        public virtual object ExecuteScalar(DbTransaction transaction, string commandText, DbParameter[] commandParameters = null)
        {
            return ExecuteScalar(transaction, CommandType.Text, commandText, commandParameters);
        }


        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// ����DataSet��ʹ��ָ�������ַ���
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
        /// ����DataSet��ʹ��Ĭ�������ַ����� CommandType.Text
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(string cmdText, DbParameter[] commandParameters = null)
        {
            return ExecuteDataSet(CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// ����DataSet��ʹ������
        /// </summary>
        /// <param name="transaction">��Ч������</param>
        /// <param name="commandType">��������</param>
        /// <param name="commandText">SQL����洢������</param>
        /// <param name="commandParameters">�������</param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters = null)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("�����ѻع����ύ�����ṩһ�����õ�����", "transaction");

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
        /// ����DataSet��ʹ������
        /// </summary>
        /// <param name="transaction">��Ч������</param>
        /// <param name="commandText">SQL���</param>
        /// <param name="commandParameters">�������</param>
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