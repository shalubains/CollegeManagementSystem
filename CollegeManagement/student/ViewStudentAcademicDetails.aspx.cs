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
    public partial class ViewStudentAcademicDetails : System.Web.UI.Page
    {
        int CourseId;


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


            string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;

            // Create SQL Connection
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
            string id = Convert.ToString(Session["id"]);
            
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure - To obtain CourseId
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_StudentGetCourseDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                // Attached to ObjSqlCommand and ObjSqlCommand2
                var P1 = new SqlParameter();
                P1.ParameterName = "@StudentId";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value = int.Parse(id);
                ObjSqlCommand.Parameters.Add(P1);


                ObjSqlConnection.Open();

                SqlDataReader objDatareader = ObjSqlCommand.ExecuteReader();
                if (objDatareader.HasRows)
                {
                    while (objDatareader.Read())
                    {
                        CourseId = objDatareader.GetInt32(0);
                        string CourseName = objDatareader.GetString(1);
                        int FacultyId = objDatareader.GetInt32(2);
                        string FacultyName = objDatareader.GetString(3);

                        lblCourseID.Text = CourseId.ToString();
                        lblCourseName.Text = CourseName.ToString();
                        lblFaculty.Text = FacultyName + " (" + FacultyId.ToString() + ")";
                    }
                    objDatareader.Close();

                }

                // Command Object for stored procedure - To get semester marks
                SqlCommand ObjSqlCommand1 = new SqlCommand();
                ObjSqlCommand1.Connection = ObjSqlConnection;
                ObjSqlCommand1.CommandText = "usp_StudentViewSemesterDetails";
                ObjSqlCommand1.CommandType = CommandType.StoredProcedure;

                // Attached to ObjSqlCommand and ObjSqlCommand2
                var P2 = new SqlParameter();
                P2.ParameterName = "@StudentId";
                P2.SqlDbType = SqlDbType.Int;
                P2.Value = int.Parse(id);
                ObjSqlCommand1.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@CourseId";
                P3.SqlDbType = SqlDbType.Int;
                P3.Value = CourseId;
                ObjSqlCommand1.Parameters.Add(P3);


                DataSet ObjDataSet = new DataSet();
                var ObjSqlDataAdapter = new SqlDataAdapter();
                ObjSqlDataAdapter.SelectCommand = ObjSqlCommand1;
                ObjSqlDataAdapter.Fill(ObjDataSet);

                dgvViewStudents.DataSource = ObjDataSet;
                dgvViewStudents.DataBind();

                
                // Get aggregate Marks
                // Command Object for stored procedure - To get semester marks
                SqlCommand ObjSqlCommand2 = new SqlCommand();
                ObjSqlCommand2.Connection = ObjSqlConnection;
                ObjSqlCommand2.CommandText = "usp_GetAggregateMarks";
                ObjSqlCommand2.CommandType = CommandType.StoredProcedure;

                // Attached to ObjSqlCommand and ObjSqlCommand2
                var P4 = new SqlParameter();
                P4.ParameterName = "@Id";
                P4.SqlDbType = SqlDbType.Int;
                P4.Value = int.Parse(id);
                ObjSqlCommand2.Parameters.Add(P4);

                
                var Marks = ObjSqlCommand2.ExecuteScalar();
                

                lblAggregateMarks.Text = "Aggregate Marks: " + Marks + "%";

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

       
    }
}