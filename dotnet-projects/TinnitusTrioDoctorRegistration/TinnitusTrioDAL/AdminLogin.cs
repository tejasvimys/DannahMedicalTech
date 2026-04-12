using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioDAL
{
    public class AdminLogin
    {
        public TinnitusTrioBO.Login CheckLogin(TinnitusTrioBO.Login objLogin)
        {
            var objLoginVal = new TinnitusTrioBO.Login();

            var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;
    
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("dbo.spAdminDetails", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@ADMINID", objLogin.UserId);
                sqlCommand.Parameters.AddWithValue("@PIN", objLogin.Pin);

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                   objLoginVal.Password = reader[0].ToString();

                    if (reader[0].ToString().Equals("FIRSTLOGIN")) continue;
                    objLoginVal.FirstName = reader[1].ToString();
                    objLoginVal.LastName = reader[2].ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return objLoginVal;
        }


        public string UpdatePassword(TinnitusTrioBO.Login objLogin)
        {
            var retVal = "";

            var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("spAdminUpdatePasswordDetails", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@ADMINID", objLogin.UserId);
                sqlCommand.Parameters.AddWithValue("@PIN", objLogin.Pin);
                sqlCommand.Parameters.AddWithValue("@Password", objLogin.Password);

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var readerval = reader[0].ToString();

                    return readerval;

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return retVal;
        }

    }
}
