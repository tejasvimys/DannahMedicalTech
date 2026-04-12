using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioBO;

namespace TinnitusTrioBAL
{
   public class AdminLogin
    {
       public TinnitusTrioBO.Login CheckLogin(Login objLogin)
       {
           try
           {
               var objDal = new TinnitusTrioDAL.AdminLogin();

               var retVal = objDal.CheckLogin(objLogin);

               if (retVal.Password.Equals("FIRSTLOGIN"))
               {
                   return retVal;
               }

               else if (retVal.Password.Equals("INVUSER"))
               {
                   return retVal;
               }

               else
               {
                   var objTrioBo = new Utilities();

                   retVal.Password = objTrioBo.DecryptText(retVal.Password, objLogin.Pin);

                   if (retVal.Password.Equals(objLogin.Password))
                   {
                       retVal.Password = "SUCCESS";
                       return retVal;
                   }
               }

               return retVal;

           }
           catch (Exception)
           {               
               throw;
           }
       }

       public string UpdatePassword(Login objLogin)
       {
           try
           {
               var AesEncrypt = new Utilities();

               objLogin.Password = AesEncrypt.EncryptText(objLogin.Password, objLogin.Pin);

               var objDal = new TinnitusTrioDAL.AdminLogin();
               var retVal = objDal.UpdatePassword(objLogin);
               return retVal;
           }
           catch (Exception)
           {
               throw;
           }
       }
    }
}
