using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication6.Helpers
{
    public class Database
    {
        private string _connectionString;

        public Database()
        {
            _connectionString = "Server=Home-PC\\WARZ;Database=aspnet;User Id=sa;Password=adnan123123;";
        }

        public DataTable Query(string sql, List<SqlParameter> parameters = null)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        resultTable.Load(dr);
                    }
                }
            }

            return resultTable;
        }

        public int Execute(string sql, List<SqlParameter> parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}