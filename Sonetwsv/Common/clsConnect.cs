using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Sonetwsv.common
{
    public class clsConnect
    {
        internal static System.Data.Common.DbProviderFactory DbProviderFactory;
        internal static System.Data.Common.DbConnection DbConnection;
        internal static System.Data.Common.DbTransaction DbTransaction;

        //Keo dai thoi gian ket noi khac phuc TimeOut
        internal static DbCommand CreateCommand()
        {
            DbCommand x = DbConnection.CreateCommand();
            x.CommandTimeout = int.MaxValue;
            return x;
        }
        /// <summary>
        /// Ham nay de mo ket noi den database
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="DatabaseName"></param>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool DB_OpenConnection(string ServerName, string DatabaseName, string UserId, string Password)
        {
            DbProviderFactory = System.Data.SqlClient.SqlClientFactory.Instance;
            DbConnectionStringBuilder DbConnectionStringBuilder = DbProviderFactory.CreateConnectionStringBuilder();

            DbConnectionStringBuilder.Add("Data Source", ServerName);
            DbConnectionStringBuilder.Add("User ID", UserId);
            DbConnectionStringBuilder.Add("Password", Password);
            DbConnectionStringBuilder.Add("Initial Catalog", DatabaseName);

            bool IsConnected;
            try
            {
                if (DbConnection == null)
                    DbConnection = DbProviderFactory.CreateConnection();
                if (DbConnection.State != ConnectionState.Open)
                {
                    DbConnection.ConnectionString = DbConnectionStringBuilder.ConnectionString;
                    DbConnection.Open();
                }
                IsConnected = true;
            }
            catch
            {
                IsConnected = false;
            }
            return IsConnected;
        }
        /// <summary>
        /// De ngat ke noi den Database
        /// </summary>
        public static void DB_CloseConnection()
        {
            if (DbConnection.State == ConnectionState.Open)
                DbConnection.Close();
        }
        /// <summary>
        /// Xac lap thong bao ket noi den Database
        /// </summary>
        /// <param name="DbException"></param>
        /// <returns></returns>
        public static int GetDbException(DbException DbException)
        {
            int Number = -1;

            if (DbException is System.Data.SqlClient.SqlException)
            {
                Number = (DbException as System.Data.SqlClient.SqlException).Number;
            }
            return Number;
        }

        public static string Encrypt(string strPassword)
        {
            System.Security.Cryptography.MD5 md5Cryptography = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return Convert.ToBase64String(md5Cryptography.ComputeHash(new System.Text.UTF8Encoding().GetBytes(strPassword)));
        }
    }
}
