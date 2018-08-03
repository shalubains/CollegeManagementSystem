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
    public partial class StudentDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                txtStudentId.Text = id;
                Label WelcomeUserLabel = (Label)Page.Master.FindControl("lblUserWelcome");

                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_StudentViewPersonalDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                var P1 = new SqlParameter();
                P1.ParameterName = "@Id";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value = int.Parse(id);
                ObjSqlCommand.Parameters.Add(P1);

                ObjSqlConnection.Open();

                SqlDataReader objDatareader = ObjSqlCommand.ExecuteReader();
                if (objDatareader.HasRows)
                {
                    while (objDatareader.Read())
                    {
                        string StudentId = objDatareader.GetInt32(0).ToString();
                        string StudentName = objDatareader.GetString(1);
                        string Email = objDatareader.GetString(2);
                        string Gender = objDatareader.GetString(3);
                        string Dob = objDatareader.GetDateTime(4).ToString("dd/MM/yyyy");
                        long Phone = objDatareader.GetInt64(5);
                        string Address = objDatareader.GetString(6);
                        string FatherName = objDatareader.GetString(7);
                        long FatherPhone = objDatareader.GetInt64(8);
                        string FatherEmail = objDatareader.GetString(9);
                        int CourseId = objDatareader.GetInt32(10);

                       
                        WelcomeUserLabel.Text = "Welcome, " + StudentName;
                        Session["UserName"] = WelcomeUserLabel.Text;

                        txtStudentId.Text = StudentId;
                        txtStudentName.Text = StudentName;
                        txtStudentEmail.Text = Email;
                        txtGender.Text = Gender;
                        txtDOB.Text = Dob;
                        txtPhone.Text = Phone.ToString();
                        txtAddress.Text = Address;
                        txtFatherName.Text = FatherName;
                        txtFatherPhone.Text = FatherPhone.ToString();
                        txtFatherEmail.Text = FatherEmail;
                        txtCourse.Text = CourseId.ToString();

                    }
                    objDatareader.Close();
                }

            }
            catch(Exception ex)
            {
                lblErrorStatus.Text = "*System Error";
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }
    }
}