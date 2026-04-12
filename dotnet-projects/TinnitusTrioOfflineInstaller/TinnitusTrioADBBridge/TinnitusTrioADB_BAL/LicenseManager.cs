using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;
using TinnitusTrioADB_DAL;

namespace TinnitusTrioADB_BAL
{
    public class LicenseManager
    {
        public TinnitusTrioADB_BO.LicenseManager CheckLicenseManager(TinnitusTrioADB_BO.LicenseManager objTinnitusTrioBo)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.ManageLicenses(objTinnitusTrioBo);
            return retval;
        }

        public TinnitusTrioADB_BO.LicenseManager CheckActiveLicense()
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.CheckLicense();
            return retval;
        }

        public string InsertDoctorDetails(string doctorid, string doctorname, string doctorcode)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.RegisterDoctor(doctorid, doctorname, doctorcode);
            return retval;
        }

        public string InsertDoctorActivation(string doctorcode)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.ActivateDoctor(doctorcode);
            return retval;
        }

        public string CheckActiveStatus(string doctorid)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.CheckActiveStatus(doctorid);
            return retval;
        }

        public string renewlicenses(string licenseCode, string doctorcode)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.GetLicenses(licenseCode, doctorcode);
            return retval;
        }

        public string getDoctorCode()
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.GetDoctorCode();
            return retval;
        }

        public string getPatientId()
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.GetPatientId();
            return retval;
        }

        public string GetAppLicenseId(string patientid)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.GetAppLicenseId(patientid);
            return retval;
        }

        public string SaveUniqueIdDetails(string uniqueId, string key, string patientId)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.SaveUniqueIdDetails(uniqueId,key, patientId);
            return retval;
        }

        public UniqueIdDetails CheckifUniqueIdExists(string patientid)
        {
            var objTinnitustrioInsertpatient = new TinnitusTrioADB_DAL.LicenseManager();
            var retval = objTinnitustrioInsertpatient.CheckifUniqueIdExists(patientid);
            return retval;
        }
    }
}
