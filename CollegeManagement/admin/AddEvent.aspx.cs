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
    public partial class AddEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = (string)Session["admin_token"];
            if (token != "AdminHere")
            {
                Response.Redirect("~/Login.aspx");
            }
        }


        //Admin creating event using stored procedure usp_AddEventDetails
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection ObjSqlConnection = new SqlConnection();
            try
            {
                string EventName = txtEventName.Text;
                string EventDescription = txtEventDescription.Text;
                DateTime DOE = Convert.ToDateTime(txtDateOfEvent.Text);
                DateTime DOC = Convert.ToDateTime(txtDateOfCreation.Text);


                string config = ConfigurationManager.ConnectionStrings["ConfigString"].ConnectionString;

                // Create SQL Connection
                
                ObjSqlConnection.ConnectionString = config;

                // Command Object for stored procedure 
                SqlCommand ObjSqlCommand = new SqlCommand();
                ObjSqlCommand.Connection = ObjSqlConnection;
                ObjSqlCommand.CommandText = "usp_AddEventDetails";
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;

                // Parameters for adding event
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
                P3.Value = DOE;
                ObjSqlCommand.Parameters.Add(P3);

                var P4 = new SqlParameter();
                P4.ParameterName = "@DOC";
                P4.SqlDbType = SqlDbType.Date;
                P4.Value = DOC;
                ObjSqlCommand.Parameters.Add(P4);
                ObjSqlConnection.Open();
                // End here

                // Add command - Returns number of rows affected
                int rows = ObjSqlCommand.ExecuteNonQuery();

                if(rows > 0)
                {             
                    Response.Redirect("Event.aspx");
                }
                else
                {
                    lblStatus.Text = "*System Error";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }


            catch (Exception)
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


        //Reseting the textfields to null
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEventDescription.Text = "";
            txtDateOfEvent.Text = "";
            txtDateOfCreation.Text = "";
        }
    }
}