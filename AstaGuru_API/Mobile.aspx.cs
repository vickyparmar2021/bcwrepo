using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        string mobile, sOTP, SMSCopy = "";

        mobile = Request.QueryString["mobile"].ToString();
        sOTP = Request.QueryString["sOTP"].ToString();
        SMSCopy = Request.QueryString["sSMSCopy"].ToString();

        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


        string postdata = "{\"flow_id\": \"61f38d19c6e1ad49fd6de6a2\",\"sender\": \"ASTAGU\",\"mobiles\": \"91" + mobile + "\",\"otp\": \"" + sOTP + "\"}";
        //Call Send SMS API
        string sendSMSUri = "https://api.msg91.com/api/v5/flow/";
        //Create HTTPWebrequest
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
        //Prepare and Add URL Encoded data
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] data = encoding.GetBytes(postdata.ToString());
        //Specify post method
        httpWReq.Method = "POST";
        httpWReq.Headers["authkey"] = "338500AD9H4VOHQl5f3135e8P1";
        httpWReq.ContentType = "application/JSON";
        httpWReq.ContentLength = data.Length;
        using (Stream stream = httpWReq.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }
        //Get the response
        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string responseString = reader.ReadToEnd();

        //Close the response
        reader.Close();
        response.Close();

    }
}