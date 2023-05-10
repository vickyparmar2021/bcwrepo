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

/// <summary>
/// Summary description for BasicFunction
/// </summary>
public class BasicFunction
{
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
        if ((Ext.ToString() == ".jpg" || Ext.ToString() == ".gif" || Ext.ToString() == ".jpeg" || Ext.ToString() == ".bmp"||Ext.ToString() == ".png"))
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

}
