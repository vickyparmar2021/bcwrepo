using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Check : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();

        string sOTP = _rdm.Next(_min, _max).ToString();
    }
}