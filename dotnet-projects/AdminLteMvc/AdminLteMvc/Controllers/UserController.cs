using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLteModels;
using AdminLteBAL;

namespace AdminLteMvc.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult CreateUser(AdminFunctions objAdminFunctions)
        {
            objAdminFunctions.Userid = objAdminFunctions.Emailaddress;
            objAdminFunctions.Password = "tinnitusTrio@786";

            var objBal = new Login();
            var retVal = objBal.InsertNewUser(objAdminFunctions);
            if (retVal.Equals("DuplicateUserName"))
            {
                ModelState.AddModelError("Username", "The User Name Exists. Please Check!");
            }

            else
            {
                ModelState.Clear();
                ModelState.AddModelError("Success", "Employee Added Successfully");
            }
            return View("User");
        }

        public ActionResult User()
        {
            return View();
        }

        public ActionResult ViewUser()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult CreateRoles(Roles objRoles)
        {
            var objBal = new Login();
            var retVal = objBal.InsertRole(objRoles);
            if (retVal.Equals("DuplicateRoleName"))
            {
                ModelState.AddModelError("Username", "The Role Name Exists. Please Check!");
            }

            else
            {
                ModelState.Clear();
                ModelState.AddModelError("Success", "Role Added Successfully");
            }
            
            return View("Roles");
        }

        public ActionResult GetRoles()
        {
            var objBal = new Login();
            var retVal = objBal.GetRoles();
            return PartialView("RolesList", retVal);
        }

        public ActionResult UserRoleMapping()
        {
            return View();
        }
	}
}