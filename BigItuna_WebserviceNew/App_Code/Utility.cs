using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Drawing;

/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
    /// <summary>
    /// BOOL success;
    /// success = ValidateDate("3403", "MMmm");    // false as 34 is not a valid month
    /// success = ValidateDate("3403", "yymm");    // true
    /// success = ValidateDate("1212", "MMdd");    // true
    /// </summary>
    /// <param name="date"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static bool ValidateDate(String date, String format)
    {
        try
        {
            System.Globalization.DateTimeFormatInfo dtfi = new
                   System.Globalization.DateTimeFormatInfo();
            dtfi.ShortDatePattern = format;
            DateTime dt = DateTime.ParseExact(date, "d", dtfi);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary> 
    /// Shows a client-side JavaScript alert in the browser. 
    /// </summary> 
    /// <param name="message">The message to appear in the alert.</param> 
    public static void Show(string message)
    {
        // Cleans the message to allow single quotation marks 
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string keyy = "key1";
        if (!page.IsStartupScriptRegistered(keyy))
        {
            page.RegisterStartupScript(keyy, script);
        }

    }

    /// <summary> 
    /// Shows a client-side JavaScript alert in the browser. 
    /// </summary> 
    /// <param name="message">The message to appear in the alert.</param> 
    public static void ShowPopup(string message)
    {
        // Cleans the message to allow single quotation marks 
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">" + cleanMessage + ";</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string keyy = "key1";
        if (!page.IsStartupScriptRegistered(keyy))
        {
            page.RegisterStartupScript(keyy, script);
        }

    }

    /// <summary> 
    /// Shows a client-side JavaScript alert in the browser for Update Panel. 
    /// </summary> 
    /// <param name="message">The message to appear in the alert.</param> 
    public static void Show(string message, UpdatePanel panel)
    {
        string cleanMessage = message.Replace("'", "\\'");
        string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string key = "alertshow";
        if (!page.IsStartupScriptRegistered(key))
        {
            ScriptManager.RegisterStartupScript(panel, panel.GetType(), key, script, false);
        }


    }


    /// <summary> 
    /// method used for union two datatable 
    /// </summary> 
    /// <param name="Datatable">datatable which you want to union<param>
    public static DataTable Union(DataTable First, DataTable Second)
    {

        //Result table

        DataTable table = new DataTable("Union");

        //Build new columns

        DataColumn[] newcolumns = new DataColumn[First.Columns.Count];

        for (int i = 0; i < First.Columns.Count; i++)
        {

            newcolumns[i] = new DataColumn(First.Columns[i].ColumnName, First.Columns[i].DataType);

        }

        //add new columns to result table

        table.Columns.AddRange(newcolumns);

        table.BeginLoadData();

        //Load data from first table

        foreach (DataRow row in First.Rows)
        {

            table.LoadDataRow(row.ItemArray, true);

        }

        //Load data from second table

        foreach (DataRow row in Second.Rows)
        {

            table.LoadDataRow(row.ItemArray, true);

        }

        table.EndLoadData();

        return table;


    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }


    public static string GetPreviousPageName()
    {
        string sRet = String.Empty;
        if (HttpContext.Current.Request.UrlReferrer != null)
        {
            string sPath = System.Web.HttpContext.Current.Request.UrlReferrer.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            sRet = oInfo.Name;
        }
        return sRet;
    }

    public static void PageTransfer(String Url)
    {
        string script = "<script type=\"text/javascript\">window.location.href='" + Url + "';</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string keyy = "PageTransfer";
        if (!page.IsStartupScriptRegistered(keyy))
        {
            page.RegisterStartupScript(keyy, script);
        }

    }
    public static void PageTransfer(String Url, UpdatePanel panel)
    {

        string script = "<script type=\"text/javascript\">window.location.href='" + Url + "';</script>";
        Page page = (Page)HttpContext.Current.CurrentHandler;
        string key = "PageTransfer";
        if (!page.IsStartupScriptRegistered(key))
        {
            ScriptManager.RegisterStartupScript(panel, panel.GetType(), key, script, false);
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="Errormsg"></param>
    /// <param name="PageCode"></param>
    /// <returns></returns>
    public static String MessageTitle(ref string Errormsg, String PageCode)
    {
        char[] s = new char[] { '@' };
        string[] strError = Errormsg.Split(s);
        string ErrorTitle = string.Empty;
        if (strError.Length > 1)
        {
            ErrorTitle = PageCode + "-" + strError[1];
            ErrorTitle = ErrorTitle.Replace("]", "");
        }

        Errormsg = strError[0];
        Errormsg = Errormsg.Replace("[", "");

        return ErrorTitle;
    }

    //public static string ShowPopupMsgBox(String Message, String Title, String Url)
    //{
    //    MessageBoxButton btnOK = new MessageBoxButton("OK");
    //    btnOK.SetLocation(Url);
    //    btnOK.SetClass("msg_button_class");


    //    String   browsertype = HttpContext.Current.Request.Browser.Type;
    //    String tplPath;
    //    if (browsertype == "IE6")
    //    {
    //        tplPath = System.Web.HttpContext.Current.Server.MapPath("~/msgboxIE6.tpl");
    //    }
    //    else
    //    {
    //        tplPath = System.Web.HttpContext.Current.Server.MapPath("~/msgbox.tpl");
    //    }
    //    MessageBox msgbox = new MessageBox(tplPath);
    //    msgbox.SetTitle(Title);
    //    Page page = (Page)HttpContext.Current.CurrentHandler;
    //    msgbox.SetIcon(page.ResolveClientUrl("~/Images/msg_icon_1.png"));
    //    msgbox.SetMessage(Message);
    //    msgbox.AddButton(btnOK.ReturnObject());
    //    //msgboxpanel.InnerHtml =
    //    return msgbox.ReturnObject();
    //}

    //public static string  ShowPopupMsgBox(String Message, String Title)
    //{
    //    String browsertype = HttpContext.Current.Request.Browser.Type;
    //    String tplPath;
    //    if (browsertype == "IE6")
    //    {
    //        tplPath = System.Web.HttpContext.Current.Server.MapPath("~/msgboxIE6.tpl");
    //    }
    //    else
    //    {
    //        tplPath = System.Web.HttpContext.Current.Server.MapPath("~/msgbox.tpl");
    //    }
    //    MessageBox msgbox = new MessageBox(tplPath);
    //    msgbox.SetTitle(Title);
    //    Page page = (Page)HttpContext.Current.CurrentHandler;
    //    msgbox.SetIcon(page.ResolveClientUrl ("~/Images/msg_icon_1.png"));
    //    msgbox.SetMessage(Message);
    //    msgbox.SetOKButton("msg_button_class");
    //    //msgboxpanel.InnerHtml = 
    //  return   msgbox.ReturnObject();
    //}

    //To resize image
    public static void ResizeImage(string OrigFile, ref int NewWidth, ref int MaxHeight, bool ResizeIfWider)
    {
        try
        {
            System.Drawing.Image FullSizeImage = System.Drawing.Image.FromFile(OrigFile);
            // Ensure the generated thumbnail is not being used by rotating it 360 degrees
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipNone);
            FullSizeImage.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipNone);

            if (ResizeIfWider)
            {
                if (FullSizeImage.Width <= NewWidth)
                {
                    NewWidth = FullSizeImage.Width;
                }
            }

            int NewHeight = FullSizeImage.Height * NewWidth / FullSizeImage.Width;
            if (NewHeight > MaxHeight) // Height resize if necessary
            {
                NewWidth = FullSizeImage.Width * MaxHeight / FullSizeImage.Height;
                NewHeight = MaxHeight;
            }

            // Create the new image with the sizes we've calculated
            System.Drawing.Image NewImage = FullSizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
            FullSizeImage.Dispose();

            NewWidth = NewWidth;
            MaxHeight = NewHeight;
        }
        catch (Exception ex)
        {

        }

    }
     //To insert no of week for news letter in newsletter table.
    public static bool NewsLetter(DateTime dueDate1)
    {
        DateTime dueDate = dueDate1;
        DateTime dateNow = DateTime.Now;

        TimeSpan ts = dueDate.Subtract(dateNow);
        try
        {
            int days = ts.Days;
            if (days > 287 || days < 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        catch
        {
            return false;
        }
    }

}

