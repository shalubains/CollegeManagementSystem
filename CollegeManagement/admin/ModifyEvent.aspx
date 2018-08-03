<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ModifyEvent.aspx.cs" Inherits="CollegeManagement.admin.ModifyEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        h1 {
            text-align: center;
            text-decoration: underline;
        }

        .registration-elements {
            margin-bottom: 7px;
        }

        .form-group {
            width: 100%;
        }

        .autogen-elements {
            background-color: whitesmoke;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h1>Update Event</h1>
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <br />

    <div class="form-group">
        <div class="event- details">
            <h4>Event Details</h4>

            <asp:TextBox ID="txtEventName" placeholder="Event Name" runat="server" CssClass="form-control registration-elements" TextMode="SingleLine" Enabled="false"></asp:TextBox>

            <asp:TextBox ID="txtEventDescription" placeholder="Event Description" runat="server" CssClass="form-control registration-elements" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEventDescription" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

            <asp:Label runat="server" CssClass="form-control registration-elements" Text="Date Of Event">
                <asp:TextBox ID="txtDateOfEvent" runat="server" CssClass="form-control registration-elements" TextMode="Date" ToolTip="Enter Date of Birth"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateOfEvent" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

            </asp:Label>
            <asp:Label runat="server" CssClass="form-control registration-elements" Text="Date Of Creation">

                <asp:TextBox ID="txtDateOfCreation" runat="server" CssClass="form-control registration-elements" TextMode="Date" ToolTip="Enter Date of Birth"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateOfCreation" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

            </asp:Label>
        </div>
        <asp:Button runat="server" Text="Update" CssClass="btn btn-primary" ID="btnUpdate" OnClick="btnUpdate_Click"></asp:Button>
    </div>


</asp:Content>
