using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Arrow.Framework.DataAccess
{
    /// <summary>
    /// 数据库提供者
    /// </summary>
    public static class DBProvider
    {
        /// <summary>
        /// System.Data.SqlClient
        /// </summary>
        public static readonly string SqlClient = "System.Data.SqlClient";

        /// <summary>
        /// System.Data.OracleClient
        /// </summary>
        //public static readonly string OracleClient = "System.Data.OracleClient";

        /// <summary>
        /// Oracle.DataAccess.Client
        /// </summary>
        public static readonly string OracleClient = "Oracle.ManagedDataAccess.Client";

        /// <summary>
        /// System.Data.OleDb
        /// </summary>
        public static readonly string OleDb = "System.Data.OleDb";

        /// <summary>
        /// System.Data.Odbc
        /// </summary>
        public static readonly string Odbc = "System.Data.Odbc";
        
    }
}
