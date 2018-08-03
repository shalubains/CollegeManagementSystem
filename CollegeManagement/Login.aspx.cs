using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CollegeManagement
{
    public partial class Login2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;


                // Create SQL Connection
                SqlConnection ObjSqlConnection = new SqlConnection();
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_CheckLoginCredentials";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                int Id;
                string StringId = txtUsername.Text;
                if (StringId == "admin")
                    Id = 1;
                else
                    Id = int.Parse(StringId);

                string Password = txtPassword.Text;

                var P1 = new SqlParameter();
                P1.ParameterName = "@Id";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value =Id;
                ObjSqlCommand.Parameters.Add(P1);

                var P2 = new SqlParameter();
                P2.ParameterName = "@password";
                P2.SqlDbType = SqlDbType.VarChar;
                P2.Value = Password;
                ObjSqlCommand.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@type";
                P3.SqlDbType = SqlDbType.VarChar;
                P3.Size = 10;
                P3.Direction = ParameterDirection.Output;
                ObjSqlCommand.Parameters.Add(P3);

                ObjSqlConnection.Open();

                ObjSqlCommand.ExecuteNonQuery();
                string Type = P3.Value.ToString();
                ObjSqlConnection.Close();

                if (Type == "student" || Type == "faculty")
                {
                    Session["id"] = txtUsername.Text;
                    Session["token"] = "qwerty";

                    if (Type == "student")
                    {
                        Response.Redirect("~/student/StudentDashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/faculty/FacultyDashboard.aspx");
                    }
                }

                else if (Type == "admin")
                {
                    Session["admin_token"] = "AdminHere";
                    Response.Redirect("~/admin/Dashboard.aspx");
                }
                else
                {
                    lblStatus.Text = "*Invalid Login ID/Password. Try again!";
                } 
            }
            catch (Exception ex)
            {
                lblStatus.Text = "*Something went wrong. System error!";
            }

        }
    }
}