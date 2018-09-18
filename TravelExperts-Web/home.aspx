<%--Threaded Project 2
    OOSD Spring August 2018
    By: Marc Javier--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="TravelExperts_Web.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div class="home-header">
        <h1 class="display-4">Welcome to TravelExperts</h1>
        <p class="lead">Spend less and travel more with our low fares.</p>
        <asp:Button ID="btnRegister" runat="server" Text="SIGN UP NOW" CausesValidation="False" CssClass="btn-primary btn-lg" PostBackUrl="~/registration.aspx" />
    </div>
    <section>
        <div class="home-content">
        <img src="images/travel-deals.PNG" />
        </div>
    </section>
</asp:Content>