<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeleteEvent.aspx.cs" Inherits="CollegeManagement.admin.DeleteEvent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

        .modalBackground {
            height: 100%;
            background-color: darkslategrey;
            opacity: 0.7;
        }

        .modalPopup {
            height: 120px;
            width: 300px;
            border: 3px solid #0DA9D0;
        }

        .header {
            background-color: cadetblue;
            height: 50px;
            color: White;
            text-align: center;
            font-size: 160%;
            font-weight: bold;
        }

        .button {
            margin-top: 10px;
            margin-left: 60px;
            margin-bottom: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

    <h1>Delete Event</h1>
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

                <asp:TextBox ID="txtDateOfCreation" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>


            </asp:Label>
        </div>
    </div>

    <br />

    <asp:Button runat="server" Text="Delete Event" CssClass="btn btn-danger" ID="btnDelete" OnClick="btnDelete_Click"></asp:Button>
    <ajaxToolkit:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="btnDelete" DisplayModalPopupID="btnDelete_ModalPopupExtender" />
    <ajaxToolkit:ModalPopupExtender ID="btnDelete_ModalPopupExtender" runat="server" TargetControlID="btnDelete" PopupControlID="Panel1" OkControlID="OkButton" CancelControlID="CancelButton" DropShadow="true" BackgroundCssClass="modal-backdrop modalBackground modalbody">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup">
        <header class="header">Are you Sure? </header>
        <asp:Button ID="OkButton" runat="server" Text="OK" CssClass="btn btn-danger button" />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="btn btn-light button" />
    </asp:Panel>

</asp:Content>
