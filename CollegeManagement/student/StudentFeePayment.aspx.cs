using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CollegeManagement.student
{
    public partial class StudentFeePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName=(string)Session["UserName"];
            Label WelcomeUserLabel = (Label)Page.Master.FindControl("lblUserWelcome");
            WelcomeUserLabel.Text = UserName;

            string token = (string)Session["token"];
            if (token != "qwerty")
            {
                Response.Redirect("~/Login.aspx");
            }


            string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;

            // Create SQL Connection
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {  
            string id = Convert.ToString(Session["id"]);

            ObjSqlConnection.ConnectionString = config;

            // Command Object for stored procedure
            SqlCommand ObjSqlCommand = new SqlCommand();
            ObjSqlCommand.Connection = ObjSqlConnection;
            ObjSqlCommand.CommandText = "usp_StudentViewFeePayment";
            ObjSqlCommand.CommandType = CommandType.StoredProcedure;

            var P1 = new SqlParameter();
            P1.ParameterName = "@Id";
            P1.SqlDbType = SqlDbType.Int;
            P1.Value = Convert.ToInt32(id);
            ObjSqlCommand.Parameters.Add(P1);

            ObjSqlConnection.Open();

            SqlDataReader objDatareader = ObjSqlCommand.ExecuteReader();
            if (objDatareader.HasRows)
            {
                while (objDatareader.Read())
                {
                    int CourseId = objDatareader.GetInt32(0);
                    string CourseName = objDatareader.GetString(1);
                    double FeeAmount = objDatareader.GetDouble(2);
                    string PaymentDate = objDatareader.GetDateTime(3).ToString("dd/MM/yyyy");
                    string LastDate = objDatareader.GetDateTime(4).ToString("dd/MM/yyyy");
                    bool PaymentStatus = objDatareader.GetBoolean(5);
                    tblCourseID.Text = Convert.ToString(CourseId);
                    tblCourseName.Text = CourseName;
                    tblAmount.Text = FeeAmount.ToString();
                    tblPaymentDate.Text = PaymentDate.ToString();
                    tblLastDate.Text = LastDate.ToString();
                    if (PaymentStatus == true)
                        tblFeeStatus.Text = "Paid";
                    else
                        tblFeeStatus.Text = "Unpaid";
                }
                objDatareader.Close();
            }

            ObjSqlConnection.Close();
            }

            catch (Exception ex)
            {
                lblStatus.Text = "*System Error";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }

        }
    }
}
       