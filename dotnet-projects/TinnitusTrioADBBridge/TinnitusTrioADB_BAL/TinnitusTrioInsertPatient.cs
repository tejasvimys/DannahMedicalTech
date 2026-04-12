using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using TinnitusTrioADB_BO;
using TinnitusTrioADB_DAL;

namespace TinnitusTrioADB_BAL
{
   public class TinnitusTrioInsertPatient
    {
       public string InsertPatientDetails(TinnitusTrioBO objTinnitusTrioBo)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioInsert();
           var retval = objTinnitustrioInsertpatient.InsertpatientDetails(objTinnitusTrioBo);
           return retval;
       }

       public string CheckifPatientExists(string patientid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioInsert();
           var retval = objTinnitustrioInsertpatient.CheckforPatientId(patientid);
           return retval;
       }

       public void SyncPatient()
       {
           var syncObject = new TinnitusTrioSync();

          //syncObject.SyncPatient();
       }
    }
}
