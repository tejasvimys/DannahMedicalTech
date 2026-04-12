using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TinnitusTrioBAL;

namespace TinnitusTrioDoctorRegistration
{
    public partial class DoctorDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var docdetails = new TinnitusTrioBAL.DoctorDetails();
                var doctorTable = docdetails.DoctorDetailsTable();
                dataTablesexample.DataSource = doctorTable;
                dataTablesexample.DataBind();
                dataTablesexample.UseAccessibleHeader = true;

                dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}