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

    public partial class Event : System.Web.UI.Page
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
                ObjSqlCommand.CommandText = "usp_EventView";
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
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }
        }

    }
}
