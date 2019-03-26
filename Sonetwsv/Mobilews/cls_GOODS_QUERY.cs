using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobilews
{
    public class cls_GOODS_QUERY
    {
        //Attribute        
        public const string PKeyLoaiHang = "@KEY_LOAI_HANG";
        public const string PKeyNhomHang = "@KEY_NHOM_HANG";

        public const string PKeyMatHang = "@KEY_MAT_HANG";
        public const string PMaMatHang = "@MA_MAT_HANG";
        public const string PTenMatHang = "@TEN_MAT_HANG";
        public const string PMaSanXuat = "@MA_SAN_XUAT";
        public const string PDonViTinh = "@DON_VI_TINH";
        public const string PDonViLon = "@DON_VI_LON";
        public const string PSoQuiDoi = "@SO_QUI_DOI";
        public const string PTileGiamGia = "@TILE_GIAM_GIA";
        public const string PTileTienLai = "@TILE_TIEN_LAI";
        public const string PHeSoThue = "@HE_SO_THUE";
        public const string PHangKeDon = "@HANG_KE_DON";
        public const string PHangDacBiet = "@HANG_DAC_BIET";
        public const string PHangKyGui = "@HANG_KY_GUI";
        public const string PDonGiaMua = "@DON_GIA_MUA";

        public const string PGiaBanLe = "@GIA_BAN_LE";
        public const string PGiaBanSi = "@GIA_BAN_SI";
        public const string PTileMoiGioi = "@TILE_MOI_GIOI";
        public const string PTileGhiDiem = "@TILE_GHI_DIEM";

        public const string PGhiGhiChu = "@GHI_GHI_CHU";
        public const string PGhiHamLuong = "@GHI_HAM_LUONG";
        public const string PGhiHoatChat = "@GHI_HOAT_CHAT";
        public const string PTonToiThieu = "@TON_TOI_THIEU";
        public const string PCoSuDung = "@CO_SU_DUNG";

        public const string PDateDongBo = "@DATE_DONG_BO";
        public const string PVersDongBo = "@VERS_DONG_BO";
        public const string PFlagDongBo = "@FLAG_DONG_BO";

        private const string PTileHangDuoi = "@TILE_HANG_DUOI";
        private const string PTileHangTren = "@TILE_HANG_TREN";
        
        /// <summary>
        /// Tim kiem ten mat hang
        /// </summary>
        private const string SpMobile_Goods_FiltGoods = "VVV_MOBILE_SEARCHMATHANG";
        public static System.Data.DataTable mGoods_FilterGoods(string TenMatHang)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_FiltGoods;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PTenMatHang;
                Parameter.Value = TenMatHang;
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
        /// Lay danh sach mat hang co gia ban nho hon gia mua
        /// </summary>        
        private const string SpMobile_Goods_FiltBelow = "VVV_MOBILE_SEARCHDUOILAI";
        public static System.Data.DataTable mGoods_FilterBelow()
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_FiltBelow;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch { DataTable = null; }

                return DataTable;
            }
        }

        /// <summary>
        /// Lay danh sach mat hang chua thiet lap gia ban
        /// </summary>        
        private const string SpMobile_Goods_FiltZeros = "VVV_MOBILE_SEARCHGIAZERO";
        public static System.Data.DataTable mGoods_FilterZeros()
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_FiltZeros;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch { DataTable = null; }

                return DataTable;
            }
        }

        /// <summary>
        /// Lay danh sach mat hang co gia ban hoa von
        /// </summary>        
        private const string SpMobile_Goods_FiltEqual = "VVV_MOBILE_SEARCHBANGGIA";
        public static System.Data.DataTable mGoods_FilterEqual()
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_FiltZeros;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");

                try { DbDataAdapter.Fill(DataTable); }
                catch { DataTable = null; }

                return DataTable;
            }
        }

        /// <summary>
        /// Tim kiem mat hang theo ti le he so lai
        /// </summary>
        private const string SpMobile_Goods_FiltLists = "VVV_MOBILE_SEARCHHESOLAI";
        public static System.Data.DataTable mGoods_FilterGoods(decimal TileHangDuoi, decimal TileHangTren)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_FiltLists;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PTileHangDuoi;
                Parameter.Value = TileHangDuoi;
                Parameters.Add(Parameter);

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PTileHangTren;
                Parameter.Value = TileHangTren;
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
        /// Lấy một mặt hàng để chỉnh giá
        /// </summary>
        private const string SpMobile_Goods_GetsGoods = "VVV_MOBILE_HANGLAYMOTMAT";
        public static System.Data.DataTable mGoods_GetoneGoods(Guid KeyMatHang)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_GetsGoods;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyMatHang;
                Parameter.Value = KeyMatHang;
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
        /// Lấy lịch sử chỉnh giá một mặt hàng
        /// </summary>
        private const string SpMobile_Goods_HistGoods = "VVV_MOBILE_HANGLAYLICHSU";
        public static System.Data.DataTable mGoods_HistoryGoods(Guid KeyMatHang)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_HistGoods;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyMatHang;
                Parameter.Value = KeyMatHang;
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
        /// Lấy lịch sử nhập mua gần nhất một mặt hàng 
        /// </summary>
        private const string SpMobile_Goods_PurcGoods = "VVV_MOBILE_HANGLAYMUAGAN";
        public static System.Data.DataTable mGoods_PurchaseGoods(Guid KeyMatHang)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Goods_PurcGoods;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyMatHang;
                Parameter.Value = KeyMatHang;
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
        /// Cap nhat lai gia ban le moi tu Mobile
        /// </summary>
        /// <param name="KeyMatHang">Kieu du lieu Guid</param>
        /// <param name="GiaBanLeMoi">(Kiem tra gia ban phia > 0)</param>
        /// <returns></returns>
        private const string SpMobile_Goods_UpdateGia = "VVV_MOBILE_HANGUPDATEGIA";
        public static int mGoods_UpdateGia(Guid KeyMatHang, Decimal GiaBanLeMoi)
        {
            int Result = 0;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand ApproveCommand = clsConnect.DbConnection.CreateCommand())
                {
                    ApproveCommand.Transaction = DbTransaction;
                    ApproveCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ApproveCommand.CommandText = SpMobile_Goods_UpdateGia;

                    DbParameter DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyMatHang;
                    DbParameter.Value = KeyMatHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PGiaBanLe;
                    DbParameter.Value = GiaBanLeMoi;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDateDongBo;
                    DbParameter.Value = DateTime.Today;
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
                        Result = clsConnect.GetDbException(DbException);
                    }
                }
            }
            
            return Result;
        }

        /// <summary>
        /// Them moi mat hang tu Mobile
        /// </summary>
        /// <param name="KeyMatHang">Kieu du lieu Guid</param>
        /// <param name="GiaBanLeMoi">(Kiem tra gia ban phia > 0)</param>
        /// <returns></returns>
        private const string SpMobile_Goods_InsertNew = "MAT_HANG_INSERT";
        public static int mGoods_InsertNew(Guid KeyLoaiHang, Guid KeyNhomHang,
            Guid KeyMatHang, string MaMatHang, string MaSanXuat,
            string TenMatHang, string DonViTinh, string DonViLon, decimal SoQuiDoi, decimal TileGiamGia,
            decimal HeSoThue, decimal TileTienLai, bool HangKeDon, bool HangDacBiet, bool HangKyGui,
            decimal DonGiaMua, decimal GiaBanLe, decimal GiaBanSi, string GhiGhiChu, string GhiHamLuong,
            string GhiHoatChat, decimal TonToiThieu, decimal TileMoiGioi, decimal TileGhiDiem, 
            bool CoSuDung, DateTime NgayDongBo, int VersDongBo, bool FlagDongBo)
        {
            int Result = 0;
            using (DbTransaction DbTransaction = clsConnect.DbConnection.BeginTransaction())
            {
                using (DbCommand ApproveCommand = clsConnect.DbConnection.CreateCommand())
                {
                    ApproveCommand.Transaction = DbTransaction;
                    ApproveCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    ApproveCommand.CommandText = SpMobile_Goods_InsertNew;

                    DbParameter DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyLoaiHang;
                    DbParameter.Value = KeyLoaiHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyNhomHang;
                    DbParameter.Value = KeyNhomHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PKeyMatHang;
                    DbParameter.Value = KeyMatHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PMaMatHang;
                    DbParameter.Value = MaMatHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTenMatHang;
                    DbParameter.Value = TenMatHang;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PMaSanXuat;
                    DbParameter.Value = MaSanXuat;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDonViTinh;
                    DbParameter.Value = DonViTinh;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDonViLon;
                    DbParameter.Value = DonViLon;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PSoQuiDoi;
                    DbParameter.Value = SoQuiDoi;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTileGiamGia;
                    DbParameter.Value = TileGiamGia;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PHeSoThue;
                    DbParameter.Value = HeSoThue;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTileTienLai;
                    DbParameter.Value = TileTienLai;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PHangKeDon;
                    DbParameter.Value = HangKeDon;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PHangDacBiet;
                    DbParameter.Value = HangDacBiet;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PHangKyGui;
                    DbParameter.Value = HangKyGui;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PDonGiaMua;
                    DbParameter.Value = DonGiaMua;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PGiaBanLe;
                    DbParameter.Value = GiaBanLe;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PGiaBanSi;
                    DbParameter.Value = GiaBanSi;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PGhiGhiChu;
                    if (string.IsNullOrEmpty(GhiGhiChu))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = GhiGhiChu;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PGhiHamLuong;
                    if (string.IsNullOrEmpty(GhiHamLuong))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = GhiHamLuong;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PGhiHoatChat;
                    if (string.IsNullOrEmpty(GhiHoatChat))
                        DbParameter.Value = "";
                    else
                        DbParameter.Value = GhiHoatChat;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTonToiThieu;
                    DbParameter.Value = TonToiThieu;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTileMoiGioi;
                    DbParameter.Value = TileMoiGioi;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PTileGhiDiem;
                    DbParameter.Value = TileGhiDiem;
                    ApproveCommand.Parameters.Add(DbParameter);

                    DbParameter = ApproveCommand.CreateParameter();
                    DbParameter.ParameterName = PCoSuDung;
                    DbParameter.Value = CoSuDung;
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

        private const string SpLoai_Lookup = "LOAI_HANG_LOOKUP";
        public static System.Data.DataTable LoaiHang_Lookup()
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpLoai_Lookup;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");
                try
                {
                    DbDataAdapter.Fill(DataTable);
                }
                catch
                {
                    DataTable = null;
                }
                return DataTable;
            }
        }

        private const string SpNhom_Lookup = "NHOM_HANG_LOOKUP";
        public static System.Data.DataTable NhomHang_Lookup()
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpNhom_Lookup;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
                DbDataAdapter DbDataAdapter = clsConnect.DbProviderFactory.CreateDataAdapter();
                DbDataAdapter.SelectCommand = DbCommand;
                System.Data.DataTable DataTable = new System.Data.DataTable("TABLE");
                try
                {
                    DbDataAdapter.Fill(DataTable);
                }
                catch
                {
                    DataTable = null;
                }
                return DataTable;
            }
        }
    }
}
