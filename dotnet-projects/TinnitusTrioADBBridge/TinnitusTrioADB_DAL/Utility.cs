using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;

namespace TinnitusTrioADB_DAL
{
   public  class Utility
    {
       public List<CountryState> GetCountry()
       {
            var countryList = new List<CountryState>();
           try
           {
               var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
               var sqlConnection = new SqlConnection(connectionString);
               sqlConnection.Open();

               var sqlCommand = new SqlCommand("GetCountry", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               var reader = sqlCommand.ExecuteReader();


               while (reader.Read())
               {
                   var objCountry = new CountryState
                   {
                       Countryid = Convert.ToInt32(reader[0].ToString()),
                       Countryname = reader[1].ToString()
                   };
                   countryList.Add(objCountry);
               }

           }

           catch (Exception)
           {

               throw;
           }

           return countryList;
       }

       public List<CountryState> GetState(int countryId)
       {
           var countryList = new List<CountryState>();
           try
           {
               var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
               var sqlConnection = new SqlConnection(connectionString);
               sqlConnection.Open();

               var sqlCommand = new SqlCommand("GetState", sqlConnection)
               {
                   CommandType = CommandType.StoredProcedure
               };
               sqlCommand.Parameters.AddWithValue("@CountryId", countryId);
               var reader = sqlCommand.ExecuteReader();


               while (reader.Read())
               {
                   var objCountry = new CountryState
                   {
                       Stateid = Convert.ToInt32(reader[0].ToString()),
                       State = reader[1].ToString()
                   };
                   countryList.Add(objCountry);
               }

           }

           catch (Exception)
           {

               throw;
           }

           return countryList;
       }
    }
}
