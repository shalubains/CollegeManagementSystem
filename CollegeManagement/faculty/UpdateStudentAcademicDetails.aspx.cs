using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CollegeManagement.faculty
{
    public partial class UpdateStudentAcademicDetails : System.Web.UI.Page
    {
        string Semester;
        string id;
        int CourseId;
        string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName = (string)Session["UserName"];
            Label WelcomeUserLabel = (Label)Page.Master.FindControl("lblUserWelcome");
            WelcomeUserLabel.Text = UserName;


            string token = (string)Session["token"];
                if (token != "qwerty")
                {
                    Response.Redirect("~/Login.aspx");
                }

            id = Request.QueryString["id"];
            GetAggregateMarks();

            if (!IsPostBack)
            {
                // Create SQL Connection
                SqlConnection ObjSqlConnection = new SqlConnection();
                try
                {
                 
                    ObjSqlConnection.ConnectionString = config;
                    txtEnrollmentNumber.Text = id;

                    // Command Object for stored procedure
                    SqlCommand ObjSqlCommand = new SqlCommand();
                    ObjSqlCommand.Connection = ObjSqlConnection;
                    ObjSqlCommand.CommandText = "usp_FacultyViewStudentDetailsBeforeUpdate";
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
                            string FatherName = objDatareader.GetString(5);
                            long FatherPhone = objDatareader.GetInt64(6);
                            string FatherEmail = objDatareader.GetString(7);
                            CourseId = objDatareader.GetInt32(8);
                            string CourseName = objDatareader.GetString(9);

                            txtEnrollmentNumber.Text = StudentId;
                            txtStudentName.Text = StudentName;
                            txtStudentEmail.Text = Email;
                            txtGender.Text = Gender;
                            txtDob.Text = Dob;
                            txtFatherName.Text = FatherName;
                            txtFatherPhone.Text = FatherPhone.ToString();
                            txtFatherEmail.Text = FatherEmail;
                            txtCourseId.Text = CourseId.ToString();
                            txtCourseName.Text = CourseName;
                        }
                        objDatareader.Close();
                    }
              
                    GetAggregateMarks();

                    if (drpSemester.Items.Count == 1)
                    {
                        // Get Semester count from course id
                        // Command Object for stored procedure
                        SqlCommand ObjSqlCommand1 = new SqlCommand();
                        ObjSqlCommand1.Connection = ObjSqlConnection;
                        ObjSqlCommand1.CommandText = "usp_GetSemesterCount";
                        ObjSqlCommand1.CommandType = CommandType.StoredProcedure;

                        var P2 = new SqlParameter();
                        P2.ParameterName = "@CourseId";
                        P2.SqlDbType = SqlDbType.Int;
                        P2.Value = CourseId;
                        ObjSqlCommand1.Parameters.Add(P2);

                        SqlDataReader objDatareader1 = ObjSqlCommand1.ExecuteReader();
                        int SemesterCount = 0;
                        if (objDatareader1.HasRows)
                        {
                            while (objDatareader1.Read())
                            {
                                SemesterCount = objDatareader1.GetInt32(0);
                            }

                            int temp = SemesterCount;
                            while (temp > 0)
                            {
                                string SemesterString = String.Format("Semester {0}", temp.ToString());
                                drpSemester.Items.Add(new ListItem(SemesterString, temp.ToString()));
                                temp -= 1;
                            }
                        }

                        objDatareader1.Close();
                
                    }
                }
                catch (Exception ex)
                {
                    lblErrorStatus.Text = "System Error";
                }
                finally
                {
                    ObjSqlConnection.Close();
                    ObjSqlConnection.Dispose();
                }
            }
        }

        //Fetching Aggregate Marks of Student
        protected void GetAggregateMarks()
        {
            // Get aggregate Marks
            // Command Object for stored procedure - To get semester marks
            SqlConnection ObjSqlConnection = new SqlConnection();
            ObjSqlConnection.ConnectionString = config;
            SqlCommand ObjSqlCommand2 = new SqlCommand();
            ObjSqlCommand2.Connection = ObjSqlConnection;
            ObjSqlCommand2.CommandText = "usp_GetAggregateMarks";
            ObjSqlCommand2.CommandType = CommandType.StoredProcedure;

            // Attached to ObjSqlCommand and ObjSqlCommand2
            var P5 = new SqlParameter();
            P5.ParameterName = "@Id";
            P5.SqlDbType = SqlDbType.Int;
            P5.Value = int.Parse(id);
            ObjSqlCommand2.Parameters.Add(P5);

            ObjSqlConnection.Open();
            var Marks = ObjSqlCommand2.ExecuteScalar();
            ObjSqlConnection.Close();
            txtAggregateMarks.Text = "Aggregate Marks: " + Marks + "%";
        }


        //Saving the changes made on student academic details
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                
                ObjSqlConnection.ConnectionString = config;
                //fetch academic details 
                SqlCommand ObjSqlCommand1 = new SqlCommand();
                ObjSqlCommand1.Connection = ObjSqlConnection;
                ObjSqlCommand1.CommandText = "usp_UpdateSemesterDetailsByFaculty";
                ObjSqlCommand1.CommandType = CommandType.StoredProcedure;

                var P2 = new SqlParameter();
                P2.ParameterName = "@Id";
                P2.SqlDbType = SqlDbType.Int;
                P2.Value = int.Parse(txtEnrollmentNumber.Text);
                ObjSqlCommand1.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@Semester";
                P3.SqlDbType = SqlDbType.VarChar;
                P3.Value = drpSemester.SelectedItem.Value;
                ObjSqlCommand1.Parameters.Add(P3);

                var P4 = new SqlParameter();
                P4.ParameterName = "@marks";
                P4.SqlDbType = SqlDbType.Int;
                P4.Value = int.Parse(txtMarks.Text);
                ObjSqlCommand1.Parameters.Add(P4);
                ObjSqlConnection.Open();
                int NoOfRowsEffected = ObjSqlCommand1.ExecuteNonQuery();
                if (NoOfRowsEffected > 0)
                {
                    string UpdateString = String.Format("Marks for Semester {0} updated successfully!", drpSemester.SelectedItem.Value);
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Text = UpdateString;
                }
                else
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "* Unable to update marks! Please try again later.";
                }

                GetAggregateMarks();

            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "System Error";
            }

            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

        protected void drpSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                if (drpSemester.SelectedItem.Value != "-1")
                {
                   
                    ObjSqlConnection.ConnectionString = config;
                    //fetch academic details 
                    SqlCommand ObjSqlCommand1 = new SqlCommand();
                    ObjSqlCommand1.Connection = ObjSqlConnection;
                    ObjSqlCommand1.CommandText = "usp_FetchSemesterMarks";
                    ObjSqlCommand1.CommandType = CommandType.StoredProcedure;

                    Semester = drpSemester.SelectedItem.Value;

                    var P2 = new SqlParameter();
                    P2.ParameterName = "@Id";
                    P2.SqlDbType = SqlDbType.Int;
                    P2.Value = int.Parse(txtEnrollmentNumber.Text);
                    ObjSqlCommand1.Parameters.Add(P2);

                    var P3 = new SqlParameter();
                    P3.ParameterName = "@Semester";
                    P3.SqlDbType = SqlDbType.VarChar;
                    P3.Value = Semester;
                    ObjSqlCommand1.Parameters.Add(P3);


                    ObjSqlConnection.Open();
                    var objSqlDataReader = ObjSqlCommand1.ExecuteReader();

                    if (objSqlDataReader.HasRows)
                    {
                        while (objSqlDataReader.Read())
                        {
                            double marks = objSqlDataReader.GetDouble(0);
                            txtMarks.Text = marks.ToString();
                        }
                        objSqlDataReader.Close();
                    }

                
                }
            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "*System Error";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtMarks.Text = "";
            drpSemester.SelectedIndex = 0;
            drpSemester.SelectedItem.Value = "-1";
        }
    }
}