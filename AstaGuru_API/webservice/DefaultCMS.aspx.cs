using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Expressions;
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

public partial class webservice_DefaultCMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetServicesDetails("{d:'JYva9xk3PzKn+LSZYICO84/5PLBqjjNAQEadheJinqp6ZUjfwJ1dVf32Sfa/OGhlJpb/swZa7lcwGQCR3e7qDg=='}");
        //GetCommonInfo("{d:'JYva9xk3PzKn+LSZYICO889BTNcvtYDGQOhTCeJvKE8I9U96DK5/cMtR54d2ptf3Hnd8YfJq5J2EggpQYNcBTg=='}");

        //GetBlogsDetails("{d:'E8ER2YhR+vU+pWvVhl8sRLk/QqfmuNUEHwa2gbLuvMg9TXYmsq+VrYwwgz1u3fQ4'}");

        //GetOurCollections();
        //GetArtMovement();

        //GetBuyDetails();

        //GetCareers();

        //GetNewsVideos();

        //GetBlogs();
    }

    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static byte[] EncryptStringAES(string plainText)
    {
        //var keybytes = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");
        //var iv = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");

        var keybytes = Encoding.UTF8.GetBytes("codastagcoduruco");
        var iv = Encoding.UTF8.GetBytes("codastagcoduruco");

        var plainttext = plainText;
        var encriptedFromJavascript = EncryptStringToBytes(plainttext, keybytes, iv);
        return encriptedFromJavascript;
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
        {
            throw new ArgumentNullException("plainText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        byte[] encrypted;
        // Create a RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static string DecryptStringAES(string cipherText)
    {
        //var keybytes = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");
        //var iv = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");

        var keybytes = Encoding.UTF8.GetBytes("codastagcoduruco");
        var iv = Encoding.UTF8.GetBytes("codastagcoduruco");

        var encrypted = Convert.FromBase64String(cipherText);
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
        return string.Format(decriptedFromJavascript);
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
            try
            {
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {

                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();

                        }

                    }
                }
            }
            catch
            {
                plaintext = "keyError";
            }
        }

        return plaintext;
    }


    [WebMethod]
    public static string GetBuyDetails()
    {
        //string decryptedString = d;

        //decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        //string replacedString = DecryptStringAES(decryptedString);
        //replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        //JObject jsonEmail = JObject.Parse(replacedString);
        //string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Buy = new DataTable();

            dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Buy'");

            if (dt_Buy.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Buy.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Buy.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Buy.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Buy.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': '" + dt_Buy.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";


                sResponse = sResponse + "},";

                string sbannerImage = dt_Buy.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim();
                string Banner_ShortDescription = dt_Buy.Rows[0]["Banner_ShortDescription"].ToString().Replace("'", "").Trim();

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Banner where Category='Buy'");

                #region banner
                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'banner': {";

                    sResponse = sResponse + "'title':'Buy',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + sbannerImage + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + sbannerImage + "'},";
                    sResponse = sResponse + "'desc':'" + Banner_ShortDescription.Replace("'", "").Trim() + "'";

                    sResponse = sResponse + "},";
                }

                #endregion banner

                #region steps
                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ItWorkesWith where Category='Buy'");

                //sResponse = sResponse + "'steps':{";

                if (dt_Buy.Rows.Count > 0)
                {
                    // sResponse = sResponse + "'title':'It works with 3 easy steps',";
                    // sResponse = sResponse + "'eachSteps':[";
                    // int i;

                    // for (i = 0; i < dt_Buy.Rows.Count; i++)
                    // {
                    // sResponse = sResponse + "{'title':'" + dt_Buy.Rows[i]["title"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["DeskImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["DeskImage"].ToString().Replace("'", "").Trim() + "'}}";

                    // if (i + 1 != dt_Buy.Rows.Count)
                    // {
                    // sResponse = sResponse + ",";
                    // }
                    // }
                    // sResponse = sResponse + "]";
                }

                //sResponse = sResponse + "},";

                #endregion steps

                #region guide

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ItsEffortless where Guide_Category='Buy'");
                //sResponse = sResponse + "'guide':{";
                //if (dt_Buy.Rows.Count > 0)
                //{
                //    sResponse = sResponse + "'title':'" + dt_Buy.Rows[0]["Guide_Title"].ToString().Replace("'", "").Trim() + "',";
                //    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Guide_ThumbImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Guide_ThumbImage"].ToString().Replace("'", "").Trim() + "'},";
                //    sResponse = sResponse + "'videoURL':'" + dt_Buy.Rows[0]["Guide_videoURL"].ToString().Replace("'", "").Trim() + "',";
                //    sResponse = sResponse + "'desc':'" + dt_Buy.Rows[0]["Guide_Desc"].ToString().Replace("'", "").Trim() + "'";
                //}
                //sResponse = sResponse + "},";

                //#endregion guide

                //#region consign

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ConsignToUs where consign_Category='Buy'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'consign':{";
                    sResponse = sResponse + "'title':'" + dt_Buy.Rows[0]["consign_Title"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["consign_image"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["consign_image"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "'desc':'" + dt_Buy.Rows[0]["consign_Desc"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                }

                //#endregion consign

                #endregion pageContent

                #region accordian
                sResponse = sResponse + "'accordion':[";

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Faq where Flag = 'T'");

                if (dt_Buy.Rows.Count > 0)
                {
                    int i;

                    for (i = 0; i < dt_Buy.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{'title':'" + dt_Buy.Rows[i]["Title"].ToString().Replace("'", "").Trim() + "','desc':'" + dt_Buy.Rows[i]["Description"].ToString().Replace("'", "").Trim() + "'}";

                        if (i + 1 != dt_Buy.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }

                sResponse = sResponse + "]";

                #endregion accordian

                #region consign

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Buy'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + ",'consign': {";
                    sResponse = sResponse + "'title': 'Consign To Us',";
                    sResponse = sResponse + "'image': {";
                    sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["ConsignToUs_Image"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["ConsignToUs_Image"].ToString().Replace("'", "").Trim() + "'},";

                    sResponse = sResponse + "'desc': '" + dt_Buy.Rows[0]["ConsignToUs_Description"].ToString().Replace("'", "").Trim() + "'}";
                }
                #endregion

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string GetSellDetails()
    {
        //string decryptedString = d;

        //decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        //string replacedString = DecryptStringAES(decryptedString);
        //replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        //JObject jsonEmail = JObject.Parse(replacedString);
        //string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Buy = new DataTable();

            dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Sell'");

            if (dt_Buy.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Buy.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Buy.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Buy.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Buy.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': '" + dt_Buy.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                string sbannerImage = dt_Buy.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim();
                string Banner_ShortDescription = dt_Buy.Rows[0]["Banner_ShortDescription"].ToString().Replace("'", "").Trim();

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Banner where Category='Sell'");

                #region banner
                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'banner': {";

                    sResponse = sResponse + "'title':'Sell',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + sbannerImage + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + sbannerImage + "'},";
                    sResponse = sResponse + "'desc':'" + Banner_ShortDescription.Replace("'", "").Trim() + "'";

                    sResponse = sResponse + "},";
                }

                #endregion banner

                #region steps
                //dt_Buy = new DataTable();
                //dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ItWorkesWith where Category='Sell'");

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Sell'");

                sResponse = sResponse + "'steps':{";

                sResponse = sResponse + "'title':'It works with 3 easy steps',";
                sResponse = sResponse + "'eachSteps':[";
                if (dt_Buy.Rows.Count > 0)
                {
                    //int i;

                    //for (i = 0; i < dt_Buy.Rows.Count; i++)
                    //{
                    sResponse = sResponse + "{'title':'1','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image1"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image1"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_Buy.Rows[0]["Step1_ShortDescription"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "{'title':'2','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image2"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image2"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_Buy.Rows[0]["Step2_ShortDescription"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "{'title':'3','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image3"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image3"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_Buy.Rows[0]["Step3_ShortDescription"].ToString().Replace("'", "").Trim() + "'}";

                    //    if (i + 1 != dt_Buy.Rows.Count)
                    //    {
                    //        sResponse = sResponse + ",";
                    //    }
                    //}

                }

                sResponse = sResponse + "]},";

                #endregion steps

                //#region guide

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ItsEffortless where Guide_Category='Sell'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'guide':{";
                    sResponse = sResponse + "'title':'" + dt_Buy.Rows[0]["Guide_Title"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Guide_ThumbImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Guide_ThumbImage"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "'videoURL':'" + dt_Buy.Rows[0]["Guide_videoURL"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'desc':'" + dt_Buy.Rows[0]["Guide_Desc"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                }

                //#endregion guide

                //#region consign

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ConsignToUs where consign_Category='Sell'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'consign':{";
                    sResponse = sResponse + "'title':'" + dt_Buy.Rows[0]["consign_Title"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'image':'" + dt_Buy.Rows[0]["consign_image"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'desc':'" + dt_Buy.Rows[0]["consign_Desc"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                }

                //#endregion consign

                #endregion pageContent

                #region accordian
                sResponse = sResponse + "'accordion':[";

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Faq where Flag = 'T'");

                if (dt_Buy.Rows.Count > 0)
                {
                    int i;

                    for (i = 0; i < dt_Buy.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{'title':'" + dt_Buy.Rows[i]["Title"].ToString().Replace("'", "").Trim() + "','desc':'" + dt_Buy.Rows[i]["Description"].ToString().Replace("'", "").Trim() + "'}";

                        if (i + 1 != dt_Buy.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }

                sResponse = sResponse + "]";

                #endregion accordian

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string GetPrivateSalesDetails()
    {
        //string decryptedString = d;

        //decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        //string replacedString = DecryptStringAES(decryptedString);
        //replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        //JObject jsonEmail = JObject.Parse(replacedString);
        //string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Buy = new DataTable();

            dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Private Sales'");

            if (dt_Buy.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Buy.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Buy.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Buy.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Buy.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': '" + dt_Buy.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                string sbannerImage = dt_Buy.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim();
                string Banner_ShortDescription = dt_Buy.Rows[0]["Banner_ShortDescription"].ToString().Replace("'", "").Trim();

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Banner where Category='Private Sales'");

                #region banner
                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'banner': {";

                    sResponse = sResponse + "'title':'Private Sales',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + sbannerImage + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + sbannerImage + "'},";
                    sResponse = sResponse + "'desc':'" + Banner_ShortDescription.Replace("'", "").Trim() + "'";

                    sResponse = sResponse + "},";
                }

                #endregion banner

                #region steps
                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Private Sales'");

                sResponse = sResponse + "'steps':{";

                sResponse = sResponse + "'title':'It works with 3 easy steps',";
                sResponse = sResponse + "'eachSteps':[";
                if (dt_Buy.Rows.Count > 0)
                {
                    //int i;

                    //for (i = 0; i < dt_Buy.Rows.Count; i++)
                    //{
                    sResponse = sResponse + "{'title':'1','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image1"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image1"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_Buy.Rows[0]["Step1_ShortDescription"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "{'title':'2','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image2"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image2"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_Buy.Rows[0]["Step2_ShortDescription"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "{'title':'3','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image3"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Step_Image3"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_Buy.Rows[0]["Step3_ShortDescription"].ToString().Replace("'", "").Trim() + "'}";

                    //    if (i + 1 != dt_Buy.Rows.Count)
                    //    {
                    //        sResponse = sResponse + ",";
                    //    }
                    //}

                }

                sResponse = sResponse + "]},";

                #endregion steps

                //#region guide

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ItsEffortless where Guide_Category='Private Sales'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'guide':{";
                    sResponse = sResponse + "'title':'" + dt_Buy.Rows[0]["Guide_Title"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Guide_ThumbImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["Guide_ThumbImage"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "'videoURL':'" + dt_Buy.Rows[0]["Guide_videoURL"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'desc':'" + dt_Buy.Rows[0]["Guide_Desc"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                }

                //#endregion guide

                //#region consign

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ConsignToUs where consign_Category='Private Sales'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + "'consign':{";
                    sResponse = sResponse + "'title':'" + dt_Buy.Rows[0]["consign_Title"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["consign_image"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["consign_image"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "'desc':'" + dt_Buy.Rows[0]["consign_Desc"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                }

                //#endregion consign

                #endregion pageContent

                #region accordian
                sResponse = sResponse + "'accordion':[";

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Faq where Flag = 'T'");

                if (dt_Buy.Rows.Count > 0)
                {
                    int i;

                    for (i = 0; i < dt_Buy.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{'title':'" + dt_Buy.Rows[i]["Title"].ToString().Replace("'", "").Trim() + "','desc':'" + dt_Buy.Rows[i]["Description"].ToString().Replace("'", "").Trim() + "'}";

                        if (i + 1 != dt_Buy.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }

                sResponse = sResponse + "]";

                #endregion accordian

                #region consign

                dt_Buy = new DataTable();
                dt_Buy = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='Private Sales'");

                if (dt_Buy.Rows.Count > 0)
                {
                    sResponse = sResponse + ",'consign': {";
                    sResponse = sResponse + "'title': 'Consign To Us',";
                    sResponse = sResponse + "'image': {";
                    sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["ConsignToUs_Image"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Buy.Rows[0]["ConsignToUs_Image"].ToString().Replace("'", "").Trim() + "'},";

                    sResponse = sResponse + "'desc': '" + dt_Buy.Rows[0]["ConsignToUs_Description"].ToString().Replace("'", "").Trim() + "'}";
                }
                #endregion

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string GetServicesDetails(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonServices = JObject.Parse(replacedString);
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            //string sResponse = string.Empty;

            DataTable dt_Services = new DataTable();

            dt_Services = BasicFunctionCMS.GetDetailsByDatatableCMS("select ser.* from tbl_Services ser,tbl_ServiceDetails det where ser.Services_Name=det.Name and det.isDelete='1' and  ser.Services_Name='" + jsonServices.SelectToken("ServiceType").ToString().Trim().Replace("-", " ") + "'");

            string sDesc = dt_Services.Rows[0]["Service_Description"].ToString();

            if (dt_Services.Rows.Count > 0)
            {

                string[] expertArray = dt_Services.Rows[0]["Expert"].ToString().Trim().Split(',');
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Services.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Services.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Services.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Services.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': '" + dt_Services.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                //dt_Services = new DataTable();
                //dt_Services = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Banner where Category='" + jsonServices.SelectToken("ServiceType").ToString().Trim().Replace("-", " ") + "'");

                #region banner
                //if (dt_Services.Rows.Count > 0)
                //{
                sResponse = sResponse + "'banner': {";

                sResponse = sResponse + "'title':'Services',";
                sResponse = sResponse + "'image': { 'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Services.Rows[0]["Service_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Services.Rows[0]["Service_Image"].ToString().Replace("'", "").Trim() + "'},";
                sResponse = sResponse + "'innerTitle':'" + jsonServices.SelectToken("ServiceType").ToString().Trim() + "',";
                sResponse = sResponse + "'desc':'" + sDesc.Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";
                //}

                #endregion banner


                #endregion pageContent

                #region accordian
                sResponse = sResponse + "'talkToExpert':{ 'title':'Talk to our experts',";

                dt_Services = new DataTable();

                //dt_Services = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Experts where Service_Type='" + jsonServices.SelectToken("ServiceType").ToString().Trim() + "'");
                dt_Services = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Experts where Id IN (" + string.Join(",", expertArray) + ")");


                if (dt_Services.Rows.Count > 0)
                {
                    sResponse = sResponse + "'expertProfile':[";
                    int i;

                    for (i = 0; i < dt_Services.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{'name':'" + dt_Services.Rows[i]["Expert_Name"].ToString().Replace("'", "").Trim() + "','designation':'" + dt_Services.Rows[i]["Designation"].ToString().Replace("'", "").Trim() + "','mail':'" + dt_Services.Rows[i]["Email_Id"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Services.Rows[i]["ThumbImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Services.Rows[i]["ThumbImage"].ToString().Replace("'", "").Trim() + "'}}";

                        if (i + 1 != dt_Services.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                    sResponse = sResponse + "]";
                }
                #endregion accordian

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetFaqDetails()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_FAQ = new DataTable();

            dt_FAQ = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='FAQS'");

            if (dt_FAQ.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_FAQ.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_FAQ.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_FAQ.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_FAQ.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_FAQ.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                dt_FAQ = new DataTable();
                dt_FAQ = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='FAQS'");

                #region banner
                if (dt_FAQ.Rows.Count > 0)
                {
                    sResponse = sResponse + "'banner': {";

                    sResponse = sResponse + "'title':'FAQs',";
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_FAQ.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_FAQ.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "'desc':'" + dt_FAQ.Rows[0]["Banner_ShortDescription"].ToString().Replace("'", "").Trim() + "'";

                    sResponse = sResponse + "},";
                }

                #endregion banner

                #region accordian
                sResponse = sResponse + "'accordion':[";

                dt_FAQ = new DataTable();
                dt_FAQ = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Faq  where Flag = 'T'");

                if (dt_FAQ.Rows.Count > 0)
                {
                    int i;

                    for (i = 0; i < dt_FAQ.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{'title':'" + dt_FAQ.Rows[i]["Title"].ToString().Replace("'", "").Trim() + "','desc':'" + dt_FAQ.Rows[i]["Description"].ToString().Replace("'", "").Trim() + "'}";

                        if (i + 1 != dt_FAQ.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }

                sResponse = sResponse + "]";

                #endregion accordian

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetTermsAndConditions()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_TnC = new DataTable();

            dt_TnC = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Terms where Flag = 'T'");

            if (dt_TnC.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_TnC.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_TnC.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_TnC.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_TnC.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_TnC.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                #region accordian
                sResponse = sResponse + "'accordion':[";

                dt_TnC = new DataTable();
                dt_TnC = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Terms where Flag = 'T'");

                if (dt_TnC.Rows.Count > 0)
                {
                    int i;

                    for (i = 0; i < dt_TnC.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{'title':'" + dt_TnC.Rows[i]["Category"].ToString().Replace("'", "").Trim() + "','desc':'" + dt_TnC.Rows[i]["Service_Description"].ToString().Replace("'", "").Trim() + "'}";

                        if (i + 1 != dt_TnC.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }

                sResponse = sResponse + "]";

                #endregion accordian

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetReachUs()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_ReachUs = new DataTable();

            dt_ReachUs = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Location where Flag = 'T'");

            if (dt_ReachUs.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_ReachUs.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_ReachUs.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_ReachUs.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_ReachUs.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";
                sResponse = sResponse + "'banner': {";
                sResponse = sResponse + "'title':'Reach Us'},";
                sResponse = sResponse + "'reachForm': {";

                DataTable dt_ReachForm = new DataTable();
                dt_ReachForm = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ReachUs where Flag='T'");

                #region reachForm
                if (dt_ReachForm.Rows.Count > 0)
                {
                    sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachForm.Rows[0]["DesktopImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "~/upload/" + dt_ReachForm.Rows[0]["MobileImage"].ToString().Replace("'", "").Trim() + "'},";
                    sResponse = sResponse + "'desc':'" + dt_ReachForm.Rows[0]["Desription"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'mobile':'" + dt_ReachForm.Rows[0]["Mobile_No"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'email':'" + dt_ReachForm.Rows[0]["Email"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'department':[{'id':'Dept-1','name':'Dept 1'},{'id':'Dept-2','name':'Dept 2'},{'id':'Dept3','name':'Dept3'}]";
                }
                sResponse = sResponse + "},";
                #endregion reachForm


                #region location
                sResponse = sResponse + "'location': {";
                sResponse = sResponse + "'title':'Location',";
                sResponse = sResponse + "'array':[";


                int i;

                for (i = 0; i < dt_ReachUs.Rows.Count; i++)
                {
                    sResponse = sResponse + "{'title':'" + dt_ReachUs.Rows[i]["Location_Name"].ToString().Replace("'", "").Trim() + "','addType':'" + dt_ReachUs.Rows[i]["Location_Name"].ToString().Replace("'", "").Replace(" ", "").Trim() + "','address':'" + dt_ReachUs.Rows[i]["Address_Description"].ToString().Replace("'", "").Replace("<p>", "").Replace("</p>", "").Trim() + "','mapUrl':'" + dt_ReachUs.Rows[i]["Latitude"].ToString().Replace("'", "").Trim().Replace("<p>", "").Replace("</p>", "") + "'}";

                    if (i + 1 != dt_ReachUs.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }
                sResponse = sResponse + "]";

                #endregion location

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetWhoWeAre()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_WhoWeAre = new DataTable();

            dt_WhoWeAre = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_WhoWeAre where Flag = 'T'");

            if (dt_WhoWeAre.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_WhoWeAre.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_WhoWeAre.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_WhoWeAre.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_WhoWeAre.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";
                //DataTable dt_Banner = new DataTable();
                //dt_Banner = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Banner where Category='Who we are'");

                #region banner
                //if (dt_Banner.Rows.Count > 0)
                //{
                sResponse = sResponse + "'banner': {";

                sResponse = sResponse + "'title':'Who we are',";
                sResponse = sResponse + "'image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["Banner_Image"].ToString() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim() + "'},";
                sResponse = sResponse + "'desc':'" + dt_WhoWeAre.Rows[0]["Banner_Description"].ToString() + "'";

                sResponse = sResponse + "},";
                //}

                #endregion banner

                #region culture
                string[] cultureArray = dt_WhoWeAre.Rows[0]["Culture_Image"].ToString().Trim().Split(',');
                sResponse = sResponse + "'culture': {";
                sResponse = sResponse + "'title':'Culture at AstaGuru',";
                sResponse = sResponse + "'image':[";


                int i;

                for (i = 0; i < cultureArray.Length; i++)
                {
                    sResponse = sResponse + "{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + cultureArray[i] + "','mobile':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + cultureArray[i] + "'}";

                    if (i + 1 != cultureArray.Length)
                    {
                        sResponse = sResponse + ",";
                    }
                }
                sResponse = sResponse + "],";
                sResponse = sResponse + "'desc_1':'" + dt_WhoWeAre.Rows[0]["Culture_ShortDescription"].ToString().Trim() + "',";
                sResponse = sResponse + "'desc_2':'" + dt_WhoWeAre.Rows[0]["Culture2_ShortDescription"].ToString().Trim() + "'},";

                #endregion culture

                #region ourValues
                sResponse = sResponse + "'ourValues':{";
                sResponse = sResponse + "'title':'Our Values',";
                sResponse = sResponse + "'array':[{'desc':'" + dt_WhoWeAre.Rows[0]["OurValue_Description"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["OurValue_Image1"].ToString().Trim() + "','mobile':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["OurValue_Image1"].ToString().Trim() + "'}},";
                sResponse = sResponse + "{'desc':'" + dt_WhoWeAre.Rows[0]["OurValue_Description2"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["OurValue_Image2"].ToString().Trim() + "','mobile':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["OurValue_Image2"].ToString().Trim() + "'}},";
                sResponse = sResponse + "{'desc':'" + dt_WhoWeAre.Rows[0]["OurValue_Description3"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["OurValue_Image3"].ToString().Trim() + "','mobile':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_WhoWeAre.Rows[0]["OurValue_Image3"].ToString().Trim() + "'}}]";
                sResponse = sResponse + "},";
                #endregion ourValues

                #region management
                sResponse = sResponse + "'management':{";
                sResponse = sResponse + "'title':'Management',";
                sResponse = sResponse + "'desc':'" + dt_WhoWeAre.Rows[0]["Management_Description"].ToString().Replace("'", "").Trim() + "',";
                string[] expertArray = dt_WhoWeAre.Rows[0]["Expert_Name"].ToString().Trim().Split(',');
                DataTable dt_Experts = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Experts where Id IN (" + string.Join(",", expertArray) + ")");


                if (dt_Experts.Rows.Count > 0)
                {
                    sResponse = sResponse + "'expertProfile':[";
                    int j;

                    for (j = 0; j < dt_Experts.Rows.Count; j++)
                    {
                        sResponse = sResponse + "{'name':'" + dt_Experts.Rows[j]["Expert_Name"].ToString().Replace("'", "").Trim() + "','designation':'" + dt_Experts.Rows[j]["Designation"].ToString().Replace("'", "").Trim() + "','mail':'" + dt_Experts.Rows[j]["Email_Id"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Experts.Rows[j]["ThumbImage"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Experts.Rows[j]["ThumbImage"].ToString().Replace("'", "").Trim() + "'}}";

                        if (j + 1 != dt_Experts.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                    sResponse = sResponse + "]";
                }
                #endregion management

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string GetPress()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Press = new DataTable();

            dt_Press = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Press where Flag = 'T' and isDelete='1' order by Publication_Date desc");

            if (dt_Press.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Press.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Press.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Press.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Press.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Press.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                #region press
                sResponse = sResponse + "'title':'Press',";
                sResponse = sResponse + "'array':[";
                int j;

                for (j = 0; j < dt_Press.Rows.Count; j++)
                {
                    DateTime date = Convert.ToDateTime(dt_Press.Rows[j]["Publication_Date"].ToString().Replace("'", "").Trim());
                    string Final_date = date.ToString("dddd, dd MMMM yyyy");

                    sResponse = sResponse + "{'Id':'" + dt_Press.Rows[j]["Id"].ToString().Trim() + "','title':'" + dt_Press.Rows[j]["News_Title"].ToString().Replace("'", "").Trim() + "','credits':'" + dt_Press.Rows[j]["Publication_Name"].ToString().Replace("'", "").Trim() + "','timestamp':'" + Final_date + "','pageUrl':'" + BasicFunctionCMS.replacesplchar(dt_Press.Rows[j]["News_Title"].ToString().ToLower().Replace("'", "").Trim()) + "-" + dt_Press.Rows[j]["Id"].ToString().Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Press.Rows[j]["News_Image"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Press.Rows[j]["News_Image"].ToString().Replace("'", "").Trim() + "'}}";

                    if (j + 1 != dt_Press.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }
                sResponse = sResponse + "]";
                #endregion press

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string GetPressDetails(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonPressDetails = JObject.Parse(replacedString);
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            //string sResponse = string.Empty;

            DataTable dt_PressDetails = new DataTable();

            dt_PressDetails = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Press where Id=" + jsonPressDetails.SelectToken("pageId").ToString().Trim() + "");

            if (dt_PressDetails.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_PressDetails.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_PressDetails.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_PressDetails.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_PressDetails.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_PressDetails.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";


                sResponse = sResponse + "'title': 'Press',";
                sResponse = sResponse + "'pressDetail': {";
                sResponse = sResponse + "'title':'" + dt_PressDetails.Rows[0]["News_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': { 'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_PressDetails.Rows[0]["Publication_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_PressDetails.Rows[0]["Publication_Image"].ToString().Replace("'", "").Trim() + "'},";
                sResponse = sResponse + "'credits':'" + dt_PressDetails.Rows[0]["Publication_Name"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'timestamp':'" + dt_PressDetails.Rows[0]["Publication_Date"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'desc':'" + dt_PressDetails.Rows[0]["Publication_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'readMoreUrl':'" + dt_PressDetails.Rows[0]["External_Link"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "}";

                #endregion pageContent

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetCommonInfo(string d)
    {
        //string decryptedString = d;

        //decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        //string replacedString = DecryptStringAES(decryptedString);
        //replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        //JObject jsonPressDetails = JObject.Parse(replacedString);
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            //string sResponse = string.Empty;

            DataTable dt_CommonInfoDetails = new DataTable();

            dt_CommonInfoDetails = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ServiceDetails where flag='T' and isdelete='1'");

            if (dt_CommonInfoDetails.Rows.Count > 0)
            {
                sResponse = "{";
                sResponse = sResponse + "'navigation':{";
                sResponse = sResponse + "'services':[";

                for (int i = 0; i < dt_CommonInfoDetails.Rows.Count; i++)
                {

                    sResponse = sResponse + "{'name':'" + dt_CommonInfoDetails.Rows[i]["Name"].ToString() + "',";
                    sResponse = sResponse + "'pageName':'" + BasicFunctionCMS.replacesplchar(dt_CommonInfoDetails.Rows[i]["Name"].ToString().ToLower()) + "'";

                    if (dt_CommonInfoDetails.Rows.Count != i + 1)
                    {
                        sResponse = sResponse + "},";
                    }
                    else
                    {
                        sResponse = sResponse + "}";
                    }

                }

                sResponse = sResponse + "],";

                sResponse = sResponse + "'footerPage':[";
                sResponse = sResponse + " {";
                sResponse = sResponse + "'name':'Terms & Conditions',";
                sResponse = sResponse + "'pageName':'Terms-Conditions'";
                sResponse = sResponse + "},";
                sResponse = sResponse + "{'name':'Privacy Policy',";

                sResponse = sResponse + "'pageName':'privacy-policy' }, {'name':'Disclaimer','pageName':'disclaimer' }, {'name':'Cookie Policy','pageName':'cookie-policy'}]";
                sResponse = sResponse + "}}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetCareers()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_ReachUs = new DataTable();

            dt_ReachUs = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Careers where Flag = 'T'");

            if (dt_ReachUs.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_ReachUs.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_ReachUs.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_ReachUs.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_ReachUs.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";
                sResponse = sResponse + "'title':'Careers',";
                sResponse = sResponse + "'banner': {";
                sResponse = sResponse + "'title': '" + dt_ReachUs.Rows[0]["Carrer_Name"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': {";
                sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[0]["Carrer_Image"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[0]["Carrer_Image"].ToString().Replace("'", "").Trim() + "'";
                sResponse = sResponse + "},";
                sResponse = sResponse + "'desc': '" + dt_ReachUs.Rows[0]["Carrer_Description"].ToString().Replace("'", "").Trim() + "'";
                sResponse = sResponse + "},";


                #region vacancies
                dt_ReachUs = new DataTable();
                dt_ReachUs = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Jobs where flag='T' and isDelete='1'");

                sResponse = sResponse + "'vacancies': {";
                sResponse = sResponse + "'title': 'Latest Jobs at AstaGuru',";
                sResponse = sResponse + "'array': [";

                if (dt_ReachUs.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_ReachUs.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{";
                        sResponse = sResponse + "'title': '" + dt_ReachUs.Rows[i]["Job_Title"].ToString().Replace("'", "").Trim() + "',";

                        DateTime date = Convert.ToDateTime(dt_ReachUs.Rows[i]["Job_Date"].ToString().Replace("'", "").Trim());
                        string Final_date = date.ToString("dddd, dd MMMM yyyy");

                        sResponse = sResponse + "'date': '" + Final_date + "',";
                        sResponse = sResponse + "'desc': '" + dt_ReachUs.Rows[i]["Job_Description"].ToString().Replace("'", "").Trim() + "'";

                        if (dt_ReachUs.Rows.Count != i + 1)
                        {
                            sResponse = sResponse + "},";
                        }
                        else
                        {
                            sResponse = sResponse + "},";
                        }
                    }

                    sResponse = sResponse + "]},";
                }


                dt_ReachUs = new DataTable();
                dt_ReachUs = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_WorkEnvironment");

                if (dt_ReachUs.Rows.Count > 0)
                {
                    sResponse = sResponse + "'workEnvironment': {";
                    sResponse = sResponse + "'title': 'Work Environment',";
                    sResponse = sResponse + "'desc': '" + dt_ReachUs.Rows[0]["Work_Description"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'image': {";
                    sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[0]["Work_Image"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[0]["Work_Image"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                    sResponse = sResponse + "'videoUrl': '" + dt_ReachUs.Rows[0]["Work_VideoLink"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "},";
                }

                dt_ReachUs = new DataTable();

                dt_ReachUs = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Employee where Flag = 'T' and isDelete='1'");

                if (dt_ReachUs.Rows.Count > 0)
                {
                    sResponse = sResponse + "'employeeSays':{";
                    sResponse = sResponse + "'title':'Employee Says',";
                    sResponse = sResponse + "'array':[";

                    for (int i = 0; i < dt_ReachUs.Rows.Count; i++)
                    {


                        sResponse = sResponse + "{";

                        sResponse = sResponse + "'image':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_ReachUs.Rows[i]["Emp_Image"].ToString().Replace("'", "").Trim() + "',";
                        sResponse = sResponse + "'name':'" + dt_ReachUs.Rows[i]["Emp_Name"].ToString().Replace("'", "").Trim() + "',";
                        sResponse = sResponse + "'designation':'" + dt_ReachUs.Rows[i]["Emp_Designation"].ToString().Replace("'", "").Trim() + "',";
                        sResponse = sResponse + "'desc':'" + dt_ReachUs.Rows[i]["Emp_Testimonial"].ToString().Replace("'", "").Trim() + "'";

                        if (dt_ReachUs.Rows.Count != i + 1)
                        {
                            sResponse = sResponse + "},";
                        }
                        else
                        {
                            sResponse = sResponse + "}";
                        }


                    }
                    sResponse = sResponse + "] },";
                }


                DataTable dt_Jobs = new DataTable();

                dt_Jobs = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Jobs where flag='T' and isDelete='1'");


                sResponse = sResponse + "'applyForJob':{";
                sResponse = sResponse + "'title':'Apply for Job',";
                sResponse = sResponse + "'desc':'AstaGuru was established by art connoisseurs and collectors Mr Vickram Sethi & Mr Tushar Sethi in the year 2008, we are Indias premium online art auction house. We constantly innovate and have been venturing into newer terrains such as antiques & collectibles. Modern & Contemporary Indian art is gaining attention globally, creating jobs for individuals who are inclined towards this dynamic world of colours.<br /> <br /> AstaGuru is growing and we are keen on exchanging ideas and working with likeminded individuals. Being a part of our institution gets you well acquainted with this booming industry and provides you with an opportunity to learn about the intangible process of creating art through imagination. Our team comprises of driven individuals and our work culture revolves around having fun while learning and working. So if you see yourself as a person who would enjoy being surrounded by art & antique artifacts, AstaGuru is just the place for you. Be part of this thriving industry and strike a balance between job satisfaction and soul satisfaction. Share your details with us and upload an updated resume.',";

                if (dt_Jobs.Rows.Count > 0)
                {
                    sResponse = sResponse + "'jobTitle':[";

                    for (int j = 0; j < dt_Jobs.Rows.Count; j++)
                    {
                        sResponse = sResponse + "{'id':'" + dt_Jobs.Rows[j]["Id"].ToString() + "','title':'" + dt_Jobs.Rows[j]["Job_Title"].ToString() + "'}";

                        if (j + 1 != dt_Jobs.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                    sResponse = sResponse + "],";
                }

                sResponse = sResponse + "'howYouKnow':[";
                sResponse = sResponse + "{'id':'1','source':'News Paper'},{'id':'2','source':'Social Media'},{'id':'3','source':'Social Networking'},{'id':'4','source':'Friend'}";

                sResponse = sResponse + "]";
                sResponse = sResponse + "}";

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion vacancies

                #endregion pageContent

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetBlogs()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Blog = new DataTable();

            dt_Blog = BasicFunctionCMS.GetDetailsByDatatableCMS("select *,(select Category_Name from tbl_BlogDetails where tbl_BlogDetails.id=tbl_Blog.Blog_Category) as CatName from tbl_Blog where Flag = 'T' order by Blog_Date desc");

            if (dt_Blog.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Blog.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim().ToLower() + "',";
                sResponse = sResponse + "'description': '" + dt_Blog.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Blog.Rows[0]["Seo_Keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Blog.Rows[0]["Seo_Type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Seo_Image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                #region press
                sResponse = sResponse + "'title':'Blog',";
                sResponse = sResponse + "'array':[";
                int j;
                string Filter = string.Empty;
                string sPageName = string.Empty;

                for (j = 0; j < dt_Blog.Rows.Count; j++)
                {
                    DataTable dt_BlogCategory = new DataTable();

                    //dt_BlogCategory = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_BlogDetails where id in (select distinct blog_category from tbl_Blog where flag='T')");
                    dt_BlogCategory = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_BlogDetails where Id in (select Blog_Category from tbl_Blog where id='" + dt_Blog.Rows[j]["Id"].ToString() + "')");

                    Filter = "";

                    for (int k = 0; k < dt_BlogCategory.Rows.Count; k++)
                    {
                        Filter = Filter + "{'Name':'Categories','Value':'" + dt_BlogCategory.Rows[k]["Category_Name"].ToString() + "'}";
                        if (j + 1 != dt_Blog.Rows.Count)
                        {
                            Filter = Filter + ",";
                        }
                    }

                    sPageName = BasicFunctionCMS.replacesplchar(dt_Blog.Rows[j]["Blog_Title"].ToString().ToLower()) + "-" + dt_Blog.Rows[j]["Id"].ToString();

                    DateTime date = Convert.ToDateTime(dt_Blog.Rows[j]["Blog_Date"].ToString().Replace("'", "").Trim());
                    string Final_date = date.ToString("dddd, dd MMMM yyyy");
                    sResponse = sResponse + "{'Id':'" + dt_Blog.Rows[j]["Id"].ToString().Trim() + "','title':'" + dt_Blog.Rows[j]["Blog_Title"].ToString().Replace("'", "").Trim().ToLower() + "','authorName':'" + dt_Blog.Rows[j]["Author_Name"].ToString().Replace("'", "").Trim() + "','pageName':'" + sPageName + "','credits':'" + dt_Blog.Rows[j]["Author_Name"].ToString().Replace("'", "").Trim() + "','timestamp':'" + Final_date + "','date':'" + dt_Blog.Rows[j]["Blog_Date"].ToString().Replace("'", "").Trim() + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[j]["Blog_Image"].ToString().Replace("'", "").Trim() + "','mobile':'" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[j]["Blog_Image"].ToString().Replace("'", "").Trim() + "'},'Filters':[" + Filter + "]}";

                    if (j + 1 != dt_Blog.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }
                sResponse = sResponse + "]";
                #endregion press

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string GetBlogsDetails(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonPressDetails = JObject.Parse(replacedString);
        string sResponse = "";

        string sPageName = string.Empty;

        try
        {
            string JSONString = string.Empty;
            //string sResponse = string.Empty;

            DataTable dt_BlogDetails = new DataTable();

            dt_BlogDetails = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Blog where Id=" + jsonPressDetails.SelectToken("pageId").ToString().Trim() + "");

            if (dt_BlogDetails.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_BlogDetails.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_BlogDetails.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_BlogDetails.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_BlogDetails.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogDetails.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";


                sResponse = sResponse + "'title': 'Blogs',";
                sResponse = sResponse + "'blogDetail': {";
                sResponse = sResponse + "'title':'" + BasicFunctionCMS.replacesplchar(dt_BlogDetails.Rows[0]["Blog_Title"].ToString()).Trim() + "',";
                sResponse = sResponse + "'image': { 'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogDetails.Rows[0]["Blog_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogDetails.Rows[0]["Blog_Image"].ToString().Replace("'", "").Trim() + "'},";
                sResponse = sResponse + "'credits':'" + dt_BlogDetails.Rows[0]["Author_Name"].ToString().Replace("'", "").Trim() + "',";
                //sResponse = sResponse + "'authorName':'" + dt_BlogDetails.Rows[0]["Author_Name"].ToString().Replace("'", "").Trim() + "',";

                DateTime date = Convert.ToDateTime(dt_BlogDetails.Rows[0]["Blog_Date"].ToString().Replace("'", "").Trim());
                string Final_date = date.ToString("dddd, dd MMMM yyyy");

                sResponse = sResponse + "'timestamp':'" + Final_date + "',";
                sResponse = sResponse + "'desc':'" + dt_BlogDetails.Rows[0]["Blog_ShortDescription"].ToString().Replace("'", "").Replace("fileman/Uploads/", "http://astaguru.bcwebwise.com/AstaGuruCMS/cms/fileman/Uploads/").Trim() + "',";
                // sResponse = sResponse + "'readMoreUrl':'" + dt_BlogDetails.Rows[0]["External_Link"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "}";

                #endregion pageContent

                DataTable dt_RemainingBlogDetails = new DataTable();

                dt_RemainingBlogDetails = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Blog where Flag = 'T' and Id!=" + jsonPressDetails.SelectToken("pageId").ToString().Trim() + "");
                string array = string.Empty;
                if (dt_RemainingBlogDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_RemainingBlogDetails.Rows.Count; i++)
                    {
                        sPageName = BasicFunctionCMS.replacesplchar(dt_RemainingBlogDetails.Rows[i]["Blog_Title"].ToString().ToLower()) + "-" + dt_RemainingBlogDetails.Rows[i]["Id"].ToString();

                        DateTime date2 = Convert.ToDateTime(dt_BlogDetails.Rows[0]["Blog_Date"].ToString().Replace("'", "").Trim());
                        string Final_date2 = date2.ToString("dddd, dd MMMM yyyy");
                        array = array + "{'title':'" + BasicFunctionCMS.replacesplchar(dt_RemainingBlogDetails.Rows[i]["Blog_Title"].ToString().ToLower()) + "','image':{'desktop':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_RemainingBlogDetails.Rows[i]["Blog_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_RemainingBlogDetails.Rows[i]["Blog_Image"].ToString().Replace("'", "").Trim() + "'},'credits':'" + dt_RemainingBlogDetails.Rows[i]["Author_Name"].ToString().Replace("'", "").Trim() + "','pageName':'" + sPageName + "','timestamp':'" + Final_date2 + "'}";

                        if (i + 1 != dt_RemainingBlogDetails.Rows.Count)
                        {
                            array = array + ",";
                        }
                    }

                }
                sResponse = sResponse + ",'array':[" + array + "]" + "}";
                sResponse = sResponse + "}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string GetOurCollections()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Blog = new DataTable();
            DataTable dt_Collections = new DataTable();

            dt_Blog = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_OurCollector where Flag = 'T'");

            if (dt_Blog.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Blog.Rows[0]["Seo_Title"].ToString() + "',";
                sResponse = sResponse + "'description': '" + dt_Blog.Rows[0]["Seo_Description"].ToString() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Blog.Rows[0]["Seo_keywords"].ToString() + "',";
                sResponse = sResponse + "'type': '" + dt_Blog.Rows[0]["Seo_type"].ToString() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Seo_image"].ToString() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";
                sResponse = sResponse + "'banner': {";
                dt_Collections = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ManageBuySell where Page_Name='OurCollector'");

                #region press
                if (dt_Collections.Rows.Count > 0)
                {
                    sResponse = sResponse + "'banner':'Collector',";
                    sResponse = sResponse + "'title': 'Our Collectors','desc': '" + dt_Collections.Rows[0]["Banner_ShortDescription"].ToString() + "'},";

                }
                else
                {
                    sResponse = sResponse + "'banner':'Collector',";
                    sResponse = sResponse + "'title': 'Our Collectors','desc': ''},";
                }
                int j;
                string Filter = string.Empty;
                string sPageName = string.Empty;


                sResponse = sResponse + "'collection':[";

                for (j = 0; j < dt_Blog.Rows.Count; j++)
                {
                    sResponse = sResponse + "{'image':{'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[j]["Collector_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[j]["Collector_Image"].ToString().Replace("'", "").Trim() + "'},'videoUrl':'" + dt_Blog.Rows[j]["Collector_VideoLink"].ToString().Replace("'", "").Trim() + "','profileImg':'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[j]["Collector_ThumbImage"].ToString().Replace("'", "").Trim() + "','name':'" + dt_Blog.Rows[j]["Collector_ExpertName"].ToString().Replace("'", "").Trim() + "','qualification':'" + dt_Blog.Rows[j]["Collector_Designation"].ToString().Replace("'", "").Trim() + "'}";

                    if (j + 1 != dt_Blog.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]";
                #endregion press

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }

            #endregion pageContent
        }

        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string GetArtMovement()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Blog = new DataTable();

            dt_Blog = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_AboutArt where Flag = 'T'");

            if (dt_Blog.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Blog.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Blog.Rows[0]["Seo_Description"].ToString().Replace("'", "").Replace("<p>", "").Replace("</p>", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Blog.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Blog.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";
                sResponse = sResponse + "'banner': {";

                #region press
                sResponse = sResponse + "'title': 'Art Movement',";

                sResponse = sResponse + "'image':{";
                //for (int j = 0; j < dt_Blog.Rows.Count; j++)
                //{
                sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Art_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Art_Image"].ToString().Replace("'", "").Trim() + "'}},";
                //    if (j + 1 != dt_Blog.Rows.Count)
                //    {
                //        sResponse = sResponse + ",";
                //    }
                //}
                //sResponse = sResponse + "}";
                #endregion press

                //sResponse = sResponse + "},";
                sResponse = sResponse + "'aboutArt':{";
                sResponse = sResponse + "'title':'About " + dt_Blog.Rows[0]["Art_Name"].ToString() + "',";
                sResponse = sResponse + "'desc':";
                sResponse = sResponse + "'" + dt_Blog.Rows[0]["Art_Description"].ToString().Replace("<p>", "").Replace("</p>", "") + "'";
                sResponse = sResponse + "},";


                DataTable dt_history = new DataTable();
                string year = "2022";//DateTime.Now.Year.ToString();
                                     // dt_history = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_History where Flag = 'T' and History_Year != '" + year + "'");
                dt_history = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_History where Flag = 'T'");

                if (dt_history.Rows.Count > 0)
                {
                    sResponse = sResponse + "'history':{";
                    sResponse = sResponse + "'title':'History',";
                    sResponse = sResponse + "'array':[";
                    for (int j = 0; j < dt_history.Rows.Count; j++)
                    {
                        sResponse = sResponse + "{'year':'" + dt_history.Rows[j]["History_Year"].ToString() + "','image':{'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_history.Rows[j]["History_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_history.Rows[j]["History_Image"].ToString().Replace("'", "").Trim() + "'},'desc':'" + dt_history.Rows[j]["History_Discription"].ToString() + "'}";
                        if (j + 1 != dt_history.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }
                sResponse = sResponse + "]";
                sResponse = sResponse + "},";

                sResponse = sResponse + "'artVideos':{";
                sResponse = sResponse + "'title':'Videos on Art Name',";
                sResponse = sResponse + "'array':[";
                DataTable dt_Video = new DataTable();

                dt_Video = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_ArtVideos where Flag = 'T'");

                if (dt_Video.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_Video.Rows.Count; j++)
                    {

                        sResponse = sResponse + "{'image':{'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Video.Rows[j]["Video_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Video.Rows[j]["Video_Image"].ToString().Replace("'", "").Trim() + "'},'videoUrl':'" + dt_Video.Rows[j]["Video_Link"].ToString().Replace("'", "").Trim() + "','title':'" + dt_Video.Rows[j]["Video_Description"].ToString() + "','Date':'" + dt_Video.Rows[j]["Video_Date"].ToString() + "'}";
                        if (j + 1 != dt_Video.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }
                sResponse = sResponse + "]";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent


                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetAuctionGuide()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Blog = new DataTable();

            dt_Blog = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_AuctionGuide where Flag = 'T'");

            if (dt_Blog.Rows.Count > 0)
            {
                sResponse = "{";


                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Blog.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Blog.Rows[0]["Seo_Description"].ToString().Replace("'", "").Replace("<p>", "").Replace("</p>", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Blog.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Blog.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";



                sResponse = sResponse + "'pageContent': {";



                sResponse = sResponse + "'title': 'Auction Guide',";

                sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["Auction_Desc"].ToString() + "',";

                sResponse = sResponse + "'array':[";

                sResponse = sResponse + "{";
                sResponse = sResponse + "'title':'About Auction',";
                sResponse = sResponse + "'dataTab':'aboutauction_tab',";
                sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["About_Auction"].ToString() + "'";
                sResponse = sResponse + "},";
                sResponse = sResponse + "{";
                sResponse = sResponse + "'title':'Buying Tips',";
                sResponse = sResponse + "'dataTab':'buyingTips_tab',";
                sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["Buying_Tips"].ToString() + "'";
                sResponse = sResponse + "},";
                sResponse = sResponse + "{";
                sResponse = sResponse + "'title':'Selling Tips',";
                sResponse = sResponse + "'dataTab':'sellingTips_tab',";
                sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["Selling_Tips"].ToString() + "'";
                sResponse = sResponse + "},";
                sResponse = sResponse + "{";
                sResponse = sResponse + "'title':'What is Proxy Bid?',";
                sResponse = sResponse + "'dataTab':'proxybid_tab',";
                sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["What_is_Proxy_Bid"].ToString() + "'";
                sResponse = sResponse + "},";

                sResponse = sResponse + "{";
                sResponse = sResponse + "'title':'Assistance',";
                sResponse = sResponse + "'dataTab':'assistance_tab',";
                sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["Assistance"].ToString() + "'";
                sResponse = sResponse + "}";
                sResponse = sResponse + "]";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";


                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));

            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetNewsVideos()
    {
        try
        {
            string JSONString = string.Empty;
            string sResponse = string.Empty;

            DataTable dt_Blog = new DataTable();

            dt_Blog = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_News where Flag = 'T'");

            if (dt_Blog.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_Blog.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_Blog.Rows[0]["Seo_Description"].ToString().Replace("'", "").Replace("<p>", "").Replace("</p>", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_Blog.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_Blog.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo



                sResponse = sResponse + "'pageContent': {";
                // sResponse = sResponse + "'banner': {";


                sResponse = sResponse + "'title': 'News & Videos',";
                sResponse = sResponse + "'banner':{";
                sResponse = sResponse + "'title':'Career Opportunities at AstaGuru',";
                sResponse = sResponse + " 'image':{";
                sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Thumb_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Thumb_Image"].ToString().Replace("'", "").Trim() + "'";
                sResponse = sResponse + "}";
                sResponse = sResponse + "},";

                sResponse = sResponse + "'latestnews':{";
                sResponse = sResponse + "'title': 'Latest News',";
                // sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["Auction_Desc"].ToString() + "',";
                //for (int j = 0; j < dt_Blog.Rows.Count; j++)
                //{
                //sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Art_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Art_Image"].ToString().Replace("'", "").Trim() + "'}},";
                //    if (j + 1 != dt_Blog.Rows.Count)
                //    {
                //        sResponse = sResponse + ",";
                //    }
                //}

                //sResponse = sResponse + "}";
                sResponse = sResponse + "'array':[";

                if (dt_Blog.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_Blog.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{";
                        sResponse = sResponse + "'title':'News Title',";
                        sResponse = sResponse + " 'image':{";
                        sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[i]["Thumb_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[i]["Thumb_Image"].ToString().Replace("'", "").Trim() + "'";
                        sResponse = sResponse + "},";

                        DateTime date = Convert.ToDateTime(dt_Blog.Rows[i]["News_Date"].ToString());
                        string Final_date = date.ToString("dddd, dd MMMM yyyy");



                        sResponse = sResponse + "'timestamp':'" + Final_date + "',";

                        sResponse = sResponse + "'pageId':'" + dt_Blog.Rows[i]["Id"].ToString() + "','pageName':'" + BasicFunctionCMS.replacesplchar(dt_Blog.Rows[i]["News_Description"].ToString()) + "-" + dt_Blog.Rows[i]["Id"].ToString() + "'";
                        sResponse = sResponse + "}";


                        if (i + 1 != dt_Blog.Rows.Count)
                        {
                            sResponse = sResponse + ",";
                        }
                    }
                }
                sResponse = sResponse + "]";
                sResponse = sResponse + "}";

                DataTable dt_BlogVideos = new DataTable();

                dt_BlogVideos = BasicFunctionCMS.GetDetailsByDatatableCMS("select * from tbl_Videos where Flag = 'T'");
                if (dt_BlogVideos.Rows.Count > 0)
                {
                    sResponse = sResponse + ",";
                    sResponse = sResponse + "'videos':{";
                    sResponse = sResponse + "'title': 'Videos',";
                    // sResponse = sResponse + "'desc':'" + dt_Blog.Rows[0]["Auction_Desc"].ToString() + "',";
                    //for (int j = 0; j < dt_Blog.Rows.Count; j++)
                    //{
                    //sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Art_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_Blog.Rows[0]["Art_Image"].ToString().Replace("'", "").Trim() + "'}},";
                    //    if (j + 1 != dt_Blog.Rows.Count)
                    //    {
                    //        sResponse = sResponse + ",";
                    //    }
                    //}

                    //sResponse = sResponse + "}";
                    sResponse = sResponse + "'array':[";

                    if (dt_BlogVideos.Rows.Count > 0)
                    {
                        List<string> Interviews = new List<string>();
                        List<string> Shipping = new List<string>();
                        List<string> Packaging = new List<string>();


                        for (int i = 0; i < dt_BlogVideos.Rows.Count; i++)
                        {


                            if (dt_BlogVideos.Rows[i]["Video_Category"].ToString() == "Interviews")
                            {
                                string sResponse1 = string.Empty;
                                // sResponse1 = sResponse1 + "'videoType':'" + dt_BlogVideos.Rows[i]["Video_Category"].ToString() + "',";
                                // sResponse = sResponse + "'inArray':";
                                sResponse1 = sResponse1 + "{";
                                sResponse1 = sResponse1 + "'title':'Story Title1: Lorem ipsum dolor sit amet consectetur.',";
                                sResponse1 = sResponse1 + " 'image':{";
                                sResponse1 = sResponse1 + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Video_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Video_Image"].ToString().Replace("'", "").Trim() + "'";
                                sResponse1 = sResponse1 + "},";

                                DateTime date = Convert.ToDateTime(dt_BlogVideos.Rows[i]["Video_Date"].ToString());
                                string Final_date = date.ToString("dddd, dd MMMM yyyy");

                                sResponse1 = sResponse1 + "'timestamp':'" + Final_date + "',";

                                // sResponse = sResponse + "'pageName':'" + HtmlRemoval.StripTagsRegex(dt_BlogVideos.Rows[i]["News_Description"].ToString()) + "-" + dt_BlogVideos.Rows[i]["Id"].ToString() + "',";
                                sResponse1 = sResponse1 + "'videoUrl':'" + dt_BlogVideos.Rows[i]["Video_Link"].ToString() + "'";
                                sResponse1 = sResponse1 + "}";
                                Interviews.Add(sResponse1);
                            }
                            else if (dt_BlogVideos.Rows[i]["Video_Category"].ToString() == "Shipping")
                            {
                                string sResponse1 = string.Empty;
                                // sResponse1 = sResponse1 + "'videoType':'" + dt_BlogVideos.Rows[i]["Video_Category"].ToString() + "',";
                                // sResponse = sResponse + "'inArray':";
                                sResponse1 = sResponse1 + "{";
                                sResponse1 = sResponse1 + "'title':'Story Title1: Lorem ipsum dolor sit amet consectetur.',";
                                sResponse1 = sResponse1 + " 'image':{";
                                sResponse1 = sResponse1 + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Video_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Video_Image"].ToString().Replace("'", "").Trim() + "'";
                                sResponse1 = sResponse1 + "},";

                                sResponse1 = sResponse1 + "'timestamp':'" + dt_BlogVideos.Rows[i]["Video_Date"].ToString() + "',";

                                // sResponse = sResponse + "'pageName':'" + HtmlRemoval.StripTagsRegex(dt_BlogVideos.Rows[i]["News_Description"].ToString()) + "-" + dt_BlogVideos.Rows[i]["Id"].ToString() + "',";
                                sResponse1 = sResponse1 + "'videoUrl':'" + dt_BlogVideos.Rows[i]["Video_Link"].ToString() + "'";
                                sResponse1 = sResponse1 + "}";
                                Shipping.Add(sResponse1);
                            }
                            else if (dt_BlogVideos.Rows[i]["Video_Category"].ToString() == "Packaging")
                            {
                                string sResponse1 = string.Empty;
                                // sResponse1 = sResponse1 + "'videoType':'" + dt_BlogVideos.Rows[i]["Video_Category"].ToString() + "',";
                                // sResponse = sResponse + "'inArray':";
                                sResponse1 = sResponse1 + "{";
                                sResponse1 = sResponse1 + "'title':'Story Title1: Lorem ipsum dolor sit amet consectetur.',";
                                sResponse1 = sResponse1 + " 'image':{";
                                sResponse1 = sResponse1 + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Video_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Video_Image"].ToString().Replace("'", "").Trim() + "'";
                                sResponse1 = sResponse1 + "},";

                                sResponse1 = sResponse1 + "'timestamp':'" + dt_BlogVideos.Rows[i]["Video_Date"].ToString() + "',";

                                // sResponse = sResponse + "'pageName':'" + HtmlRemoval.StripTagsRegex(dt_BlogVideos.Rows[i]["News_Description"].ToString()) + "-" + dt_BlogVideos.Rows[i]["Id"].ToString() + "',";
                                sResponse1 = sResponse1 + "'videoUrl':'" + dt_BlogVideos.Rows[i]["Video_Link"].ToString() + "'";
                                sResponse1 = sResponse1 + "}";
                                Packaging.Add(sResponse1);
                            }
                            //sResponse = sResponse + "'videoType':'"+ dt_BlogVideos.Rows[i]["Video_Category"].ToString() + "',";
                            //sResponse = sResponse + "'inArray':";
                            //sResponse = sResponse + " 'image':{";
                            //sResponse = sResponse + "'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Thumb_Image"].ToString().Replace("'", "").Trim() + "','mobile': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_BlogVideos.Rows[i]["Thumb_Image"].ToString().Replace("'", "").Trim() + "'";
                            //sResponse = sResponse + "}";

                            //sResponse = sResponse + "'timestamp':'" + dt_BlogVideos.Rows[i]["News_Date"].ToString() + "',";

                            //sResponse = sResponse + "'pageName':'" + HtmlRemoval.StripTagsRegex(dt_BlogVideos.Rows[i]["News_Description"].ToString()) + "-" + dt_BlogVideos.Rows[i]["Id"].ToString() + "',";
                            //sResponse = sResponse + "'videoUrl':'" + dt_BlogVideos.Rows[i]["Video_Link"].ToString() + "'";
                            //sResponse = sResponse + "}";
                        }
                        string Interviews_final = string.Join(",", Interviews);
                        string Shipping_final = string.Join(",", Shipping);
                        string Packaging_final = string.Join(",", Packaging);

                        sResponse = sResponse + "{'videoType':'Interviews',";
                        sResponse = sResponse + "'inArray':[" + Interviews_final + "]},";

                        sResponse = sResponse + "{'videoType':'Shipping',";
                        sResponse = sResponse + "'inArray':[" + Shipping_final + "]},";

                        sResponse = sResponse + "{'videoType':'Packaging',";
                        sResponse = sResponse + "'inArray':[" + Packaging_final + "]}";

                        //if (i + 1 != dt_BlogVideos.Rows.Count)
                        //{
                        //    sResponse = sResponse + ",";
                        //}

                    }
                    sResponse = sResponse + "]";
                    sResponse = sResponse + "}";
                }
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));

            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetArtistsListing()
    {
        string sResponse = "";
        string JSONString = string.Empty;

        try
        {

            DataTable db_Artist = new DataTable();
            //string str = "select Lot_Bid_EstimateFrom,Lot_Bid_EstimateTo from  ManageLotMaster  where Lot_ID='" + jsonReg.SelectToken("LotId").ToString().Trim() + "'";
            db_Artist = BasicFunction.GetDetailsByDatatableCRM("select * from Artist where (IsActive=1 or IsActive=4 or IsActive=5)");

            if (db_Artist.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '',";
                sResponse = sResponse + "'description': '',";
                sResponse = sResponse + "'keywords': '',";
                sResponse = sResponse + "'type': '',";
                sResponse = sResponse + "'image': ''";


                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                sResponse = sResponse + "'artists': {";
                sResponse = sResponse + "'array':[";

                #region banner
                if (db_Artist.Rows.Count > 0)
                {
                    for (int i = 0; i < db_Artist.Rows.Count; i++)
                    {
                        sResponse = sResponse + "{";
                        sResponse = sResponse + "'image':{";
                        sResponse = sResponse + "'desktop': '" + db_Artist.Rows[i]["Artist_Profile_Images"].ToString() + "',";
                        sResponse = sResponse + "'mobile': '" + db_Artist.Rows[i]["Artist_Profile_Images"].ToString() + "',";
                        sResponse = sResponse + "},";
                        sResponse = sResponse + "'name':'" + db_Artist.Rows[i]["ArtistName"].ToString() + "',";
                        sResponse = sResponse + "'pageName':'" + BasicFunction.replacesplchar(db_Artist.Rows[i]["ArtistName"].ToString().Trim()) + "-" + db_Artist.Rows[i]["ID"].ToString().Trim() + "','pageId' : '" + db_Artist.Rows[i]["ID"].ToString().Trim() + "'";

                        sResponse = sResponse + ",'Filters': [{'Name': 'Department','Value': 'Modern Art'}]";

                        if (i + 1 != db_Artist.Rows.Count)
                        {
                            sResponse = sResponse + "},";
                        }
                        else
                        {
                            sResponse = sResponse + "}";
                        }
                    }
                }

                #endregion banner

                sResponse = sResponse + "]";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                #endregion pageContent

                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }


        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetArtistProfile(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonPressDetails = JObject.Parse(replacedString);
        string sResponse = "";

        string sPageName = string.Empty;

        try
        {
            string JSONString = string.Empty;
            //string sResponse = string.Empty;

            DataTable dt_BlogDetails = new DataTable();

            dt_BlogDetails = BasicFunction.GetDetailsByDatatableCRM("select * from Artist where (IsActive=1 or IsActive=4 or IsActive=5) and Id=" + jsonPressDetails.SelectToken("pageId").ToString().Trim() + "");

            if (dt_BlogDetails.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '',";
                sResponse = sResponse + "'description': '',";
                sResponse = sResponse + "'keywords': '',";
                sResponse = sResponse + "'type': '',";
                sResponse = sResponse + "'image': ''";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";


                sResponse = sResponse + "'banner': {";
                sResponse = sResponse + "'title':'Artists Profile',";
                sResponse = sResponse + "'image': { 'desktop': '" + dt_BlogDetails.Rows[0]["Artist_Profile_Images"].ToString().Replace("'", "").Trim() + "','mobile': '" + dt_BlogDetails.Rows[0]["Artist_Profile_Images"].ToString().Replace("'", "").Trim() + "'},";

                sResponse = sResponse + "'name':'" + dt_BlogDetails.Rows[0]["ArtistName"].ToString() + "',";
                sResponse = sResponse + "'bornYear':'1960',";
                sResponse = sResponse + "'desc':'" + dt_BlogDetails.Rows[0]["Description"].ToString().Replace("'", "") + "',";

                sResponse = sResponse + "},";

                #endregion pageContent

                //sResponse = sResponse + "'currentAuctionLot':{";
                //sResponse = sResponse + "'title':'Current Auction Lot',";
                //sResponse = sResponse + "'array':[{}]";
                //sResponse = sResponse + "},";
                //sResponse = sResponse + "'pastLot':{";
                //sResponse = sResponse + "'title':'Past Lot',";
                //sResponse = sResponse + "'array':[{}]}";
                //sResponse = sResponse + "}}";

                //JObject jsonServicesobj = JObject.Parse(sResponse);
                //JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                //return Convert.ToBase64String(EncryptStringAES(JSONString));

                sResponse = sResponse + "'currentAuctionLot':{";
                sResponse = sResponse + "'title':'Current Auction Lot',";

                string userid = "0";
                string Status = "UpComming";
                string id = "41";

                DataTable db_mail = new DataTable();
                db_mail = BasicFunction.GetDetailsByDatatableCRM("select AuctionId from AuctionListMaster where status='" + Status + "'");
                //status
                if (db_mail.Rows.Count > 0)
                {
                    //id = db_mail.Rows[0]["AuctionId"].ToString();
                }
                string Result = BasicFunction.Upcoming_Lots(id, userid, Status);
                Result = Result.Replace("UpComming", "UpComing");
                Result = Result.Replace("Current", "Live");
                Result = Result.Replace("LiveBid", "CurrentBid");


                sResponse = sResponse + "'array':[" + Result + "]";
                sResponse = sResponse + "},";

                // userid = "0";
                Status = "Past";
                // DataTable db_mail = new DataTable();
                id = "48";
                db_mail = BasicFunction.GetDetailsByDatatableCRM("select AuctionId from AuctionListMaster where status='" + Status + "'");
                //
                if (db_mail.Rows.Count > 0)
                {
                    //id = db_mail.Rows[0]["AuctionId"].ToString();
                }
                Result = BasicFunction.Upcoming_Lots(id, userid, Status);
                Result = Result.Replace("UpComming", "UpComing");
                Result = Result.Replace("Current", "Live");
                Result = Result.Replace("LiveBid", "CurrentBid");

                sResponse = sResponse + "'pastLot':{";
                sResponse = sResponse + "'title':'Past Lot',";
                sResponse = sResponse + "'array':[" + Result + "]}";
                sResponse = sResponse + "}}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetNewsDetails()
    {
        //string decryptedString = d;

        //decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        //string replacedString = DecryptStringAES(decryptedString);
        //replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        //JObject jsonPressDetails = JObject.Parse(replacedString);
        //string sResponse = "";
        string sResponse = "";
        string JSONString = string.Empty;

        try
        {
            //string JSONString = string.Empty;
            //string sResponse = string.Empty;

            DataTable dt_PressDetails = new DataTable();

            // dt_PressDetails = BasicFunctionCMS.GetDetailsByDatatableCMS("Select Id, '~/upload/' + News_Image as News_Image,'~/upload/' + Thumb_Image as Thumb_Image, Left(News_Description, 200) as News_Description, News_Date, Flag From tbl_News");
            dt_PressDetails = BasicFunctionCMS.GetDetailsByDatatableCMS("Select * From tbl_News where Flag='T'");
            //and Id=" + jsonPressDetails.SelectToken("pageId").ToString().Trim() + "

            if (dt_PressDetails.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_PressDetails.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_PressDetails.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_PressDetails.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_PressDetails.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_PressDetails.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";


                sResponse = sResponse + "'title': 'News',";
                sResponse = sResponse + "'newsDetail': {";
                sResponse = sResponse + "'title':'News Name Lorem ipsum dolor sit amet, consetetur.',";
                sResponse = sResponse + "'image': { 'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_PressDetails.Rows[0]["News_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_PressDetails.Rows[0]["News_Image"].ToString().Replace("'", "").Trim() + "'},";
                sResponse = sResponse + "'credits':'Anand Roy',";
                sResponse = sResponse + "'timestamp':'" + dt_PressDetails.Rows[0]["News_Date"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'desc':'" + dt_PressDetails.Rows[0]["News_Description"].ToString().Replace("'", "").Trim() + "',";
                //sResponse = sResponse + "'readMoreUrl':'" + dt_PressDetails.Rows[0]["External_Link"].ToString().Replace("'", "").Trim() + "'";
                sResponse = sResponse + "'readMoreUrl':''";

                sResponse = sResponse + "}";

                #endregion pageContent

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetFooterPage(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonFooterPage = JObject.Parse(replacedString);
        string sResponse = "";
        string JSONString = string.Empty;

        try
        {
            DataTable dt_FooterPage = new DataTable();


            //dt_FooterPage = BasicFunctionCMS.GetDetailsByDatatableCMS("Select * From tbl_ManageBuySell where Flag='T' and Page_Name = '" + jsonFooterPage.SelectToken("FooterPageName").ToString().Trim() + "'");

            string str = jsonFooterPage.SelectToken("FooterPageName").ToString().Trim();

            if (str == "Terms-Conditions")
            {
                str = "Terms & Conditions";
            }
            if (str == "privacy-policy")
            {
                str = "Privacy Policy";
            }
            if (str == "disclaimer")
            {
                str = "Disclaimer";
            }
            if (str == "cookie-policy")
            {
                str = "Cookie Policy";
            }

            dt_FooterPage = BasicFunctionCMS.GetDetailsByDatatableCMS("Select * From tbl_ManageBuySell where Flag='T' and Page_Name = '" + str + "'");


            if (dt_FooterPage.Rows.Count > 0)
            {
                sResponse = "{";

                #region seo
                sResponse = sResponse + " 'seo': {";
                sResponse = sResponse + "'title': '" + dt_FooterPage.Rows[0]["Seo_Title"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'description': '" + dt_FooterPage.Rows[0]["Seo_Description"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'keywords': '" + dt_FooterPage.Rows[0]["Seo_keywords"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'type': '" + dt_FooterPage.Rows[0]["Seo_type"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_FooterPage.Rows[0]["Seo_image"].ToString().Replace("'", "").Trim() + "'";

                sResponse = sResponse + "},";

                #endregion seo

                #region pageContent

                sResponse = sResponse + "'pageContent': {";
                sResponse = sResponse + "'banner': {";
                sResponse = sResponse + "'title':'" + dt_FooterPage.Rows[0]["Page_Name"].ToString().Replace("'", "").Trim() + "',";
                sResponse = sResponse + "'image': { 'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_FooterPage.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_FooterPage.Rows[0]["Banner_Image"].ToString().Replace("'", "").Trim() + "'},";
                sResponse = sResponse + "'desc':'" + dt_FooterPage.Rows[0]["Banner_ShortDescription"].ToString().Replace("'", "").Trim() + "'";
                sResponse = sResponse + "}";

                #endregion pageContent

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string HomeBanner()
    {

        string sResponse = "";
        string JSONString = string.Empty;

        try
        {
            DataTable dt_HomeBanner = new DataTable();
            dt_HomeBanner = BasicFunctionCMS.GetDetailsByDatatableCMS("Select * From tbl_NewAuction where Flag='T' order by Id desc");


            if (dt_HomeBanner.Rows.Count > 0)
            {
                sResponse = "{";

                #region pageContent

                sResponse = sResponse + "'pageContent': {";

                sResponse = sResponse + "'banner': [";

                for (int i = 0; i < dt_HomeBanner.Rows.Count; i++)
                {
                    sResponse = sResponse + "{";
                    sResponse = sResponse + "'title_1': '" + dt_HomeBanner.Rows[i]["Page_Title"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'title_2': '" + dt_HomeBanner.Rows[i]["Page_Title2"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'title_3':'" + dt_HomeBanner.Rows[i]["Page_Date"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'button':{";
                    sResponse = sResponse + "'text':'" + dt_HomeBanner.Rows[i]["Page_Text"].ToString().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'cta':{";
                    sResponse = sResponse + "'type':'" + dt_HomeBanner.Rows[i]["Page_Type"].ToString().ToLower().Replace("'", "").Trim() + "',";
                    sResponse = sResponse + "'link':'" + dt_HomeBanner.Rows[i]["Page_Link"].ToString().Replace("'", "").Trim() + "'";
                    sResponse = sResponse + "}";
                    sResponse = sResponse + "},";
                    sResponse = sResponse + "'image': { 'desktop': 'http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_HomeBanner.Rows[i]["Banner_Image"].ToString().Replace("'", "").Trim() + "','mobile': '" + "http://astaguru.bcwebwise.com/AstaGuruCMS/upload/" + dt_HomeBanner.Rows[i]["Mobile_Image"].ToString().Replace("'", "").Trim() + "'}";


                    if (i + 1 != dt_HomeBanner.Rows.Count)
                    {
                        sResponse = sResponse + "},";
                    }
                    else
                    {
                        sResponse = sResponse + "}";
                    }
                }
                sResponse = sResponse + "]";
                #endregion pageContent

                sResponse = sResponse + "}";
                sResponse = sResponse + "}";

                JObject jsonServicesobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonServicesobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Records Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string HomeUpcomingAuctions()
    {
        string sResponse = "";
        string JSONString = string.Empty;
        try
        {
            DataTable dt_HomeBanner = new DataTable();
            dt_HomeBanner = BasicFunctionCMS.GetDetailsByDatatableCRM(@"select  *,
                                CASE WHEN auctiondate != 'TBA' THEN CONVERT(varchar, auctiondate, 23) END AS auctiondate11,
                                CASE WHEN auctiondate != 'TBA' THEN DATENAME(month, auctiondate) END AS DisplayMonth,
                                CASE WHEN auctiondate != 'TBA' THEN DATENAME(month, auctionenddate) END AS DisplayMonthTo,
                                CASE WHEN auctiondate != 'TBA' THEN day(auctiondate) END AS DateFrom,
                                CASE WHEN auctiondate != 'TBA' THEN day(auctionenddate) END AS DateTo,
                                CASE WHEN auctiondate != 'TBA' THEN year(auctiondate) END AS DateYear,
                                CASE WHEN auctiondate != 'TBA' THEN convert(varchar(10), cast(auctiondate as date), 120)

                                 END AS auctiondate1,
                                convert(varchar(10), cast(auctionenddate as date), 120)

                                 AS AuctionEndDate,
                                '' as DisplayDate, '' as AuctionURL from AuctionListMaster
                                where status = 'UpComming' and isactive = '1'");

            sResponse = sResponse + "{";
            sResponse = sResponse + "'heading':'Auctions On The Way',";
            sResponse = sResponse + "'desc':'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.'";
            List<string> Auction_detail = new List<string>();
            if (dt_HomeBanner.Rows.Count > 0)
            {
                for (int i = 0; i < dt_HomeBanner.Rows.Count; i++)
                {
                    string year = dt_HomeBanner.Rows[i]["DateYear"].ToString();
                    if (string.IsNullOrEmpty(dt_HomeBanner.Rows[i]["DateYear"].ToString()))
                    {
                        year = DateTime.Now.Year.ToString();
                    }
                    if (dt_HomeBanner.Rows[i]["auctiondate"].ToString() != "TBA")
                    {
                        dt_HomeBanner.Rows[i]["DisplayDate"] = dt_HomeBanner.Rows[i]["DisplayMonth"].ToString().Trim() + " " + dt_HomeBanner.Rows[i]["DateFrom"].ToString().Trim() + " - " + dt_HomeBanner.Rows[i]["DisplayMonthTo"].ToString().Trim() + " " + dt_HomeBanner.Rows[i]["DateTo"].ToString().Trim() + ", " + dt_HomeBanner.Rows[i]["DateYear"].ToString().Trim();
                        dt_HomeBanner.Rows[i]["AuctionURL"] = "/auctions/" + year.Trim() + "/" + BasicFunction.replacesplchar(dt_HomeBanner.Rows[i]["AuctionName"].ToString().Trim()) + "-" + dt_HomeBanner.Rows[i]["AuctionId"].ToString().Trim();
                    }

                    string detail = "{ 'image':'" + dt_HomeBanner.Rows[i]["image"].ToString() + "','title':'" + dt_HomeBanner.Rows[i]["auctiontitle"].ToString() + "','displayDate':'" + dt_HomeBanner.Rows[i]["auctiontitle"].ToString() + "','displayDate':'" + dt_HomeBanner.Rows[i]["DisplayDate"].ToString() + "','link':'" + dt_HomeBanner.Rows[i]["AuctionURL"].ToString() + "' }";
                    Auction_detail.Add(detail);
                }
            }

            string str = string.Join(",", Auction_detail);

            string Result = "" + str + "";
            sResponse = sResponse + ",";
            sResponse = sResponse + "'auctionArray':";

            sResponse = sResponse + "[" + Result + "]";
            sResponse = sResponse + "}";

            JObject jsonServicesobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonServicesobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString));
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }
}
