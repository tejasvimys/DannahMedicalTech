using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminLteBusinessFunctions;
using AdminLteModels;
using AdminLteDAL;

namespace AdminLteBAL
{
    public class Login
    {
        public string UserLogin(LoginModel objLogin)
        {
            try
            {
                var objAdminDal = new AdminLteDAL.Login();
                var mapperobj = new ObjectMapper();

                var mappedObj = mapperobj.MapLoginDetails(objLogin);
                var bzfs = new BusinessFunctions();

                mappedObj.password = bzfs.EncryptText(mappedObj.password);
               

                var strLogin = objAdminDal.LoginUser(mappedObj);

                return strLogin;
            }

            catch
            {
                throw;
            }
        }

        public string InsertNewUser(AdminFunctions objEmployeeId)
        {
            try
            {
                objEmployeeId.Password = "tinnitusTrio@786";
                var objAdminDal = new AdminLteDAL.Login();
                var mapperobj = new ObjectMapper();

                var mappedObj = mapperobj.MapUserEntryDetails(objEmployeeId);

                var strInsertUser = objAdminDal.InsertNewUser(mappedObj);

                if (strInsertUser.Equals("Success"))
                {
                    var objBusinessfunctions = new BusinessFunctions();
                    objBusinessfunctions.SendEmployeeEmail(objEmployeeId.Emailaddress, objEmployeeId.Password);
                }

                return strInsertUser;
            }

            catch
            {
                throw;
            }
        }

        public string ResetPassword(ResetPassword objEmployeeId)
        {
            var objAdminDal = new AdminLteDAL.Login();
            var mapperobj = new ObjectMapper();

            var mappedObj = mapperobj.MapResetPassword(objEmployeeId);
            var strInsertUser = objAdminDal.ResetPassword(mappedObj);
            return strInsertUser;
        }

        public string InsertRole(Roles objRoles)
        {
            try
            {
               
                var objAdminDal = new AdminLteDAL.Login();
                var mapperobj = new ObjectMapper();

                var mappedObj = mapperobj.MapRoleDetails(objRoles);

                var strInsertUser = objAdminDal.InsertRole(mappedObj);

                return strInsertUser;
            }

            catch
            {
                throw;
            }
        }

        public List<Roles> GetRoles()
        {
            try
            {

                var objAdminDal = new AdminLteDAL.Login();
                var mapperRole = new ObjectMapper();
                var lstRoles = mapperRole.MapRolesWithEntity(objAdminDal.GetRoles());
                return lstRoles;
            }

            catch
            {
                throw;
            }
        }
    }
}
