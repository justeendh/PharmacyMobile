using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobilews
{
    public class cls_CASHS_DELIVE
    {
        //Attribute
        private const string PKeyChiNhanh = "@KEY_CHI_NHANH";
        private const string PKeyTienGiao = "@KEY_TIEN_GIAO";
        private const string PBanTienGiao = "@BAN_TIEN_GIAO";
        private const string POkeTienGiao = "@OKE_TIEN_GIAO";
        private const string PNgayBatDau = "@NGAY_BAT_DAU";
        private const string PNgayKetThuc = "@NGAY_KET_THUC";

        private const string PKeyThuNgan = "@KEY_THU_NGAN";
        private const string PKeyNguoiDung = "@KEY_NGUOI_DUNG";
        
        /// <summary>
        /// Tinh tien doanh so ban cua ca
        /// </summary>
        private const string SpMobile_Cashs_SumSales = "TIEN_GIAO_SUMBAN";
        public static decimal mCashs_SumSales(Guid KeyChiNhanh, Guid KeyThuNgan, Guid KeyNguoiDung,
            DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Cashs_SumSales;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyChiNhanh;
                Parameter.Value = KeyChiNhanh;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyThuNgan;
                Parameter.Value = KeyThuNgan;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyNguoiDung;
                Parameter.Value = KeyNguoiDung;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PNgayBatDau;
                Parameter.Value = NgayBatDau;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PNgayKetThuc;
                Parameter.Value = NgayKetThuc;
                Parameters.Add(Parameter);

                decimal nSumBan = 0;
                try
                {
                    using (DbDataReader DbDataReader = DbCommand.ExecuteReader())
                    {
                        if (DbDataReader.Read())
                        {
                            nSumBan = DbDataReader.GetDecimal(DbDataReader.GetOrdinal(PBanTienGiao));
                            DbDataReader.Close();
                        }
                    }
                }
                catch {nSumBan = 0;}

                return nSumBan;
            }
        }
        /// <summary>
        /// Xac nhan da kiem tra tien tu Mobile
        /// </summary>
        /// <param name="KeyMatHang">Kieu du lieu Guid</param>
        /// <param name="GiaBanLeMoi">(Kiem tra gia ban phia > 0)</param>
        /// <returns></returns>
        private const string SpMobile_Cashs_Confirms = "VVV_MOBILE_UPDATECABANGIAO";
        public static int mCashs_Confirms(Guid KeyTienGiao, decimal BanTienGiao, bool OkeTienGiao)
        {
            int Result = 0;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand ApproveCommand = clsConnect.DbConnection.CreateCommand())
                {
                    ApproveCommand.Transaction = DbTransaction;
                    ApproveCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ApproveCommand.CommandText = SpMobile_Cashs_Confirms;

                    DbParameter DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyTienGiao;
                    DbParameter.Value = KeyTienGiao;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PBanTienGiao;
                    DbParameter.Value = BanTienGiao;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = POkeTienGiao;
                    DbParameter.Value = OkeTienGiao;
                    ApproveCommand.Parameters.Add(DbParameter);

                    try
                    {
                        ApproveCommand.ExecuteNonQuery();
                        DbTransaction.Commit();
                    }
                    catch (DbException DbException)
                    {
                        DbTransaction.Rollback();
                        Result = clsConnect.GetDbException(DbException);
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// Chi tiet 1 ca ban giao 
        /// </summary>
        private const string SpMobile_Cashs_OneShift = "VVV_MOBILE_LAYMOTCABANGIAO";
        public static System.Data.DataTable mCashs_OneShift(Guid KeyTienGiao)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Cashs_OneShift;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyTienGiao;
                Parameter.Value = KeyTienGiao;
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
        /// Chi nhanh ban giao theo chi nhanh
        /// </summary>
        private const string SpMobile_Cashs_OneBrand = "VVV_MOBILE_CHINHANHBANGIAO";
        public static System.Data.DataTable mCashs_OneBrand(Guid KeyChiNhanh, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Cashs_OneBrand;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyChiNhanh;
                Parameter.Value = KeyChiNhanh;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PNgayBatDau;
                Parameter.Value = NgayBatDau;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PNgayKetThuc;
                Parameter.Value = NgayKetThuc;
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
        /// Tong hop ban giao theo chi nhanh
        /// </summary>
        private const string SpMobile_Cashs_SumBrand = "VVV_MOBILE_TONGHOPBANGIAO";
        public static System.Data.DataTable mCashs_SumBrand(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Cashs_SumBrand;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PNgayBatDau;
                Parameter.Value = NgayBatDau;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PNgayKetThuc;
                Parameter.Value = NgayKetThuc;
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
