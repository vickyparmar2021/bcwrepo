using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using System.Web.Mail;
using System.Text.RegularExpressions;
using System.Net.Mail;

/// <summary>
/// Summary description for mailfunction
/// </summary>
public class mailfunction
{
    public mailfunction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool SendMail(string To, string subject, string body, string Bcc)
    {
        System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

        string smtpServer = "180.149.240.111";
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
        smg.Bcc = Bcc;

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


    public static bool SendMailWithCC(string To, string subject, string body, string Cc, string Bcc)
    {
        System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

        string smtpServer = "180.149.240.111";
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
        smg.Cc = Cc;
        smg.Bcc = Bcc;

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
