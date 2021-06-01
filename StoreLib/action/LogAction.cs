using StoreLib.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StoreLib.action
{
    public static class LogAction
    {
        public static int WriteLog(string connectionString, string log)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            int result = 0;

            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("WriteLog", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Message", log);
                conn.Open();
                cmd.ExecuteNonQuery();            
            }
            catch (Exception ex)
            {
                return 1;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return result;
        }

    }
}
