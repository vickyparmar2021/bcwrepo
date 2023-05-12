<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="include_header" %>

<div class="navbar">
    <div class="navbar-inner">
        <div class="container-fluid">
            <a class="btn btn-navbar" data-toggle="collapse" data-target=".top-nav.nav-collapse,.sidebar-nav.nav-collapse">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </a>
             <%--<span class="brand"><img src="assets/mtb-logo.png" alt="Mahindra truck and Bus" title="Mahindra Truck and Bus"></span>--%>

            <!-- user dropdown starts -->
            <div class="btn-group pull-right">
                <asp:Button ID="Button1" runat="server" Text="Logout" OnClick="Button1_Click" CausesValidation="false" />
                <%--<a class="btn dropdown-toggle" data-toggle="dropdown" href="#">
						<i class="icon-user"></i><span class="hidden-phone"> admin</span>
						<span class="caret"></span>
					</a>
					<ul class="dropdown-menu">
						
						<li><a href="Default.aspx">Logout</a></li>
					</ul>--%>
            </div>
            <!-- user dropdown ends -->


        </div>
    </div>
</div>
