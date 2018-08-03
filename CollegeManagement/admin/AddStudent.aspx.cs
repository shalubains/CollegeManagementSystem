using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace CollegeManagement.admin
{
    public partial class AddStudent : System.Web.UI.Page
    {
        int FetchedCourseId;
        string config;

        protected void Page_Load(object sender, EventArgs e)
        {
            config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
            if (!IsPostBack)
            {
                string token = (string)Session["admin_token"];
                if (token != "AdminHere")
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
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
                        else
                        {

                        }
                        objDatareader.Close();

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
            }
        }


        //Register new student along with login, fee and course details
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string Name = txtFullName.Text;
                string Email = txtEmailAddress.Text;
                string Gender = drpGender.SelectedItem.Value;
                DateTime DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                long StudentPhone = Convert.ToInt64(txtPhoneNumber.Text);
                string Address = txtAddress.Text;
                string FatherName = txtFatherName.Text;
                long FatherPhone = Convert.ToInt64(txtFatherPhone.Text);
                string FatherEmail = txtFatherEmail.Text;
                int SelectedCourse = int.Parse(drpCourseList.SelectedItem.Value);

                // Create SQL Connection

                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure - Register Student
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminAddStudent";
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
                PUserType.Value = "student";
                ObjSqlCommandGetCredentials.Parameters.Add(PUserType);
                // End here

                // Parameters for adding user
                var P1 = new SqlParameter();
                P1.ParameterName = "@Name";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = Name;
                ObjSqlCommand.Parameters.Add(P1);


                var P2 = new SqlParameter();
                P2.ParameterName = "@Email";
                P2.SqlDbType = SqlDbType.VarChar;
                P2.Value = Email;
                ObjSqlCommand.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@Gender";
                P3.SqlDbType = SqlDbType.VarChar;
                P3.Value = Gender;
                ObjSqlCommand.Parameters.Add(P3);

                var P4 = new SqlParameter();
                P4.ParameterName = "@DOB";
                P4.SqlDbType = SqlDbType.Date;
                P4.Value = DOB;
                ObjSqlCommand.Parameters.Add(P4);

                var P5 = new SqlParameter();
                P5.ParameterName = "@Phone";
                P5.SqlDbType = SqlDbType.BigInt;
                P5.Value = StudentPhone;
                ObjSqlCommand.Parameters.Add(P5);

                var P6 = new SqlParameter();
                P6.ParameterName = "@Address";
                P6.SqlDbType = SqlDbType.VarChar;
                P6.Value = Address;
                ObjSqlCommand.Parameters.Add(P6);

                var P7 = new SqlParameter();
                P7.ParameterName = "@FatherName";
                P7.SqlDbType = SqlDbType.VarChar;
                P7.Value = FatherName;
                ObjSqlCommand.Parameters.Add(P7);

                var P8 = new SqlParameter();
                P8.ParameterName = "@FatherPhone";
                P8.SqlDbType = SqlDbType.BigInt;
                P8.Value = FatherPhone;
                ObjSqlCommand.Parameters.Add(P8);

                var P9 = new SqlParameter();
                P9.ParameterName = "@FatherEmail";
                P9.SqlDbType = SqlDbType.VarChar;
                P9.Value = FatherEmail;
                ObjSqlCommand.Parameters.Add(P9);

                var P10 = new SqlParameter();
                P10.ParameterName = "@CourseId";
                P10.SqlDbType = SqlDbType.Int;
                P10.Value = SelectedCourse;
                ObjSqlCommand.Parameters.Add(P10);

                ObjSqlConnection.Open();
                // End here

                // Add command - Returns number of rows affected
                int rows = ObjSqlCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    var objSqlDataReader = ObjSqlCommandGetCredentials.ExecuteReader();

                    if (objSqlDataReader.HasRows)
                    {
                        while (objSqlDataReader.Read())
                        {
                            int LoginId = objSqlDataReader.GetInt32(0);
                            string LoginPassword = objSqlDataReader.GetString(1);

                            lblEnrollmentNumber.Text = LoginId.ToString();
                            lblLoginID.Text = LoginId.ToString();
                            lblPassword.Text = LoginPassword;

                        }
                        objSqlDataReader.Close();


                        /* Sending Email
                     
                        try
                        {
                        string Text = String.Format("Thank you for registering. Your login credentials are:\n\nLogin Id: {0}\nPassword: {1}", lblLoginID.Text, lblPassword.Text);
                        SendEmail(Email, Text);
                        string LabelText = String.Format("Student added successfully!\nAn email has been sent to {0} with Login Details.", Email);
                        lblStatus.Text = LabelText;
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        }

                        catch(System.Net.Mail.SmtpException)
                        {
                            lblStatus.Text = "Student added successfully! Unable to send Email. Login Details given below.";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                        } 
                        
                    */



                    }

                    lblStatus.Text = "Student added successfully! Login details below!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }

                else
                {
                    lblStatus.Text = "Unable to add student. Please try again later!";
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

        protected void drpCourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                if (drpCourseList.SelectedItem.Value != "-1")
                {
                    //lblTest.Text = "Selected item!";
                    int SelectedCourseId = int.Parse(drpCourseList.SelectedItem.Value);

                    string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                    ObjSqlConnection.ConnectionString = config;

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


                    string FetchedCourseName;
                    double FetchedCourseFee;
                    int SemesterCount;

                    ObjSqlConnection.Open();
                    var objSqlDataReader = ObjSqlCommand.ExecuteReader();

                    if (objSqlDataReader.HasRows)
                    {
                        while (objSqlDataReader.Read())
                        {
                            FetchedCourseId = objSqlDataReader.GetInt32(0);
                            FetchedCourseName = objSqlDataReader.GetString(1);
                            FetchedCourseFee = objSqlDataReader.GetDouble(2);
                            SemesterCount = objSqlDataReader.GetInt32(3);

                            lblCourseId.Text = FetchedCourseId.ToString();
                            lblCourseName.Text = FetchedCourseName;
                            lblTotalSemesters.Text = SemesterCount.ToString();
                            lblCourseFee.Text = FetchedCourseFee.ToString();
                        }

                        objSqlDataReader.Close();
                    }
                    ObjSqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "*Unable to fetch course Details. System Error!";
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

       

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtEmailAddress.Text = "";

            drpGender.SelectedIndex = 0;
            txtDateOfBirth.Text = "";
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtFatherName.Text = "";
            txtFatherPhone.Text = "";
            txtFatherEmail.Text = "";

            drpCourseList.SelectedIndex = 0;
            lblCourseFee.Text = "--Course Fee--";
            lblCourseId.Text = "--Course Id--";
            lblCourseName.Text = "--Course Name--";
            lblEnrollmentNumber.Text = "--Enrollment Number--";
            lblLoginID.Text = "--Login--";
            lblPassword.Text = "--Password--";
            lblStatus.Text = "";
            lblTotalSemesters.Text = "--Semesters--";
        }
    }
}