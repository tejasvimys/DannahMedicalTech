using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DoctorRegistrationBL
/// </summary>
public class DoctorRegistrationBL
{
	public DoctorRegistrationBL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// Business layer to insert doctor details.
    /// </summary>
    /// <param name="objDoctorRegistrationBo"></param>
    /// <returns></returns>
    public string InsertUserInformation(DoctorRegistrationBO objDoctorRegistrationBo)
    {
        var DAL = new DoctorRegistrationDAL();
        return DAL.InsertUserInformation(objDoctorRegistrationBo);
    }
    /// <summary>
    /// Business layer function to check Login details and to check user first login
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="pin"></param>
    /// <returns></returns>
    public int LoginCheck(string username, string password,string pin)
    {
        try
        {
            DataTable dt;
            int result = 0;
            var objDoctorRegistrationDal = new DoctorRegistrationDAL();
            dt = objDoctorRegistrationDal.LoginCheck(username, password, pin);
            if (dt.Rows.Count > 0)
            {
                string isfirstlogin = dt.Rows[0][3].ToString();
                if (isfirstlogin == "True")
                    result = 1;
                else if (isfirstlogin == "False")
                    result = 2;
                
            }
            else
            {
                result = 0;
            }
            return result;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    public string UpdateLogin(LoginBO objLoginBo)
    {
        var DAL = new DoctorRegistrationDAL();
        return DAL.UpdateUserInformation(objLoginBo);
    }

    
 }