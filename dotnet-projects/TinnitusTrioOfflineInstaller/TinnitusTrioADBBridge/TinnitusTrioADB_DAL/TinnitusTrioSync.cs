using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_DAL
{
   public class TinnitusTrioSync
    {
       public void SyncPatient()
       {
           var connectionString = ConfigurationManager.ConnectionStrings["ExternalConnectionString"].ConnectionString;

           GetPatientDetailsfromInternalDatabase();

       }

       public void GetPatientDetailsfromInternalDatabase()
       {
           try
           {
               var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
               var sqlConnection = new SqlConnection(connectionString);
               sqlConnection.Open();

               try
               {
                   var sqlCommand = new SqlCommand("SyncPatient", sqlConnection)
                   {
                       CommandType = CommandType.StoredProcedure
                   };
                   var reader = sqlCommand.ExecuteReader();


                   while (reader.Read())
                   {
                       var XMLOutput = reader[0].ToString();
                   }
               }
               catch (Exception)
               {
                   
                   throw;
               }
           }
           catch (Exception)
           {
               throw;
           }
       }

       public void SetFrequencyForPatient(string patientid, string frequency)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("InsertFrequencyDetails", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@FREQUENCY", frequency);
              

               var reader = sqlCommand.ExecuteReader();

               SetFrequencyForPatientExternalDatabase( patientid,  frequency);

               //while (reader.Read())
               //{
               //    if (reader[0].ToString() == "1")
               //    {
               //        var readervalue = reader[1].ToString();
               //        readerval = reader[0].ToString();

               //    }

               //    else
               //    {
               //        readerval = reader[0].ToString();
               //    }

               //}

           }
           catch (Exception ex)
           {

               readerval = ex.ToString();
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }
       }

       public void SetFrequencyForPatientExternalDatabase(string patientid, string frequency)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("InsertFrequencyDetails", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@FREQUENCY", frequency);


               var reader = sqlCommand.ExecuteReader();

           }
           catch (Exception ex)
           {

               readerval = ex.ToString();
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }
       }

       public void DeActivateAppsSync(string patientid, string AppName)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("DeActivateApps", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@AppName", AppName);


               var reader = sqlCommand.ExecuteReader();

           }
           catch (Exception ex)
           {

               readerval = ex.ToString();
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }
       }

       public void DeActivateApps(string patientid, string AppName)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("DeActivateApps", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@AppName", AppName);


               var reader = sqlCommand.ExecuteReader();

               DeActivateAppsSync(patientid, AppName);

           }
           catch (Exception ex)
           {

               readerval = ex.ToString();
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }
       }

       public void InstallAndActivateApps(string patientid, string AppName)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("ActivateApps", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@AppName", AppName);

               var reader = sqlCommand.ExecuteReader();

               InstallActivateAppsSync(patientid, AppName);

           }
           catch (Exception ex)
           {

               readerval = ex.ToString();
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }
       }

       public void InstallActivateAppsSync(string patientid, string AppName)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("ActivateApps", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@AppName", AppName);


               var reader = sqlCommand.ExecuteReader();

           }
           catch (Exception ex)
           {

               readerval = ex.ToString();
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }
       }
    }
}
