<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contact-us.aspx.cs" Inherits="contact_us" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Big-I Tuna Communications Pvt. Ltd.  </title>
    <link href="css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function RemoveSpecialChar(txtVal) {
            if (txtVal.value != '' && txtVal.value.match(/^[\w ]+$/) == null) {
                txtVal.value = txtVal.value.replace(/[\W]/g, '');
            }
        }

    </script>

</head>

<body id="contact-us">
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-57387079-1', 'auto');
        ga('send', 'pageview');

    </script>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div id="logo">
                <a href="index.aspx">
                    <img src="images/bigituna_logo.png" alt="Big-I Tuna" /></a>
            </div>
            <div id="navcontent">
                <div id="nav">
                    <ul>
                        <li><a href="about-us.aspx">who are we?</a></li>
                        <!--<li><a href="our-work.aspx">our work</a></li>-->
                        <li><a href="our-services.aspx">what we do?</a></li>
                        <li><a href="contact-us.aspx">contact us</a></li>
                    </ul>
                </div>
                <div id="content">
                    <h1>Contact Us </h1>
                    <table width="615" border="0" cellpadding="0" cellspacing="10">
                        <tr>
                            <td width="133" align="right">Name:</td>
                            <td width="452">
                                <asp:TextBox ID="txtname" runat="server" onkeyup="javascript:RemoveSpecialChar(this)" onBlur="javascript:RemoveSpecialChar(this)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="White"
                                    ControlToValidate="txtname" ValidationGroup="v1">                                                                                        
                                                 Please Enter Your Name                            </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Email:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="White"
                                    ControlToValidate="txtEmail" ValidationGroup="v1">                                                                                        
                                                  #                            </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="validateEmail"
                                    runat="server" ErrorMessage="Invalid Email." ForeColor="White"
                                    ControlToValidate="txtEmail" ValidationGroup="v1"
                                    ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Contact  No.:</td>
                            <td>
                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>

                                <asp:RegularExpressionValidator ValidationExpression="^[0-9]*$" ControlToValidate="txtPhone" ID="RegExpVal_txtPhone" runat="server" ForeColor="White" ValidationGroup="v1" ErrorMessage="Invalid Format" Display="Dynamic" /></td>
                        </tr>
                        <tr>
                            <td align="right">Comment: </td>
                            <td>
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="White"
                                    ControlToValidate="txtComment" ValidationGroup="v1">                                                                                        
                                                   Please Fill Comments                                </asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">Budget (if any):</td>
                            <td>
                                <asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;</td>
                            <td>
                                <asp:ImageButton ID="imgsubmit" runat="server" ImageUrl="images/get-in-touch.gif" alt="Get in Touch" Width="190" Height="56" ValidationGroup="v1" OnClick="imgsubmit_Click" /></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="border: 0; color: #FDCD00; background-color: #FDCD00; height: 1px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>For further queries, get in touch with us on email:  
                            <a href="mailto:big@bigituna.com">big@bigituna.com</a></td>
                        </tr>
                    </table>
                    <p>&nbsp;</p>
                    <div id="footer">
                        Big-I Tuna Communications Pvt. Ltd. //
                    <!-- Terms &amp; Conditions // -->
                        &copy; 2019
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </form>
</body>
</html>


