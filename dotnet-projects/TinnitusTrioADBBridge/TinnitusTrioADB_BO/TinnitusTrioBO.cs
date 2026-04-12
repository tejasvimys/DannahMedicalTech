using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioADB_BO
{
    public class TinnitusTrioBO
    {
        public string PatientId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DoctorCode { get; set; }

    }

    public class LicenseManager
    {
        public string Serial1 { get; set; }
        public string Serial2 { get; set; }
        public string Serial3 { get; set; }
        public string Serial4 { get; set; }
        public string Serial5 { get; set; }
        public string RegistrarName { get; set; }
        public string RegistrarId { get; set; }
        public string IsActive { get; set; }
        public string DoctorCode { get; set; }
    }

    public class CountryState
    {
        public string Countryname { get; set; }
        public int Countryid { get; set; }
        public string State { get; set; }
        public int Stateid { get; set; }
    }

    public class Login
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public bool IsFirstLogin { get; set; }

    }

    public class DoctorLogging : TinnitusTrioBO
    {
        public string AppName { get; set; }
    }
}
