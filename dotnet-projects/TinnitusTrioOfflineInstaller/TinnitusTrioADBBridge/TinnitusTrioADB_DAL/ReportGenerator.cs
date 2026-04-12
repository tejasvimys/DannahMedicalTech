using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioADB_DAL
{
   public class ReportGenerator
    {
       public DataSet GeerateCmtReport(string patientid)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmtReport", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);

               using (var da = new SqlDataAdapter(sqlCommand))
               {
                  
                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmtDateWiseReport(string patientid, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmtReportDateWise", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmmReport(string patientid)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmmReport", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmmDateWiseReport(string patientid, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmmReportDateWise", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmesReport(string patientid)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmesReport", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmesDateWiseReport(string patientid, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmesReportDateWise", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmNReport(string patientid)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmnReport", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GeerateCmNDateWiseReport(string patientid, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateCmnReportDateWise", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@PATIENTID", patientid);
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }


       public DataSet GeneratePatientReports()
       {
           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GenerateReportPatient", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       public DataSet GetDoctorName()
       {
           var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

           var dataset = new DataSet();

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("GetDoctorName", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }

       //get the app installation report
       public DataSet GenerateAppInstallationReport(string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }

       public DataSet GenerateAppInstallationReport(string patientid, string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }

       public DataSet GenerateAppInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }

       public DataSet GenerateAppInstallationReport( string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
               sqlCommand.Parameters.AddWithValue("@indate", indate);
               sqlCommand.Parameters.AddWithValue("@outdate", outdate);
               sqlCommand.Parameters.AddWithValue("@flg", flag);

               using (var da = new SqlDataAdapter(sqlCommand))
               {

                   da.Fill(dataset);
                   return dataset;
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

           return dataset;
       }



       //get the Frequency installation report
       public DataSet GenerateFreqInstallationReport(string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }

       public DataSet GenerateFreqInstallationReport(string patientid, string doctorid, string flag)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }

       public DataSet GenerateFreqInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }

       public DataSet GenerateFreqInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var dataset = new DataSet();

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
                   return dataset;
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

           return dataset;
       }
    }
}
