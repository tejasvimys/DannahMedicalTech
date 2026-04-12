using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoctorRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    /// <summary>
    /// Save into doctor details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var objDoctorRegistrationBo = new DoctorRegistrationBO();
            objDoctorRegistrationBo.FirstName = txtfirstnme.Text;
            objDoctorRegistrationBo.LastName = txtLastNme.Text;
            objDoctorRegistrationBo.MiddleName = txtMiddleNme.Text;
            objDoctorRegistrationBo.Address1 = txtAddress1.Text;
            objDoctorRegistrationBo.Address2 = txtAddress2.Text;
            objDoctorRegistrationBo.City = txtcity.Text;
            //objDoctorRegistrationBo.State = country.Text;
            //objDoctorRegistrationBo.Country = txtcountry.Text;
            objDoctorRegistrationBo.ZipCode = txtzip.Text;
            objDoctorRegistrationBo.Phone = txtphone.Text;
            objDoctorRegistrationBo.Mobileno = txtmobile.Text;
            objDoctorRegistrationBo.Fax = txtfax.Text;
            objDoctorRegistrationBo.Email = txtemail.Text;
            objDoctorRegistrationBo.Website = txtwebsite.Text;

            System.Guid desiredGuid = System.Guid.NewGuid();
            string substringdata = desiredGuid.ToString();

            var data = substringdata.Substring(0, 8);
            objDoctorRegistrationBo.UniqueId = data.ToString();
            objDoctorRegistrationBo.HospitalNme = txthospital.Text;
            objDoctorRegistrationBo.Appdemodate = txtDate.Text;

            objDoctorRegistrationBo.UseragreementGenerated = true;

            System.Guid guid1 = System.Guid.NewGuid();
            var substring1 = guid1.ToString();
            substring1 = substring1.Substring(0, 5);

            System.Guid guid2 = System.Guid.NewGuid();
            var substring2 = guid2.ToString();
            substring2 = substring2.Substring(0, 5);

            System.Guid guid3 = System.Guid.NewGuid();
            var substring3 = guid3.ToString();
            substring3 = substring3.Substring(0, 5);

            System.Guid guid4 = System.Guid.NewGuid();
            var substring4 = guid4.ToString();
            substring4 = substring4.Substring(0, 5);

            System.Guid guid5 = System.Guid.NewGuid();
            var substring5 = guid5.ToString();
            substring5 = substring5.Substring(0, 5);

            objDoctorRegistrationBo.Serial1 = substring1;
            objDoctorRegistrationBo.Serial2 = substring2;
            objDoctorRegistrationBo.Serial3 = substring3;
            objDoctorRegistrationBo.Serial4 = substring4;
            objDoctorRegistrationBo.Serial5 = substring5;

            var objDoctorRegistrationBl = new DoctorRegistrationBL();
            objDoctorRegistrationBl.InsertUserInformation(objDoctorRegistrationBo);
            
            cleardata();

        }
        catch (Exception)
        {
            
            throw;
        }
    }

   
    public void cleardata()
    {

        txthospital.Text = string.Empty;
        txtfirstnme.Text = string.Empty;
        txtMiddleNme.Text = string.Empty;
        txtLastNme.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        txtcity.Text = string.Empty;
        //txtstate.Text = string.Empty;
        //txtcountry.Text = string.Empty;
        txtzip.Text = string.Empty;
        txtphone.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtmobile.Text = string.Empty;
        txtfax.Text = string.Empty;
        txtwebsite.Text = string.Empty;
        txtDate.Text = string.Empty;

    }
}