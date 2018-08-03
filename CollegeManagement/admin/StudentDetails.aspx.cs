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
    public partial class StudentDetails : System.Web.UI.Page
    {

        int CourseId;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string token = (string)Session["admin_token"];
            if (token != "AdminHere")
            {
                Response.Redirect("~/Login.aspx");
            }
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string id = Request.QueryString["id"];
                txtId.Text = "Student Id : " + id;
                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;

                // Create SQL Connection
                
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
                        CourseId = objDatareader.GetInt32(10);

                        txtId.Text = StudentId;
                        txtFullName.Text = StudentName;
                        txtEmailAddress.Text = Email;
                        txtGender.Text = Gender;
                        txtDateOfBirth.Text = Dob;
                        txtPhoneNumber.Text = Phone.ToString();
                        txtAddress.Text = Address;
                        txtFatherName.Text = FatherName;
                        txtFatherPhone.Text = FatherPhone.ToString();
                        txtFatherEmail.Text = FatherEmail;

                    }

                }

                objDatareader.Close();

                // Command Object for stored procedure - Get credentials
                SqlCommand ObjSqlCommand1 = new SqlCommand();
                ObjSqlCommand1.Connection = ObjSqlConnection;
                ObjSqlCommand1.CommandText = "usp_GetUserLoginDetails";
                ObjSqlCommand1.CommandType = CommandType.StoredProcedure;



                // Used to fetch credentials from DB after registration is successful
                var PId = new SqlParameter();
                PId.ParameterName = "@Id";
                PId.SqlDbType = SqlDbType.Int;
                PId.Value = int.Parse(id);
                ObjSqlCommand1.Parameters.Add(PId);

                var PUserType = new SqlParameter();
                PUserType.ParameterName = "@Type";
                PUserType.SqlDbType = SqlDbType.VarChar;
                PUserType.Value = "student";
                ObjSqlCommand1.Parameters.Add(PUserType);
                // End here

                var objSqlDataReader2 = ObjSqlCommand1.ExecuteReader();

                if (objSqlDataReader2.HasRows)
                {
                    while (objSqlDataReader2.Read())
                    {
                        int LoginId = objSqlDataReader2.GetInt32(0);
                        string LoginPassword = objSqlDataReader2.GetString(1);

                        txtLoginId.Text = LoginId.ToString();
                        txtPassword.Text = LoginPassword;

                    }

                }

                objSqlDataReader2.Close();


                // Command Object for stored procedure
                SqlCommand ObjSqlCommand2 = new SqlCommand();
                ObjSqlCommand2.Connection = ObjSqlConnection;
                ObjSqlCommand2.CommandText = "usp_GetCoursesTableContents";
                ObjSqlCommand2.CommandType = CommandType.StoredProcedure;

                var P2 = new SqlParameter();
                P2.ParameterName = "@CourseId";
                P2.SqlDbType = SqlDbType.Int;
                P2.Value = CourseId;
                ObjSqlCommand2.Parameters.Add(P2);


                SqlDataReader objSqlDataReader1 = ObjSqlCommand2.ExecuteReader();
                int SemesterCount;

                if (objSqlDataReader1.HasRows)
                {
                    while (objSqlDataReader1.Read())
                    {
                        int FetchedCourseId = objSqlDataReader1.GetInt32(0);
                        string FetchedCourseName = objSqlDataReader1.GetString(1);
                        double FetchedCourseFee = objSqlDataReader1.GetDouble(2);
                        if (FetchedCourseId == 101 || FetchedCourseId == 102)
                            SemesterCount = 8;
                        else
                            SemesterCount = 4;

                        lblCourseId.Text = FetchedCourseId.ToString();
                        lblCourseName.Text = FetchedCourseName;
                        lblTotalSemesters.Text = SemesterCount.ToString();
                        lblCourseFee.Text = FetchedCourseFee.ToString();
                    }
                }

                objSqlDataReader1.Close();


                // Command Object for stored procedure
                SqlCommand ObjSqlCommand3 = new SqlCommand();
                ObjSqlCommand3.Connection = ObjSqlConnection;
                ObjSqlCommand3.CommandText = "usp_StudentViewFeePayment";
                ObjSqlCommand3.CommandType = CommandType.StoredProcedure;

                var P3 = new SqlParameter();
                P3.ParameterName = "@Id";
                P3.SqlDbType = SqlDbType.Int;
                P3.Value = int.Parse(id);
                ObjSqlCommand3.Parameters.Add(P3);

                SqlDataReader objSqlDataReader3 = ObjSqlCommand3.ExecuteReader();
                if (objSqlDataReader3.HasRows)
                {
                    while (objSqlDataReader3.Read())
                    {

                        string DateOfPayment = objSqlDataReader3.GetDateTime(3).ToString("dd/MM/yyyy");
                        bool IsPaid = objSqlDataReader3.GetBoolean(5);

                        if (IsPaid)
                        {
                            lblPayStatus.Text = "Paid";
                        }
                        else
                        {
                            lblPayStatus.Text = "Not Paid";
                        }

                        lblPayDate.Text = DateOfPayment;

                    }


                }

                objSqlDataReader3.Close();
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


    }

       
}