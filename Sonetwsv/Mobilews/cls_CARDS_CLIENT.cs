using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobilews
{
    public class cls_CARDS_CLIENT
    {
        //Attribute
        public const string PKeyCardClient = "@KEY_CARD_CLIENT";
        public const string PCodCardClient = "@COD_CARD_CLIENT";
        public const string PTenCardClient = "@TEN_CARD_CLIENT";
        public const string PSexCardClient = "@SEX_CARD_CLIENT";
        public const string PBirCardClient = "@BIR_CARD_CLIENT";
        public const string PTelCardClient = "@TEL_CARD_CLIENT";
        public const string PEmlCardClient = "@EML_CARD_CLIENT";
        public const string PAddCardClient = "@ADD_CARD_CLIENT";
        public const string POrgCardClient = "@ORG_CARD_CLIENT";

        public const string PBanCardClient = "@BAN_CARD_CLIENT";
        public const string PDoiCardClient = "@DOI_CARD_CLIENT";
        public const string PConCardClient = "@CON_CARD_CLIENT";

        public const string PDateDongBo = "@DATE_DONG_BO";
        public const string PVersDongBo = "@VERS_DONG_BO";
        public const string PFlagDongBo = "@FLAG_DONG_BO";
        
        /// <summary>
        /// Tim kiem khach hang
        /// </summary>
        private const string SpMobile_Client_FiltClient = "VVV_MOBILE_SEARCHCARDCLIENT";
        public static System.Data.DataTable mClient_FilterClient(string TenCardClient)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Client_FiltClient;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PCodCardClient;
                Parameter.Value = TenCardClient;
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
        /// Cap nhat them khach hang moi tu Mobile
        /// </summary>
        /// <returns></returns>
        private const string SpMobile_Client_InsertNew = "CARD_CLIENT_INSERT";
        public static int mClient_InsertNew(Guid KeyCardClient, string CodCardClient,
            string TenCardClient, string SexCardClient, DateTime? BirCardClient,
            string TelCardClient, string EmlCardClient, string AddCardClient,
            string OrgCardClient, int BanCardClient, int DoiCardClient, int ConCardClient,
            DateTime NgayDongBo, int VersDongBo, bool FlagDongBo)
        {
            int Result = 0;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand ApproveCommand = clsConnect.DbConnection.CreateCommand())
                {
                    ApproveCommand.Transaction = DbTransaction;
                    ApproveCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ApproveCommand.CommandText = SpMobile_Client_InsertNew;

                    DbParameter DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyCardClient;
                    DbParameter.Value = KeyCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PCodCardClient;
                    DbParameter.Value = CodCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTenCardClient;
                    DbParameter.Value = TenCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PSexCardClient;
                    DbParameter.Value = SexCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PBirCardClient;
                    if (BirCardClient.HasValue)
                        DbParameter.Value = BirCardClient;
                    else
                        DbParameter.Value = DBNull.Value;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTelCardClient;
                    if (string.IsNullOrEmpty(TelCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = TelCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PEmlCardClient;
                    if (string.IsNullOrEmpty(EmlCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = EmlCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PAddCardClient;
                    if (string.IsNullOrEmpty(AddCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = AddCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = POrgCardClient;
                    if (string.IsNullOrEmpty(OrgCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = OrgCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PBanCardClient;
                    DbParameter.Value = BanCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDoiCardClient;
                    DbParameter.Value = DoiCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PConCardClient;
                    DbParameter.Value = ConCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDateDongBo;
                    DbParameter.Value = DateTime.Today;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PVersDongBo;
                    DbParameter.Value = VersDongBo;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PFlagDongBo;
                    DbParameter.Value = true;
                    ApproveCommand.Parameters.Add(DbParameter);

                    try
                    {
                        ApproveCommand.ExecuteNonQuery();
                        DbTransaction.Commit();
                    }
                    catch (DbException DbException)
                    {
                        DbTransaction.Rollback();
                        //Result = DbException.Message + DbException.StackTrace;
                        Result = clsConnect.GetDbException(DbException);
                    }
                }
            }

            return Result;
        }
        
        /// <summary>
        /// Cap nhat thong tin khach hang tu Mobile
        /// </summary>
        /// <returns></returns>
        private const string SpMobile_Client_UpdateOne = "CARD_CLIENT_CHANGE";
        public static int mClient_UpdateOne(Guid KeyCardClient, string CodCardClient,
            string TenCardClient, string SexCardClient, DateTime? BirCardClient,
            string TelCardClient, string EmlCardClient, string AddCardClient, string OrgCardClient, 
            DateTime NgayDongBo, int VersDongBo, bool FlagDongBo)
        {
            int Result = 0;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand ApproveCommand = clsConnect.DbConnection.CreateCommand())
                {
                    ApproveCommand.Transaction = DbTransaction;
                    ApproveCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ApproveCommand.CommandText = SpMobile_Client_UpdateOne;

                    DbParameter DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyCardClient;
                    DbParameter.Value = KeyCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PCodCardClient;
                    DbParameter.Value = CodCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTenCardClient;
                    DbParameter.Value = TenCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PSexCardClient;
                    DbParameter.Value = SexCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PBirCardClient;
                    if (BirCardClient.HasValue)
                        DbParameter.Value = BirCardClient;
                    else
                        DbParameter.Value = DBNull.Value;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTelCardClient;
                    if (string.IsNullOrEmpty(TelCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = TelCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PEmlCardClient;
                    if (string.IsNullOrEmpty(EmlCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = EmlCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PAddCardClient;
                    if (string.IsNullOrEmpty(AddCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = AddCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = POrgCardClient;
                    if (string.IsNullOrEmpty(OrgCardClient))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = OrgCardClient;
                    ApproveCommand.Parameters.Add(DbParameter);
                    
                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDateDongBo;
                    DbParameter.Value = DateTime.Today;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PVersDongBo;
                    DbParameter.Value = VersDongBo;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PFlagDongBo;
                    DbParameter.Value = true;
                    ApproveCommand.Parameters.Add(DbParameter);

                    try
                    {
                        ApproveCommand.ExecuteNonQuery();
                        DbTransaction.Commit();
                    }
                    catch (DbException DbException)
                    {
                        DbTransaction.Rollback();
                        //Result = DbException.Message + DbException.StackTrace;
                        Result = clsConnect.GetDbException(DbException);
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// Lấy một khach hàng để xem thong tin
        /// </summary>
        private const string SpMobile_Client_GetsClient = "VVV_MOBILE_CARDLAYMOTCLIENT";
        public static System.Data.DataTable mClient_GetoneClient(Guid KeyCardClient)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Client_GetsClient;
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
                catch { DataTable = null; }

                return DataTable;
            }
        }
    }
}
