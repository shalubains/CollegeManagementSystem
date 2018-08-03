<%@ Page Title="Admin | Add Student" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="CollegeManagement.admin.AddStudent" %>

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

        .error {
            border: 1px solid red;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Student Registration</h1>
    <br />
    <asp:Label runat="server" ID="lblStatus"></asp:Label>
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <br />



    <div class="form-group">
        <div class="personal-details">
            <h4>Student Personal Details</h4>
            <asp:TextBox type="text" ID="txtFullName" placeholder="Student Full Name" runat="server" CssClass="form-control registration-elements"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFullName" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtEmailAddress" placeholder="Student Email" runat="server" CssClass="form-control registration-elements" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailAddress" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control registration-elements">
                <asp:ListItem Enabled="true" Text="--Gender--" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpGender" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:Label runat="server" CssClass="form-control registration-elements" Text="Date Of Birth">
                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control registration-elements" TextMode="Date" ToolTip="Enter Date of Birth"></asp:TextBox>
            </asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDateOfBirth" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtAddress" placeholder="Student Address" runat="server" CssClass="form-control registration-elements" TextMode="Multiline"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtPhoneNumber" placeholder="Phone Number" runat="server" CssClass="form-control registration-elements" TextMode="Number"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*Enter 10 Digits Mobile Number" ControlToValidate="txtPhoneNumber" ForeColor="Red" MaximumValue="9999999999" MinimumValue="1111111111" Type="Double"></asp:RangeValidator>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFatherName" placeholder="Father's Name" runat="server" CssClass="form-control registration-elements"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFatherName" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFatherPhone" placeholder="Father's Phone Number" runat="server" CssClass="form-control registration-elements" TextMode="Number"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtFatherPhone" ErrorMessage="*Enter 10 Digits Mobile Number" ForeColor="Red" MaximumValue="9999999999" MinimumValue="1111111111" Type="Double"></asp:RangeValidator>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFatherPhone" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFatherEmail" placeholder="Father's Email Address" runat="server" CssClass="form-control registration-elements" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFatherEmail" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <br />
        <div class="academic-details">
            <h4>Student Course Details</h4>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="drpCourseList" runat="server" CssClass="form-control registration-elements" AutoPostBack="True" OnSelectedIndexChanged="drpCourseList_SelectedIndexChanged">
                        <asp:ListItem Enabled="true" Text="--Select Course--" Value="-1"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFullName" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

                    <br />
                    <asp:Label CssClass="form-control" runat="server" Text="Course ID">
                        <asp:Label CssClass="form-control autogen-elements" ID="lblCourseId" runat="server" Text="--Course ID--"></asp:Label>
                        <br />
                        <asp:Label CssClass="" runat="server" Text="Course Name"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblCourseName" runat="server" Text="--Course Name--"></asp:Label>
                        <br />
                        <asp:Label CssClass="" runat="server" Text="Total Semesters"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblTotalSemesters" runat="server" Text="--Semesters--" Enabled="false"></asp:Label>
                        <br />
                        <asp:Label CssClass="" runat="server" Text="Course Fee"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblCourseFee" runat="server" Text="--Fee--" Enabled="false"></asp:Label>
                    </asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <br />
        <div class="login-details">
            <h4>Auto-generated Login details and Enrollment Number</h4>
            <asp:Label CssClass="form-control" runat="server" Text="Enrollment Number">
                <asp:Label CssClass="form-control autogen-elements" ID="lblEnrollmentNumber" runat="server" Text="--Enrollment Number--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Login ID"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblLoginID" runat="server" Text="--Login ID--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Password"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblPassword" runat="server" Text="--Password--" Enabled="false"></asp:Label>
            </asp:Label>
        </div>
        <br />

        <asp:Button runat="server" Text="Add Student" CssClass="btn btn-primary" ID="btnAdd" OnClick="btnAdd_Click"></asp:Button>
        <asp:Button runat="server" Text="Reset" CssClass="btn btn-danger" ID="btnReset" OnClick="btnReset_Click1"></asp:Button>
    </div>

</asp:Content>
