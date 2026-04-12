using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_DAL
{
    public class TinnitusTrioInsert
    {
        public string InsertpatientDetails(TinnitusTrioBO objTinnitusTrioBo)
        {

            //getting guid and trimming to get the substring of the same
            System.Guid guid1 = System.Guid.NewGuid();
            var substring1 = guid1.ToString();
            substring1 = substring1.Substring(0, 8);

            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";
            var readerval = "";

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("InsertPatientDetails", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PatientID", objTinnitusTrioBo.PatientId);
                sqlCommand.Parameters.AddWithValue("@firstname", objTinnitusTrioBo.Firstname);
                sqlCommand.Parameters.AddWithValue("@middlename", objTinnitusTrioBo.Middlename);
                sqlCommand.Parameters.AddWithValue("@lastname", objTinnitusTrioBo.Lastname);
                sqlCommand.Parameters.AddWithValue("@addressline1", objTinnitusTrioBo.Addressline1);
                sqlCommand.Parameters.AddWithValue("@addressline2", objTinnitusTrioBo.Addressline2);
                sqlCommand.Parameters.AddWithValue("@city", objTinnitusTrioBo.City);
                sqlCommand.Parameters.AddWithValue("@state", objTinnitusTrioBo.State);
                sqlCommand.Parameters.AddWithValue("@country", objTinnitusTrioBo.Country);
                sqlCommand.Parameters.AddWithValue("@zipcode", objTinnitusTrioBo.Zipcode);
                sqlCommand.Parameters.AddWithValue("@email", objTinnitusTrioBo.Email);
                sqlCommand.Parameters.AddWithValue("@telephone", objTinnitusTrioBo.Telephone);
                sqlCommand.Parameters.AddWithValue("@mobile", objTinnitusTrioBo.Mobile);
                sqlCommand.Parameters.AddWithValue("@doctorcode", objTinnitusTrioBo.DoctorCode);
                sqlCommand.Parameters.AddWithValue("@UniqueId", substring1);

              var reader =  sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0].ToString() == "1")
                    {
                        var readervalue = reader[1].ToString();
                        //SyncPatientwithmaindatabase(readervalue, substring1, objTinnitusTrioBo);
                        readerval = reader[1].ToString();
                        //SendMail(substring1, objTinnitusTrioBo);
                    }

                    else
                    {
                        readerval = reader[0].ToString();
                    }
                    
                }

            }
            catch (Exception ex)
            {

                readerval=ex.ToString();
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return readerval;
        }

        public void SyncPatientwithmaindatabase(string patientid, string uniqueid, TinnitusTrioBO objTinnitusTrioBo)
        {

            var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;
            //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";
            var readerval = "";

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("InsertPatientDetails", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PatientID", patientid);
                sqlCommand.Parameters.AddWithValue("@firstname", objTinnitusTrioBo.Firstname);
                sqlCommand.Parameters.AddWithValue("@middlename", objTinnitusTrioBo.Middlename);
                sqlCommand.Parameters.AddWithValue("@lastname", objTinnitusTrioBo.Lastname);
                sqlCommand.Parameters.AddWithValue("@addressline1", objTinnitusTrioBo.Addressline1);
                sqlCommand.Parameters.AddWithValue("@addressline2", objTinnitusTrioBo.Addressline2);
                sqlCommand.Parameters.AddWithValue("@city", objTinnitusTrioBo.City);
                sqlCommand.Parameters.AddWithValue("@state", objTinnitusTrioBo.State);
                sqlCommand.Parameters.AddWithValue("@country", objTinnitusTrioBo.Country);
                sqlCommand.Parameters.AddWithValue("@zipcode", objTinnitusTrioBo.Zipcode);
                sqlCommand.Parameters.AddWithValue("@email", objTinnitusTrioBo.Email);
                sqlCommand.Parameters.AddWithValue("@telephone", objTinnitusTrioBo.Telephone);
                sqlCommand.Parameters.AddWithValue("@mobile", objTinnitusTrioBo.Mobile);
                sqlCommand.Parameters.AddWithValue("@doctorcode", objTinnitusTrioBo.DoctorCode);
                sqlCommand.Parameters.AddWithValue("@UniqueId", uniqueid);

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
        }

        public string CheckforPatientId (string patientid)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";
            var readerval = "";

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("checkforpatientid", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PatientID", patientid);
                
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

        public void SendMail(string uniqueid, TinnitusTrioBO objTinnitusTrioBo)
        {
            try
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("tinnitustrio.danaah@gmail.com");
                    mail.To.Add(objTinnitusTrioBo.Email);
                    mail.Subject = "Registration Successful! Welcome E-Mail";

                    mail.IsBodyHtml = true;
                    string htmlBody;

                    htmlBody = "<p>Hello "+objTinnitusTrioBo.Firstname+" "+objTinnitusTrioBo.Lastname+"!! </p><h1>Welcome to Tinnitus Trio</h1><br/><p>Thank you for using our product!!</p></br><p>This E-mail Provides you the user Credentials for Tinnitus Trio Software.</p><br/><p>Your UNIQUE ID IS: <h1>"+uniqueid+"</h1></p><br/><p>Kindly use this Unique id to activate your Tinnitus trio applications in your mobile phone</p><br/><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

                    mail.Body = htmlBody;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("tinnitustrio.danaah", "danaah@#77");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataTable getAppInstalledStatus(string patientId)
        {
         
                var getData = new DataTable();
                var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
           
            var readerval = "";

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("OfflineCheckAPKInstalledForUser", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PatientID", patientId);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

                da.Fill(getData);
                return getData;
                
            }

            catch (Exception)
            {

                throw;
            }
        }

        public void SetFlagforAPKInstall(string patientid, string appName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("OfflineSetFlagforAPKinstall", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@PatientID", patientid);
                sqlCommand.Parameters.AddWithValue("@InstallFlag", appName);

                sqlCommand.ExecuteNonQuery();

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

        }
    }
}
