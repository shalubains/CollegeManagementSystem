using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace CollegeManagement.admin
{
    public partial class AddFaculty : System.Web.UI.Page
    {
        string config;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                string token = (string)Session["admin_token"];
                if (token != "AdminHere")
                {
                    Response.Redirect("~/Login.aspx");
                }

                config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                SqlConnection ObjSqlConnection = new SqlConnection();
                try
                {
                    // Populate dropdown menu to get courses from DB
                    ObjSqlConnection.ConnectionString = config;

                        // Command Object for stored procedure
                        SqlCommand ObjSqlCommand = new SqlCommand();
                        ObjSqlCommand.Connection = ObjSqlConnection;
                        ObjSqlCommand.CommandText = "usp_GetCourses";
                        ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                        ObjSqlConnection.Open();

                        SqlDataReader objDatareader = ObjSqlCommand.ExecuteReader();

                        string CourseId;
                        string CourseName;

                        if (objDatareader.HasRows)
                        {
                            while (objDatareader.Read())
                            {
                                CourseId = objDatareader.GetInt32(0).ToString();
                                CourseName = objDatareader.GetString(1);

                                drpCourseList.Items.Add(new ListItem(CourseName, CourseId));
                            }
                        }
                       
                        objDatareader.Close();
                    }
                catch (Exception ex)
                {
                    lblErrorStatus.Text = "*Unable to fetch Course List!";
                    lblErrorStatus.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    ObjSqlConnection.Close();
                    ObjSqlConnection.Dispose();
                }
            }
        }


        //Register faculty along with login and course details 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
            ObjSqlConnection.ConnectionString = config;
            try
            {
                string Name = txtFullName.Text;
                string Email = txtEmailAddress.Text;
                string Gender = drpGender.SelectedItem.Value;
                DateTime DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                DateTime DOJ = Convert.ToDateTime(txtDateOfJoining.Text);
                long Phone = Convert.ToInt64(txtPhoneNumber.Text);
                double Salary = Convert.ToDouble(txtSalary.Text);

                int SelectedCourse = int.Parse(drpCourseList.SelectedItem.Value);

                // Command Object for stored procedure - Register Student
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminAddFaculty";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                // Command Object for stored procedure - Get credentials
                SqlCommand ObjSqlCommandGetCredentials = new SqlCommand();
                ObjSqlCommandGetCredentials.Connection = ObjSqlConnection;
                ObjSqlCommandGetCredentials.CommandText = "usp_GetNewUserCredentials";
                ObjSqlCommandGetCredentials.CommandType = CommandType.StoredProcedure;

                // Used to fetch credentials from DB after registration is successful
                var PUserType = new SqlParameter();
                PUserType.ParameterName = "@Type";
                PUserType.SqlDbType = SqlDbType.VarChar;
                PUserType.Value = "faculty";
                ObjSqlCommandGetCredentials.Parameters.Add(PUserType);
                // End here

                // Parameters for adding user
                var P1 = new SqlParameter();
                P1.ParameterName = "@Name";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = Name;
                ObjSqlCommand.Parameters.Add(P1);

                var P2 = new SqlParameter();
                P2.ParameterName = "@Phone";
                P2.SqlDbType = SqlDbType.BigInt;
                P2.Value = Phone;
                ObjSqlCommand.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@Email";
                P3.SqlDbType = SqlDbType.VarChar;
                P3.Value = Email;
                ObjSqlCommand.Parameters.Add(P3);

                var P4 = new SqlParameter();
                P4.ParameterName = "@Gender";
                P4.SqlDbType = SqlDbType.VarChar;
                P4.Value = Gender;
                ObjSqlCommand.Parameters.Add(P4);

                var P5 = new SqlParameter();
                P5.ParameterName = "@DOB";
                P5.SqlDbType = SqlDbType.Date;
                P5.Value = DOB;
                ObjSqlCommand.Parameters.Add(P5);

                var P6 = new SqlParameter();
                P6.ParameterName = "@DOJ";
                P6.SqlDbType = SqlDbType.Date;
                P6.Value = DOJ;
                ObjSqlCommand.Parameters.Add(P6);

                var P7 = new SqlParameter();
                P7.ParameterName = "@Salary";
                P7.SqlDbType = SqlDbType.Float;
                P7.Value = Salary;
                ObjSqlCommand.Parameters.Add(P7);

                var P8 = new SqlParameter();
                P8.ParameterName = "@CourseId";
                P8.SqlDbType = SqlDbType.Int;
                P8.Value = SelectedCourse;
                ObjSqlCommand.Parameters.Add(P8);
                // End here

                ObjSqlConnection.Open();


                // Add command - Returns number of rows affected
                int rows = ObjSqlCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblStatus.Text = "Faculty Added successfully! Login Details are given below.";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    // Get user credentials
                    var objSqlDataReader = ObjSqlCommandGetCredentials.ExecuteReader();
                    if (objSqlDataReader.HasRows)
                    {
                        while (objSqlDataReader.Read())
                        {
                            int LoginId = objSqlDataReader.GetInt32(0);
                            string LoginPassword = objSqlDataReader.GetString(1);
                            lblLoginID.Text = LoginId.ToString();
                            lblPassword.Text = LoginPassword;
                        }
                    }
                    objSqlDataReader.Close();


                    /* Sending Email
                    try
                    {
                        string Text = String.Format("Thank you for registering. Your login credentials are:\n\nLogin Id: {0}\nPassword: {1}", lblLoginID.Text, lblPassword.Text);
                        SendEmail(Email, Text);
                        string LabelText = String.Format("Faculty added successfully!\nAn email has been sent to {0} with Login Details.", Email);
                        lblStatus.Text = LabelText;
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }

                    catch (System.Net.Mail.SmtpException)
                    {
                        lblStatus.Text = "Faculty added successfully! Unable to send Email. Login Details given below.";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    */

                }

                else
                {
                    lblStatus.Text = "Unable to add faculty. Please Try again Later!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }

            catch (Exception ex)
            {
                lblErrorStatus.Text = ex.Message;
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }


        // Fetching the values of courses from the database
        protected void drpCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
            ObjSqlConnection.ConnectionString = config;

            try
            {
                if (drpCourseList.SelectedItem.Value != "-1")
                {
                    int SelectedCourseId = int.Parse(drpCourseList.SelectedItem.Value);

                    // Command Object for stored procedure
                    SqlCommand ObjSqlCommand = new SqlCommand();
                    ObjSqlCommand.Connection = ObjSqlConnection;
                    ObjSqlCommand.CommandText = "usp_GetCoursesTableContents";
                    ObjSqlCommand.CommandType = CommandType.StoredProcedure;


                    var P1 = new SqlParameter();
                    P1.ParameterName = "@CourseId";
                    P1.SqlDbType = SqlDbType.VarChar;
                    P1.Value = SelectedCourseId;
                    ObjSqlCommand.Parameters.Add(P1);

                    ObjSqlConnection.Open();
                    var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                    if (objSqlDataReader.HasRows)
                    {
                        while (objSqlDataReader.Read())
                        {
                            string FetchedCourseId = objSqlDataReader.GetInt32(0).ToString();
                            string FetchedCourseName = objSqlDataReader.GetString(1);
                            string FetchedCourseFee = objSqlDataReader.GetDouble(2).ToString();
                            string SemesterCount = objSqlDataReader.GetInt32(3).ToString();

                            lblCourseId.Text = FetchedCourseId.ToString();
                            lblCourseName.Text = FetchedCourseName;
                            lblTotalSemesters.Text = SemesterCount.ToString();
                        }
                        objSqlDataReader.Close();
                    }
                    
                }
                else
                {
                    lblCourseId.Text = "--Course Id--";
                    lblCourseName.Text = "--Course Name--";
                    lblTotalSemesters.Text = "--Total Semesters--";
                }
            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "*Unable to fetch Course details!";
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }


        //Reseting the fields to null
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFullName.Text="";
            txtEmailAddress.Text="";
            drpGender.SelectedIndex = 0;
            txtDateOfBirth.Text="";
            txtDateOfJoining.Text="";
            txtPhoneNumber.Text="";
            txtSalary.Text="";
            drpCourseList.SelectedIndex = 0;
            lblTotalSemesters.Text = "--Semesters--";
            lblCourseId.Text = "--Course Id--";
            lblCourseName.Text = "--Course Name--";
            
            lblLoginID.Text = "--Login--";
            lblPassword.Text = "--Password--";
            lblStatus.Text = "";

        }
    }
}