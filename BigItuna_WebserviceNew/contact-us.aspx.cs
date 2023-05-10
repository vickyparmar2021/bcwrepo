using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact_us : System.Web.UI.Page
{
    protected string strmessage = "";
    SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BCBigitunaConn"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void imgsubmit_Click(object sender, ImageClickEventArgs e)
    {
        con.Open();
        SqlCommand MyCommand = new SqlCommand("inContactUs", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@Name", txtname.Text.ToString());
        MyCommand.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
        MyCommand.Parameters.AddWithValue("@PhoneNo ", txtPhone.Text.ToString());
        MyCommand.Parameters.AddWithValue("@Comment ", txtComment.Text.Trim().Replace(@"#$%<>+^~", " "));
        MyCommand.Parameters.AddWithValue("@Budget ", txtBudget.Text.Trim());
        MyCommand.ExecuteNonQuery();
        strmessage = "";
        strmessage = "<body><font face='Arial' size='2'>Big-I Tuna ContactUs Details:<br><br>";
        strmessage += " Name: <strong>" + txtname.Text.Trim().Replace("'", "") + "</strong><br>";
        strmessage += " Email ID: <strong>" + txtEmail.Text.Trim().Replace("'", "") + "</strong><br>";
        strmessage += " Phone Number : <strong>" + txtPhone.Text.Trim().Replace("'", "") + "</strong><br>";
        strmessage += " Comment : <strong>" + txtComment.Text.ToString() + "</strong><br>";
        strmessage += " Budget : <strong>" + txtBudget.Text.ToString() + "</strong><br>";
        strmessage += "</font></body>";
        //SendMail("rakesh.harmalkar@bcwebwise.com", "Contact Us Details", strmessage, "aamod.joshi@bcwebwise.com");
        SendMail("Chaaya.Baradhwaaj@bigituna.com,Asha.ravaliya@bigituna.com,vijayalakshmi.vardan@bigituna.com", "Big-I Tuna ContactUs Details", strmessage, "");
        txtEmail.Text = "";
        txtname.Text = "";
        txtPhone.Text = "";
        txtComment.Text = "";
		txtBudget.Text  = "";
		
        BasicFunction.Show1("Form Submitted Successfully");
    }

    public static string SendMail(string To, string subject, string body, string Bcc)
    {
        System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

        string smtpServer = "192.168.5.25";
        string userName = "info@bigituna.com";
        string password = "BigI123!@#";

        int cdoBasic = 1;
        int cdoSendUsingPort = 2;
        if (userName.Length > 0)
        {
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
        }

        smg.Body = body;
        smg.Subject = subject;
        smg.BodyFormat = MailFormat.Html;
        smg.From = "info@bigituna.com";
        smg.To = To;
        smg.Bcc = Bcc;

        try
        {
            SmtpMail.Send(smg);
            return "true";
        }
        catch (Exception ex)
        {
            return ex.Message;

        }
    }
}