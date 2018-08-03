<%@ Page Title="" Language="C#" MasterPageFile="~/faculty/FacultyMaster.Master" AutoEventWireup="true" CodeBehind="FacultyDashboard.aspx.cs" Inherits="CollegeManagement.faculty.FacultyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            margin-top: 10px;
        }

        .textbox {
            padding-left: 10px;
            padding: 5px 10px;
            width: 300px;
        }

        h2 {
            margin-top: 5px;
        }

        label {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="output-table table-responsive">

        <h2>Faculty Details</h2>
        </>
                     <br />
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />
        <table class="table table-borderless">
            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Faculty ID</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtFacultyId" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Faculty Name</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtFacultyName" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>

                <td>
                    <label class="text-dark font-weight-bold">Date of Joining</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtDoj" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <label class="text-dark font-weight-bold mr-2">Gender</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtGender" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
                <td>
                    <label class="text-dark font-weight-bold">Date of Birth</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtDOB" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Faculty Course</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtFacultyCourse" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
                <td>
                    <label class="text-dark font-weight-bold">Salary</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtSalary" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Phone Number</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtPhone" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
                <td>
                    <label class="text-dark font-weight-bold">Email Address</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtEmail" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
            </tr>

        </table>

    </div>
</asp:Content>
