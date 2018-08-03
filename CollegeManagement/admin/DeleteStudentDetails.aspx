<%@ Page Title="Admin | Delete Student" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DeleteStudentDetails.aspx.cs" Inherits="CollegeManagement.admin.DeleteStudentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        h1 {
            text-align: center;
            text-decoration: underline;
        }

        .registration-elements {
            margin-bottom: 3px;
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



    <h1>Student Details</h1>
    <br />
    <asp:Label runat="server" ID="lblStatus"></asp:Label>
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <br />

    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

    <div class="form-group">
        <asp:TextBox ID="txtId" placeholder="ID" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>
        <br />
        <div class="personal-details">

            <h4>Student Personal Details</h4>
            <asp:TextBox ID="txtFullName" placeholder="Student Full Name" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtEmailAddress" placeholder="Student Email" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>

            <asp:TextBox ID="txtGender" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>

            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>

            <asp:TextBox ID="txtAddress" placeholder="Student Address" runat="server" CssClass="form-control registration-elements" TextMode="Multiline" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtPhoneNumber" placeholder="Phone Number" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtFatherName" placeholder="Father's Name" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtFatherPhone" placeholder="Father's Phone Number" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtFatherEmail" placeholder="Father's Email Address" runat="server" CssClass="form-control registration-elements" Enabled="false"></asp:TextBox>

        </div>
        <br />
        <div class="academic-details">
            <h4>Student Course Details</h4>

            <asp:Label CssClass="form-control" runat="server" Text="Course ID">
                <asp:Label CssClass="form-control autogen-elements" ID="lblCourseId" runat="server" Text="--Course ID--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Course Name"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblCourseName" runat="server" Text="--Course Name--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Total Semesters"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblTotalSemesters" runat="server" Text="--Semesters--" Enabled="false"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Course Fee"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblCourseFee" runat="server" Text="--Fee--" Enabled="false"></asp:Label>
            </asp:Label>

        </div>


        <div class="fee-details">
            <h4>Fee Details</h4>
            <table class="table table-bordered">
                <tr>
                    <td>
                        <label class="text-dark font-weight-bold">Fee Status</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblPayStatus" CssClass="text-dark font-weight-bold"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <label class="text-dark font-weight-bold">Date of Payment</label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblPayDate" CssClass="text-dark font-weight-bold"></asp:Label>

                    </td>
                </tr>
            </table>
        </div>
        <br />

        <div class="login-details">
            <h4>Login details</h4>
            <asp:TextBox ID="txtLoginId" placeholder="Login ID" runat="server" CssClass="form-control autogen-elements" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" CssClass="form-control autogen-elements" Enabled="False"></asp:TextBox>
        </div>
        <br />

        <asp:Button runat="server" Text="Delete Student" CssClass="btn btn-danger" ID="btnDelete" OnClick="btnDelete_Click"></asp:Button>
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

