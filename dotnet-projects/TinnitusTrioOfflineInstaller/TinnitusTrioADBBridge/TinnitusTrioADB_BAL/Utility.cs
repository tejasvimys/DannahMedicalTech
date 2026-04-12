using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinnitusTrioADB_BO;
using TinnitusTrioADB_DAL;

namespace TinnitusTrioADB_BAL
{
    public class Utility
    {
        public List<CountryState> GetCountry()
        {
            try
            {
                var lstUtility = new TinnitusTrioADB_DAL.Utility();
                var lstCountry = lstUtility.GetCountry();
                return lstCountry;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<CountryState> GetStates(int countryid)
        {
            try
            {
                var lstUtility = new TinnitusTrioADB_DAL.Utility();
                var lstCountry = lstUtility.GetState(countryid);
                return lstCountry;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
