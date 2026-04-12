using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

/// <summary>
/// Summary description for DoctorRegistrationDAL
/// </summary>
public class DoctorRegistrationDAL
{
	public DoctorRegistrationDAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string conection = (ConfigurationManager.ConnectionStrings["TrioConnection"].ConnectionString);

    /// <summary>
    /// DAL Function to insert doctor details.
    /// </summary>
    /// <param name="ObjDoctorRegistrationBO"></param>
    /// <returns></returns>
    public string InsertUserInformation(DoctorRegistrationBO ObjDoctorRegistrationBO)
    {
        SqlConnection con = new SqlConnection(conection);
        con.Open();
        SqlCommand cmd = new SqlCommand("SP_InsertDoctorRegistrationdetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@FirstName", ObjDoctorRegistrationBO.FirstName);
            cmd.Parameters.AddWithValue("@MiddleName", ObjDoctorRegistrationBO.MiddleName);
            cmd.Parameters.AddWithValue("@LastName", ObjDoctorRegistrationBO.LastName);
            cmd.Parameters.AddWithValue("@Address1", ObjDoctorRegistrationBO.Address1);
            cmd.Parameters.AddWithValue("@Address2", ObjDoctorRegistrationBO.Address2);
            cmd.Parameters.AddWithValue("@city", ObjDoctorRegistrationBO.City);
            cmd.Parameters.AddWithValue("@state", ObjDoctorRegistrationBO.State);
            cmd.Parameters.AddWithValue("@country", ObjDoctorRegistrationBO.Country);
            cmd.Parameters.AddWithValue("@ZipCode", ObjDoctorRegistrationBO.ZipCode);
            cmd.Parameters.AddWithValue("@phone", ObjDoctorRegistrationBO.Phone);
            cmd.Parameters.AddWithValue("@mobileno", ObjDoctorRegistrationBO.Mobileno);
            cmd.Parameters.AddWithValue("@fax", ObjDoctorRegistrationBO.Fax);
            cmd.Parameters.AddWithValue("@email", ObjDoctorRegistrationBO.Email);
            cmd.Parameters.AddWithValue("@website", ObjDoctorRegistrationBO.Website);
            cmd.Parameters.AddWithValue("@uniqueId", ObjDoctorRegistrationBO.UniqueId);
            cmd.Parameters.AddWithValue("@HospitalNme", ObjDoctorRegistrationBO.HospitalNme);
            cmd.Parameters.AddWithValue("@appdemodate", ObjDoctorRegistrationBO.Appdemodate);
            cmd.Parameters.AddWithValue("@useragreementGenerated", ObjDoctorRegistrationBO.UseragreementGenerated);
            cmd.Parameters.AddWithValue("@Serial1", ObjDoctorRegistrationBO.Serial1);
            cmd.Parameters.AddWithValue("@Serial2", ObjDoctorRegistrationBO.Serial2);
            cmd.Parameters.AddWithValue("@Serial3", ObjDoctorRegistrationBO.Serial3);
            cmd.Parameters.AddWithValue("@Serial4", ObjDoctorRegistrationBO.Serial4);
            cmd.Parameters.AddWithValue("@Serial5", ObjDoctorRegistrationBO.Serial5);
            cmd.ExecuteNonQuery();
          
            string strMessage = "Doctor Details Inserted";
            con.Close();
            return strMessage;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
    }

    /// <summary>
    /// DAL function to check user Login details and check isfirst Login of the user.
    /// </summary>
    /// <param name="Username"></param>
    /// <param name="Password"></param>
    /// <param name="Pin"></param>
    /// <returns></returns>
    public DataTable LoginCheck(string Username, string Password, string Pin)
    {
        try
        {
            var con = new SqlConnection(conection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_loginCheck", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1 = new SqlParameter("@UserName", Username);
            SqlParameter param2 = new SqlParameter("@Password", Password);
            SqlParameter param3 = new SqlParameter("@pin", Pin);

            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
     
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            // data table Logic
            DataTable dt;
            dt = ds.Tables[0];
            return dt;

        }
        catch (Exception)
        {
           throw;
        }
    }
    /// <summary>
    /// Function to update Login table details
    /// </summary>
    /// <param name="objLoginBo"></param>
    /// <returns></returns>
    public string UpdateUserInformation(LoginBO objLoginBo)
    {
        SqlConnection con = new SqlConnection(conection);
        con.Open();
        SqlCommand cmd = new SqlCommand("UpdateLogindetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@UserName", objLoginBo.UserName);
            cmd.Parameters.AddWithValue("@Password", objLoginBo.Password);
            cmd.Parameters.AddWithValue("@Pin", objLoginBo.Pin);
            cmd.ExecuteNonQuery();
            string strMessage = "Updated";
            con.Close();
            return strMessage;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

    }

   }