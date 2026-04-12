using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioADB_BAL
{
   public class ReportGenerator
    {
       public DataSet GenerateCmtReport(string patientid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
          var ds = objTinnitustrioInsertpatient.GeerateCmtReport(patientid);

           return ds;
       }

       public DataSet GeerateCmtDateWiseReport(string patientid, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmtDateWiseReport(patientid, indate, outdate);

           return ds;
       }

       public DataSet GenerateCmmReport(string patientid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmmReport(patientid);

           return ds;
       }

       public DataSet GeerateCmmDateWiseReport(string patientid, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmmDateWiseReport(patientid, indate, outdate);

           return ds;
       }

       public DataSet GenerateCmesReport(string patientid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmesReport(patientid);

           return ds;
       }

       public DataSet GeerateCmesDateWiseReport(string patientid, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmesDateWiseReport(patientid, indate, outdate);

           return ds;
       }

       public DataSet GenerateCmnReport(string patientid)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmNReport(patientid);

           return ds;
       }

       public DataSet GeerateCmnDateWiseReport(string patientid, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeerateCmNDateWiseReport(patientid, indate, outdate);

           return ds;
       }

       public DataSet GeneratePatientReports()
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GeneratePatientReports();

           return ds;
       }

       public DataSet GetDoctorName()
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GetDoctorName();

           return ds;
       }

       public DataSet GenerateAppInstallationReport(string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(doctorid, flag);

           return ds;
       }

       public DataSet GenerateAppInstallationReport(string patientid, string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(patientid, doctorid, flag);

           return ds;
       }

       public DataSet GenerateAppInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(patientid, doctorid, flag, indate, outdate);

           return ds;
       }

       public DataSet GenerateAppInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateAppInstallationReport(doctorid, flag, indate, outdate);

           return ds;
       }

       public DataSet GenerateFreqInstallationReport(string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(doctorid, flag);

           return ds;
       }

       public DataSet GenerateFreqInstallationReport(string patientid, string doctorid, string flag)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(patientid, doctorid, flag);

           return ds;
       }

       public DataSet GenerateFreqInstallationReport(string patientid, string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(patientid, doctorid, flag, indate, outdate);

           return ds;
       }

       public DataSet GenerateFreqInstallationReport(string doctorid, string flag, string indate, string outdate)
       {
           var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.ReportGenerator();
           var ds = objTinnitustrioInsertpatient.GenerateFreqInstallationReport(doctorid, flag, indate, outdate);

           return ds;
       }


    }
}
