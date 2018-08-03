<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/student/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="CollegeManagement.student.StudentDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            margin-top: 30px;
        }

        .textbox {
            width: 300px;
            padding-left: 10px;
            padding: 5px 10px;
        }

        h2 {
            margin-top: 10px;
        }

        label {
            margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="output-table table-responsive">

        <h2>Student Details</h2>
        </>
               <br />
        <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
        <table class="table table-borderless">
            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Student Id</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtStudentId" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
                <td>
                    <label class="text-dark font-weight-bold">Address</label></td>
                <td>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAddress" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>

            </tr>
            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Student Name</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtStudentName" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
                <td>
                    <label class="text-dark font-weight-bold">Father's Name</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtFatherName" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>

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
                    <label class="text-dark font-weight-bold">Email Address</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtStudentEmail" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>

                <td>
                    <label class="text-dark font-weight-bold">Course Id</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtCourse" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>

            </tr>
            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Phone Number</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtPhone" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
                <td>
                    <label class="text-dark font-weight-bold">Father's Email Address</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtFatherEmail" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <label class="text-dark font-weight-bold">Father's PhoneNo</label></td>
                <td>
                    <asp:TextBox runat="server" type="text" ID="txtFatherPhone" ReadOnly="true" CssClass="textbox"></asp:TextBox></td>

            </tr>
        </table>

    </div>

</asp:Content>
