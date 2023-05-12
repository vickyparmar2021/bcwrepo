using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckSFTPCOnn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string SftpPth = "//ANGC_TO_CC";

            string SftPHost = "103.247.208.64";//Use Your IP Address

            string SftpUserName = "angc_user";

            string SftpPsd = "Password1";

            int SftpPort = 22; //Port 22 is defaulted for SFTP upload

            //Local file address

            string source = Server.MapPath("~/file//Book111111.xls");//Upload File Address

            string destination = SftpPth;// If destination address available

            UploadSFTPFile(SftPHost, SftpUserName, SftpPsd, source, destination, SftpPort);

            lblresponsemsg.Text = "SFTP Is Working...";

            sendMail("aamod.joshi@bcwebwise.com,prashant.channe@bcwebwise.com,dharampal.ram@bcwebwise.com,aamodjoshi007@gmail.com", "SFTP Is Working.", "SFTP Working....");
        }
        catch (Exception ex)
        {
            lblresponsemsg.Text = ex.Message.ToString();

            sendMail("aamod.joshi@bcwebwise.com,prashant.channe@bcwebwise.com,dharampal.ram@bcwebwise.com,aamodjoshi007@gmail.com", "SFTP Not Wokring... Error is :- " + ex.Message + "<br><br>" + ex.StackTrace.ToString(), "SFTP Not Working....");

        }
    }


    protected void sendMail(string sEmailId, string SMailBody, string sSubject)
    {
        try
        {
            System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

            string smtpServer = "smtp-relay.gmail.com";
            //string userName = "ebadmin@booking.heromotocorp.com";
            //string password = "book123!@#";

            int cdoBasic = 1;
            int cdoSendUsingPort = 2;
            //if (userName.Length > 0)
            //{
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 587);
            //smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
            //smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
            //smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
            //smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
            //}

            smg.BodyEncoding = System.Text.Encoding.UTF8;

            smg.Body = SMailBody;
            smg.Subject = sSubject;
            smg.BodyFormat = MailFormat.Html;
            smg.From = "info@heromotocorp.com";

            smg.To = sEmailId;
            //smg.To = "aamod.joshi@bcwebwise.com,prashant.channe@bcwebwise.com";
            //smg.Bcc = "aamod.joshi@bcwebwise.com";
            SmtpMail.Send(smg);


        }
        catch
        {
            throw;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private void UploadSFTPFile(string host, string username, string password, string sourcefile, string destination, int port)
    {
        using (SftpClient client = new SftpClient(host, port, username, password))
        {
            client.Connect();

            client.ChangeDirectory(destination);

            using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
            {
                client.BufferSize = 4 * 1024;
                client.UploadFile(fs, Path.GetFileName(sourcefile));
            }

            client.Disconnect();
        }
    }
}