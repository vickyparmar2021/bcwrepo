using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for BasicFunction
/// </summary>̣
public class BasicFunction
{
    struct MyObj
    {
        public static string message { get; set; }
    }
    static string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();
    static string connStrCRM = ConfigurationManager.ConnectionStrings["AstaguruConnectionCRM"].ToString();

    public BasicFunction()
    {	//
        // TODO: Add constructor logic here
        //
    }
    public static void Show1(string message)
    {
        // Cleans the message to allow single quotation marks 
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
        // string script = "<script type=\"text/javascript\">" + cleanMessage + "</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string keyy = "key1";
        if (!page.IsStartupScriptRegistered(keyy))
        {

            page.RegisterStartupScript(keyy, script);
        }

    }

    public static void ShowAndRedirect(string message, string URL)
    {
        // Cleans the message to allow single quotation marks 
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');window.location.href='" + URL + "'</script>";
        // string script = "<script type=\"text/javascript\">" + cleanMessage + "</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string keyy = "key1";
        if (!page.IsStartupScriptRegistered(keyy))
        {

            page.RegisterStartupScript(keyy, script);
        }
    }

    public static DataTable GetDetailsByDatatable(string Query)
    {
        SqlConnection conn = new SqlConnection();

        SqlDataAdapter adp;
        DataTable dTable = new DataTable("DataTable1");
        conn.ConnectionString = connStr;

        adp = new SqlDataAdapter(Query, conn);
        adp.Fill(dTable);
        return dTable;
    }

    public static DataTable GetDetailsByDatatableCRM(string Query)
    {
        SqlConnection sConnCRM = new SqlConnection();

        SqlDataAdapter adp;
        DataTable dTable = new DataTable("DataTable1");
        sConnCRM.ConnectionString = connStrCRM;

        adp = new SqlDataAdapter(Query, sConnCRM);
        adp.Fill(dTable);
        return dTable;
    }

    public static void Show(string message, UpdatePanel panel)
    {
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string key = "alertshow";
        if (!page.ClientScript.IsStartupScriptRegistered(key))
        {
            ScriptManager.RegisterStartupScript(panel, panel.GetType(), key, script, false);
        }
    }
    //checking image file extension e.g .jpg,.bmp etc.
    public static bool CheckImageFileType(string FileName)
    {
        //string tmp = FileUpload1.PostedFile.FileName.ToString();
        string tmp = FileName;
        string f1 = System.IO.Path.GetExtension(tmp);
        string Ext = f1.ToString().Trim().ToLower();
        if ((Ext.ToString() == ".jpg" || Ext.ToString() == ".gif" || Ext.ToString() == ".jpeg" || Ext.ToString() == ".bmp" || Ext.ToString() == ".png"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string TitleCase(string value)
    {

        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);

    }

    public static string DataTableToJSONWithJSONNet(DataTable table, string valid, string message)
    {
        string JSONString = string.Empty;
        dynamic collectionWrapper;

        if (table.Rows.Count > 0)
        {
            collectionWrapper = new
            {
                login = valid,
                data = table,
                message = message
            };
        }
        else
        {
            collectionWrapper = new
            {
                login = valid,
                message = message
            };
        }

        JSONString = JsonConvert.SerializeObject(collectionWrapper);
        //return JSONString;
        return JSONString.Replace("[", "").Replace("]", "");
    }

    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static void SendOTPOnMobile(string mobile)
    {
        string sSMSCopy = "";
        string strSendSMS = "";
        string sOTP = RandomNumber(000000, 999999).ToString();

        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Mahindra_Save_OTP", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@Mobile", mobile);
        MyCommand.Parameters.AddWithValue("@OTP", sOTP);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();

        sSMSCopy = sOTP + " is your OTP for Mahindra Trucks Login. Kindly submit it for your mobile verification. DO NOT SHARE IT WITH OTHERS.";

        strSendSMS = "http://subsms.obligr.com/api/pushsms.php?username=bcwebwise&password=53832&sender=BLKsMs&message=" + sSMSCopy + "&numbers=" + mobile + "&unicode=false&flash=false";

        //SendOTP otp = new SendOTP();
        //otp.SendSMS(strSendSMS);
    }

    public static void SendOTPOnEmail(string email, string sSMSCopy, string sOTP)
    {
        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Astaguru_Save_Email_OTP", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@Email", email);
        MyCommand.Parameters.AddWithValue("@OTP", sOTP);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();
        //var smtpClient = new SmtpClient("smtp.gmail.com")
        //{
        //    Port = 587,
        //    Credentials = new NetworkCredential("bid@astaguru.com", "astaguru#1234"),
        //    //  Credentials = new NetworkCredential("crm@astaguru.com", "astagurucrm@12348"),
        //    EnableSsl = false,
        //};

        //smtpClient.Send("bid@astaguru.com", email, "Astaguru OTP for registration.", "Your OTP is :- " + sOTP);

        //string redirectURl = string.Format("https://www.bcw.co.in/BCOfferLetterModule/sendemail.aspx?email={0}&sOTP={1}", email, sOTP);
        //HttpContext.Current.Response.Redirect(redirectURl);

    }

    public static void SendOTPOnMobile(string mobile, string sSMSCopy, string sOTP)
    {
        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Astaguru_Save_Mobile_OTP", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@Mobile", mobile);
        MyCommand.Parameters.AddWithValue("@OTP", sOTP);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();


        //ServicePointManager.Expect100Continue = true;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //string postdata = "{\"flow_id\": \"61f38d19c6e1ad49fd6de6a2\",\"sender\": \"ASTAGU\",\"mobiles\": \"91" + mobile + "\",\"otp\": \"" + sOTP + "\"}";
        ////Call Send SMS API
        //string sendSMSUri = "https://api.msg91.com/api/v5/flow/";
        ////Create HTTPWebrequest
        //HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
        ////Prepare and Add URL Encoded data
        //UTF8Encoding encoding = new UTF8Encoding();
        //byte[] data = encoding.GetBytes(postdata.ToString());
        ////Specify post method
        //httpWReq.Method = "POST";
        //httpWReq.Headers["authkey"] = "338500AD9H4VOHQl5f3135e8P1";
        //httpWReq.ContentType = "application/JSON";
        //httpWReq.ContentLength = data.Length;
        //using (Stream stream = httpWReq.GetRequestStream())
        //{
        //    stream.Write(data, 0, data.Length);
        //}
        ////Get the response
        //HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
        //StreamReader reader = new StreamReader(response.GetResponseStream());
        //string responseString = reader.ReadToEnd();

        ////Close the response
        //reader.Close();
        //response.Close();

        //string redirectURl = string.Format("https://www.bcw.co.in/BCOfferLetterModule/sendemail.aspx?mobile={0}&sOTP={1}", mobile, sOTP);
        //HttpContext.Current.Response.Redirect(redirectURl);

        //HttpContext.Current.Response.Redirect("https://intranet.bcwebwise.com/mobile.aspx?mobile=" + mobile + "&sOTP=" + sOTP + "&SMSCopy=" + sSMSCopy + "");
    }

    public static void Update_UserLogin(string mobile, string email, string ipAddress, string userAgent, string host, DateTime request_date, string login_from, string device_code, string fcm_tocken_mobile, string fcm_tocken_website)
    {
        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_UserLogin", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@mobile", mobile);//jsonEmail.SelectToken("mobile").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@email", mobile);//jsonEmail.SelectToken("email").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@ipAddress", ipAddress);
        MyCommand.Parameters.AddWithValue("@userAgent", userAgent);
        MyCommand.Parameters.AddWithValue("@host", host);
        MyCommand.Parameters.AddWithValue("@request_date", request_date);
        MyCommand.Parameters.AddWithValue("@login_from", login_from);//jsonEmail.SelectToken("login_from").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@device_code", device_code);
        MyCommand.Parameters.AddWithValue("@fcm_tocken_mobile", fcm_tocken_mobile);//jsonEmail.SelectToken("fcm_tocken_mobile").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@fcm_tocken_website", fcm_tocken_website);//jsonEmail.SelectToken("fcm_tocken_website").ToString().Trim());
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();
    }

    public static void Logmaintain(string ipAddress, string userAgent, string host, string url, DateTime request_date)
    {
        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Logmaintain", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        //MyCommand.Parameters.AddWithValue("@mobile", mobile);//jsonEmail.SelectToken("mobile").ToString().Trim());
        //MyCommand.Parameters.AddWithValue("@email", mobile);//jsonEmail.SelectToken("email").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@url", url);
        MyCommand.Parameters.AddWithValue("@ipAddress", ipAddress);
        MyCommand.Parameters.AddWithValue("@userAgent", userAgent);
        MyCommand.Parameters.AddWithValue("@host", host);
        MyCommand.Parameters.AddWithValue("@request_date", request_date);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();
    }

    public static void Insert_authkey(int userid, string authkey_web, string authkey_mobile)
    {
        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Insert_authkey", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        //MyCommand.Parameters.AddWithValue("@mobile", mobile);//jsonEmail.SelectToken("mobile").ToString().Trim());
        //MyCommand.Parameters.AddWithValue("@email", mobile);//jsonEmail.SelectToken("email").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@userid", userid);
        MyCommand.Parameters.AddWithValue("@authkey_mobile", authkey_mobile);
        MyCommand.Parameters.AddWithValue("@authkey_web", authkey_web);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();
    }
    public static void Insert_authkey(int userid)
    {
        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Insert_authkey", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@mobile", "");//jsonEmail.SelectToken("mobile").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@email", "");//jsonEmail.SelectToken("email").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@userid", userid);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();
    }

    public static string Profile_Count(string id)
    {
        int sum1 = 0;
        int sum2 = 0;
        DataTable dt_count1 = new DataTable();
        dt_count1 = BasicFunction.GetDetailsByDatatable("select first_name,last_name,nick_name,email,country_code,gender,dob,hear_aboutus,interested_in,interested_in_bidding,gst_no,device_code,account_num,ifsc_code,pan_card_num,adhaar_card_num,birthday,birthmonth,birthyear,passport_num,passportURl,photoid_card_num from user_details where Id=" + id);
        if (dt_count1.Rows.Count > 0)
        {
            var count1 = Enumerable.Range(0, dt_count1.Columns.Count)
                 .Select(x => new { column = x, count = dt_count1.AsEnumerable().Where(row => row[x] != DBNull.Value).Count() })
                 .ToList();
            foreach (var item in count1)
            {
                if (item.count != 0)
                {
                    sum1 = sum1 + 1;
                }
            }
        }
        DataTable dt_count2 = new DataTable();
        dt_count2 = BasicFunction.GetDetailsByDatatable("select your_name,billing_add_line1,billing_add_line2,billing_pin_code,billing_country,billing_state,billing_city,billing_location from user_address_details where Id=" + id);
        if (dt_count2.Rows.Count > 0)
        {
            var count2 = Enumerable.Range(0, dt_count2.Columns.Count)
                 .Select(x => new { column = x, count = dt_count2.AsEnumerable().Where(row => row[x] != DBNull.Value).Count() })
                 .ToList();
            foreach (var item in count2)
            {
                if (item.count != 0)
                {
                    sum2 = sum2 + 1;
                }
            }
        }
        return ((sum1 + sum2) * 3.23).ToString();
    }

    public static string Profile_Count_UserLogin(string sEmail, string sMobileNo)
    {
        int sum1 = 0;
        int sum2 = 0;
        DataTable dt_count1 = new DataTable();
        dt_count1 = BasicFunction.GetDetailsByDatatable("select first_name,last_name,nick_name,email,country_code,gender,dob,hear_aboutus,interested_in,interested_in_bidding,gst_no,device_code,account_num,ifsc_code,pan_card_num,adhaar_card_num,birthday,birthmonth,birthyear,passport_num,passportURl,photoid_card_num from user_details where email='" + sEmail + "' or mobile='" + sMobileNo + "'");
        if (dt_count1.Rows.Count > 0)
        {
            var count1 = Enumerable.Range(0, dt_count1.Columns.Count)
                 .Select(x => new { column = x, count = dt_count1.AsEnumerable().Where(row => row[x] != DBNull.Value).Count() })
                 .ToList();
            foreach (var item in count1)
            {
                if (item.count != 0)
                {
                    sum1 = sum1 + 1;
                }
            }
        }
        return ((sum1) * 4.54).ToString();
    }

    public static string Profile_Count_UserId(string sUserID)
    {
        int sum1 = 0;

        DataTable dt_count1 = new DataTable();
        dt_count1 = BasicFunction.GetDetailsByDatatable("select first_name,last_name,nick_name,email,country_code,gender,dob,hear_aboutus,interested_in,interested_in_bidding,gst_no,device_code,account_num,ifsc_code,pan_card_num,adhaar_card_num,birthday,birthmonth,birthyear,passport_num,passportURl,photoid_card_num from user_details where Id=" + sUserID);
        if (dt_count1.Rows.Count > 0)
        {
            var count1 = Enumerable.Range(0, dt_count1.Columns.Count)
                 .Select(x => new { column = x, count = dt_count1.AsEnumerable().Where(row => row[x] != DBNull.Value).Count() })
                 .ToList();
            foreach (var item in count1)
            {
                if (item.count != 0)
                {
                    sum1 = sum1 + 1;
                }
            }
        }
        return ((sum1) * 4.54).ToString();
    }


    public static void Save_OTP(string userid, string emailotp, string mobileotp)
    {

        SqlConnection con = new SqlConnection(connStr);
        SqlCommand MyCommand = new SqlCommand("sp_Insert_otp", con);
        MyCommand.CommandType = CommandType.StoredProcedure;
        MyCommand.Parameters.AddWithValue("@userid", Convert.ToInt32(userid));//jsonEmail.SelectToken("mobile").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@emailotp", emailotp);//jsonEmail.SelectToken("email").ToString().Trim());
        MyCommand.Parameters.AddWithValue("@mobileotp", mobileotp);
        MyCommand.Connection.Open();
        MyCommand.ExecuteNonQuery();

    }

    public static bool veryfy_authkey(string id, string authkey_web, string authkey_mobile)
    {
        // Get Auth_key from table
        DataTable db_auth = new DataTable();
        db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + id + "'");
        string auth_mobile = "";
        string auth_web = "";
        if (db_auth.Rows.Count > 0)
        {
            auth_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
            auth_web = db_auth.Rows[0]["authkey_web"].ToString();
            //

            if (authkey_web == auth_web && !string.IsNullOrEmpty(authkey_web))
            //jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web)
            {
                return true;
            }
            else if (authkey_mobile == auth_mobile && !string.IsNullOrEmpty(authkey_mobile))
            //jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static void change_password(int id, string new_password)
    {

        string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

        SqlConnection conn = new SqlConnection();
        SqlCommand cmdInsert;
        conn.ConnectionString = connStr;

        conn.Open();
        cmdInsert = new SqlCommand("sp_ChangePassword", conn);
        cmdInsert.CommandType = CommandType.StoredProcedure;


        cmdInsert.Parameters.AddWithValue("@id", id);
        //
        // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
        //
        cmdInsert.Parameters.AddWithValue("@new_password", new_password);

        //

        cmdInsert.ExecuteNonQuery();
        conn.Dispose();


    }


    public static string GetUpcomingAuctionDetails(string Status)
    {
        SqlConnection sConnCRM = new SqlConnection(connStrCRM);
        DataTable dTable = new DataTable();
        string strAuction = "";
        try
        {

            if (sConnCRM.State == ConnectionState.Open)
            {
                sConnCRM.Close();
                sConnCRM.Dispose();
            }

            string strQuery = @"select  *,
                                CASE WHEN auctiondate!='TBA' THEN CONVERT(varchar,auctiondate,23) END AS auctiondate11,
                                CASE WHEN auctiondate!='TBA' THEN DATENAME(month, auctiondate) END AS DisplayMonth,
                                CASE WHEN auctiondate!='TBA' THEN DATENAME(month, auctionenddate) END AS DisplayMonthTo,
                                CASE WHEN auctiondate!='TBA' THEN day(auctiondate) END AS DateFrom,
                                CASE WHEN auctiondate!='TBA' THEN day(auctionenddate) END AS DateTo,
                                CASE WHEN auctiondate!='TBA' THEN year(auctiondate) END AS DateYear,
                                CASE WHEN auctiondate!='TBA' THEN convert(varchar(10), cast(auctiondate as date),120)
								 END AS auctiondate1,
                                convert(varchar(10), cast(auctionenddate as date),120)
								 AS AuctionEndDate,
                                '' as DisplayDate,'' as AuctionURL from AuctionListMaster 
                                where status='" + Status + "' and isactive='1'";

            SqlDataAdapter adp;
            adp = new SqlDataAdapter(strQuery, sConnCRM);
            adp.Fill(dTable);

            if (dTable.Rows.Count > 0)
            {
                strAuction = "'result':{";
                strAuction = strAuction + "'auctions' : [";
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    if (dTable.Rows[i]["auctiondate"].ToString() != "TBA")
                    {
                        dTable.Rows[i]["DisplayDate"] = dTable.Rows[i]["DisplayMonth"].ToString().Trim() + " " + dTable.Rows[i]["DateFrom"].ToString().Trim() + " - " + dTable.Rows[i]["DisplayMonthTo"].ToString().Trim() + " " + dTable.Rows[i]["DateTo"].ToString().Trim() + ", " + dTable.Rows[i]["DateYear"].ToString().Trim();
                        dTable.Rows[i]["AuctionURL"] = dTable.Rows[i]["DateYear"].ToString().Trim() + "/" + BasicFunction.replacesplchar(dTable.Rows[i]["AuctionName"].ToString().Trim()) + "-" + dTable.Rows[i]["AuctionId"].ToString().Trim();
                    }
                    else
                    {
                        dTable.Rows[i]["AuctionDate1"] = "TBA";
                        dTable.Rows[i]["DisplayDate"] = "TBA";
                    }

                    DataTable db_Category = new DataTable();
                    db_Category = BasicFunction.GetDetailsByDatatableCRM("select auctionId,AuctionUserId,auctiontitle,auctionCategory from AuctionProductInventoryMaster where auctionId='" + dTable.Rows[i]["AuctionId"].ToString().Trim() + "'");
                    List<string> CategoryName = new List<string>();
                    List<string> Category = new List<string>();
                    List<string> uniqueList = new List<string>();

                    for (int j = 0; j < db_Category.Rows.Count; j++)
                    {
                        string category = db_Category.Rows[j]["auctionCategory"].ToString();
                        Category.Add(category);
                    }

                    uniqueList = Category.Distinct().ToList();

                    for (int k = 0; k < uniqueList.Count; k++)
                    {
                        string categoryNameList = "{ 'id':'" + (1 + k) + "',  'name':'" + uniqueList[k] + "'}";
                        CategoryName.Add(categoryNameList);
                    }

                    string finalString = string.Join(",", CategoryName);

                    strAuction = strAuction + "{'AuctionURL' : '" + dTable.Rows[i]["AuctionURL"].ToString() + "','AuctionName' : '" + dTable.Rows[i]["AuctionName"].ToString().Trim() + "','AnnouncedAudio' : '" + dTable.Rows[i]["auction_Sound_files"].ToString().Trim() + "','Image' : '" + dTable.Rows[i]["Image"].ToString().Trim() + "','AuctionDate' : '" + dTable.Rows[i]["AuctionDate1"].ToString().Trim() + "','AuctionEndDate' : '" + dTable.Rows[i]["AuctionEndDate"].ToString().Trim() + "','AuctionId' : '" + dTable.Rows[i]["AuctionId"].ToString().Trim() + "','DisplayDate' : '" + dTable.Rows[i]["DisplayDate"].ToString().Trim() + "','Departments' : [" + finalString + "]}";
                    if (i + 1 != dTable.Rows.Count)
                    {
                        strAuction = strAuction + ",";
                    }
                }

                strAuction = strAuction + "]";
                strAuction = strAuction + "}";

                return strAuction;
            }
            else
            {
                return strAuction;
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            sConnCRM.Close();
            sConnCRM.Dispose();
        }

    }

    public static bool IntrestInAuction(JObject jsonReg)
    {
        string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

        SqlConnection conn = new SqlConnection();
        SqlCommand cmdInsert;
        conn.ConnectionString = connStr;

        if (!string.IsNullOrEmpty(jsonReg.SelectToken("AuctionId").ToString().Trim()))
        {
            cmdInsert = new SqlCommand("sp_ShowInterestInAuction", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;

            // cmdInsert.Parameters.AddWithValue("@userid", jsonReg.SelectToken("bank_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@email", jsonReg.SelectToken("email").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@country_code", jsonReg.SelectToken("country_code").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@mobile", jsonReg.SelectToken("mobile").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@message", jsonReg.SelectToken("message").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@AuctionId", jsonReg.SelectToken("AuctionId").ToString().Trim());
            conn.Open();
            cmdInsert.ExecuteNonQuery();
            return true;
        }
        else
        {
            return false;
        }

    }

    public static string GetSchedule(string AuctionId)
    {
        DataTable db_auth = new DataTable();
        db_auth = BasicFunction.GetDetailsByDatatable("select Auction_Title,Auction_Description from tbl_AuctionClosingSchedule where AuctionId='" + AuctionId + "'");

        string Auction_Title = "";
        string Auction_Description = "";

        if (db_auth.Rows.Count > 0)
        {
            Auction_Title = db_auth.Rows[0]["Auction_Title"].ToString().ToLower();
            Auction_Description = db_auth.Rows[0]["Auction_Description"].ToString().ToLower();
            DataTable db_mail = new DataTable();
            db_mail = BasicFunction.GetDetailsByDatatable("select ClosingTime_IST,ClosingTime_HongKong,LotNumbers from tbl_AuctionClosingScheduleGroup where AuctionId='" + AuctionId + "'");
            //

            List<string> address = new List<string>();

            if (db_mail.Rows.Count > 0)
            {
                for (int i = 0; i < db_mail.Rows.Count; i++)
                {
                    string All_Group = "['" + (i + 1).ToString() + "','" + db_mail.Rows[i]["LotNumbers"].ToString() + "','" + db_mail.Rows[i]["ClosingTime_IST"].ToString() + "','" + db_mail.Rows[i]["ClosingTime_HongKong"].ToString() + "']";
                    address.Add(All_Group);
                }
            }
            else
            {
                string All_Group = "{}";
                address.Add(All_Group);
            }

            //
            string str = string.Join(",", address);

            string Result = "'Title':'" + Auction_Title + "','Desc':'" + Auction_Description + "','ClosingData':[['Group','Lot Nos','Closing Time (IST)','Closing Time (Hong Kong)']," + str + "]";
            return Result;

        }
        else
        {
            return string.Empty;
        }
    }

    public static string UpcomingAuctionInformation(string AuctionId)
    {
        //SqlConnection sConnCRM = new SqlConnection(connStrCRM);
        DataTable dTable = new DataTable();
        DataTable dTime = new DataTable();
        string strAuction = "";
        try
        {

            //if (sConnCRM.State == ConnectionState.Open)
            //{
            //    sConnCRM.Close();
            //    sConnCRM.Dispose();
            //}

            dTable = BasicFunction.GetDetailsByDatatableCRM(@"select  *,
                                CASE WHEN auctiondate!='TBA' THEN CONVERT(varchar,auctiondate,23) END AS auctiondate1,
                                CASE WHEN auctiondate!='TBA' THEN DATENAME(month, auctiondate) END AS DisplayMonth,
                                CASE WHEN auctiondate!='TBA' THEN DATENAME(month, auctionenddate) END AS DisplayMonthTo,
                                CASE WHEN auctiondate!='TBA' THEN day(auctiondate) END AS DateFrom,
                                CASE WHEN auctiondate!='TBA' THEN day(auctionenddate) END AS DateTo,
                                CASE WHEN auctiondate!='TBA' THEN year(auctiondate) END AS DateYear,
                                '' as DisplayDate from AuctionListMaster
                                where AuctionId='" + AuctionId + "'");

            dTime = BasicFunction.GetDetailsByDatatableCRM("select * from ManageLotMaster where Lot_AuctionId='" + AuctionId + "'");

            //SqlDataAdapter adp;
            //adp = new SqlDataAdapter(strQuery, sConnCRM);
            //adp.Fill(dTable);

            //SqlDataAdapter adp1;
            //adp1 = new SqlDataAdapter(strQuery1, sConnCRM);
            //adp1.Fill(dTime);
            string CataloguePDF = "";
            string WalkThrough3D = "";
            string Banner = "";
            DataTable db_detail = new DataTable();
            db_detail = BasicFunction.GetDetailsByDatatableCRM("select recentAuctionBanner,auction_E_CatlogueLink,auction_3_DWalkthroughLink from AuctionListMaster where AuctionId=" + "" + AuctionId + "");
            if (db_detail.Rows.Count > 0)
            {
                CataloguePDF = db_detail.Rows[0]["auction_E_CatlogueLink"].ToString();
                WalkThrough3D = db_detail.Rows[0]["auction_3_DWalkthroughLink"].ToString();
                Banner = db_detail.Rows[0]["recentAuctionBanner"].ToString();
            }

            if (dTable.Rows.Count > 0)
            {
                strAuction = "'result':";
                string AuctionStating = "";
                string AuctionEnding = "";

                if (dTime.Rows.Count > 0)
                {
                    string sDate = dTime.Rows[0]["Lot_Bid_StartDate"].ToString().Replace(" 00:00:00", "").Replace(" 12:00:00 AM", "");
                    string sTime = dTime.Rows[0]["Lot_Bid_StartTime"].ToString().Replace(" 00:00:00", "").Replace(" 12:00:00 AM", "");
                    string cDate = dTime.Rows[0]["Lot_Bid_CloseDate"].ToString().Replace(" 00:00:00", "").Replace(" 12:00:00 AM", "");
                    string cTime = dTime.Rows[0]["Lot_Bid_CloseTime"].ToString().Replace(" 00:00:00", "").Replace(" 12:00:00 AM", "");

                    string fsDate = sDate + " " + sTime;
                    string fcDate = cDate + " " + cTime;

                    // string Calender = "'start':'" + fsDate + "','end':'" + fcDate + "','timezone':'Asia/Kolkata','title':'" + dTable.Rows[0]["AuctionName"].ToString().Trim() + "','description':'http://livedomain/auctions/upcoming/2022/Modern-Indian-art-1','location':'','organizer':'AstaGuru - India’s Premium Auction House'";


                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        if (dTable.Rows[i]["auctiondate"].ToString() != "TBA")
                        {
                            dTable.Rows[i]["DisplayDate"] = dTable.Rows[i]["DisplayMonth"].ToString().Trim() + " " + dTable.Rows[i]["DateFrom"].ToString().Trim() + " - " + dTable.Rows[i]["DisplayMonthTo"].ToString().Trim() + " " + dTable.Rows[i]["DateTo"].ToString().Trim() + ", " + dTable.Rows[i]["DateYear"].ToString().Trim();

                            DateTime starting = Convert.ToDateTime(dTable.Rows[i]["AuctionDate1"].ToString().Trim());
                            AuctionStating = starting.ToString();
                            DateTime Ending = starting.AddDays(1);
                            AuctionEnding = Ending.ToString();
                        }
                        else
                        {
                            dTable.Rows[i]["AuctionDate1"] = "TBA";

                            dTable.Rows[i]["DisplayDate"] = "TBA";

                        }
                        string Calender = "'start':'" + AuctionStating.Replace(" 12:00:00 AM", "") + "','end':'" + AuctionEnding.Replace(" 12:00:00 AM", "") + "','timezone':'Asia/Kolkata','title':'" + dTable.Rows[0]["AuctionName"].ToString().Trim() + "','description':'http://livedomain/auctions/upcoming/2022/Modern-Indian-art-1','location':'','organizer':'AstaGuru - India’s Premium Auction House'";

                        strAuction = strAuction + "{'AuctionTitle' : '" + dTable.Rows[i]["AuctionName"].ToString().Trim() + "','AuctionDate' : '" + dTable.Rows[i]["AuctionDate1"].ToString().Trim() + "','AuctionId' : '" + dTable.Rows[i]["AuctionId"].ToString().Trim() + "','DisplayDate' : '" + dTable.Rows[i]["DisplayDate"].ToString().Trim() + "','CataloguePDF' : '" + CataloguePDF + "','WalkThrough3D' : '" + WalkThrough3D + "','Banner' : '" + Banner + "','Calendar':{" + Calender + "}}";
                        if (i + 1 != dTable.Rows.Count)
                        {
                            strAuction = strAuction + ",";
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        if (dTable.Rows[i]["auctiondate"].ToString() != "TBA")
                        {
                            dTable.Rows[i]["DisplayDate"] = dTable.Rows[i]["DisplayMonth"].ToString().Trim() + " " + dTable.Rows[i]["DateFrom"].ToString().Trim() + "-" + dTable.Rows[i]["DateTo"].ToString().Trim() + ", " + dTable.Rows[i]["DateYear"].ToString().Trim();
                            DateTime starting = Convert.ToDateTime(dTable.Rows[i]["AuctionDate1"].ToString().Trim());
                            AuctionStating = starting.ToString();
                            DateTime Ending = starting.AddDays(1);
                            AuctionEnding = Ending.ToString();
                        }
                        else
                        {
                            dTable.Rows[i]["AuctionDate1"] = "TBA";

                            dTable.Rows[i]["DisplayDate"] = "TBA";
                        }

                        strAuction = strAuction + "{'AuctionTitle' : '" + dTable.Rows[i]["AuctionName"].ToString().Trim() + "','AuctionDate' : '" + dTable.Rows[i]["AuctionDate1"].ToString().Trim() + "','AuctionId' : '" + dTable.Rows[i]["AuctionId"].ToString().Trim() + "','DisplayDate' : '" + dTable.Rows[i]["DisplayDate"].ToString().Trim() + "','CataloguePDF' : '" + CataloguePDF + "','WalkThrough3D' : '" + WalkThrough3D + "','Banner' : '" + Banner + "'}";
                        if (i + 1 != dTable.Rows.Count)
                        {
                            strAuction = strAuction + ",";
                        }
                    }
                }
                //strAuction = strAuction + "'auctions' : [";


                // strAuction = strAuction + "]";
                //strAuction = strAuction + "}";

                return strAuction;
            }
            else
            {
                return strAuction;
            }

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            //sConnCRM.Close();
            //sConnCRM.Dispose();
        }

    }




    public static string replacesplchar(string value)
    {
        string strval = value;
        if (strval != "" && strval != null)
        {
            strval = strval.Replace(" ", "-");
            strval = strval.Replace("''", "");
            strval = strval.Replace(".", "");
            strval = strval.Replace(":", "");
            strval = strval.Replace(",", "");
            strval = strval.Replace("&", "");
            strval = strval.Replace("!", "");
            strval = strval.Replace("?", "");
            strval = strval.Replace("(", "");
            strval = strval.Replace(")", "");
            strval = strval.Replace("@", "");
            strval = strval.Replace("#", "");
            strval = strval.Replace("$", "");
            strval = strval.Replace("%", "");
            strval = strval.Replace("^", "");
            strval = strval.Replace("*", "");
            strval = strval.Replace("<", "");
            strval = strval.Replace(">", "");
            strval = strval.Replace("+", "");
            strval = strval.Replace("=", "");
            strval = strval.Replace("\\", "");
            strval = strval.Replace("/", "-");
            strval = strval.Replace("{", "");
            strval = strval.Replace("}", "");
            strval = strval.Replace("[", "");
            strval = strval.Replace("]", "");
            strval = strval.Replace("`", "");
            strval = strval.Replace("~", "");
            strval = strval.Replace("'", "");
        }
        return strval;

    }

    //
    #region Upcoming_Lots
    public static string Upcoming_Lots(string AuctionId, string userid, string AuctionStatus) //
    {
        DataTable db_mail = new DataTable();
        //db_mail = BasicFunction.GetDetailsByDatatableCRM("select AL.AuctionId,AL.Auctionname as 'title',AL.DollarRate,REPLACE(AL.status, 'UpComming', 'UpComing') as status,ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_StartDate,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId where ML.DeletedBy is null or ML.DeletedBy=''  and ML.Lot_AuctionId='" + AuctionId + "'");
        db_mail = BasicFunction.GetDetailsByDatatableCRM(@"select ICD.AntiquecraftsDescription,AM.auctionSeq_ReferenceNo,IC.id as ProductNo,AL.AuctionId,AL.Auctionname as 'title',
AL.DollarRate, REPLACE(AL.status, 'UpComming', 'UpComing') as status,
ML.Lot_ID, ML.Lot_Name as 'Lot_Name1', ML.Lot_Bid_StartDate, ML.Lot_Bid_CloseDate, ICD.EstimateInRs as Lot_Bid_EstimateFrom,
ICD.EstimateInUs as Lot_Bid_EstimateTo,
IC.ItemImagesURL, IC.[3DImageURL] as 'ThreeD', IC.Value, IC.title as 'Lot_Name'
from AuctionListMaster AL, ManageLotMaster ML, AuctionProductInventoryMaster AM,
InventoryCategories IC,InventoryCategoryDetails ICD
where IC.InventoryId=ICD.InventoryID
and AL.AuctionId = ML.Lot_AuctionId
and AM.AuctionId = AL.AuctionId
and AM.auctionlot_id = ML.Lot_ID
and AM.auctioninventoryid = IC.InventoryID
and IC.isactive = '1'
and(ML.DeletedBy is null or ML.DeletedBy = '')  and AM.AuctionId = '" + AuctionId + "' order by AM.auctionSeq_ReferenceNo ");


        //
        DataTable db_auth = new DataTable();
        db_auth = BasicFunction.GetDetailsByDatatable("select * from user_details where Id=" +  userid + " and is_active=1");
        //
        string LotLike = "";
        List<string> address = new List<string>();
        string Lot_Bid_StartYear = string.Empty;


        if (db_mail.Rows.Count > 0)
        {
            Lot_Bid_StartYear = (DateTime.Parse(db_mail.Rows[0]["Lot_Bid_StartDate"].ToString()).Year).ToString() + "/" + db_mail.Rows[0]["title"].ToString().Replace(" ", "-") + "-" + db_mail.Rows[0]["AuctionId"].ToString();
        }
        else
        {
            Lot_Bid_StartYear = "1900";
        }

        if (db_auth.Rows.Count > 0)
        {
            if (db_mail.Rows.Count > 0)
            {
                DataTable db_lot = new DataTable();
                string access = db_auth.Rows[0]["interested_in_bidding"].ToString();
                string pan_card_num = db_auth.Rows[0]["pan_card_num"].ToString();
                string adhaar_card_num = db_auth.Rows[0]["adhaar_card_num"].ToString();
                string passport_num = db_auth.Rows[0]["passport_num"].ToString();
                string Status = db_mail.Rows[0]["status"].ToString();
                string Access = string.Empty;
                string Reason = string.Empty;
                string ProxyStatus = string.Empty;
                string ExportType = string.Empty;

                if (access == "Yes")
                {
                    Access = "true";

                }
                else
                {
                    if ((pan_card_num != null && adhaar_card_num != null) || (passport_num != null))
                    {
                        Access = "false";
                        Reason = "KYC";
                    }
                    else
                    {
                        Access = "false";
                        Reason = "Block";
                    }
                }

                for (int i = 0; i < db_mail.Rows.Count; i++)
                {
                    //
                    string Lot_NO = db_mail.Rows[i]["ProductNo"].ToString();
                    string Prd_NO = db_mail.Rows[i]["ProductNo"].ToString();
                    string Lot_Title = db_mail.Rows[i]["Lot_Name"].ToString();
                    double DollarRate = Convert.ToDouble(db_mail.Rows[i]["DollarRate"].ToString());
                    //
                    double ProxyBidAmount = 0;
                    //
                    string Half_Details = Category_List(Prd_NO, Lot_Title);
                    //
                    DataTable db_proxy = new DataTable();
                    db_proxy = BasicFunction.GetDetailsByDatatableCRM("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + " and UserId=" + "" + userid + "" + " and isActive=1");

                    if (db_proxy.Rows.Count > 0)
                    {

                        ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                        ProxyStatus = "Pending";

                        if (db_proxy.Rows[0]["Flag"].ToString() == "True")
                        {
                            ProxyStatus = "Approved";
                        }
                        ProxyBidAmount = Convert.ToDouble(db_proxy.Rows[0]["ProxyBidAmount"].ToString());

                    }
                    else
                    {
                        ProxyStatus = "CanBid";
                    }
                    //

                    db_lot = BasicFunction.GetDetailsByDatatable("select LotLike from tbl_UpdateLotToWishList where LotId='" + Lot_NO + "'  and userid='" + userid + "'");
                    if (db_lot.Rows.Count > 0)
                    {
                        if (db_lot.Rows[0]["LotLike"].ToString() == "1")
                        {
                            //LotLike = db_lot.Rows[0]["LotLike"].ToString();
                            LotLike = "true";
                        }
                        else
                        {
                            LotLike = "false";
                        }
                    }
                    else
                    {
                        LotLike = "false";
                    }
                    double EstimatedFrom = Convert.ToDouble(db_mail.Rows[i]["Lot_Bid_EstimateFrom"].ToString());
                    double EstimatedTo = Convert.ToDouble(db_mail.Rows[i]["Lot_Bid_EstimateTo"].ToString());
                    //
                    DataTable db_Lot = new DataTable();

                    //db_Lot = BasicFunction.GetDetailsByDatatableCRM("select ICD.NonExportable,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',IC.Value,IC.Title from InventoryCategories IC join InventoryCategoryDetails ICD on IC.LotNo=ICD.LotNo where IC.LotNo=" + "" + Lot_NO + "" + "and IC.IsActive=1");
                    db_Lot = BasicFunction.GetDetailsByDatatableCRM("select ICD.NonExportable,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',IC.Value as Value,IC.Title from InventoryCategories IC join InventoryCategoryDetails ICD on IC.InventoryId=ICD.InventoryID where IC.ID=" + "" + Lot_NO + "" + "and IC.IsActive=1");

                    string ItemImagesURL = "";
                    string Three_DImageURL = "";
                    string Value = "";
                    string Title = "";
                    double OpeningBid = 0;
                    if (db_Lot.Rows.Count > 0)
                    {
                        ItemImagesURL = db_Lot.Rows[0]["ItemImagesURL"].ToString();

                        JArray textArray = JArray.Parse(ItemImagesURL);
                        ItemImagesURL = textArray[0].ToString();
                        JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

                        if (jsonMobileobj.Count > 0)
                        {
                            ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
                        }
                        ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();

                        Three_DImageURL = db_mail.Rows[i]["ThreeD"].ToString();
                        if (string.IsNullOrEmpty(db_Lot.Rows[0]["Value"].ToString()))
                        {
                            OpeningBid = 0;
                        }
                        else
                        {
                            OpeningBid = Convert.ToDouble(db_Lot.Rows[0]["Value"].ToString());
                        }
                        Title = db_Lot.Rows[0]["Title"].ToString();
                        ExportType = db_Lot.Rows[0]["NonExportable"].ToString();
                        if (ExportType == "Yes")
                        {
                            ExportType = "NonExportable";
                        }
                        else
                        {
                            ExportType = "International";
                        }
                    }
                    var Openingbid = Math.Round(OpeningBid, 0);
                    var FinalOpeningBid = Math.Round(OpeningBid / DollarRate, 0);
                    var FinalEstimatedFrom = Math.Round(EstimatedFrom / DollarRate, 0);
                    var FinalEstimatedTo = Math.Round(EstimatedTo / DollarRate, 0);
                    var FinalProxyBid = Math.Round(ProxyBidAmount / DollarRate, 0);

                    //LIVE SECTION 

                    DataTable db_Live = new DataTable();
                    Double bidAmount = 0;
                    db_Live = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where Flag='1' and LotId='" + db_mail.Rows[i]["ProductNo"].ToString() + "'");

                    DataTable db_Live_User = new DataTable();
                    // Double bidAmount = 0;
                    db_Live_User = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where userid=" + userid + "  and LotId='" + db_mail.Rows[i]["ProductNo"].ToString() + "'");
                    string Live_detail = "";

                    //26.5.2022

                    DataTable db_Time = new DataTable();
                    db_Time = BasicFunction.GetDetailsByDatatableCRM("select CONVERT(char(10),cast(Prod_Bid_CloseDate as datetime) ,126) as 'date' , Prod_Bid_CloseTime as 'time' from AuctionProductInventoryMaster where IsActive='1' and auctionId='" + AuctionId + "'");

                    string remaining_second = "0";
                    if (db_Time.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(db_Time.Rows[0]["date"].ToString());
                        TimeSpan time = TimeSpan.Parse(db_Time.Rows[0]["time"].ToString());

                        DateTime startDate = date + time;
                        DateTime endDate = DateTime.Now;
                        var TotalMinutes = startDate.Subtract(endDate).TotalSeconds;
                        remaining_second = (Convert.ToInt32(TotalMinutes)).ToString();
                    }

                    if (db_Live.Rows.Count > 0)
                    {
                        DateTime Before_Last_Time = Convert.ToDateTime(db_Live.Rows[0]["LiveBidTiming"].ToString());
                        double liveamount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        double proxyamount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        double max_value = Math.Max(liveamount, proxyamount);

                        if (Convert.ToInt32(remaining_second) < 180 && Convert.ToInt32(remaining_second) > 0)
                        {
                            DataTable db_LastProxy = new DataTable();
                            db_LastProxy = BasicFunction.GetDetailsByDatatable("select * from tbl_InsertTimeLimitBid where LotId=" + db_mail.Rows[i]["ProductNo"].ToString() + " and Amount='" + liveamount + "'");
                            if (db_LastProxy.Rows.Count == 0)
                            {

                                remaining_second = "180";

                                string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                                SqlConnection conn = new SqlConnection();
                                SqlCommand cmdInsert;
                                conn.ConnectionString = connStr;
                                cmdInsert = new SqlCommand("sp_InsertLastBidAmpount", conn);
                                cmdInsert.CommandType = CommandType.StoredProcedure;

                                cmdInsert.Parameters.AddWithValue("@LotId", Convert.ToInt32(db_mail.Rows[i]["ProductNo"].ToString()));
                                cmdInsert.Parameters.AddWithValue("@Amount", liveamount);
                                cmdInsert.Parameters.AddWithValue("@Bid_Time", DateTime.Now);

                                conn.Open();
                                cmdInsert.ExecuteNonQuery();

                            }
                        }
                    }


                    //

                    if (db_Live.Rows.Count > 0 && db_Live_User.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        }
                        else
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        }
                        string UserStatus = "";
                        string IsOutBid = "";
                        if (db_Live_User.Rows[0]["Flag"].ToString() == "True")
                        {
                            UserStatus = "Leading";
                            IsOutBid = "false";
                        }
                        else
                        {
                            UserStatus = "CanBid";
                            IsOutBid = "true";
                        }

                        var first = Math.Round(bidAmount / 60, 0);
                        var second = Math.Round(bidAmount / 60, 0);
                        var third = Math.Round(bidAmount * 1.15, 0);
                        var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                        var fifth = Math.Round(((bidAmount * 1.50) * 1.10), 0);

                        var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                        var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                        var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                        var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                        var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                        Live_detail = "'LeadingUser':{'Id':'" + db_Live.Rows[0]["UserId"].ToString() + "','Bid':{'INR':'" + bidAmount + "','USD':'" + first + "'},'Notes':'(Inclusive 15% margin)'},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        if (AuctionStatus == "UpComming")
                        {
                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        }

                        //
                    }
                    else
                    {
                        var opening_one = Math.Round(OpeningBid * 1.15, 0);
                        var opening_two = Math.Round((OpeningBid * 1.15) / 60, 0);
                        var opening_three = Math.Round((OpeningBid * 1.50) * 1.10, 0);
                        var opening_four = Math.Round(((OpeningBid * 1.50) * 1.10) / 60, 0);
                        var opening_five = Math.Round(((OpeningBid * 1.50) * 1.10) * 1.10, 0);
                        var opening_six = Math.Round((((OpeningBid * 1.50) * 1.10) * 1.10) / 60, 0);
                        var opening_seven = Math.Round((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10, 0);
                        var opening_eight = Math.Round(((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var opening_nine = Math.Round(((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var opening_ten = Math.Round((((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var opening_eleven = Math.Round((((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var opening_twelve = Math.Round(((((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                        Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'Status':'','IsOutBid':'','remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + Openingbid + "','USD':'" + FinalOpeningBid + "'},'NextValidBid':{'INR':'" + opening_one + "','USD':'" + opening_two + "'},'Next5ValidBid':[{'INR':'" + opening_three + "','USD':'" + opening_four + "'},{'INR':'" + opening_five + "','USD':'" + opening_six + "'},{'INR':'" + opening_seven + "','USD':'" + opening_eight + "'},{'INR':'" + opening_nine + "','USD':'" + opening_ten + "'},{'INR':'" + opening_eleven + "','USD':'" + opening_twelve + "'}]}";

                        if (db_Live.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                            }
                            else
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                            }
                            var second = Math.Round(bidAmount / 60, 0);
                            var third = Math.Round(bidAmount * 1.15, 0);
                            var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                            var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                            var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                            var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                            var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                            var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                            var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";

                        }

                    }

                    string Full_Detail = "{'LotId':'" + db_mail.Rows[i]["ProductNo"].ToString() + "','LotDesc':'" + db_mail.Rows[i]["title"].ToString() + "','ExportType':'" + ExportType + "','LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title.Replace(" ", "-").ToLower() + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[i]["auctionSeq_ReferenceNo"].ToString() + "','Url3D':'" + Three_DImageURL + "','LotTitle':'" + db_mail.Rows[i]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "'," + Live_detail + ",'ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyBidAmount + "','USD':'" + FinalProxyBid + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[i]["status"].ToString().Replace("UpComming", "UpComing") + "','BidIncrementPercentage':'10','OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}}";

                    if (!string.IsNullOrEmpty(Half_Details))
                    {
                        Full_Detail = "{'LotId':'" + db_mail.Rows[i]["ProductNo"].ToString() + "','LotDesc':'" + db_mail.Rows[i]["title"].ToString() + "','ExportType':'" + ExportType + "','LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title.Replace(" ", "-").ToLower() + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[i]["auctionSeq_ReferenceNo"].ToString() + "','Url3D':'" + Three_DImageURL + "','LotTitle':'" + db_mail.Rows[i]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "'," + Live_detail + ",'ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyBidAmount + "','USD':'" + FinalProxyBid + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[i]["status"].ToString().Replace("UpComming", "UpComing") + "','BidIncrementPercentage':'10','OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}," + Half_Details + "}";

                    }
                    address.Add(Full_Detail);
                }
                string str = string.Join(",", address);

                string Result = "" + str + "";
                return Result;
            }
            else
            {
                return string.Empty;
            }

        }
        else
        {
            if (db_mail.Rows.Count > 0)
            {
                DataTable db_lot = new DataTable();
                string access = null;
                string pan_card_num = null;
                string adhaar_card_num = null;
                string passport_num = null;
                string Access = string.Empty;
                string Reason = string.Empty;
                string ProxyStatus = string.Empty;
                string ExportType = string.Empty;

                if (access == "Yes")
                {
                    Access = "true";

                }
                else
                {
                    if ((pan_card_num != null && adhaar_card_num != null) || (passport_num != null))
                    {
                        Access = "false";
                        Reason = "KYC";
                    }
                    else
                    {
                        Access = "false";
                        Reason = "Block";
                    }
                }

                for (int i = 0; i < db_mail.Rows.Count; i++)
                {
                    //
                    string Lot_NO = db_mail.Rows[i]["ProductNo"].ToString();
                    string Lot_Title = db_mail.Rows[i]["Lot_Name"].ToString();
                    double DollarRate = Convert.ToDouble(db_mail.Rows[i]["DollarRate"].ToString());

                    //
                    string Half_Details = Category_List(Lot_NO, Lot_Title);
                    //
                    DataTable db_proxy = new DataTable();
                    db_proxy = BasicFunction.GetDetailsByDatatableCRM("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + "and UserId=" + "" + userid + "" + "and isActive=1");
                    int ProxyBidAmount = 0;
                    if (db_proxy.Rows.Count > 0)
                    {
                        ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                        ProxyStatus = "Pending";

                        if (db_proxy.Rows[0]["Flag"].ToString() == "True")
                        {
                            ProxyStatus = "Approved";
                        }

                        ProxyBidAmount = Convert.ToInt32(db_proxy.Rows[0]["ProxyBidAmount"].ToString());

                    }
                    else
                    {
                        ProxyStatus = "CanBid";
                    }
                    //

                    db_lot = BasicFunction.GetDetailsByDatatable("select LotLike from tbl_UpdateLotToWishList where LotId='" + Lot_NO + "'  and userid='" + userid + "'");
                    if (db_lot.Rows.Count > 0)
                    {
                        if (db_lot.Rows[0]["LotLike"].ToString() == "True")
                        {
                            //LotLike = db_lot.Rows[0]["LotLike"].ToString();
                            LotLike = "true";
                        }
                        else
                        {
                            LotLike = "false";
                        }
                    }
                    else
                    {
                        LotLike = "false";
                    }
                    double EstimatedFrom = Convert.ToDouble(db_mail.Rows[i]["Lot_Bid_EstimateFrom"].ToString());
                    double EstimatedTo = Convert.ToDouble(db_mail.Rows[i]["Lot_Bid_EstimateTo"].ToString());
                    //
                    DataTable db_Lot = new DataTable();

                    //db_Lot = BasicFunction.GetDetailsByDatatableCRM("select ICD.NonExportable,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',IC.Value,IC.Title from InventoryCategories IC join InventoryCategoryDetails ICD on IC.LotNo=ICD.LotNo where IC.LotNo='" + Lot_NO + "' and IC.isActive=1");
                    db_Lot = BasicFunction.GetDetailsByDatatableCRM("select ICD.NonExportable,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',IC.Value as Value,IC.Title from InventoryCategories IC join InventoryCategoryDetails ICD on IC.InventoryId=ICD.InventoryID where IC.ID=" + "" + Lot_NO + "" + "and IC.IsActive=1");


                    string ItemImagesURL = "";
                    string Three_DImageURL = "";
                    string Value = "";
                    string Title = "";
                    double OpeningBid = 0;
                    if (db_Lot.Rows.Count > 0)
                    {
                        ItemImagesURL = db_Lot.Rows[0]["ItemImagesURL"].ToString();

                        JArray textArray = JArray.Parse(ItemImagesURL);
                        ItemImagesURL = textArray[0].ToString();
                        JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

                        if (jsonMobileobj.Count > 0)
                        {
                            ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
                        }
                        ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();

                        Three_DImageURL = db_mail.Rows[i]["ThreeD"].ToString();

                        if (!string.IsNullOrEmpty(db_Lot.Rows[0]["Value"].ToString()))
                        {

                            OpeningBid = Convert.ToDouble(db_Lot.Rows[0]["Value"].ToString());
                        }
                        else
                        {
                            OpeningBid = 0;
                        }

                        Title = db_Lot.Rows[0]["Title"].ToString();
                        ExportType = db_Lot.Rows[0]["NonExportable"].ToString();
                        if (ExportType == "Yes")
                        {
                            ExportType = "NonExportable";
                        }
                        else
                        {
                            ExportType = "International";
                        }
                    }
                    var Openingbid = Math.Round(OpeningBid, 0);
                    var FinalOpeningBid = Math.Round(OpeningBid / DollarRate, 0);
                    var FinalEstimatedFrom = Math.Round(EstimatedFrom / DollarRate, 0);
                    var FinalEstimatedTo = Math.Round(EstimatedTo / DollarRate, 0);




                    //db_proxy = new DataTable();
                    //db_proxy = BasicFunction.GetDetailsByDatatableCRM("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + "and UserId=" + "" + userid + "" + "and isActive=1");

                    //if (db_proxy.Rows.Count > 0)
                    //{
                    //    ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                    //    ProxyBidAmount = Convert.ToInt32(db_proxy.Rows[0]["ProxyBidAmount"].ToString());

                    //}
                    //else
                    //{
                    //    ProxyStatus = "CanBid";
                    //}

                    //FinalOpeningBid = Math.Round(OpeningBid / DollarRate, 0);
                    FinalEstimatedFrom = Math.Round(EstimatedFrom / DollarRate, 0);
                    FinalEstimatedTo = Math.Round(EstimatedTo / DollarRate, 0);
                    var FinalProxyBid = Math.Round(ProxyBidAmount / DollarRate, 0);

                    //LIVE SECTION 

                    DataTable db_Live = new DataTable();
                    Double bidAmount = 0;
                    db_Live = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where Flag='1' and LotId='" + db_mail.Rows[i]["ProductNo"].ToString() + "'");

                    DataTable db_Live_User = new DataTable();
                    // Double bidAmount = 0;
                    db_Live_User = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where userid=" + userid + "  and LotId='" + db_mail.Rows[i]["ProductNo"].ToString() + "'");

                    string Live_detail = "";

                    //26.05.2022

                    DataTable db_Time = new DataTable();
                    db_Time = BasicFunction.GetDetailsByDatatableCRM("select CONVERT(char(10),cast(Prod_Bid_CloseDate as datetime) ,126) as 'date' , Prod_Bid_CloseTime as 'time' from AuctionProductInventoryMaster where IsActive='1' and auctionId='" + AuctionId + "'");

                    string remaining_second = "0";
                    if (db_Time.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(db_Time.Rows[0]["date"].ToString());
                        TimeSpan time = TimeSpan.Parse(db_Time.Rows[0]["time"].ToString());

                        DateTime startDate = date + time;
                        DateTime endDate = DateTime.Now;
                        var TotalMinutes = startDate.Subtract(endDate).TotalSeconds;
                        remaining_second = (Convert.ToInt32(TotalMinutes)).ToString();
                    }

                    //
                    if (db_Live.Rows.Count > 0)
                    {
                        DateTime Before_Last_Time = Convert.ToDateTime(db_Live.Rows[0]["LiveBidTiming"].ToString());
                        double liveamount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        double proxyamount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        double max_value = Math.Max(liveamount, proxyamount);

                        if (Convert.ToInt32(remaining_second) < 180 && Convert.ToInt32(remaining_second) > 0)
                        {
                            DataTable db_LastProxy = new DataTable();
                            db_LastProxy = BasicFunction.GetDetailsByDatatable("select * from tbl_InsertTimeLimitBid where LotId=" + db_mail.Rows[i]["ProductNo"].ToString() + " and Amount=" + liveamount + "");
                            if (db_LastProxy.Rows.Count == 0)
                            {

                                remaining_second = "180";

                                string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                                SqlConnection conn = new SqlConnection();
                                SqlCommand cmdInsert;
                                conn.ConnectionString = connStr;
                                cmdInsert = new SqlCommand("sp_InsertLastBidAmpount", conn);
                                cmdInsert.CommandType = CommandType.StoredProcedure;

                                cmdInsert.Parameters.AddWithValue("@LotId", Convert.ToInt32(db_mail.Rows[i]["ProductNo"].ToString()));
                                cmdInsert.Parameters.AddWithValue("@Amount", liveamount);
                                cmdInsert.Parameters.AddWithValue("@Bid_Time", DateTime.Now);

                                conn.Open();
                                cmdInsert.ExecuteNonQuery();

                            }
                        }
                    }

                    //
                    //

                    if (db_Live.Rows.Count > 0 && db_Live_User.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        }
                        else
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        }
                        string UserStatus = "";
                        string IsOutBid = "";
                        if (db_Live_User.Rows[0]["Flag"].ToString() == "True")
                        {
                            UserStatus = "Leading";
                            IsOutBid = "false";
                        }
                        else
                        {
                            UserStatus = "CanBid";
                            IsOutBid = "true";
                        }
                        var first = Math.Round(bidAmount / 60, 0);
                        var second = Math.Round(bidAmount / 60, 0);
                        var third = Math.Round(bidAmount * 1.15, 0);
                        var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                        var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                        var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                        var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                        var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                        var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                        var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                        Live_detail = "'LeadingUser':{'Id':'" + db_Live.Rows[0]["UserId"].ToString() + "','Bid':{'INR':'" + bidAmount + "','USD':'" + first + "'},'Notes':'(Inclusive 15% margin)'},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        if (AuctionStatus == "UpComming")
                        {
                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        }
                        //
                    }
                    else
                    {
                        var opening_one = Math.Round(OpeningBid * 1.15, 0);
                        var opening_two = Math.Round((OpeningBid * 1.15) / 60, 0);
                        var opening_three = Math.Round((OpeningBid * 1.50) * 1.10, 0);
                        var opening_four = Math.Round(((OpeningBid * 1.50) * 1.10) / 60, 0);
                        var opening_five = Math.Round(((OpeningBid * 1.50) * 1.10) * 1.10, 0);
                        var opening_six = Math.Round((((OpeningBid * 1.50) * 1.10) * 1.10) / 60, 0);
                        var opening_seven = Math.Round((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10, 0);
                        var opening_eight = Math.Round(((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var opening_nine = Math.Round(((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var opening_ten = Math.Round((((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var opening_eleven = Math.Round((((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var opening_twelve = Math.Round(((((((OpeningBid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                        Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'Status':'','IsOutBid':'','remainingSeconds':'"+remaining_second+"','CurrentBid':{'INR':'" + Openingbid + "','USD':'" + FinalOpeningBid + "'},'NextValidBid':{'INR':'" + opening_one + "','USD':'" + opening_two + "'},'Next5ValidBid':[{'INR':'" + opening_three + "','USD':'" + opening_four + "'},{'INR':'" + opening_five + "','USD':'" + opening_six + "'},{'INR':'" + opening_seven + "','USD':'" + opening_eight + "'},{'INR':'" + opening_nine + "','USD':'" + opening_ten + "'},{'INR':'" + opening_eleven + "','USD':'" + opening_twelve + "'}]}";

                        if (db_Live.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                            }
                            else
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                            }
                           // var first = Math.Round(bidAmount, 0);
                            var second = Math.Round(bidAmount / 60, 0);
                            var third = Math.Round(bidAmount * 1.15, 0);
                            var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                            var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                            var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                            var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                            var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                            var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                            var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";

                        }

                    }



                    string Full_Detail = "{'LotId':'" + db_mail.Rows[i]["ProductNo"].ToString() + "','LotDesc':'" + db_mail.Rows[i]["title"].ToString() + "','ExportType':'" + ExportType + "','LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title.Replace(" ", "-").ToLower() + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[i]["auctionSeq_ReferenceNo"].ToString() + "','Url3D':'" + Three_DImageURL + "','LotTitle':'" + db_mail.Rows[i]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "'," + Live_detail + ",'ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyBidAmount + "','USD':'" + FinalProxyBid + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[i]["status"].ToString().Replace("UpComming", "UpComing") + "','BidIncrementPercentage':'10','OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}}";

                    if (!string.IsNullOrEmpty(Half_Details))
                    {
                        Full_Detail = "{'LotId':'" + db_mail.Rows[i]["ProductNo"].ToString() + "','LotDesc':'" + db_mail.Rows[i]["title"].ToString() + "','ExportType':'" + ExportType + "','LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title.Replace(" ", "-").ToLower() + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[i]["auctionSeq_ReferenceNo"].ToString() + "','Url3D':'" + Three_DImageURL + "','LotTitle':'" + db_mail.Rows[i]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "'," + Live_detail + ",'ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyBidAmount + "','USD':'" + FinalProxyBid + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[i]["status"].ToString().Replace("UpComming", "UpComing") + "','BidIncrementPercentage':'10','OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}," + Half_Details + "}";
                    }
                    address.Add(Full_Detail);
                }
                string str = string.Join(",", address);

                string Result = "" + str + "";
                return Result;
            }
            else
            {
                return string.Empty;
            }
        }

    }

    public static string LotDetail_Lots(string LotId, string userid, string LotNumber) //
    {
        //  DataTable db_mail = new DataTable();
        // db_mail = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.status,AL.auctiontitle as 'title',ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId where ML.Lot_AuctionId='" + AuctionId + "'");
        //
        DataTable db_auth = new DataTable();
        db_auth = BasicFunction.GetDetailsByDatatable("select * from user_details where Id=" + "" + userid + "" + "and is_active=1");
        //
        string LotLike = "";
        List<string> address = new List<string>();


        if (db_auth.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(LotId))
            {
                DataTable db_lot = new DataTable();
                string access = db_auth.Rows[0]["interested_in_bidding"].ToString();
                string pan_card_num = db_auth.Rows[0]["pan_card_num"].ToString();
                string adhaar_card_num = db_auth.Rows[0]["adhaar_card_num"].ToString();
                string passport_num = db_auth.Rows[0]["passport_num"].ToString();
                string Access = string.Empty;
                string Reason = string.Empty;
                string ProxyStatus = string.Empty;

                if (access == "Yes")
                {
                    Access = "true";

                }
                else
                {
                    if ((pan_card_num != null && adhaar_card_num != null) || (passport_num != null))
                    {
                        Access = "false";
                        Reason = "KYC";
                    }
                    else
                    {
                        Access = "false";
                        Reason = "Block";
                    }
                }

                //
                string Lot_NO = LotId;//db_mail.Rows[i]["Lot_ID"].ToString();
                DataTable db_mail = new DataTable();


                //db_mail = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.DollarRate,AL.status,AL.auctiontitle as 'title',ML.Lot_Description,ML.Lot_Bid_StartDate,ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo,ML.Lot_ID,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',ICD.NonExportable from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId join InventoryCategories IC on ML.Lot_ID=IC.LotNo join InventoryCategoryDetails ICD on IC.LotNo=ICD.LotNo where IC.ID='" + Lot_NO + "'");

                db_mail = BasicFunction.GetDetailsByDatatableCRM(@"select ICD.AntiquecraftsDescription as Lot_Description,AM.auctionSeq_ReferenceNo,'' as LotURL1,IC.id as ProductNo,AL.DollarRate,AL.status,AL.AuctionId,AL.auctiontitle as 'title',ICD.NonExportable,
AL.DollarRate, REPLACE(AL.status, 'UpComming', 'UpComing') as status,
ML.Lot_ID, ML.Lot_Name as 'Lot_Name1', ML.Lot_Bid_StartDate, ML.Lot_Bid_CloseDate, ICD.EstimateInRs as Lot_Bid_EstimateFrom,
ICD.EstimateInUs as Lot_Bid_EstimateTo,
IC.ItemImagesURL, IC.[3DImageURL] as 'ThreeD', IC.Value as Value, IC.title as 'Lot_Name',AL.auction_E_CatlogueLink
from AuctionListMaster AL, ManageLotMaster ML, AuctionProductInventoryMaster AM,
InventoryCategories IC, InventoryCategoryDetails ICD

where AL.AuctionId = ML.Lot_AuctionId
and AM.AuctionId = AL.AuctionId
and AM.auctionlot_id = ML.Lot_ID
and AM.auctioninventoryid = IC.InventoryID
and IC.InventoryID = ICD.InventoryID
and IC.isactive = '1'
and(ML.DeletedBy is null or ML.DeletedBy = '')  and IC.id = '" + Lot_NO + "' order by AM.auctionSeq_ReferenceNo");


                if (db_mail.Rows.Count > 0)
                {
                    string AuctionId = db_mail.Rows[0]["AuctionId"].ToString();

                    string sPDF = db_mail.Rows[0]["auction_E_CatlogueLink"].ToString();
                    string AuctionStatus = db_mail.Rows[0]["status"].ToString();

                    string Lot_Title = db_mail.Rows[0]["title"].ToString();
                    //
                    string Lot_Description = db_mail.Rows[0]["Lot_Description"].ToString();
                    //
                    string ItemImagesURL = db_mail.Rows[0]["ItemImagesURL"].ToString();

                    List<string> Image = new List<string>();
                    JArray textArray = JArray.Parse(ItemImagesURL);
                    for (int i = 0; i < textArray.Count; i++)
                    {
                        ItemImagesURL = textArray[i].ToString();
                        JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

                        if (jsonMobileobj.Count > 0)
                        {
                            ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
                        }
                        ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();
                        Image.Add(ItemImagesURL);
                    }
                    int count = 0;
                    if (Image.Count == 1)
                    {
                        count = 1;
                    }
                    else if (Image.Count == 2)
                    {
                        count = 1;
                    }
                    else if (Image.Count == 3)
                    {
                        count = 1;
                    }
                    //for (int i = 0; i < 4 - Image.Count; i++)
                    //{
                    //    Image[count + i] = "";
                    //}
                    Image.Add("");
                    Image.Add("");
                    Image.Add("");
                    Image.Add("");
                    //
                    string Three_D_URL = db_mail.Rows[0]["ThreeD"].ToString();
                    //
                    double DollarRate = Convert.ToDouble(db_mail.Rows[0]["DollarRate"].ToString());
                    //
                    string Lot_Bid_StartYear = (DateTime.Parse(db_mail.Rows[0]["Lot_Bid_StartDate"].ToString()).Year).ToString();
                    //
                    string ExportType = db_mail.Rows[0]["NonExportable"].ToString();
                    //
                    if (ExportType == "Yes")
                    {
                        ExportType = "NonExportable";
                    }
                    else
                    {
                        ExportType = "International";
                    }
                    double opningbid;
                    string Half_Details = Category_List_Detail(Lot_NO, Lot_Title, DollarRate, sPDF, out opningbid);
                    //
                    double ProxyAmount = 0;
                    //
                    DataTable db_proxy = new DataTable();
                    db_proxy = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + "and UserId=" + "" + userid + "" + "and isActive=1");

                    if (db_proxy.Rows.Count > 0)
                    {
                        ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                        ProxyStatus = "Pending";

                        if (db_proxy.Rows[0]["Flag"].ToString() == "True")
                        {
                            ProxyStatus = "Approved";
                        }

                        //if (ProxyStatus == "1")
                        //{
                        //    ProxyStatus = "true";
                        //}
                        //else
                        //{
                        //    ProxyStatus = "false";
                        //}


                        ProxyAmount = Convert.ToDouble(db_proxy.Rows[0]["ProxyBidAmount"].ToString());
                    }
                    else
                    {
                        ProxyStatus = "CanBid";
                    }
                    //
                    var FinalProxyAmount = Math.Round(ProxyAmount / DollarRate, 0);                 //

                    db_lot = BasicFunction.GetDetailsByDatatable("select LotLike from tbl_UpdateLotToWishList where LotId='" + Lot_NO + "' and userid='" + userid + "'");

                    if (db_lot.Rows.Count > 0)
                    {
                        if (db_lot.Rows[0]["LotLike"].ToString() == "1")
                        {
                            //LotLike = db_lot.Rows[0]["LotLike"].ToString();
                            LotLike = "true";
                        }
                        else
                        {
                            LotLike = "false";
                        }
                    }
                    else
                    {
                        LotLike = "false";
                    }

                    double EstimatedFrom = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateFrom"].ToString());
                    double EstimatedTo = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateTo"].ToString());


                    var FinalEstimatedFrom = Math.Round(EstimatedFrom / DollarRate, 0);
                    var FinalEstimatedTo = Math.Round(EstimatedTo / DollarRate, 0);

                    //LIVE SECTION 

                    DataTable db_Live = new DataTable();
                    Double bidAmount = 0;
                    db_Live = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where Flag='1' and LotId='" + Lot_NO + "'");

                    DataTable db_Live_User = new DataTable();
                    // Double bidAmount = 0;
                    db_Live_User = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where userid=" + userid + "  and LotId='" + Lot_NO + "'");
                    string Live_detail = "";


                    //26.5.2022

                    DataTable db_Time = new DataTable();
                    db_Time = BasicFunction.GetDetailsByDatatableCRM("select CONVERT(char(10),cast(Prod_Bid_CloseDate as datetime) ,126) as 'date' , Prod_Bid_CloseTime as 'time' from AuctionProductInventoryMaster where IsActive='1' and auctionId='" + AuctionId + "'");

                    string remaining_second = "0";
                    if (db_Time.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(db_Time.Rows[0]["date"].ToString());
                        TimeSpan time = TimeSpan.Parse(db_Time.Rows[0]["time"].ToString());

                        DateTime startDate = date + time;
                        DateTime endDate = DateTime.Now;
                        var TotalMinutes = startDate.Subtract(endDate).TotalSeconds;
                        remaining_second = (Convert.ToInt32(TotalMinutes)).ToString();
                    }

                    //
                    if (db_Live.Rows.Count > 0)
                    {
                        DateTime Before_Last_Time = Convert.ToDateTime(db_Live.Rows[0]["LiveBidTiming"].ToString());
                        double liveamount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        double proxyamount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        double max_value = Math.Max(liveamount, proxyamount);

                        if (Convert.ToInt32(remaining_second) < 180 && Convert.ToInt32(remaining_second) > 0)
                        {
                            DataTable db_LastProxy = new DataTable();
                            db_LastProxy = BasicFunction.GetDetailsByDatatable("select * from tbl_InsertTimeLimitBid where LotId=" + Lot_NO + " and Amount=" + liveamount + "");
                            if (db_LastProxy.Rows.Count == 0)
                            {

                                remaining_second = "180";

                                string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                                SqlConnection conn = new SqlConnection();
                                SqlCommand cmdInsert;
                                conn.ConnectionString = connStr;
                                cmdInsert = new SqlCommand("sp_InsertLastBidAmpount", conn);
                                cmdInsert.CommandType = CommandType.StoredProcedure;

                                cmdInsert.Parameters.AddWithValue("@LotId", Convert.ToInt32(Lot_NO));
                                cmdInsert.Parameters.AddWithValue("@Amount", liveamount);
                                cmdInsert.Parameters.AddWithValue("@Bid_Time", DateTime.Now);

                                conn.Open();
                                cmdInsert.ExecuteNonQuery();

                            }
                        }
                    }

                    //

                    //

                    if (db_Live.Rows.Count > 0 && db_Live_User.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        }
                        else
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        }
                        string UserStatus = "";
                        string IsOutBid = "";
                        if (db_Live_User.Rows[0]["Flag"].ToString() == "True")
                        {
                            UserStatus = "Leading";
                            IsOutBid = "false";
                        }
                        else
                        {
                            UserStatus = "CanBid";
                            IsOutBid = "true";
                        }

                        var first = Math.Round(bidAmount / 60, 0);
                        var second = Math.Round(bidAmount / 60, 0);
                        var third = Math.Round(bidAmount * 1.15, 0);
                        var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                        var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                        var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                        var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                        var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                        var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                        var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                        Live_detail = "'LeadingUser':{'Id':'" + db_Live.Rows[0]["UserId"].ToString() + "','Bid':{'INR':'" + bidAmount + "','USD':'" + first + "'},'Notes':'(Inclusive 15% margin)'},'LiveStatus':{'remainingSeconds':'"+remaining_second+"','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        if (AuctionStatus == "UpComming")
                        {
                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'"+remaining_second+"','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        }
                        //
                    }
                    else
                    {
                        var Openingbid = Math.Round(opningbid, 0);
                        var Openingbid_USD= Math.Round(opningbid/60, 0);
                        Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'Status':'','IsOutBid':'','remainingSeconds':'"+remaining_second+"','CurrentBid':{'INR':'" + Openingbid + "','USD':'" + Openingbid_USD + "'},'NextValidBid':{'INR':'" + opningbid * 1.15 + "','USD':'" + (opningbid * 1.15) / 60 + "'},'Next5ValidBid':[{'INR':'" + (opningbid * 1.50) * 1.10 + "','USD':'" + ((opningbid * 1.50) * 1.10) / 60 + "'},{'INR':'" + ((opningbid * 1.50) * 1.10) * 1.10 + "','USD':'" + (((opningbid * 1.50) * 1.10) * 1.10) / 60 + "'},{'INR':'" + (((opningbid * 1.50) * 1.10) * 1.10) * 1.10 + "','USD':'" + ((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) / 60 + "'},{'INR':'" + ((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10 + "','USD':'" + (((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60 + "'},{'INR':'" + (((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10 + "','USD':'" + ((((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60 + "'}]}";

                        if (db_Live.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                            }
                            else
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                            }
                            var second = Math.Round(bidAmount / 60, 0);
                            var third = Math.Round(bidAmount * 1.15, 0);
                            var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                            var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                            var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                            var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                            var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                            var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                            var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";

                        }

                    }


                    if (!string.IsNullOrEmpty(Half_Details))
                    {
                        string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["ProductNo"].ToString() + "','ExportType':'" + ExportType + "','LotDesc':'" + Lot_Description + "','LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[0]["auctionSeq_ReferenceNo"].ToString() + "'," + Live_detail + ",'Url3D':'" + Three_D_URL + "','Images':[{'ThumbImage':'" + Image[0] + "','BigImage':'" + Image[0] + "'},{'ThumbImage':'" + Image[1] + "','BigImage':'" + Image[1] + "'},{'ThumbImage':'" + Image[2] + "','BigImage':'" + Image[2] + "'}],'LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyAmount + "','USD':'" + FinalProxyAmount + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}," + Half_Details + "}";
                        address.Add(Full_Detail);
                    }
                    else
                    {
                        string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["ProductNo"].ToString() + "','ExportType':'" + ExportType + "','Url3D':'" + Three_D_URL + "','LotDesc':'" + db_mail.Rows[0]["title"].ToString() + "','LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[0]["auctionSeq_ReferenceNo"].ToString() + "'," + Live_detail + ",'Images':[{'ThumbImage':'" + Image[0] + "','BigImage':'" + Image[0] + "'},{'ThumbImage':'" + Image[1] + "','BigImage':'" + Image[1] + "'},{'ThumbImage':'" + Image[2] + "','BigImage':'" + Image[2] + "'}],'LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyAmount + "','USD':'" + FinalProxyAmount + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}}";
                        address.Add(Full_Detail);
                    }
                    string str = string.Join(",", address);

                    string Result = "" + str + "";
                    return Result;
                }
                else
                {
                    return string.Empty;
                }

            }
            else
            {
                return string.Empty;
            }

        }
        else
        {
            if (!string.IsNullOrEmpty(LotId))
            {
                DataTable db_lot = new DataTable();
                string access = null;
                string pan_card_num = null;
                string adhaar_card_num = null;
                string passport_num = null;
                string Access = string.Empty;
                string Reason = string.Empty;
                string ProxyStatus = string.Empty;

                if (access == "Yes")
                {
                    Access = "true";

                }
                else
                {
                    if ((pan_card_num != null && adhaar_card_num != null) || (passport_num != null))
                    {
                        Access = "false";
                        Reason = "KYC";
                    }
                    else
                    {
                        Access = "false";
                        Reason = "Block";
                    }
                }
                //
                string Lot_NO = LotId.ToString();
                DataTable db_mail = new DataTable();
                //db_mail = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.DollarRate,AL.status,AL.auctiontitle as 'title',ML.Lot_Description,ML.Lot_Bid_StartDate,ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo,ML.Lot_ID,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',ICD.NonExportable from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId join InventoryCategories IC on ML.Lot_ID=IC.LotNo join InventoryCategoryDetails ICD on IC.LotNo=ICD.LotNo where IC.ID='" + Lot_NO + "'");

                db_mail = BasicFunction.GetDetailsByDatatableCRM(@"select ICD.AntiquecraftsDescription as Lot_Description,AM.auctionSeq_ReferenceNo,'' as LotURL1,IC.id as ProductNo,AL.DollarRate,AL.status,AL.AuctionId,AL.auctiontitle as 'title',ICD.NonExportable,
AL.DollarRate, REPLACE(AL.status, 'UpComming', 'UpComing') as status,
ML.Lot_ID, ML.Lot_Name as 'Lot_Name1', ML.Lot_Bid_StartDate, ML.Lot_Bid_CloseDate, ICD.EstimateInRs as Lot_Bid_EstimateFrom,
ICD.EstimateInUs as Lot_Bid_EstimateTo,
IC.ItemImagesURL, IC.[3DImageURL] as 'ThreeD', IC.Value as Value, IC.title as 'Lot_Name',AL.auction_E_CatlogueLink
from AuctionListMaster AL, ManageLotMaster ML, AuctionProductInventoryMaster AM,
InventoryCategories IC, InventoryCategoryDetails ICD
where AL.AuctionId = ML.Lot_AuctionId
and AM.AuctionId = AL.AuctionId
and AM.auctionlot_id = ML.Lot_ID
and AM.auctioninventoryid = IC.InventoryID
and IC.InventoryID = ICD.InventoryID
and IC.isactive = '1'
and(ML.DeletedBy is null or ML.DeletedBy = '')  and IC.id = '" + Lot_NO + "' order by AM.auctionSeq_ReferenceNo");

                if (db_mail.Rows.Count > 0)
                {
                    string AuctionId = db_mail.Rows[0]["AuctionId"].ToString();

                    string AuctionStatus = db_mail.Rows[0]["status"].ToString();

                    string sPDF = db_mail.Rows[0]["auction_E_CatlogueLink"].ToString();

                    string Lot_Title = db_mail.Rows[0]["title"].ToString();
                    //
                    string Lot_Description = db_mail.Rows[0]["Lot_Description"].ToString();
                    //
                    string ItemImagesURL = db_mail.Rows[0]["ItemImagesURL"].ToString();

                    List<string> Image = new List<string>();
                    JArray textArray = JArray.Parse(ItemImagesURL);
                    for (int i = 0; i < textArray.Count; i++)
                    {
                        ItemImagesURL = textArray[i].ToString();
                        JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

                        if (jsonMobileobj.Count > 0)
                        {
                            ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
                        }
                        ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();
                        Image.Add(ItemImagesURL);
                    }
                    int count = 0;
                    if (Image.Count == 1)
                    {
                        count = 1;
                    }
                    else if (Image.Count == 2)
                    {
                        count = 1;
                    }
                    else if (Image.Count == 3)
                    {
                        count = 1;
                    }
                    //for (int i = 0; i <4- Image.Count; i++)
                    //{
                    //    Image[count + i] = "";
                    //}
                    Image.Add("");
                    Image.Add("");
                    Image.Add("");
                    Image.Add("");
                    //JArray textArray = JArray.Parse(ItemImagesURL);
                    //ItemImagesURL = textArray[0].ToString();
                    //JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

                    //if (jsonMobileobj.Count > 0)
                    //{
                    //    ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
                    //}
                    //ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();

                    //
                    string Three_D_URL = db_mail.Rows[0]["ThreeD"].ToString();
                    //
                    double DollarRate = Convert.ToDouble(db_mail.Rows[0]["DollarRate"].ToString());
                    //
                    string Lot_Bid_StartYear = (DateTime.Parse(db_mail.Rows[0]["Lot_Bid_StartDate"].ToString()).Year).ToString();
                    //
                    string ExportType = db_mail.Rows[0]["NonExportable"].ToString();
                    if (ExportType == "Yes")
                    {
                        ExportType = "NonExportable";
                    }
                    else
                    {
                        ExportType = "International";
                    }
                    //
                    double opningbid;
                    string Half_Details = Category_List_Detail(Lot_NO, Lot_Title, DollarRate, sPDF, out opningbid);
                    //

                    DataTable db_proxy = new DataTable();
                    db_proxy = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + "and UserId=" + "" + userid + "" + "and isActive=1");
                    double ProxyAmount = 0;
                    if (db_proxy.Rows.Count > 0)
                    {
                        ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                        ProxyStatus = "Pending";

                        if (db_proxy.Rows[0]["Flag"].ToString() == "True")
                        {
                            ProxyStatus = "Approved";
                        }

                        ProxyAmount = Convert.ToDouble(db_proxy.Rows[0]["ProxyBidAmount"].ToString());
                    }
                    else
                    {
                        ProxyStatus = "CanBid";
                    }
                    //
                    var FinalProxyAmount = Math.Round(ProxyAmount / DollarRate, 0);
                    //

                    db_lot = BasicFunction.GetDetailsByDatatable("select LotLike from tbl_UpdateLotToWishList where LotId='" + Lot_NO + "'  and userid='" + userid + "'");


                    if (db_lot.Rows.Count > 0)
                    {
                        if (db_lot.Rows[0]["LotLike"].ToString() == "1")
                        {
                            //LotLike = db_lot.Rows[0]["LotLike"].ToString();
                            LotLike = "true";
                        }
                        else
                        {
                            LotLike = "false";
                        }
                    }
                    else
                    {
                        LotLike = "false";
                    }

                    double EstimatedFrom = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateFrom"].ToString());
                    double EstimatedTo = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateTo"].ToString());


                    var FinalEstimatedFrom = Math.Round(EstimatedFrom / DollarRate, 0);
                    var FinalEstimatedTo = Math.Round(EstimatedTo / DollarRate, 0);
                   
                    //LIVE SECTION 
                    DataTable db_Live = new DataTable();
                    Double bidAmount = 0;
                    db_Live = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where Flag='1' and LotId='" + Lot_NO + "'");

                    DataTable db_Live_User = new DataTable();
                    // Double bidAmount = 0;
                    db_Live_User = BasicFunction.GetDetailsByDatatable("select * from tbl_AuctionProxyBidInsert where userid=" + userid + "  and LotId='" + Lot_NO + "'");
                    string Live_detail = "";

                    //26.5.2022

                    DataTable db_Time = new DataTable();
                    db_Time = BasicFunction.GetDetailsByDatatableCRM("select CONVERT(char(10),cast(Prod_Bid_CloseDate as datetime) ,126) as 'date' , Prod_Bid_CloseTime as 'time' from AuctionProductInventoryMaster where IsActive='1' and auctionId='" + AuctionId + "'");

                    string remaining_second = "0";
                    if (db_Time.Rows.Count > 0)
                    {
                        DateTime date = Convert.ToDateTime(db_Time.Rows[0]["date"].ToString());
                        TimeSpan time = TimeSpan.Parse(db_Time.Rows[0]["time"].ToString());

                        DateTime startDate = date + time;
                        DateTime endDate = DateTime.Now;
                        var TotalMinutes = startDate.Subtract(endDate).TotalSeconds;
                        remaining_second = (Convert.ToInt32(TotalMinutes)).ToString();
                    }

                    //
                    if (db_Live.Rows.Count > 0)
                    {
                        DateTime Before_Last_Time = Convert.ToDateTime(db_Live.Rows[0]["LiveBidTiming"].ToString());
                        double liveamount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        double proxyamount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        double max_value = Math.Max(liveamount, proxyamount);

                        if (Convert.ToInt32(remaining_second) < 180 && Convert.ToInt32(remaining_second) > 0)
                        {
                            DataTable db_LastProxy = new DataTable();
                            db_LastProxy = BasicFunction.GetDetailsByDatatable("select * from tbl_InsertTimeLimitBid where LotId=" + Lot_NO + " and Amount=" + liveamount + "");
                            if (db_LastProxy.Rows.Count == 0)
                            {

                                remaining_second = "180";

                                string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                                SqlConnection conn = new SqlConnection();
                                SqlCommand cmdInsert;
                                conn.ConnectionString = connStr;
                                cmdInsert = new SqlCommand("sp_InsertLastBidAmpount", conn);
                                cmdInsert.CommandType = CommandType.StoredProcedure;

                                cmdInsert.Parameters.AddWithValue("@LotId", Convert.ToInt32(Lot_NO));
                                cmdInsert.Parameters.AddWithValue("@Amount", liveamount);
                                cmdInsert.Parameters.AddWithValue("@Bid_Time", DateTime.Now);

                                conn.Open();
                                cmdInsert.ExecuteNonQuery();

                            }
                        }
                    }

                    //

                    //

                    if (db_Live.Rows.Count > 0 && db_Live_User.Rows.Count > 0)
                    {

                        if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                        }
                        else
                        {
                            bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                        }
                        string UserStatus = "";
                        string IsOutBid = "";
                        if (db_Live_User.Rows[0]["Flag"].ToString() == "True")
                        {
                            UserStatus = "Leading";
                            IsOutBid = "false";
                        }
                        else
                        {
                            UserStatus = "CanBid";
                            IsOutBid = "true";
                        }

                        var first = Math.Round(bidAmount / 60, 0);
                        var second = Math.Round(bidAmount / 60, 0);
                        var third = Math.Round(bidAmount * 1.15, 0);
                        var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                        var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                        var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                        var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                        var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                        var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                        var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                        var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                        var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                        Live_detail = "'LeadingUser':{'Id':'" + db_Live.Rows[0]["UserId"].ToString() + "','Bid':{'INR':'" + bidAmount + "','USD':'" + first + "'},'Notes':'(Inclusive 15% margin)'},'LiveStatus':{'remainingSeconds':'"+remaining_second+"','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + first + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        if (AuctionStatus == "UpComming")
                        {
                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'"+remaining_second+"','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";
                        }
                        //
                    }
                    else
                    {
                        var Openingbid = Math.Round(opningbid, 0);
                        var Openingbid_USD = Math.Round(opningbid / 60, 0);
                        Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'Status':'','IsOutBid':'','remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + Openingbid + "','USD':'" + Openingbid_USD + "'},'NextValidBid':{'INR':'" + opningbid * 1.15 + "','USD':'" + (opningbid * 1.15) / 60 + "'},'Next5ValidBid':[{'INR':'" + (opningbid * 1.50) * 1.10 + "','USD':'" + ((opningbid * 1.50) * 1.10) / 60 + "'},{'INR':'" + ((opningbid * 1.50) * 1.10) * 1.10 + "','USD':'" + (((opningbid * 1.50) * 1.10) * 1.10) / 60 + "'},{'INR':'" + (((opningbid * 1.50) * 1.10) * 1.10) * 1.10 + "','USD':'" + ((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) / 60 + "'},{'INR':'" + ((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10 + "','USD':'" + (((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60 + "'},{'INR':'" + (((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10 + "','USD':'" + ((((((opningbid * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60 + "'}]}";

                        if (db_Live.Rows.Count > 0)
                        {
                            if (Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString()) >= Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString()))
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["ProxyBidAmount"].ToString());
                            }
                            else
                            {
                                bidAmount = Convert.ToDouble(db_Live.Rows[0]["LiveBidAmount"].ToString());
                            }
                            var second = Math.Round(bidAmount / 60, 0);
                            var third = Math.Round(bidAmount * 1.15, 0);
                            var fourth = Math.Round((bidAmount * 1.15) / 60, 0);
                            var fifth = Math.Round((bidAmount * 1.50) * 1.10, 0);
                            var sixth = Math.Round(((bidAmount * 1.50) * 1.10) / 60, 0);
                            var seventh = Math.Round(((bidAmount * 1.50) * 1.10) * 1.10, 0);
                            var eight = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) / 60, 0);
                            var nine = Math.Round((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10, 0);
                            var ten = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var eleven = Math.Round(((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var twelve = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);
                            var thirtin = Math.Round((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10, 0);
                            var fourtin = Math.Round(((((((bidAmount * 1.50) * 1.10) * 1.10) * 1.10) * 1.10) * 1.10) / 60, 0);

                            Live_detail = "'LeadingUser':{'Id':'','Bid':{'INR':'','USD':''},'Notes':''},'LiveStatus':{'remainingSeconds':'" + remaining_second + "','CurrentBid':{'INR':'" + bidAmount + "','USD':'" + second + "'},'NextValidBid':{'INR':'" + third + "','USD':'" + fourth + "'},'Next5ValidBid':[{'INR':'" + fifth + "','USD':'" + sixth + "'},{'INR':'" + seventh + "','USD':'" + eight + "'},{'INR':'" + nine + "','USD':'" + ten + "'},{'INR':'" + eleven + "','USD':'" + twelve + "'},{'INR':'" + thirtin + "','USD':'" + fourtin + "'}]}";

                        }

                    }


                    if (!string.IsNullOrEmpty(Half_Details))
                    {
                        string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["ProductNo"].ToString() + "','ExportType':'" + ExportType + "','LotDesc':'" + Lot_Description + "'," + Live_detail + ",'LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title + "-" + Lot_NO + "','Images':[{'ThumbImage':'" + Image[0] + "','BigImage':'" + Image[0] + "'},{'ThumbImage':'" + Image[1] + "','BigImage':'" + Image[1] + "'},{'ThumbImage':'" + Image[2] + "','BigImage':'" + Image[2] + "'}],'LotNumber':'" + db_mail.Rows[0]["auctionSeq_ReferenceNo"].ToString() + "','LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyAmount + "','USD':'" + FinalProxyAmount + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}," + Half_Details + "}";
                        address.Add(Full_Detail);
                    }
                    else
                    {
                        string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["ProductNo"].ToString() + "','Url3D':'" + Three_D_URL + "','LotDesc':'" + db_mail.Rows[0]["title"].ToString() + "'," + Live_detail + ",'LotURL':'" + Lot_Bid_StartYear + "/" + Lot_Title + "-" + Lot_NO + "','LotNumber':'" + db_mail.Rows[0]["auctionSeq_ReferenceNo"].ToString() + "','Images':'" + ItemImagesURL + "','Images':[{'ThumbImage':'" + Image[0] + "','BigImage':'" + Image[0] + "'},{'ThumbImage':'" + Image[1] + "','BigImage':'" + Image[1] + "'},{'ThumbImage':'" + Image[2] + "','BigImage':'" + Image[2] + "'}],'LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyAmount + "','USD':'" + FinalProxyAmount + "'}},'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}}";
                        address.Add(Full_Detail);
                    }
                    string str = string.Join(",", address);

                    string Result = "" + str + "";
                    return Result;
                }
                else
                {
                    return string.Empty;
                }

            }
            else
            {
                return string.Empty;
            }
        }

    }


    public static string SimilarUpcoming_Lots(List<string> LotList, string userid) //
    {
        //  DataTable db_mail = new DataTable();
        // db_mail = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.status,AL.auctiontitle as 'title',ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId where ML.Lot_AuctionId='" + AuctionId + "'");
        //
        DataTable db_auth = new DataTable();
        db_auth = BasicFunction.GetDetailsByDatatable("select * from user_details where Id=" + "" + userid + "" + "and is_active=1");
        //
        string LotLike = "";
        List<string> address = new List<string>();


        if (db_auth.Rows.Count > 0)
        {
            if (LotList.Count > 0)
            {
                DataTable db_lot = new DataTable();
                string access = db_auth.Rows[0]["interested_in_bidding"].ToString();
                string pan_card_num = db_auth.Rows[0]["pan_card_num"].ToString();
                string adhaar_card_num = db_auth.Rows[0]["adhaar_card_num"].ToString();
                string passport_num = db_auth.Rows[0]["passport_num"].ToString();
                string Access = string.Empty;
                string Reason = string.Empty;
                string ProxyStatus = string.Empty;

                if (access == "Yes")
                {
                    Access = "true";

                }
                else
                {
                    if ((pan_card_num != null && adhaar_card_num != null) || (passport_num != null))
                    {
                        Access = "false";
                        Reason = "KYC";
                    }
                    else
                    {
                        Access = "false";
                        Reason = "Block";
                    }
                }

                for (int i = 0; i < LotList.Count; i++)
                {
                    if (LotList[i] != "0")
                    {
                        //
                        string Lot_NO = LotList[i];//db_mail.Rows[i]["Lot_ID"].ToString();
                        DataTable db_mail = new DataTable();
                        db_mail = BasicFunction.GetDetailsByDatatableCRM("select ICD.AntiquecraftsDescription,'' as LotURL1,AL.DollarRate,AL.status,AL.auctiontitle as 'title',ML.Lot_Description,ML.Lot_Bid_StartDate,ML.Lot_ID,ML.Lot_Name,ICD.EstimateInRs as Lot_Bid_EstimateFrom,ICD.EstimateInUs as Lot_Bid_EstimateTo,ML.Lot_ID,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',ICD.NonExportable from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId join InventoryCategories IC on ML.Lot_ID=IC.LotNo join InventoryCategoryDetails ICD on IC.LotNo=ICD.LotNo where ML.Lot_ID='" + Lot_NO + "'");

                        if (db_mail.Rows.Count > 0)
                        {
                            string Lot_Title = db_mail.Rows[0]["title"].ToString();

                            //
                            string Lot_Bid_StartYear = (DateTime.Parse(db_mail.Rows[0]["Lot_Bid_StartDate"].ToString()).Year).ToString();
                            //
                            double DollarRate = Convert.ToDouble(db_mail.Rows[0]["DollarRate"].ToString());

                            //
                            string ExportType = db_mail.Rows[0]["NonExportable"].ToString();
                            if (ExportType == "Yes")
                            {
                                ExportType = "NonExportable";
                            }
                            else
                            {
                                ExportType = "International";
                            }
                            //
                            string Half_Details = Category_List(Lot_NO, Lot_Title);
                            //
                            DataTable db_proxy = new DataTable();
                            db_proxy = BasicFunction.GetDetailsByDatatableCRM("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + "and UserId=" + "" + userid + "" + "and isActive=1");

                            if (db_proxy.Rows.Count > 0)
                            {
                                ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                                ProxyStatus = "Pending";

                                if (db_proxy.Rows[0]["Flag"].ToString() == "True")
                                {
                                    ProxyStatus = "Approved";
                                }

                                int ProxyAmount = Convert.ToInt32(db_proxy.Rows[0]["ProxyBidAmount"].ToString());
                                var USDPrice = Math.Round(ProxyAmount / DollarRate, 0);
                                ProxyStatus = "{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyAmount + "','USD':'" + USDPrice + "'}}";
                            }
                            else
                            {
                                ProxyStatus = "CanBid";
                                ProxyStatus = "{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'0','USD':'0'}}";
                            }
                            //

                            db_lot = BasicFunction.GetDetailsByDatatable("select LotLike from tbl_UpdateLotToWishList where LotId='" + Lot_NO + "' and userid='" + userid + "'");
                            if (db_lot.Rows.Count > 0)
                            {
                                if (db_lot.Rows[0]["LotLike"].ToString() == "1")
                                {
                                    LotLike = "true";
                                }
                                else
                                {
                                    LotLike = "false";
                                }
                            }
                            else
                            {
                                LotLike = "";
                            }
                            double EstimatedFrom = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateFrom"].ToString());
                            double EstimatedTo = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateTo"].ToString());

                            if (!string.IsNullOrEmpty(Half_Details))
                            {
                                string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["Lot_ID"].ToString() + "','LotDesc':'" + db_mail.Rows[0]["title"].ToString() + "','LotURL':'" + Lot_Bid_StartYear + " / " + Lot_Title + " - " + Lot_NO + "','LotNumber':'" + (i + 1).ToString() + "','ExportType':'" + ExportType + "','LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':" + ProxyStatus + ",'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + EstimatedFrom / 60 + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + EstimatedTo / 60 + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + EstimatedFrom / 60 + "-" + EstimatedTo / 60 + "'}," + Half_Details + "}";
                                address.Add(Full_Detail);
                            }
                            else
                            {
                                string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["Lot_ID"].ToString() + "','LotDesc':'" + db_mail.Rows[0]["title"].ToString() + "','LotURL':'" + Lot_Bid_StartYear + " / " + Lot_Title + " - " + Lot_NO + "','LotNumber':'" + (i + 1).ToString() + "','ExportType':'" + ExportType + "','LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':" + ProxyStatus + ",'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + EstimatedFrom / 60 + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + EstimatedTo / 60 + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + EstimatedFrom / 60 + "-" + EstimatedTo / 60 + "'}}";
                                address.Add(Full_Detail);
                            }
                        }
                        //else
                        //{
                        //    string Full_Detail = "{}";
                        //    address.Add(Full_Detail);
                        //}
                    }
                    //else
                    //{
                    //    string Full_Detail = "{}";
                    //    address.Add(Full_Detail);
                    //}
                }
                string str = string.Join(",", address);

                string Result = "" + str + "";
                return Result;
            }
            else
            {
                return string.Empty;
            }

        }
        else
        {
            if (LotList.Count > 0)
            {
                DataTable db_lot = new DataTable();
                string access = null;
                string pan_card_num = null;
                string adhaar_card_num = null;
                string passport_num = null;
                string Access = string.Empty;
                string Reason = string.Empty;
                string ProxyStatus = string.Empty;

                if (access == "Yes")
                {
                    Access = "true";

                }
                else
                {
                    if ((pan_card_num != null && adhaar_card_num != null) || (passport_num != null))
                    {
                        Access = "false";
                        Reason = "KYC";
                    }
                    else
                    {
                        Access = "false";
                        Reason = "Block";
                    }
                }

                for (int i = 0; i < LotList.Count; i++)
                {
                    if (LotList[i] != "0")
                    {
                        //
                        string Lot_NO = LotList[i].ToString();
                        DataTable db_mail = new DataTable();
                        // db_mail = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.status,AL.auctiontitle as 'title',AL.DollarRate,ML.Lot_ID,ML.Lot_Bid_StartDate,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId where ML.Lot_ID='" + Lot_NO + "'");
                        db_mail = BasicFunction.GetDetailsByDatatableCRM("select ICD.AntiquecraftsDescription,'' as LotURL1,AL.DollarRate,AL.status,AL.auctiontitle as 'title',ML.Lot_Description,ML.Lot_Bid_StartDate,ML.Lot_ID,ML.Lot_Name,ICD.EstimateInRs as Lot_Bid_EstimateFrom,ICD.EstimateInUs as Lot_Bid_EstimateTo,ML.Lot_ID,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',ICD.NonExportable from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId join InventoryCategories IC on ML.Lot_ID=IC.LotNo join InventoryCategoryDetails ICD on IC.LotNo=ICD.LotNo where ML.Lot_ID='" + Lot_NO + "'");

                        if (db_mail.Rows.Count > 0)
                        {
                            string Lot_Title = db_mail.Rows[0]["title"].ToString();
                            //
                            string Half_Details = Category_List(Lot_NO, Lot_Title);
                            //
                            double DollarRate = Convert.ToDouble(db_mail.Rows[0]["DollarRate"].ToString());
                            //
                            string ExportType = db_mail.Rows[0]["NonExportable"].ToString();
                            if (ExportType == "Yes")
                            {
                                ExportType = "NonExportable";
                            }
                            else
                            {
                                ExportType = "International";
                            }
                            //
                            string Lot_Bid_StartYear = (DateTime.Parse(db_mail.Rows[0]["Lot_Bid_StartDate"].ToString()).Year).ToString();
                            //
                            DataTable db_proxy = new DataTable();
                            db_proxy = BasicFunction.GetDetailsByDatatableCRM("select * from tbl_AuctionProxyBidInsert where LotId=" + "" + Lot_NO + "" + "and UserId=" + "" + userid + "" + "and isActive=1");

                            if (db_proxy.Rows.Count > 0)
                            {
                                // ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                                ProxyStatus = db_proxy.Rows[0]["Status"].ToString();
                                ProxyStatus = "Pending";

                                if (db_proxy.Rows[0]["Flag"].ToString() == "True")
                                {
                                    ProxyStatus = "Approved";
                                }

                                int ProxyAmount = Convert.ToInt32(db_proxy.Rows[0]["ProxyBidAmount"].ToString());
                                var USDPrice = Math.Round(ProxyAmount / DollarRate, 0);
                                ProxyStatus = "{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'" + ProxyAmount + "','USD':'" + USDPrice + "'}}";

                            }
                            else
                            {
                                // ProxyStatus = "CanBid";
                                ProxyStatus = "CanBid";
                                ProxyStatus = "{'Status':'" + ProxyStatus + "','ProxyAmount':{'INR':'0','USD':'0'}}";

                            }
                            //

                            db_lot = BasicFunction.GetDetailsByDatatable("select LotLike from tbl_UpdateLotToWishList where LotId='" + Lot_NO + "'  and userid='" + userid + "'");
                            if (db_lot.Rows.Count > 0)
                            {
                                if (db_lot.Rows[0]["LotLike"].ToString() == "1")
                                {
                                    LotLike = "true";
                                }
                                else
                                {
                                    LotLike = "false";
                                }
                            }
                            else
                            {
                                LotLike = "";
                            }
                            double EstimatedFrom = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateFrom"].ToString());
                            double EstimatedTo = Convert.ToDouble(db_mail.Rows[0]["Lot_Bid_EstimateTo"].ToString());


                            var FinalEstimatedFrom = Math.Round(EstimatedFrom / DollarRate, 0);
                            var FinalEstimatedTo = Math.Round(EstimatedTo / DollarRate, 0);

                            if (!string.IsNullOrEmpty(Half_Details))
                            {
                                string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["Lot_ID"].ToString() + "','LotDesc':'" + db_mail.Rows[0]["title"].ToString() + "','ExportType':'" + ExportType + "','LotURL':'" + Lot_Bid_StartYear + " / " + Lot_Title/*.Replace("", "-")*/ + " - " + Lot_NO + "','LotNumber':'" + (i + 1).ToString() + "','Url3D':'tree - house - painting - 22','LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':" + ProxyStatus + ",'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}," + Half_Details + "}";
                                address.Add(Full_Detail);
                            }
                            else
                            {
                                string Full_Detail = "{'LotId':'" + db_mail.Rows[0]["Lot_ID"].ToString() + "','LotDesc':'" + db_mail.Rows[0]["title"].ToString() + "','ExportType':'" + ExportType + "','LotURL':'" + Lot_Bid_StartYear + " / " + Lot_Title/*.Replace("", "-")*/ + " - " + Lot_NO + "','LotNumber':'" + (i + 1).ToString() + "','Url3D':'tree - house - painting - 22','LotTitle':'" + db_mail.Rows[0]["Lot_Name"].ToString() + "','IsLiked':'" + LotLike + "','ProxyStatus':" + ProxyStatus + ",'BidAccess':{'Access':'" + Access + "','Reason':'" + Reason + "'},'Status':'" + db_mail.Rows[0]["status"].ToString() + "','EstimateFrom':{'INR':'" + EstimatedFrom + "','USD':'" + FinalEstimatedFrom + "'},'EstimateTo':{'INR':'" + EstimatedTo + "','USD':'" + FinalEstimatedTo + "'},'EstimateDisplayText':{'INR':'" + EstimatedFrom + "-" + EstimatedTo + "','USD':'" + FinalEstimatedFrom + "-" + FinalEstimatedTo + "'}}";
                                address.Add(Full_Detail);

                            }
                        }
                        //else
                        //{
                        //    string Full_Detail = "";
                        //    address.Add(Full_Detail);
                        //}

                    }
                    //else
                    //{
                    //    string Full_Detail = "{}";
                    //    address.Add(Full_Detail);
                    //}
                }
                string str = string.Join(",", address);

                string Result = "" + str + "";
                return Result;
            }
            else
            {
                return string.Empty;
            }
        }

    }

    //public static string Sub_Category(string subCategory)//
    //{
    //    return subCategory;
    //}
    public static string Category_List(string Lot_Id, string Lot_Title)// 
    {
        DataTable db_Category = new DataTable();
        db_Category = BasicFunction.GetDetailsByDatatableCRM("select CM.CategoryName,IC.InventoryID,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',IC.CategoryID from CategoryMaster CM join InventoryCategories IC on CM.CategoryID=IC.CategoryID where IC.id='" + Lot_Id + "'");
        string CategoryName = "";
        string InventoryId = "";
        string ItemImagesURL = "";
        string Three_D_URL = "";
        string CategoryID = "";
        string Details = "";
        string Lot_Detail = "";

        if (db_Category.Rows.Count > 0)
        {
            CategoryName = db_Category.Rows[0]["CategoryName"].ToString().Replace(" ", "").Trim();
            InventoryId = db_Category.Rows[0]["InventoryID"].ToString();
            ItemImagesURL = db_Category.Rows[0]["ItemImagesURL"].ToString();

            JArray textArray = JArray.Parse(ItemImagesURL);

            ItemImagesURL = textArray[0].ToString();
            JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

            if (jsonMobileobj.Count > 0)
            {
                ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
            }
            ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();

            Three_D_URL = db_Category.Rows[0]["ThreeD"].ToString();
            CategoryID = db_Category.Rows[0]["CategoryID"].ToString();

            DataTable db_CategoryName = new DataTable();


            string DepartmentID = "";
            string ArtistID = "";
            string MediumID = "";
            string Year = "";
            string SizeID = "";
            string Length = "";
            string Breadth = "";
            string Height = "";
            string Size = "";
            // string Lot_Detail = "";
            string ArtistName = "";
            string Subject = "";
            string ArtistNumber = "";

            DataTable db_Category_Name = new DataTable();

            if (CategoryName == "Painting")
            {
                Lot_Detail = BasicFunction.Painting(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }
            else if (CategoryName == "JewelleryandSilver")
            {
                Lot_Detail = BasicFunction.JewelleryandSilver(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }
            else if (CategoryName == "Sculpture")
            {
                Lot_Detail = BasicFunction.Sculpture(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }
            else if (CategoryName == "TimepiecesandClocks")
            {
                Lot_Detail = BasicFunction.TimepiecesandClocks(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }

        }
        //else
        //{
        //    Details = "";
        //}
        return Lot_Detail;
    }

    public static string Category_List_Detail(string Lot_Id, string Lot_Title, double DollarRate, string sPDF, out double opningbid)//
    {
        DataTable db_Category = new DataTable();
        db_Category = BasicFunction.GetDetailsByDatatableCRM("select CM.CategoryName,ICD.Description,ICD.ProvenanceDescription,IC.InventoryID,IC.Value as Value,IC.ItemImagesURL,IC.[3DImageURL] as 'ThreeD',IC.CategoryID from CategoryMaster CM join InventoryCategories IC on CM.CategoryID=IC.CategoryID join InventoryCategoryDetails ICD on ICD.InventoryId=IC.InventoryId where IC.ID='" + Lot_Id + "'");


        string CategoryName = "";
        string InventoryId = "";
        string ItemImagesURL = "";
        string Three_D_URL = "";
        string CategoryID = "";
        string Lot_Detail = "";
        string ProvenanceDescription = "";
        string Description = "";
        double OpeningBid = 0;
        if (db_Category.Rows.Count > 0)
        {
            CategoryName = db_Category.Rows[0]["CategoryName"].ToString().Replace(" ", "").Trim();
            InventoryId = db_Category.Rows[0]["InventoryID"].ToString();
            ItemImagesURL = db_Category.Rows[0]["ItemImagesURL"].ToString();
            Description = db_Category.Rows[0]["Description"].ToString();
            ProvenanceDescription = db_Category.Rows[0]["ProvenanceDescription"].ToString();

            JArray textArray = JArray.Parse(ItemImagesURL);
            ItemImagesURL = textArray[0].ToString();
            JObject jsonMobileobj = JObject.Parse(ItemImagesURL);

            if (jsonMobileobj.Count > 0)
            {
                ItemImagesURL = JsonConvert.SerializeObject(jsonMobileobj.SelectToken("url").ToString());
            }
            ItemImagesURL = ItemImagesURL.Replace('"', ' ').Trim();

            Three_D_URL = db_Category.Rows[0]["ThreeD"].ToString();
            CategoryID = db_Category.Rows[0]["CategoryID"].ToString();
            if (string.IsNullOrEmpty(db_Category.Rows[0]["Value"].ToString()))
            {
                OpeningBid = 0;
            }
            else
            {
                OpeningBid = Convert.ToDouble(db_Category.Rows[0]["Value"].ToString());
            }
            DataTable db_CategoryName = new DataTable();


            string DepartmentID = "";
            string ArtistID = "";
            string MediumID = "";
            string Year = "";
            string SizeID = "";
            string Length = "";
            string Breadth = "";
            string Height = "";
            string Size = "";
            string ArtistName = "";
            string AboutArtist = "";
            string ArtistNumber = "";

            DataTable db_Category_Name = new DataTable();
            var FinalOpeningBid = Math.Round(OpeningBid / DollarRate, 0);

            //if (CategoryName == "Painting")
            //{
            //    db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select " + CategoryName + "ID as DepartmentID,ArtistID,MediumID,Year,SizeID,Length,Breadth,Height from " + CategoryName + "  where InventoryID='" + InventoryId + "'");
            //    DepartmentID = db_Category_Name.Rows[0]["DepartmentID"].ToString();
            //    ArtistID = db_Category_Name.Rows[0]["ArtistID"].ToString();
            //    MediumID = db_Category_Name.Rows[0]["MediumID"].ToString();
            //    Year = db_Category_Name.Rows[0]["Year"].ToString();
            //    SizeID = db_Category_Name.Rows[0]["SizeID"].ToString();
            //    Length = db_Category_Name.Rows[0]["Length"].ToString();
            //    Breadth = db_Category_Name.Rows[0]["Breadth"].ToString();
            //    Height = db_Category_Name.Rows[0]["Height"].ToString();

            //    //DataTable db_Artist = new DataTable();
            //    //db_Artist = BasicFunction.GetDetailsByDatatable("select ArtistName from Artist where ArtistID='" + ArtistID + "'");
            //    //string ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();

            //    if (!string.IsNullOrEmpty(ArtistID))
            //    {
            //        DataTable db_Artist = new DataTable();
            //        db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ID,ArtistName,DescribeAboutYourself from Artist where ArtistID='" + ArtistID + "'");
            //        ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();
            //        AboutArtist = db_Artist.Rows[0]["DescribeAboutYourself"].ToString();
            //        ArtistNumber = db_Artist.Rows[0]["ID"].ToString();
            //    }

            //    DataTable db_Size = new DataTable();
            //    db_Size = BasicFunction.GetDetailsByDatatableCRM("select Size from SizeMaster where SizeID='" + SizeID + "'");
            //    string SizeUnit = db_Size.Rows[0]["Size"].ToString();
            //    Size = Length + "  * " + Breadth + " * " + Height + " " + SizeUnit;

            //    DataTable db_Medium = new DataTable();
            //    db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");
            //    string MediumName = db_Medium.Rows[0]["Medium"].ToString();
            //    string Description = db_Medium.Rows[0]["Description"].ToString();



            //     Lot_Detail = "'OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Type':'Painting','DetailInfo':{'Title':'" + ArtistName + "','Desc':'" + AboutArtist + "'},'Info':{'artistPageName':'" + ArtistName.Replace(" ", "-").ToLower() + "-" + ArtistNumber + "','Title':'" + ArtistName + "','SubTitle':'Culcuta Series','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'Circa " + Year + "','Description':'" + Description + "','Circa':''},'AdditionalInfo':{'Description':'Lorem ipsum dolor sit, amet consectetur adipisicing elit. Totam eaque expedita, cum laudantium hic tempora, officiis beatae saepe doloremque at pariatur molestias excepturi et corporis, minima nemo. Deleniti assumenda alias maxime. Autem at architecto ullam sunt aut! Quia eveniet quas ullam officiis tempore ut qui! Laboriosam, dolor. Asperiores, esse at?','ConditionReport':[{'Title':'About Report','PDF':'PDF URL'},{'Title':'About Report','PDF':'PDF URL'}],'Provenance':'No Record Found'}";

            //}

            //else
            //{
            //    db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select " + CategoryName + "ID as DepartmentID,MediumID,Year,SizeID,Length,Breadth,Height from " + CategoryName + "  where InventoryID='" + InventoryId + "'");
            //    DepartmentID = db_Category_Name.Rows[0]["DepartmentID"].ToString();
            //    // string ArtistID = db_Category_Name.Rows[0]["ArtistID"].ToString();
            //    MediumID = db_Category_Name.Rows[0]["MediumID"].ToString();
            //    Year = db_Category_Name.Rows[0]["Year"].ToString();
            //    SizeID = db_Category_Name.Rows[0]["SizeID"].ToString();
            //    Length = db_Category_Name.Rows[0]["Length"].ToString();
            //    Breadth = db_Category_Name.Rows[0]["Breadth"].ToString();
            //    Height = db_Category_Name.Rows[0]["Height"].ToString();

            //    DataTable db_Size = new DataTable();
            //    db_Size = BasicFunction.GetDetailsByDatatableCRM("select Size from SizeMaster where SizeID='" + SizeID + "'");
            //    string SizeUnit = db_Size.Rows[0]["Size"].ToString();
            //    Size = /*Length + "  * " +*/ Breadth + " * " + Height;

            //    DataTable db_Medium = new DataTable();
            //    db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");
            //    string MediumName = db_Medium.Rows[0]["Medium"].ToString();
            //    string Description = db_Medium.Rows[0]["Description"].ToString();

            //    Lot_Detail = "'OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'Type':'Painting','DetailInfo':{'Title':'" + ArtistName + "','Desc':'" + AboutArtist + "'},'Info':{'artistPageName':'','Title':'" + Lot_Title + "','SubTitle':'','Medium':'','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'},'AdditionalInfo':{'Description':'Lorem ipsum dolor sit, amet consectetur adipisicing elit. Totam eaque expedita, cum laudantium hic tempora, officiis beatae saepe doloremque at pariatur molestias excepturi et corporis, minima nemo. Deleniti assumenda alias maxime. Autem at architecto ullam sunt aut! Quia eveniet quas ullam officiis tempore ut qui! Laboriosam, dolor. Asperiores, esse at?','ConditionReport':[{'Title':'About Report','PDF':'PDF URL'},{'Title':'About Report','PDF':'PDF URL'}],'Provenance':'No Record Found'}";

            //}

            if (CategoryName == "Painting")
            {
                Lot_Detail = BasicFunction.Painting(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }
            else if (CategoryName == "JewelleryandSilver")
            {
                Lot_Detail = BasicFunction.JewelleryandSilver(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }
            else if (CategoryName == "Sculpture")
            {
                Lot_Detail = BasicFunction.Sculpture(InventoryId, ItemImagesURL, CategoryID, Three_D_URL);
            }


            if (!string.IsNullOrEmpty(Lot_Detail))
            {
                Lot_Detail = "'OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'DetailInfo':{'Title':'" + ArtistName + "','Desc':'" + AboutArtist + "'}," + Lot_Detail + ",'AdditionalInfo':{'Description':'" + Description + "','ConditionReport':[{'Title':'About Report','PDF':'" + sPDF + "'}],'Provenance':'" + ProvenanceDescription + "'}";

            }
            else
            {
                Lot_Detail = "'OpeningBid':{'INR':'" + OpeningBid + "','USD':'" + FinalOpeningBid + "'},'DetailInfo':{'Title':'" + ArtistName + "','Desc':'" + AboutArtist + "'},'AdditionalInfo':{'Description':'" + Description + "','ConditionReport':[{'Title':'About Report','PDF':'" + sPDF + "'}],'Provenance':'" + ProvenanceDescription + "'}";

            }

            ////Filter Section
            //DataTable db_SubCategory = new DataTable();
            //db_SubCategory = BasicFunction.GetDetailsByDatatableCRM("select SubCategory,MediumID from " + CategoryName + "SubCategory where " + CategoryName + "ID='" + DepartmentID + "'");
            //List<string> Filter_List = new List<string>();

            //if (db_SubCategory.Rows.Count > 0)
            //{

            //    for (int i = 0; i < db_SubCategory.Rows.Count; i++)
            //    {
            //        string SubCategory_Name = db_SubCategory.Rows[i]["SubCategory"].ToString();
            //        string MediumId = db_SubCategory.Rows[i]["MediumID"].ToString();

            //        DataTable db_Medium = new DataTable();
            //        db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");
            //        string MediumName = db_Medium.Rows[0]["Medium"].ToString();
            //        // string Description = db_Medium.Rows[0]["Description"].ToString();

            //        if (CategoryName == "Painting")
            //        {
            //            string Filter = "{'Name':'Year','Value':'Circa " + Year + "'},{'Name':'Department','Value':'" + SubCategory_Name + "'},{'Name':'Medium','Value':'" + MediumName + "'},{'Name':'Size','Value':'" + Size + "'}";
            //            Filter_List.Add(Filter);
            //        }
            //        else
            //        {
            //            string Filter = "{'Name':'Year','Value':'" + Year + "'},{'Name':'Department','Value':'" + SubCategory_Name + "'},{'Name':'Medium','Value':'" + MediumName + "'},{'Name':'Size','Value':'" + Size + "'}";
            //            Filter_List.Add(Filter);
            //        }

            //    }
            //}
            //else
            //{
            //    string Filter = "";
            //    Filter_List.Add(Filter);
            //}

            //    if (!string.IsNullOrEmpty(Lot_Detail))
            //    {
            //        string str = string.Join(",", Filter_List);

            //        // string Details = "" + Lot_Detail + "," + str + "";
            //        Details = "" + Lot_Detail + ",'Filters':[" + str + "]";
            //        // string Details = Lot_Detail;
            //    }
            //    else
            //    {
            //        string str = string.Join(",", Filter_List);

            //        // string Details = "" + Lot_Detail + "," + str + "";

            //        Details = "'Filters':[" + str + "]";

            //        // string Details = Lot_Detail;    
            //    }
        }
        //else
        //{
        //    Details = "";
        //}
        opningbid = OpeningBid;
        return Lot_Detail;
    }

    #endregion

    public static string InsertBidAmpount(string UserId, string LotId, string ProxyBidAmount, string LiveBidAmount)
    {
        //
        DataTable db_Category = new DataTable();
        db_Category = BasicFunction.GetDetailsByDatatable("select Balance from tbl_ClientAccountBalance where UserId=" + "" + UserId + "");
        double BankBalance = 0;
        if (db_Category.Rows.Count > 0)
        {
            BankBalance = Convert.ToDouble(db_Category.Rows[0]["Balance"].ToString());

        }
        double Amount = Convert.ToDouble(ProxyBidAmount);
        double LiveAmount = Convert.ToDouble(LiveBidAmount);

        //
        if (true)//(Amount <= BankBalance)
        {
            DataTable db_mail = new DataTable();
            db_mail = BasicFunction.GetDetailsByDatatableCRM("select Value from InventoryCategories where LotNo='" + LotId + "'");
            //
            if (db_mail.Rows.Count > 0)
            {
                int Estimate_Amount = Convert.ToInt32(db_mail.Rows[0]["Value"].ToString());
                double User_Insert_Amount = Estimate_Amount + Estimate_Amount * 0.1;
                if (User_Insert_Amount <= Amount || User_Insert_Amount <= LiveAmount)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;
                    cmdInsert = new SqlCommand("sp_InsertBidAmpount", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;

                    cmdInsert.Parameters.AddWithValue("@LotId", LotId);
                    cmdInsert.Parameters.AddWithValue("@ProxyBidAmount", ProxyBidAmount);
                    cmdInsert.Parameters.AddWithValue("@UserId", UserId);
                    cmdInsert.Parameters.AddWithValue("@LiveBidAmount", LiveBidAmount);
                    //cmdInsert.Parameters.AddWithValue("@OpeningBidAmount", Estimate_Amount);

                    conn.Open();
                    cmdInsert.ExecuteNonQuery();
                    return "success";
                }
                else
                {
                    return "InvalidAmount";
                }
            }
            else
            {
                return "InsufisuntBalance";
            }

        }
        else
        {
            return "InsufisuntBalance";
        }

    }

    public static string Painting(string InventoryId, string ItemImagesURL, string CategoryID, string Three_D_URL)
    {
        string DepartmentID = "";
        string ArtistID = "";
        string MediumID = "";
        string Year = "";
        string SizeID = "";
        string Length = "";
        string Breadth = "";
        string Height = "";
        string Size = "";
        string Lot_Detail = "";
        string ArtistName = "";
        string Subject = "";
        string ArtistNumber = "";
        string CategoryName = "Painting";

        string Details = "";

        string sArtist = "";
        string MediumName = "";
        string Description = "";

        string sArtistNameFilter = "";

        DataTable db_Category_Name = new DataTable();

        //db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select P.PaintingID as DepartmentID,P.ArtistID,P.MediumID,P.Year,P.SizeID,P.Length,P.Breadth,P.Height,A.Subject from Painting P join ArtistSubCategory A on P.ArtistID =A.ArtistID  where P.InventoryID='" + InventoryId + "'");
        db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select P.PaintingID as DepartmentID,P.ArtistID,ICD.MediumName,ICD.Year,ICD.Size,P.Length,P.Breadth,P.Height,(select A.Subject from ArtistSubCategory A where  P.ArtistID =A.ArtistID) as Subject from Painting P join InventoryCategoryDetails ICD on ICD.InventoryID=p.InventoryID  where P.InventoryID='" + InventoryId + "'");

        if (db_Category_Name.Rows.Count > 0)
        {
            DepartmentID = db_Category_Name.Rows[0]["DepartmentID"].ToString();
            ArtistID = db_Category_Name.Rows[0]["ArtistID"].ToString();
            MediumID = db_Category_Name.Rows[0]["MediumName"].ToString();
            Year = db_Category_Name.Rows[0]["Year"].ToString();
            Size = db_Category_Name.Rows[0]["Size"].ToString();
            Length = db_Category_Name.Rows[0]["Length"].ToString();
            Breadth = db_Category_Name.Rows[0]["Breadth"].ToString();
            Height = db_Category_Name.Rows[0]["Height"].ToString();
            Subject = db_Category_Name.Rows[0]["Subject"].ToString();



            //DataTable db_Artist = new DataTable();
            //db_Artist = BasicFunction.GetDetailsByDatatable("select ArtistName from Artist where ArtistID='" + ArtistID + "'");
            //string ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();

            if (!string.IsNullOrEmpty(ArtistID))
            {
                DataTable db_Artist = new DataTable();
                db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ID, ArtistName from Artist where ArtistID='" + ArtistID + "'");
                ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();
                ArtistNumber = db_Artist.Rows[0]["ArtistName"].ToString();
            }

            //DataTable db_Size = new DataTable();
            //db_Size = BasicFunction.GetDetailsByDatatableCRM("select Size from SizeMaster where SizeID='" + SizeID + "'");
            //string SizeUnit = db_Size.Rows[0]["Size"].ToString();

            //if (string.IsNullOrEmpty(Length))
            //{
            //    Length = "0";
            //}
            //if (string.IsNullOrEmpty(Breadth))
            //{
            //    Breadth = "0";
            //}
            //if (string.IsNullOrEmpty(Height))
            //{
            //    Height = "0";
            //}

            //Size = Length + "  x " + Breadth + " x " + Height;

            if (!string.IsNullOrEmpty(MediumID))
            {
                DataTable db_Medium = new DataTable();
                db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");

                if (db_Medium.Rows.Count > 0)
                {
                    MediumName = db_Medium.Rows[0]["Medium"].ToString();
                    Description = db_Medium.Rows[0]["Description"].ToString();
                }
            }

            // sArtist = "";


            if (!string.IsNullOrEmpty(ArtistName))
            {
                sArtist = ArtistName + " - " + ArtistName;
                sArtistNameFilter = ArtistName;
            }

            Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'Circa " + Year + "','Description':'" + Description + "','Circa':''}";


        }
        else
        {

            //Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{}";
            // Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "'";

            Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'Circa " + Year + "','Description':'" + Description + "','Circa':''}";

        }
        Details = BasicFunction.Filter(Lot_Detail, CategoryName, DepartmentID, Year, Size, sArtistNameFilter);
        return Details;

    }

    public static string JewelleryandSilver(string InventoryId, string ItemImagesURL, string CategoryID, string Three_D_URL)
    {
        string CategoryName = "JewelleryandSilver";
        string DepartmentID = "";
        //string ArtistID = "";
        string MediumID = "";
        string Year = "";
        //string SizeID = "";
        //string Length = "";
        //string Breadth = "";
        //string Height = "";
        string Size = "";
        string Lot_Detail = "";
        string ArtistName = "";
        string Subject = "";
        string MediumName = "";
        //string ArtistNumber = "";
        //string CategoryName = "Painting";
        string Description = "";

        string Details = "";

        string sArtist = "";

        string sArtistNameFilter = "";

        DataTable db_Category_Name = new DataTable();

        //db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select JewelleryAndSilverID,Circa,Description from JewelleryAndSilver where InventoryID='" + InventoryId + "'");
        db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select ICD.AntiquecraftsDescription as Description,J.JewelleryAndSilverID,ICD.InventoryID,ICD.mediumname,ICD.size,ICD.Year as Circa from JewelleryAndSilver J,InventoryCategoryDetails ICD where ICD.InventoryID=J.InventoryID and J.InventoryID='" + InventoryId + "'");

        if (db_Category_Name.Rows.Count > 0)
        {
            DepartmentID = db_Category_Name.Rows[0]["JewelleryAndSilverID"].ToString();
            // ArtistID = db_Category_Name.Rows[0]["ArtistID"].ToString();
            MediumID = db_Category_Name.Rows[0]["MediumName"].ToString();
            Year = db_Category_Name.Rows[0]["Circa"].ToString();
            Description = db_Category_Name.Rows[0]["Description"].ToString();
            Size = db_Category_Name.Rows[0]["Size"].ToString();
            // Length = db_Category_Name.Rows[0]["Length"].ToString();
            //Breadth = db_Category_Name.Rows[0]["Breadth"].ToString();
            // Height = db_Category_Name.Rows[0]["Height"].ToString();
            // Subject = db_Category_Name.Rows[0]["Subject"].ToString();

            //DataTable db_Artist = new DataTable();
            //db_Artist = BasicFunction.GetDetailsByDatatable("select ArtistName from Artist where ArtistID='" + ArtistID + "'");
            //string ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();

            //if (!string.IsNullOrEmpty(ArtistID))
            //{
            //    DataTable db_Artist = new DataTable();
            //    db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ID, ArtistName from Artist where ArtistID='" + ArtistID + "'");
            //    ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();
            //    ArtistNumber = db_Artist.Rows[0]["ArtistName"].ToString();
            //}

            //DataTable db_Size = new DataTable();
            //db_Size = BasicFunction.GetDetailsByDatatableCRM("select Size from SizeMaster where SizeID='" + SizeID + "'");
            //string SizeUnit = db_Size.Rows[0]["Size"].ToString();
            //Size = Length + "  * " + Breadth + " * " + Height;
            if (!string.IsNullOrEmpty(MediumID))
            {
                DataTable db_Medium = new DataTable();
                db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");

                if (db_Medium.Rows.Count > 0)
                {
                    MediumName = db_Medium.Rows[0]["Medium"].ToString();
                    Description = db_Medium.Rows[0]["Description"].ToString();
                }
            }

            //string sArtist = "";


            if (!string.IsNullOrEmpty(ArtistName))
            {
                sArtist = ArtistName + " - " + ArtistName;
                sArtistNameFilter = ArtistName;
            }

            Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'}";


        }
        else
        {
            //Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{}";
            //Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "'";

            Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'}";
        }

        Details = BasicFunction.Filter(Lot_Detail, CategoryName, DepartmentID, Year, Size, sArtistNameFilter);
        return Details;

    }

    public static string Sculpture(string InventoryId, string ItemImagesURL, string CategoryID, string Three_D_URL)
    {
        string CategoryName = "Sculpture";
        string DepartmentID = "";
        string ArtistID = "";
        string MediumID = "";
        string Year = "";
        string SizeID = "";
        string Length = "";
        string Breadth = "";
        string Height = "";
        string Size = "";
        string Lot_Detail = "";
        string ArtistName = "";
        string Subject = "";
        string MediumName = "";
        string ArtistNumber = "";
        //string CategoryName = "Painting";
        string Description = "";

        string Details = "";
        string sArtist = "";

        string sArtistNameFilter = "";

        DataTable db_Category_Name = new DataTable();

        //db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select SculptureID,Year,SizeID,Length,Breadth,Height,MediumID,ArtistID,Description from Sculpture where InventoryID='" + InventoryId + "'");
        db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select ICD.AntiquecraftsDescription as Description,S.SculptureID,ICD.Year,S.Length,S.Breadth,S.Height,S.ArtistID,ICD.mediumname,ICD.size from Sculpture S,InventoryCategoryDetails ICD where ICD.InventoryID=S.InventoryID and S.InventoryID='" + InventoryId + "'");

        if (db_Category_Name.Rows.Count > 0)
        {
            DepartmentID = db_Category_Name.Rows[0]["SculptureID"].ToString();
            ArtistID = db_Category_Name.Rows[0]["ArtistID"].ToString();
            MediumID = db_Category_Name.Rows[0]["MediumName"].ToString();
            Year = db_Category_Name.Rows[0]["Year"].ToString();
            Description = db_Category_Name.Rows[0]["Description"].ToString();
            Size = db_Category_Name.Rows[0]["Size"].ToString();
            Length = db_Category_Name.Rows[0]["Length"].ToString();
            Breadth = db_Category_Name.Rows[0]["Breadth"].ToString();
            Height = db_Category_Name.Rows[0]["Height"].ToString();
            // Subject = db_Category_Name.Rows[0]["Subject"].ToString();

            DataTable db_Artist = new DataTable();
            db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ArtistName from Artist where ArtistID='" + ArtistID + "'");
            ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();

            if (!string.IsNullOrEmpty(ArtistID))
            {
                //  DataTable db_Artist = new DataTable();
                db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ID, ArtistName from Artist where ArtistID='" + ArtistID + "'");
                ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();
                ArtistNumber = db_Artist.Rows[0]["ArtistName"].ToString();
            }

            //DataTable db_Size = new DataTable();
            //db_Size = BasicFunction.GetDetailsByDatatableCRM("select Size from SizeMaster where SizeID='" + SizeID + "'");
            //string SizeUnit = db_Size.Rows[0]["Size"].ToString();
            //Size = Length + "  * " + Breadth + " * " + Height;

            if (!string.IsNullOrEmpty(MediumID))
            {
                DataTable db_Medium = new DataTable();
                db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");
                if (db_Medium.Rows.Count > 0)
                {
                    MediumName = db_Medium.Rows[0]["Medium"].ToString();
                    Description = db_Medium.Rows[0]["Description"].ToString();
                }
            }

            //string sArtist = "";



            if (!string.IsNullOrEmpty(ArtistName))
            {
                sArtist = ArtistName + " - " + ArtistName;
                sArtistNameFilter = ArtistName;
            }

            Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'}";


        }
        else
        {
            //Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{}";
            //Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "'";

            Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'}";

        }
        Details = BasicFunction.Filter(Lot_Detail, CategoryName, DepartmentID, Year, Size, sArtistNameFilter);
        return Details;

    }

    public static string TimepiecesandClocks(string InventoryId, string ItemImagesURL, string CategoryID, string Three_D_URL)
    {
        string CategoryName = "TimepiecesandClocks";
        string DepartmentID = "";
        string ArtistID = "";
        string MediumID = "";
        string Year = "";
        string SizeID = "";
        string Length = "";
        string Breadth = "";
        string Height = "";
        string Size = "";
        string Lot_Detail = "";
        string ArtistName = "";
        string Subject = "";
        string MediumName = "";
        string ArtistNumber = "";
        //string CategoryName = "Painting";
        string Description = "";

        string Details = "";
        string sArtist = "";

        string sArtistNameFilter = "";

        DataTable db_Category_Name = new DataTable();

        //db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select * from TimePeicesAndClock where InventoryID='" + InventoryId + "'");
        db_Category_Name = BasicFunction.GetDetailsByDatatableCRM("select ICD.AntiquecraftsDescription,T.*,ICD.mediumname,ICD.size,ICD.Year from TimePeicesAndClock T,InventoryCategoryDetails ICD where ICD.InventoryID=T.InventoryID and T.InventoryID='" + InventoryId + "'");

        if (db_Category_Name.Rows.Count > 0)
        {
            DepartmentID = db_Category_Name.Rows[0]["TimePeicesAndClockID"].ToString();
            //ArtistID = db_Category_Name.Rows[0]["ArtistID"].ToString();
            MediumID = db_Category_Name.Rows[0]["MediumName"].ToString();
            Year = db_Category_Name.Rows[0]["Year"].ToString();
            Description = db_Category_Name.Rows[0]["AntiquecraftsDescription"].ToString();

            Size = db_Category_Name.Rows[0]["Size"].ToString();

            if (string.IsNullOrEmpty(Size))
            {
                Size = "";
            }

            if (string.IsNullOrEmpty(MediumName))
            {
                MediumName = "";
            }

            if (string.IsNullOrEmpty(Year))
            {
                Year = "";
            }

            if (string.IsNullOrEmpty(Description))
            {
                Description = "";
            }

            //Length = db_Category_Name.Rows[0]["Length"].ToString();
            //Breadth = db_Category_Name.Rows[0]["Breadth"].ToString();
            //Height = db_Category_Name.Rows[0]["Height"].ToString();
            // Subject = db_Category_Name.Rows[0]["Subject"].ToString();

            //DataTable db_Artist = new DataTable();
            //db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ArtistName from Artist where ArtistID='" + ArtistID + "'");
            //ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();

            //if (!string.IsNullOrEmpty(ArtistID))
            //{
            //    //  DataTable db_Artist = new DataTable();
            //    db_Artist = BasicFunction.GetDetailsByDatatableCRM("select ID, ArtistName from Artist where ArtistID='" + ArtistID + "'");
            //    ArtistName = db_Artist.Rows[0]["ArtistName"].ToString();
            //    ArtistNumber = db_Artist.Rows[0]["ArtistName"].ToString();
            //}

            //DataTable db_Size = new DataTable();
            //db_Size = BasicFunction.GetDetailsByDatatableCRM("select Size from SizeMaster where SizeID='" + SizeID + "'");
            //string SizeUnit = db_Size.Rows[0]["Size"].ToString();
            //Size = Length + "  * " + Breadth + " * " + Height;

            if (!string.IsNullOrEmpty(MediumID))
            {
                DataTable db_Medium = new DataTable();
                db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumID + "'");
                if (db_Medium.Rows.Count > 0)
                {
                    MediumName = db_Medium.Rows[0]["Medium"].ToString();
                    Description = db_Medium.Rows[0]["Description"].ToString();
                }
            }



            //string sArtist = "";


            if (!string.IsNullOrEmpty(ArtistName))
            {
                sArtist = ArtistName + " - " + ArtistName;
                sArtistNameFilter = ArtistName;
            }


            Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'}";

        }
        else
        {
            //Lot_Detail = "'Type':'Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{}";
            //Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "'";

            Lot_Detail = "'Type':'Non-Painting','ThumbImage':'" + ItemImagesURL + "','Url3D':'" + Three_D_URL + "','Info':{'artistPageName':'" + sArtist + "','Title':'" + ArtistName + "','SubTitle':'" + Subject + "','Medium':'" + MediumName + "','Size':'" + Size + "','Year':'','Description':'" + Description + "','Circa':'" + Year + "'}";

        }
        Details = BasicFunction.Filter(Lot_Detail, CategoryName, DepartmentID, Year, Size, sArtistNameFilter);
        return Details;

    }


    public static string Filter(string Lot_Detail, string CategoryName, string DepartmentID, string Year, string Size, string ArtistName)
    {
        string Filter = "";
        string Details = "";
        List<string> Filter_List = new List<string>();
        //Filter Section
        DataTable db_SubCategory = new DataTable();
        if (CategoryName == "Painting")
        {
            db_SubCategory = BasicFunction.GetDetailsByDatatableCRM("select SubCategory,MediumID from " + CategoryName + "SubCategory where " + CategoryName + "ID='" + DepartmentID + "'");


            //else
            //{
            //    db_SubCategory = BasicFunction.GetDetailsByDatatableCRM("select SubCategory,MediumID from " + CategoryName + "SubCategory where " + CategoryName + "ID='" + DepartmentID + "'");

            //}


            if (db_SubCategory.Rows.Count > 0)
            {

                for (int i = 0; i < db_SubCategory.Rows.Count; i++)
                {
                    string SubCategory_Name = db_SubCategory.Rows[i]["SubCategory"].ToString();
                    string MediumId = db_SubCategory.Rows[i]["MediumID"].ToString();

                    DataTable db_Medium = new DataTable();
                    db_Medium = BasicFunction.GetDetailsByDatatableCRM("select Medium,Description from MediumMaster where MediumID='" + MediumId + "'");
                    string MediumName = db_Medium.Rows[0]["Medium"].ToString();

                    // string Description = db_Medium.Rows[0]["Description"].ToString();

                    if (CategoryName == "Painting")
                    {
                        Filter = "{'Name':'Year','Value':'Circa " + Year + "'},{'Name':'Department','Value':'" + SubCategory_Name + "'},{'Name':'Medium','Value':'" + MediumName + "'},{'Name':'Size','Value':'" + Size + "'},{'Name':'Arist Name','Value':'" + ArtistName + "'}";
                        Filter_List.Add(Filter);
                    }
                    else
                    {
                        Filter = "{'Name':'Year','Value':'" + Year + "'},{'Name':'Department','Value':'" + SubCategory_Name + "'},{'Name':'Medium','Value':'" + MediumName + "'},{'Name':'Size','Value':'" + Size + "'";
                        Filter_List.Add(Filter);
                    }

                }
            }
            else
            {
                Filter = "{'Name':'Arist Name','Value':'" + ArtistName + "'}";
                Filter_List.Add(Filter);
            }
        }
        //else
        //{
        //    Filter_List =
        //}
        string str = string.Join(",", Filter_List);

        // string Details = "" + Lot_Detail + "," + str + "";
        if (!string.IsNullOrEmpty(Lot_Detail))
        {
            //Details = "" + Lot_Detail + "";
            //if (!string.IsNullOrEmpty(str))
            //{
            Details = "" + Lot_Detail + ",'Filters':[" + str + "]";
            //}
        }
        else if (!string.IsNullOrEmpty(str))
        {
            Details = "'Filters':[" + str + "]";
        }
        else
        {
            Details = string.Empty;
        }
        //Details = "'Filters':[" + str + "]";
        // string Details = Lot_Detail;
        return Details;


    }

}
