<%@ Page Title="Admin | Dashboard" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="CollegeManagement.admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .student-faculty-bar {
            text-align: center;
            margin-top: 0;
            background-color: ghostwhite;
            padding: 5px 0;
            border-radius: 5px;
        }

        .top-bar-item {
            margin: 0 100px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="student-faculty-bar">
        <h5><a href="Student.aspx" class="top-bar-item">Student</a> | <a href="Faculty.aspx" class="top-bar-item">Faculty</a></h5>
        <br />
        <asp:Label ID="lblErrorStatus" runat="server"></asp:Label>
        <br />
    </div>
    <br />
    <div class="search-bar"></div>


</asp:Content>
