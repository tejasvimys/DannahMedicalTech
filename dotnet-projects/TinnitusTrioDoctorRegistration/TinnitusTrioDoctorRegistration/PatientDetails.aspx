<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PatientDetails.aspx.cs" Inherits="TinnitusTrioDoctorRegistration.PatientDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="css/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="css/datatables/media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="css/datatables/media/js/jquery.dataTables.min.js"></script>
    
    <link href="css/jqueryui.css" rel="stylesheet" />
    <script src="js/jqueryui.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= txtDoctorId.ClientID %>").hide();

            $("#<%= optionsRadiosInline1.ClientID %>").click(function () {
                $("#<%= txtDoctorId.ClientID %>").hide();
            });

            $("#<%= optionsRadiosInline2.ClientID %>").click(function () {
                $("#<%= txtDoctorId.ClientID %>").show();
            });

            $("#fromGrp").hide();

            $("#toGrp").hide();

            $("#<%= optionsRadiosInline3.ClientID %>").click(function () {
                $("#fromGrp").hide();
                $("#toGrp").hide();
             });

            $("#<%= optionsRadiosInline4.ClientID %>").click(function () {
                $("#fromGrp").show();
               $("#toGrp").show();
            });

            $("#<%= txtDateFrom.ClientID %>").datepicker();

            $("#<%= txtDateTo.ClientID %>").datepicker();

            $(".myTable").DataTable({
                responsive: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row">
        <div class="col-lg-12">
           
            <ol class="breadcrumb">
                <li class="active">
                    <i class="fa fa-dashboard"></i><a href="PatientDetails.aspx">App Installation History and Patient Details</a>
                </li>
               

            </ol>
        </div>
    </div>
    
    <div class="row">
        <div class="col-lg-12">
         <div class="panel panel-yellow">
                            <div class="panel-heading">
                                <h3 class="panel-title">App installation History</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-4">
                                    
                                      <div class="form-group">
                                <label>Report Type</label>
                                          
                                          

                                          
                                <label class="radio-inline">
                                    <%--<input type="radio" name="optionsRadiosInline" id="optionsRadiosInline1" value="option1">Full--%>
                                    <asp:RadioButton ID="optionsRadiosInline1" value="option1" GroupName="optionsRadiosInline" runat="server"/> Full
                                     
                                </label>
                                <label class="radio-inline">
                                   <%-- <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline2" value="option2">--%>
                                    <asp:RadioButton ID="optionsRadiosInline2" value="option2" GroupName="optionsRadiosInline" runat="server"/>Doctor ID Specific
                                </label>
                                
                            </div>

                                     <div class="form-group">
                                        <asp:TextBox ID="txtDoctorId" runat="server" CssClass="form-control" placeholder="Enter Doctor ID"></asp:TextBox>
                                         

                            </div>
                                   

                                </div>
                                <div class="col-lg-4">
                                    
                                    
                                      <div class="form-group">
                                <label>Report Duration</label>
                                <label class="radio-inline">
                                   <%-- <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline3" value="option3">--%>
                                    <asp:RadioButton ID="optionsRadiosInline3" value="option3" GroupName="optionsRadiosInline1" runat="server"/>Full
                                </label>
                                <label class="radio-inline">
                                    <%--<input type="radio" name="optionsRadiosInline" id="optionsRadiosInline4" value="option4">--%>
                                    <asp:RadioButton ID="optionsRadiosInline4" value="option4" GroupName="optionsRadiosInline1" runat="server"/>Between Dates
                                </label>
                                
                            </div>
                                    
                                    <div class="form-group" id="fromGrp">
                                <label>From</label>
                                <asp:TextBox ID="txtDateFrom" CssClass="form-control" runat="server"></asp:TextBox>
                               
                            </div>
                                    
                                    <div class="form-group" id="toGrp">
                                <label>To</label>
                                <asp:TextBox ID="txtDateTo" CssClass="form-control" runat="server"></asp:TextBox>
                                
                            </div>

                                </div>
                                 <div class="col-lg-4"><asp:Button ID="Button1" CssClass="btn btn-lg btn-success" OnClick="btnSearchClick_Click" runat="server" Text="Search" /></div>
                            </div>
                        </div>
            
            </div>
    </div>
    
     <div class="row">
         <div class="col-lg-12">
             <asp:GridView ID="dataTablesexample" runat="server" CssClass="myTable"></asp:GridView>
             </div>
    </div>

</asp:Content>
