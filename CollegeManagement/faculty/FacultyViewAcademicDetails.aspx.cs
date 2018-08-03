using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace CollegeManagement.faculty
{
    public partial class FacultyViewAcademicDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName = (string)Session["UserName"];
            Label WelcomeUserLabel = (Label)Page.Master.FindControl("lblUserWelcome");
            WelcomeUserLabel.Text = UserName;

            string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;

                string token = (string)Session["token"];
                if (token != "qwerty")
                {
                    Response.Redirect("~/Login.aspx");
                }

            // Create SQL Connection
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {  
                ObjSqlConnection.ConnectionString = config;
                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_ViewAllStudentsAcademicDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                ObjSqlConnection.Open();

                SqlDataReader objDatareader = ObjSqlCommand.ExecuteReader();
                if (objDatareader.HasRows)
                {
                    while (objDatareader.Read())
                    {
                        int semester = objDatareader.GetInt32(0);
                        double marks = objDatareader.GetDouble(1);
                        int CourseId = objDatareader.GetInt32(2);
                        int StudentId = objDatareader.GetInt32(3);

                        tblStudentId.Text = StudentId.ToString();
                        tblCourseId.Text = CourseId.ToString();
                        tblSemester.Text = semester.ToString();
                        tblPercentage.Text = marks.ToString();

                    }
                    objDatareader.Close();
                }
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