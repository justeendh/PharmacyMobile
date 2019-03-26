using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Sonetwsv
{
    public class BaseModel
    {
        public static object GetValue(DataRow row, string column)
        {
            if (row != null && row.Table.Columns.Contains(column))
            {
                if (row[column].GetType() == typeof(DBNull)) return null;
                else return row[column];
            }
            return null;
        }

        public static List<T> ParseListFromDataTable<T>(DataTable dt)
        {
            var typeT = typeof(T);
            List<T> result = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow rowData in dt.Rows)
                {
                    T itemData = (T)Activator.CreateInstance(typeT);
                    foreach (var property in typeT.GetProperties())
                    {
                        object valueSet = GetValue(rowData, property.Name);
                        if (valueSet != null) property.SetValue(itemData, valueSet);
                    }
                    result.Add(itemData);
                }
                return result;
            }
            return null;
        }

        public static T ParseFromDataTable<T>(DataTable dt)
        {
            List<T> lstData = ParseListFromDataTable<T>(dt);
            if (lstData != null && lstData.Count >= 1) return lstData[0];
            else return default(T);
        }

        public static void CreateParameterCollection<T>(DbCommand DbCommand, T item)
        {
            DbParameterCollection Parameters = DbCommand.Parameters;

            var typeT = typeof(T);
            foreach (var property in typeT.GetProperties())
            {
                DbParameter Parameter = DbCommand.CreateParameter();
                Parameter.ParameterName = string.Format("@{0}", property.Name);
                Parameter.Value = property.GetValue(item);
                Parameters.Add(Parameter);
            }

            return;
        }
    }
}