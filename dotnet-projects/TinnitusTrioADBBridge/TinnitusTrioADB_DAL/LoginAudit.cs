using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_DAL
{
   public class LoginAudit
    {
       public string AuditPatientDetails(DoctorLogging objTinnitusTrioBo)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;
           //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";
           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("AppInstallLogger", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
             
               sqlCommand.Parameters.AddWithValue("@installDoctorId", objTinnitusTrioBo.DoctorCode);
               sqlCommand.Parameters.AddWithValue("@installDoctorName", objTinnitusTrioBo.Firstname);
               sqlCommand.Parameters.AddWithValue("@installPatientid", objTinnitusTrioBo.PatientId);
               sqlCommand.Parameters.AddWithValue("@InstalledAppName", objTinnitusTrioBo.AppName);

               var reader = sqlCommand.ExecuteReader();

               while (reader.Read())
               {
                       readerval = reader[0].ToString();
               }

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

           return readerval;
       }

       public string AuditFrequencyDetails(DoctorLogging objTinnitusTrioBo)
       {
           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;
           //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";
           var readerval = "";

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("FreqInstallerLogger", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };

               sqlCommand.Parameters.AddWithValue("@installDoctorId", objTinnitusTrioBo.DoctorCode);
               sqlCommand.Parameters.AddWithValue("@installDoctorName", objTinnitusTrioBo.Firstname);
               sqlCommand.Parameters.AddWithValue("@installPatientid", objTinnitusTrioBo.PatientId);
               sqlCommand.Parameters.AddWithValue("@InstalledFreqValue", objTinnitusTrioBo.Mobile);

               var reader = sqlCommand.ExecuteReader();

               while (reader.Read())
               {
                   readerval = reader[0].ToString();
               }

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

           return readerval;
       }
    }
}
