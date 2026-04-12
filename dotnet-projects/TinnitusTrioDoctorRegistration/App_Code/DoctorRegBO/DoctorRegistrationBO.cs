using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoctorRegistrationBO
/// </summary>
public class DoctorRegistrationBO
{
    public DoctorRegistrationBO()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Supritha
    /// Creating business objects for Doctor Registration.
    /// </summary>
    public int id { get; set; }
    public string FirstName { get; set; }
    public string Serial1 { get; set; }

    public string Serial2 { get; set; }

    public string Serial3 { get; set; }

    public string Serial4 { get; set; }

    public string Serial5 { get; set; }
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

    public string UniqueId { get; set; }

    public string HospitalNme { get; set; }

    public string Appdemodate { get; set; }

    public bool UseragreementGenerated { get; set; }

    public bool Isactive { get; set; }
}