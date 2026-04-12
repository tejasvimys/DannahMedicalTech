using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLteBAL;
using AdminLteModels;
using DoctorRegistration = AdminLteMvc.Models.DoctorRegistration;

namespace AdminLteMvc.Controllers
{
    public class DoctorRegistrationController : Controller
    {
        //
        // GET: /DoctorRegistration/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult InsertDoctor(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retVal = objDocRegBal.InsertDoctorRegistration(objDoctorRegistration);
            return Json(retVal);
        }

        public JsonResult InsertOnlineDoctor(AdminLteModels.DoctorRegistration objDoctorRegistration)
        {
            var objDocreg = new AdminLteBAL.DoctorRegistration();
            var retVal = objDocreg.GetDoctorDetails(objDoctorRegistration);
            return Json(retVal);

        }

        public ActionResult OnlineDoctorReg()
        {
            return View();
        }

        public ActionResult GetDoctorDetails()
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstDoctorRegVals = objDocRegBal.GetDoctorRegistrations();
            return PartialView("_DoctorDetails", lstDoctorRegVals);
        }

        public ActionResult ViewDoctorDetails()
        {
            return View();
        }

        public ActionResult DoctorStatusResults(string id)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retVal = objDocRegBal.DeActivateDoctor(Convert.ToInt32(id));
            return View("ViewDoctorDetails");
        }

        public ActionResult ReactivateDoctorStatus(string id)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retVal = objDocRegBal.ReactivateDoctor(Convert.ToInt32(id));
            return View("ViewDoctorDetails");
        }
       
	}
}