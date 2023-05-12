using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SFTPDaily : System.Web.UI.Page
{
    MySqlConnection conMySql = new MySqlConnection("SERVER=103.228.152.225;Database=heromoto_2015;port=3306;UID='heromoto_2015';Pwd='2.~V;XRrBQ6,';convert zero datetime=True");
    SqlConnection conSql = new SqlConnection("SERVER=103.228.152.227;Database=cust_heromotocorp;UID=cusheromotocorp;Pwd=motoherocust@123;");
    //MySqlConnection conMySqlFestive = new MySqlConnection("SERVER=103.228.152.71;Database=festiveh_2021New;port=3306;UID='festiveh_2021new';Pwd='Y&%kePtj&{p=';convert zero datetime=True");

    MySqlDataAdapter adpMySql;
    SqlDataAdapter adpSql;
    SqlDataAdapter adpSqlBikeName;
    MySqlDataAdapter adpYoutube;
   // MySqlDataAdapter adpFestive;

    string strQuery = "";
    string strQuerySql = "";
    string strQuerySqlBikeName = "";
    string strQueryYoutube = "";
    string sStateName = "";
    //string strQueryFestive = "";

    DataTable dTable;
    DataTable dtSql;
    DataTable dtSqlBikeName;
    DataTable sortedDT;
    DataTable dtYoutube;
   // DataTable dtFestive;

    string bikeid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //txtFromTime.Text = DateTime.Now.ToString("HH:mm");
            txtFromTime.Text = DateTime.Now.AddHours(-14).ToString("HH:mm");
            txtToTime.Text = DateTime.Now.ToString("HH:mm");

            if (!IsPostBack)
            {
                ddlUtmTerm.DataSource = GetUtmTerm();
                ddlUtmTerm.DataValueField = "utm_term";
                ddlUtmTerm.DataTextField = "utm_term";
                ddlUtmTerm.DataBind();
                ddlUtmTerm.Items.Insert(0, new ListItem("All UTM Terms"));

                conMySql = new MySqlConnection("SERVER=103.228.152.225;Database=heromoto_2015;port=3306;UID='heromoto_2015';Pwd='2.~V;XRrBQ6,';convert zero datetime=True");
                if (bikeid != "")
                {
                    strQuery = "select prod_id,prod_name from product_master where prod_id in(" + bikeid + ")  order by prod_name";
                }
                else
                {
                    strQuery = "select prod_id,prod_name from product_master  order by prod_name";
                }

                adpMySql = new MySqlDataAdapter(strQuery, conMySql);
                dTable = new DataTable();
                adpMySql.Fill(dTable);

                if (bikeid != "" && bikeid.Contains("2020"))
                {
                    DataRow dr1 = dTable.NewRow();
                    dr1[0] = "2020";
                    dr1[1] = "Other";
                    dTable.Rows.InsertAt(dr1, 0);
                }
                if (bikeid != "" && bikeid.Contains("600"))
                {
                    DataRow dr2 = dTable.NewRow();
                    dr2[0] = "600";
                    dr2[1] = "Maestro Edge 125";
                    dTable.Rows.InsertAt(dr2, 1);
                }
                if (bikeid != "" && bikeid.Contains("2000"))
                {
                    DataRow dr3 = dTable.NewRow();
                    dr3[0] = "2000";
                    dr3[1] = "Other Lead";
                    dTable.Rows.InsertAt(dr3, 2);
                }
                if (bikeid != "" && bikeid.Contains("3000"))
                {
                    DataRow dr5 = dTable.NewRow();
                    dr5[0] = "0";
                    dr5[1] = "Passion";
                    dTable.Rows.InsertAt(dr5, 4);
                }
                if (bikeid != "" && bikeid.Contains("3001"))
                {
                    DataRow dr4 = dTable.NewRow();
                    dr4[0] = "0";
                    dr4[1] = "Splendor";
                    dTable.Rows.InsertAt(dr4, 3);
                }

                if (bikeid != "" && bikeid.Contains("3002"))
                {
                    DataRow dr6 = dTable.NewRow();
                    dr6[0] = "0";
                    dr6[1] = "Super Splendor";
                    dTable.Rows.InsertAt(dr6, 5);
                }

                if (bikeid == "")
                {
                    DataRow dr1 = dTable.NewRow();
                    dr1[0] = "2020";
                    dr1[1] = "Other";
                    dTable.Rows.InsertAt(dr1, 0);

                    DataRow dr2 = dTable.NewRow();
                    dr2[0] = "600";
                    dr2[1] = "Maestro Edge 125";
                    dTable.Rows.InsertAt(dr2, 1);

                    DataRow dr3 = dTable.NewRow();
                    dr3[0] = "2000";
                    dr3[1] = "Other Lead";
                    dTable.Rows.InsertAt(dr3, 2);

                    DataRow dr4 = dTable.NewRow();
                    dr4[0] = "0";
                    dr4[1] = "Splendor";
                    dTable.Rows.InsertAt(dr4, 3);

                    DataRow dr5 = dTable.NewRow();
                    dr5[0] = "0";
                    dr5[1] = "Passion";
                    dTable.Rows.InsertAt(dr5, 4);

                    DataRow dr6 = dTable.NewRow();
                    dr6[0] = "0";
                    dr6[1] = "Super Splendor";
                    dTable.Rows.InsertAt(dr6, 5);
                }

                dTable.DefaultView.Sort = "prod_name ASC";

                CheckBoxList1.DataSource = dTable;
                CheckBoxList1.DataBind();

                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    CheckBoxList1.Items[i].Selected = true;
                }
                chkAll.Checked = true;

                btnSubmit_Click(null, null);
            }
        }
        catch (Exception Ex)
        {

            errorloging.ErrorLogInsert("Daily Report", "Page_Load", Ex.Message, Ex.StackTrace);
        }
    }

    public DataTable GetUtmTerm()
    {
        strQuery = "select distinct(utm_term) from bike_lead_generation where utm_term!='' order by utm_term";
        strQuerySql = "select distinct(utm_term) from bike_lead_generation where utm_term!='' order by utm_term";
        strQueryYoutube = "select distinct(utm_term) from youtude_lead_generation where utm_term!='' order by utm_term";
        //strQueryFestive = "select distinct(utm_term) from bike_lead_generation where utm_term!='' order by utm_term";

        adpMySql = new MySqlDataAdapter(strQuery, conMySql);
        adpSql = new SqlDataAdapter(strQuerySql, conSql);
        adpYoutube = new MySqlDataAdapter(strQueryYoutube, conMySql);
        //adpFestive = new MySqlDataAdapter(strQueryFestive, conMySqlFestive);

        dTable = new DataTable();
        dtSql = new DataTable();
        dtYoutube = new DataTable();
        //dtFestive = new DataTable();

        adpMySql.Fill(dTable);
        adpSql.Fill(dtSql);
        adpYoutube.Fill(dtYoutube);
        //adpFestive.Fill(dtFestive);

        dtSql.Merge(dTable, true, MissingSchemaAction.Ignore);
        dtSql.Merge(dtYoutube, true, MissingSchemaAction.Ignore);
        //dtSql.Merge(dtFestive, true, MissingSchemaAction.Ignore);

        DataView view = new DataView(dtSql);
        view.Sort = "utm_term";
        DataTable distinctValues = view.ToTable(true, "utm_term");

        return distinctValues;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ExportGridToCsv();
    }

    private string GetCheckBoxListSelections()
    {
        string[] cblItems;
        ArrayList cblSelections = new ArrayList();
        foreach (ListItem item in CheckBoxList1.Items)
        {
            //item.Selected = true;

            if (item.Selected)
            {
                cblSelections.Add(item.Value);

                //if you want values instead of text then cblSelections.Add(item.Value);
            }
        }
        cblItems = (string[])cblSelections.ToArray(typeof(string));
        return string.Join(", ", cblItems);
    }

    private string[] GetCheckBoxListSelectionsArray()
    {
        string[] cblItems;
        ArrayList cblSelections = new ArrayList();
        foreach (ListItem item in CheckBoxList1.Items)
        {
            if (item.Selected)
            {
                cblSelections.Add(item.Text);

                //if you want values instead of text then cblSelections.Add(item.Value);
            }
        }
        cblItems = (string[])cblSelections.ToArray(typeof(string));
        return cblItems;
    }

    public void ExportGridToCsv()
    {
        try
        {
            GridView2.Visible = true;
            DateTime result;

            if (txtFrom.Text.Trim() != "")
            {
                if (!DateTime.TryParseExact(txtFrom.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
                {
                    BasicFunction.Show1("Invalid start date format");
                    return;
                }
            }
            if (txtTo.Text.Trim() != "")
            {
                if (!DateTime.TryParseExact(txtTo.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
                {
                    BasicFunction.Show1("Invalid end date format");
                    return;
                }
            }

            if (txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "" && txtFromTime.Text.Trim() == "" && txtToTime.Text.Trim() == "")
            {
                DateTime start_date = Convert.ToDateTime(txtFrom.Text.Trim());
                DateTime end_date = Convert.ToDateTime(txtTo.Text.Trim());

                if (string.IsNullOrEmpty(GetCheckBoxListSelections()))
                {
                    BasicFunction.Show1("Please select at least one check box");
                    return;
                }
                else if ((end_date - start_date).TotalDays + 1 < 0 || (end_date - start_date).TotalDays + 1 > 31)
                {
                    BasicFunction.Show1("The difference between start date and end date cannot be more than 31 or less than 0 days");
                    return;
                }
                else
                {
                    if (ddlUtmTerm.SelectedItem.Text == "All UTM Terms")
                    {
                        if (string.IsNullOrEmpty(bikeid))
                        {
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            //strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                        }
                        else
                        {
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where youtude_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                           // strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(bikeid))
                        {
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                           // strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                        }
                        else
                        {
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where youtude_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                          //  strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                        }
                    }


                    dTable = new DataTable();
                    dTable = GetDetailsByDatatable(strQuery);

                    dtYoutube = new DataTable();
                    dtYoutube = GetDetailsByDatatable(strQueryYoutube);

                    //adpFestive = new MySqlDataAdapter(strQueryFestive, conMySqlFestive);
                    //dtFestive = new DataTable();
                    //adpFestive.Fill(dtFestive);
                   

                    adpSql = new SqlDataAdapter(strQuerySql, conSql);
                    dtSql = new DataTable();
                    adpSql.Fill(dtSql);

                    dtSql.Merge(dTable, true, MissingSchemaAction.Ignore);
                    dtSql.Merge(dtYoutube, true, MissingSchemaAction.Ignore);
                    //dtSql.Merge(dtFestive, true, MissingSchemaAction.Ignore);

                    bool status = false;

                    string[] strArray = GetCheckBoxListSelectionsArray();
                    string bike1 = "";
                    string bike2 = "";
                    string bike3 = "";
                    string bikenames = "0";

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].ToString() == "Splendor")
                        {
                            status = true;
                            bike1 = "Splendor";
                        }
                        if (strArray[i].ToString() == "Passion")
                        {
                            status = true;
                            bike2 = "Passion";
                        }
                        if (strArray[i].ToString() == "Super Splendor")
                        {
                            status = true;
                            bike3 = "Super Splendor";
                        }
                    }
                    if (!string.IsNullOrEmpty(bike1))
                    {
                        bikenames = bike1;
                    }
                    if (!string.IsNullOrEmpty(bike2))
                    {
                        bikenames = bikenames + "," + bike2;
                    }
                    if (!string.IsNullOrEmpty(bike3))
                    {
                        bikenames = bikenames + "," + bike3;
                    }

                    if (status == true)
                    {
                        if (ddlUtmTerm.SelectedItem.Text == "All UTM Terms")
                        {
                            strQuerySqlBikeName = @"select *,'' as state_name,'' as state from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bikename in(" + "'" + bikenames.Replace(",", "','") + "'" + ") order by id desc ";
                        }
                        else
                        {
                            strQuerySqlBikeName = @"select *,'' as state_name,'' as state from bike_lead_generation where (cast(date as date) >= '" + txtFrom.Text.Trim() + "' and cast(date as date) <= '" + txtTo.Text.Trim() + "') and (date is not null and date !='') and bikename in(" + "'" + bikenames.Replace(",", "','") + "'" + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc ";
                        }
                        adpSqlBikeName = new SqlDataAdapter(strQuerySqlBikeName, conSql);
                        dtSqlBikeName = new DataTable();
                        adpSqlBikeName.Fill(dtSqlBikeName);
                        if (dtSqlBikeName.Rows.Count > 0)
                        {
                            dtSql.Merge(dtSqlBikeName, true, MissingSchemaAction.Ignore);
                        }
                    }
                    DataView dv = dtSql.DefaultView;
                    dv.Sort = "date desc";
                    sortedDT = dv.ToTable();

                    if (sortedDT.Rows.Count > 0)
                    {
                        BasicFunction.Show1(sortedDT.Rows.Count.ToString() + " records found");

                        GridView2.DataSource = sortedDT;
                        GridView2.DataBind();
                        export();
                    }
                    else
                    {
                        BasicFunction.Show1("No records found");
                    }

                    GridView2.Visible = false;
                }
            }
            else if (txtFromTime.Text.Trim() == "" && txtToTime.Text.Trim() != "")
            {
                BasicFunction.Show1("Please select start time");
                return;
            }
            else if (txtToTime.Text.Trim() == "" && txtFromTime.Text.Trim() != "")
            {
                BasicFunction.Show1("Please select end time");
                return;
            }
            else if (txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "" && txtFromTime.Text.Trim() != "" && txtToTime.Text.Trim() != "")
            {
                DateTime startTime = DateTime.ParseExact(txtFromTime.Text.Trim(), "HH:mm", CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact(txtToTime.Text.Trim(), "HH:mm", CultureInfo.InvariantCulture);

                DateTime start_date = Convert.ToDateTime(txtFrom.Text.Trim());
                DateTime end_date = Convert.ToDateTime(txtTo.Text.Trim());

                if (string.IsNullOrEmpty(GetCheckBoxListSelections()))
                {
                    BasicFunction.Show1("Please select at least one check box");
                    return;
                }
                else if ((end_date - start_date).TotalDays < 0 || (end_date - start_date).TotalDays > 07)
                {
                    BasicFunction.Show1("The difference between start date and end date cannot be more than 07 or less than 0 days");
                    return;
                }
                else if (txtFrom.Text.Trim() == txtTo.Text.Trim() && (endTime - startTime).TotalMinutes < 0)
                {
                    BasicFunction.Show1("The difference between start time and end time cannot be less than 0 minutes when start date and end date are same");
                    return;
                }
                else
                {
                    if (ddlUtmTerm.SelectedItem.Text == "All UTM Terms")
                    {
                        if (string.IsNullOrEmpty(bikeid))
                        {
                            //strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc" : @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc";
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            //strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                        }
                        else
                        {
                            //strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc" : @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc";
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where youtude_lead_generation.bike_id in(" + bikeid + ") and (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                            //strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") order by id desc";
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(bikeid))
                        {
                            //strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc" : @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc";
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            //strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                        }
                        else
                        {
                            //strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc" : @"select *,'' as state,from_unixtime(insert_time,'%H:%i') as inserttime from bike_lead_generation where (insert_date between '" + txtFrom.Text.Trim() + "' and '" + txtTo.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and from_unixtime(insert_time,'%H:%i') between '" + txtFromTime.Text.Trim() + "' and '" + txtToTime.Text.Trim() + "' order by id desc";
                            strQuery = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQuerySql = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,'' as state_name from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,'' as state_name from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and bike_id!='0' and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            strQueryYoutube = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where youtude_lead_generation.bike_id in(" + bikeid + ") and (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select id,utm_source,utm_medium,utm_campaign,utm_term,utm_content,bike_id,name,mobile,email,city,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(youtude_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,'' as state_name,source,postal_code,interested_in,preferred_dealership,vehcile_purchase_plan as vehicle_purchase_plan,own_vehicle,date,bikename from youtude_lead_generation where (date between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                            //strQueryFestive = string.IsNullOrEmpty(GetCheckBoxListSelections()) ? @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where bike_lead_generation.bike_id in(" + bikeid + ") and (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc" : @"select *,(SELECT CASE WHEN STATE IS NULL THEN '0' ELSE STATE END AS STATE from tbl_heroconnect_state_city where UPPER(bike_lead_generation.city)=tbl_heroconnect_state_city.CITY LIMIT 0,1)  AS state,fld_timestamp as date from bike_lead_generation where (fld_timestamp between '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (insert_date is not null and insert_date !='') and bike_id in(" + GetCheckBoxListSelections() + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                        }
                    }


                    dTable = new DataTable();
                    dTable = GetDetailsByDatatable(strQuery);

                    dtYoutube = new DataTable();
                    dtYoutube = GetDetailsByDatatable(strQueryYoutube);

                    //adpFestive = new MySqlDataAdapter(strQueryFestive, conMySqlFestive);
                    //dtFestive = new DataTable();
                    //adpFestive.Fill(dtFestive);
                   

                    adpSql = new SqlDataAdapter(strQuerySql, conSql);
                    dtSql = new DataTable();
                    adpSql.Fill(dtSql);

                    dtSql.Merge(dTable, true, MissingSchemaAction.Ignore);
                    dtSql.Merge(dtYoutube, true, MissingSchemaAction.Ignore);
                    //dtSql.Merge(dtFestive, true, MissingSchemaAction.Ignore);

                    bool status = false;

                    string[] strArray = GetCheckBoxListSelectionsArray();
                    string bike1 = "";
                    string bike2 = "";
                    string bike3 = "";
                    string bikenames = "0";

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].ToString() == "Splendor")
                        {
                            status = true;
                            bike1 = "Splendor";
                        }
                        if (strArray[i].ToString() == "Passion")
                        {
                            status = true;
                            bike2 = "Passion";
                        }
                        if (strArray[i].ToString() == "Super Splendor")
                        {
                            status = true;
                            bike3 = "Super Splendor";
                        }

                    }
                    if (!string.IsNullOrEmpty(bike1))
                    {
                        bikenames = bike1;
                    }
                    if (!string.IsNullOrEmpty(bike2))
                    {
                        bikenames = bikenames + "," + bike2;
                    }
                    if (!string.IsNullOrEmpty(bike3))
                    {
                        bikenames = bikenames + "," + bike3;
                    }

                    if (status == true)
                    {
                        if (ddlUtmTerm.SelectedItem.Text == "All UTM Terms")
                        {
                            strQuerySqlBikeName = @"select *,'' as state_name,'' as state from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bikename in(" + "'" + bikenames.Replace(",", "','") + "'" + ") order by id desc";
                        }
                        else
                        {
                            strQuerySqlBikeName = @"select *,'' as state_name,'' as state from bike_lead_generation where (cast(date as datetime) >= '" + txtFrom.Text.Trim() + " " + txtFromTime.Text.Trim() + "' and cast(date as datetime) <= '" + txtTo.Text.Trim() + " " + txtToTime.Text.Trim() + "') and (date is not null and date !='') and bikename in(" + "'" + bikenames.Replace(",", "','") + "'" + ") and utm_term = '" + ddlUtmTerm.SelectedItem.Text + "' order by id desc";
                        }
                        adpSqlBikeName = new SqlDataAdapter(strQuerySqlBikeName, conSql);
                        dtSqlBikeName = new DataTable();
                        adpSqlBikeName.Fill(dtSqlBikeName);
                        if (dtSqlBikeName.Rows.Count > 0)
                        {
                            dtSql.Merge(dtSqlBikeName, true, MissingSchemaAction.Ignore);
                        }
                    }

                    DataView dv = dtSql.DefaultView;
                    dv.Sort = "date desc";
                    sortedDT = dv.ToTable();

                    if (sortedDT.Rows.Count > 0)
                    {
                        BasicFunction.Show1(dTable.Rows.Count.ToString() + " records found");

                        GridView2.DataSource = sortedDT;
                        GridView2.DataBind();
                        export();
                    }
                    else
                    {
                        BasicFunction.Show1("No records found");
                    }

                    GridView2.Visible = false;
                }
            }
            else
            {
                BasicFunction.Show1("Please enter start date and end date");
                return;
            }
        }
        catch (Exception Ex)
        {
            //errorloging.ErrorLogInsert("Daily Report", "Submit", Ex.Message, Ex.StackTrace);
            Response.Write(Ex.Message);
        }
    }

    public DataTable GetDetailsByDatatable(string Query)
    {
        adpMySql = new MySqlDataAdapter(Query, conMySql);
        adpMySql.SelectCommand.CommandTimeout = 30000;
        DataTable dTable = new DataTable("DataTable1");
        adpMySql.Fill(dTable);
        return dTable;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    public void export()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Report.csv");
        Response.Charset = "";
        Response.ContentType = "application/text";
        GridView2.AllowPaging = false;
        GridView2.AutoGenerateColumns = true;
        GridView2.DataSource = sortedDT;
        GridView2.DataBind();
        StringBuilder columnbind = new StringBuilder();
        for (int k = 0; k < GridView2.Columns.Count; k++)
        {
            columnbind.Append(GridView2.Columns[k].HeaderText + ',');
        }
        columnbind.Append("\r\n");
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            for (int k = 0; k < GridView2.Columns.Count; k++)
            {
                columnbind.Append(GridView2.Rows[i].Cells[k].Text + ',');
            }
            columnbind.Append("\r\n");
        }

        string path = "D:\\Inetpub\\vhosts\\portal.heromotocorp.com\\httpdocs\\Excel";

        GridView2.GridLines = GridLines.None;

        DataTable dt = new DataTable();
        for (int j = 0; j < GridView2.Columns.Count - 1; j++)
        {
            dt.Columns.Add(GridView2.Columns[j].HeaderText);
        }

        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            for (int j = 0; j < GridView2.Columns.Count - 1; j++)
            {
                dr[GridView2.Columns[j].HeaderText] = GridView2.Rows[i].Cells[j].Text;
            }
            dt.Rows.Add(dr);
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "DownloadedSheet1");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                string sName = "HeroLeads7to9_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();
                string sPath = sName;

                wb.SaveAs(path + "//" + sPath + ".xlsx");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('File Saved Succesfully in " + path + " Drive.');", true);

                PushExcelToSFTP(path + "//" + sPath + ".xlsx");
            }
        }
    }

    private void PushExcelToSFTP(string sFullPath)
    {
        string SftpPth = "//ANGC_TO_CC";

        string SftPHost = "103.247.208.64";//Use Your IP Address

        string SftpUserName = "angc_user";

        string SftpPsd = "Password1";

        int SftpPort = 22; //Port 22 is defaulted for SFTP upload

        //Local file address

        string source = sFullPath;//Upload File Address

        string destination = SftpPth;// If destination address available

        UploadSFTPFile(SftPHost, SftpUserName, SftpPsd, source, destination, SftpPort);
    }

    private void UploadSFTPFile(string host, string username, string password, string sourcefile, string destination, int port)
    {
        using (SftpClient client = new SftpClient(host, port, username, password))
        {
            client.Connect();

            client.ChangeDirectory(destination);

            using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
            {
                client.BufferSize = 4 * 1024;
                client.UploadFile(fs, Path.GetFileName(sourcefile));
            }

            client.Disconnect();
        }
    }

    protected void Check_UnCheckAll(object sender, EventArgs e)
    {
        foreach (ListItem item in CheckBoxList1.Items)
        {
            item.Selected = chkAll.Checked;
        }
    }

    protected void CheckBox_Checked_Unchecked(object sender, EventArgs e)
    {
        bool isAllChecked = true;
        foreach (ListItem item in CheckBoxList1.Items)
        {
            if (!item.Selected)
            {
                isAllChecked = false;
                break;
            }
        }

        chkAll.Checked = isAllChecked;
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string squery = "";
        MySqlDataAdapter adptMySql;
        DataTable dt;
        HiddenField HiddenField2 = new HiddenField();
        HiddenField2 = e.Row.FindControl("HiddenField2") as HiddenField;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("&nbsp;", "");
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace("&nbsp;", "");
            e.Row.Cells[2].Text = e.Row.Cells[2].Text.Replace("&nbsp;", "");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace("&nbsp;", "");
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("&nbsp;", "");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("&nbsp;", "");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace("&nbsp;", "");
            e.Row.Cells[7].Text = e.Row.Cells[7].Text.Replace("&nbsp;", "");
            e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("&nbsp;", "");
            e.Row.Cells[9].Text = e.Row.Cells[9].Text.Replace("&nbsp;", "");
            e.Row.Cells[10].Text = e.Row.Cells[10].Text.Replace("&nbsp;", "");
            e.Row.Cells[11].Text = e.Row.Cells[11].Text.Replace("&nbsp;", "");
            e.Row.Cells[12].Text = e.Row.Cells[12].Text.Replace("&nbsp;", "");
            e.Row.Cells[13].Text = e.Row.Cells[13].Text.Replace("&nbsp;", "");
            e.Row.Cells[14].Text = e.Row.Cells[14].Text.Replace("&nbsp;", "");
            e.Row.Cells[15].Text = e.Row.Cells[15].Text.Replace("&nbsp;", "");
            e.Row.Cells[16].Text = e.Row.Cells[16].Text.Replace("&nbsp;", "");
            e.Row.Cells[17].Text = e.Row.Cells[17].Text.Replace("&nbsp;", "");
            e.Row.Cells[18].Text = e.Row.Cells[18].Text.Replace("&nbsp;", "");

            if (e.Row.Cells[5].Text.Trim() == "2020")
            {
                e.Row.Cells[5].Text = "Other";
            }
            else if (e.Row.Cells[5].Text.Trim() == "600")
            {
                e.Row.Cells[5].Text = "Maestro Edge 125";
            }
            else if (e.Row.Cells[5].Text.Trim() == "2000")
            {
                e.Row.Cells[5].Text = "Other Lead";
            }
            else
            {
                squery = "select prod_id,prod_name from product_master where prod_id=" + e.Row.Cells[5].Text.Trim() + " order by prod_name";
                adptMySql = new MySqlDataAdapter(squery, conMySql);
                dt = new DataTable();
                adptMySql.Fill(dt);
                //e.Row.Cells[5].Text = dt.Rows[0]["prod_name"].ToString();
                if (dt.Rows.Count > 0)
                {
                    e.Row.Cells[5].Text = dt.Rows[0]["prod_name"].ToString();
                }
                else
                {
                    //e.Row.Cells[6].Visible = true;
                    // e.Row.Cells[5].Text = dtSql.Rows[e.Row.RowIndex]["bikename"].ToString();
                    e.Row.Cells[5].Text = HiddenField2.Value;
                    //e.Row.Cells[6].Visible = false;
                }
            }

            if (e.Row.Cells[10].Text.Trim() == "")
            {
                e.Row.Cells[10].Text = "Others";
            }
            else
            {
                e.Row.Cells[10].Text = GetStateName(e.Row.Cells[10].Text.Trim());
            }
        }
    }

    private string GetStateName(string sName)
    {
        if (sName == "AP".Trim())
        {
            sStateName = "Andhra Pradesh";
        }
        else if (sName == "ARP".Trim())
        {
            sStateName = "Arunachal Pradesh";
        }
        else if (sName == "AS".Trim())
        {
            sStateName = "Assam";
        }
        else if (sName == "AN".Trim())
        {
            sStateName = "Andaman and Nicobar";
        }
        else if (sName == "BH".Trim())
        {
            sStateName = "Bihar";
        }
        else if (sName == "CD".Trim())
        {
            sStateName = "Chandigarh";
        }
        else if (sName == "CG".Trim())
        {
            sStateName = "Chhattisgarh";
        }
        else if (sName == "DEL".Trim())
        {
            sStateName = "Delhi";
        }
        else if (sName == "DH".Trim())
        {
            sStateName = "Dadra and Nagar Haveli";
        }
        else if (sName == "GDD".Trim())
        {
            sStateName = "GOA";
        }
        else if (sName == "GJ".Trim())
        {
            sStateName = "Gujarat";
        }
        else if (sName == "HP".Trim())
        {
            sStateName = "Himachal Pradesh";
        }
        else if (sName == "HR".Trim())
        {
            sStateName = "Haryana";
        }
        else if (sName == "JK".Trim())
        {
            sStateName = "Jammu and Kashmir";
        }
        else if (sName == "JR".Trim())
        {
            sStateName = "Jharkhand";
        }
        else if (sName == "KAR".Trim())
        {
            sStateName = "Karnataka";
        }
        else if (sName == "KER".Trim())
        {
            sStateName = "Kerala";
        }
        else if (sName == "MAH".Trim())
        {
            sStateName = "Maharashtra";
        }
        else if (sName == "MGH".Trim())
        {
            sStateName = "Meghalaya";
        }
        else if (sName == "MP".Trim())
        {
            sStateName = "Madhya Pradesh";
        }
        else if (sName == "MPR".Trim())
        {
            sStateName = "Manipur";
        }
        else if (sName == "MZ".Trim())
        {
            sStateName = "Mizoram";
        }
        else if (sName == "NG".Trim())
        {
            sStateName = "Nagaland";
        }
        else if (sName == "OR".Trim())
        {
            sStateName = "Orissa";
        }
        else if (sName == "PB".Trim())
        {
            sStateName = "Punjab";
        }
        else if (sName == "PY".Trim())
        {
            sStateName = "Pondicherry";
        }
        else if (sName == "RJ".Trim())
        {
            sStateName = "Rajasthan";
        }
        else if (sName == "TLG".Trim())
        {
            sStateName = "Telangana";
        }
        else if (sName == "TN".Trim())
        {
            sStateName = "Tamil Nadu";
        }
        else if (sName == "TRI".Trim())
        {
            sStateName = "Tripura";
        }
        else if (sName == "UP".Trim())
        {
            sStateName = "Uttar Pradesh";
        }
        else if (sName == "UR".Trim())
        {
            sStateName = "Uttarakhand";
        }
        else if (sName == "WB".Trim())
        {
            sStateName = "West Bengal";
        }
        else
        {
            sStateName = sName;
        }

        return sStateName;
    }
}