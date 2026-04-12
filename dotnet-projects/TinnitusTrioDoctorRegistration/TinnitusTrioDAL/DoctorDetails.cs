using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioBO;

namespace TinnitusTrioDAL
{
   public class DoctorDetails
    {
       public string SaveDoctorDetails(DoctorRegistration objDoctorRegistration, GenerateGuid objGuid)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("spCreateDoctor", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@FirstName", objDoctorRegistration.FirstName);
               sqlCommand.Parameters.AddWithValue("@MiddleName", objDoctorRegistration.MiddleName);
               sqlCommand.Parameters.AddWithValue("@LastName", objDoctorRegistration.LastName);
               sqlCommand.Parameters.AddWithValue("@Address1", objDoctorRegistration.Address1);
               sqlCommand.Parameters.AddWithValue("@Address2", objDoctorRegistration.Address2);
               sqlCommand.Parameters.AddWithValue("@Country", objDoctorRegistration.Country);
               sqlCommand.Parameters.AddWithValue("@State", objDoctorRegistration.State);
               sqlCommand.Parameters.AddWithValue("@City", objDoctorRegistration.City);
               sqlCommand.Parameters.AddWithValue("@ZipCode", objDoctorRegistration.ZipCode);
               sqlCommand.Parameters.AddWithValue("@Phone", objDoctorRegistration.Phone);
               sqlCommand.Parameters.AddWithValue("@Mobile", objDoctorRegistration.Mobileno);
               sqlCommand.Parameters.AddWithValue("@Fax", objDoctorRegistration.Fax);
               sqlCommand.Parameters.AddWithValue("@Email", objDoctorRegistration.Email);
               sqlCommand.Parameters.AddWithValue("@Website", objDoctorRegistration.Website);
               sqlCommand.Parameters.AddWithValue("@HospitalName", objDoctorRegistration.HospitalName);
               sqlCommand.Parameters.AddWithValue("@DoctorCode", objGuid.DoctorCode);
               sqlCommand.Parameters.AddWithValue("@SubscriptionType", objDoctorRegistration.SubscriptionType);
               sqlCommand.Parameters.AddWithValue("@SubscriptionDate", objDoctorRegistration.SubscriptionDate);

               sqlCommand.Parameters.AddWithValue("@GUID1", objGuid.Guid1);
               sqlCommand.Parameters.AddWithValue("@GUID2", objGuid.Guid2);
               sqlCommand.Parameters.AddWithValue("@GUID3", objGuid.Guid3);
               sqlCommand.Parameters.AddWithValue("@GUID4", objGuid.Guid4);
               sqlCommand.Parameters.AddWithValue("@GUID5", objGuid.Guid5);

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

       public DataTable GetDoctorDetails()
       {
           var retVal= new DataTable();

           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("fetchDoctorDetails", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               var da = new SqlDataAdapter(sqlCommand);
               da.Fill(retVal);

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


        //get the app installation report

       public DataTable GenerateAppInstallationReport()
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("APPINSTALLHISTORYWILDCARD", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       public DataTable GenerateAppInstallationReport(string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("APPINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", "");
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", "");
               sqlCommand.Parameters.AddWithValue("@outdate", "");
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       public DataTable GenerateAppInstallationReport(string patientid, string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("APPINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", patientid);
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", "");
               sqlCommand.Parameters.AddWithValue("@outdate", "");
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       public DataTable GenerateAppInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("APPINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", patientid);
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       //without doctorid
       public DataTable GenerateAppInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("appinstallhistoryWithoutDoctorId", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@intime", indate);
               sqlCommand.Parameters.AddWithValue("@outtime", outdate);
               sqlCommand.Parameters.AddWithValue("@flag", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }



       //get the Frequency installation report
       public DataTable GenerateFreqInstallationReport(string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("FREQINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", "");
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", "");
               sqlCommand.Parameters.AddWithValue("@outdate", "");
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       public DataTable GenerateFreqInstallationReport(string patientid, string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("FREQINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", patientid);
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", "");
               sqlCommand.Parameters.AddWithValue("@outdate", "");
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       public DataTable GenerateFreqInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;
           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("FREQINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", patientid);
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }

       public DataTable GenerateFreqInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();
           DataTable dt;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("FREQINSTALLHISTORY", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@patientid", "");
               sqlCommand.Parameters.AddWithValue("@doctorid", doctorid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset); 
                   dt = dataset.Tables[0];
                   return dt;
               }

           }
           catch (Exception)
           {
               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return dt;
       }


       public string UpdateDoctorSubscription(DoctorRegistration objDoctorRegistration)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("spUpdateDoctorSubscription", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@doctorid", objDoctorRegistration.DoctorCode);
               sqlCommand.Parameters.AddWithValue("@subscriptiondate", objDoctorRegistration.SubscriptionDate);
      
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

       public string UpdateDoctorSubscriptionType(DoctorRegistration objDoctorRegistration)
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("spUpdateDoctorSubscriptionTypenDate", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@doctorid", objDoctorRegistration.DoctorCode);
               sqlCommand.Parameters.AddWithValue("@subscriptiondate", objDoctorRegistration.SubscriptionDate);
               sqlCommand.Parameters.AddWithValue("@subscriptionType", objDoctorRegistration.SubscriptionType);

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
