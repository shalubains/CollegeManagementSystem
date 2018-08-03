using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CollegeManagement.admin
{
    public partial class ModifyStudent : System.Web.UI.Page
    {
        int id;
        string config;
        protected void Page_Load(object sender, EventArgs e)
        {
            config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
            try
            {
                if (!IsPostBack)
                {
                    string token = (string)Session["admin_token"];
                    if (token != "AdminHere")
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    //id = 1015;
                    id = int.Parse(Request.QueryString["id"]);
                    lblId.Text = id.ToString();
                    GetStudentDetails(id);
                }
            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "*System Error";
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }

        }


        //Fetching student details from database before update
        protected void GetStudentDetails(int id)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                ObjSqlConnection.ConnectionString = config;

                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminGetStudentDetailsBeforeupdate";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                var P1 = new SqlParameter();
                P1.ParameterName = "@id";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value = id;
                ObjSqlCommand.Parameters.Add(P1);

                ObjSqlConnection.Open();

                var ObjSqlReader = ObjSqlCommand.ExecuteReader();

                if (ObjSqlReader.HasRows)
                {
                    while (ObjSqlReader.Read())
                    {
                        string Name = ObjSqlReader.GetString(1);
                        string EmailAddress = ObjSqlReader.GetString(2);
                        string Gender = ObjSqlReader.GetString(3);
                        string DOB = ObjSqlReader.GetDateTime(4).ToString("yyyy-MM-dd");
                        string PhoneNumber = ObjSqlReader.GetInt64(5).ToString();
                        string Address = ObjSqlReader.GetString(6);
                        string FatherName = ObjSqlReader.GetString(7);
                        string FatherPhone = ObjSqlReader.GetInt64(8).ToString();
                        string FatherEmail = ObjSqlReader.GetString(9);
                        string CourseId = ObjSqlReader.GetInt32(10).ToString();
                        string CourseName = ObjSqlReader.GetString(13);
                        string CourseFee = ObjSqlReader.GetDouble(14).ToString();
                        string TotalSemesters = ObjSqlReader.GetInt32(15).ToString();
                        string LoginId = ObjSqlReader.GetInt32(16).ToString();
                        string LoginPassword = ObjSqlReader.GetString(17);
                        string RegistrationDate = ObjSqlReader.GetDateTime(19).ToString();

                        txtFullName.Text = Name;
                        txtEmailAddress.Text = EmailAddress;
                        drpGender.SelectedIndex = drpGender.Items.IndexOf(drpGender.Items.FindByText(Gender));
                        drpGender.SelectedItem.Value = Gender;
                        txtDateOfBirth.Text = DOB;
                        txtPhoneNumber.Text = PhoneNumber;
                        txtAddress.Text = Address;
                        txtFatherName.Text = FatherName;
                        txtFatherPhone.Text = FatherPhone;
                        txtFatherEmail.Text = FatherEmail;
                        lblCourseName.Text = CourseName;
                        lblCourseId.Text = CourseId;
                        lblCourseFee.Text = CourseFee;
                        lblTotalSemesters.Text = TotalSemesters;
                        lblLoginID.Text = LoginId;
                        txtPassword.Text = LoginPassword;
                        lblRegistrationDate.Text = RegistrationDate;
                    }
                }
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }


            //updating the existing student
            protected void btnModify_Click(object sender, EventArgs e)
            {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;


                string Name = txtFullName.Text;
                string EmailAddress = txtEmailAddress.Text;
                string Gender = drpGender.SelectedItem.Value;
                DateTime DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                long StudentPhone = Convert.ToInt64(txtPhoneNumber.Text);
                string Address = txtAddress.Text;
                string FatherName = txtFatherName.Text;
                long FatherPhone = Convert.ToInt64(txtFatherPhone.Text);
                string FatherEmail = txtFatherEmail.Text;
                string CourseId = lblCourseId.Text;
                string LoginId = lblLoginID.Text;
                string LoginPassword = txtPassword.Text;



                // Create SQL Connection
                
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure - Register Student
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminModifyStudentDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;


                // Parameters for adding user

                var P1 = new SqlParameter();
                P1.ParameterName = "@Id";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value = int.Parse(lblId.Text);
                ObjSqlCommand.Parameters.Add(P1);

                var P2 = new SqlParameter();
                P2.ParameterName = "@Name";
                P2.SqlDbType = SqlDbType.VarChar;
                P2.Value = Name;
                ObjSqlCommand.Parameters.Add(P2);


                var P3 = new SqlParameter();
                P3.ParameterName = "@Email";
                P3.SqlDbType = SqlDbType.VarChar;
                P3.Value = EmailAddress;
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
                P6.ParameterName = "@Phone";
                P6.SqlDbType = SqlDbType.BigInt;
                P6.Value = StudentPhone;
                ObjSqlCommand.Parameters.Add(P6);

                var P7 = new SqlParameter();
                P7.ParameterName = "@Address";
                P7.SqlDbType = SqlDbType.VarChar;
                P7.Value = Address;
                ObjSqlCommand.Parameters.Add(P7);

                var P8 = new SqlParameter();
                P8.ParameterName = "@FatherName";
                P8.SqlDbType = SqlDbType.VarChar;
                P8.Value = FatherName;
                ObjSqlCommand.Parameters.Add(P8);

                var P9 = new SqlParameter();
                P9.ParameterName = "@FatherPhone";
                P9.SqlDbType = SqlDbType.BigInt;
                P9.Value = FatherPhone;
                ObjSqlCommand.Parameters.Add(P9);

                var P10 = new SqlParameter();
                P10.ParameterName = "@FatherEmail";
                P10.SqlDbType = SqlDbType.VarChar;
                P10.Value = FatherEmail;
                ObjSqlCommand.Parameters.Add(P10);

                
                var P11 = new SqlParameter();
                P11.ParameterName = "@Password";
                P11.SqlDbType = SqlDbType.VarChar;
                P11.Value = LoginPassword;
                ObjSqlCommand.Parameters.Add(P11);

                ObjSqlConnection.Open();
                // End here

                // Add command - Returns number of rows affected
                int NoOfRowsAffected = ObjSqlCommand.ExecuteNonQuery();

                if (NoOfRowsAffected > 0)
                {
                    lblStatus.Text = "Update Successful!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    GetStudentDetails(id);
                }
                else
                {
                    lblStatus.Text = "Unable to perform update. Please try again later!";
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


        //Reseting the fields to null
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtEmailAddress.Text = "";
            drpGender.SelectedItem.Value = "-1";
            txtDateOfBirth.Text = "";
            txtPhoneNumber.Text = "";
            txtAddress.Text = "";
            txtFatherName.Text = "";
            txtFatherPhone.Text = "";
            txtFatherEmail.Text = "";
            lblStatus.Text = "";
            lblTotalSemesters.Text = "--Semesters--";
        }
    }
}
    
