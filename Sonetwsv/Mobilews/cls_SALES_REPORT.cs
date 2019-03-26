using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobilews
{
    public class cls_SALES_REPORT
    {
        //Attribute
        private const string PKeyChiNhanh = "@KEY_CHI_NHANH";
        private const string PKeyMatHang = "@KEY_MAT_HANG";
        private const string PNgayBatDau = "@NGAY_BAT_DAU";
        private const string PNgayKetThuc = "@NGAY_KET_THUC";

        /// <summary>
        /// Bao cao tong hop doanh so ban va loi nhuan theo cua hang
        /// </summary>
        private const string SpMobile_Sales_SumStores = "VVV_MOBILE_SALESUMSTORES";
        public static DataTable mSales_SumStores(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Sales_SumStores;
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
        /// Bao cao tong hop doanh so ban va loi nhuan theo một cửa hàng
        /// </summary>
        private const string SpMobile_Sales_OneStores = "VVV_MOBILE_SALEONESTORES";
        public static DataTable mSales_OneStores(Guid KeyChiNhanh, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Sales_OneStores;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;
                System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PKeyChiNhanh;
                DbParameter.Value = KeyChiNhanh;
                DbParameters.Add(DbParameter);

                DbParameter = DbCommand.CreateParameter();
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
        /// Bao cao so lieu bieu do tat cua mot cua hang
        /// </summary>        
        private const string SpMobile_Sales_OneCharts = "VVV_MOBILE_ONESTORECHART";
        public static DataTable mSales_OneStoreChart(Guid KeyChiNhanh, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Sales_OneCharts;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;
                System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PKeyChiNhanh;
                DbParameter.Value = KeyChiNhanh;
                DbParameters.Add(DbParameter);

                DbParameter = DbCommand.CreateParameter();
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
        /// Bao cao so lieu bieu do tat ca cac cua hang
        /// </summary>        
        private const string SpMobile_Sales_AllCharts = "VVV_MOBILE_ALLSTORECHART";
        public static DataTable mSales_StoreChart(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Sales_AllCharts;
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
        /// Bao cao hang ban chay tat ca cac cua hang
        /// </summary>
        private const string SpMobile_Sales_BestSaler = "VVV_MOBILE_BESTSALESTORE";
        public static DataTable mSales_BestSales(DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Sales_BestSaler;
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
        /// Bao cao ban chay cua mot mat hang
        /// </summary>
        private const string SpMobile_Sales_BestGoods = "VVV_MOBILE_BESTSALEGOODS";
        public static DataTable mSales_BestGoods(Guid KeyMatHang, DateTime NgayBatDau, DateTime NgayKetThuc)
        {
            using (DbCommand DbCommand = clsConnect.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_Sales_BestGoods;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection DbParameters = DbCommand.Parameters;
                System.Data.Common.DbParameter DbParameter = DbCommand.CreateParameter();
                DbParameter.ParameterName = PKeyMatHang;
                DbParameter.Value = KeyMatHang;
                DbParameters.Add(DbParameter);

                DbParameter = DbCommand.CreateParameter();
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

        
    }
}
