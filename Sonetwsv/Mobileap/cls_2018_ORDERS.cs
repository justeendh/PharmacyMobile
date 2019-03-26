using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobileap
{
    public class cls_2018_ORDERS
    {
        //Attribute
        private const string PKeyOrderClient = "@KEY_ORDER_CLIENT";
        private const string PKeyCardClient = "@KEY_CARD_CLIENT";

        //Procedure
        private const string SpMobile_2018_GetallOrders = "VVV_2018_GETLISTALLORDERS";
        private const string SpMobile_2018_GetoneOrders = "VVV_2018_GETLISTONEORDERS";
        private const string SpMobile_2018_GetsallGoods = "VVV_2018_GETLISTGOODORDER";

        private const string SpMobile_2018_InsertOrders = "VVV_2018_INSERT_ORDERS";
        private const string SpMobile_2018_InsertGoodToOrders = "VVV_2018_INSERT_GOOD_ORDERS";

        private const string SpMobile_2018_UpdateOrders = "VVV_2018_UPDATEDONEORDERS";
        private const string SpMobile_2018_UpdatedGoods = "VVV_2018_UPDATEORDERGOODS";
        
        //Methodology

        /// <summary>
        /// Them don hang moi
        /// </summary>
        /// Tra ve: Don hang duoc them
        public static bool m2018_InsertNewOrder(ORDER_CLIENT item, ORDER_GOODS[] lstGood)
        {
            bool Result = true;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
                {
                    DbCommand.CommandText = SpMobile_2018_InsertOrders;
                    DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    DbCommand.Transaction = DbTransaction;
                    ORDER_CLIENT.CreateParameterCollection<ORDER_CLIENT>(DbCommand, item);

                    try
                    {
                        DbCommand.ExecuteNonQuery();                        
                    }
                    catch (DbException DbException)
                    {
                        DbTransaction.Rollback();
                        Result = false;
                        return Result;
                    }
                }
                                
                foreach (var itemGood in lstGood)
                {
                    try
                    {
                        m2018_InsertGoodToOrder(itemGood, DbTransaction);
                    }
                    catch (DbException DbException)
                    {
                        DbTransaction.Rollback();
                        Result = false;
                        return Result;
                    }
                }

                DbTransaction.Commit();
                return Result;
            }
        }

        public static void m2018_InsertGoodToOrder(ORDER_GOODS item, DbTransaction transaction)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_InsertGoodToOrders;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                if (transaction != null) DbCommand.Transaction = transaction;
                ORDER_GOODS.CreateParameterCollection<ORDER_GOODS>(DbCommand, item);

                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");
                DbCommand.ExecuteNonQuery();   
            }
        }
                
        /// <summary>
        /// Lay danh sach don hang thuoc cua khach hang
        /// </summary>
        /// Tra ve: Danh sach don hang duoc dang ky
        public static System.Data.DataTable m2018_GetallOrders(Guid KeyCardClient)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GetallOrders;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyCardClient;
                Parameter.Value = KeyCardClient;
                Parameters.Add(Parameter);

                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch (Exception ex) {
                    DataTable = null;
                }

                return DataTable;
            }
        }

        /// <summary>
        /// Lay mot don hang de xu ly
        /// </summary>
        /// Tra ve: Table chua don hang 
        public static System.Data.DataTable m2018_GetoneOrders(Guid KeyOrderClient)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GetoneOrders;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyOrderClient;
                Parameter.Value = KeyOrderClient;
                Parameters.Add(Parameter);

                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch { DataTable = null; }

                return DataTable;
            }
        }

        /// <summary>
        /// Lay tat ca mat hang mot don hang
        /// </summary>
        /// Tra ve: Table chua don hang 
        public static System.Data.DataTable m2018_GetsallGoods(Guid KeyOrderClient)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GetsallGoods;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyOrderClient;
                Parameter.Value = KeyOrderClient;
                Parameters.Add(Parameter);

                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch { DataTable = null; }

                return DataTable;
            }
        }
    }
}
