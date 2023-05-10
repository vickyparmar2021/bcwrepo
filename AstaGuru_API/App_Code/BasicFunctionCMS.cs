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
/// Summary description for BasicFunctionCMS
/// </summary>
public class BasicFunctionCMS
{
    static string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnectionCMS"].ToString();
    static string connCRM = ConfigurationManager.ConnectionStrings["AstaguruConnectionCRM"].ToString();

    public BasicFunctionCMS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable GetDetailsByDatatableCMS(string Query)
    {
        SqlConnection conn = new SqlConnection();

        SqlDataAdapter adp;
        DataTable dTable = new DataTable("DataTable1");
        conn.ConnectionString = connStr;

        adp = new SqlDataAdapter(Query, conn);
        adp.Fill(dTable);
        return dTable;
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

    public static DataTable GetDetailsByDatatableCRM(string Query)
    {
        SqlConnection conn = new SqlConnection();

        SqlDataAdapter adp;
        DataTable dTable = new DataTable("DataTable1");
        conn.ConnectionString = connCRM;

        adp = new SqlDataAdapter(Query, conn);
        adp.Fill(dTable);
        return dTable;
    }
}