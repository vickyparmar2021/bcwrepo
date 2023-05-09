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
/// Summary description for adminBAL
/// </summary>
public class adminBAL
{
    public adminBAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int updatePassword(string password)
    {
        adminDAL edal = new adminDAL();
        try
        {
            return edal.updatePassword(password);
        }
        catch
        {
            throw;
        }
        finally
        {
            edal = null;
        }
    }

    public bool chkExistance(string table_name, string col_name, string col_val)
    {
        adminDAL edal = new adminDAL();
        try
        {
            return edal.chkExistance(table_name, col_name, col_val);
        }
        catch
        {
            throw;
        }
        finally
        {
            edal = null;
        }
    }
    public bool chkExistOnCondition(string table_name, string colname1, string colvalue1, string colname2, string colvalue2)
    {
        adminDAL edal = new adminDAL();
        try
        {
            return edal.chkExistOnCondition(table_name, colname1, colvalue1,colname2,colvalue2);
        }
        catch
        {
            throw;
        }
        finally
        {
            edal = null;
        }
    }

    public DataTable GetExistOnCond(string table_name, string colname1, string colvalue1, string colname2, string colvalue2)
    {
        adminDAL edal = new adminDAL();
        try
        {
            return edal.GetExistOnCond(table_name, colname1, colvalue1, colname2, colvalue2);
        }
        catch
        {
            throw;
        }
        finally
        {
            edal = null;
        }
    }

    public DataTable GetList(string table_name, string orderbycol_name)
    {
        adminDAL bcDAL = new adminDAL();
        try
        {
            return bcDAL.GetList(table_name, orderbycol_name);
        }
        catch
        {
            throw;
        }
        finally
        {
            bcDAL = null;
        }
    }


    public int RejectRecord(string tName, string id)
    {
        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.RejectRecord(tName, id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    public int ApproveRecord(string tName, string id)
    {
        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.ApproveRecord(tName, id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    public int DeleteRecord(string tName, string colName, string colValue)
    {
        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.DeleteRecord(tName, colName, colValue);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    public DataTable RecordListCMS(string tName, string col_name, string col_val, string orderby_col)
    {
        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.RecordListCMS(tName, col_name, col_val, orderby_col);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }
   

    public DataTable GetExistingRecord(string table_name, string col_name, string col_val)
    {
        adminDAL aDAL = new adminDAL();
       
        try
        {
            return aDAL.GetExistingRecord(table_name, col_name, col_val);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    public SqlDataReader GetExistingDetails(string table_name, string col_name, string col_val)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.GetExistingDetails(table_name, col_name, col_val);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }
    //---------Add  Client - Indian
    public int inindianclient(string ClientName)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.inindianclient(ClientName);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Update  Client
    public int updateindianclient(string ClientName, string id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updateindianclient(ClientName,id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Add  Client Address - Indian
    public int inindianclientadd(string ClientId, string ShortAddress, string Address)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.inindianclientadd(ClientId,ShortAddress,Address);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Update  Client Address - Indian
    public int updateindianclientadd(string ClientId, string ShortAddress, string Address,string id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updateindianclientadd(ClientId, ShortAddress, Address,id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }


    //get Clientname - Indian
    public DataTable getindianclient()
    {
        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.getindianclient();
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Add  Client - International
    public int ininternationalclient(string ClientName)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.ininternationalclient(ClientName);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Update  Client - International
    public int updateinternationalclient(string ClientName, string id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updateinternationalclient(ClientName, id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Add  Client Address - International
    public int ininternationalclientadd(string ClientId, string ShortAddress, string Address)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.ininternationalclientadd(ClientId, ShortAddress, Address);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Update  Client Address - International
    public int updateintclientaddress(string ClientId, string ShortAddress, string Address, string id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updateintclientaddress(ClientId, ShortAddress, Address, id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }


    //get Clientname - International
    public DataTable getinterclient()
    {
        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.getinterclient();
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }




    //---------Update  Sales Tax
    public int updatesalestax(string SalesTax, string id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updatesalestax(SalesTax, id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }


    //---------Update  EducationCess
    public int updateEducationCess(string EducationCess, string id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updateEducationCess(EducationCess, id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Add  Invoice No Format
    public int inInvoiceNoFormat(string InvoiceFormat, string InvoiceNumber)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.inInvoiceNoFormat(InvoiceFormat,InvoiceNumber);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }


    //---------Edit  Invoice No Format
    public int updateInvoiceNoFormat(string InvoiceFormat, string InvoiceNumber, string Id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updateInvoiceNoFormat(InvoiceFormat, InvoiceNumber,Id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }

    //---------Add  Invoice No Format - INTERNATIONAL
    public int inInvoiceNoFormatint(string InvoiceFormat, string InvoiceNumber)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.inInvoiceNoFormatint(InvoiceFormat, InvoiceNumber);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }


    //---------Edit  Invoice No Format - INTERNATIONAL
    public int updatetblInvoiceNoFormatInt(string InvoiceFormat, string InvoiceNumber, string Id)
    {

        adminDAL aDAL = new adminDAL();
        try
        {
            return aDAL.updatetblInvoiceNoFormatInt(InvoiceFormat, InvoiceNumber, Id);
        }
        catch
        {
            throw;
        }
        finally
        {
            aDAL = null;
        }
    }
}
