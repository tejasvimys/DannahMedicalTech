using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TinnitusTrioDoctorRegistration
{
    public partial class FirstLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
             
            try
            {
                var username = UserName.Text.Trim();
                var pwd = password.Text.Trim();
                var pincode = pin.Text.Trim();

                var objTrioBo = new TinnitusTrioBO.Login {UserId = username, Pin = pincode, Password = pwd};
                var objBal = new TinnitusTrioBAL.AdminLogin();

                var retVal = objBal.UpdatePassword(objTrioBo);

                 if (retVal.Equals("SUCCESS"))
                {
                    Response.Redirect("Dashboard.aspx");
                }

                else
                {
                    AlertSpan.InnerText = "Username or Password or Pin Invalid! Please Check";
                   
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}