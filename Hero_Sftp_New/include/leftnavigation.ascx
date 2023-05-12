<%@ Control Language="C#" AutoEventWireup="true" CodeFile="leftnavigation.ascx.cs" Inherits="hmc_lead_cms_include_leftnavigation" %>
<div class="span2 main-menu-span">
    <div class="well nav-collapse sidebar-nav">
        <ul class="nav nav-tabs nav-stacked main-menu">
            <li class="nav-header hidden-tablet">Menu</li>
            <li><a class="ajax-link" href="hero_lead_dashboard_report_new.aspx"><i></i><span class="hidden-tablet">Datewise Report</span></a></li>
            <%if (Session["UserName"].ToString() == "pravesh" || Session["UserName"].ToString() == "reeya" || Session["UserName"].ToString() == "robin" || Session["UserName"].ToString() == "hero")
                {%>
            <%--  <li><a class="ajax-link" href="HeroLeadReport_new.aspx"><i></i><span class="hidden-tablet">Monthly Report</span></a></li>--%>
            <%} %>
            <%if (Session["SUPER_ADMIN"] == "SUPER_ADMIN")
                {%>
            <li><a class="ajax-link" href="viewadmins.aspx"><i></i><span class="hidden-tablet">Admin</span></a></li>
            <%} %>
        </ul>

    </div>
    <!--/.well -->
</div>
