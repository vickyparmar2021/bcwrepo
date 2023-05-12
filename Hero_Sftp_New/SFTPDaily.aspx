<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SFTPDaily.aspx.cs" Inherits="SFTPDaily" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <title></title>
    <link href="css/bootstrap-cerulean.css" rel="stylesheet" />
    <style type="text/css">
        body {
            padding-bottom: 40px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }
    </style>
    <link href="css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/charisma-app.css" rel="stylesheet" />
    <link href='css/uniform.default.css' rel='stylesheet' />
    <link href='css/uploadify.css' rel='stylesheet' />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.timepicker').timepicker({
                timeFormat: 'HH:mm',
                interval: 15,
                minTime: '00:00',
                maxTime: '23:59',
                startTime: '00:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true
            });
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {

                return false;
            }
            return true;
        }
    </script>

    <style type="text/css">
        label {
            display: inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- topbar starts -->

        <!-- topbar ends -->
        <div class="container-fluid">
            <div class="row-fluid">
                <!-- left menu starts -->

                <!-- left menu ends -->
                <div id="content" class="span10">
                    <div class="row-fluid sortable">
                        <div class="box span12">
                            <div class="box-content">
                                <fieldset>
                                    <legend>VIEW LEADS</legend>
                                    <div>
                                        Start Date :<asp:TextBox ID="txtFrom" runat="server" autocomplete="off" Width="150"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        End Date :
                                        <asp:TextBox ID="txtTo" runat="server" autocomplete="off" Width="150"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        Start Time :<asp:TextBox ID="txtFromTime" runat="server" class="timepicker" onkeypress="return isNumberKey(event)" autocomplete="off" Width="150"></asp:TextBox>
                                        End Time :
                                        <asp:TextBox ID="txtToTime" runat="server" class="timepicker" onkeypress="return isNumberKey(event)" autocomplete="off" Width="150"></asp:TextBox>
                                        <br />
                                        UTM Term :
                                        <asp:DropDownList ID="ddlUtmTerm" runat="server"></asp:DropDownList>
                                        <br />
                                        Bikes:
                                        <br />
                                        <br />
                                        &nbsp;
                                        <asp:CheckBox ID="chkAll" Text="Select All" runat="server" AutoPostBack="true" TextAlign="Right" />
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataTextField="prod_name" DataValueField="prod_id" RepeatDirection="Horizontal" RepeatColumns="6" TextAlign="Right" AutoPostBack="true"></asp:CheckBoxList>
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                                        <br />
                                        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" Visible="true"></asp:Label>
                                    </div>
                                    <br />
                                    <div class="EU_TableScroll" id="showData" style="display: block">
                                        <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" Visible="False" OnRowDataBound="GridView2_RowDataBound" >
                                            <Columns>
                                                <asp:BoundField DataField="utm_source" HeaderText="UTM Source" />
                                                <asp:BoundField DataField="utm_medium" HeaderText="UTM Medium" />
                                                <asp:BoundField DataField="utm_campaign" HeaderText="UTM Campaign" />
                                                <asp:BoundField DataField="utm_term" HeaderText="UTM Term" />
                                                <asp:BoundField DataField="utm_content" HeaderText="UTM Content" />
                                                <asp:BoundField DataField="bike_id" HeaderText="Bike Name" />
                                                <%--<asp:BoundField DataField="bike_id" HeaderText="Bike Name" />--%>
                                                <asp:BoundField DataField="name" HeaderText="Customer Name" />
                                                <asp:BoundField DataField="mobile" HeaderText="Mobile" />
                                                <asp:BoundField DataField="email" HeaderText="Email id" />
                                                <asp:BoundField DataField="city" HeaderText="City" />
                                                <asp:BoundField DataField="state" HeaderText="State" />
                                                <asp:BoundField DataField="state_name" HeaderText="State2" />
                                                <asp:BoundField DataField="source" HeaderText="source" />
                                                <asp:BoundField DataField="postal_code" HeaderText="Postal Code" />
                                                <asp:BoundField DataField="interested_in" HeaderText="Interested In" />
                                                <asp:BoundField DataField="preferred_dealership" HeaderText="Preferred Dealership" />
                                                <asp:BoundField DataField="vehicle_purchase_plan" HeaderText="Vehicle Purchase Plan" />
                                                <%-- <asp:TemplateField HeaderText="Vehicle Purchase Plan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0} {1}", Eval("vehicle_purchase_plan"), Eval("vehcile_purchase_plan")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="own_vehicle" HeaderText="Own Vehicle" />
                                                <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("bikename") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!--/row-->
                    <!-- content ends -->
                </div>
                <!--/#content.span10-->
            </div>
            <!--/fluid-row-->
            <hr />

        </div>
    </form>
</body>
</html>
