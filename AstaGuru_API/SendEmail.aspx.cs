using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("crm@astaguru.com", "astagurucrm@12348"),
                EnableSsl = true,
            };

            smtpClient.Send("crm@astaguru.com", "aamodjoshi007@gmail.com", "Astaguru OTP for registration.", "Your OTP is :- ");

            Response.Write("Otp send on your Email ID");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        //SendMailWithCC("aamodjoshi007@gmail.com0", "Test Subject", "Test Body");
    }

    public static bool SendMailWithCC(string To, string subject, string body)
    {
        System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

        //string smtpServer = "206.183.111.148";
        string smtpServer = "192.168.5.25";
        string userName = "bcwadmin@bcw.co.in";
        string password = "adminbcw#@!";

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
        smg.From = "bcwadmin@bcw.co.in";
        smg.To = To;
        //smg.Cc = Cc;
        //smg.Bcc = Bcc;

        try
        {
            SmtpMail.Send(smg);
            return true;
        }
        catch (Exception ex)
        {
            return false;

        }
    }
}