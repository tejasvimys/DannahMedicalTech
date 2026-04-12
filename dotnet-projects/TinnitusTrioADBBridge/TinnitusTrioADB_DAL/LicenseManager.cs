using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_DAL
{
    public class LicenseManager
    {
        public TinnitusTrioADB_BO.LicenseManager ManageLicenses(TinnitusTrioADB_BO.LicenseManager objTinnitusTrioBo)
        {
            TinnitusTrioADB_BO.LicenseManager objLicenseManager = null;
            var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("serialValidation", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@serial1", objTinnitusTrioBo.Serial1);
                sqlCommand.Parameters.AddWithValue("@serial2", objTinnitusTrioBo.Serial2);
                sqlCommand.Parameters.AddWithValue("@serial3", objTinnitusTrioBo.Serial3);
                sqlCommand.Parameters.AddWithValue("@serial4", objTinnitusTrioBo.Serial4);
                sqlCommand.Parameters.AddWithValue("@serial5", objTinnitusTrioBo.Serial5);
                sqlCommand.Parameters.AddWithValue("@DoctorID", 0);
               

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var readerval = reader[0].ToString();

                    if (String.Equals(readerval, "INVUSR"))
                    {
                        objLicenseManager = new TinnitusTrioADB_BO.LicenseManager
                        {
                            RegistrarName = reader[0].ToString(),
                        };
                    }

                    else
                    {
                        objLicenseManager = new TinnitusTrioADB_BO.LicenseManager
                        {
                            RegistrarName = reader[0].ToString(),
                            RegistrarId = reader[1].ToString(),
                            DoctorCode = reader[2].ToString()
                        };
                    }
 
                    return objLicenseManager;
                }

            }
            catch (Exception)
            {

                objLicenseManager = new TinnitusTrioADB_BO.LicenseManager {RegistrarName = "Error"};
                return objLicenseManager;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return objLicenseManager;
        }

        public TinnitusTrioADB_BO.LicenseManager CheckLicense()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";

            var objlicensemanager = new TinnitusTrioADB_BO.LicenseManager();

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("LicenseCheck", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                
                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                   objlicensemanager.IsActive = reader[0].ToString();
                   objlicensemanager.RegistrarName = reader[1].ToString();
                   objlicensemanager.RegistrarId = reader[2].ToString();
                    //activeStatus = CheckForInternetConnection() ? CheckActiveStatus(objlicensemanager.RegistrarId) : "ACTIVE";

                    if (CheckForInternetConnection())
                    {
                        var activeStatus = CheckActiveStatus(objlicensemanager.RegistrarId);

                        if (activeStatus != "INACTIVE")
                        {
                            Activate();
                            objlicensemanager.IsActive ="True";
                            return objlicensemanager;
                        }

                        else
                        {
                            DeActivate();
                            objlicensemanager.IsActive = "INACTIVE";
                            objlicensemanager.RegistrarName = "INACTIVE";
                            objlicensemanager.RegistrarId = reader[2].ToString();
                            return objlicensemanager;
                        }
                    }

                    else
                    {
                        var actStat = CheckActiveStatusApplevel();

                         if (actStat != "INACTIVE")
                         {
                             Activate();
                             objlicensemanager.IsActive = "True";
                             return objlicensemanager;
                         }

                         else
                         {
                             DeActivate();
                             objlicensemanager.IsActive = "INACTIVE";
                             objlicensemanager.RegistrarName = "INACTIVE";
                             objlicensemanager.RegistrarId = reader[2].ToString();
                             return objlicensemanager;
                         }
                    }

                    //if (activeStatus != "INACTIVE")
                    //{
                    //    var actStat = CheckActiveStatusApplevel();
                    //    if (actStat != "INACTIVE")
                    //    {
                    //        Activate();
                    //        return objlicensemanager;
                    //    }

                    //    else
                    //    {
                    //        DeActivate();
                    //        objlicensemanager.IsActive = "INACTIVE";
                    //        objlicensemanager.RegistrarName = "INACTIVE";
                    //        objlicensemanager.RegistrarId = reader[2].ToString();
                    //        return objlicensemanager;
                    //    }
                        
                    //}

                    //else
                    //{
                    //    DeActivate();
                    //    objlicensemanager.IsActive = "INACTIVE";
                    //    objlicensemanager.RegistrarName = "INACTIVE";
                    //    objlicensemanager.RegistrarId = reader[2].ToString();
                    //    return objlicensemanager;
                    //}

                   //return objlicensemanager;
                }

            }
            catch (Exception ex)
            {
                objlicensemanager.IsActive = "0";
                return objlicensemanager;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return objlicensemanager;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public string CheckActiveStatus(string doctorId)
        {
            var retVal = "";

            var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("CheckActiveStatusDoctor", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@doctorid", doctorId);

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var readerval = reader[0].ToString();

                    retVal = String.Equals(readerval, "INACTIVE") ? "INACTIVE" : "ACTIVE";
                }
            }
            catch (Exception ex)
            {

                retVal = "ERROR";
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return retVal;
        }

        public string CheckActiveStatusApplevel()
        {
            var retVal = "";

            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("CheckActiveStatus", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var readerval = reader[0].ToString();

                    retVal = String.Equals(readerval, "INACTIVE") ? "INACTIVE" : "ACTIVE";
                }
            }
            catch (Exception ex)
            {

                retVal = "ERROR";
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return retVal;
        }

        public string RegisterDoctor(string doctorid, string doctorname, string doctorcode)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            //var connectionString = MediaTypeNames.Application.StartupPath  +"\\TEST.MDF";

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("InsertLicenseDetails", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.Parameters.AddWithValue("@doctorName", doctorname);
                sqlCommand.Parameters.AddWithValue("@doctorId", doctorid);
                sqlCommand.Parameters.AddWithValue("@doctorCode", doctorcode);

                sqlCommand.ExecuteReader();

                return "1";


            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return "";
        }

        public void DeActivate()
        {
            var retVal = "";

            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("DeActivateDoctor", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }

        public void Activate()
        {
            var retVal = "";

            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("ActivateDoctor", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }

        public string GetDoctorCode()
        {
            var LocalconnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            var sqlLocalConnection = new SqlConnection(LocalconnectionString);
            sqlLocalConnection.Open();
            try
            {
                var sqlCommand = new SqlCommand("OfflineGetDoctorCode", sqlLocalConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = sqlCommand.ExecuteReader();
                //get the licenses which are active from the database and check if the licenses are present for the user. if not, get the licenses for the user
                var doctorcode = "";
                while (reader.Read())
                {
                    doctorcode = reader[0].ToString();
                }

                return doctorcode;
            }

            catch
            {
                throw;
            }

            finally
            {
                sqlLocalConnection.Close();
                sqlLocalConnection.Dispose();
            }
        }
    }
}
