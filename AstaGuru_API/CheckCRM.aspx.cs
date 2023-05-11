using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckCRM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStrCRM = ConfigurationManager.ConnectionStrings["AstaguruConnectionCRM"].ToString();
        SqlConnection sConnCRM = new SqlConnection();
        sConnCRM.ConnectionString = connStrCRM;

        sConnCRM.Open();
        Response.Write(sConnCRM.State.ToString());

        sConnCRM.Close();
    }
}