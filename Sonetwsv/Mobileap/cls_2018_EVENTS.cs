using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Sonetwsv.common;

namespace Sonetwsv.Mobileap
{
    public class cls_2018_EVENTS
    {
        //Attribute
        private const string PKeyNewsEvent = "@KEY_NEWS_EVENT";
        //Procedure
        private const string SpMobile_2018_GetoneEvents = "VVV_2018_GETONENEWSEVENTS";
        private const string SpMobile_2018_GettopEvents = "VVV_2018_GETTOPNEWSEVENTS";
        private const string SpMobile_2018_OthersEvents = "VVV_2018_OTHERSNEWSEVENTS";
        //Methodology

        /// <summary>
        /// Lay danh sach tin top khac tin hien tai
        /// </summary>
        /// Tra ve: Datatable event duoc chon 
        public static System.Data.DataTable m2018_GetoneEvents(Guid KeyNewsEvent)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GetoneEvents;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyNewsEvent;
                Parameter.Value = KeyNewsEvent;
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
        /// Lay danh sach tin top khac tin hien tai
        /// </summary>
        /// Tra ve: Datatable news
        public static System.Data.DataTable m2018_OthersEvents(Guid KeyNewsEvent)
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_OthersEvents;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;

                System.Data.Common.DbParameterCollection Parameters = DbCommand.Parameters;
                System.Data.Common.DbParameter Parameter;

                Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = PKeyNewsEvent;
                Parameter.Value = KeyNewsEvent;
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
        /// Lay top 10 news & events
        /// </summary>
        /// Tra ve: datatable
        public static System.Data.DataTable m2018_GettopEvents()
        {
            using (DbCommand DbCommand = clsConnect.DbConnection.CreateCommand())
            {
                DbCommand.CommandText = SpMobile_2018_GettopEvents;
                DbCommand.CommandType = System.Data.CommandType.StoredProcedure;
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
