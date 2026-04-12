<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TinnitusTrioRequest.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <address>
        Tinnitus Trio Clinic<br />
        Mysore<br />
        <abbr title="Phone">P:</abbr>
        9897454544
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">TinnitusTrio@gmail.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">TinnitusTrio@gmail.com</a>
    </address>
</asp:Content>
