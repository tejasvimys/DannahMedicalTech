using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioADB_DAL
{
   public class Login
    {
       public string CheckFirstLogin(string doctorId)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("CHECKISFIRSTLOGIN", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@doctorCode", doctorId);

               var reader = sqlCommand.ExecuteReader();

               while (reader.Read())
               {
                   var readerval = reader[0].ToString();

                   retVal = String.Equals(readerval, "FIRSTLOGIN") ? "FIRSTLOGIN" : "NEXTLOGIN";
               }
           }
           catch (Exception ex)
           {

               retVal = "ERROR";
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return retVal;
       }

       public string ResetPassword(string password, string pin, string doctorcode, string doctorName)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("ResetPassword", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@password", password);
               sqlCommand.Parameters.AddWithValue("@pin", Convert.ToInt32(pin));
               sqlCommand.Parameters.AddWithValue("@doctorcode", doctorcode);
               sqlCommand.Parameters.AddWithValue("@doctorName", doctorName);

               var reader = sqlCommand.ExecuteReader();

              while (reader.Read())
               {
                   var readerval = reader[0].ToString();

                   retVal = String.Equals(readerval, "SUCCESS") ? "SUCCESS" : "FAILURE";
               }
           }
           catch (Exception ex)
           {

               retVal = "ERROR";
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return retVal;
       }

       public string CheckLogin(string doctorId, string password, string pin, string doctorcode)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("LoginDoctor", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               //sqlCommand.Parameters.AddWithValue("@doctorid", doctorId);
               sqlCommand.Parameters.AddWithValue("@password", password);
               sqlCommand.Parameters.AddWithValue("@pin", pin);
               sqlCommand.Parameters.AddWithValue("@loginid", doctorcode);

               var reader = sqlCommand.ExecuteReader();

               while (reader.Read())
               {
                   var readerval = reader[0].ToString();
                   if (retVal.Equals("INVALIDUSER"))
                   {
                       return "INVALIDUSER";
                   }
                   else
                   {
                       return readerval;
                   }
                   
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

       public string GetPatientName(string patientid)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GetPatientNameforDisplay", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@patientid", patientid);

               var reader = sqlCommand.ExecuteReader();

               while (reader.Read())
               {
                   var readerval = reader[0].ToString() + " " + reader[1].ToString();

                   retVal= readerval;
               }
           }
           catch (Exception ex)
           {

               retVal = "ERROR";
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
