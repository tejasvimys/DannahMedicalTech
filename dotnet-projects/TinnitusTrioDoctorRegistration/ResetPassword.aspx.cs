using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResetPassword : System.Web.UI.Page
{
    string username;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPassword.Text != txtconfirmpassword.Text)
            {
                Label1.Text = "Password and Confirm Password should match";
                Label1.ForeColor = Color.Red;
            }
            else if (txtpin.Text != txtconfirmpin.Text)
            {
                Label1.Text = "Pin and Confirm Pin Should match";
                Label1.ForeColor = Color.Red;
            }
            else
            {
                username = Session["username"].ToString();
                var objLoginBo = new LoginBO();
                objLoginBo.UserName = username;
                objLoginBo.Password = txtPassword.Text;
                objLoginBo.Pin = txtpin.Text;
                var objDoctorRegistrationBl = new DoctorRegistrationBL();
                objDoctorRegistrationBl.UpdateLogin(objLoginBo);
                Label1.Text = "Updated Successfully";
                Label1.ForeColor = Color.Green;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}