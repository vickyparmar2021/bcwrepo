using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dropdown : System.Web.UI.Page
{
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        drpCategory.Items.Add(new ListItem("Select", "Select"));
        drpCategory.DataSource = dt;
        drpCategory.DataBind();
    }
}