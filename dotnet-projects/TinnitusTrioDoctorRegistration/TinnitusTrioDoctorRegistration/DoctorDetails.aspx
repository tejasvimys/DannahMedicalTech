<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DoctorDetails.aspx.cs" Inherits="TinnitusTrioDoctorRegistration.DoctorDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
 <!-- DataTables CSS -->
   
    <link href="css/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="css/datatables/media/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="css/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        
        $(document).ready(function () {
            $(".myTable").DataTable({
                responsive: true
            });

          
        });
    </script>
    
    <style>
        #page-wrapper {
            width: 250%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Doctor Details
                        </h1>
            <ol class="breadcrumb">
                <li class="active">
                    <i class="fa fa-dashboard"></i><a href="DoctorDetails.aspx">Doctor Details</a>
                </li>
               

            </ol>
        </div>
        
    </div>
    
    <div class="row">
         <div class="col-lg-12">
             <asp:GridView ID="dataTablesexample" runat="server" CssClass="myTable"></asp:GridView>
             </div>
    </div>

</asp:Content>
