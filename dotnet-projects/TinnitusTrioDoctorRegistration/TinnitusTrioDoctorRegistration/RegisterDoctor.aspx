<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegisterDoctor.aspx.cs" Inherits="TinnitusTrioDoctorRegistration.RegisterDoctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery.js"></script>
     <script type="text/javascript" src="js/countries.js"></script>
    <script type="text/javascript" src="js/json2.js"></script>
    <script src="js/jqueryui.js"></script>
    <link href="css/jqueryui.css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward();
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
</script>
    <style>
        #ajaxBusy {
  display: none;
  margin: 0px 0px 0px -50px; /* left margin is half width of the div, to centre it */
  padding: 30px 10px 10px 10px;
  position: absolute;
  left: 30%;
  top: 325px;
  width: 100%;
  height: 100%;
  text-align: center;
  background: #e8e8e8 no-repeat center center;
  border: 1px solid #000;
}
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $('body').append('<div id="ajaxBusy"><img src="Images/Loading.gif"/></div>');
            $('#ajaxBusy').hide();

            $("#subUntil").hide();

            $("#txtSubscriptionTo").datepicker({ minDate: 0 });

            populateCountries("country", "state");

            function validateTextBox() {
                
                if ($("#txtHospitalName").val() == "") {

                    $("#divHospitalName").removeClass("form-group");
                    $("#divHospitalName").addClass("form-group has-error");
                    $("#errorMsg").show();
                    return false;
                }

                if ($("#txtFirstName").val() == "") {
                    $("#divFirstName").removeClass("form-group");
                    $("#divFirstName").addClass("form-group has-error");
                    $("#errorMsg").show();
                    return false;
                }

                if ($("#txtLastName").val() == "") {
                    $("#divLastName").removeClass("form-group");
                    $("#divLastName").addClass("form-group has-error");
                    $("#errorMsg").show();
                    return false;
                }

                if ($("#txtMobileNo").val() == "") {
                    $("#divMobileNo").removeClass("form-group");
                    $("#divMobileNo").addClass("form-group has-error");
                    $("#errorMsg").show();
                    return false;
                }

                if ($("#txtEmail").val() == "") {
                    $("#divEmailid").removeClass("form-group");
                    $("#divEmailid").addClass("form-group has-error");
                    $("#errorMsg").show();
                    return false;
                }

                var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
                if (filter.test($("#txtEmail").val())) {
                    return true;
                } else {
                    $("#divEmailid").removeClass("form-group");
                    $("#divEmailid").addClass("form-group has-error");
                    $("#errorMsg").show();
                    return false;
                }

                return true;
            }

            $("#successMsg").hide();
            $("#errorMsg").hide();

            //Reset functionality
            $("#btnReset").click(function () {
               
                $("#txtHospitalName").val("");
                $("#txtFirstName").val("");
                $("#txtMiddleName").val("");
                $("#txtLastName").val("");
                $("#txtAddressLine1").val("");
                $("#txtAddressLine2").val("");
                
                $("#txtCity").val("");
                $("#txtZipCode").val("");
                $("#txtTelephone").val("");
                $("#txtMobileNo").val("");
                $("#txtFax").val("");
                $("#txtEmail").val(""); 
                $("#txtWebsite").val("");
                $("#txtSubscriptionTo").val("");
                $("#subUntil").hide();

                $("#optionsRadiosInline2").click();
                $("#optionsRadiosInline1").click();
            });

            $("#optionsRadiosInline2").click(function() {
                $("#subUntil").show();
            });

            $("#optionsRadiosInline1").click(function () {
                $("#subUntil").hide();
            });

            $("#btnSave").click(function () {

                $('#ajaxBusy').show();

                var hospitalName = $("#txtHospitalName").val();
                var FirstName = $("#txtFirstName").val();
                var MiddleName = $("#txtMiddleName").val();
                var lastName = $("#txtLastName").val();
                var AddressLine1 = $("#txtAddressLine1").val();
                var AddressLine2 = $("#txtAddressLine2").val();
                var country = $("#country option:selected").text();
                var state = $("#state option:selected").text();
                var City = $("#txtCity").val();
                var ZipCode = $("#txtZipCode").val();
                var Telephone = $("#txtTelephone").val();
                var MobileNo = $("#txtMobileNo").val();
                var Fax = $("#txtFax").val();
                var Email = $("#txtEmail").val();
                var Website = $("#txtWebsite").val();

                var subscriptionType = 1;

                var subscriptionTo = $("#txtSubscriptionTo").val();

                if ($('#optionsRadiosInline2').is(':checked')) {
                    subscriptionType = 0;
                } else {
                    subscriptionType = 1;
                }

                //    $("#txtSubscriptionTo").val("");
                //$("#subUntil").hide();

                //$("#optionsRadiosInline2").click();
                //$("#optionsRadiosInline1").click();

                var jsonString = {};
                jsonString.HospitalName = hospitalName;
                jsonString.FirstName = FirstName;
                jsonString.MiddleName = MiddleName;
                jsonString.LastName = lastName;
                jsonString.Address1 = AddressLine1;
                jsonString.Address2 = AddressLine2;
                jsonString.City = City;
                jsonString.State = state;
                jsonString.Country = country;
                jsonString.ZipCode = ZipCode;
                jsonString.Phone = Telephone;
                jsonString.Mobileno = MobileNo;
                jsonString.Fax = Fax;
                jsonString.Email = Email;
                jsonString.Website = Website;

                jsonString.SubscriptionType = subscriptionType;
                jsonString.SubscriptionDate = subscriptionTo;

                var dto = { 'objDoctorRegistration': jsonString };

                if (validateTextBox()) {
                    $.ajax({
                        type: "POST",
                        url: "RegisterDoctor.aspx/SaveDoctorDetail",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(dto), // Check this call.
                        success: function(data) {
                            $("#successMsg").show();
                            $("#errorMsg").hide();
                            $('#ajaxBusy').hide();
                        },

                        error: function (data) {
                            $("#successMsg").hide();
                            $("#errorMsg").show();
                            $('#ajaxBusy').hide();
                        }
                    });
                }

            });
        });
    </script>
    
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <!-- Page Heading -->
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Register Doctor
                        </h1>
            <ol class="breadcrumb">
                <li class="active">
                    <i class="fa fa-dashboard"></i><a href="RegisterDoctor.aspx">Register Doctor</a>
                </li>

            </ol>
        </div>
    </div>
    <div class="row">
         <div id="successMsg" class="alert alert-success">
                    <strong>Success!</strong> Doctor Details Saved Successfully. An E-Mail has been sent to the Doctor and the Admin which contains the credentials!
                </div>
            
             <div id="errorMsg" class="alert alert-danger">
                    <strong>Oh snap!</strong> Change a few things up and try submitting again.
                </div>
        <div class="col-lg-6">
            
            <div id="divHospitalName" class="form-group">
                <label>Clinic/Hospital Name</label>
                <%--<asp:TextBox class="form-control" ID="txtHospitalName" runat="server"></asp:TextBox>--%>
                <input id="txtHospitalName" class="form-control" type="text" />
            </div>

            <div id="divFirstName" class="form-group">
                <label>First Name</label>
                <%--<asp:TextBox class="form-control" ID="txtFirstName" runat="server"></asp:TextBox>--%>
                 <input id="txtFirstName" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <label>Middle Name</label>
                <%--<asp:TextBox class="form-control" ID="txtMiddleName" runat="server"></asp:TextBox>--%>
                 <input id="txtMiddleName" class="form-control" type="text" />
            </div>

            <div id="divLastName" class="form-group">
                <label>Last Name</label>
                <%--<asp:TextBox class="form-control" ID="txtLastName" runat="server"></asp:TextBox>--%>
                 <input id="txtLastName" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <label>Address Line 1</label>
                <%--<asp:TextBox class="form-control" ID="txtAddressLine1" runat="server"></asp:TextBox>--%>
                 <input id="txtAddressLine1" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <label>Address Line 2</label>
                <%--<asp:TextBox class="form-control" ID="txtAddressLine2" runat="server"></asp:TextBox>--%>
                 <input id="txtAddressLine2" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <label>Country</label>
                 <select id="country"  class="form-control" name ="country"></select>
            </div>

            <div class="form-group">
                <label>State</label><select name ="state"  class="form-control" id ="state"></select>
            </div>

            <div class="form-group">
                <label>City</label>
                <%--<asp:TextBox class="form-control" ID="txtCity" runat="server"></asp:TextBox>--%>
                 <input id="txtCity" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <label>Zip Code</label>
                <%--<asp:TextBox class="form-control" ID="txtZipCode" runat="server"></asp:TextBox>--%>
                 <input id="txtZipCode" class="form-control" type="text" />
            </div>


        </div>

        <div class="col-lg-6">

            <div class="form-group">
                <label>Telephone</label>
                <%--<asp:TextBox class="form-control" ID="txtTelephone" runat="server"></asp:TextBox>--%>
                 <input id="txtTelephone" class="form-control" type="text" />
            </div>

            <div id="divMobileNo" class="form-group">
                <label>Mobile No</label>
                <%--<asp:TextBox class="form-control" ID="txtMobileNo" runat="server"></asp:TextBox>--%>
                 <input id="txtMobileNo" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <label>Fax</label>
                <%--<asp:TextBox class="form-control" ID="txtFax" runat="server"></asp:TextBox>--%>
                 <input id="txtFax" class="form-control" type="text" />
            </div>
            <div id="divEmailid" class="form-group">
                <label>E-Mail</label>
                <%--<asp:TextBox class="form-control" ID="txtEmail" runat="server"></asp:TextBox>--%>
                 <input id="txtEmail" class="form-control" type="text" />
            </div>
            <div class="form-group">
                <label>Website</label>
                <%--<asp:TextBox class="form-control" ID="txtWebsite" runat="server"></asp:TextBox>--%>
                 <input id="txtWebsite" class="form-control" type="text" />
            </div>
            
                <div class="form-group">
                 <label>License Type</label>
                                <label class="radio-inline">
                                    <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline1" value="option1" checked="">Full
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline2" value="option2">Subscription Based
                                </label>
                              
            </div>
           
            
             <div class="form-group" id="subUntil">
                <label>Subscription Until</label>
                <%--<asp:TextBox class="form-control" ID="txtWebsite" runat="server"></asp:TextBox>--%>
                 <input id="txtSubscriptionTo" class="form-control" type="text" />
            </div>

            <div class="form-group">
                <button type="button" id="btnSave" class="btn btn-lg btn-success">Save</button>
                <button type="button" id="btnReset" class="btn btn-lg btn-warning">Reset</button>
            </div>
        </div>
    </div>

</asp:Content>
