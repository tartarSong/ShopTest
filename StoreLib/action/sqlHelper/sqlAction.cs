using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StoreLib.models;

namespace StoreLib.action.sqlHelper
{
    public static class sqlAction
    {
        public static List<Product> GetProduct(string connectionString)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            var result = new List<Product>();

            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("GetAllProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var temp = new Product()
                    {
                        ProductId = (int)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = double.Parse(reader["ProductPrice"].ToString()),
                    };
                    result.Add(temp);
                }
            }
            catch (Exception ex) 
            {
                LogAction.WriteLog(connectionString, ex.Message);
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

        public static List<ProductDiscount> AddProductCart(string connectionString, int productId)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            var result = new List<ProductDiscount>();

            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("GetProductDiscount", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", productId.ToString());
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var temp = new ProductDiscount()
                    {
                        DiscountType = (int)reader["DiscountTypeId"],
                        OnProductId = (int)reader["OnProductId"],
                        ConditionProductId = (int)reader["ConditionProductId"],
                        ConditionQuantity = (int)reader["ConditionQuantity"],
                        DiscountNumber = (double)reader["DiscountNumber"]
                    };
                    result.Add(temp);
                }
            }
            catch (Exception ex)
            {
                LogAction.WriteLog(connectionString, ex.Message);
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

        public static void InsertData(string connectionString, string logData)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;

            try
            {
                conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("InsertData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Logdata", logData);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogAction.WriteLog(connectionString, ex.Message);
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
        }
    }
}
