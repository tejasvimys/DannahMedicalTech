using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLteDAL;
using AdminLteBusinessFunctions;

namespace AdminLteDAL
{

    public class DoctorRegistration
    {
        //creating db context object
        private TinnitusTrioEntities dbContext = new TinnitusTrioEntities();

        public string InsertDoctor(DoctorDetail dctrDet)
        {
            try
            {
                dbContext.DoctorDetails.Add(dctrDet);
                dbContext.SaveChanges();
                return "Success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

        //check for doctors existance
        public DoctorDetail GetDoctorDetails(string doctorCode)
        {
            try
            {
                var doctorDetail = dbContext.DoctorDetails.FirstOrDefault(b => b.DoctorCode == doctorCode);
                return doctorDetail;
            }
           
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

        public List<OfflineLicenseRequest> GetAllDoctorRequests()
        {
            try
            {
                var doctorDetails = dbContext.OfflineLicenseRequests.Where(x => x.requestActiveStatus == true).ToList();
                return doctorDetails;
            }
            
             catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }

        }

        public List<OFFLINELICENSESTRACKER> GetAllLicenseDetails()
        {
            try
            {
                var doctorDetails = dbContext.OFFLINELICENSESTRACKERs.Where(x => x.isActive == true).ToList();
                return doctorDetails;
            }

            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
           
        }

        public string SetLicensesForDoctor(List<uniqueID> lstUniqueId, string doctorCode)
        {
            try
            {

                foreach (var objUniqueid in lstUniqueId)
                {
                    dbContext.uniqueIDs.Add(objUniqueid);
                }

                //dbContext.SaveChanges();
                //getting the serial key generated for the doctor code
                var objLicensekeys = new OFFLINELICENSESTRACKER();
                var businessfunction = new BusinessFunctions();

                //get the active request id for the doctor
                var requestId =
                    dbContext.OfflineLicenseRequests.Where(
                        x => (x.doctorCode == doctorCode) && (x.requestActiveStatus == true)).First();

                objLicensekeys.DoctorCode = doctorCode;
                objLicensekeys.SerialKey = businessfunction.LicenseKeyGenerator();
                objLicensekeys.requestId = (requestId.requestId);
                objLicensekeys.isActive = true;

                dbContext.OFFLINELICENSESTRACKERs.Add(objLicensekeys);
                //dbContext.SaveChanges();

                //to send email for renewal of licenses.
                var emailAddressEntity = dbContext.DoctorDetails.Where(x => x.DoctorCode == doctorCode).First();
                var strFirstname = emailAddressEntity.FirstName;
                var strLastName = emailAddressEntity.LastName;
                var stremail = emailAddressEntity.email;

                businessfunction.SendLicenseEmail(strFirstname, strLastName, stremail, objLicensekeys.SerialKey);

                //updating the record for the request
                var contextVal =
                    dbContext.OfflineLicenseRequests.SingleOrDefault(
                        x => (x.doctorCode == doctorCode) && (x.requestActiveStatus == true));
                if (contextVal != null)
                {
                    contextVal.requestActiveStatus = false;
                    contextVal.requestisserviced = true;
                    contextVal.requestenddate = DateTime.Now.Date;

                }

                dbContext.SaveChanges();

                return "1";
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

        public int GetLatestCountforDoctor(string doctorCode)
        {
            try
            {
                var docCount = dbContext.uniqueIDs.Where(b => b.DoctorCode == doctorCode);
                return docCount != null ? Convert.ToInt32(docCount.Max(x => x.offlineSerial)) : 0;
            }
            //check and get the last incremental number from the database for the uniqueid. 
            //if not available, set the counter to 1 and start incrementing.
            //var docCount = dbContext.uniqueIDs.OrderByDescending(b => b.DoctorCode == doctorCode).FirstOrDefault();
            //return docCount != null ? Convert.ToInt32(docCount.offlineSerial) : 0;
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }

           
        }

        public List<DoctorDetail> GetDoctorRegistrations()
        {
            try
            {
                var docReg = dbContext.DoctorDetails.ToList();
                return docReg;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
           
        }

        public List<uniqueID> GetUniqueIdList(string docCode)
        {
            try
            {
                var lstUniqueId = dbContext.uniqueIDs.Where(u => u.patiendid.Contains(docCode)).ToList();
                return lstUniqueId;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
           
        }

        public List<userauthentication> GetUserAuthenticationList(string uniqueid)
        {
            try
            {
                var lstUserAuthentication = dbContext.userauthentications.Where(u => u.userId == uniqueid).ToList();
                return lstUserAuthentication;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }

           
        }

        public bool ReActivatePatient(string userId)
        {
            try
            {
                var patientRcd = dbContext.userauthentications.Single(item => item.userId == userId);
                //dbContext.userauthentications.(patientRcd);
                ////dbContext.userauthentications.


                dbContext.userauthentications.Remove(patientRcd);
                //dbContext.SaveChanges();

                var uniqueId = dbContext.uniqueIDs.Single(item => item.uniqueid1 == userId);
                uniqueId.isActive = true;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }

        }

        public bool DeActivateDoctor(int id)
        {
            try
            {
                var deActivDoc = dbContext.DoctorDetails.Single(item => item.id == id);
                deActivDoc.Isactive = false;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

        public bool ReactivateDoctor(int id)
        {
            try
            {
                var deActivDoc = dbContext.DoctorDetails.Single(item => item.id == id);
                deActivDoc.Isactive = true;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

        public string SaveDoctorDetails(DoctorDetail objDoctorRegistration, SerialNumber objGuid)
        {
            var retVal = "";

            var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("spCreateDoctor", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@FirstName", objDoctorRegistration.FirstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", objDoctorRegistration.MiddleName);
                sqlCommand.Parameters.AddWithValue("@LastName", objDoctorRegistration.LastName);
                sqlCommand.Parameters.AddWithValue("@Address1", objDoctorRegistration.Address1);
                sqlCommand.Parameters.AddWithValue("@Address2", objDoctorRegistration.Address2);
                sqlCommand.Parameters.AddWithValue("@Country", objDoctorRegistration.country);
                sqlCommand.Parameters.AddWithValue("@State", objDoctorRegistration.state);
                sqlCommand.Parameters.AddWithValue("@City", objDoctorRegistration.city);
                sqlCommand.Parameters.AddWithValue("@ZipCode", objDoctorRegistration.ZipCode);
                sqlCommand.Parameters.AddWithValue("@Phone", objDoctorRegistration.phone);
                sqlCommand.Parameters.AddWithValue("@Mobile", objDoctorRegistration.mobileno);
                sqlCommand.Parameters.AddWithValue("@Fax", objDoctorRegistration.fax);
                sqlCommand.Parameters.AddWithValue("@Email", objDoctorRegistration.email);
                sqlCommand.Parameters.AddWithValue("@Website", objDoctorRegistration.website);
                sqlCommand.Parameters.AddWithValue("@HospitalName", objDoctorRegistration.HospitalNme);
                sqlCommand.Parameters.AddWithValue("@DoctorCode", objGuid.DoctorID);
                sqlCommand.Parameters.AddWithValue("@SubscriptionType", objDoctorRegistration.SubscriptionType);
                sqlCommand.Parameters.AddWithValue("@SubscriptionDate", objDoctorRegistration.SubscriptionDate);

                sqlCommand.Parameters.AddWithValue("@GUID1", objGuid.Serial1);
                sqlCommand.Parameters.AddWithValue("@GUID2", objGuid.Serial2);
                sqlCommand.Parameters.AddWithValue("@GUID3", objGuid.Serial3);
                sqlCommand.Parameters.AddWithValue("@GUID4", objGuid.Serial4);
                sqlCommand.Parameters.AddWithValue("@GUID5", objGuid.Serial5);

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var readerval = reader[0].ToString();

                    return readerval;

                }
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return retVal;
        }

        public List<AdminLteDAL.PatientRegistration>GetPatientData(string doctorId)
        {
            try
            {
                var lstPatientDetails =
               dbContext.PatientRegistrations.Where(u => u.referredDoctor.Contains(doctorId)).ToList();
                return lstPatientDetails;
            }

            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
           
        }

        public PatientRegistration GetAppTransferDetails(string patientId)
        {
            try
            {
                var objPatientAppTransferDetails =
                dbContext.PatientRegistrations.Single(u => u.patientID.Contains(patientId));
                return objPatientAppTransferDetails;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
            
        }

        public bool DeActivateaPatient(string patientId)
        {
            try
            {
                var uniqueId = dbContext.uniqueIDs.Single(item => item.patiendid == patientId);
                uniqueId.isActive = false;
                var patientRegistration = dbContext.PatientRegistrations.Single(item => item.patientID == patientId);
                patientRegistration.isActive = false;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

        public bool ReActivateaPatient(string patientId)
        {
            try
            {
                var uniqueId = dbContext.uniqueIDs.Single(item => item.patiendid == patientId);
                uniqueId.isActive = true;
                var patientRegistration = dbContext.PatientRegistrations.Single(item => item.patientID == patientId);
                patientRegistration.isActive = true;

                var useridSet = (from r in dbContext.uniqueIDs where r.patiendid == patientId select new {r.uniqueid1}).Single();
                var patientRcd = dbContext.userauthentications.FirstOrDefault(item => item.userId==useridSet.uniqueid1);
                if (patientRcd != null)
                {
                    dbContext.userauthentications.Remove(patientRcd);
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
        }

    }

}
