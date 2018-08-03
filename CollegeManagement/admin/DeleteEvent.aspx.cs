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
    public partial class DeleteEvent : System.Web.UI.Page
    {
        string Name;
        string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            string token = (string)Session["admin_token"];
            if (token != "AdminHere")
            {
                Response.Redirect("~/Login.aspx");
            }
            try
            {
                Name = Request.QueryString["id"];
                txtEventName.Text = Name;

                GetEventDetails(Name);

            }
            catch (Exception ex)
            {
                lblErrorStatus.Text = "*System Error";
                lblErrorStatus.ForeColor = System.Drawing.Color.Red;
            }

        }


        //fetching event details from database
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
                        string EventName = ObjSqlReader.GetString(0);
                        string EventDescription = ObjSqlReader.GetString(1);
                        string DOE = ObjSqlReader.GetDateTime(2).ToString("yyyy-MM-dd");
                        string DOC = ObjSqlReader.GetDateTime(3).ToString("yyyy-MM-dd");

                        txtEventName.Text = EventName;
                        txtEventDescription.Text = EventDescription;
                        txtDateOfEvent.Text = DOE;
                        txtDateOfCreation.Text = DOC;

                    }
                }

                ObjSqlReader.Close();
            }
            finally
            {
                ObjSqlConnection.Close();
                ObjSqlConnection.Dispose();
            }

        }


        //Delete the existing event from the record
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;
                // Create SQL Connection
                
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_DeleteEventDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                var P1 = new SqlParameter();
                P1.ParameterName = "@Name";
                P1.SqlDbType = SqlDbType.VarChar;
                P1.Value = txtEventName.Text;
                ObjSqlCommand.Parameters.Add(P1);

                ObjSqlConnection.Open();


                int rows = ObjSqlCommand.ExecuteNonQuery();
                ObjSqlConnection.Close();

                if (rows > 0)
                    Response.Redirect("Event.aspx");
                else
                    lblStatus.Text = "Unable to perform delete. Please try again later!";
                lblStatus.ForeColor = System.Drawing.Color.Red;
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