<%@ Page Title="Admin - Student" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="CollegeManagement.admin.Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title></title>

    <style>
        .search-box {
            width: 45%;
            height: 40px;
            margin: 0;
            border-radius: 0;
            float: left;
        }

        .search-button {
            border-radius: 0;
            width: 80px;
            height: 40px;
        }

        .search-dropdown {
            width: 100px;
            border-radius: 0;
            height: 40px;
            text-align: center;
            margin-left: 5px;
            position: fixed;
            clear: both;
        }

        #frmAdminAction {
            text-align: center;
            margin-top: 50px;
        }

        .action-button {
            margin: 5px 10px;
            width: 150px;
        }

        td, th {
            text-align: center;
        }

        .table-responsive {
            max-height: 500px;
            border: 0.25px solid lightgrey;
            position: relative;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <br />
    <asp:TextBox runat="server" ID="txtSearchBox" CssClass="form-control search-box" placeholder="Search student"></asp:TextBox>
    <div class="search-buttons">
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success search-button" OnClick="btnSearch_Click"></asp:Button>
    </div>



    <br />
    <div class="output-table table-responsive">
        <asp:GridView ID="dgvViewStudents" CssClass="table table-bordered table-hover" runat="server" EmptyDataText="No results found!" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Student_Id" HeaderText="Student ID" SortExpression="Student_Id" />
                <asp:BoundField DataField="Student_Name" HeaderText="Student Name" SortExpression="Student_Name" />
                <asp:BoundField DataField="Course_Name" HeaderText="Course Name" SortExpression="Course_Name" />
                <asp:HyperLinkField DataNavigateUrlFields="Student_Id" DataNavigateUrlFormatString="StudentDetails.aspx?id={0}" HeaderText="Details" NavigateUrl="StudentDetails.aspx" Text="Details" />
                <asp:HyperLinkField DataNavigateUrlFields="Student_Id" DataNavigateUrlFormatString="ModifyStudent.aspx?id={0}" HeaderText="Modify" Text="Modify" />
                <asp:HyperLinkField DataNavigateUrlFields="Student_Id" DataNavigateUrlFormatString="DeleteStudentDetails.aspx?id={0}" HeaderText="Delete" Text="Delete" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>

    <br />

    <div class="admin-action-buttons">
        <a href="AddStudent.aspx" id="linkAddStudent" class="btn btn-primary action-button">Add Student</a>
    </div>



</asp:Content>
