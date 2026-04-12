using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinnitusTrioDoctorWinService
{
   public class WinService
    {
       public string ResetDoctorDetails()
       {
           var retVal = "";

           var connectionString = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

           var sqlConnection = new SqlConnection(connectionString);
           sqlConnection.Open();
           try
           {
               var sqlCommand = new SqlCommand("spCheckActivationStatusDoctoronDate", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               

               var reader = sqlCommand.ExecuteReader();

               while (reader.Read())
               {
                   var readerval = reader[0].ToString();

                   return readerval;

               }
           }
           catch (Exception ex)
           {

               throw;
           }

           finally
           {
               sqlConnection.Close();
               sqlConnection.Dispose();
           }

           return retVal;
       }
    }
}
