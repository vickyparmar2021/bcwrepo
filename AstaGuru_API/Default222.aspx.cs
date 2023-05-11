using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default222 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("https://www.bcw.co.in/BCOfferLetterModule/sendemail.aspx?mobile=9975587265&sOTP=123456");
    }
}