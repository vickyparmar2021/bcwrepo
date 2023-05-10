using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeleteAddress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblErrrorMessage.Text = "";

        if (string.IsNullOrEmpty(txtAddressId.Text))
        {
            lblErrrorMessage.Text = "Please enter address id";
        }

        else
        {
            BasicFunction.GetDetailsByDatatable("update user_address_details set is_deleted='1' where Id='" + txtAddressId.Text + "'");
            lblErrrorMessage.Text = txtAddressId.Text + " Id record deleted.";

            Response.Write("<br><br>" + "update user_address_details set is_deleted='1' where Id='" + txtAddressId.Text + "'");

        }
    }
}