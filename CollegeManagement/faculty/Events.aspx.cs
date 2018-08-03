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
    public partial class Events : System.Web.UI.Page
    {
        string config;

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

            try
            {
                lblError.Text = "";
                config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                // Create SQL Connection
                SqlConnection ObjSqlConnection = new SqlConnection();
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_StudentFacultyViewEvent";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                DataSet ObjDataSet = new DataSet();
                var ObjSqlDataAdapter = new SqlDataAdapter();
                ObjSqlDataAdapter.SelectCommand = ObjSqlCommand;
                ObjSqlDataAdapter.Fill(ObjDataSet);

                dgvViewEvent.DataSource = ObjDataSet;
                dgvViewEvent.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = "*System Error";
                lblError.Text = ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }


        }

    }
}