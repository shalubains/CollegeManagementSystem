<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="CollegeManagement.admin.EventDetails" %>

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
    <h1>Event Details</h1>
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <br />

    <div class="form-group">
        <div class="event- details">
            <h4>Event Details</h4>

            <asp:TextBox ID="txtEventName" placeholder="Event Name" runat="server" CssClass="form-control registration-elements" TextMode="SingleLine" Enabled="false"></asp:TextBox>

            <asp:TextBox ID="txtEventDescription" placeholder="Event Description" runat="server" CssClass="form-control registration-elements" TextMode="MultiLine" Enabled="false"></asp:TextBox>

            <asp:Label runat="server" CssClass="form-control registration-elements" Text="Date Of Event">
                <asp:TextBox ID="txtDateOfEvent" runat="server" CssClass="form-control registration-elements" TextMode="Date" Enabled="false"></asp:TextBox>

            </asp:Label>
            <asp:Label runat="server" CssClass="form-control registration-elements" Text="Date Of Creation">

                <asp:TextBox ID="txtDateOfCreation" runat="server" CssClass="form-control registration-elements" TextMode="Date" Enabled="false"></asp:TextBox>

            </asp:Label>
        </div>
    </div>

</asp:Content>
