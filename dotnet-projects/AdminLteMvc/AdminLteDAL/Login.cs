using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace AdminLteDAL
{
    public class Login
    {
        //creating db context object
        private TinnitusTrioEntities dbContext = new TinnitusTrioEntities();

        public string LoginUser(admin_Employee objEmployeeId)
        {
            try
            {
                //var DataVal = dbContext.admin_Employee.Where(x => (x.userid == objEmployeeId.userid) && (x.password == objEmployeeId.password) && (x.isactive == true));
                var DataVal = dbContext.admin_Employee.Where(x => (x.userid == objEmployeeId.userid && x.password == objEmployeeId.password && x.isactive == true));
                if(DataVal.Count()>0)
                {
                    var isFirstLogin = DataVal.Select(x => x.isFirstLogin).SingleOrDefault();
                    if(isFirstLogin.Equals(true))
                    {
                        return "isfirstlogin";
                    }

                    else
                    {
                        return "normalLogin";
                    }
                }

                else
                {
                    return "invalidUser";
                }

            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string InsertNewUser(admin_Employee objEmployeeId)
        {
            try
            {
                //check for duplicate user name
                var dataVal = dbContext.admin_Employee.Where(x => (x.userid == objEmployeeId.userid));

                if (dataVal.Any())
                {
                    return "DuplicateUserName";
                }
                else
                {
                    dbContext.admin_Employee.Add(objEmployeeId);
                    dbContext.SaveChanges();
                    return "Success";
                }

               
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ResetPassword(admin_Employee objAdminEmployee)
        {
            try
            {
                var dataVal = dbContext.admin_Employee.SingleOrDefault(x => (x.emailaddress == objAdminEmployee.emailaddress));

                if (dataVal == null) return "ResetFailed";
                dataVal.password = objAdminEmployee.password;
                dataVal.isFirstLogin = false;
                dbContext.SaveChanges();
                return "Success";
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {
               
                throw;
            }
        }

        public string InsertRole(Role objRole)
        {
            try
            {
                //check for duplicate user name
                var dataVal = dbContext.Roles.Where(x => (x.RoleName == objRole.RoleName));

                if (dataVal.Any())
                {
                    return "DuplicateRoleName";
                }
                else
                {
                    dbContext.Roles.Add(objRole);
                    dbContext.SaveChanges();
                    return "Success";
                }


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                var dataVal = dbContext.Roles.Where(x => x.isActive == true).ToList();
                return dataVal;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
