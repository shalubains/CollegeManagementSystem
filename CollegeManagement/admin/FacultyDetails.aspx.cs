using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CollegeManagement.admin
{
    public partial class FacultyDetails : System.Web.UI.Page
    {
        string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {

            string token = (string)Session["admin_token"];
            if (token != "AdminHere")
            {
                Response.Redirect("~/Login.aspx");
            }
            try
            {
                //id = 5047;
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

                        lblFullName.Text = Name;
                        lblEmailAddress.Text = EmailAddress;
                        lblGender.Text = Gender;
                        lblDateOfBirth.Text = DOB;
                        lblPhoneNumber.Text = PhoneNumber;
                        lblDateOfJoining.Text = DOJ;
                        //drpCourseList.SelectedIndex = drpCourseList.Items.IndexOf(drpCourseList.Items.FindByValue(CourseId));
                        lblCourseName.Text = CourseName;
                        lblCourseId.Text = CourseId;
                        lblTotalSemesters.Text = TotalSemesters;
                        lblRegistrationDate.Text = RegistrationDate;
                        lblLoginID.Text = LoginId;
                        lblPassword.Text = LoginPassword;
                        lblSalary.Text = Salary;
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
    }
}