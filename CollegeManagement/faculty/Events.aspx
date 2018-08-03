<%@ Page Title="" Language="C#" MasterPageFile="~/faculty/FacultyMaster.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="CollegeManagement.faculty.Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        h1 {
            text-align: center;
            text-decoration: underline;
        }

        .action-button {
            margin: 5px 10px;
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>College Events</h1>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <br />
    <div class="admin-action-buttons">
        <asp:GridView ID="dgvViewEvent" CssClass="table table-bordered table-hover" runat="server" EmptyDataText="No results found!" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Event_Name" HeaderText="Event Name" SortExpression="Event_Name" />
                <asp:BoundField DataField="Event_Description" HeaderText="Event Description" SortExpression="Event_Description" />
                <asp:BoundField DataField="Event_Date" HeaderText="Event Date" SortExpression="Event_Date" />
                <asp:BoundField DataField="Date_Of_Creation" HeaderText="Event Creation Date" SortExpression="Date_Of_Creation" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <br />
</asp:Content>
