<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangeSubscription.aspx.cs" Inherits="TinnitusTrioDoctorRegistration.ChangeSubscription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <script src="js/jquery.js"></script>
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
    
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtSubscriptionTo").datepicker({ minDate: 0 });

            $("#subUntil").hide();

            $("#optionsRadiosInline2").click(function () {
                $("#subUntil").show();
            });

            $("#optionsRadiosInline1").click(function () {
                $("#subUntil").hide();
            });

            $("#btnSave").click(function() {
                var DoctorId = $("#txtDoctorId").val();
                var subscriptionTo = $("#txtSubscriptionTo").val();

                var subscriptionType = 1;

                var subscriptionTo = $("#txtSubscriptionTo").val();

                if ($('#optionsRadiosInline2').is(':checked')) {
                    subscriptionType = 0;
                } else {
                    subscriptionType = 1;
                }

                alert(subscriptionType);

                var jsonString = {};
                jsonString.DoctorCode = DoctorId;
                jsonString.SubscriptionDate = subscriptionTo;
                jsonString.SubscriptionType = subscriptionType;

                var dto = { 'objDoctorRegistration': jsonString };

                $.ajax({
                    type: "POST",
                    url: "ChangeSubscription.aspx/UpdateSubscriptionType",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(dto), // Check this call.
                    success: function (data) {

                        if (data.d == "1") {

                            
                            alert("Subscription Type Changed Successfully");
                        }

                    },

                    error: function (data) {
                        alert("Error");
                    }
                });
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
     <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Change Subscription Type
                        </h1>
            <ol class="breadcrumb">
                <li class="active">
                    <i class="fa fa-dashboard"></i><a href="ChangeSubscription.aspx">Change Subscription Type</a>
                </li>

            </ol>
        </div>
    </div>
    
     <div class="col-lg-6">
          
           <div id="divDoctorName" class="form-group">
                <label>Enter Doctor ID</label>
                <input id="txtDoctorId" class="form-control" type="text" />
            </div>
         
         <div class="form-group">
                 <label>License Type</label>
                                <label class="radio-inline">
                                    <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline1" value="option1" checked>Full
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="optionsRadiosInline" id="optionsRadiosInline2" value="option2">Subscription Based
                                </label>
                              
            </div>
          
           
             <div class="form-group" id="subUntil">
                <label>Extension of Subscription Until</label>
                <%--<asp:TextBox class="form-control" ID="txtWebsite" runat="server"></asp:TextBox>--%>
                 <input id="txtSubscriptionTo" class="form-control" type="text" />
            </div>
          
            <div class="form-group">
                <button type="button" id="btnSave" class="btn btn-lg btn-success">Save</button>
                <button type="button" id="btnReset" class="btn btn-lg btn-warning">Reset</button>
            </div>

          </div>

</asp:Content>
