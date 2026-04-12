using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TinnitusTrioBO;

namespace TinnitusTrioDoctorRegistration
{
    public partial class Login : System.Web.UI.Page
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

                var retVal = objBal.CheckLogin(objTrioBo);

                if (retVal.Password.Equals("FIRSTLOGIN"))
                {
                    Response.Redirect("FirstLogin.aspx");
                }

                else if (retVal.Password.Equals("INVUSER"))
                {
                    AlertSpan.InnerText = "Username or Password or Pin Invalid! Please Check";
                }

                else if (retVal.Password.Equals("SUCCESS"))
                {
                    Session["FirstName"] = retVal.FirstName;
                    Session["LastName"] = retVal.LastName;
                    Response.Redirect("Dashboard.aspx");
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}