using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AdminLteMvc.Controllers
{
    public class PatientController : Controller
    {
        //
        // GET: /Patient/
        public ActionResult GetPatientDetails()
        {
            return View();
        }

        public ActionResult ViewDoctorDetails()
        {
            return View();
        }

        public ActionResult GetDoctorDetails()
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstDoctorRegVals = objDocRegBal.GetDoctorRegistrations();
            return PartialView("_ViewDoctors", lstDoctorRegVals);
        }

        public ActionResult GetDoctorDetailsForPatients()
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstDoctorRegVals = objDocRegBal.GetDoctorRegistrations();
            return PartialView("_ViewDoctorForPatientDetails", lstDoctorRegVals);
        }


        public ActionResult GetPatientInformation(string doctorCode)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstUniqueIds = objDocRegBal.GetUniqueIdLists(doctorCode);
            return View("GetPatientInformation", lstUniqueIds);
        }

        [HttpPost]
        public ActionResult GetUserAuthenticationDetails(string uniqueId)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstUniqueIds = objDocRegBal.GetUserAuthenticationDetails(uniqueId);
            return PartialView("_UserAuthenticationDetails", lstUniqueIds);
        }

        [HttpPost]
        public JsonResult UpdateDoctorDetails(string userId)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var docRegDetails = objDocRegBal.ReActivatePatient(userId);
            return Json(docRegDetails);
        }

        public ActionResult ShowPatientDetails()
        {
            return View("ShowPatientInformation");
        }

        public ActionResult GetPatientDetailsForTransaction(string doctorCode)
        {
            Session["DoctorCode"] = doctorCode;
            var objPatientRegistration = new AdminLteBAL.DoctorRegistration();
            var lstPatientDetails = objPatientRegistration.GetPatientDetails(doctorCode);
            return View("PatientInformation", lstPatientDetails);
        }

        [HttpPost]
        public ActionResult GetPatientInformationforPatient(string doctorCode)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstUniqueIds = objDocRegBal.GetUniqueIdLists(doctorCode);
            return PartialView("_DetailedPatientView", lstUniqueIds);
        }

        [HttpPost]
        public ActionResult GetUserAuthenticationDetailsforPatient(string uniqueId)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var lstUniqueIds = objDocRegBal.GetUserAuthenticationDetails(uniqueId);
            return PartialView("_UserAuthenticationDetails", lstUniqueIds);
        }

        public JsonResult DeActivateaPatient(string patientId)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retValbal = objDocRegBal.DeActivateaPatient(patientId);
            return Json(retValbal);
        }

        public JsonResult ReActivateaPatient(string patientId)
        {
            var objDocRegBal = new AdminLteBAL.DoctorRegistration();
            var retValbal = objDocRegBal.ReActivateaPatient(patientId);
            return Json(retValbal);
        }
    }
}