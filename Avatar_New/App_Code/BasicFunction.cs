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
    static string connStr = ConfigurationManager.ConnectionStrings["AvatarConnection"].ToString();

    public BasicFunction()
    {	//
        // TODO: Add constructor logic here
        //
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

    public static string RandomNumber()
    {
        Random generator = new Random();
        return generator.Next(0, 1000000).ToString("D6");
    }



}
