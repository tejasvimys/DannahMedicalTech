using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TinnitusTrioDoctorRegistration
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);

            AutoRedirect();

        }

        public void AutoRedirect()
        {

            var intMilliSecondsTimeOut = (this.Session.Timeout*60000);

            var strScript = @"

   <script type='text/javascript'> 

    intervalset = window.setInterval('Redirect()'," + intMilliSecondsTimeOut.ToString(CultureInfo.InvariantCulture) +
                            @");

    function Redirect()

    {

       alert('Your session has been expired. Redirecting to Login Page!\n\n');

       window.location.href='login.aspx'; 

    }

</script>";
           
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", strScript);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["FirstName"] == null || Session["LastName"] == null)
            {
                Response.Redirect("Login.aspx");
                
            }

            else
            {
                Label1.Text = Session["FirstName"].ToString() + " " + Session["LastName"].ToString();
            }
        }

        protected void logout_click(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}