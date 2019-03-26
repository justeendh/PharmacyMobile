using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobilews
{
    public class cls_USERS_MOBILE
    {
        //Attribute        
        private const string PMobNguoiDung = "@MOB_NGUOI_DUNG";
        private const string PPasNguoiDung = "@PAS_NGUOI_DUNG";
        private const string PTenNguoiDung = "@TEN_NGUOI_DUNG";
        /// <summary>
        /// Tim kiem ten mat hang
        /// </summary>
        private const string SpMobile_Users_Logins = "VVV_MOBILE_CHECKUSERLOGIN";

        public static bool Login(string UserName, string Password, out string TenNguoiDung)
        {
            bool HasRows = false; TenNguoiDung = string.Empty;
            if (clsConnect.DB_OpenConnection("", "", "", ""))
            {
                using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
                {
                    DbCommand.CommandText = SpMobile_Users_Logins;
                    DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;

                    System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                    DbParameter.ParameterName = PMobNguoiDung;
                    DbParameter.Value = UserName;
                    DbParameters.Add(DbParameter);

                    DbParameter = DbCommand.CreateParameter();
                    DbParameter.ParameterName = PPasNguoiDung;
                    string EnPass = clsConnect.Encrypt(Password);
                    DbParameter.Value = EnPass;
                    DbParameters.Add(DbParameter);
                    
                    try
                    {
                        using (DbDataReader DbDataReader = DbCommand.ExecuteReader())
                        {
                            HasRows = DbDataReader.HasRows;
                            if (DbDataReader.Read())
                                TenNguoiDung = DbDataReader.GetString(DbDataReader.GetOrdinal(PTenNguoiDung));
                            else
                                TenNguoiDung = "Chưa xác định";
                        }
                    }
                    catch { }                    
                }
            }

            return HasRows;
        }
    }
}
