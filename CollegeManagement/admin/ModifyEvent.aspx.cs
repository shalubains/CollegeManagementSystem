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
    public partial class ModifyEvent : System.Web.UI.Page
    {

        string Name;
        string config;
        string EventDescription;
        string DOE;
        string DOC;
        protected void Page_Load(object sender, EventArgs e)
        {
            config = config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
            string token = (string)Session["admin_token"];
            if (token != "AdminHere")
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    Name = Request.QueryString["id"];
                    GetEventDetails(Name);
                }
                catch (Exception ex)
                {
                    lblErrorStatus.Text = "*System Error";
                    lblErrorStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        //Fetching event details
        protected void GetEventDetails(string Name)
        {

            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                ObjSqlConnection.ConnectionString = config;

                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AdminGetEventDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                var P1 = new SqlParameter();
                P1.ParameterName = "@Name";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = Name;
                ObjSqlCommand.Parameters.Add(P1);

                ObjSqlConnection.Open();

                var ObjSqlReader = ObjSqlCommand.ExecuteReader();

                if (ObjSqlReader.HasRows)
                {
                    while (ObjSqlReader.Read())
                    {
                        //string EventName = ObjSqlReader.GetString(0);
                        EventDescription = ObjSqlReader.GetString(1);
                        DOE = ObjSqlReader.GetDateTime(2).ToString("yyyy-MM-dd");
                        DOC = ObjSqlReader.GetDateTime(3).ToString("yyyy-MM-dd");

                        txtEventName.Text = Name;
                        txtEventDescription.Text = EventDescription;
                        txtDateOfEvent.Text = DOE;
                        txtDateOfCreation.Text = DOC;
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
      

        //Updating the existing event
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                //string Name = txtEventName.Text;
                string EventName = txtEventName.Text;
                string EventDescription = txtEventDescription.Text;
                DateTime EventDOE = Convert.ToDateTime(txtDateOfEvent.Text);
                DateTime EventDOC = Convert.ToDateTime(txtDateOfCreation.Text);

                // Create SQL Connection
                
                ObjSqlConnection.ConnectionString = config;
                

                // Command Object for stored procedure 
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_ModifyEventByAdmin";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;


                // Parameters for adding user
                var P1 = new SqlParameter();
                P1.ParameterName = "@Name";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = EventName;
                ObjSqlCommand.Parameters.Add(P1);

                var P2 = new SqlParameter();
                P2.ParameterName = "@Description";
                P2.SqlDbType = SqlDbType.VarChar;
                P2.Value = EventDescription;
                ObjSqlCommand.Parameters.Add(P2);

                var P3 = new SqlParameter();
                P3.ParameterName = "@DOE";
                P3.SqlDbType = SqlDbType.Date;
                P3.Value = EventDOE;
                ObjSqlCommand.Parameters.Add(P3);

                var P4 = new SqlParameter();
                P4.ParameterName = "@DOC";
                P4.SqlDbType = SqlDbType.Date;
                P4.Value = EventDOC;
                ObjSqlCommand.Parameters.Add(P4);

                ObjSqlConnection.Open();

                // Add command - Returns number of rows affected
                int rows = ObjSqlCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblStatus.Text = "Event Updated successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }

                else
                {
                    lblStatus.Text = "Unable to update Event. Please Try again Later!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
                ObjSqlConnection.Close();
            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = ex.Message;
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
