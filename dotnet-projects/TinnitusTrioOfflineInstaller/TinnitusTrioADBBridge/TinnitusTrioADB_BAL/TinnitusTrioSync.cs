using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;
using TinnitusTrioADB_DAL;

namespace TinnitusTrioADB_BAL
{
   public  class TinnitusTrioSync
    {
       public void InsertFrequencyDetails(string patientid, string frequency)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.TinnitusTrioSync();
           objTinnitustrioInsertpatient.SetFrequencyForPatient(patientid, frequency);
       }

       public void DeactivateApps(string patientid, string Appname)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.TinnitusTrioSync();
           objTinnitustrioInsertpatient.DeActivateApps(patientid, Appname);
       }

       public void InstallandactivateApps(string patientid, string Appname)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.TinnitusTrioSync();
           objTinnitustrioInsertpatient.InstallAndActivateApps(patientid, Appname);
       }
    }
}
