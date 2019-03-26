using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobileap
{
    public class cls_2018_GOODS
    {
        //Attribute
        private const string PKeyMatHang = "@KEY_MAT_HANG";
        private const string PTenMatHang = "@TEN_MAT_HANG";
        //Procedure
        private const string SpMobile_2018_FilterGoods = "VVV_2018_SEARCHMATHANG";
        private const string SpMobile_2018_GetoneGoods = "VVV_2018_HANGLAYMOTMAT";
        
        //Methodology
        
        /// <summary>
        /// Lấy một mặt hàng để chỉnh giá
        /// </summary>
        /// Tra ve datatable chua mat hang
        public static System.Data.DataTable m2018_GetoneGoods(Guid KeyMatHang)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GetoneGoods;
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
        /// Tim kiem ten mat hang
        /// </summary>        
        /// Tra ve: DataTable
        public static System.Data.DataTable m2018_FilterGoods(string TenMatHang)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_FilterGoods;
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
    }
}
