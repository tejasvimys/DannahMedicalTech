using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrioBO;
using log4net;

namespace TrioDAL
{
   public class ClsGetUniqueId
   {
       private TrioConnection _connection = null;
       private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       public DataSet GetUniqueId(TrioBusinessObject objBusinessObject)
       {

           _connection = new TrioConnection();
           var conString = _connection.GetConnectionString();
           var sqlConnection = new SqlConnection(conString);
           var cmdSql = new SqlCommand("getUniqueID", sqlConnection)
           {
               CommandType = CommandType.StoredProcedure
           };

           try
           {
               cmdSql.Parameters.Add(new SqlParameter("@UniqueID", SqlDbType.Int, 30));
               cmdSql.Parameters["@UniqueID"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@UniqueID"].Value = Convert.ToInt32(objBusinessObject.UniqueId);

               cmdSql.Parameters.Add(new SqlParameter("@RecordCount", SqlDbType.Int));
               cmdSql.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

               var da = new SqlDataAdapter(cmdSql);
               var ds = new DataSet();
               da.Fill(ds);
               return ds;
           }
           catch (Exception ex)
           {
               Log.Error("Error in DAL: " + ex);
               throw;
           }

           finally
           {
               sqlConnection.Close();
               cmdSql.Dispose();
               cmdSql.Parameters.Clear();
           }
       }

       public DataSet GetUniqueId1()
       {

           _connection = new TrioConnection();
           var conString = _connection.GetConnectionString();
           var sqlConnection = new SqlConnection(conString);
           var cmdSql = new SqlCommand("getUniqueID", sqlConnection)
           {
               CommandType = CommandType.StoredProcedure
           };

           try
           {

               var da = new SqlDataAdapter(cmdSql);
               var ds = new DataSet();
               da.Fill(ds);
               return ds;
           }
           catch (Exception ex)
           {
               Log.Error("Error in DAL: " + ex);
               throw;
           }

           finally
           {
               sqlConnection.Close();
               cmdSql.Dispose();
               cmdSql.Parameters.Clear();
           }
       }

       public string InsertPatientDetails(TrioUseridObject objBusinessObject)
       {

           _connection = new TrioConnection();
           var conString = _connection.GetConnectionString();
           var sqlConnection = new SqlConnection(conString);
           var cmdSql = new SqlCommand("insertDevicedetails", sqlConnection)
           {
               CommandType = CommandType.StoredProcedure
           };

           var retVal = "";

           try
           {
               cmdSql.Parameters.Add(new SqlParameter("@UniqueID", SqlDbType.VarChar, 30));
               cmdSql.Parameters["@UniqueID"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@UniqueID"].Value = (objBusinessObject.UniqueId);

               cmdSql.Parameters.Add(new SqlParameter("@macid", SqlDbType.VarChar, 30));
               cmdSql.Parameters["@macid"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@macid"].Value = (objBusinessObject.Macid);

               cmdSql.Parameters.Add(new SqlParameter("@serialno", SqlDbType.VarChar, 30));
               cmdSql.Parameters["@serialno"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@serialno"].Value =(objBusinessObject.SerialNo);

               cmdSql.Parameters.Add(new SqlParameter("@hashString", SqlDbType.VarChar, 300));
               cmdSql.Parameters["@hashString"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@hashString"].Value = (objBusinessObject.UniqueString);

           sqlConnection.Open();

               using (var reader = cmdSql.ExecuteReader())
               {
                   if (reader.Read())
                   {
                       var successfunction = reader[0];
                      retVal = successfunction.ToString();
                       
                   }
               }

           }
           catch (Exception ex)
           {
               Log.Error("Error in DAL: " + ex);
               throw;
           }

           finally
           {
               sqlConnection.Close();
               cmdSql.Dispose();
               cmdSql.Parameters.Clear();
           }
           return retVal;

       }

       public string SyncLogFiles(string xmlString, string module)
       {
           _connection = new TrioConnection();
           var conString = _connection.GetConnectionString();
           var sqlConnection = new SqlConnection(conString);
           var cmdSql = new SqlCommand("XMLLogSync", sqlConnection)
           {
               CommandType = CommandType.StoredProcedure
           };

           var retVal = "";

           try
           {
               cmdSql.Parameters.AddWithValue("@XML", xmlString);
               cmdSql.Parameters.AddWithValue("@Module", module);

               sqlConnection.Open();

               using (var reader = cmdSql.ExecuteReader())
               {
                   if (reader.Read())
                   {
                       var successfunction = reader[0];
                       retVal = successfunction.ToString();

                   }
               }

           }
           catch (Exception ex)
           {
               Log.Error("Error in DAL: " + ex);
               throw;
           }

           return retVal;
       }

       //checking the active status of the apps
       public string CheckActiveStatus(string patientid)
       {
           _connection = new TrioConnection();
           var conString = _connection.GetConnectionString();
           var sqlConnection = new SqlConnection(conString);
           var cmdSql = new SqlCommand("checkActiveStatus", sqlConnection)
           {
               CommandType = CommandType.StoredProcedure
           };

           var retVal = "";

           try
           {
               cmdSql.Parameters.AddWithValue("@uniqueid", patientid);
             

               sqlConnection.Open();

               using (var reader = cmdSql.ExecuteReader())
               {
                   if (reader.Read())
                   {
                       var successfunction = reader[0];
                       retVal = successfunction.ToString();

                   }
               }

           }
           catch (Exception ex)
           {
               Log.Error("Error in DAL: " + ex);
               throw;
           }

           return retVal;
       }

       public string CheckPatientPhoneCreds(TrioUseridObject objBusinessObject)
       {

           _connection = new TrioConnection();
           var conString = _connection.GetConnectionString();
           var sqlConnection = new SqlConnection(conString);
           var cmdSql = new SqlCommand("AuthUser", sqlConnection)
           {
               CommandType = CommandType.StoredProcedure
           };

           var retVal = "";

           try
           {
               cmdSql.Parameters.Add(new SqlParameter("@UniqueID", SqlDbType.Int, 30));
               cmdSql.Parameters["@UniqueID"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@UniqueID"].Value = Convert.ToInt32(objBusinessObject.UniqueId);

               cmdSql.Parameters.Add(new SqlParameter("@macid", SqlDbType.VarChar, 30));
               cmdSql.Parameters["@macid"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@macid"].Value = (objBusinessObject.Macid);

               cmdSql.Parameters.Add(new SqlParameter("@serialno", SqlDbType.VarChar, 30));
               cmdSql.Parameters["@serialno"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@serialno"].Value = (objBusinessObject.SerialNo);

               cmdSql.Parameters.Add(new SqlParameter("@hashString", SqlDbType.VarChar, 300));
               cmdSql.Parameters["@hashString"].Direction = ParameterDirection.Input;
               cmdSql.Parameters["@hashString"].Value = (objBusinessObject.UniqueString);

               sqlConnection.Open();

               using (var reader = cmdSql.ExecuteReader())
               {
                   if (reader.Read())
                   {
                       var successfunction = reader[0];
                       retVal = successfunction.ToString();

                   }
               }

           }
           catch (Exception ex)
           {
               Log.Error("Error in DAL: " + ex);
               throw;
           }

           finally
           {
               sqlConnection.Close();
               cmdSql.Dispose();
               cmdSql.Parameters.Clear();
           }
           return retVal;

       }
    }
}
