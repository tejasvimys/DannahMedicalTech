using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_BAL
{
   public class LoginAudit
    {
       public string AuditPatientDetails(DoctorLogging objTinnitusTrioBo)
       {
           try
           {
               var objAudit = new TinnitusTrioADB_DAL.LoginAudit();
               var retVal  = objAudit.AuditPatientDetails(objTinnitusTrioBo);
               return retVal;
           }
           catch (Exception)
           {
               throw;
           }
       }

       public string AuditFrequencyDetails(DoctorLogging objTinnitusTrioBo)
       {
           try
           {
               var objAudit = new TinnitusTrioADB_DAL.LoginAudit();
               var retVal = objAudit.AuditFrequencyDetails(objTinnitusTrioBo);
               return retVal;
           }
           catch (Exception)
           {
               throw;
           }
       }

    }
}
