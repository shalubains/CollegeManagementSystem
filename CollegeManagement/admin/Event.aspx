<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="CollegeManagement.admin.Event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .action-button {
            margin: 5px 10px;
            width: 150px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <br />
    <div class="admin-action-buttons">
        <asp:GridView ID="dgvViewEvent" CssClass="table table-bordered table-hover" runat="server" EmptyDataText="No results found!" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Event_Name" HeaderText="Event Name" SortExpression="Event_Name" />
                <asp:BoundField DataField="Event_Date" HeaderText="Event Date" SortExpression="Event_Date" />
                <asp:HyperLinkField DataNavigateUrlFields="Event_Name" DataNavigateUrlFormatString="EventDetails.aspx?id={0}" HeaderText="Details" NavigateUrl="EventDetails.aspx" Text="Details" />
                <asp:HyperLinkField DataNavigateUrlFields="Event_Name" DataNavigateUrlFormatString="ModifyEvent.aspx?id={0}" HeaderText="Modify" Text="Modify" />
                <asp:HyperLinkField DataNavigateUrlFields="Event_Name" DataNavigateUrlFormatString="DeleteEvent.aspx?id={0}" HeaderText="Delete" Text="Delete" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        </asp:GridView>

        <br />
        <a href="AddEvent.aspx" id="linkAddEvent" class="btn btn-primary action-button">Add Event</a><br />
    </div>
</asp:Content>
