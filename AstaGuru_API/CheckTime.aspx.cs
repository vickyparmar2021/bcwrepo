using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string remaining_second = "0";
        DateTime date = Convert.ToDateTime("2022-05-26");
        TimeSpan time = TimeSpan.Parse("20:30:00.0000000");

        DateTime startDate = date + time;
        DateTime endDate = DateTime.Now;
        var TotalMinutes = startDate.Subtract(endDate).TotalSeconds;
        remaining_second = (Convert.ToInt32(TotalMinutes)).ToString();

        Response.Write(remaining_second);
    }
}