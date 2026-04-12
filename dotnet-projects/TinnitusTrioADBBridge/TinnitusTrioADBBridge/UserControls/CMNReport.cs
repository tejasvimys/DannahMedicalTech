using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace TinnitusTrioADBBridge.UserControls
{
    public partial class CMNReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CMNReport(string patientid, string moduletype, string indate, string outdate, string patientName)
        {
            InitializeComponent();

            if (moduletype == "Full")
            {
                GenerateReport(patientid);
            }

            else
            {
                GenerateReport(patientid, indate, outdate);
            }


            lblPatientID.Text = patientid;
            lblDateTime.Text = DateTime.Now.ToShortDateString();
            lblPatientname.Text = patientName;
        }

        public void GenerateReport(string patientid)
        {
            try
            {
                xrTable1.Rows.Clear();

                var cmtobj = new TinnitusTrioADB_BAL.ReportGenerator();
                var cmtDs = cmtobj.GenerateCmnReport(patientid);

                var dt = cmtDs.Tables[0];
                var rowCnt = dt.Rows.Count;

                if (rowCnt <= 0) return;
                

                //generating Header Row
                var rowheader = new XRTableRow();
                var headercell = new XRTableCell();
                headercell.Text = "Log Date";
                rowheader.Cells.Add(headercell);
                headercell = new XRTableCell();
                headercell.Text = "Log Time";
                rowheader.Cells.Add(headercell);
                headercell = new XRTableCell();
                headercell.Text = "Duration H:M:S";
                rowheader.Cells.Add(headercell);

                xrTable1.Rows.Add(rowheader);

                var totalDuration = 0;

                for (var i = 0; i < rowCnt; i++)
                {
                    var row = new XRTableRow();

                    for (var j = 0; j < dt.Columns.Count; j++)
                    {

                        var rowVal = dt.Rows[i][j].ToString();

                        var newCell = new XRTableCell
                        {
                            Text = rowVal,
                            Borders = DevExpress.XtraPrinting.BorderSide.All
                        };

                        row.Cells.Add(newCell);
                    }

                    xrTable1.Rows.Add(row);
                }

                //get the sum total value, append and display in the last row
                var rowBlank = new XRTableRow();
                xrTable1.Rows.Add(rowBlank);
                var rowFooter = new XRTableRow();
                var sumRow = cmtDs.Tables[1].Rows[0][0].ToString() + " H-" + cmtDs.Tables[2].Rows[0][0].ToString() +
                             " M-" + cmtDs.Tables[3].Rows[0][0].ToString() + " S-";
                var nWCell = new XRTableCell
                {
                    Text = "",
                    Borders =
                              DevExpress.XtraPrinting.BorderSide.All
                };

                rowFooter.Cells.Add(nWCell);
                var nWCell1 = new XRTableCell
                {
                    Text = "Total Duration",
                    Borders =
                              DevExpress.XtraPrinting.BorderSide.All
                };
                rowFooter.Cells.Add(nWCell1);

                var nWCell2 = new XRTableCell
                {
                    Text = sumRow,
                    Borders =
                              DevExpress.XtraPrinting.BorderSide.All
                };
                rowFooter.Cells.Add(nWCell2);

                xrTable1.Rows.Add(rowFooter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GenerateReport(string patientid, string indate, string outdate)
        {
            try
            {
                xrTable1.Rows.Clear();

                var cmtobj = new TinnitusTrioADB_BAL.ReportGenerator();
                var cmtDs = cmtobj.GeerateCmnDateWiseReport(patientid, indate, outdate);

                var dt = cmtDs.Tables[0];
                var rowCnt = dt.Rows.Count;

                if (rowCnt <= 0) return;
               


                //generating Header Row
                var rowheader = new XRTableRow();
                var headercell = new XRTableCell();
                headercell.Text = "Log Date";
                rowheader.Cells.Add(headercell);
                headercell = new XRTableCell();
                headercell.Text = "Log Time";
                rowheader.Cells.Add(headercell);
                headercell = new XRTableCell();
                headercell.Text = "Duration";
                rowheader.Cells.Add(headercell);

                xrTable1.Rows.Add(rowheader);

               

                for (var i = 0; i < rowCnt; i++)
                {
                    var row = new XRTableRow();

                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        var rowVal = dt.Rows[i][j].ToString();
                        var newCell = new XRTableCell
                        {
                            Text = rowVal,
                            Borders =
                                      DevExpress.XtraPrinting.BorderSide.All
                        };
                        row.Cells.Add(newCell);
                    }

                    xrTable1.Rows.Add(row);
                }

                var rowBlank = new XRTableRow();
                xrTable1.Rows.Add(rowBlank);

                //get the sum total value, append and display in the last row
                var rowFooter = new XRTableRow();
                var sumRow = cmtDs.Tables[1].Rows[0][0].ToString() + " H-" + cmtDs.Tables[2].Rows[0][0].ToString() +
                             " M-" + cmtDs.Tables[3].Rows[0][0].ToString() + " S-";
                var nWCell = new XRTableCell
                {
                    Text = "",
                    Borders =
                              DevExpress.XtraPrinting.BorderSide.All 
                };

                rowFooter.Cells.Add(nWCell);
                var nWCell1 = new XRTableCell
                {
                    Text = "Total Duration",
                    Borders =
                              DevExpress.XtraPrinting.BorderSide.All
                };
                rowFooter.Cells.Add(nWCell1);

                var nWCell2 = new XRTableCell
                {
                    Text = sumRow,
                    Borders =
                              DevExpress.XtraPrinting.BorderSide.All
                };
                rowFooter.Cells.Add(nWCell2);

                xrTable1.Rows.Add(rowFooter);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
