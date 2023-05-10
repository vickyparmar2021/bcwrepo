<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckGEOLocation.aspx.cs" Inherits="CheckGEOLocation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        var x = document.getElementById("demo");
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition);
            } else {
                x.innerHTML = "Geolocation is not supported by this browser.";
            }
        }

        function showPosition(position) {
            document.getElementById("HiddenFieldLatitude")    =position.coords.latitude +
             document.getElementById("HiddenFieldLongitude")  = position.coords.longitude;
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>Hi Bcwebwise
    <asp:HiddenField ID="HiddenFieldLatitude" runat="server" />
    <asp:HiddenField ID="HiddenFieldLongitude" runat="server" />
    </div>
    </form>
</body>
</html>
