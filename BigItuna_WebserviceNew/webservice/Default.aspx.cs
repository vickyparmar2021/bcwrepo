using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class webservice_Default : System.Web.UI.Page
{
    public string isbillingaddress { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

        //InsertProxyBidAmount("{d:'1br14shEMBwOsgo1r5FDvRc1GGwwbUnjW+8NOCQUEV3eAS3lFOAJjW34eweneBlji/Kl+C9x5Zz/n8sI7Ql0hKyWN7JJpK3Oy7VEhhyeZ0NyV9X0KZgKUVdGvUBNANjO6iQpO00yTXOddb3pheiyu3fUDxS2T6v6kUxvXoLLD5dShZVNows6bL/CGVerVqsdlrKzWfkQmVfFgQk1+vg/g5y9Dg/YXMeS0W6SCRRfn4GER+uQTFATa/Ce2k2aHbRaIPdP21aANauxoHOo1uXN5X/X9Isc+CASjGfCAOotnLQ='}");
        //InsertContactUs("{'d': {'Name': 'Aamod','Email': 'Aamod@asdcpm.com','PhoneNo': '9975587265','Comment': 'Test','Budget': '10Cr'}}");
    }

    [WebMethod]
    public static string InsertContactUs(string Name, string Email, string PhoneNo, string Comment, string Budget)
    {

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BCBigitunaConn"].ToString());

        try
        {

            //string decryptedString = d;

            //decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
            //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

            //string replacedString = decryptedString;
            //replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

            //JObject jsonContactUs = JObject.Parse(d);

            string sResponse = "";

            string JSONString = string.Empty;


            con.Open();
            SqlCommand MyCommand = new SqlCommand("inContactUs", con);
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.Parameters.AddWithValue("@Name", Name.Trim());
            MyCommand.Parameters.AddWithValue("@Email", Email.Trim());
            MyCommand.Parameters.AddWithValue("@PhoneNo ", PhoneNo.Trim());
            MyCommand.Parameters.AddWithValue("@Comment ", Comment.Trim().Replace(@"#$%<>+^~", " ").Trim());
            MyCommand.Parameters.AddWithValue("@Budget ", Budget.Trim());
            MyCommand.ExecuteNonQuery();

            //string strmessage = "";
            //strmessage = "<body><font face='Arial' size='2'>Big-I Tuna ContactUs Details:<br><br>";
            //strmessage += " Name: <strong>" + Name.Trim().Replace("'", "") + "</strong><br>";
            //strmessage += " Email ID: <strong>" + Email.Trim().Replace("'", "") + "</strong><br>";
            //strmessage += " Phone Number : <strong>" + PhoneNo.Trim().Replace("'", "") + "</strong><br>";
            //strmessage += " Comment : <strong>" + Comment.ToString() + "</strong><br>";
            //strmessage += " Budget : <strong>" + Budget.ToString() + "</strong><br>";
            //strmessage += "</font></body>";
            ////SendMail("rakesh.harmalkar@bcwebwise.com", "Contact Us Details", strmessage, "aamod.joshi@bcwebwise.com");
            //BasicFunction.SendMail("Chaaya.Baradhwaaj@bigituna.com,Asha.ravaliya@bigituna.com,vijayalakshmi.vardan@bigituna.com", "Big-I Tuna ContactUs Details", strmessage, "");

            sResponse = "{'status': 'true','message': 'Form Submitted Successfully'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);


            return sResponse;

        }

        catch (Exception ex)
        {

            return ex.StackTrace.ToString();
        }
        finally
        {
            con.Close();
        }
    }
}
