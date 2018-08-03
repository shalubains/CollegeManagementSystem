<%@ Page Title="Academic Details" Language="C#" MasterPageFile="~/student/StudentMaster.Master" AutoEventWireup="true" CodeBehind="ViewStudentAcademicDetails.aspx.cs" Inherits="CollegeManagement.student.ViewStudentAcademicDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        td, th {
            text-align: center;
        }

        .table-responsive {
            margin-top: 40px;
            max-height: 500px;
        }

        a {
            margin-top: 10px;
        }

        .marks {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Student Academic Details</h2>

    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>
    <br />

    <table class="table table-bordered ">
        <tr>
            <td>
                <asp:Label runat="server" Text="Course ID"></asp:Label></td>
            <td>
                <asp:Label ID="lblCourseID" runat="server"></asp:Label></td>

        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Course Name"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCourseName" runat="server" Text="Course Name"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Faculty"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFaculty" runat="server" Text="Faculty"></asp:Label>
            </td>
        </tr>
    </table>


    <div class="output-table table-responsive">
        <asp:GridView ID="dgvViewStudents" CssClass="table table-bordered table-hover" runat="server" EmptyDataText="No results found!">

            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>


    <br />
    <asp:Label CssClass="marks" runat="server" ID="lblAggregateMarks" Font-Bold="true"></asp:Label><br />

</asp:Content>
