using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using System.Web.Hosting;

public partial class profile : System.Web.UI.Page
{
    string strQuery = "";
    SqlConnection conn;
    DataTable dTable, dTableName, dTableCheck, dTableEmail;
    SqlDataAdapter adp;
    string sEmpid = "";
    int iYear, iYearTo = 0;



    protected void Page_Load(object sender, EventArgs e)
    {
        string connstr = ConfigurationManager.ConnectionStrings["bcmgrconnection"].ToString();
        conn = new SqlConnection(connstr);

        if (Request["Empid"] != null)
        {
            sEmpid = BasicFunction.Decrypt(Request["Empid"].ToString());
        }

        if (Session["username"] != null && Session["empid"] != null && Session["empid"].ToString() != "" && Session["deptname"].ToString() != "")
        {
            txtName.InnerText = BasicFunction.TitleCase(Convert.ToString(Session["username"]));
            //lbldeptname.text = basicfunction.titlecase(convert.tostring(session["deptname"]));

            HiddenFieldDeptId.Value = Session["deptid"].ToString();
            HiddenFieldEmpId.Value = Session["empid"].ToString();
        }
        else
        {
            Response.Redirect("default.aspx");
        }

        hrefProfile.HRef = "Profile.aspx?Empid=" + BasicFunction.Encrypt(Session["empid"].ToString());

        strQuery = "select empemail from tblEmployee where empid='" + HiddenFieldEmpId.Value + "'";
        dTableEmail = new DataTable();
        dTableEmail = BasicFunction.GetDetailsByDatatable(strQuery);
        if (dTableEmail.Rows.Count > 0)
        {
            HiddenFieldEmployeeFromEmailId.Value = dTableEmail.Rows[0]["empemail"].ToString();
        }

        if (sEmpid == Session["empid"].ToString())
        {
            btnEdit.Visible = true;
        }
        else
        {
            btnEdit.Visible = false;
        }


        if (!Page.IsPostBack)
        {
            EmployeeBirthday();
            FillYear();
            FillPersoanlInformation();
            FillEmployeeDropdown();
        }
    }

    private void FillPersoanlInformation()
    {
        strQuery = "select ProfileImage from tbl_Revamp_Profile where EmpId='" + Session["empid"].ToString() + "'";
        DataTable dTableName = new DataTable();
        dTableName = BasicFunction.GetDetailsByDatatable(strQuery);
        if (dTableName.Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(dTableName.Rows[0]["ProfileImage"].ToString()))
            {
                itemImgOutput.Src = "~/ProfileImage/user-post.jpg";
            }
            else
            {
                itemImgOutput.Src = "~/ProfileImage/" + dTableName.Rows[0]["ProfileImage"].ToString();
            }
        }
        else
        {
            itemImgOutput.Src = "~/ProfileImage/user-post.jpg";
        }


        strQuery = " select emp.*,deptname,day(emp.joiningdate) as joiningdate1,DATENAME(month, emp.joiningdate) as joiningdate2, year(emp.joiningdate) as joiningdate3 from tblEmployee emp,tblDept dept where  emp.deptid=dept.deptid and  emp.EmpId='" + sEmpid + "'";
        dTableName = new DataTable();
        dTableName = BasicFunction.GetDetailsByDatatable(strQuery);
        if (dTableName.Rows.Count > 0)
        {


            SpanFirstName.InnerText = dTableName.Rows[0]["empname"].ToString();
            SpanDepartment.InnerText = BasicFunction.TitleCase(dTableName.Rows[0]["deptname"].ToString());
            spanDateOfJoining.InnerText = dTableName.Rows[0]["joiningdate1"].ToString() + " " + dTableName.Rows[0]["joiningdate2"].ToString() + " " + dTableName.Rows[0]["joiningdate3"].ToString();
        }

        strQuery = "select prof.*,dep.deptname,day(emp.joiningdate) as joiningdate1,DATENAME(month, emp.joiningdate) as joiningdate2, year(emp.joiningdate) as joiningdate3 from tbl_Revamp_Profile prof,tblEmployee emp,tblDept dep where emp.empid = prof.empid and emp.deptid = dep.deptid and prof.EmpId='" + sEmpid + "'";
        dTable = new DataTable();
        dTable = BasicFunction.GetDetailsByDatatable(strQuery);

        if (dTable.Rows.Count > 0)
        {
            //SpanSurName.InnerText = BasicFunction.Decrypt(dTable.Rows[0]["LastName"].ToString());

            if (string.IsNullOrEmpty(dTable.Rows[0]["ProfileImage"].ToString()))
            {
                imgProfileImage.Src = "~/ProfileImage/user-post.jpg";
            }
            else
            {
                imgProfileImage.Src = "~/ProfileImage/" + dTable.Rows[0]["ProfileImage"].ToString();
            }

            SpanDateOfBirth.InnerText = dTable.Rows[0]["DateOfBirth"].ToString();
            SpanMobilePhones.InnerText = dTable.Rows[0]["MobilePhone"].ToString();
            SpanGender.InnerText = dTable.Rows[0]["Gender"].ToString();
            SpanExtensionNo.InnerText = dTable.Rows[0]["ExtensionNo"].ToString();
            hrefEmailId.InnerText = dTable.Rows[0]["EmailAddress"].ToString();
            hrefEmailId.HRef = "mailto:" + dTable.Rows[0]["EmailAddress"].ToString();
            hrefFacebook.InnerText = dTable.Rows[0]["Facebook"].ToString();
            hrefFacebook.HRef = dTable.Rows[0]["Facebook"].ToString();
            hrefLinkedIn.InnerText = dTable.Rows[0]["LinkedIn"].ToString();
            hrefLinkedIn.HRef = dTable.Rows[0]["LinkedIn"].ToString();
            hrefInstagram.InnerText = dTable.Rows[0]["Instagram"].ToString();
            hrefInstagram.HRef = dTable.Rows[0]["Instagram"].ToString();
            SpanDesignation.InnerText = dTable.Rows[0]["Designation"].ToString();

            divSummary.InnerText = dTable.Rows[0]["Summary"].ToString();
        }
        else
        {
            imgProfileImage.Src = "~/ProfileImage/user-post.jpg";
        }
    }

    protected void lnkbtnSendMessage_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtnSendMessage;
        lnkbtnSendMessage = (sender) as LinkButton;
        RepeaterItem item = (RepeaterItem)lnkbtnSendMessage.NamingContainer;

        HiddenField HiddenFieldEmployeeTo = item.FindControl("HiddenFieldEmployeeTo") as HiddenField;
        HiddenField HiddenFieldEmployeeFrom = item.FindControl("HiddenFieldEmployeeFrom") as HiddenField;
        HiddenField HiddenFieldEmployeeToEmailId = item.FindControl("HiddenFieldEmployeeToEmailId") as HiddenField;

        HtmlTextArea txtBirthdayComment = item.FindControl("txtBirthdayComment") as HtmlTextArea;

        //Response.Write(HiddenFieldEmployeeTo.Value + " " + HiddenFieldEmployeeFrom.Value + " " + txtBirthdayComment.Value);

        if (string.IsNullOrEmpty(txtBirthdayComment.Value))
        {
            BasicFunction.Show1("Please enter Wish.");
        }
        else
        {

            conn.Open();
            SqlCommand cmd_Update = new SqlCommand("sp_Revamp_BirthdayWish_Insert", conn);
            cmd_Update.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd_Update.Parameters.AddWithValue("@WishFrom", HiddenFieldEmployeeFrom.Value);
                cmd_Update.Parameters.AddWithValue("@WishTo", HiddenFieldEmployeeTo.Value);
                cmd_Update.Parameters.AddWithValue("@Message", txtBirthdayComment.Value);
                cmd_Update.Parameters.AddWithValue("@BirthDate", DateTime.Now.ToShortDateString());

                cmd_Update.ExecuteNonQuery();

                strQuery = "select empemail from tblEmployee where username='" + Session["username"].ToString() + "'";
                DataTable dtEmail = new DataTable();

                dtEmail = BasicFunction.GetDetailsByDatatable(strQuery);

                string strEmail = "";

                if (dtEmail.Rows.Count > 0)
                {
                    strEmail = dtEmail.Rows[0]["empemail"].ToString();
                }

                SendMail(HiddenFieldEmployeeToEmailId.Value, "Wishes From " + Session["username"].ToString(), txtBirthdayComment.Value, "", strEmail);

                txtBirthdayComment.Value = "";

                BasicFunction.Show1("Wish submitted successfully.");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cmd_Update.Dispose();
                conn.Close();
                conn.Dispose();

                ddlYear.SelectedIndex = 0;

            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //BasicFunction.Show1("Triggered");
        if (Session["empid"] != null)
        {
            Session["empid"] = null;
            Session["empid"] = "";

        }
        if (Session["username"] != null)
        {
            Session["username"] = null;
            Session["username"] = "";
        }
        if (Session["adminuser"] != null)
        {
            Session["adminuser"] = null;
            Session["adminuser"] = "";
        }
        if (Session["adminUserid"] != null)
        {
            Session["adminUserid"] = null;
            Session["adminUserid"] = "";
        }
        if (Session["deptid"] != null)
        {
            Session["deptid"] = null;
            Session["deptid"] = "";
        }
        if (Session["deptname"] != null)
        {
            Session["deptname"] = null;
            Session["deptname"] = "";
        }
        if (Session["propic"] != null)
        {
            Session["propic"] = null;
            Session["propic"] = "";
        }
        if (Session["empemail"] != null)
        {
            Session["empemail"] = null;
            Session["empemail"] = "";
        }
        if (Session["teamuser"] != null)
        {
            Session["teamuser"] = null;
            Session["teamuser"] = "";
        }
        if (Session["teamUserid"] != null)
        {
            Session["teamUserid"] = null;
            Session["teamUserid"] = "";
        }
        if (Session["lmsuser"] != null)
        {
            Session["lmsuser"] = null;
            Session["lmsuser"] = "";
        }
        if (Session["lmsUserid"] != null)
        {
            Session["lmsUserid"] = null;
            Session["lmsUserid"] = "";
        }

        if (Session["lmsuser"] == "lmsuser")
        {
            Session["lmsuser"] = null;
            Session["lmsuser"] = "";
        }


        if (Session["teamuser"] == "teamuser")
        {
            Session["teamuser"] = null;
            Session["teamuser"] = "";
        }


        if (Session["lmsteamuser"] != null)
        {
            Session["lmsteamuser"] = null;
            Session["lmsteamuser"] = "";
        }
        if (Session["lmsteamuserid"] != null)
        {
            Session["lmsteamuserid"] = null;
            Session["lmsteamuserid"] = "";
        }


        if (Session["deptid"] != null)
        {
            Session["deptid"] = null;
            Session["deptid"] = "";
        }

        if (Session["Probation"] != null)
        {
            Session["Probation"] = null;
            Session["Probation"] = "";
        }

        Response.Redirect("http://bigituna.com/bctasksheet/default.aspx");
    }

    protected void drplistEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drplistEmployee.SelectedIndex != 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "SendMail", "document.location = 'mailto:test@test.com?Subject=Your subject';", true);
        //}

        Response.Redirect("Profile.aspx?Empid=" + BasicFunction.Encrypt(drplistEmployee.SelectedValue));
    }

    private void FillYear()
    {
        //ddlYear.Items.Add(new ListItem("Select Year", "Select Year"));
        iYear = DateTime.Now.Year;
        iYearTo = DateTime.Now.Year + 1;

        //for (int i = iYear; i <= (iYear + 5); i++)
        //{
        ddlYear.Items.Add(new ListItem(iYear.ToString(), iYear.ToString()));
        //}
    }

    private void FillEmployeeDropdown()
    {
        strQuery = @"SELECT distinct emp.empname,emp.empid FROM tblLogin logg,tblEmployee emp
                    where emp.empid=logg.empid and (emp.empemail is not null and emp.empemail!='') and (emp.empstatus='P' or emp.empstatus='E')
                    order by emp.empname";

        dTable = new DataTable();
        adp = new SqlDataAdapter(strQuery, conn);
        dTable = new DataTable();

        adp.Fill(dTable);

        drplistEmployee.DataTextField = "empname";
        drplistEmployee.DataValueField = "empid";

        drplistEmployee.Items.Add(new ListItem("Select Contact", "Select Contact"));

        drplistEmployee.DataSource = dTable;
        drplistEmployee.DataBind();
    }


    private void EmployeeBirthday()
    {
        strQuery = @"SELECT empid,empemail,empname,birthdate,joiningdate,'" + Session["empid"].ToString() + "' as empfrom FROM tblEmployee  where empstatus is not null and (birthdate is not null or birthdate!='') and ((DAY(getdate())=DAY(birthdate) and MONTH(getdate())= MONTH(birthdate)) or (joiningdate is not null or joiningdate!='') and (DAY(getdate())=DAY(joiningdate) and MONTH(getdate())= MONTH(joiningdate)))";

        dTable = new DataTable();
        adp = new SqlDataAdapter(strQuery, conn);
        dTable = new DataTable();

        adp.Fill(dTable);

        if (dTable.Rows.Count > 0)
        {
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dTable.Rows[i]["birthdate"].ToString()).Day == DateTime.Now.Day && Convert.ToDateTime(dTable.Rows[i]["birthdate"].ToString()).Month == DateTime.Now.Month)
                {
                    dTable.Rows[i]["empname"] = dTable.Rows[i]["empname"] + "'s Birthday";
                }
                else if (Convert.ToDateTime(dTable.Rows[i]["joiningdate"].ToString()).Day == DateTime.Now.Day && Convert.ToDateTime(dTable.Rows[i]["joiningdate"].ToString()).Month == DateTime.Now.Month)
                {
                    dTable.Rows[i]["empname"] = dTable.Rows[i]["empname"] + "'s Work Anniversary";
                }
            }
        }

        rptrBirthday.DataSource = dTable;
        rptrBirthday.DataBind();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        pnlProfileOne.Visible = false;
        pnlProfileTwo.Visible = true;

        FillEmployeePersonalInformation();
    }

    private void FillEmployeePersonalInformation()
    {
        strQuery = " select emp.*,deptname,day(emp.joiningdate) as joiningdate1,DATENAME(month, emp.joiningdate) as joiningdate2, year(emp.joiningdate) as joiningdate3 from tblEmployee emp,tblDept dept where  emp.deptid=dept.deptid and  emp.EmpId='" + sEmpid + "'";
        dTableName = new DataTable();
        dTableName = BasicFunction.GetDetailsByDatatable(strQuery);
        if (dTableName.Rows.Count > 0)
        {
            spanFirstNameEdit.InnerHtml = dTableName.Rows[0]["empname"].ToString();
            spanDepartmentEdit.InnerHtml = BasicFunction.TitleCase(dTableName.Rows[0]["deptname"].ToString());
            spanDateOfJoiningEdit.InnerHtml = dTableName.Rows[0]["joiningdate1"].ToString() + " " + dTableName.Rows[0]["joiningdate2"].ToString() + " " + dTableName.Rows[0]["joiningdate3"].ToString();
        }

        strQuery = "select prof.*,dep.deptname,day(emp.joiningdate) as joiningdate1,DATENAME(month, emp.joiningdate) as joiningdate2, year(emp.joiningdate) as joiningdate3 from tbl_Revamp_Profile prof,tblEmployee emp,tblDept dep where emp.empid = prof.empid and emp.deptid = dep.deptid and prof.EmpId='" + sEmpid + "'";
        dTable = new DataTable();
        dTable = BasicFunction.GetDetailsByDatatable(strQuery);

        if (dTable.Rows.Count > 0)
        {
            //spanSurnameEdit.InnerHtml = BasicFunction.Decrypt(dTable.Rows[0]["LastName"].ToString());
            txtDateOfBirth.Text = dTable.Rows[0]["DateOfBirth"].ToString();
            txtMobile.Text = dTable.Rows[0]["MobilePhone"].ToString();

            if (string.IsNullOrEmpty(dTable.Rows[0]["Gender"].ToString()))
            {
                drplistGender.SelectedIndex = 0;
            }
            else
            {
                drplistGender.SelectedValue = dTable.Rows[0]["Gender"].ToString();
            }

            txtExtensionNo.Text = dTable.Rows[0]["ExtensionNo"].ToString();
            txtEmail.Text = dTable.Rows[0]["EmailAddress"].ToString();
            txtFacebook.Text = dTable.Rows[0]["Facebook"].ToString();
            txtLinkedIn.Text = dTable.Rows[0]["LinkedIn"].ToString();
            txtInstagram.Text = dTable.Rows[0]["Instagram"].ToString();
            txtDesignation.Text = dTable.Rows[0]["Designation"].ToString();
            txtSummary.Value = dTable.Rows[0]["Summary"].ToString();
        }
    }

    protected void lnkbtnCancel_Click(object sender, EventArgs e)
    {
        pnlProfileOne.Visible = true;
        pnlProfileTwo.Visible = false;
    }

    protected void lnkbtnUpdate_Click(object sender, EventArgs e)
    {
        if (IsValidate())
        {
            SqlConnection conn;
            SqlCommand cmd_Update;

            string connstr = ConfigurationManager.ConnectionStrings["bcmgrconnection"].ToString();
            conn = new SqlConnection(connstr);

            bool status;

            try
            {
                //if (!string.IsNullOrEmpty(imagename))
                //{
                //    string fn = System.IO.Path.GetFileName(imagename);
                //    string savelocation = System.Web.HttpContext.Current.Server.MapPath("~/uploads/postimage/") + "\\" + fn;
                //    //fuimage.postedfile.saveas(savelocation);
                //}

                strQuery = "select * from tbl_Revamp_Profile where EmpId='" + sEmpid + "'";

                dTableCheck = new DataTable();
                dTableCheck = BasicFunction.GetDetailsByDatatable(strQuery);
                if (dTableCheck.Rows.Count == 0)
                {
                    conn.Open();

                    cmd_Update = new SqlCommand("sp_revamp_insertprofile", conn);
                    cmd_Update.CommandType = CommandType.StoredProcedure;
                    cmd_Update.Parameters.AddWithValue("@DateOfBirth", txtDateOfBirth.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@MobilePhone", txtMobile.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Gender", drplistGender.SelectedValue);
                    cmd_Update.Parameters.AddWithValue("@ExtensionNo", txtExtensionNo.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@EmailAddress", txtEmail.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Facebook", txtFacebook.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@LinkedIn", txtLinkedIn.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Instagram", txtInstagram.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Summary", txtSummary.Value.Trim());
                    cmd_Update.Parameters.AddWithValue("@EmpId", sEmpid);
                    cmd_Update.ExecuteNonQuery();

                    pnlProfileOne.Visible = true;
                    pnlProfileTwo.Visible = false;

                    FillPersoanlInformation();
                }
                else
                {
                    conn.Open();

                    cmd_Update = new SqlCommand("sp_revamp_updateprofile", conn);
                    cmd_Update.CommandType = CommandType.StoredProcedure;
                    cmd_Update.Parameters.AddWithValue("@DateOfBirth", txtDateOfBirth.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@MobilePhone", txtMobile.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Gender", drplistGender.SelectedValue);
                    cmd_Update.Parameters.AddWithValue("@ExtensionNo", txtExtensionNo.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@EmailAddress", txtEmail.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Facebook", txtFacebook.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@LinkedIn", txtLinkedIn.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Instagram", txtInstagram.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());
                    cmd_Update.Parameters.AddWithValue("@Summary", txtSummary.Value.Trim());
                    cmd_Update.Parameters.AddWithValue("@EmpId", sEmpid);
                    cmd_Update.ExecuteNonQuery();

                    pnlProfileOne.Visible = true;
                    pnlProfileTwo.Visible = false;

                    FillPersoanlInformation();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                status = false;

                Response.Write(ex.Message);
            }
        }
    }

    private bool IsValidate()
    {
        DivErrorMessage.InnerHtml = "";

        if (drplistGender.SelectedIndex == 0)
        {
            DivErrorMessage.InnerHtml = "Please select Gender from dropdown";
            drplistGender.Focus();
            return false;
        }

        if (!string.IsNullOrEmpty(txtMobile.Text.Trim()))
        {
            if (!IsSQLCharPresent(txtMobile.Text.Trim()))
            {
                DivErrorMessage.InnerHtml = "Please enter valid mobile no";
                return false;
            }
            if (!BasicFunction.ValidateMobile(txtMobile.Text.Trim()))
            {
                DivErrorMessage.InnerHtml = "Please enter valid mobile no";
                return false;
            }
        }

        if (!IsSQLCharPresent(txtExtensionNo.Text.Trim()))
        {
            DivErrorMessage.InnerHtml = "Please enter valid extension no";
            return false;
        }

        if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            if (!IsSQLCharPresent(txtEmail.Text.Trim()))
            {
                DivErrorMessage.InnerHtml = "Please enter valid email id";
                return false;
            }
            if (!BasicFunction.ValidateEmail(txtEmail.Text.Trim()))
            {
                DivErrorMessage.InnerHtml = "Please enter valid email id";
                return false;
            }
        }

        if (!IsSQLCharPresent(txtDesignation.Text.Trim()))
        {
            DivErrorMessage.InnerHtml = "Please enter valid designation";
            return false;
        }

        if (!IsSQLCharPresent(txtSummary.InnerText.Trim()))
        {
            DivErrorMessage.InnerHtml = "Please enter valid summary";
            return false;
        }

        return true;
    }


    public static bool SendMail(string To, string subject, string body, string Bcc, string Cc)
    {
        System.Web.Mail.MailMessage smg = new System.Web.Mail.MailMessage();

        //string smtpServer = "206.183.111.148";
        string smtpServer = "192.168.5.25";
        string userName = "bcwadmin@bcw.co.in";
        string password = "adminbcw#@!";

        int cdoBasic = 1;
        int cdoSendUsingPort = 2;
        if (userName.Length > 0)
        {
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtpServer);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", cdoSendUsingPort);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
            smg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);
        }

        smg.Body = body;
        smg.Subject = subject;
        smg.BodyFormat = MailFormat.Html;
        smg.From = "bcwadmin@bcw.co.in";
        smg.To = To;
        smg.Cc = Cc;
        smg.Bcc = Bcc;

        try
        {
            SmtpMail.Send(smg);
            return true;
        }
        catch (Exception ex)
        {
            BasicFunction.Show1(ex.Message);
            return false;
        }
    }

    [WebMethod]
    public static string SaveProfileImage(string ProfileImage)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["bcmgrConnection"].ToString());

        string x = ProfileImage.Trim().Replace("data:image/jpeg;base64,", "");
        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(x);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);


        string imageName = DateTime.Now.ToString("ddMMyyyyhhmmss") + ".jpeg";

        // Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length);
        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        image.Save(HostingEnvironment.MapPath("~/ProfileImage/" + imageName), System.Drawing.Imaging.ImageFormat.Jpeg);

        string strQuery = "select * from tbl_Revamp_Profile where EmpId='" + HttpContext.Current.Session["empid"].ToString() + "'";

        DataTable dTableCheck = new DataTable();
        dTableCheck = BasicFunction.GetDetailsByDatatable(strQuery);
        if (dTableCheck.Rows.Count == 0)
        {
            con.Open();

            SqlCommand cmd_Update = new SqlCommand("sp_Revamp_ProfileImage_Insert", con);
            cmd_Update.CommandType = CommandType.StoredProcedure;

            cmd_Update.Parameters.AddWithValue("@ProfileImage", imageName);
            cmd_Update.Parameters.AddWithValue("@EmpId", HttpContext.Current.Session["empid"].ToString());
            cmd_Update.ExecuteNonQuery();
        }
        else
        {
            con.Open();
            SqlCommand cmd_Update = new SqlCommand("sp_Revamp_ProfileImage_Update", con);
            cmd_Update.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd_Update.Parameters.AddWithValue("@ProfileImage", imageName);
                cmd_Update.Parameters.AddWithValue("@EmpId", HttpContext.Current.Session["empid"].ToString());

                cmd_Update.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                cmd_Update.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        return "True";

    }

    public static bool IsSQLCharPresent(string txt)
    {
        if (!Regex.IsMatch(txt, @"[^A-Za-z0-9- ?().',@]"))
        {
            //Show("Special characters not allowed.only (-_ .@&) are allowed.");
            return true;
        }
        return false;
    }
}