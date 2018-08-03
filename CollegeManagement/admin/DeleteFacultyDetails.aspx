<%@ Page Title="Admin | Delete Faculty" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeleteFacultyDetails.aspx.cs" Inherits="CollegeManagement.admin.DeleteFacultyDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        h1 {
            text-align: center;
        }

        .form-control-elements {
            margin-bottom: 3px;
        }

        .autogen-elements {
            background-color: #F2F2F2;
        }

        .form-element {
            border: none;
            padding: 0;
            border-radius: 0;
            padding-bottom: 15px;
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

    <h1>Faculty Details</h1>
    <br />
    <asp:Label runat="server" ID="lblStatus"></asp:Label>
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <br />

    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <div class="form-group">
        <asp:Label runat="server" CssClass="form-control" Text="Faculty ID" Font-Bold="true">
            <asp:Label ID="lblId" runat="server" CssClass="form-control autogen-elements" Font-Bold="false"></asp:Label>
        </asp:Label>
        <br />

        <div class="personal-details">
            <h4>Faculty Personal Details</h4>
            <asp:Label CssClass="form-control" runat="server">
                <asp:Label runat="server" CssClass="form-control form-element" Text="Full Name" Font-Bold="true">
                    <asp:Label ID="lblFullName" runat="server" CssClass="form-control form-control-elements autogen-elements" Font-Bold="false"></asp:Label>
                </asp:Label>
                <asp:Label CssClass="form-control form-element" runat="server" Text="Email Address" Font-Bold="true">
                    <asp:Label ID="lblEmailAddress" runat="server" CssClass="form-control form-control-elements autogen-elements" Font-Bold="false"></asp:Label>
                </asp:Label>

                <asp:Label CssClass="form-control form-element" runat="server" Text="Gender" Font-Bold="true">
                    <asp:Label ID="lblGender" runat="server" CssClass="form-control form-control-elements autogen-elements" Font-Bold="false"></asp:Label>
                </asp:Label>

                <asp:Label runat="server" CssClass="form-control form-element" Text="Date Of Birth" Font-Bold="true">
                    <asp:Label ID="lblDateOfBirth" runat="server" CssClass="form-control form-control-elements autogen-elements" Font-Bold="false"></asp:Label>
                </asp:Label>

                <asp:Label CssClass="form-control form-element" runat="server" Text="Phone Number" Font-Bold="true">
                    <asp:Label ID="lblPhoneNumber" placeholder="Phone Number" runat="server" CssClass="form-control form-control-elements autogen-elements" Font-Bold="false"></asp:Label>
                </asp:Label>
            </asp:Label>
        </div>
        <br />
        <div class="academic-details">

            <h4>Faculty Course Details</h4>

            <asp:Label CssClass="form-control" Font-Bold="true" runat="server" Text="Course ID">
                <asp:Label CssClass="form-control autogen-elements" Font-Bold="false" ID="lblCourseId" runat="server"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Course Name"></asp:Label><asp:Label CssClass="form-control autogen-elements autogen-elements" ID="lblCourseName" runat="server" Font-Bold="false" Text="--Course Name--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Total Semesters"></asp:Label><asp:Label CssClass="form-control autogen-elements autogen-elements" ID="lblTotalSemesters" runat="server" Font-Bold="false"></asp:Label>
                <br />
                <asp:Label CssClass="form-control form-element" runat="server" Text="Salary" Font-Bold="true">
                    <asp:Label ID="lblSalary" runat="server" CssClass="form-control form-control-elements autogen-elements" Font-Bold="false"></asp:Label>
                </asp:Label>

            </asp:Label>

        </div>
        <br />

        <div class="registration-date">
            <asp:Label CssClass="form-control" runat="server" Font-Bold="true" Text="Date Of Joining">
                <asp:Label CssClass="form-control autogen-elements" runat="server" Placeholder="Enter Date Of Joining" ID="lblDateOfJoining" TextMode="Date" Font-Bold="false"></asp:Label>

            </asp:Label>

        </div>
        <br />
        <div class="login-details">
            <h4>Login details</h4>
            <asp:Label CssClass="form-control" runat="server" Text="Login ID" Font-Bold="true">
                <asp:Label CssClass="form-control autogen-elements" ID="lblLoginID" runat="server" Font-Bold="false" Text="--Login ID--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Password"></asp:Label><asp:Label Font-Bold="false" CssClass="form-control autogen-elements" ID="lblPassword" runat="server" Placeholder="--Password--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Registration Date"></asp:Label><asp:Label Font-Bold="false" CssClass="form-control autogen-elements" ID="lblRegistrationDate" runat="server"></asp:Label>
            </asp:Label>
        </div>
        <br />
        <asp:Button runat="server" Text="Delete Faculty" CssClass="btn btn-danger" ID="btnDelete" OnClick="btnDelete_Click"></asp:Button>
        <ajaxToolkit:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" TargetControlID="btnDelete" DisplayModalPopupID="btnDelete_ModalPopupExtender" />
        <ajaxToolkit:ModalPopupExtender ID="btnDelete_ModalPopupExtender" runat="server" TargetControlID="btnDelete" PopupControlID="Panel1" OkControlID="OkButton" CancelControlID="CancelButton" DropShadow="true" BackgroundCssClass="modal-backdrop modalBackground modalbody">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup">
            <header class="header">Are you Sure? </header>
            <asp:Button ID="OkButton" runat="server" Text="OK" CssClass="btn btn-danger button" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="btn btn-light button" />
        </asp:Panel>

    </div>
</asp:Content>
