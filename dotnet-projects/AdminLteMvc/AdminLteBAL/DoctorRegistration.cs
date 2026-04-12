using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using AdminLteDAL;
using AdminLteModels;
using AdminLteBusinessFunctions;

namespace AdminLteBAL
{
    public class DoctorRegistration
    {
        public string InsertDoctorRegistration(AdminLteModels.DoctorRegistration objDocRegmodel)
        {
            try
            {
                var objDAlDocReg = new AdminLteDAL.DoctorRegistration();
                var mapperobj = new ObjectMapper();
                var objBusinessObjects = new BusinessFunctions();
                objDocRegmodel.DoctorCode = (objBusinessObjects.GenerateDoctorCode().ToUpper());
                var authenticationCode = objBusinessObjects.ResolveSerialNumber(objDocRegmodel);
                authenticationCode = authenticationCode + objDocRegmodel.DoctorCode;
                objDocRegmodel.AuthenticationCode = authenticationCode;


                var retVal = objDAlDocReg.InsertDoctor(mapperobj.MapDocDetails(objDocRegmodel));

                return retVal;
            }

            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                return ex.ToString();
            }
        }

        public AdminLteModels.DoctorRegistration GetDoctorCodeRegistrationDetails(string doctorCode)
        {
            try
            {
                var objDAlDocReg = new AdminLteDAL.DoctorRegistration();
                var mapperobj = new ObjectMapper();

                var objDocDetails = objDAlDocReg.GetDoctorDetails(doctorCode);
                var objModelDocDetails = mapperobj.MapDoctorCodeDetails(objDocDetails);
                return objModelDocDetails;
            }
            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
            
        }

        //GENERATE LICENSE KEYS FOR THE CURRENT USER
        public int GenerateLicenses(AdminLteModels.DoctorRegistration objDoctorRegistration,  int licenseCount)
        {
            try
            {
                var objDAlDocReg = new AdminLteDAL.DoctorRegistration();
                var mapperobj = new ObjectMapper();
                var objBusinessObjects = new BusinessFunctions();
                var docCount = objDAlDocReg.GetLatestCountforDoctor(objDoctorRegistration.DoctorCode);

                var lstLicenses = objBusinessObjects.GenerateLicenseDetails(objDoctorRegistration.DoctorCode, docCount, licenseCount);

                var lstDoctorLicenses = mapperobj.MapLicenseDetails(lstLicenses);

                var docRegRetVal = objDAlDocReg.SetLicensesForDoctor(lstDoctorLicenses, objDoctorRegistration.DoctorCode);

                return Convert.ToInt32(docRegRetVal);
            }

            catch (Exception ex)
            {
                BusinessFunctions.LogFileWrite(ex.ToString());
                throw;
            }
            
        }

        public List<AdminLteModels.DoctorRegistration> GetDoctorRegistrations()
        {
            var docReg = new AdminLteDAL.DoctorRegistration();
            var lstModelDocReg = docReg.GetDoctorRegistrations();
            var mapperobj = new ObjectMapper();
            var lstDocReg = mapperobj.MapDoctorRegistrations(lstModelDocReg);
            return lstDocReg;
        }

        public List<AdminLteModels.UniqueIds> GetUniqueIdLists(string docCode)
        {
            var objDocreg = new AdminLteDAL.DoctorRegistration();
            var lstUniqueId = objDocreg.GetUniqueIdList(docCode);
            var mapperobj = new ObjectMapper();
            var lstUniqueIdsModel = mapperobj.MaUniqueIdses(lstUniqueId);
            return lstUniqueIdsModel;
        }

        public List<AdminLteModels.UserAuthentication> GetUserAuthenticationDetails(string uniqueId)
        {
            var objDocreg = new AdminLteDAL.DoctorRegistration();
            var lstUserAuthentication = objDocreg.GetUserAuthenticationList(uniqueId);
            var mapperobj = new ObjectMapper();
            var lstuserauthenticationModel = mapperobj.MapUserAuthentication(lstUserAuthentication);
            return lstuserauthenticationModel;
        }

        public bool ReActivatePatient(string userId)
        {
            var objDocreg = new AdminLteDAL.DoctorRegistration();
            return objDocreg.ReActivatePatient(userId);
        }

        public bool DeActivateDoctor(int id)
        {
            var objDocreg = new AdminLteDAL.DoctorRegistration();
            return objDocreg.DeActivateDoctor(id);
        }

        public bool ReactivateDoctor(int id)
        {
            var objDocreg = new AdminLteDAL.DoctorRegistration();
            return objDocreg.ReactivateDoctor(id);
        }

        public string GetDoctorDetails(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            try
            {
                var objDoctorDal = new AdminLteDAL.DoctorRegistration();
                var objUtilities = new BusinessFunctions();
                var genobjGuid = new GenerateGuid();
                var objGuid = objUtilities.GenerateGuid(genobjGuid);

                var objDoctorDalFunctions = new ObjectMapper();
                objDoctorRegistration.DoctorCode = objGuid.DoctorCode;
                var objDDal = objDoctorDalFunctions.MapOnlineDoctorDetails(objDoctorRegistration);
                var objDalGuid = objDoctorDalFunctions.MapGuidDetails(objGuid);

                var retVal = objDoctorDal.SaveDoctorDetails(objDDal, objDalGuid);
                if (retVal.Equals("SUCCESS"))
                {
                    objUtilities.SendOnlineMail(objDoctorRegistration, objGuid);
                }

                return retVal;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<PatientDetails> GetPatientDetails(string doctorId)
        {
            try
            {
                var objPatientDal = new AdminLteDAL.DoctorRegistration();
                var objObjectMapper = new ObjectMapper();
                var lstPatientDetails = objObjectMapper.MapPatientDetails(objPatientDal.GetPatientData(doctorId));
                return lstPatientDetails;

            }
            catch (Exception)
            {               
                throw;
            }
        }

        public bool DeActivateaPatient(string patientId)
        {
            try
            {
                var objPatientDal = new AdminLteDAL.DoctorRegistration();
                var retVal = objPatientDal.DeActivateaPatient(patientId);
                return retVal;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool ReActivateaPatient(string patientId)
        {
            try
            {
                var objPatientDal = new AdminLteDAL.DoctorRegistration();
                var retVal = objPatientDal.ReActivateaPatient(patientId);
                return retVal;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public UserUniqueIdViewModel GetDoctorRequestDetails()
        {
            var objDoctorRequestDal = new AdminLteDAL.DoctorRegistration();
            var lstDoctorDetails = objDoctorRequestDal.GetAllDoctorRequests();
            var objObjectMapper = new ObjectMapper();
            var mappedDoctorDetails = objObjectMapper.MapOfflineLicensesRequests(lstDoctorDetails);
            var objUseruniqueidViewModel = new UserUniqueIdViewModel {OfllineLicensesRequest = mappedDoctorDetails};
            return objUseruniqueidViewModel;
        }

        public OfflineSerialViewModel GetOfflineSerialDetails()
        {
            var objDoctorRequestDal = new AdminLteDAL.DoctorRegistration();
            var lstDoctorDetails = objDoctorRequestDal.GetAllLicenseDetails();
            var objObjectMapper = new ObjectMapper();
            var mappedDoctorDetails = objObjectMapper.MapOfflineLicenseTracker(lstDoctorDetails);
            var objUseruniqueidViewModel = new OfflineSerialViewModel { OfflineSerialDetails = mappedDoctorDetails };
            return objUseruniqueidViewModel;
        }
    }

    public class ObjectMapper
    {
        public DoctorDetail MapDocDetails(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            var objEntityDoctordetail = new DoctorDetail
            {
                HospitalNme = objDoctorRegistration.HospitalName,
                FirstName = objDoctorRegistration.FirstName,
                MiddleName = objDoctorRegistration.MiddleName,
                LastName = objDoctorRegistration.LastName,
                Address1 = objDoctorRegistration.Address1,
                Address2 = objDoctorRegistration.Address2,
                country = objDoctorRegistration.Country,
                state = objDoctorRegistration.State,
                city = objDoctorRegistration.City,
                ZipCode = objDoctorRegistration.ZipCode,
                phone = objDoctorRegistration.Phone,
                mobileno = objDoctorRegistration.Mobileno,
                fax = objDoctorRegistration.Fax,
                email = objDoctorRegistration.Email,
                website = objDoctorRegistration.Website,
                DoctorCode = objDoctorRegistration.DoctorCode,
                Isactive = true,
                uniqueId = "",
                macId = objDoctorRegistration.SerialNumber,
                softwareKey = objDoctorRegistration.AuthenticationCode
            };

            return objEntityDoctordetail;
        }

        public AdminLteModels.DoctorRegistration MapDoctorCodeDetails(DoctorDetail objDoctorDetails)
        {
            var objModelDoctorCode = new AdminLteModels.DoctorRegistration
            {
                FirstName = objDoctorDetails.FirstName,
                LastName = objDoctorDetails.LastName,
                Email = objDoctorDetails.email
            };

            return objModelDoctorCode;
        }

        public List<uniqueID> MapLicenseDetails(List<AdminLteModels.GenerateUniqueId> lstGenUniqueid)
        {
            var lstMapLicenseDetails = lstGenUniqueid.Select(objUniqueid => new uniqueID
            {
                uniqueid1 = objUniqueid.Uniqueid,
                patiendid = objUniqueid.PatientId,
                offlineSerial = objUniqueid.OfflineSerialNumber,
                DoctorCode = objUniqueid.DoctorCode,
                isActive = true
            }).ToList();

            return lstMapLicenseDetails;
        }

        public AdminLteDAL.admin_Employee MapLoginDetails(AdminLteModels.LoginModel objLogin)
        {
            try
            {
                var objadminemployee = new admin_Employee
                {
                    userid = objLogin.Userid,
                    password = objLogin.Password
                };

                return objadminemployee;
            }

            catch
            {
                throw;
            }
        }

        public AdminLteDAL.admin_Employee MapUserEntryDetails(AdminLteModels.AdminFunctions objUser)
        {
            try
            {
                var bzfns = new BusinessFunctions();
                var objUserEntry = new admin_Employee
                {
                    userid = objUser.Emailaddress,
                    password = bzfns.EncryptText(objUser.Password),
                    firstname = objUser.Firstname,
                    middlename = objUser.Middlename,
                    lastname = objUser.Lastname,
                    addressline1 = objUser.Addressline1,
                    addressline2 = objUser.Addressline2,
                    country = objUser.Country,
                    state = objUser.State,
                    city = objUser.City,
                    pincode = objUser.Pincode,
                    telephone = objUser.Telephone,
                    mobileno = objUser.Mobileno,
                    emailaddress = objUser.Emailaddress,
                    isAdmin = objUser.IsAdmin,
                    isSuperAdmin = false,
                    isactive = true,
                    createdBy = objUser.CreatedBy,
                    isFirstLogin = true
                };

                return objUserEntry;
            }

            catch
            {
                throw;
            }
        }

        public AdminLteDAL.admin_Employee MapResetPassword(ResetPassword objResetPassword)
        {
            try
            {
             var bzfns = new BusinessFunctions();
                var objUserEntry = new admin_Employee
                {
                   
                    password = bzfns.EncryptText(objResetPassword.Password),
                    emailaddress = objResetPassword.Emailaddress,
                   
                };

                return objUserEntry;
            }

            catch
            {
                throw;
            }
        }

        public AdminLteDAL.Role MapRoleDetails(AdminLteModels.Roles objUser)
        {
            try
            {
                var objRoleEntry = new Role
                {
                    RoleName = objUser.RoleName,
                    RoleDescription = objUser.RoleDescription,
                    createdBy = objUser.createdBy,
                    createdDate = DateTime.Now,
                    isActive = true
                };

                return objRoleEntry;
            }

            catch
            {
                throw;
            }
        }

        //mapping of Entity to model here
        public List<AdminLteModels.Roles> MapRolesWithEntity(List<AdminLteDAL.Role> lstRoles)
        {
            try
            {
                var lstAdminRoles = lstRoles.Select(item => new AdminLteModels.Roles
                {
                    Roleid = item.Roleid,
                    RoleName = item.RoleName,
                    RoleDescription = item.RoleDescription,
                    isActive = item.isActive
                }).ToList();

                return lstAdminRoles;
            }

            catch
            {
                throw;
            }
        }

        //mapping of doctor registration list entity to model
        public List<AdminLteModels.DoctorRegistration> MapDoctorRegistrations(
            List<AdminLteDAL.DoctorDetail> lstDoctorDetails)
        {
            return lstDoctorDetails.Select(item => new AdminLteModels.DoctorRegistration
            {
                DoctorId = item.id, DoctorCode = item.DoctorCode, HospitalName = item.HospitalNme, FirstName = item.FirstName, MiddleName = item.MiddleName, LastName = item.LastName, City = item.city, State = item.state, Country = item.country, Phone = item.phone, Mobileno = item.mobileno, Email = item.email, Isactive = item.Isactive, IsFirstLogin = item.isFirstLogin, SubscriptionType = item.SubscriptionType, SubscriptionDate = item.SubscriptionDate, SerialNumber = item.macId, AuthenticationCode = item.softwareKey
            }).ToList();
        }

        public List<AdminLteModels.UniqueIds> MaUniqueIdses(List<AdminLteDAL.uniqueID> lstUniqueIds)
        {
            try
            {
                return lstUniqueIds.Select(item => new AdminLteModels.UniqueIds
                {
                    Uniqueid = item.uniqueid1, IsActive = item.isActive, Patiendid = item.patiendid, DoctorCode = item.DoctorCode
                }).ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<AdminLteModels.UserAuthentication> MapUserAuthentication(List<AdminLteDAL.userauthentication> lstUserauthentications)
        {
            try
            {
                return lstUserauthentications.Select(item => new AdminLteModels.UserAuthentication
                {
                    UserId = item.userId,
                    IsActive = item.isActive,
                    MacId = item.macID,
                    UniqueString = item.uniqueString
                }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DoctorDetail MapOnlineDoctorDetails(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            try
            {
                var objDoctordetails = new DoctorDetail
                {
                    HospitalNme = objDoctorRegistration.HospitalName,
                    FirstName = objDoctorRegistration.FirstName,
                    MiddleName = objDoctorRegistration.MiddleName,
                    LastName = objDoctorRegistration.LastName,
                    Address1 = objDoctorRegistration.Address1,
                    Address2 = objDoctorRegistration.Address2,
                    country = objDoctorRegistration.Country,
                    state = objDoctorRegistration.State,
                    city = objDoctorRegistration.City,
                    ZipCode = objDoctorRegistration.ZipCode,
                    phone = objDoctorRegistration.Phone,
                    mobileno = objDoctorRegistration.Mobileno,
                    fax = objDoctorRegistration.Fax,
                    email = objDoctorRegistration.Email,
                    website = objDoctorRegistration.Website,
                    DoctorCode = objDoctorRegistration.DoctorCode,
                    Isactive = true,
                    SubscriptionDate = objDoctorRegistration.SubscriptionDate,
                    SubscriptionType = objDoctorRegistration.SubscriptionType,
                    useragreementGenerated = true,
                };

                return objDoctordetails;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public SerialNumber MapGuidDetails(GenerateGuid objGenerateGuid)
        {
            try
            {
                var newGuid = new SerialNumber
                {
                    DoctorID = objGenerateGuid.DoctorCode,
                    Serial1 = objGenerateGuid.Guid1,
                    Serial2 = objGenerateGuid.Guid2,
                    Serial3 = objGenerateGuid.Guid3,
                    Serial4 = objGenerateGuid.Guid4,
                    Serial5 = objGenerateGuid.Guid5
                };

                return newGuid;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<PatientDetails> MapPatientDetails(List<AdminLteDAL.PatientRegistration> objPatientRegistrations)
        {
            try
            {
                return objPatientRegistrations.Select(item => new PatientDetails
                {
                    PatientId = item.patientID,
                    DoctorId = item.referredDoctor,
                    FirstName = item.firstname,
                    MiddleName = item.middlename,
                    LastName = item.lastname,
                    City = item.city,
                    State = item.state,
                    Country = item.country,
                    Email = item.email,
                    Mobile = item.mobile,
                    IsActive = item.isActive
                }).ToList();
            }
            catch (Exception)
            {           
                throw;
            }
        }

        public List<OfflineLicensesRequest> MapOfflineLicensesRequests(
            List<OfflineLicenseRequest> lstOfflineLicenseRequests)
        {
            return lstOfflineLicenseRequests.Select(item => new OfflineLicensesRequest
            {
                DoctorCode = item.doctorCode, LicenseQuantity = item.licenseQuantity, RequestActiveStatus = item.requestActiveStatus, RequestDate = item.requestdate, RequestId = item.requestId
            }).ToList();
        }

        public List<OfflineSerialDetails> MapOfflineLicenseTracker(
           List<OFFLINELICENSESTRACKER> lstOfflineLicenseRequests)
        {
            return lstOfflineLicenseRequests.Select(item => new OfflineSerialDetails
            {
                DoctorCode = item.DoctorCode,
                SerialKey = item.SerialKey,
                isActive = item.isActive,
                RequestId = Convert.ToString(item.requestId)
            }).ToList();
        }
    }
}
