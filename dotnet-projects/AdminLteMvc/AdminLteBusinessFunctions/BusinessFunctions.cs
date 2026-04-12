using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdminLteModels;
using System.IO;

namespace AdminLteBusinessFunctions
{
   public class BusinessFunctions
    {
       public void SendEmail(AdminLteModels.DoctorRegistration objDocRegmodel)
       {
           try
           {
               try
               {
                  // var mail = new MailMessage();
                  // var smtpServer = new SmtpClient("danaahtech.com");

                  // mail.From = new MailAddress("registration@danaahtech.com");
                  // mail.To.Add(objDocRegmodel.Email);
                  // mail.CC.Add("mahaboobshahnawaz@gmail.com");
        
                  // mail.Subject = "Registration Successful! Welcome E-Mail";

                  // mail.IsBodyHtml = true;

                  // var htmlBody = "<p>Hello Dr/Mr/Ms/Mrs." + objDocRegmodel.FirstName + " " + objDocRegmodel.LastName + " </p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your user Credentials</p><br/><p>Your Login ID is: <h1>" + objDocRegmodel.DoctorCode + "</h1></p><br/><p>Kindly use this Doctor code to Login and Activate your Tinnitus trio applications</p><br/><p>Your Activation Serial Number is: </p><p><h1>"+objDocRegmodel.AuthenticationCode+"</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

                  // mail.Body = htmlBody;

                  // smtpServer.Port = 25;
                  // smtpServer.Credentials = new System.Net.NetworkCredential("registration@danaahtech.com", "Abek187*");
                  // smtpServer.EnableSsl = false;

                  // System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                  //System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                  //System.Security.Cryptography.X509Certificates.X509Chain chain,
                  //System.Net.Security.SslPolicyErrors sslPolicyErrors)
                  // {
                  //     return true;
                  // };

                  // smtpServer.Send(mail);

                   var mail = new MailMessage();
                   var smtpServer = new SmtpClient("smtp.gmail.com");

                   mail.From = new MailAddress("tinnitustrio.danaah@gmail.com");
                   mail.To.Add(objDocRegmodel.Email);
                   mail.CC.Add("syeddawoo@gmail.com");

                   mail.Subject = "Registration Successful! Welcome E-Mail";

                   mail.IsBodyHtml = true;

                   var htmlBody = "<p>Hello Dr/Mr/Ms/Mrs." + objDocRegmodel.FirstName + " " + objDocRegmodel.LastName + " </p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your user Credentials</p><br/><p>Your Login ID is: <h1>" + objDocRegmodel.DoctorCode + "</h1></p><br/><p>Kindly use this Doctor code to Login and Activate your Tinnitus trio applications</p><br/><p>Your Activation Serial Number is: </p><p><h1>" + objDocRegmodel.AuthenticationCode + "</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

                   mail.Body = htmlBody;

                   smtpServer.Port = 587;
                   smtpServer.Credentials = new System.Net.NetworkCredential("tinnitustrio.danaah@gmail.com", "danaah@#88");
                   smtpServer.EnableSsl = true;

                   System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                  System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                  System.Security.Cryptography.X509Certificates.X509Chain chain,
                  System.Net.Security.SslPolicyErrors sslPolicyErrors)
                   {
                       return true;
                   };

                   smtpServer.Send(mail);
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

       public string ResolveSerialNumber(AdminLteModels.DoctorRegistration objDocRegmodel)
       {
           using (var hash = SHA256.Create())
           {
               return String.Join("", hash
                 .ComputeHash(Encoding.UTF8.GetBytes(objDocRegmodel.SerialNumber))
                 .Select(item => item.ToString("x2")));
           }
       }

       public string GenerateDoctorCode()
       {
           var guid6 = System.Guid.NewGuid();
           var substring6 = guid6.ToString();
           substring6 = substring6.Substring(0, 3);
           return substring6;
       }

       public string GenerateUniqueId()
       {
           System.Guid guid1 = System.Guid.NewGuid();
           var substring1 = guid1.ToString();
           substring1 = substring1.Substring(0, 8);
           return substring1;
       }

       public List<GenerateUniqueId> GenerateLicenseDetails(string doctorCode, int doctorCount, int licenseCount)
       {
           var lstGenerateuniqueid = new List<GenerateUniqueId>();
          
           for (var i = 1; i < licenseCount+1; i++)
           {
               var patientId = doctorCount + i;
               var objGenerateUniqueid = new GenerateUniqueId
               {
                   DoctorCode = doctorCode,
                   OfflineSerialNumber = patientId,
                   Uniqueid = GenerateUniqueId(),
                   PatientId = doctorCode + "-" + patientId
                
               };

               lstGenerateuniqueid.Add(objGenerateUniqueid);
           }

           return lstGenerateuniqueid;
       }

       public string LicenseKeyGenerator()
       {
           var random = new Random();
           const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
           return new string(Enumerable.Repeat(chars, 8)
               .Select(s => s[random.Next(s.Length)]).ToArray());

       }

       public void SendLicenseEmail(string firstname, string lastname, string email, string renewalcode)
       {
           try
           {
               try
               {
                  // var mail = new MailMessage();
                  // var smtpServer = new SmtpClient("danaahtech.com");

                  // mail.From = new MailAddress("registration@danaahtech.com");
                  // mail.To.Add(email);
                  // mail.CC.Add("mahaboobshahnawaz@gmail.com");
                  // mail.Subject = "Renewal Successful! Welcome E-Mail";

                  // mail.IsBodyHtml = true;

                  // var htmlBody = "<p>Hello Dr/Mr/Ms/Mrs." + firstname + " " + lastname + " </p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your Renewal Details</p><br/><p>Kindly use this Renewal code to Login and Renew your Tinnitus trio applications</p><br/><p>Your Renewal Serial Number is: </p><p><h1>" + renewalcode + "</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

                  // mail.Body = htmlBody;

                  // smtpServer.Port = 25;
                  // smtpServer.Credentials = new System.Net.NetworkCredential("registration@danaahtech.com", "Abek187*");
                  // smtpServer.EnableSsl = false;

                  // System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                  //System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                  //System.Security.Cryptography.X509Certificates.X509Chain chain,
                  //System.Net.Security.SslPolicyErrors sslPolicyErrors)
                  // {
                  //     return true;
                  // };

                  // smtpServer.Send(mail);

                   var mail = new MailMessage();
                   var smtpServer = new SmtpClient("smtp.gmail.com");

                   mail.From = new MailAddress("tinnitustrio.danaah@gmail.com");
                   mail.To.Add(email);
                   mail.CC.Add("syeddawoo@gmail.com");
                   mail.Subject = "Renewal Successful! Welcome E-Mail";

                   mail.IsBodyHtml = true;

                   var htmlBody = "<p>Hello Dr/Mr/Ms/Mrs." + firstname + " " + lastname + " </p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your Renewal Details</p><br/><p>Kindly use this Renewal code to Login and Renew your Tinnitus trio applications</p><br/><p>Your Renewal Serial Number is: </p><p><h1>" + renewalcode + "</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

                   mail.Body = htmlBody;

                   smtpServer.Port = 587;
                   smtpServer.Credentials = new System.Net.NetworkCredential("tinnitustrio.danaah@gmail.com", "danaah@#88");
                   smtpServer.EnableSsl = true;

                   System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                  System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                  System.Security.Cryptography.X509Certificates.X509Chain chain,
                  System.Net.Security.SslPolicyErrors sslPolicyErrors)
                   {
                       return true;
                   };

                   smtpServer.Send(mail);
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
       public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
       {
           byte[] encryptedBytes = null;

           // Set your salt here, change it to meet your flavor:
           // The salt bytes must be at least 8 bytes.
           var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

           using (var ms = new MemoryStream())
           {
               using (var aes = new RijndaelManaged())
               {
                   aes.KeySize = 256;
                   aes.BlockSize = 128;

                   var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                   aes.Key = key.GetBytes(aes.KeySize / 8);
                   aes.IV = key.GetBytes(aes.BlockSize / 8);

                   aes.Mode = CipherMode.CBC;

                   using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                   {
                       cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                       cs.Close();
                   }
                   encryptedBytes = ms.ToArray();
               }
           }

           return encryptedBytes;
       }

       public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
       {
           byte[] decryptedBytes = null;

           // Set your salt here, change it to meet your flavor:
           // The salt bytes must be at least 8 bytes.
           var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

           using (var ms = new MemoryStream())
           {
               using (var aes = new RijndaelManaged())
               {
                   aes.KeySize = 256;
                   aes.BlockSize = 128;

                   var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                   aes.Key = key.GetBytes(aes.KeySize / 8);
                   aes.IV = key.GetBytes(aes.BlockSize / 8);

                   aes.Mode = CipherMode.CBC;

                   using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                   {
                       cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                       cs.Close();
                   }
                   decryptedBytes = ms.ToArray();
               }
           }

           return decryptedBytes;
       }

       public string EncryptText(string password)
       {
           // Get the bytes of the string
           var bytesToBeEncrypted = Encoding.UTF8.GetBytes("7860");
           var passwordBytes = Encoding.UTF8.GetBytes(password);

           // Hash the password with SHA256
           passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

           var bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

           var result = Convert.ToBase64String(bytesEncrypted);

           return result;
       }

       public string DecryptText( string password)
       {
           // Get the bytes of the string
           var bytesToBeDecrypted = Convert.FromBase64String("7860");
           var passwordBytes = Encoding.UTF8.GetBytes(password);
           passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

           var bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

           var result = Encoding.UTF8.GetString(bytesDecrypted);

           return result;
       }


       public void SendEmployeeEmail(string Email, string password)
       {
           try
           {
               try
               {
                    //var mail = new MailMessage();
                    //var smtpServer = new SmtpClient("danaahtech.com");

                    //mail.From = new MailAddress("registration@danaahtech.com");
                    //mail.To.Add(Email);
                    //mail.CC.Add("mahaboobshahnawaz@gmail.com");
                    var mail = new MailMessage();
                    var smtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("tinnitustrio.danaah@gmail.com");
                    mail.To.Add(Email);
                    mail.CC.Add("syeddawoo@gmail.com");
                    mail.Subject = "Welcome to Danaah Medical Technologies, a Tinnitus Trio Company";

                   mail.IsBodyHtml = true;

                   var htmlBody = "<p>Hello</p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your Credentials. </p><br/><p>Kindly use this to Login to the Admin Console of Tinnitus trio</p><br/><p>Your User ID is: </p><p><h1>" + Email + "</h1></p></br><p>Your Password is: </p><p><h1>" + password + "</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Admin Team, Tinnitus Trio</p>";

                   mail.Body = htmlBody;

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential("tinnitustrio.danaah@gmail.com", "danaah@#88");
                    smtpServer.EnableSsl = true;

                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                  System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                  System.Security.Cryptography.X509Certificates.X509Chain chain,
                  System.Net.Security.SslPolicyErrors sslPolicyErrors)
                   {
                       return true;
                   };

                   smtpServer.Send(mail);
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

       public GenerateGuid GenerateGuid(GenerateGuid objGuid)
       {
           try
           {
               var guid1 = System.Guid.NewGuid();
               var substring1 = guid1.ToString();
               substring1 = substring1.Substring(0, 5);

               var guid2 = System.Guid.NewGuid();
               var substring2 = guid2.ToString();
               substring2 = substring2.Substring(0, 5);

               var guid3 = System.Guid.NewGuid();
               var substring3 = guid3.ToString();
               substring3 = substring3.Substring(0, 5);

               var guid4 = System.Guid.NewGuid();
               var substring4 = guid4.ToString();
               substring4 = substring4.Substring(0, 5);

               var guid5 = System.Guid.NewGuid();
               var substring5 = guid5.ToString();
               substring5 = substring5.Substring(0, 5);

               var guid6 = System.Guid.NewGuid();
               var substring6 = guid6.ToString();
               substring6 = substring6.Substring(0, 3);

               objGuid.Guid1 = substring1.ToUpper();
               objGuid.Guid2 = substring2.ToUpper();
               objGuid.Guid3 = substring3.ToUpper();
               objGuid.Guid4 = substring4.ToUpper();
               objGuid.Guid5 = substring5.ToUpper();
               objGuid.DoctorCode = substring6.ToUpper();

               return objGuid;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public void SendOnlineMail(DoctorRegistration objRegistration, GenerateGuid objGuid)
       {
           try
           {
               try
               {
                   MailMessage mail = new MailMessage();
                   SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                   mail.From = new MailAddress("tinnitustrio.danaah@gmail.com");
                   mail.To.Add(objRegistration.Email);
                   mail.CC.Add("syeddawoo@gmail.com");
                   mail.Subject = "Registration Successful! Welcome E-Mail";

                   mail.IsBodyHtml = true;
                   string htmlBody;

                   htmlBody = "<p>Hello Dr/Mr/Ms/Mrs." + objRegistration.FirstName + " " + objRegistration.LastName + " </p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your user Credentials</p><br/><p>Your Login ID is: <h1>" + objGuid.DoctorCode + "</h1></p><br/><p>Kindly use this Doctor code to Login and Activate your Tinnitus trio applications</p><br/><p>Your Activation Serial Number is: </p><p><h1>" + objGuid.Guid1 + " - " + objGuid.Guid2 + " - " + objGuid.Guid3 + " - " + objGuid.Guid4 + " - " + objGuid.Guid5 + "</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

                   mail.Body = htmlBody;

                   SmtpServer.Port = 587;
                   SmtpServer.Credentials = new System.Net.NetworkCredential("tinnitustrio.danaah", "danaah@#88");
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

        public static void LogFileWrite(string message)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                var logFilePath = "c:\\LogError\\";

                logFilePath = logFilePath + "ProgramLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                var logFileInfo = new FileInfo(logFilePath);
                if (logFileInfo.DirectoryName != null) logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (logDirInfo != null && !logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                fileStream = !logFileInfo.Exists ? logFileInfo.Create() : new FileStream(logFilePath, FileMode.Append);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(message);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }

        }

    }
}
