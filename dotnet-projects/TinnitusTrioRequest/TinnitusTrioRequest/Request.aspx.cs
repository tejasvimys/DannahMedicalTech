using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TinnitusTrioRequest
{
    public partial class Request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

        }

        public void clear()
        {
            txtdoctorcode.Text = string.Empty;
            txtremarks.Text = string.Empty;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
          
            using (var dbcontext = new TinnitusTrioEntities())
            {
                var retVal = dbcontext.DoctorDetails.FirstOrDefault(x => x.DoctorCode == txtdoctorcode.Text.Trim());
                var retstatus = dbcontext.OfflineLicenseRequests.FirstOrDefault(x => (x.requestActiveStatus == true) && (x.doctorCode == retVal.DoctorCode));
                if (retstatus != null && retstatus.requestActiveStatus == true)
                {
                    string script = "<script type=\"text/javascript\">alert('There is an active request,kindly wait or contact administrator Tinnitus Trio');</script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                    clear();
                }
                if (retstatus == null)
                {
                    var objofflineRequestor = new OfflineLicenseRequest
                    {
                        doctorCode = txtdoctorcode.Text,
                        licenseQuantity = Convert.ToInt32(drplicenceqty.SelectedItem.ToString()),
                        remarks = txtremarks.Text,
                        requestActiveStatus = true,
                        requestisserviced = false,
                        requestdate = DateTime.Now
                    };
                    dbcontext.OfflineLicenseRequests.Add(objofflineRequestor);
                    dbcontext.SaveChanges();
                    clear();
                    string script = "<script type=\"text/javascript\">alert('Request has been raised successfully!!');</script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                }
               else
                {

                    string script = "<script type=\"text/javascript\">alert('Doctor Code Already Exists');</script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                    clear();
                }


            }
        }
    }
}