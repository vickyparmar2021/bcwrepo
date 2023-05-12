using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for errorloging
/// </summary>
public class errorloging
{
    SqlConnection con;
    public errorloging()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string ErrorLogInsert(string page, string section, string message,string stackTrace)
    {
        SqlConnection con = new SqlConnection("SERVER=103.228.152.227;Database=cust_heromotocorp;UID=cusheromotocorp;Pwd=motoherocust@123;");

        con.Open();
        SqlCommand cmd_Insert = new SqlCommand("sp_ErrorLog_Insert", con);
        cmd_Insert.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd_Insert.Parameters.AddWithValue("@page", page);
            cmd_Insert.Parameters.AddWithValue("@section", section);
            cmd_Insert.Parameters.AddWithValue("@message", message);
            cmd_Insert.Parameters.AddWithValue("@stackTrace", stackTrace);

            cmd_Insert.ExecuteNonQuery();
            return "success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            cmd_Insert.Dispose();
            con.Close();
            con.Dispose();
        }
    }
}