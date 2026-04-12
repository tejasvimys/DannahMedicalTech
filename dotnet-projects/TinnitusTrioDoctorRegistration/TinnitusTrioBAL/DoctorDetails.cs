using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioBO;

namespace TinnitusTrioBAL
{
   public class DoctorDetails
    {
       public string GetDoctorDetails(DoctorRegistration objDoctorRegistration)
       {
           try
           {
               var objDoctorDal = new TinnitusTrioDAL.DoctorDetails();
               var objUtilities = new Utilities();
               var genobjGuid = new GenerateGuid();
               var objGuid = objUtilities.GenerateGuid(genobjGuid);

               var retVal = objDoctorDal.SaveDoctorDetails(objDoctorRegistration, objGuid);
               if (retVal.Equals("SUCCESS"))
               {
                   objUtilities.SendMail(objDoctorRegistration, objGuid);
               }
              
               return retVal;

           }
           catch (Exception)
           {
               
               throw;
           }
       }

       public DataTable DoctorDetailsTable()
       {
           try
           {
               var objDoctorDal = new TinnitusTrioDAL.DoctorDetails();
               var retVal = objDoctorDal.GetDoctorDetails();
               return retVal;

           }
           catch (Exception)
           {

               throw;
           }
       }

       public DataTable GenerateAppInstallationReport()
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails();
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport();

           return ds;
       }

       public DataTable GenerateAppInstallationReport(string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails();
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(doctorid, flag);

           return ds;
       }

       public DataTable GenerateAppInstallationReport(string patientid, string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(patientid, doctorid, flag);

           return ds;
       }

       public DataTable GenerateAppInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(patientid, doctorid, flag, indate, outdate);

           return ds;
       }

       public DataTable GenerateAppInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(doctorid, flag, indate, outdate);

           return ds;
       }

       public DataTable GenerateFreqInstallationReport(string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(doctorid, flag);

           return ds;
       }

       public DataTable GenerateFreqInstallationReport(string patientid, string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(patientid, doctorid, flag);

           return ds;
       }

       public DataTable GenerateFreqInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(patientid, doctorid, flag, indate, outdate);

           return ds;
       }

       public DataTable GenerateFreqInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioDAL.DoctorDetails(); ;
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(doctorid, flag, indate, outdate);

           return ds;
       }

       public string UpdateDoctorSubscription(DoctorRegistration objDoctorRegistration)
       {
           try
           {
               var objDoctorDal = new TinnitusTrioDAL.DoctorDetails();
   
               var retVal = objDoctorDal.UpdateDoctorSubscription(objDoctorRegistration);

               return retVal;

           }
           catch (Exception)
           {

               throw;
           }
       }

       public string UpdateDoctorSubscriptionType(DoctorRegistration objDoctorRegistration)
       {
           try
           {
               var objDoctorDal = new TinnitusTrioDAL.DoctorDetails();

               var retVal = objDoctorDal.UpdateDoctorSubscriptionType(objDoctorRegistration);

               return retVal;

           }
           catch (Exception)
           {

               throw;
           }
       }
    }
}
