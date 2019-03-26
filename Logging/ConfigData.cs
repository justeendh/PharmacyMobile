using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CommonLibs
{
    [Serializable]
    public enum CONFIGKEY
    {
        DB_CONNECTION_STRING = 0,
        LAST_USER_LOGIN,
        EMPLOYEE_COL_WIDTH,
        DEFAULT_DEVICE_ID,
        ID_DON_VI, TEN_DON_VI,
        MIN_TIME_BETWEEN_2LOG, STA_NUMBER_OF_DAY, STA_CONG_QD,
        ROUND_TIME, DATE_START_ATT,
        RD_SYNC_SCH_VAL, TIME_SYNC_SCH_VAL_1, TIME_SYNC_SCH_VAL_2,
        DELETE_DATA_AFTER_DOWNLOAD,
        RD_UP_DATA_SV_SCH_VAL, TIME_UP_DATA_SV_SCH_VAL_1, TIME_UP_DATA_SV_SCH_VAL_2,

        HE_SO_LUONG_COL_WIDTH, MUC_LUONG_CV_COL_WIDTH, BANG_TINH_LUONG_COL_WIDTH, BANG_LUONG_COL_WIDTH, BANG_GIAM_TRU_LUONG,

        HOST_FTP, USERNAME_FTP, PASSWORD_FTP,
        LAM_TRON_CONG_05, LAM_TRON_GIO_LAM_05,

        BANG_CHAM_CONG_COL_WIDTH
    }

    [Serializable]
    public class ConfigData
    {
        private Hashtable lstConfig;
        public const string CONFIG_FILENAME = "\\wsdtcf.bin";
        public const string PASS_ENCRYPT = "wspassattendance88";

        public ConfigData()
        {
            lstConfig = new Hashtable();
            LoadConfig();
        }

        public void setConfigValue(CONFIGKEY KeySet, object ValueSet)
        {
            if (ValueSet != null && ValueSet.GetType() == typeof(string))
            {
                if (lstConfig.ContainsKey(KeySet))
                    lstConfig[KeySet] = SecuritiesLib.EncryptString((string)ValueSet, PASS_ENCRYPT);
                else lstConfig.Add(KeySet, SecuritiesLib.EncryptString((string)ValueSet, PASS_ENCRYPT));
            }
            else
            {
                if (lstConfig.ContainsKey(KeySet))
                    lstConfig[KeySet] = ValueSet;
                else lstConfig.Add(KeySet, ValueSet);
            }
        }

        public object getConfigValue(CONFIGKEY KeyGet)
        {
            if (lstConfig.Contains(KeyGet))
            {
                object val = lstConfig[KeyGet];
                if (val == null) return null;
                if (val.GetType() == typeof(string)) return SecuritiesLib.DecryptString((string)val, PASS_ENCRYPT);
                else return lstConfig[KeyGet];
            }
            else return null;
        }

        public void SaveConfig()
        {
            Stream s = File.Open(Environment.CurrentDirectory + CONFIG_FILENAME, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binary = new BinaryFormatter();
            binary.Serialize(s, lstConfig);
            s.Close();
        }

        public void LoadConfig()
        {
            if (!File.Exists(Environment.CurrentDirectory + CONFIG_FILENAME))
            {
                lstConfig[CONFIGKEY.DB_CONNECTION_STRING] = SecuritiesLib.EncryptString(DefaultConnectionString(), PASS_ENCRYPT);
                foreach (CONFIGKEY val in Enum.GetValues(typeof(CONFIGKEY)))
                {
                    if (val != CONFIGKEY.DB_CONNECTION_STRING) lstConfig[val] = null;
                }

                SaveConfig();
                return;
            }

            Stream s = File.Open(Environment.CurrentDirectory + CONFIG_FILENAME, FileMode.Open, FileAccess.Read);
            BinaryFormatter binary = new BinaryFormatter();
            s.Position = 0;
            lstConfig = (Hashtable)binary.Deserialize(s);
            s.Close();
        }

        private static string DefaultConnectionString()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            // Set the properties for the data source.
            sqlBuilder.DataSource = "localhost";
            sqlBuilder.InitialCatalog = "wsattendance";
            sqlBuilder.UserID = "sa";
            sqlBuilder.Password = "";
            return sqlBuilder.ToString();
        }

        public static string GetConnectionString(string server, string DB, string user, string password)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            // Set the properties for the data source.
            sqlBuilder.DataSource = server;
            sqlBuilder.InitialCatalog = DB;
            sqlBuilder.UserID = user;
            sqlBuilder.Password = password;
            return sqlBuilder.ToString();
        }

        public static DBConnectParam GetConnectionInfo(string constr)
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(constr);
            // Set the properties for the data source.
            DBConnectParam param = new DBConnectParam();
            param.SERVER = sqlBuilder.DataSource;
            param.DATABASE = sqlBuilder.InitialCatalog;
            param.USER = sqlBuilder.UserID;
            param.PASSWORD = sqlBuilder.Password;
            return param;
        }
    }


    public class DBConnectParam
    {
        public string SERVER { set; get; }
        public string DATABASE { set; get; }
        public string USER { set; get; }
        public string PASSWORD { set; get; }
    }
}
