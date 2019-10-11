using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using Microsoft.Win32;

namespace DomainLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLog(String.Join(" ", args));
        }

        static string CutString(string s, int l)
        {
            return s.Length <= l ? s : s.Substring(0, l);
        }

        private static void WriteLog(string args)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@Username", Environment.UserName, DbType.String);
                dal.AddParameter("@Name", Environment.MachineName, DbType.String);
                dal.AddParameter("@Model", GetWMIValue("Win32_ComputerSystem", "Model"), DbType.String);
                dal.AddParameter("@OS", GetWMIValue("Win32_OperatingSystem", "Caption"), DbType.String);
                dal.AddParameter("@SerialNumber", GetWMIValue("Win32_BIOS", "SerialNumber"), DbType.String);
                dal.AddParameter("@TeamViewerId", GetTeamViewerId(), DbType.String);
                dal.AddParameter("@Args", args, DbType.String);
                dal.ExecuteStoredProcedure("WriteLog");
                dal.ClearParameters();
            }
            catch (Exception) { }
        }

        private static string GetTeamViewerId()
        {
            object key = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\TeamViewer\", "ClientID", "");
            if (key == null)
                return "TeamViewer ikke installeret";
            else if (key.ToString() == "")
                return "Key 'ClientID' findes ikke";
            else
                return key.ToString();
        }

        private static string GetWMIValue(string wminame, string property)
        {
            var name = (from x in new ManagementObjectSearcher($"SELECT {property} FROM {wminame}").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue(property)).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }
    }

    public class DataAccessLayer
    {
        private SqlConnection Conn;
        private List<SqlParameter> Parameters;

        public void AddParameter(string name, object value, DbType type)
        {
            SqlParameter p = new SqlParameter(name, type);
            p.Value = value;
            Parameters.Add(p);
        }
        public void ClearParameters()
        {
            Parameters.Clear();
        }
        public DataAccessLayer()
        {
            string connectionstring = @"Data Source=TRR-SRV1\SQLEXPRESS;Initial Catalog=DLDB;User Id=XXX;Password=XXXX;";
            Conn = new SqlConnection(connectionstring);
            Parameters = new List<SqlParameter>();
        }
        public int ExecuteStoredProcedure(string SQL)
        {
            using (SqlCommand Comm = Conn.CreateCommand())
            {
                Comm.CommandText = SQL;
                Comm.CommandType = CommandType.StoredProcedure;

                if (Parameters.Count > 0)
                    Comm.Parameters.AddRange(Parameters.ToArray());

                Conn.Open();
                int i = Comm.ExecuteNonQuery();
                Conn.Close();

                return i;
            }
        }
    }
}