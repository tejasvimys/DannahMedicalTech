
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioBO
{
   public class BusinessObjects
    {
    }

   public class Login
   {
       public string UserId { get; set; }
       public string Password { get; set; }
       public string Pin { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
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
        public string SubscriptionDate { get; set; }
        public int SubscriptionType { get; set; }
    }
}
