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
    public partial class Students : System.Web.UI.Page
    {
        string config;
        string id;

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


            config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
            // Create SQL Connection
            SqlConnection ObjSqlConnection = new SqlConnection();

            try
            {
                lblStatus.Text = "";
                lblStatus.Enabled = false;
               
                id = Convert.ToString(Session["id"]);
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_FacultyStudentList";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                var P1 = new SqlParameter();
                P1.ParameterName = "@FacultyId";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = int.Parse(id);
                ObjSqlCommand.Parameters.Add(P1);

                DataSet ObjDataSet = new DataSet();
                var ObjSqlDataAdapter = new SqlDataAdapter();
                ObjSqlDataAdapter.SelectCommand = ObjSqlCommand;
                ObjSqlDataAdapter.Fill(ObjDataSet);

                dgvViewStudents.DataSource = ObjDataSet;
                dgvViewStudents.DataBind();

                ObjSqlConnection.Close();
            }

            catch (Exception ex)
            {
                lblStatus.Enabled = true;
                lblStatus.Text = "*System error!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

     
            //Faculty Searching student
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Create SQL Connection
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {   
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_FacultySearchStudent";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                int InputId = int.Parse(txtSearchBox.Text);
                if (InputId <= 0)
                    throw new FormatException();

                var P1 = new SqlParameter();
                P1.ParameterName = "@FacultyId";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = int.Parse(id);
                ObjSqlCommand.Parameters.Add(P1);

                var P2 = new SqlParameter();
                P2.ParameterName = "@StudentId";
                P2.SqlDbType = SqlDbType.Int;
                P2.Value = InputId;
                ObjSqlCommand.Parameters.Add(P2);


                DataSet ObjDataSet = new DataSet();
                var ObjSqlDataAdapter = new SqlDataAdapter();
                ObjSqlDataAdapter.SelectCommand = ObjSqlCommand;
                ObjSqlDataAdapter.Fill(ObjDataSet);

                dgvViewStudents.DataSource = ObjDataSet;
                dgvViewStudents.DataBind();
            }
            catch (FormatException)
            {
                lblStatus.Text = "*Please enter valid ID";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "*System Error";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}