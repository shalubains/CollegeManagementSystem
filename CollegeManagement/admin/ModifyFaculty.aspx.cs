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
    public partial class ModifyFaculty : System.Web.UI.Page
    {
        int id;
        string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = (string)Session["admin_token"];
            if (token != "AdminHere")
            {
                Response.Redirect("~/Login.aspx");
            }
            try
            {
                id = int.Parse(Request.QueryString["id"]);
                lblId.Text = id.ToString();

                if (!IsPostBack)
                {
                    GetFacultyDetails(id);
                }
            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "*System Error";
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }

        }


        //fetching faculty details
        protected void GetFacultyDetails(int id)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                ObjSqlConnection.ConnectionString = config;

                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminGetFacultyDetailsBeforeUpdate";
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
                        string PhoneNumber = ObjSqlReader.GetInt64(2).ToString();
                        string EmailAddress = ObjSqlReader.GetString(3);
                        string Gender = ObjSqlReader.GetString(4);
                        string DOB = ObjSqlReader.GetDateTime(5).ToString("yyyy-MM-dd");
                        string DOJ = ObjSqlReader.GetDateTime(6).ToString("yyyy-MM-dd");
                        string Salary = ObjSqlReader.GetDouble(7).ToString();
                        string CourseId = ObjSqlReader.GetInt32(8).ToString();
                        string CourseName = ObjSqlReader.GetString(10);

                        string TotalSemesters = ObjSqlReader.GetInt32(12).ToString();
                        string LoginId = ObjSqlReader.GetInt32(13).ToString();
                        string LoginPassword = ObjSqlReader.GetString(14);
                        string RegistrationDate = ObjSqlReader.GetDateTime(16).ToString();

                        txtFullName.Text = Name;
                        txtEmailAddress.Text = EmailAddress;
                        drpGender.SelectedIndex = drpGender.Items.IndexOf(drpGender.Items.FindByText(Gender));
                        drpGender.SelectedItem.Value = Gender;
                        txtDateOfBirth.Text = DOB;
                        txtPhoneNumber.Text = PhoneNumber;
                        txtDateOfJoining.Text = DOJ;
                        lblCourseName.Text = CourseName;
                        lblCourseId.Text = CourseId;
                        lblTotalSemesters.Text = TotalSemesters;
                        lblRegistrationDate.Text = RegistrationDate;
                        lblLoginID.Text = LoginId;
                        txtPassword.Text = LoginPassword;
                        txtSalary.Text = Salary;
                    }
                    ObjSqlReader.Close();
                }

            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

        //updating the existing faculty
        protected void btnModify_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                string Name = txtFullName.Text;
                string Email = txtEmailAddress.Text;
                string Gender = drpGender.SelectedItem.Value;
                DateTime DOB = Convert.ToDateTime(txtDateOfBirth.Text);
                DateTime DOJ = Convert.ToDateTime(txtDateOfJoining.Text);
                long Phone = Convert.ToInt64(txtPhoneNumber.Text);
                double Salary = Convert.ToDouble(txtSalary.Text);
                string Password = txtPassword.Text;

                // Create SQL Connection
               
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure - Register Student
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_ModifyFacultyByAdmin";
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
                P1.ParameterName = "@Id";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value = id;
                ObjSqlCommand.Parameters.Add(P1);

                // Parameters for adding user
                var P2 = new SqlParameter();
                P2.ParameterName = "@Name";
                P2.SqlDbType = SqlDbType.VarChar;
                P2.Value = Name;
                ObjSqlCommand.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@Phone";
                P3.SqlDbType = SqlDbType.BigInt;
                P3.Value = Phone;
                ObjSqlCommand.Parameters.Add(P3);

                var P4 = new SqlParameter();
                P4.ParameterName = "@Email";
                P4.SqlDbType = SqlDbType.VarChar;
                P4.Value = Email;
                ObjSqlCommand.Parameters.Add(P4);

                var P5 = new SqlParameter();
                P5.ParameterName = "@Gender";
                P5.SqlDbType = SqlDbType.VarChar;
                P5.Value = Gender;
                ObjSqlCommand.Parameters.Add(P5);

                var P6 = new SqlParameter();
                P6.ParameterName = "@DOB";
                P6.SqlDbType = SqlDbType.Date;
                P6.Value = DOB;
                ObjSqlCommand.Parameters.Add(P6);

                var P7 = new SqlParameter();
                P7.ParameterName = "@DOJ";
                P7.SqlDbType = SqlDbType.Date;
                P7.Value = DOJ;
                ObjSqlCommand.Parameters.Add(P7);

                var P8 = new SqlParameter();
                P8.ParameterName = "@Salary";
                P8.SqlDbType = SqlDbType.Float;
                P8.Value = Salary;
                ObjSqlCommand.Parameters.Add(P8);

                var P9 = new SqlParameter();
                P9.ParameterName = "@Password";
                P9.SqlDbType = SqlDbType.VarChar;
                P9.Value = Password;
                ObjSqlCommand.Parameters.Add(P9);
                // End here

                ObjSqlConnection.Open();


                // Add command - Returns number of rows affected
                int rows = ObjSqlCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblStatus.Text = "Faculty Updated successfully!";
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
                            txtPassword.Text = LoginPassword;
                        }
                    }
                    objSqlDataReader.Close();

                }

                else
                {
                    lblStatus.Text = "Unable to update faculty. Please Try again Later!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                ObjSqlConnection.Close();
                // Add faculty ends here!

            }
            catch (Exception ex)
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


        //Reseting the values of fields to null
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtEmailAddress.Text = "";
            drpGender.SelectedItem.Value = "";
            txtDateOfBirth.Text = "";
            txtDateOfJoining.Text = "";
            txtPhoneNumber.Text = "";
            txtSalary.Text = "";
        }
    }
}
