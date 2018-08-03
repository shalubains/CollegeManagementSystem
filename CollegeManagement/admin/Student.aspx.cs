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
    public partial class Student : System.Web.UI.Page
    {
        string config;
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
                lblError.Text = "";
                config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                // Create SQL Connection
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminStudentDashboard";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                DataSet ObjDataSet = new DataSet();
                var ObjSqlDataAdapter = new SqlDataAdapter();
                ObjSqlDataAdapter.SelectCommand = ObjSqlCommand;
                ObjSqlDataAdapter.Fill(ObjDataSet);

                dgvViewStudents.DataSource = ObjDataSet;
                dgvViewStudents.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "*System Error";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

            //Searching student by student id
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                // Create SQL Connection               
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminSearchStudentDashboard";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                int id = int.Parse(txtSearchBox.Text);
                if (id <= 0)
                    throw new FormatException();

                var P1 = new SqlParameter();
                P1.ParameterName = "@Id";
                P1.SqlDbType = SqlDbType.Int;
                P1.Value = id;
                ObjSqlCommand.Parameters.Add(P1);


                DataSet ObjDataSet = new DataSet();
                var ObjSqlDataAdapter = new SqlDataAdapter();
                ObjSqlDataAdapter.SelectCommand = ObjSqlCommand;
                ObjSqlDataAdapter.Fill(ObjDataSet);

                dgvViewStudents.DataSource = ObjDataSet;
                dgvViewStudents.DataBind();
            }
            catch(FormatException)
            {
                lblError.Text = "*Please enter valid ID";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblError.Text = "*System Error";
                
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }
    }
}