using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrioDAL
{
    public class TrioConnection
    {
        public string GetConnectionString()
        {
            try
            {
                var constr = ConfigurationManager.ConnectionStrings["DBTinnitusTrio"].ConnectionString;

                return constr;
            }

            catch (Exception ex)
            {
                // ignored
                return ex.ToString();
            }
        }
    }
}
