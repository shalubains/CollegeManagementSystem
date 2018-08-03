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
    public partial class FacultyMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = (string)Session["token"];
            if (token == "qwerty")
            {

                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                // Create SQL Connection
                SqlConnection ObjSqlConnection = new SqlConnection();

                try
                {
                    string id = Convert.ToString(Session["id"]);
                    Label WelcomeUserLabel = (Label)Page.Master.FindControl("lblUserWelcome");
                    ObjSqlConnection.ConnectionString = config;

                    // Command Object for stored procedure
                    SqlCommand ObjSqlCommand = new SqlCommand();
                    ObjSqlCommand.Connection = ObjSqlConnection;
                    ObjSqlCommand.CommandText = "usp_FacultyViewPersonalDetails";
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
                            string FacultyId = objDatareader.GetInt32(0).ToString();
                            string FacultyName = objDatareader.GetString(1);
                            long Phone = objDatareader.GetInt64(2);
                            string Email = objDatareader.GetString(3);
                            string Gender = objDatareader.GetString(4);
                            string Dob = objDatareader.GetDateTime(5).ToString("dd/MM/yyyy");
                            string Doj = objDatareader.GetDateTime(6).ToString("dd/MM/yyyy");
                            double Salary = objDatareader.GetDouble(7);
                            int CourseId = objDatareader.GetInt32(8);
                            string CourseName = objDatareader.GetString(10);

                            WelcomeUserLabel.Text = "Welcome, " + FacultyName;
                            Session["UserName"] = WelcomeUserLabel.Text;

                            txtFacultyId.Text = FacultyId;
                            txtFacultyName.Text = FacultyName;
                            txtPhone.Text = Phone.ToString();
                            txtEmail.Text = Email;
                            txtGender.Text = Gender;
                            txtDOB.Text = Dob;
                            txtDoj.Text = Doj;
                            txtSalary.Text = Salary.ToString();
                            txtFacultyCourse.Text = CourseName + " ("+Convert.ToString(CourseId)+")";
                        }

                        objDatareader.Close();
                    }

                }

                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }

                finally
                {
                    ObjSqlConnection.Close();
                    ObjSqlConnection.Dispose();
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            }

        }
            
}
            
    
