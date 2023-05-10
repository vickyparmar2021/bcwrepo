<%@ Application Language="C#" %>

<script RunAt="server">

    protected void Application_BeginRequest()
    {
        if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
        {
            // These headers are handling the "pre-flight" OPTIONS call sent by the browser
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }

    }

</script>
