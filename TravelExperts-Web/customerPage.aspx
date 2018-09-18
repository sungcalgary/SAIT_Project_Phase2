<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="customerPage.aspx.cs" Inherits="TravelExperts_Web.WebForm6" %>
<%--
    Author: Sunghyun Lee
    Created: 2018-07
    --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div class="Customer-header">
    <h1 class="display-4">Your Personal Details</h1>
    </div>

    <div class="container">
        <h3><asp:Label ID="lblWelcome" runat="server"></asp:Label></h3>
        <div class="row">
            <asp:Button ID="btnChangePassword" runat="server" class="btn btn-info" Text="Change Password" OnClick="btnChangePassword_Click" />
        </div>
        <div class="row">
            <div class="col-sm-2">ID:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtId" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6"></div>
        </div>
        <div class="row">
            <div class="col-sm-2">Email:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtEmail" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6"></div>
        </div>
        <div class="row">
            <div class="col-sm-2">First Name:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtFname" CssClass="form-control customer-textbox" runat="server" Enabled="False" OnTextChanged="txtFname_TextChanged"></asp:TextBox></div>
            <div class="col-sm-6">
                <asp:Button ID="Button1" class="btn btn-warning" runat="server" Text="Edit" OnClick="btnFname_Click"  CausesValidation="False" />       
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFname" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="txtFname" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-2">Last Name:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtLname" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6">
                <asp:Button ID="Button2" class="btn btn-warning" runat="server" Text="Edit" OnClick="Button2_Click" CausesValidation="False" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLname" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="txtLname" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">Address:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtAddress" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6">
                <asp:Button ID="btnAddress" class="btn btn-warning" runat="server" Text="Edit" OnClick="btnAddress_Click" CausesValidation="False"  />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="txtAddress" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">City:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtCity" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6">
                <asp:Button ID="btnCity" class="btn btn-warning" runat="server" Text="Edit"  CausesValidation="False" OnClick="btnCity_Click"  />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCity" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="txtCity" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2">Province:</div>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control customer-textbox" Enabled="False">
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
            <div class="col-sm-6"><asp:Button ID="btnProvince" class="btn btn-warning" runat="server" Text="Edit" OnClick="btnProvince_Click" CausesValidation="False" /></div>
        </div>
        <div class="row">
            <div class="col-sm-2">Postal Code:</div>
            <div class="col-sm-4"><asp:TextBox ID="txtPostalCode" CssClass=" form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6">
                <asp:Button ID="Button3" class="btn btn-warning" runat="server" Text="Edit" OnClick="Button3_Click" CausesValidation="False" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPostalCode" runat="server" ControlToValidate="txtPostalCode" CssClass="alert alert-danger" Display="Dynamic" ErrorMessage="Please enter a valid postal code format. (X#X X#X or X#X#X# are valid.)" ValidationExpression="^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$"></asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2"> Country:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtCountry" CssClass="form-control customer-textbox" runat="server" Enabled="False" OnTextChanged="txtCountry_TextChanged"></asp:TextBox></div>
            <div class="col-sm-6"> 
                <asp:Button ID="btnCountry" class="btn btn-warning" runat="server" Text="Edit" OnClick="btnCountry_Click" CausesValidation="False" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCountry" runat="server" ErrorMessage="This is a mandatory field; please check and try again." ControlToValidate="txtCountry" Display="Dynamic" CssClass="alert alert-danger"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2"> Phone:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtHomePhone" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6"> 
                <asp:Button ID="btnHomePhone" class="btn btn-warning" runat="server" Text="Edit" OnClick="btnHomePhone_Click" CausesValidation="False" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhoneNumber" runat="server" ControlToValidate="txtHomePhone" ErrorMessage="Please enter a valid phone number." ValidationExpression="^(\s*|\d{10})$" CssClass="alert alert-danger"></asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2"> Bus Phone:</div>
            <div class="col-sm-4"> <asp:TextBox ID="txtBusPhone" CssClass="form-control customer-textbox" runat="server" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-6"> 
                <asp:Button ID="btnBusPhone" class="btn btn-warning" runat="server" Text="Edit" OnClick="btnBusPhone_Click" CausesValidation="False" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorBusPhone" runat="server" ControlToValidate="txtBusPhone" ErrorMessage="Please enter a valid phone number." ValidationExpression="^(\s*|\d{10})$" CssClass="alert alert-danger"></asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-2"></div>
            <div class="col-sm-4">
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-success" OnClick="btnSave_Click"/>
            </div>
            
        </div>
        
    </div>
</asp:Content>
