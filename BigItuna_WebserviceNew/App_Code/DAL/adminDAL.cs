using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;

/// <summary>
/// Summary description for admin
/// </summary>
public class adminDAL
{
    string connStr = ConfigurationManager.ConnectionStrings["constring"].ToString();
    public adminDAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool chkExistance(string table_name, string col_name, string col_val)
    {

        bool s = false;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlDataReader sdtr;
        SqlCommand scmd = new SqlCommand("chkExistance", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", table_name);
            scmd.Parameters.AddWithValue("@col_name", col_name);
            scmd.Parameters.AddWithValue("@col_value", col_val);
            sdtr = scmd.ExecuteReader();
            if (sdtr.HasRows)
            {
                s = true;
            }
            sdtr.Close();
            return s;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    public DataTable GetList(string table_name, string orderbycol_name)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataAdapter sdapt;
        DataTable dTable = new DataTable();
        conn.Open();
        SqlCommand scmd = new SqlCommand("sp_getlist", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", table_name);
            scmd.Parameters.AddWithValue("@col_name", orderbycol_name);
            sdapt = new SqlDataAdapter(scmd);
            sdapt.Fill(dTable);
            return dTable;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();

        }
    }
    public bool chkExistOnCondition(string table_name, string colname1, string colvalue1, string colname2, string colvalue2)
    {

        bool s = false;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlDataReader sdtr;
        SqlCommand scmd = new SqlCommand("chkExistanceonCond", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", table_name);
            scmd.Parameters.AddWithValue("@col_name", colname1);
            scmd.Parameters.AddWithValue("@col_value", colvalue1);
            scmd.Parameters.AddWithValue("@col_name1", colname2);
            scmd.Parameters.AddWithValue("@col_value1", colvalue2);
            sdtr = scmd.ExecuteReader();
            if (sdtr.HasRows)
            {
                s = true;
            }
            sdtr.Close();
            return s;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    public DataTable GetExistOnCond(string table_name, string colname1, string colvalue1, string colname2, string colvalue2)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlDataAdapter sdapt;
        DataTable dTable = new DataTable();
        conn.Open();
        SqlCommand scmd = new SqlCommand("sp_chkexist_cond", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", table_name);
            scmd.Parameters.AddWithValue("@col_name", colname1);
            scmd.Parameters.AddWithValue("@col_value", colvalue1);
            scmd.Parameters.AddWithValue("@col_name2", colname2);
            scmd.Parameters.AddWithValue("@cond_value", colvalue2);
            sdapt = new SqlDataAdapter(scmd);
            sdapt.Fill(dTable);
            return dTable;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    public int updatePassword(string password)
    {
        SqlConnection conn = new SqlConnection(connStr);

        conn.Open();
        SqlCommand scmd = new SqlCommand("UpdateAdminLoginPassword", conn);
        scmd.CommandType = CommandType.StoredProcedure;

        try
        {
            scmd.Parameters.AddWithValue("@Password", password);
            return scmd.ExecuteNonQuery();

        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    public int DeleteRecord(string tName, string colName, string colValue)
    {
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("RecordDelete", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", tName);
            scmd.Parameters.AddWithValue("@col_name", colName);
            scmd.Parameters.AddWithValue("@col_value", colValue);
            return scmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    public int RejectRecord(string tName, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("RecordReject", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", tName);
            scmd.Parameters.AddWithValue("@id", id);
            return scmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    public int ApproveRecord(string tName, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("RecordApprove", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", tName);
            scmd.Parameters.AddWithValue("@id", id);
            return scmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    public DataTable GetExistingRecord(string table_name, string col_name, string col_val)
    {

        SqlConnection conn = new SqlConnection(connStr);
        SqlDataAdapter sdapt;
        DataTable dTable = new DataTable();
        conn.Open();
        SqlCommand scmd = new SqlCommand("chkExistance", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", table_name);
            scmd.Parameters.AddWithValue("@col_name", col_name);
            scmd.Parameters.AddWithValue("@col_value", col_val);
            sdapt = new SqlDataAdapter(scmd);
            sdapt.Fill(dTable);
            return dTable;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn = null;

        }
    }

    public SqlDataReader GetExistingDetails(string table_name, string col_name, string col_val)
    {
        SqlDataReader sdtr;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("chkExistance", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        try
        {
            scmd.Parameters.AddWithValue("@table_name", table_name);
            scmd.Parameters.AddWithValue("@col_name", col_name);
            scmd.Parameters.AddWithValue("@col_value", col_val);
            sdtr = scmd.ExecuteReader();
            return sdtr;
        }
        catch
        {
            throw;
        }
        finally
        {
            sdtr = null;
            scmd.Dispose();
            conn = null;
        }
    }

    public DataTable RecordListCMS(string tName, string col_name, string col_val, string orderby_col)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("RecordListCMS", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sdapt;

        try
        {
            scmd.Parameters.AddWithValue("@table_name", tName);
            scmd.Parameters.AddWithValue("@col_name", col_name);
            scmd.Parameters.AddWithValue("@col_val", col_val);
            scmd.Parameters.AddWithValue("@orderby_col", orderby_col);
            sdapt = new SqlDataAdapter(scmd);
            sdapt.Fill(dt);
            return dt;
        }
        catch
        {
            throw;
        }
        finally
        {
            //sdapt.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Add Client
    public int inindianclient(string ClientName)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("inindianclient", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@ClientName", ClientName);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    //----Update Client
    public int updateindianclient(string ClientName, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updateindianclient", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@ClientName", ClientName);
            scmd.Parameters.AddWithValue("@Id", id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Add Client Address - Indian
    public int inindianclientadd(string ClientId, string ShortAddress, string Address)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("inindianclientadd", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@Client", ClientId);
            scmd.Parameters.AddWithValue("@ShortAddress", ShortAddress);
            scmd.Parameters.AddWithValue("@Address", Address);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Update Client Address - Indian
    public int updateindianclientadd(string ClientId, string ShortAddress, string Address, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updateindianclientadd", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@Client", ClientId);
            scmd.Parameters.AddWithValue("@ShortAddress", ShortAddress);
            scmd.Parameters.AddWithValue("@Address", Address);
            scmd.Parameters.AddWithValue("@Id", id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    // get ClientName - Indian
    public DataTable getindianclient()
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("getindianclient", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sdapt;

        try
        {
            sdapt = new SqlDataAdapter(scmd);
            sdapt.Fill(dt);
            return dt;
        }
        catch
        {
            throw;
        }
        finally
        {
            //sdapt.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }




    //----Add Client -Inernational
    public int ininternationalclient(string ClientName)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("ininternationalclient", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@ClientName", ClientName);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    //----Update Client - -Inernational
    public int updateinternationalclient(string ClientName, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updateinternationalclient", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@ClientName", ClientName);
            scmd.Parameters.AddWithValue("@Id", id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Add Client Address - International
    public int ininternationalclientadd(string ClientId, string ShortAddress, string Address)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("ininternationalclientadd", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@Client", ClientId);
            scmd.Parameters.AddWithValue("@ShortAddress", ShortAddress);
            scmd.Parameters.AddWithValue("@Address", Address);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Update Client Address - International
    public int updateintclientaddress(string ClientId, string ShortAddress, string Address, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updateintclientadd", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@Client", ClientId);
            scmd.Parameters.AddWithValue("@ShortAddress", ShortAddress);
            scmd.Parameters.AddWithValue("@Address", Address);
            scmd.Parameters.AddWithValue("@Id", id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    // get ClientName - International
    public DataTable getinterclient()
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();
        SqlCommand scmd = new SqlCommand("getinterclient", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter sdapt;

        try
        {
            sdapt = new SqlDataAdapter(scmd);
            sdapt.Fill(dt);
            return dt;
        }
        catch
        {
            throw;
        }
        finally
        {
            //sdapt.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    //----Update Sales tAX
    public int updatesalestax(string SalesTax, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updatesalestax", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@SalesTax", SalesTax);
            scmd.Parameters.AddWithValue("@Id", id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    //----Update  EducationCess
    public int updateEducationCess(string EducationCess, string id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updateEducationCess", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@EducationCess", EducationCess);
            scmd.Parameters.AddWithValue("@Id", id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Add Invoice Format
    public int inInvoiceNoFormat(string InvoiceFormat, string InvoiceNumber)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("inInvoiceNoFormat", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@InvoiceFormat", InvoiceFormat);
            scmd.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Edit Invoice Format
    public int updateInvoiceNoFormat(string InvoiceFormat, string InvoiceNumber, string Id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updateInvoiceNoFormat", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@InvoiceFormat", InvoiceFormat);
            scmd.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
            scmd.Parameters.AddWithValue("@Id", Id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }


    //----Add Invoice Format INTERNATIONAL
    public int inInvoiceNoFormatint(string InvoiceFormat, string InvoiceNumber)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("inInvoiceNoFormatint", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@InvoiceFormat", InvoiceFormat);
            scmd.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }

    //----Edit Invoice Format - INTERNATIONAL
    public int updatetblInvoiceNoFormatInt(string InvoiceFormat, string InvoiceNumber, string Id)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlParameter sqlParameter;
        conn.Open();
        SqlCommand scmd = new SqlCommand("updatetblInvoiceNoFormatInt", conn);
        scmd.CommandType = CommandType.StoredProcedure;
        sqlParameter = scmd.Parameters.Add("ReturnValue", SqlDbType.Int);
        sqlParameter.Direction = ParameterDirection.ReturnValue;
        try
        {
            scmd.Parameters.AddWithValue("@InvoiceFormat", InvoiceFormat);
            scmd.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
            scmd.Parameters.AddWithValue("@Id", Id);
            scmd.ExecuteNonQuery();
            int retvalue = (int)scmd.Parameters["ReturnValue"].Value;
            return retvalue;
        }
        catch
        {
            throw;
        }
        finally
        {
            scmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }




}
