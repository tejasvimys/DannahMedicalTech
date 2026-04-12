using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLteBAL;

namespace AdminLteMvc.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(AdminLteModels.LoginModel objAdminfunctions)
        {
            if (!ModelState.IsValid) return View("Login");
            var objAdminBal = new AdminLteBAL.Login();
            
            var retVal = objAdminBal.UserLogin(objAdminfunctions);
            Session["AdminUser"] = objAdminfunctions.Userid;

            if (retVal.Equals("isfirstlogin"))
            {
                return RedirectToAction("ResetPassword", "Account");
            }

            else if (retVal.Equals("normalLogin"))
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }

            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View("Login");
            }
        }
	}
}