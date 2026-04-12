using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AdminLteMvc.Controllers
{
    public class SubscriptionController : Controller
    {
        //
        // GET: /Subscription/
        public ActionResult Index()
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retval = objDocRegBal.GetDoctorRequestDetails();
            return View("Index", retval);
        }

        [HttpPost]
        public JsonResult GetDoctorDetails(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var objDoctorDetails = objDocRegBal.GetDoctorCodeRegistrationDetails(objDoctorRegistration.DoctorCode);
            if (objDoctorDetails != null)
            {
                var json = new JavaScriptSerializer().Serialize(objDoctorDetails);
                return Json(json);
            }

            else
            {
                return Json("");
            }
            
        }

        public JsonResult ActivateLicenses(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retVal = objDocRegBal.GenerateLicenses(objDoctorRegistration,
                Convert.ToInt32(objDoctorRegistration.Email));

            return Json(retVal);
        }

        public ActionResult SubscriptionDetails()
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retval = objDocRegBal.GetOfflineSerialDetails();
            return View("SubscriptionDetails", retval);
        }

	}
}