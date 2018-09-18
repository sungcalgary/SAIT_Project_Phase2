<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TravelExperts_Web.WebForm4" %>
<%--
    Author: Sunghyun, Marc
    Created: 2018-07

    Sunghyun created individual input elements
    Marc did overall styling
    --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    
    <div class="login-header">
        <h1 class="display-4">Log In</h1>
        <p>Enter your login details</p>
    </div>
    <div class="login-container">
        <div class="row">
            <asp:Label ID="lblId" runat="server" Text="Email Address"></asp:Label>
            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="row">
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>
        <div class="row">
            <asp:Label ID="lblError" runat="server" class="text-danger"></asp:Label>
        </div>
        <p></p>
        <p></p>
        <div class="row">
            <asp:Button ID="btnLogIn" runat="server" class="btn-success" Text="Log in" OnClick="btnLogIn_Click" Width="100%" />
        </div>

        <div class="row">
            <asp:Label ID="lblAsk" runat="server" ></asp:Label>
            <asp:Button ID="btnSignUp" runat="server" class="btn-outline-dark" Text="Join Now" Width="100%" PostBackUrl="~/registration.aspx" />
            <asp:Button ID="btnLogOff" runat="server" class="btn-outline-dark" Text="Log Off" CausesValidation="False" OnClick="btnLogOff_Click" />
        </div>
    </div>
</asp:Content>
    
