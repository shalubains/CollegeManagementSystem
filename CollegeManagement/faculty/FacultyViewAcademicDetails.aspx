<%@ Page Title="" Language="C#" MasterPageFile="~/faculty/FacultyMaster.Master" AutoEventWireup="true" CodeBehind="FacultyViewAcademicDetails.aspx.cs" Inherits="CollegeManagement.faculty.FacultyViewAcademicDetails" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="output-table table-responsive">
        <h2>Students Academic Details</h2>
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />
        <asp:Table ID="AcademicDetailsTable" runat="server" CssClass="table table-bordered table-hover">
            <asp:TableHeaderRow TableSection="TableHeader" CssClass="table-secondary">
                <asp:TableCell ID="StudentIdHeader" runat="server">
                        Student ID
                </asp:TableCell>
                <asp:TableCell ID="CourseIdHeader" runat="server">
                        Course ID
                </asp:TableCell>

                <asp:TableCell ID="SemesterHeader" runat="server">
                         Semester
                </asp:TableCell>
                <asp:TableCell ID="PercentageHeader" runat="server">
                     Aggregate Percentage
                </asp:TableCell>
            </asp:TableHeaderRow>

            <asp:TableRow ID="AcademicDetailsData" runat="server" CssClass="thead-dark">
                <asp:TableCell ID="tblStudentId" runat="server">
                        
                </asp:TableCell>
                <asp:TableCell ID="tblCourseId" runat="server">
                       
                </asp:TableCell>
                <asp:TableCell ID="tblSemester" runat="server">
                        
                </asp:TableCell>
                <asp:TableCell ID="tblPercentage" runat="server">
                </asp:TableCell>

            </asp:TableRow>

        </asp:Table>
    </div>
</asp:Content>
