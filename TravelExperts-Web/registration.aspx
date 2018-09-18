<%--Threaded Project 2
    OOSD Spring August 2018
    By: Marc Javier--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="TravelExperts_Web.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="server">

    <div class="registration-header">
        <h1 class="display-4">Join TravelExperts</h1>
        <p>Open up a world of rewards every time you travel. Enjoy reward flights, upgrades, exclusive benefits and more.</p>
    </div>

    <div class="reg-container">
        <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="first-name">First Name</label>
                <asp:TextBox ID="tbFirstName" runat="server" MaxLength="25" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFname" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbFirstName" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group col-sm-5">
                <label for="last-name">Last Name</label>
                <asp:TextBox ID="tbLastName" runat="server" MaxLength="25" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLname" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbLastName" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="city">Address</label>
                <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbAddress" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
            </div>
        </div>


        <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="city">City</label>
                <asp:TextBox ID="tbCity" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCity" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbCity" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group col-sm-3">
                <label for="province">Province</label>
                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control">
                    <asp:ListItem Value="AB">Alberta</asp:ListItem>
                    <asp:ListItem Value="BC">British Columbia</asp:ListItem>
                    <asp:ListItem Value="MB">Manitoba</asp:ListItem>
                    <asp:ListItem Value="NB">New Brunswick</asp:ListItem>
                    <asp:ListItem Value="NL">Newfoundland and Labrador</asp:ListItem>
                    <asp:ListItem Value="NS">Nova Scotia</asp:ListItem>
                    <asp:ListItem Value="ON">Ontario</asp:ListItem>
                    <asp:ListItem Value="PE">Prince Edward Island</asp:ListItem>
                    <asp:ListItem Value="QC">Quebec</asp:ListItem>
                    <asp:ListItem Value="SK">Saskatchewan</asp:ListItem>
                    <asp:ListItem Value="NT">Northwest Territories</asp:ListItem>
                    <asp:ListItem Value="NU">Nunavut</asp:ListItem>
                    <asp:ListItem Value="YT">Yukon</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-sm-3">
                <label for="country">Country</label>
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                    <asp:ListItem Selected="True">Canada</asp:ListItem>
                    <asp:ListItem Enabled="False">USA</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-sm-5">
                <label for="postal-code">Postal Code</label>
                <asp:TextBox ID="tbPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPostalCode" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbPostalCode" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPostalCode" runat="server" ControlToValidate="tbPostalCode" CssClass="alert alert-danger" Display="Dynamic" ErrorMessage="Please enter a valid postal code format. (X#X X#X or X#X#X# are valid.)" ValidationExpression="^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="home-phone">Home Phone</label>
         <asp:TextBox ID="tbHomePhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhoneNumber" runat="server" ControlToValidate="tbHomePhone" ErrorMessage="Please enter a valid phone number. (Ten digit number, no dash or spaces)." ValidationExpression="^(\s*|\d{10})$" CssClass="alert alert-danger"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group col-sm-5">
                <label for="bus-phone">Business Phone</label>
                <asp:TextBox ID="tbBusinessPhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorBusPhone" runat="server" ControlToValidate="tbBusinessPhone" ErrorMessage="Please enter a valid phone number. (Ten digit number, no dash or spaces)." ValidationExpression="^(\s*|\d{10})$" CssClass="alert alert-danger"></asp:RegularExpressionValidator>

            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-sm-5">
                <label for="email">Email Address</label>
                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="tbEmail" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
                <asp:Label ID="validEmail" runat="server" CssClass="alert alert-danger" Visible="False"></asp:Label>
                <%--<asp:CustomValidator ID="ExistingEmailValidator" runat="server" ErrorMessage="The email address you have entered is already registered. " Display="Dynamic" OnServerValidate="ExistingEmailValidator_ServerValidate" CssClass="alert alert-danger" ControlToValidate="tbEmail"></asp:CustomValidator>--%>
            </div>
        </div>

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
        <br />
        <br />
        <br />
        <div class="form-row">
            <div class="form-group">
                By creating an account, you agree to the TravelExperts program rules.<br />
                <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" OnClick="btnCreateAccount_Click" CssClass="btn-success" />
            </div>
        </div>
    </div>
</asp:Content>

