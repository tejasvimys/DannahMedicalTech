using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_BAL
{
   public class Login
    {
       public string CheckFirstLogin(string doctorid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.Login();
           var retval = objTinnitustrioInsertpatient.CheckFirstLogin(doctorid);
           return retval;
       }

       public string ResetPassword(string password, string pin, string doctorcode, string doctorName)
       {
           var encryptstring = new TinnitusTrioLogger();

           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.Login();
           var retval = objTinnitustrioInsertpatient.ResetPassword(encryptstring.EncryptText(password, pin), pin, doctorcode, doctorName);
           return retval;
       }

       public string CheckLogin(string doctorId, string password, string pin, string doctorcode)
       {
           var encryptstring = new TinnitusTrioLogger();

           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.Login();
           var retval = objTinnitustrioInsertpatient.CheckLogin(doctorId, encryptstring.EncryptText(password, pin), pin, doctorcode);

           if (retval.Equals("INVALIDUSER"))
           {
               return retval;
           }

           else
           {
               var decryptedVal = encryptstring.DecryptText(retval, pin);
               return decryptedVal.Equals(password) ? "PASS" : "FAIL";

           }
           
       }

       public string GetPatientName(string patientid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.Login();
           var retval = objTinnitustrioInsertpatient.GetPatientName(patientid);
           return retval;
       }
    }
}
