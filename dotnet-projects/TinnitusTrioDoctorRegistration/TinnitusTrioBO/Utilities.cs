using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioBO
{
   public class Utilities
    {
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

        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            var result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(input);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            var result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
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

        public void SendMail(DoctorRegistration objRegistration, GenerateGuid objGuid)
        {
            try
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("tinnitustrio.danaah@gmail.com");
                    mail.To.Add(objRegistration.Email);
                    mail.CC.Add("suhel.shalls@gmail.com");
                    mail.Subject = "Registration Successful! Welcome E-Mail";

                    mail.IsBodyHtml = true;
                    string htmlBody;

                    htmlBody = "<p>Hello Dr/Mr/Ms/Mrs." + objRegistration.FirstName + " " + objRegistration.LastName + " </p><h1>Welcome to Tinnitus Trio</h1><br/><p>This Email Contains your user Credentials</p><br/><p>Your Login ID is: <h1>" + objGuid.DoctorCode + "</h1></p><br/><p>Kindly use this Doctor code to Login and Activate your Tinnitus trio applications</p><br/><p>Your Activation Serial Number is: </p><p><h1>" + objGuid.Guid1 + " - " + objGuid.Guid2 + " - " + objGuid.Guid3 + " - " + objGuid.Guid4 + " - " + objGuid.Guid5 + "</h1></p></br><p>For More information, kindly contact <h3>support@danaahglobal.com</h3></p><br/><p>Regards</p><p>Registration Team, Tinnitus Trio</p>";

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
    }
}
