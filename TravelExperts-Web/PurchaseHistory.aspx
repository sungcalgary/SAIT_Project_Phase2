<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseHistory.aspx.cs" Inherits="TravelExperts_Web.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div class="PurchaseHistory-header">
    <h1 class="display-4">Your Purchase Records</h1>
    </div>

    <asp:Label ID="lblCustId" runat="server"></asp:Label>
    <br />
    <br />
    <asp:GridView class="table table-hover table-responsive" ID="gv" runat="server" GridLines="None">
    </asp:GridView>
    <br />
</asp:Content>
