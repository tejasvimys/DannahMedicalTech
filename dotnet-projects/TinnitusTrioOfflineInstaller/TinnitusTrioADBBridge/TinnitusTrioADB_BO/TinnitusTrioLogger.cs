using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioADB_BO
{
   public class TinnitusTrioLogger
    {
       public static void SendErrorReport(string errorMessage)
       {
           try
           {
               var message = new MailMessage();
               var smtp = new SmtpClient();

               message.From = new MailAddress("tinnitustrio.danaah@gmail.com");
               message.To.Add(new MailAddress("tejasvi.qits@gmail.com"));
               message.Subject = "Error in Tinnitus Trio Application";
               message.Body = errorMessage;

               smtp.Port = 587;
               smtp.Host = "smtp.gmail.com";
               smtp.EnableSsl = true;
               smtp.UseDefaultCredentials = false;
               smtp.Credentials = new NetworkCredential("tinnitustrio.danaah@gmail.com", "danaah@#77");
               smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               smtp.Send(message);
           }
           catch (Exception ex)
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
    
    }
}
