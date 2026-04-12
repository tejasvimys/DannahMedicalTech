using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    /// <summary>
    /// Function to Login user based on firstlogin.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       var objDoctorRegistrationBl = new DoctorRegistrationBL();
        int result = 0;
        result =  objDoctorRegistrationBl.LoginCheck(txtUsername.Text, txtPassword.Text, txtpin.Text);
        if (result == 1)
        {
            var username = txtUsername.Text;
            Session["username"] = username;
            Response.Redirect("ResetPassword.aspx"); 
        }
        else if(result == 2)
        {
            Response.Redirect("Home.aspx"); 
        }
        else if(result == 0)
        {
            Response.Redirect("Login.aspx");
        }
            
    }
}