using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(SendMail("aamod.joshi@bcwebwise.com", "Testtttt", "Bodyyyyy"));
    }

    public static string SendMail(string To, string subject, string body)
    {
        //System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

        ////string smtpServer = "206.183.111.148";
        ////string smtpServer = "smtp.gmail.com";
        //string userName = "info@bcwebwise.com";
        //string password = "info@1234!@#";

        //SmtpClient client = new SmtpClient("smtp.gmail.com");
        //client.EnableSsl = true;


        //int cdoBasic = 1;
        //int cdoSendUsingPort = 2;
        //if (userName.Length > 0)
        //{
        //    smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", client.ToString());
        //    smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 587);
        //    smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
        //    smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
        //    smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
        //    smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
        //}

        //smg.Body = body;
        //smg.Subject = subject;
        //smg.BodyFormat = MailFormat.Html;
        //smg.From = "info@bcwebwise.com";
        //smg.To = To;

        //try
        //{
        //    SmtpMail.Send(smg);
        //    return "Done";
        //}
        //catch (Exception ex)
        //{
        //    return ex.Message;

        //}

        var smtpClient = new SmtpClient("192.168.5.34")
        {
            Port = 587,
            Credentials = new NetworkCredential("bcwadmin@bcw.co.in", "adminbcw#@!"),
            EnableSsl = true,
        };

        smtpClient.Send("bcwadmin@bcw.co.in", "aamod.joshi@bcwebwise.com", "subjasdasdaasdsaecttttt", "bbasdasdasdasdbbody");

        return "1";

    }
}