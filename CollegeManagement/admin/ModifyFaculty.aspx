<%@ Page Title="Admin | Modify Faculty" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ModifyFaculty.aspx.cs" Inherits="CollegeManagement.admin.ModifyFaculty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        h1 {
            text-align: center;
        }

        .form-control-elements {
            margin-bottom: 3px;
        }

        .autogen-elements {
            background-color: lightgrey;
        }

        .form-element {
            border: none;
            padding: 0;
            border-radius: 0;
            padding-bottom: 15px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Update Faculty Details</h1>
    <br />
    <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
    <asp:Label runat="server" ID="lblStatus"></asp:Label>
    <br />

    <div class="form-group">
        <asp:Label runat="server" CssClass="form-control" Text="Faculty ID" Font-Bold="true">
            <asp:Label ID="lblId" runat="server" CssClass="form-control autogen-elements" Font-Bold="false"></asp:Label>
        </asp:Label>
        <br />


        <div class="personal-details">
            <h4>Faculty Personal Details</h4>
            <asp:Label CssClass="form-control" runat="server">
                <asp:Label runat="server" CssClass="form-control form-element top-form-element" Text="Full Name" Font-Bold="true">
                    <asp:TextBox ID="txtFullName" placeholder="Faculty Full Name" runat="server" CssClass="form-control form-control-elements"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFullName" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:Label>
                <asp:Label CssClass="form-control form-element" runat="server" Text="Email Address" Font-Bold="true">
                    <asp:TextBox ID="txtEmailAddress" placeholder="Faculty Email" runat="server" CssClass="form-control form-control-elements" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailAddress" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </asp:Label>

                <asp:Label CssClass="form-control form-element" runat="server" Text="Gender" Font-Bold="true">
                    <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control form-control-elements">
                        <asp:ListItem Enabled="true" Text="--Gender--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpGender" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </asp:Label>

                <asp:Label runat="server" CssClass="form-control form-element" Text="Date Of Birth" Font-Bold="true">
                    <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control form-control-elements" TextMode="Date" ToolTip="Enter Date of Birth"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDateOfBirth" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>

                </asp:Label>

                <asp:Label CssClass="form-control form-element" runat="server" Text="Phone Number" Font-Bold="true">
                    <asp:TextBox ID="txtPhoneNumber" placeholder="Phone Number" runat="server" CssClass="form-control form-control-elements" TextMode="Phone"></asp:TextBox>
                </asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*Enter 10 Digits Mobile Number" ControlToValidate="txtPhoneNumber" ForeColor="Red" MaximumValue="9999999999" MinimumValue="1111111111" Type="Double"></asp:RangeValidator>
            </asp:Label>

        </div>
        <br />
        <div class="academic-details">

            <h4>Faculty Course Details</h4>
            <br />

            <asp:Label runat="server" CssClass="form-control" Text="" Font-Bold="true">
                <asp:Label CssClass="form-control" Font-Bold="true" runat="server" Text="Course ID">
                    <asp:Label CssClass="form-control autogen-elements" Font-Bold="false" ID="lblCourseId" runat="server" Text="--Course ID--"></asp:Label>
                    <br />
                    <asp:Label CssClass="" runat="server" Text="Course Name"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblCourseName" runat="server" Font-Bold="false" Text="--Course Name--"></asp:Label>
                    <br />
                    <asp:Label CssClass="" runat="server" Text="Total Semesters"></asp:Label><asp:Label CssClass="form-control autogen-elements" ID="lblTotalSemesters" runat="server" Font-Bold="false" Text="--Semesters--" Enabled="false"></asp:Label>


                </asp:Label>
                <br />
                <asp:Label CssClass="form-control form-element" runat="server" Text="Salary" Font-Bold="true">
                    <asp:TextBox ID="txtSalary" placeholder="Enter Salary" runat="server" CssClass="form-control form-control-elements" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSalary" ErrorMessage="* Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </asp:Label>
            </asp:Label>
        </div>
        <br />

        <div class="registration-date">
            <asp:Label CssClass="form-control" runat="server" Font-Bold="true" Text="Date Of Joining">
                <asp:TextBox CssClass="form-control" runat="server" Placeholder="Enter Date Of Joining" ID="txtDateOfJoining" TextMode="Date"></asp:TextBox>

            </asp:Label>

        </div>
        <br />
        <div class="login-details">
            <h4>Login details</h4>
            <asp:Label CssClass="form-control" runat="server" Text="Login ID" Font-Bold="true">
                <asp:Label CssClass="form-control autogen-elements" ID="lblLoginID" runat="server" Font-Bold="false" Text="--Login ID--"></asp:Label>
                <br />
                <asp:Label CssClass="" runat="server" Text="Password"></asp:Label><asp:TextBox Font-Bold="false" CssClass="form-control" ID="txtPassword" runat="server" Placeholder="--Password--"></asp:TextBox>
                <br />
                <asp:Label CssClass="" runat="server" Text="Registration Date"></asp:Label><asp:Label Font-Bold="false" CssClass="form-control autogen-elements" ID="lblRegistrationDate" runat="server"></asp:Label>
            </asp:Label>


        </div>
        <br />

        <asp:Button runat="server" Text="Update Faculty" CssClass="btn btn-primary" ID="btnModify" OnClick="btnModify_Click"></asp:Button>
        <asp:Button runat="server" Text="Reset" CssClass="btn btn-danger" ID="btnReset" OnClick="btnReset_Click"></asp:Button>
    </div>


</asp:Content>
