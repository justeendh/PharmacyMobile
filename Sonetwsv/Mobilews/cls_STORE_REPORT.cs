using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobilews
{
    public class cls_STORE_REPORT
    {
        //Attribute
        private const string PKeyChiNhanh = "@KEY_CHI_NHANH";
        private const string PKeyMatHang = "@KEY_MAT_HANG";
        private const string PNgayBatDau = "@NGAY_BAT_DAU";
        private const string PNgayKetThuc = "@NGAY_KET_THUC";
        
        private const string PKeyDieuHang = "@KEY_DIEU_HANG";
        private const string PNoiDieuHang = "@NOI_DIEU_HANG";

        /// <summary>
        /// Bao cao tong hop so luong hang can goi cua cac hang
        /// </summary>        
        private const string SpMobile_Store_SumStores = "VVV_MOBILE_STORESCALLSUM";
        public static DataTable mStore_SumStores(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Store_SumStores;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;
                System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PNgayBatDau;
                DbParameter.Value = NgayBatDau;
                DbParameters.Add(DbParameter);

                DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PNgayKetThuc;
                DbParameter.Value = NgayKetThuc;
                DbParameters.Add(DbParameter);

                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch (Exception ex) { DataTable = null; }

                return DataTable;
            }
        }

        /// <summary>
        /// Bao cao tong hop so luong hang can goi nhom theo cua hang
        /// </summary>        
        private const string SpMobile_Store_OneStores = "VVV_MOBILE_STORESCALLONE";
        public static DataTable mStore_OneStores(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Store_OneStores;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;
                System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PNgayBatDau;
                DbParameter.Value = NgayBatDau;
                DbParameters.Add(DbParameter);

                DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PNgayKetThuc;
                DbParameter.Value = NgayKetThuc;
                DbParameters.Add(DbParameter);

                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch (Exception ex) { DataTable = null; }

                return DataTable;
            }
        }

        /// <summary>
        /// Cap nhat xac nhan chon noi goi hang cua mat hang tu Mobile
        /// </summary>
        /// <param name="KeyMatHang">Kieu du lieu Guid</param>
        /// <param name="GiaBanLeMoi">(Kiem tra gia ban phia > 0)</param>
        /// <returns></returns>
        private const string SpMobile_Store_PlaceCall = "VVV_MOBILE_STOREPLACECAL";
        public static int mStore_PlaceCall(Guid KeyDieuHang, int NoiDieuHang)
        {
            int Result = 0;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand ApproveCommand = clsConnect.DbConnection.CreateCommand())
                {
                    ApproveCommand.Transaction = DbTransaction;
                    ApproveCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ApproveCommand.CommandText = SpMobile_Store_PlaceCall;

                    DbParameter DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyDieuHang;
                    DbParameter.Value = KeyDieuHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PNoiDieuHang;
                    DbParameter.Value = NoiDieuHang;
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
    }
}
