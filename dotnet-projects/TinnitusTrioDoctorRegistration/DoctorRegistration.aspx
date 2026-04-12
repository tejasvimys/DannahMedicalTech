<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorRegistration.aspx.cs" Inherits="DoctorRegistration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Admin</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
   
    <link href="./css/style.css" rel="stylesheet">
    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet">
    
<link type="text/css" href="css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="js/countries.js"></script>

    
   <script type="text/javascript">
       function validate() {
           var firstnme, lastnme, middlenme, addrs1, adds2, city, state, country, zip, phone, mobile, fax, email, website, hospital, appdemodate;
           var emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([com\co\.\in])+$/;
           firstnme = document.getElementById('<%=txtfirstnme.ClientID %>').value;
           lastnme = document.getElementById('<%=txtLastNme.ClientID %>').value;
           middlenme = document.getElementById('<%=txtMiddleNme.ClientID %>').value;
           addrs1 = document.getElementById('<%=txtAddress1.ClientID %>').value;
           adds2 = document.getElementById('<%=txtAddress2.ClientID %>').value;
           city = document.getElementById('<%=txtcity.ClientID %>').value;
           <%--state = document.getElementById('<%=txtstate.ClientID %>').value;
           country = document.getElementById('<%=txtcountry.ClientID %>').value;--%>
           zip = document.getElementById('<%=txtzip.ClientID %>').value;
           phone = document.getElementById('<%=txtphone.ClientID %>').value;
           mobile = document.getElementById('<%=txtmobile.ClientID %>').value;
           fax = document.getElementById('<%=txtfax.ClientID %>').value;
           email = document.getElementById('<%=txtemail.ClientID %>').value;
         
           website = document.getElementById('<%=txtwebsite.ClientID %>').value;
           hospital = document.getElementById('<%=txthospital.ClientID %>').value;
           appdemodate = document.getElementById('<%=txtDate.ClientID %>').value;
           if (hospital == "") {
               alert("Enter Hospital Name");
               return false;
           }
           if (firstnme == "") {
               alert("Enter First Name");
               return false;
           }
           if (lastnme == "") {
               alert("Enter Last Name");
               return false;
           }
           if (middlenme == "") {
               alert("Enter Middle Name");
               return false;
           }
           if (addrs1 == "") {
               alert("Enter address1");
               return false;
           }
           if (adds2 == "") {
               alert("Enter address2");
               return false;
           }
           if (firstnme == "") {
               alert("Enter First Name");
               return false;
           }
           if (city == "") {
               alert("Enter city name");
               return false;
           }
           if (state == "") {
               alert("Enter state Name");
               return false;
           }
           if (country == "") {
               alert("Enter country Name");
               return false;
           }
           if (zip == "") {
               alert("Enter zip code");
               return false;
           }
           if (phone == "") {
               alert("Enter phone number");
               return false;
           }
           if (email == "") {
               alert("Enter email Id");
               return false;
           }
           if (!email.match(emailExp)) {
               alert("Invalid Email Id");
               return false;
           }
           if (website == "") {
               alert("Enter website Name");
               return false;
           }
           
           if (appdemodate == "") {
               alert("choose Date");
               return false;
           }

       }
   </script>

   <style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style>
    
    
<script type="text/javascript">
    $(function () {
        $("#txtDate").datepicker();
    });
</script>
    
     <link href="./css/style.css" rel="stylesheet"/>
<%--<script src="./js/jquery.min.js"></script>--%>
<%--<script src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>
<script src="./js/jquery.geocomplete.min.js"></script>
<script>
    $(function () {
        $("#location").geocomplete({
            details: ".geo-details",
            detailsAttribute: "data-geo"
        });

    });</script>--%>
    
    
    <style>
        img
        {
            width:250px;
        }
    </style>
 
    
    <script type="text/javascript">
    $( document ).ready(function() {
        populateCountries("country", "state");
    });
        </script>
</head>
<body>
    

    <form id="form2" runat="server">
       
       <%-- <div id="countries" runat="server">
            Select Country (with states):   <select id="country" name ="country"></select>
Select State: <select name ="state" id ="state"></select>
 <script language="javascript">
     
 </script>--%>
            
       <%-- </div>--%>

        <div id="wrapper">

            <!-- Page Content -->
            <div id="page-wrapper">
                <div class="row">
                    <div class="col-lg-12">
                        
                        <h1 class="page-header">Doctor-Registration</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <div class="row">
                               <div class="col-lg-6">
                        <div class="form-group">
                            <label>Hospital/Clinic Name</label>
                            <asp:TextBox ID="txthospital" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>First Name</label>
                            <asp:TextBox ID="txtfirstnme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Middle Name</label>
                            <asp:TextBox ID="txtMiddleNme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Last Name</label>
                            <asp:TextBox ID="txtLastNme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Address1</label>
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Address2</label>
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                     
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>City</label>
                            <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Country</label>
                           <%-- <asp:TextBox ID="txtcountry" runat="server" CssClass="form-control"></asp:TextBox>
                           --%>
                            <%--<select id="country" name ="country"></select>--%>
                            <asp:DropDownList ID="country" name ="country" runat="server"></asp:DropDownList>

                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>State</label>
                           <%-- <asp:TextBox ID="txtstate" runat="server" CssClass="form-control"></asp:TextBox>--%>
                            <%--<select name ="state" id ="state"></select>--%>
                             <asp:DropDownList ID="state" name ="country" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>ZipCode</label>
                            <asp:TextBox ID="txtzip" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Phone</label>
                            <asp:TextBox ID="txtphone" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Mobile Number </label>
                            <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Email Id </label>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Fax</label>
                            <asp:TextBox ID="txtfax" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Website</label>
                            <asp:TextBox ID="txtwebsite" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Demo Date</label>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClientClick="return validate();"
                            CssClass="btn btn-lg btn-success btn-block" onclick="btnSave_Click" />
                    </div>
                </div>
            </div>
            <!-- /#page-wrapper -->

        </div>


        <!-- jQuery Version 1.11.0 -->
        <%--<script src="js/jquery-1.11.0.js"></script>--%>

        <!-- Bootstrap Core JavaScript -->
        <script src="js/bootstrap.min.js"></script>

        <!-- Metis Menu Plugin JavaScript -->
        <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

        <!-- Custom Theme JavaScript -->
        <script src="js/sb-admin-2.js"></script>

        <%--<script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>--%>
        
    </form>
</body>
</html>
