using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TinnitusTrioBO;
using TinnitusTrioBAL;

namespace TinnitusTrioDoctorRegistration
{
    public partial class RegisterDoctor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string SaveDoctorDetail(DoctorRegistration objDoctorRegistration)
        {
            try
            {
                var objbusiness = new TinnitusTrioBAL.DoctorDetails();
                var retVal = objbusiness.GetDoctorDetails(objDoctorRegistration);
                return retVal;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}