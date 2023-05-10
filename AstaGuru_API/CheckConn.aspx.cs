using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckConn : System.Web.UI.Page
{

    SqlConnection msconn;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msconn = new SqlConnection("Data Source=43.240.64.140\\SQLEXPRESS;Initial Catalog=DB_Astaguru;");
            Response.Write(msconn.ConnectionString + "<br><br>");

            msconn.Open();

            if (msconn.State == System.Data.ConnectionState.Open)
            {
                Response.Write("Microsoft SQL Server DB Connection is Opened.");
            }

            msconn.Close();


            Response.Write("<br><br>Both Connections are Closed.");

        }
        catch (Exception ex)
        {
            Response.Write(ex.InnerException.ToString());
        }
        finally
        {
            msconn.Close();

        }


    }
}