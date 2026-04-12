<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstLogin.aspx.cs" Inherits="TinnitusTrioDoctorRegistration.FirstLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tinnitus Trio Login</title>
    
    <link rel='stylesheet prefetch' href='css/FontStyleAwesome.css'/>

    <link rel="stylesheet" href="css/LoginStyle.css"/>
    
    <script src='js/jquery.js'></script>

        <script src="js/LoginJS.js"></script>

</head>
<body>
    <form id="form1" runat="server">
     <div class="login-form">
     <img alt="Tinnitus Trio" src="Images/500x300.png" width="300" height="100" style="margin-bottom: 20px;"/>
     <div class="form-group log-status">
         <asp:TextBox ID="UserName" runat="server" class="form-control" placeholder="Username" ></asp:TextBox>
     </div>
     <div class="form-group log-status1"> 
          <asp:TextBox ID="password" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
     </div>
         
         <div class="form-group log-status1"> 
          <asp:TextBox ID="checkpassword" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
     </div>
         
          <div class="form-group log-status2"> 
          <asp:TextBox ID="pin" runat="server" class="form-control" placeholder="Pin" TextMode="Password" MaxLength="4"></asp:TextBox>
     </div>
           <div class="form-group log-status2"> 
          <asp:TextBox ID="checkpin" runat="server" class="form-control" placeholder="Pin" TextMode="Password" MaxLength="4"></asp:TextBox>
     </div>
      <span id="AlertSpan" class="alert" runat="server">Invalid Credentials</span>
      <a class="link" href="#">Lost your password?</a>
         
         <asp:Button ID="btnLogin" runat="server" Text="Log in" CausesValidation="true" class="log-btn" OnClick="btnLogin_Click" /> 
   </div>
    </form>
</body>
</html>
