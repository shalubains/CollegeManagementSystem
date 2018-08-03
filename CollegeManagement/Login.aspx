<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CollegeManagement.Login2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Student | Faculty Login</title>
    <style>
        table {
            margin-top: 10%;
        }

        td {
            text-align: center;
        }

        .InputBox {
            width: 300px;
            height: 50px;
            padding-left: 10px;
        }

        .login-selector {
            margin: 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="table table-borderless">
        <tr>
            <td>
                <h1>Login</h1>
                Student | Faculty</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" type="text" placeholder="Username" ID="txtUsername" class="InputBox"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
                <asp:TextBox runat="server" placeholder="Password" ID="txtPassword" class="InputBox" TextMode="Password"></asp:TextBox></td>


        </tr>

        <tr>
            <td>

                <asp:Button runat="server" Text="Login" class="btn btn-primary" ID="btnLogin" OnClick="btnLogin_Click"></asp:Button>
                <asp:Button runat="server" Text="Reset" class="btn btn-danger" ID="btnReset"></asp:Button>
                <br />
                <br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="*ID Required" CssClass="ml-auto mr-auto" ForeColor="Red"></asp:RequiredFieldValidator><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassword" ErrorMessage="*Password Required" CssClass="ml-auto mr-auto" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>

</asp:Content>
