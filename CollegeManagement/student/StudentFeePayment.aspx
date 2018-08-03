<%@ Page Title="Fee Payment" Language="C#" MasterPageFile="~/student/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentFeePayment.aspx.cs" Inherits="CollegeManagement.student.StudentFeePayment" %>

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

    <a href="#" id="ProceedPayment" class="btn btn-warning action-button">Pay Fee</a>
    <div class="output-table table-responsive">
        <h2>Student Fee Payment Details</h2>
        <br />
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />
        <asp:Table ID="FeeTable" runat="server" CssClass="table table-bordered table-hover">
            <asp:TableHeaderRow TableSection="TableHeader" CssClass="table-secondary">
                <asp:TableCell ID="CourseIDHeader" runat="server">
                        Course ID
                </asp:TableCell>
                <asp:TableCell ID="CourseNameHeader" runat="server">
                        Course Name
                </asp:TableCell>

                <asp:TableCell ID="AmountHeader" runat="server">
                        Amount
                </asp:TableCell>
                <asp:TableCell ID="PaymentDateHeader" runat="server">
                     Date Of Payment
                </asp:TableCell>
                <asp:TableCell ID="LastDateOfPaymentHeader" runat="server">
                    Last Date Of Payment
                </asp:TableCell>

                <asp:TableCell ID="FeeStatusHeader" runat="server">
                    Paid/Unpaid
                </asp:TableCell>
            </asp:TableHeaderRow>

            <asp:TableRow ID="FeeData" runat="server" CssClass="thead-dark">
                <asp:TableCell ID="tblCourseID" runat="server">
                        
                </asp:TableCell>
                <asp:TableCell ID="tblCourseName" runat="server">
                       
                </asp:TableCell>
                <asp:TableCell ID="tblAmount" runat="server">
                        
                </asp:TableCell>
                <asp:TableCell ID="tblPaymentDate" runat="server">
                        
                </asp:TableCell>
                <asp:TableCell ID="tblLastDate" runat="server">
                     
                </asp:TableCell>
                <asp:TableCell ID="tblFeeStatus" runat="server">
                    
                </asp:TableCell>


            </asp:TableRow>

        </asp:Table>
    </div>
</asp:Content>
