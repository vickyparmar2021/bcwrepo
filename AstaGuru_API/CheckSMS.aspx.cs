using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Callsmsurl("Copy", "9975587265");
    }

    public void Callsmsurl(string SMSCopy, string MobileNo)
    {

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // .NET 4.5
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // .NET 4.0

        //WebRequest request = HttpWebRequest.Create(url);
        //WebResponse response = request.GetResponse();
        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
        //Response.Write(urlText.ToString());

        string username = "heromotchttp"; // API Username
        string password = "hercht08"; // API Password
        string senderid = "HMCLTD"; // sender
        string url = "https://hahttp.myvfirst.com/smpp/sendsms?";
        //string url = "https://myvfirst.com/smpp/sendsms?";

        string to = "91" + MobileNo;

        string service_url = url + "username=" + username + "&password=" + password + "&to=" + to + "&from=" + senderid + "&text=&dlt_templateid=1707160041954385355";
        string data_string = SMSCopy;
        string urlNew = service_url + data_string;

        WebRequest request = HttpWebRequest.Create(urlNew);
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
        Response.Write(urlText.ToString());

    }
}