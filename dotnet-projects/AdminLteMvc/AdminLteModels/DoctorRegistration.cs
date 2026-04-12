using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLteModels
{
    public class DoctorRegistration
    {
        public string HospitalName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Mobileno { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string DoctorCode { get; set; }
        public string SerialNumber { get; set; }
        public string AuthenticationCode { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public int DoctorId { get; set; }
        public bool? IsFirstLogin { get; set; }
        public int? SubscriptionType { get; set; }
        public bool Isactive { get; set; }

    }
    public class GenerateGuid
    {
        public string Guid1 { get; set; }
        public string Guid2 { get; set; }
        public string Guid3 { get; set; }
        public string Guid4 { get; set; }
        public string Guid5 { get; set; }
        public string DoctorCode { get; set; }
    }

    public class GenerateUniqueId : DoctorRegistration
    {
        public string Uniqueid { get; set; }
        public string PatientId { get; set; }
        public int DoctorSerial { get; set; }
        public int OfflineSerialNumber { get; set; }
    }

    public class User
    {
        public string Userid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class OfflineLicensesRequest
    {
        public int RequestId { get; set; }
        public string DoctorCode { get; set; }
        public bool? RequestActiveStatus { get; set; }
        public int? LicenseQuantity { get; set; }
        public DateTime? RequestDate { get; set; }
        
    }

    public class UserUniqueIdViewModel
    {
        public User GetUser { get; set; }
        public GenerateUniqueId UniqueIdGeneration { get; set; }
        public List<OfflineLicensesRequest> OfllineLicensesRequest { get; set; }
    }

    public class OfflineSerialViewModel
    {
        public User GetUser { get; set; }
        public List<OfflineSerialDetails> OfflineSerialDetails { get; set; }
    }

    public class UniqueIds
    {
        public string Uniqueid { get; set; }
        public bool? IsActive { get; set; }
        public string Patiendid { get; set; }
        public string DoctorCode { get; set; }
        public int? OfflineSerial { get; set; }
    }

    public class UserAuthentication
    {
        public string UserId { get; set; }
        public string MacId { get; set; }
        public string SerialNo { get; set; }
        public string UniqueString { get; set; }    
        public bool? IsActive { get; set; }
    }

    public class OfflineSerialDetails
    {
        public string DoctorCode { get; set;}
        public string RequestId { get; set; }
        public string SerialKey { get; set; }
        public bool? isActive { get; set; }
    }
}
