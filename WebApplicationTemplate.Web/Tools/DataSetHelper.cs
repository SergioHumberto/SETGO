using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using WebApplicationTemplate.DAL;

namespace WebApplicationTemplate.Web.Tools
{
    public class DataSetHelper
    {
        public static DataTable ExecuteStoredProcedure(string nameProcedure, SqlParameter [] arrParams)
        {
            DataTable dt = new DataTable();

            SqlConnection sqlConnection = new SqlConnection(DAL.DAL.ConnectionString);
            using (SqlCommand sqlCommand = new SqlCommand(nameProcedure, sqlConnection))
            {
                sqlConnection.Open();

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = sqlConnection.ConnectionTimeout;

                if (arrParams != null)
                {
                    sqlCommand.Parameters.AddRange(arrParams);
                }

                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlDataAdapter.Fill(dt);
                }

                sqlConnection.Close();
            }
            
            return dt;
        }
    }
}