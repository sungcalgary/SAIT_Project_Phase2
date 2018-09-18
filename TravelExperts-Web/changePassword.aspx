<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="changePassword.aspx.cs" Inherits="TravelExperts_Web.WebForm8" %>
<%--
    Author: Sunghyun Lee
    Created: 2018-07
    --%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div class="container">
    <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="password">Password</label>
                <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbPassword" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group col-sm-12">
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="tbPassword" Display="Dynamic" ErrorMessage="Password must be at least 8 characters long. Contains AT LEAST one lower case letter, one upper case letter, one number and one symbol." ValidationExpression="^.*(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[\W]).*$" CssClass="alert alert-danger"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="password-verify">Verify Password</label>
                <asp:TextBox ID="tbPasswordVerify" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ErrorMessage="Passwords you entered does not match!" CssClass="alert alert-danger" ControlToValidate="tbPasswordVerify" Display="Dynamic" ControlToCompare="tbPassword"></asp:CompareValidator>
            </div>
        </div>
        <div class="row">
            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-success" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-warning" CausesValidation="False" OnClick="btnCancel_Click" />
        </div>
        </div>
<asp:Label ID="lblError" runat="server" CssClass="text-danger" ></asp:Label>
</asp:Content>

