using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobileap
{
    public class cls_2018_CLIENT
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
        
        //Procedure
        private const string SpMobile_2018_CheckClient = "VVV_2018_CHECKEDCLIENTMOB";
        private const string SpMobile_2018_GetsClients = "VVV_2018_CARDLAYMOTCLIENT";
        private const string SpMobile_2018_EditClients = "VVV_2018_CARDCLIENTCHANGE";
        //Methodology
        
        /// <summary>
        /// Lấy một khach hàng để xem thong tin
        /// </summary>
        /// Tra ve: Table chua dong record khach hang
        public static System.Data.DataTable m2018_GetoneClient(string CodCardClient)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GetsClients;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PCodCardClient;
                Parameter.Value = CodCardClient;
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
        /// Kiem tra xem so dien thoai khach hang da co tren he thong chua
        /// </summary>
        /// Tra ve: True - Da ton tại; False - Chua ton tai
        public static bool m2018_CheckClient(string CodCardClient)
        {
            using (System.Data.Common.DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_CheckClient;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;

                System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PCodCardClient;
                DbParameter.Value = CodCardClient;
                DbParameters.Add(DbParameter);

                bool HasRows = false;
                try
                {
                    using (DbDataReader DbDataReader = DbCommand.ExecuteReader())
                    { HasRows = DbDataReader.HasRows; }
                }
                catch{ }
                return HasRows;
            }
        }

        /// <summary>
        /// Cap nhat thong tin khach hang tu Mobile
        /// </summary>
        /// Tra ve: 0 - Thanh cong ; #0 - That bai        
        public static int m2018_UpdateClient(Guid KeyCardClient, string CodCardClient,
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
                    ApproveCommand.CommandText = SpMobile_2018_EditClients;

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
    }
}
