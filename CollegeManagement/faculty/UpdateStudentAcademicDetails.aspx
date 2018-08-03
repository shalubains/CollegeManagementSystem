<%@ Page Title="" Language="C#" MasterPageFile="~/faculty/FacultyMaster.Master" AutoEventWireup="true" CodeBehind="UpdateStudentAcademicDetails.aspx.cs" Inherits="CollegeManagement.faculty.UpdateStudentAcademicDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        h1 {
            text-align: center;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Update Student Details</h1>
    <br />
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <br />
    <div class="form-group">
        <div class="personal-details">
            <asp:TextBox ID="txtEnrollmentNumber" placeholder="Enrollment Number" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            <br />
            <h4>Student Personal Details</h4>
            <asp:TextBox ID="txtStudentName" placeholder="First Name" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtStudentEmail" placeholder="Student Email address" runat="server" CssClass="form-control" TextMode="Email" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtGender" placeholder="Gender" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtDob" placeholder="Date of Birth" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtFatherName" placeholder="Father's Name" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtFatherPhone" placeholder="Father's Phone Number" runat="server" CssClass="form-control" TextMode="Phone" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtFatherEmail" placeholder="Father Email Address" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
        </div>
        <br />
        <div class="academic-details">
            <h4>Student Acedemic Details</h4>

            <asp:TextBox ID="txtCourseId" placeholder="Course ID" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>

            <asp:TextBox ID="txtCourseName" placeholder="Course Name" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>

            <asp:TextBox runat="server" ID="txtAggregateMarks" Enabled="false" CssClass="form-control"></asp:TextBox>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="drpSemester" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpSemester_SelectedIndexChanged">
                        <asp:ListItem Text="--Select Semester--" Value="-1"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:TextBox ID="txtMarks" placeholder="Aggregate Marks" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMarks" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMarks" ErrorMessage="*Marks Allowed 0-100" ForeColor="Red" MaximumValue="100" MinimumValue="0" Type="Integer"></asp:RangeValidator>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button runat="server" Text="Save" class="btn btn-primary" ID="btnSave" OnClick="btnSave_Click"></asp:Button>
        <asp:Button runat="server" Text="Reset" class="btn btn-danger" ID="btnReset" OnClick="btnReset_Click"></asp:Button>
    </div>
</asp:Content>
