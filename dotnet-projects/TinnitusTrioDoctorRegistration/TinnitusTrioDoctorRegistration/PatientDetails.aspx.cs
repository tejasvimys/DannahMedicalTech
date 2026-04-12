using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TinnitusTrioDoctorRegistration
{
    public partial class PatientDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearchClick_Click(object sender, EventArgs e)
        {
            try
            {
                if (optionsRadiosInline1.Checked == true)
                {
                    if (optionsRadiosInline3.Checked == true)
                    {
                        var clsobj = new TinnitusTrioBAL.DoctorDetails();
                        var txtPatientId = txtDoctorId.Text.Trim();
                        var dtObj = clsobj.GenerateAppInstallationReport("", "doctorid", "","");
                        dataTablesexample.DataSource = dtObj;
                        dataTablesexample.DataBind();
                        dataTablesexample.UseAccessibleHeader = true;
                        dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    else if (optionsRadiosInline4.Checked == true)
                    {
                        var clsobj = new TinnitusTrioBAL.DoctorDetails();
                        var txtPatientId = txtDoctorId.Text.Trim();
                        var txtTo = txtDateTo.Text.Trim();
                        var txtfrom = txtDateFrom.Text.Trim();
                        var dtObj = clsobj.GenerateAppInstallationReport("", "doctoridwithdate", txtfrom, txtTo);
                        dataTablesexample.DataSource = dtObj;
                        dataTablesexample.DataBind();
                        dataTablesexample.UseAccessibleHeader = true;
                        dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    else
                    {
                        var clsobj = new TinnitusTrioBAL.DoctorDetails();
                        var dtObj = clsobj.GenerateAppInstallationReport();
                        dataTablesexample.DataSource = dtObj;
                        dataTablesexample.DataBind();
                        dataTablesexample.UseAccessibleHeader = true;
                        dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }

                else if (optionsRadiosInline2.Checked == true)
                {
                    if (optionsRadiosInline3.Checked == true)
                    {
                        var clsobj = new TinnitusTrioBAL.DoctorDetails();
                        var txtPatientId = txtDoctorId.Text.Trim();
                        var dtObj = clsobj.GenerateAppInstallationReport(txtPatientId, "full");
                        dataTablesexample.DataSource = dtObj;
                        dataTablesexample.DataBind();
                        dataTablesexample.UseAccessibleHeader = true;
                        dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    else if (optionsRadiosInline4.Checked == true)
                    {
                        var clsobj = new TinnitusTrioBAL.DoctorDetails();
                        var txtPatientId = txtDoctorId.Text.Trim();
                        var txtTo = txtDateTo.Text.Trim();
                        var txtfrom = txtDateFrom.Text.Trim();
                        var dtObj = clsobj.GenerateAppInstallationReport("",txtPatientId, "patientid", txtfrom, txtTo);
                        dataTablesexample.DataSource = dtObj;
                        dataTablesexample.DataBind();
                        dataTablesexample.UseAccessibleHeader = true;
                        dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    else
                    {
                        var clsobj = new TinnitusTrioBAL.DoctorDetails();
                        var dtObj = clsobj.GenerateAppInstallationReport();
                        dataTablesexample.DataSource = dtObj;
                        dataTablesexample.DataBind();
                        dataTablesexample.UseAccessibleHeader = true;
                        dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }

                else
                {
                    
                        if (optionsRadiosInline3.Checked == true)
                        {

                        }

                        else if (optionsRadiosInline4.Checked == true)
                        {

                        }

                        else
                        {
                            var clsobj = new TinnitusTrioBAL.DoctorDetails();
                            var dtObj = clsobj.GenerateAppInstallationReport();
                            dataTablesexample.DataSource = dtObj;
                            dataTablesexample.DataBind();
                            dataTablesexample.UseAccessibleHeader = true;
                            dataTablesexample.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                    
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}