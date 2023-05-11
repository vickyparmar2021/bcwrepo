using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class webservice_Default : System.Web.UI.Page
{
    public string isbillingaddress { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

        //Response.Write(DecryptStringAES("xIhcPloK4v3F6bUA3OmPg3uZTRjDI/sDDAtDuqMULwvR47bO7QmfQUqmKwp1ANhulKfcBpU6/eadGZvWfkEAd9FnZTLDCbUv72h5Bz4xTHYlTGMKaBJzmsSyAyVC+ExtwXbY68Duo09PgbFxeSep59rnTeNBQgHoU6NSU90E+ta2Bne39oVig2mKRhLZIojDjWp0IHJCxzPjZ1gnZwBJtE3enFgfOyJwj+UPsaE0VUti66/7QmTaIKczvB0O9W/H+JGZ6fIz2xvzJmgthntannIZtMhI9F9RzMGBbkOIbBqCDwgoAWQ8vk7TsST8tIkbSrHLIysunPvO+/JJvUykiw49IG5JSYhlcbb48hI6SmNfL1gyM14YQWns8PpsNSO3/4nWAsrB3MuDA2yjfBCj5yhCb1YR4jhi9JgzcIOYhsE3PIXdJN/MA1MFmemTDYp2HAnUELnHoz9lOhluUbDGvduk5YZF9yoFOpQI2BxP/d2ZhdhsOEMGTvkiSrS5u/oDdR/q9oNTgTZsojulTlR87zLt+wt79fGYo1wsaMGSg6BHC3Ub7xlWmQWM+0YiRW/R0Th5+kWvWdZYe22K5m82dj93PWC6D4odxuMcLfh5JMtL8S4YSaZVG7B2E5M59T2Bk+kKL8QedgluMBVmqlI/zM1PTVBvN7RG4ts22rdM5PFaZHhxfHRCb034DyoMQClymqdkLzRyPrEJzxqyOI1IWA=="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemHKUY6fe3C/6Ry//71G1Xbu0m8vCNU7+HP0m+1NcC2I03rYXiSIvUBlgqFD5fOsHQEe0T1dNpS5qL+LRJbqAlAmKgCLRuNpLkNQiYu28oNEKiNHJw76oiANYK8ioElBmO2hzCKuBLYn1BX9yhn0hcDZJt+/Ad6W6jMEIm+tuoaJIlTPtBx2/q93zUeRPHgXSdtiqgZa+Mhx3GRkrTlpvOck="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1LomsxlXyh0lIIvy3WWnL5G/B2BePF4MompTVBPCo/mYTik5+W8zynuqkRtDMnrydUtVravu2jZntVowPsx+FOgdg9vk0fGk6l0xoUwH31UAjIfUnD5pNvFu87O+e/kMrxGmXuEAV2suVrXGoApPJr41redotS5RWLr+QMpMBAOpboJ7L0QcvOPFSeAJAbkoaBUbJQgDU2rT+HogsVQObMIv43rDU="));
        //Response.Write(DecryptStringAES("gwSORvHlm8WCZ9i/Rgmf/sz4J9OSm9TJaWMPU8QrJKjAX1iP8FR1nzGNu0dSEawkYKM/PxLP/iZyGxuh6TJgBVWSqFdQ/dbeYYB9gpBtO7p8z+XTtog5paZKdlPuwkua8rnMzgJiDiW41PVsxQwG6Aa7S3REoGBC8XPJ8dHNVSsdnJ248zp2My3Jlax14QuV1vROI7GCeB8/ac6EboNzgp5OVxqdedYKndMBx7D/NHcOIRsfoS2s0usjhLKrxeU+FnQSTlyaJeKrUGoKlMMdmRXfuol8QRLr+dvN/vBWO8bRSvs4l3yDnx8dps3vO0CyT9fBtGxJJioGz7iurSKoKQ=="));
        // Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//2WHefBhhAREF8gWaLeC2/erUccwE7DjfwagU/EZJZxKA=="));
        // Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBg8hLZ+ClTgU1e3DNvC4uBfihR1UijDz+svNPEHCA5qo="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBf6tng1UNZu1zcqdfJg3rji8PD/yGdDD3dole36bC7rHQW4CSejOXwtXZAobtnKi+oPcpPvRJwkJb30rNUCOOKZPcCuezs1NE5NsxP8Vvpbs="));
        // Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBf6tng1UNZu1zcqdfJg3rjhHtzlbZtgu3IzqDNknJSle4wzhBg85RikEJWf5fqcOzSMN0I7XlvOJgFt44fIhH8JCxSmrLiccvf2Do6g+udBo="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBf6tng1UNZu1zcqdfJg3rjkLseT7znd69xqnhiWk/aoKILvjo6rUs+qjTChDW7SV3odS1yGAV/29imAG3MS/l39489BxIhEVms/OJhgj6qYw="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBf6tng1UNZu1zcqdfJg3rjsYARMWYmomawTwX0rJhsDez3yTkzqk31JC+MeH2NDlq4iTIOkMLMdOwr6HW+rO2u+Q+e/yaM29ZG8TRL9/Oz+E="));
        //Response.Write(DecryptStringAES("gwSORvHlm8WCZ9i/Rgmf/lHTvWUM1X7EGQpvD7TEpCya24HvnB5nlxz+l9FPtMUsHM63fYXK70b8EqfTJfRK/PJ29gOhZLgelIJ0VzrH8+kNa4RhcGdi8DjPGzcFSawDewuCPzrS/OPh4+ND9mVtHcOya8u3qIm5XOeWCPoNQTE3y5XcybESU2aLghuUYNlnt/h3aZMQ1mBvp1BPPy7GWgk2q9IQZ+X5Jh8D53x9BaCObc3wEELeXag45L/FssUy9yejaNqcIH9cA2w9VWgOz5YZwkKRYetss5mR7eqG+3G7OEXf4dj1sN6D31HwY068VIougQjduv02b4ixv5VOUeRDJKUZ3yP5MhKtcGxYIWXABrCk//pP+tSKln+YIDAwu0NMUxsaFQQC0lkxoS9mvSI+7qtRPz2ytPnhYePiTaJTfLZkw6y4+h7UymTIWn7O0seGmXV4nBp3H0CSp4WiFCFSmpcQZ/DxIHN/vrj/zvUSqFHMQb9nqNysEwlmaIGPV7xy6D3sUD/hpsT994g8acvnMgZTmOp/8c/r25eNwqUSxfIPqSOc0GOybzZ+8f5/deLU3Qv3r+AdnodGQgEB5JsoPFRZaEbl/xi8l7kZFBzupAkPIwjs08HX0/Kv3TdqE139UYFs2FqsCDYIJXx3nQ9W+Le8kDtvBZWP99a8Wr7UUwFJEkBsxLBdvyn2Ox+pmXBWN/SkeSnav1vZXC6gekeHTU0LrTlOlN2NSeXp/C+cN88Mi1D+nvOCpxH+gVAF1+qwbc9el7eM1wUo+5sXfRrL+BOuF/avgvHmZu4NXtq4d7iv4Q6VezwZuPdWkQd8j+DawMzQwSDvQJ7+yC2zTZrFlbK/VQUhIe2MBuSNQjt8c0pXo9rFJNQ/1MDHKbQ2ZTGL8QB2A3S1Zq5OblnQ9C5mjgTkM7JGcsi1hHjS/LESQwXfBCinGJCF29EhuMuOL/iUuDAvXaaloUrJh8s/J7QgnSEieVyvyPn7rne5pIc1eWLYuk3ct5KOuIQZYjcCpfenq5JcEEkDjN2qdV6xSJLQgXA/wDE5tWl0asiAqrT/Js6TxqQofmVXo1lf0EnEDraOFvnk4tKex7b30iqknWmjcDq0fLt2zvl4b5ITsjZ2yX0SoEhE6qXS7aQuR5LhlpqlBImLuHRpngsCX588R/hT0ylkCeqpEf30A5/D2IL+49TuH13Gj+eoRko6O+2ynck2fhLccPKTn4Ksie6+vPgJJ0jtfUb6jCgIhPE3UmHqqT0/9HA9pJ6jV+MIpyZu0aO3VwxXz1b81HPX9jCEL4gws6nDvYdwMMtnXma4LdVVKOW5f6yFLo0piIvE6I3CGPEezxt3nKdYmp44irB8D/je22kvG11R48z2Nhpd9Eos/3hWWiXfTkWa3TeCAY0cETs4sB2XDunoXjY+dbslduk24CbGJpDolVYjRWk9/3dqDbVJIfUMxXIjcsUw3HjJWUhEmqNFE4cuzYQLxgOtu8akZGKl/wwO3uGIQrpGd8AiQZ5KHk5c3Pg6F8F/6wUnfoC2GwlrxY9tzLNH76W4LuTHO39OuzlwKAty9y3v7McQVnVLyu2U+YHjm/nEbOHsGOi6vYNmocgTmtbgoQXABs4lQAlskPju6jmyDNCeCpftOkhhdz0Pn9ahicxjnsBWVrD1gmJaWCYwUtVeJIn1MF/2+3lNHsMnYLogm6/kApeivLOEejE0QAkAEE71v9BbKrWptvVCOIZu5/k6DPYpwdK6gfNrz9pawQ6TP2kRLUlBLVI7Gdh314OA5PQxit3aRo4wlAyfNqyFx91oRDiCG6Za0fnj2avrl2fhXSpnVi8bWpCC1ISq6wi1ZzcoSHZT/D2+Omz/EL9d85xDZsIOMBo2oNfD1in7/gRfzMedKmzvNTdkbx1Za0P6kL5QUQT8u+ngEsefBBP9f/ptG0wD97VZ6zL21ZsY+zQ7RCLryoYKSwdHFZ1Y92jbr29GaXZNi9qqDLS6ZfjuytR/AdFFMFKp145MJDmVXJ0OWjMtF/LJ9PdphHfS73lM3X35cMrGKgL2K9GQ8rkof42dOERKzg8bV1hJv7gjQBrjGfK9QWAopl+rfQQjjU5tmH/ZF1j+exq22odlUZUeS0T7joEilaAsumj8ahcvN4c1EDtKPRe0WEXVHzEHRH4OfYykoUmo4pw2GIKUO+vNT9uVgfKi2TcV1PmpgNeRVK0nu5+Y986PaPjlwe1vek+LhKZxw0p7OMAbxWjlkbVZ+XFaHjgUk8pxqOPUJrBpHufHT0R7WuuOUS8wE0ncNI3AMJ9OlSiwZ9RkfYccoA7qvHd6i8TU2GKA/dEsMG6y6bzWeAHFTWyA46LN+rkJmVUghwmeAt/EkFjZl9lPVXA4GUTSR58zNL7bATKJw7SsKpVXxAgwG6Nwrzr8y7A3AdQyn/elZDppNixbN8v1OPC3YdOtu2jzfbjq+Ix+ejzMcZdIUs+KK4Z48oCrd0PtYB1ShZrviAnYi5gXN9RjJDxeRk3Y2IAoprXiCjNlTSvGDEVIDHhxiMbV/94kW3yl4hILLDo0ObwoXBaYpgW2MtTulWUgHo9UuQPVsI3lvfkMFGXXBhoUgx8+8taQFbuEH4ltGuQcuPLndn1k4iHjebk3qvgbWYIkgBalfjXH3fX/KKo5+hQgaVco6sQ5U9tafRYJDcQ73fh6ocpI5fuV+GtlsIF748a9f74RUaOAUOmgziVVrjtsyMiRG8ZokkIhQ6gNN9z9iCZLcL7g71WtMgKBknbneJxSyQ=="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBf6tng1UNZu1zcqdfJg3rjsaWuEe9Yer3TAcpjM5RkG4PKP/686Y24ucK+nCrS1c+U8DWk/fiWh/Yr9hMEOIXvCGS+xnTWkiVrbCJCtrRmv0="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBf6tng1UNZu1zcqdfJg3rjuum/lFTGXX1rH/2MRwyTzHhTb9VOjkeP5YASmVxESJSV1ufc+5pFFVxIyOBPLKS28nYQEUl0aKoy4kRyQWzp8I="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1Lomsxlby7cG0IcmRDPiET8HfCUorazBKc3C6wjGOpfXJRrrnCuQ0Nj2dWfTjvY5afP4wemPAykuky+t712PNQrzahFGnu81P8E3C2uT2gxHDO0rfBKniRN+K7mMJACL/n8q2QRhEwbmrnfk4ImS+bvXnwis441YPHQFqrmHfZCSSV3qBHy4vCknEw99uNbETRCGFcSCGBNtA+NbVyrQfUNm1FDkk="));
        //Response.Write(DecryptStringAES("nfGcQ/9vlO/J5gB1DfrrzWuLLzObrLwZUdn0AvcW//0h++sA5rHzhwf9R68I7EWf7I4frXXW/OP8gU1LomsxlVHya/+gTeLmVkTHiXn7THDxlLewM/QASlY8n3mU+vCAFJisewAx6sqXccafJxIm8IhO/1s/A4UJGSTjNX9QQUXl0APEOazHHkg4Hlo4SxUltQOmOOKVTZPQVvz7pUGbOPp/tUBfcSuXW9MXnso9eKQ6bhhTZaLZfpY6OlUhvAzj2jX+38aBK14qXDKvQSawVw=="));
        //string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();
        //conn.ConnectionString = connStr;
        //SqlConnection conn;
        //SqlCommand cmdInsert;
        //SqlCommand cmsUpdate; 

        //Response.Write(DecryptStringAES("").Replace("curleyfront", "{").Replace("curleyback", "}"));
        //JObject jsonCity = JObject.Parse(DecryptStringAES("juAlQok8GBKIcebOlKcEENR31xFXwSZ3FR2DOUq70/xjQSsN3wdn5O2p/fmOj6zT+DKPHqgeu2J5VxTUozGf0JTXNDZh3ZpY/VTEIobCFHw=").Replace("curleyfront", "{").Replace("curleyback", "}"));
        //Response.Write(jsonCity.SelectToken("d.CityId").ToString());

        //GetName("bD0nWQMMpYp3oEYtNiT72KCoJwyUkQy/Ye7CxVV2ULW7S7xYRCB1laUx9u1vzUGMr1E+U4X3T51zGK+PjL1cHE32H1cAlPVBDW7PCyTCeew=");

        //GetLocalitiesList("eGZdCK0bs5VFQDCIXbLzSfsCjAekOmM+4J0vMsQ+AdSZoFBetih+cejH9ffV3di7");

        //verifyMobile("{d: 'Hh1NgbeXp/a33trJBzbpaa4hEyViz8fIs0mH9L1pICRKNvCdsNTPLn408+nx9lVKKi9gWjgP21qlv74NFitcxQ=='}");

        //verifyEmail("{d: 'T8X2F50ahs2MHHqEO2txl2b0ib89cIViaqWbFcjv1oA9hBrh04xYx4IL9kKdM9MIdKKBZVMKXh/3dztp05uQrQ=='}");

        //InsertReg("{d: 'O6zg+B1whrs7FWV2k3bN6bc9wbJUV16Hb8Tdt2KdL0eVJautqSaa6QhsLkp3uZOpZAViO/ciavzEhLLFNhW2urJalYlT3bqznWhi2QMhVz5KkNWLha0S2I8AQvFBci4EHpOwZPM2FDl2V3x1pIhnkhzG6O8USRP4rkmAI7wv0zwQs0OlIWUCf7AN8JuLjR8z78nuVwYZRoj4KRxlLg1GCFRJOmT7pHkuP6XdXOdVGc0J8Rwa/BqEc2r3chG4vFdmvCOxtU2xTpKkrZQu+N1W+BexS6+6glJu6neUkp568dtuPFWYzmBnayILGdXcJxi2'}");

        // UpdateRegPersonalDetails("{d:'SqJvNpkXVm5cFuLGpoYreKWXe4UyEF+9yNG2Q9FhNliaLlhRbrDTIS0wHBvhQEzzyA+csGR3KfEQlIssvMhYv2rIYmCaP9vEkksdGku8i8rybJpmegV59xC1ZPCLwEjNa2dtZDnonvKa0aCIOSsAwnCbupOMoAzfjlK44YkObknhxpXyPr/BFhzwXYT0LXleEVqWJ6UiLx2N0d6jSFgqEJXk15GjieiNsCnMssr/dPnu7/WMv4RssdBOt5rhyfunkbCCwNKyrDS9XuloTRO1KBgojyAuyUxNjlVHlU2MB59x/PbPI/Et6WhG7Yh0LNHZhFaD0ppDunzgXRCevPPDNg=='}");

        //RegInfo();

        //GetCity("{d:'nfGcQ/9vlO/J5gB1DfrrzU7p4G/pYTtqZeIvbQgg2es491R+QlS1fKUBlkCnD6Pg'}");


        //UserLogin("{d:'O6zg+B1whrs7FWV2k3bN6cwiicAySg2NjeXWxEvyA/FDBQgSZFCSxmxbXL+Jc0YkmEZV0qumhf3ZkB9qCy6x/bTbXrFMJzP465QDPCoeJT9WioqjDnuoTsIZyViqvkizdhuDl7Jfb0MFkMPc4JL5DKaISQxbuWpcYrbAQ+VD/Dc8RWSYkOh7pKXZ1i5sBl5SjhGMQbS50pHSwTLLOoTgeJmLdKuFsc7X1cWXm7KHTIxW1BRNN+jgA6u2oEw47g5Gy/e7j2tf1SG//DBrtAOYOWzQTw6fLarNcv8sSt0zl08='}");

        //string rand = RandomString(30);

        //PreCheckLogin("{d:'2l/wnD3OllwrnTdrf58UcA0B7Og/Neadb69PrhfYpabHaSEpFeUKHR/Ht6y8+GkBmsOFfQpvQf9EweGJJvYqZv0VKKoqb2qoIdLZ7GDAHOBAKVIBSnGAGhXDF3mFTd3t4e9I8m4ciXvezLb6QvLCUsLevtgK27AfuEWBEzGMFamiwjW0OeDD+D5Wvsd+kvf99eJry/xJRS8suJVN2Tsjdg=='}");

        //udpateMyAddress("{d:'e99T4yqRGy3ltF7xvxzzk97WjovW0uR2N04wXoctHLVA3UrgjY4WYHrBAcPKsIFP16OIJqMczRuHAy2a9rTXqJHP0m0IwUKwku089KseQNqIJdlkgzYWgBScmC44P+6Rp/XCkHMm7PmOmZuA0/NrLGZBMO/4Jo3mvd6CbV5qhZw8dn3le6HUawCDnnr/jlN/f3+gq0p+LfKCMaNLogkTu/Jkc4BFu0T9zEBAfE6XP4H5wVUr40MTdsb1FrWCAJSl/xIlqRIjmYe+JNFGvjkCMIDqdGjP/7FbXElrVckB81mxMA67FW2f0HhDsyb8kuVwlwnGnbfPEiZnPJcIJSvdeoMU54nk6IVwqpSWm1tah+nJ8Iz98wUBVNK04xJqWqwnILNlZApTQXltaNvIJg+zTcpxx2s4aX3Kd7+l9KHhgsINJPjInPIjcS6OWNdQfej2h7cdhUbIkgDGqcrLTok8i9pxpxxY8Tsjct7UaRvF5/U='}");

        //UpdateRegMyAddress("{d:'SqJvNpkXVm5cFuLGpoYreKa1KXmNsUgNdFr4v6VEnPINUu5d8SOQsLtcl8eDGv29Zss/WBchTnwH8tjpKSOXBQ+4LcYPmoArjuz0LPOv9KnvEbBJvkYRcl1BBkiGGINLT9iHKyXC3GJs37LDjWkuCGtE6g/39EE0bGwmob7Ww9w3IQX4qDk5AeaHt2KP7vcB9U7M8MpARLMAt0p5vqCjabX3gGwe9ENLYw3IFIfPMCdOoVIm306v3rked3IZmx4djFnPwTmZNlR2bIT0s1ruCDmsZ9Lfm2SDZ5n40UalcKAZPBPZXUl42FvbGOF0rnDI+iXaoKpPQV2Hc4Ji5mNG0REzNw+jnDP7q7tZQJcnHyjN+6x7E9fWkw1CQT4Q3RjwZLPoECBszn4Bw8Xjr/zRjdnQxBES2IUPSQeR/Bd0SFaC04gSdc9p8Y/L9v/qcOV1swlqhVXt0uKZWHlL1JW8jewHGSOZUHGTvdbUr4pb5yVMEYDoH3SxTB1jhfyTXKD51YhyFn0buVMSW3TY6tjL7KIJskldpzjohsfNdCWd4Q3hKSRwmHpEtCVDOCZS2cVgCRwMk7QyV1gRpKi1A4XR4Q=='}");

        //GetUserAllDetails("{d:'Qd3rxwYSYmQic+5Vtp0L+ljl3Yhico7eofFbvm/IfebIDeYy7zY0s1kGjbyTlYI4+LTym9ROuo8mqqNkwOwmftt6gdtbwwnBGteGQtsXNY+Uaht68/+95Q+Sko0pojPqj2Cu94uQclI2dQ0vIoDe4A=='}");

        //UserLogin("{d:'O6zg+B1whrs7FWV2k3bN6cwiicAySg2NjeXWxEvyA/FDBQgSZFCSxmxbXL+Jc0YkmEZV0qumhf3ZkB9qCy6x/bP8/qJF7mzGE7b6MzAhfwa4Va1njRqf/0auNpCP4R997cwD8xp+qYzlNclJDRaAfbD2GTYpJ96p2D5lVBRw/91gcKPMmBzkje5qVObWCjPoBnUyJJP00WQAfv5n0OhUkzFmq90XpLgtQfLjp8SPIGzZg99w9wSxXP4U4kvZwi6qr3rYOdANPAswF64YCcOi4OGMS6Es/k5u7o85qXkdMe4='}");

        // verifyEmail("{d:'Qd3rxwYSYmQic+5Vtp0L+ljl3Yhico7eofFbvm/IfeYDS4WdwOqI+ETmmlBJcCVZskYiKpa4gxDMvvCrULwF++P5LLQVs67fkqvPyAg/2Wgnyv7J0NqBzT+0LtjFrOBZnjajuzvRg77Xo0dA19zp5w=='}");

        //GetUserAllDetails("{d:'Qd3rxwYSYmQic+5Vtp0L+ljl3Yhico7eofFbvm/IfebIDeYy7zY0s1kGjbyTlYI4+LTym9ROuo8mqqNkwOwmftt6gdtbwwnBGteGQtsXNY+Uaht68/+95Q+Sko0pojPqj2Cu94uQclI2dQ0vIoDe4A=='}");

        //UpdateBankingDetails("{d:'e99T4yqRGy3ltF7xvxzzk19/wZxyvLHQCsLoTNbQgjrUdJs44OHltQThJdemoTq2M+Kn/Zvj0lDQI47fN1VHrtk4tHNQsJAfoReSL13++UK69irRdrv54Kzwfzik8YHcv7v9kkdvMDIToCjy2v+mwazaLGoh67yLqbCBac8p5cPke8wXAN6ShoeGzDCEICNJE0cgbUNFpXq9YisHlxA/5BdAkGPX9mArw0x03Mk346fItTnO1lLsixwNokA1Xvreb8i9gmmyPl+skr6Za7ldgc550bI7H5xRtMLPfNGHqnGr4kcvtCI/s2lRyVnh+YEntSLI8Nm8OiDs+ozRXuPdmUOZGJmWVOdovFp7rUwFu01VZI1vKSo+sS9eMxl4Kyj74r4tGi9w4WKxesqN75ym/T1wen1nwWpiHAHSSoDMPM0ma1nrH5qsYFAqC3CdvGpgIwtYqJ6gnIesgT9GSY5A8I48PYl5MPQglIpo5HZvCaHmD8wd6534HOSg8G3jkPR3OZ/DxuKY6wsKfj9hbSob9JXi5ly1eFmNtacIowfNgTP4+FZ2VzjAWtTypHmkrUece0c9hXmp+H5L1my/P4WybZsE0brn1t54Qp4tUV9N//bnxkDOxIZ1qboDK6Fqv+7f4AChBhpdZTgP1vR83wlhCrCzGyoQKghxcT0Lf6xGgkMkjnU3lR9H8/HHhhXRDoGe'}");

        //UpdateMyAddress("{d:'e99T4yqRGy3ltF7xvxzzk19/wZxyvLHQCsLoTNbQgjpQ4EOsL91mtMGPZF2lchs6qCSM9dBoYGtksbtl2l9tlQQgAw3LQfmoa/ggnjWVBQzpcwXXzkpBUYM9Aw2vdkEkcjzIXhFjhK0JfupbJzcWzPD3yt1DFJkSIhVsMTwTSoMdp3fAB7hZdW5DfXLr7+unp1dAkVvWUEITnp2RVVG2gw8P2YWQuTFJDod8OowBOzoHdWw6CmKkYmttWQM8Op35E9tbYzS8pSVD17JFRZBwQW3hhE3uPIhGjBQmRRZdvqi9WpUQ16EHh/s0ASXUf7YhYxbtpr3VEHWnf4KutcWhUGale6/LV15LVvP7yoeNT4Lt4a1uDfazVZwPTX8r74FHw8ovhvPuT0dxN6iEqXf1NVEKLc+IgdnUFMAnJ8wukDiYGze7SOJoy6bcBEkyrqWzGA0dMk3ymt0JspZoJ9IYmg=='}");

        //UpdateRegMyAddress("{d:'E1sCHbsQ03yM+bXEj4bOs3CS/RqNujUX+X8ICP76Kri9fHsfynIw41LOfo2WEVTdFwUhv8sl7XnPfq8Jjo97mklWSWGjxdlwNiQd9RGzkCkQII74kW68WLslyRSDQFbTEtUmN0gK0POUSUOhAAdfMPDJrFyGyfPdX51FNq3EP2U6oxXIbPumJUSZmDfHU5BubqsHKH56tLxPRhBfbtRZ/7su+evfACO8KcK9OM8XQ7lIdelwckKv8ueCzCSqT86r20cvXqucYMznQ+GOt7G14I1HCwBva2ley+ir6zR8NVcA6zmWzXs5m8ZAiiZ0iLpbtLSwGmHH+9YTnOeqZ9AbBTssEpmLyUgBJczLUDD7dtNiDizag0OAehFp2W9LyrvH6EGPotSxscQeP5hxkC/EHNXzqnC2P7EgnAQ+qQkqxBQXGypJ70VnFIlHy4vpxe7t4NMIZeOf5EAyP0CZ9IeRxXeJX0irsMfV6rkxj3Mm/6TkfpKpHNN+W8vs1udoAKuSI4PAoBjbPMyA2KvOIXKJQ3jfuB5wCW/pZ+8XbG8Af6u8IpjBf5+1qaw3ntZblYPYUKuVSQsWfCabyr+qyj9pHqvlr/IQVNxDGGjuvwrrp9o='}");

        //AddRemoveLotToWishList("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC0bhOm8clulrB/VtINriPOKcH7EjFITtwy0T61PSX5z8zA9xIXab4ikx0nV73USEAVnT60yHSuKWcqatRu3GYT+bgjClZMgF+6aYSXcKZUIWl3BxN/jKJ6SBXRBSoobI8pV5LPFWDpP87101L+7E71o'}");

        //ShowInterestInAuction("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC28O/g3YOhrYkfVC+5bUo18QCTd9pf2CeXZPpNN7p8DJofTmzbNH9s/37xTNYTc/zdEXwOE2M36NStGpbgmixA2gMNKTNjVv4TEUol2bOtihExqYh/vV1OxtwqmMdOjg91jaxu8VbtVchLCDMmJjU8dwRogoijUAPIK3iLYE5stEc2+6OoyA2k2CsOOb6Pdr76lON6EgmLRFhsP8NZlMJDT12nNh9vGi5dIn3SZOh8nR8QvxtS9hOfMdD8rSL35YdY='}");

        //AuctionClosingSchedule("{d:'e99T4yqRGy3ltF7xvxzzk5MeaN132J0fhhY1w+8SrHfpDAYakZ+BX7hlPBsUBw1rPN8RTlntGFVP6m2VV0OI7FJPhvctJuQHU/QFMlyFBNoy2qYE/2pEXlbrt66xNUDsSXwkFwm3zEK+Qlqt2Y9nEaZyYYFTUsWi6wjGANFx1IU='}");

        //UpcomingAuction("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC0bhOm8clulrB/VtINriPOKcH7EjFITtwy0T61PSX5z8zA9xIXab4ikx0nV73USEAU1ewEFv/NfzVtkioT7wY2HuF39qJKCF6nHW+p/QiN6Dg=='}");

        //UpcomingAuctionInformation("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC0bhOm8clulrB/VtINriPOKcH7EjFITtwy0T61PSX5z8zA9xIXab4ikx0nV73USEAW5jkHU2ATf9ceidDZz5/SfLldg9w7amp00Bp8ia7/Zy13ACm3svmEHjeQFtKrOk428H7Ns79qx9wuuqzFpwNMYxnFzyYvymeLskzi1gXQNNQ=='}");

        //UpcomingLots("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC0bhOm8clulrB/VtINriPOKcH7EjFITtwy0T61PSX5z8zA9xIXab4ikx0nV73USEAW5jkHU2ATf9ceidDZz5/SfZqUBr5l+yfe3XSYKYODQa9bfkHYNpXEqPAmknjau/rg='}");

        //GetProxyBidAmount("{d:'e99T4yqRGy3ltF7xvxzzk9Z3V5Jho2Yx6B0jteAx834aJrIbtMgTO2i0Mtn3pDkupoMSMdbh/OXT52LJhNIJsd53pj5rJJKvi3Ac4Hb1yIs456i7ojXTLiwVrG5bEQuvnMv6MF8beGTzY+2ukSny942+KFbimsFZp1fPS9XpsfYu9yJ7GxFlNQAw/a8TC+Mp/s7DP9/vdrbO/XHmzZBY6A=='}");

        //InsertProxyBidAmount("{d:'e99T4yqRGy3ltF7xvxzzkzLWWgj0B31N4zsp9QoxYQohUibvhAASPKqSXiRffeqqxxVKOxU9oFLIMbni6HqdHYUiRm7tDedBpQ4xCdkNynfq1SBT6R1MGUUCWIPb0eUGCIXxab/+v3jDlSAJDVo3nzC193rqEz+o0xcRhlXcEh7IT1IiyPBpdTzJ5wHeqyfLgp6fux1TvV8sYK0p71BYfB3zeJpq3foJSXhZeUOXqMk='}");

        //GetLotDetails("{d:'e99T4yqRGy3ltF7xvxzzk3lalUZYhV8dVdpJmFUXAinCJWHDbj/6/n4sJ2bdAGC2Vynfh9bQmCeiEvOWFslEXj1ku7skn5TdKSj21DSYCtbOGmCvndOAXhOjDwAg6S3UYC1xxN37T7wfXZTYSX5Oas/mQmE4kBVkWrw+X+V/CHM='}");

        //GetSimilarLots("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC0bhOm8clulrB/VtINriPOKcH7EjFITtwy0T61PSX5z8zA9xIXab4ikx0nV73USEAVnT60yHSuKWcqatRu3GYT+xM2F314Vn53P9OeHUJ9pRXf9zMM0BuoJq2s3x6e6XxU='}");

        //AdditionalCharges("{d:'e99T4yqRGy3ltF7xvxzzk1P4s05TJzgpUK9msjKICC0bhOm8clulrB/VtINriPOKcH7EjFITtwy0T61PSX5z8zA9xIXab4ikx0nV73USEAVnT60yHSuKWcqatRu3GYT+qsnhRGeRw7IIml48Z3rZDBEFNKU+XyhqJyZdh88ZUZ8='}");

        //InsertProxyBidAmount("{d:'1br14shEMBwOsgo1r5FDvRc1GGwwbUnjW+8NOCQUEV3eAS3lFOAJjW34eweneBlji/Kl+C9x5Zz/n8sI7Ql0hKyWN7JJpK3Oy7VEhhyeZ0NyV9X0KZgKUVdGvUBNANjO6iQpO00yTXOddb3pheiyu3fUDxS2T6v6kUxvXoLLD5dShZVNows6bL/CGVerVqsdlrKzWfkQmVfFgQk1+vg/g5y9Dg/YXMeS0W6SCRRfn4GER+uQTFATa/Ce2k2aHbRaIPdP21aANauxoHOo1uXN5X/X9Isc+CASjGfCAOotnLQ='}");
    }


    private static Random random = new Random();

    // Random authkey generation
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    //
    [WebMethod]
    public static string DummyService(string d)
    {
        string decryptedString = DecryptStringAES(d);
        string replacedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}");

        JObject jsonCity = JObject.Parse(replacedString);
        //string i = jsonCity.SelectToken("title").ToString().Trim();

        //DataTable dt_city = new DataTable();
        //dt_city.Columns.Add("Name");

        //DataRow drow = dt_city.NewRow();

        //drow["Name"] = jsonCity.ToString().Trim();
        //dt_city.Rows.Add(drow);

        string JSONString = string.Empty;
        //JSONString = JsonConvert.SerializeObject(dt_city);
        JSONString = JsonConvert.SerializeObject(jsonCity);

        //JSONString = JsonConvert.SerializeObject(dt_city);

        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback"))).Replace("[", "").Replace("]", "");

    }

    [WebMethod]
    public static string verifyEmail(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonEmail = JObject.Parse(replacedString);
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            DataTable dt_Email = new DataTable();
            dt_Email = BasicFunction.GetDetailsByDatatable("select email from user_details where email='" + jsonEmail.SelectToken("email").ToString().Trim() + "'");
            if (dt_Email.Rows.Count > 0)
            {
                sResponse = "{'status':'false','message':'User already exist'}";
                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                //sending Email OTP

                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();

                string sOTP = _rdm.Next(_min, _max).ToString();
                BasicFunction.SendOTPOnEmail(jsonEmail.SelectToken("email").ToString().Trim(), "111", sOTP);
                sResponse = "{'status': 'true','message': 'OTP send to your Email Id','result': {'otp': '" + sOTP + "'}}";
                //sResponse = sResponse.Replace("'", "\"");
                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string verifyMobile(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonEmail = JObject.Parse(replacedString);
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            DataTable dt_Email = new DataTable();
            dt_Email = BasicFunction.GetDetailsByDatatable("select mobile from user_details where mobile='" + jsonEmail.SelectToken("mobile").ToString().Trim() + "'");
            if (dt_Email.Rows.Count > 0)
            {
                sResponse = "{'status':'false','message':'User already exist'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);

                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
            else
            {
                //sending Email OTP

                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();

                string sOTP = _rdm.Next(_min, _max).ToString();

                BasicFunction.SendOTPOnMobile(jsonEmail.SelectToken("mobile").ToString().Trim(), "111", sOTP);
                sResponse = "{'status': 'true','message': 'OTP send to your Mobile Number','result': {'otp': '" + sOTP + "'}}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string ForgotPassword(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonEmail = JObject.Parse(replacedString);
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;
            DataTable dt_Email = new DataTable();
            dt_Email = BasicFunction.GetDetailsByDatatable("select email,password from user_details where email='" + jsonEmail.SelectToken("email").ToString().Trim() + "'");
            if (dt_Email.Rows.Count > 0)
            {
                //sending Email OTP

                //BasicFunction.SendOTPOnEmail(jsonEmail.SelectToken("email").ToString().Trim(), "111", sOTP);
                sResponse = "{'status': 'true','message': 'Password send to your Email Id'}";
                //sResponse = sResponse.Replace("'", "\"");
                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
            else
            {
                sResponse = "{'status':'false','message':'Invalid Email Id.'}";
                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string InsertReg(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        string sResponse = "";

        string JSONString = string.Empty;

        try
        {
            //Validation


            //


            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_InsertReg", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;


            cmdInsert.Parameters.AddWithValue("@first_name", jsonReg.SelectToken("first_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@last_name", jsonReg.SelectToken("last_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@email", jsonReg.SelectToken("email").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@country_code", jsonReg.SelectToken("country_code").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@mobile", jsonReg.SelectToken("mobile").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@password", jsonReg.SelectToken("password").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@register_from", jsonReg.SelectToken("register_from").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@device_code", "code1");

            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string UpdateRegPersonalDetails(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        string sResponse = "";

        string JSONString = string.Empty;

        try
        {
            //Validation


            //


            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_UpdateRegPersonalDetails", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;


            cmdInsert.Parameters.AddWithValue("@gender", jsonReg.SelectToken("gender").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@dob", jsonReg.SelectToken("dob").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@hear_aboutus", jsonReg.SelectToken("hear_aboutus").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@interested_in", jsonReg.SelectToken("interested_in").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@nationality", jsonReg.SelectToken("nationality").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@register_from", jsonReg.SelectToken("register_from").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@device_code", jsonReg.SelectToken("device_code").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@first_name", jsonReg.SelectToken("first_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@last_name", jsonReg.SelectToken("last_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@password", jsonReg.SelectToken("password").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@interested_in_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());

            //Where Condition parameters
            cmdInsert.Parameters.AddWithValue("@email", jsonReg.SelectToken("email").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@mobile", jsonReg.SelectToken("mobile").ToString().Trim());
            //

            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string UpdateRegMyAddress(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        string sResponse = "";

        string JSONString = string.Empty;

        try
        {
            //Validation


            //


            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_UpdateRegMyAddress", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;

            cmdInsert.Parameters.AddWithValue("@gst_num", jsonReg.SelectToken("gst_num").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@add_line1", jsonReg.SelectToken("add_line1").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@add_line2", jsonReg.SelectToken("add_line2").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@pin_code", jsonReg.SelectToken("pin_code").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@country", jsonReg.SelectToken("country").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@state", jsonReg.SelectToken("state").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@city", jsonReg.SelectToken("city").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@location", jsonReg.SelectToken("location").ToString().Trim());

            //Where Condition parameters
            cmdInsert.Parameters.AddWithValue("@email", jsonReg.SelectToken("email").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@mobile", jsonReg.SelectToken("mobile").ToString().Trim());
            //

            cmdInsert.ExecuteNonQuery();
            //


            DataTable dt_Email = new DataTable();
            dt_Email = BasicFunction.GetDetailsByDatatable("select id from user_details where mobile='" + jsonReg.SelectToken("mobile").ToString().Trim() + "' and email='" + jsonReg.SelectToken("email").ToString().Trim() + "'");
            if (dt_Email.Rows.Count > 0)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();
                cmdInsert = new SqlCommand("sp_InsertRegMyAddress", conn);
                cmdInsert.CommandType = CommandType.StoredProcedure;


                cmdInsert.Parameters.AddWithValue("@gst_num", jsonReg.SelectToken("billing_gst_num").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@isbillingaddress", jsonReg.SelectToken("isbillingaddress").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@your_name", jsonReg.SelectToken("your_name").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_add_line1", jsonReg.SelectToken("billing_add_line1").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_add_line2", jsonReg.SelectToken("billing_add_line2").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_pin_code", jsonReg.SelectToken("billing_pin_code").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_country", jsonReg.SelectToken("billing_country").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_state", jsonReg.SelectToken("billing_state").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_city", jsonReg.SelectToken("billing_city").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_location", jsonReg.SelectToken("billing_location").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@UserDetailsId", dt_Email.Rows[0]["id"].ToString());

                cmdInsert.Parameters.AddWithValue("@gst_num1", jsonReg.SelectToken("gst_num").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@your_name1", jsonReg.SelectToken("your_name").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_add_line11", jsonReg.SelectToken("add_line1").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_add_line21", jsonReg.SelectToken("add_line2").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_pin_code1", jsonReg.SelectToken("pin_code").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_country1", jsonReg.SelectToken("country").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_state1", jsonReg.SelectToken("state").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_city1", jsonReg.SelectToken("city").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@billing_location1", jsonReg.SelectToken("location").ToString().Trim());


                cmdInsert.ExecuteNonQuery();
            }

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

        }
        catch (Exception ex)
        {

            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string UpdateRegBankingDetails(string d)
    {
        //Maintain Log Fields
        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string UserAgent = HttpContext.Current.Request.Headers["User-Agent"];
        var Agent = HttpContext.Current.Request.Headers["Host"];

        var DateTime = HttpContext.Current.Timestamp;
        string Date = DateTime.Date.ToString();
        //
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        string sResponse = "";

        string JSONString = string.Empty;

        try
        {
            //

            //

            string sType = "";
            string sAdhaarCardFileName = "";
            string sPanCardFileName = "";

            string sPassportFileName = "";
            string sPhotoFileName = "";

            string PancardImage = jsonReg.SelectToken("pan_card_base64").ToString().Trim();
            string AdhaarImage = jsonReg.SelectToken("adhaar_card_base64").ToString().Trim();

            string PassportImage = jsonReg.SelectToken("passport_base64").ToString().Trim();
            string PhotoImage = jsonReg.SelectToken("photoid_base64").ToString().Trim();


            if (!string.IsNullOrEmpty(PassportImage))
            {
                sType = PassportImage;

                if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                {
                    sType = ".jpg";
                }
                if (sType.Contains("data:application/pdf;base64"))
                {
                    sType = ".pdf";
                }
                if (sType.Contains("data:image/png;base64"))
                {
                    sType = ".png";
                }

                PassportImage = PassportImage.Replace("data:image/jpeg;base64,", "");
                PassportImage = PassportImage.Replace("data:application/pdf;base64,", "");
                PassportImage = PassportImage.Replace("data:image/png;base64,", "");

                if (sType == ".jpg" || sType == ".png")
                {
                    using (MemoryStream msPancard = new MemoryStream(Convert.FromBase64String(PassportImage)))
                    {
                        using (Bitmap bmpPancard = new Bitmap(msPancard))
                        {
                            sPassportFileName = "Passport_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));

                            bmpPancard.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPassportFileName + sType));
                            sPassportFileName = sPassportFileName + sType;
                        }
                    }
                }
                else
                {
                    sPassportFileName = "";

                    sPassportFileName = "Passport_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    byte[] bytes = Convert.FromBase64String(PassportImage);
                    File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPassportFileName + sType), bytes);
                    sPassportFileName = sPassportFileName + sType;
                }
            }

            if (!string.IsNullOrEmpty(PhotoImage))
            {
                sType = "";
                sType = PhotoImage;

                if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                {
                    sType = ".jpg";
                }
                if (sType.Contains("data:application/pdf;base64"))
                {
                    sType = ".pdf";
                }
                if (sType.Contains("data:image/png;base64"))
                {
                    sType = ".png";
                }

                PhotoImage = PhotoImage.Replace("data:image/jpeg;base64,", "");
                PhotoImage = PhotoImage.Replace("data:application/pdf;base64,", "");
                PhotoImage = PhotoImage.Replace("data:image/png;base64,", "");

                if (sType == ".jpg" || sType == ".png")
                {
                    using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PhotoImage)))
                    {
                        using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                        {
                            sPhotoFileName = "Adhaar_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType));
                            sPhotoFileName = sPhotoFileName + sType;
                        }
                    }
                }
                else
                {
                    sPhotoFileName = "";

                    sPhotoFileName = "Adhaar_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    byte[] bytes = Convert.FromBase64String(PhotoImage);
                    File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType), bytes);
                    sPhotoFileName = sPhotoFileName + sType;
                }
            }


            if (!string.IsNullOrEmpty(PancardImage))
            {
                sType = PancardImage;

                if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                {
                    sType = ".jpg";
                }
                if (sType.Contains("data:application/pdf;base64"))
                {
                    sType = ".pdf";
                }
                if (sType.Contains("data:image/png;base64"))
                {
                    sType = ".png";
                }

                PancardImage = PancardImage.Replace("data:image/jpeg;base64,", "");
                PancardImage = PancardImage.Replace("data:application/pdf;base64,", "");
                PancardImage = PancardImage.Replace("data:image/png;base64,", "");

                if (sType == ".jpg" || sType == ".png")
                {
                    using (MemoryStream msPancard = new MemoryStream(Convert.FromBase64String(PancardImage)))
                    {
                        using (Bitmap bmpPancard = new Bitmap(msPancard))
                        {
                            sPanCardFileName = "Pan_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));

                            bmpPancard.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPanCardFileName + sType));
                            sPanCardFileName = sPanCardFileName + sType;
                        }
                    }
                }
                else
                {
                    sPanCardFileName = "";

                    sPanCardFileName = "Pan_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    byte[] bytes = Convert.FromBase64String(PancardImage);
                    File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPanCardFileName + sType), bytes);
                    sPanCardFileName = sPanCardFileName + sType;
                }
            }

            if (!string.IsNullOrEmpty(AdhaarImage))
            {
                sType = "";
                sType = AdhaarImage;

                if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                {
                    sType = ".jpg";
                }
                if (sType.Contains("data:application/pdf;base64"))
                {
                    sType = ".pdf";
                }
                if (sType.Contains("data:image/png;base64"))
                {
                    sType = ".png";
                }

                AdhaarImage = AdhaarImage.Replace("data:image/jpeg;base64,", "");
                AdhaarImage = AdhaarImage.Replace("data:application/pdf;base64,", "");
                AdhaarImage = AdhaarImage.Replace("data:image/png;base64,", "");

                if (sType == ".jpg" || sType == ".png")
                {
                    using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(AdhaarImage)))
                    {
                        using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                        {
                            sAdhaarCardFileName = "Adhaar_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sAdhaarCardFileName + sType));
                            sAdhaarCardFileName = sAdhaarCardFileName + sType;
                        }
                    }
                }
                else
                {
                    sAdhaarCardFileName = "";

                    sAdhaarCardFileName = "Adhaar_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    byte[] bytes = Convert.FromBase64String(AdhaarImage);
                    File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sAdhaarCardFileName + sType), bytes);
                    sAdhaarCardFileName = sAdhaarCardFileName + sType;
                }
            }

            //// Validation on Bank Name
            //if (jsonReg.SelectToken("bank_name").ToString().Trim() != null)
            //{
            //    // Validation on Bank Account Number
            //    if (jsonReg.SelectToken("account_num").ToString().Trim() != null)
            //    {
            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_UpdateRegBankingDetails", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;

            cmdInsert.Parameters.AddWithValue("@bank_name", jsonReg.SelectToken("bank_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@account_num", jsonReg.SelectToken("account_num").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@ifsc_code", jsonReg.SelectToken("ifsc_code").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@swift_code", jsonReg.SelectToken("swift_code").ToString().Trim());
            //new parameter added
            cmdInsert.Parameters.AddWithValue("@pan_card_num", jsonReg.SelectToken("pan_card_num").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@adhaar_card_num", jsonReg.SelectToken("adhaar_card_num").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@passport_num", jsonReg.SelectToken("passport_num").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@photoid_card_num", jsonReg.SelectToken("photoid_num").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@term_condition", jsonReg.SelectToken("term_condition").ToString().Trim());
            //
            cmdInsert.Parameters.AddWithValue("@pan_card_url", sPanCardFileName);
            cmdInsert.Parameters.AddWithValue("@adhaar_card_url", sAdhaarCardFileName);
            cmdInsert.Parameters.AddWithValue("@passport_url", sPassportFileName);
            cmdInsert.Parameters.AddWithValue("@photoid_card_url", sPhotoFileName);


            //Where Condition parameters
            cmdInsert.Parameters.AddWithValue("@email", jsonReg.SelectToken("email").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@mobile", jsonReg.SelectToken("mobile").ToString().Trim());
            //

            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            //    }
            //    else
            //    {
            //        sResponse = "{'status': 'false','message': 'Plesse enter account number'}";
            //        JObject jsonMobileobj = JObject.Parse(sResponse);
            //        JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            //    }
            //}
            //else
            //{
            //    sResponse = "{'status': 'false','message': 'Plesse enter bank name'}";
            //    JObject jsonMobileobj = JObject.Parse(sResponse);
            //    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            //}
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
        //
    }

    [WebMethod]
    public static string RegInfo()
    {
        string sResponse = "";

        try
        {
            string JSONString = string.Empty;

            #region Country
            //County
            DataTable dt_Country = new DataTable();
            dt_Country = BasicFunction.GetDetailsByDatatable("select phonecode as code,REPLACE(nicename, '''', '') as name from tbl_country");
            if (dt_Country.Rows.Count > 0)
            {

                sResponse = "{'countryList': [";
                //sResponse = sResponse + "{'code': 'Select',";
                //sResponse = sResponse + "'name': 'Country Code'},";

                for (int i = 0; i < dt_Country.Rows.Count; i++)
                {
                    //sResponse = sResponse + "{";
                    sResponse = sResponse + "{'code': '" + dt_Country.Rows[i]["code"].ToString() + "',";
                    sResponse = sResponse + "'name': '" + dt_Country.Rows[i]["name"].ToString() + "'}";

                    if (i + 1 != dt_Country.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]";
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No record found"));
            }

            #endregion Country

            #region hear about us
            sResponse = sResponse + ",'hearAboutList': [";

            DataTable dt_hearaboutus = new DataTable();
            dt_hearaboutus = BasicFunction.GetDetailsByDatatable("select id,name from tbl_HearAboutList");
            if (dt_hearaboutus.Rows.Count > 0)
            {
                for (int i = 0; i < dt_hearaboutus.Rows.Count; i++)
                {
                    //sResponse = sResponse + "{";
                    sResponse = sResponse + "{'id': '" + dt_hearaboutus.Rows[i]["id"].ToString() + "',";
                    sResponse = sResponse + "'name': '" + dt_hearaboutus.Rows[i]["name"].ToString() + "'}";

                    if (i + 1 != dt_hearaboutus.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]";
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No record found"));
            }
            #endregion hear about us

            #region interested in
            sResponse = sResponse + ",'interestedList': [";

            DataTable dt_interestedin = new DataTable();
            dt_interestedin = BasicFunction.GetDetailsByDatatable("select id,InterestValue as name,InterestImage as img from tbl_InterestedIn");
            if (dt_interestedin.Rows.Count > 0)
            {
                for (int i = 0; i < dt_interestedin.Rows.Count; i++)
                {
                    //sResponse = sResponse + "{";
                    sResponse = sResponse + "{'id': '" + dt_interestedin.Rows[i]["id"].ToString() + "',";
                    sResponse = sResponse + "'name': '" + dt_interestedin.Rows[i]["name"].ToString() + "','img': '" + "http://astaguru.bcwebwise.com/AstaGuru_api/images/" + dt_interestedin.Rows[i]["img"].ToString() + "'}";

                    if (i + 1 != dt_interestedin.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]";
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No record found"));
            }
            #endregion interested in

            #region state list
            sResponse = sResponse + ",'indianStateList': [";

            DataTable dt_state = new DataTable();
            dt_state = BasicFunction.GetDetailsByDatatable("select stateid as id,state as name from tbl_state");
            if (dt_state.Rows.Count > 0)
            {
                for (int i = 0; i < dt_state.Rows.Count; i++)
                {
                    //sResponse = sResponse + "{";
                    sResponse = sResponse + "{'id': '" + dt_state.Rows[i]["id"].ToString() + "',";
                    sResponse = sResponse + "'name': '" + dt_state.Rows[i]["name"].ToString() + "'}";

                    if (i + 1 != dt_state.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]";
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No record found"));
            }
            #endregion state list

            #region bank list
            sResponse = sResponse + ",'indianBankList': [";

            DataTable dt_bank = new DataTable();
            dt_bank = BasicFunction.GetDetailsByDatatable("select BankNameFull as fulll,BankNameShort as short from tbl_Bank");
            if (dt_bank.Rows.Count > 0)
            {
                for (int i = 0; i < dt_bank.Rows.Count; i++)
                {
                    //sResponse = sResponse + "{";
                    sResponse = sResponse + "{'full': '" + dt_bank.Rows[i]["fulll"].ToString() + "',";
                    sResponse = sResponse + "'short': '" + dt_bank.Rows[i]["short"].ToString() + "'}";

                    if (i + 1 != dt_bank.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]}";
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No record found"));
            }
            #endregion bank list

            //
            sResponse = sResponse.Replace("'", "\"");
            sResponse.Replace("{", "curleyfront").Replace("}", "curleyback");
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString));

            //
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string GetCity(string d)
    {
        string sResponse = "";

        try
        {
            string decryptedString = d;

            decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
            decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

            string replacedString = DecryptStringAES(decryptedString);
            replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

            JObject jsonCity = JObject.Parse(replacedString);

            string JSONString = string.Empty;

            #region city list
            sResponse = "{'cityList': [";

            DataTable dt_city = new DataTable();
            dt_city = BasicFunction.GetDetailsByDatatable("select city_id as code,city_vc as name  FROM tbl_city where state_id='" + jsonCity.SelectToken("state_id").ToString().Trim() + "'");
            if (dt_city.Rows.Count > 0)
            {
                for (int i = 0; i < dt_city.Rows.Count; i++)
                {
                    //sResponse = sResponse + "{";
                    sResponse = sResponse + "{'code': '" + dt_city.Rows[i]["code"].ToString() + "',";
                    sResponse = sResponse + "'name': '" + dt_city.Rows[i]["name"].ToString() + "'}";

                    if (i + 1 != dt_city.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }

                sResponse = sResponse + "]}";
            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No record found"));
            }
            #endregion city list

            //
            sResponse = sResponse.Replace("'", "\"");
            sResponse.Replace("{", "curleyfront").Replace("}", "curleyback");
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString));
            //
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string UserLogin(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonEmail = JObject.Parse(replacedString);
        //JObject jsonEmail = JObject.Parse(decryptedString);
        string sResponse = "";

        //Maintain Log Fields

        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
        string host = HttpContext.Current.Request.Headers["Host"];
        //string RequestType = HttpContext.Current.Request.ServerVariables["request_method"].ToString();
        // string server_software = HttpContext.Current.Request.ServerVariables["server_software"].ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        var datetime = HttpContext.Current.Timestamp;
        DateTime request_date = Convert.ToDateTime(datetime.Date.ToString());
        BasicFunction.Logmaintain(ipAddress, userAgent, host, url, request_date);


        try
        {
            //string m = jsonEmail.SelectToken("mobile").ToString().Trim();
            //string e = jsonEmail.SelectToken("email").ToString().Trim();
            //string l = jsonEmail.SelectToken("login_from").ToString().Trim();
            //string dc = "";
            ////string dc = jsonEmail.SelectToken("device_code").ToString().Trim();
            //string ftm = "";
            ////string ftm = jsonEmail.SelectToken("fcm_tocken_mobile").ToString().Trim();
            //string ftw = jsonEmail.SelectToken("fcm_tocken_website").ToString().Trim();

            string JSONString = string.Empty;
            DataTable dt_Email = new DataTable();
            if (jsonEmail.SelectToken("login_from").ToString().Trim() == "Website")
            {
                //
                string m = jsonEmail.SelectToken("mobile").ToString().Trim();
                string e = jsonEmail.SelectToken("email").ToString().Trim();
                string l = jsonEmail.SelectToken("login_from").ToString().Trim();
                string dc = "";
                //string dc = jsonEmail.SelectToken("device_code").ToString().Trim();
                string ftm = "";
                //string ftm = jsonEmail.SelectToken("fcm_tocken_mobile").ToString().Trim();
                string ftw = jsonEmail.SelectToken("fcm_tocken_website").ToString().Trim();
                //

                BasicFunction.Update_UserLogin(m, e, ipAddress, userAgent, host, request_date, l, dc, ftm, ftw);



                ////Store authkey in Cookies 
                //HttpCookie authkey = new HttpCookie("authkey");
                //authkey.Values["authkey_web"] = wd.authkey_web;
                //authkey.Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies.Add(authkey);
                ////

                if (string.IsNullOrEmpty(jsonEmail.SelectToken("password").ToString().Trim()))
                {

                    //send OTP Email
                    if (!string.IsNullOrEmpty(jsonEmail.SelectToken("email").ToString().Trim()))
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,email,mobile,country_code from user_details where email='" + jsonEmail.SelectToken("email").ToString().Trim() + "'");
                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string userid;
                        if (dt_Email.Rows.Count > 0)
                        {
                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            //
                            //string sOTP = RandomNumber(000000, 999999).ToString();

                            int _min = 1000;
                            int _max = 9999;
                            Random _rdm = new Random();

                            string sOTP = _rdm.Next(_min, _max).ToString();

                            string mOTP = "";
                            // save otp in table
                            BasicFunction.Save_OTP(userid, sOTP, mOTP);

                            // BasicFunction.SendOTPOnEmail(jsonEmail.SelectToken("email").ToString().Trim(), "111", sOTP);
                            sResponse = "{'status': 'true','message': 'OTP send to your Email Id','result': {'userid': '" + userid + "','otp': '" + sOTP + "'}}";
                            //sResponse = sResponse.Replace("'", "\"");
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                            // return JSONString;
                        }
                        else
                        {
                            sResponse = "{'status':'false','message':'Invalid  Email Id / Mobile'}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString));
                            //return JSONString;
                        }
                    }
                    //send OTP Mobile
                    else
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,email,mobile,country_code from user_details where mobile='" + jsonEmail.SelectToken("mobile").ToString().Trim() + "'");
                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string userid = "";
                        if (dt_Email.Rows.Count > 0)
                        {
                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            //
                            //
                            // string sOTP = RandomNumber(000000, 999999).ToString();

                            int _min = 1000;
                            int _max = 9999;
                            Random _rdm = new Random();

                            string sOTP = _rdm.Next(_min, _max).ToString();

                            string mOTP = "";
                            // save otp in table
                            BasicFunction.Save_OTP(userid, mOTP, sOTP);

                            //BasicFunction.SendOTPOnMobile(jsonEmail.SelectToken("mobile").ToString().Trim(), "111", sOTP);
                            sResponse = "{'status': 'true','message': 'OTP send to your Mobile Number','result': {'userid': '" + userid + "','otp': '" + sOTP + "'}}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                        else
                        {
                            sResponse = "{'status':'false','message':'Invalid  Email Id / Mobile'}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString));
                        }
                    }

                    //

                }
                else
                {
                    string email = "";
                    string mobile = "";
                    string country_code = "";
                    string userid = "";

                    if (!string.IsNullOrEmpty(jsonEmail.SelectToken("email").ToString().Trim()))
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select first_name,last_name,'http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/' + profilePicUrl as profilePicUrl,Id,email,mobile,country_code from user_details where email='" + jsonEmail.SelectToken("email").ToString().Trim() + "' and password='" + jsonEmail.SelectToken("password").ToString().Trim() + "' COLLATE SQL_Latin1_General_CP1_CS_AS");
                    }
                    else if (!string.IsNullOrEmpty(jsonEmail.SelectToken("mobile").ToString().Trim()))
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select first_name,last_name,'http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/' + profilePicUrl as profilePicUrl,Id,email,mobile,country_code from user_details where mobile='" + jsonEmail.SelectToken("mobile").ToString().Trim() + "' and password='" + jsonEmail.SelectToken("password").ToString().Trim() + "' COLLATE SQL_Latin1_General_CP1_CS_AS");
                    }

                    if (dt_Email.Rows.Count > 0)
                    {
                        string count = BasicFunction.Profile_Count_UserLogin(jsonEmail.SelectToken("email").ToString().Trim(), jsonEmail.SelectToken("mobile").ToString().Trim());
                        //
                        email = dt_Email.Rows[0]["email"].ToString();
                        mobile = dt_Email.Rows[0]["mobile"].ToString();
                        country_code = dt_Email.Rows[0]["country_code"].ToString();
                        userid = dt_Email.Rows[0]["Id"].ToString();

                        string first_name = dt_Email.Rows[0]["first_name"].ToString();
                        string last_name = dt_Email.Rows[0]["last_name"].ToString();
                        string profilePicUrl = dt_Email.Rows[0]["profilePicUrl"].ToString();

                        //

                        string authkey_web = RandomString(30); /*"efgh456$%^";*/
                        string authkey_mobile = "";
                        // store authkey in database against userid
                        BasicFunction.Insert_authkey(Convert.ToInt32(userid), authkey_web, authkey_mobile);
                        //
                        //sResponse = "{'status': 'true','message': 'success', 'result': { 'email': '" + dt_Email.Rows[0]["email"].ToString() + "','mobile': '" + dt_Email.Rows[0]["mobile"].ToString() + "','country_code':'" + country_code + "','userid':'" + userid + "','authkey_web':'" + authkey_web + "'}}";
                        sResponse = "{'status': 'true','message': 'success', 'result': { 'email': '" + dt_Email.Rows[0]["email"].ToString() + "','mobile': '" + dt_Email.Rows[0]["mobile"].ToString() + "','country_code':'" + country_code + "','userid':'" + userid + "','authkey_web':'" + authkey_web + "','first_name':'" + first_name + "','last_name':'" + last_name + "','profile_pic_url':'" + profilePicUrl + "','profile_completeness':'" + count + "'}}";

                        //sResponse = sResponse.Replace("'", "\"");
                        JObject jsonEmailobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                    }
                    else
                    {
                        sResponse = "{'status':'false','message':'Invalid  Email Id / Mobile'}";
                        JObject jsonEmailobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString));
                    }
                }
            }

            //if (jsonEmail.SelectToken("login_from").ToString().Trim() == "iOS" || jsonEmail.SelectToken("login_from").ToString().Trim() == "Android")
            else
            {

                //
                string m = jsonEmail.SelectToken("mobile").ToString().Trim();
                string e = jsonEmail.SelectToken("email").ToString().Trim();
                string l = jsonEmail.SelectToken("login_from").ToString().Trim();
                //string dc = "";
                string dc = jsonEmail.SelectToken("device_code").ToString().Trim();
                //string ftm = "";
                string ftm = jsonEmail.SelectToken("fcm_tocken_mobile").ToString().Trim();
                string ftw = "";
                //string ftw = jsonEmail.SelectToken("fcm_tocken_website").ToString().Trim();
                //

                BasicFunction.Update_UserLogin(m, e, ipAddress, userAgent, host, request_date, l, dc, ftm, ftw);

                //
                //string authkey_mobile = "abcd123!@#";
                string authkey_mobile = RandomString(30);
                string authkey_web = "";
                //

                ////Store authkey in Cookies 
                //HttpCookie authkey = new HttpCookie("authkey");
                //authkey.Values["authkey_mobile"] = wd.authkey_mobile;
                //authkey.Expires = DateTime.Now.AddDays(1);
                //HttpContext.Current.Response.Cookies.Add(authkey);
                ////

                if (string.IsNullOrEmpty(jsonEmail.SelectToken("password").ToString().Trim()))
                {


                    //send OTP Email
                    if (!string.IsNullOrEmpty(jsonEmail.SelectToken("email").ToString().Trim()))
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,email,mobile,country_code from user_details where email='" + jsonEmail.SelectToken("email").ToString().Trim() + "'");
                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string userid;
                        if (dt_Email.Rows.Count > 0)
                        {
                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            //
                            //string sOTP = RandomNumber(000000, 999999).ToString();

                            int _min = 1000;
                            int _max = 9999;
                            Random _rdm = new Random();

                            string sOTP = _rdm.Next(_min, _max).ToString();

                            string mOTP = "";
                            // save otp in table
                            BasicFunction.Save_OTP(userid, sOTP, mOTP);

                            // BasicFunction.SendOTPOnEmail(jsonEmail.SelectToken("email").ToString().Trim(), "111", sOTP);
                            sResponse = "{'status': 'true','message': 'OTP send to your Email Id','result': {'userid': '" + userid + "','otp': '" + sOTP + "'}}";
                            //sResponse = sResponse.Replace("'", "\"");
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                            // return JSONString;
                        }
                        else
                        {
                            sResponse = "{'status':'false','message':'Invalid  Email Id / Mobile'}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString));
                            //return JSONString;
                        }
                    }
                    //send OTP Mobile
                    else
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,email,mobile,country_code from user_details where mobile='" + jsonEmail.SelectToken("mobile").ToString().Trim() + "'");
                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string userid = "";
                        if (dt_Email.Rows.Count > 0)
                        {
                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            //
                            //
                            // string sOTP = RandomNumber(000000, 999999).ToString();

                            int _min = 1000;
                            int _max = 9999;
                            Random _rdm = new Random();

                            string sOTP = _rdm.Next(_min, _max).ToString();

                            string mOTP = "";
                            // save otp in table
                            BasicFunction.Save_OTP(userid, mOTP, sOTP);

                            //BasicFunction.SendOTPOnMobile(jsonEmail.SelectToken("mobile").ToString().Trim(), "111", sOTP);
                            sResponse = "{'status': 'true','message': 'OTP send to your Mobile Number','result': {'userid': '" + userid + "','otp': '" + sOTP + "'}}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                        else
                        {
                            sResponse = "{'status':'false','message':'Invalid  Email Id / Mobile'}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString));
                        }
                    }

                    //

                }
                else
                {
                    string count = BasicFunction.Profile_Count_UserLogin(jsonEmail.SelectToken("email").ToString().Trim(), jsonEmail.SelectToken("mobile").ToString().Trim());

                    string email = "";
                    string mobile = "";
                    string country_code = "";
                    string userid = "";

                    string first_name, last_name, profilePicUrl = "";

                    if (!string.IsNullOrEmpty(jsonEmail.SelectToken("email").ToString().Trim()))
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select first_name,last_name,'http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/' + profilePicUrl as profilePicUrl,Id,email,mobile,country_code from user_details where email='" + jsonEmail.SelectToken("email").ToString().Trim() + "' and password='" + jsonEmail.SelectToken("password").ToString().Trim() + "' COLLATE SQL_Latin1_General_CP1_CS_AS");
                    }
                    else if (!string.IsNullOrEmpty(jsonEmail.SelectToken("mobile").ToString().Trim()))
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select first_name,last_name,'http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/' + profilePicUrl as profilePicUrl,Id,email,mobile,country_code from user_details where mobile='" + jsonEmail.SelectToken("mobile").ToString().Trim() + "' and password='" + jsonEmail.SelectToken("password").ToString().Trim() + "' COLLATE SQL_Latin1_General_CP1_CS_AS");
                    }

                    if (dt_Email.Rows.Count > 0)
                    {
                        //
                        email = dt_Email.Rows[0]["email"].ToString();
                        mobile = dt_Email.Rows[0]["mobile"].ToString();
                        country_code = dt_Email.Rows[0]["country_code"].ToString();
                        userid = dt_Email.Rows[0]["Id"].ToString();


                        first_name = dt_Email.Rows[0]["first_name"].ToString();
                        last_name = dt_Email.Rows[0]["last_name"].ToString();
                        profilePicUrl = dt_Email.Rows[0]["profilePicUrl"].ToString();

                        if (!string.IsNullOrEmpty(dt_Email.Rows[0]["profilePicUrl"].ToString().Replace("http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/", "")))
                        {
                            profilePicUrl = dt_Email.Rows[0]["profilePicUrl"].ToString();
                        }

                        //
                        //
                        BasicFunction.Insert_authkey(Convert.ToInt32(userid), authkey_web, authkey_mobile);
                        //


                        sResponse = "{'status': 'true','message': 'success', 'result': { 'email': '" + dt_Email.Rows[0]["email"].ToString() + "','mobile': '" + dt_Email.Rows[0]["mobile"].ToString() + "','country_code':'" + country_code + "','userid':'" + userid + "','authkey_mobile':'" + authkey_mobile + "','first_name':'" + first_name + "','last_name':'" + last_name + "','profile_pic_url':'" + profilePicUrl + "','profile_completeness':'" + count + "'}}";
                        //sResponse = sResponse.Replace("'", "\"");
                        JObject jsonEmailobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                    }
                    else
                    {
                        sResponse = "{'status':'false','message':'Invalid  Email Id / Mobile'}";
                        JObject jsonEmailobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString));
                    }
                }
            }

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string UserLoginOTPConfirm(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            DataTable dt_Email = new DataTable();
            // Get Auth_key from table
            DataTable db_otp = new DataTable();
            db_otp = BasicFunction.GetDetailsByDatatable("select mobile_otp,email_otp from tbl_OTP where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
            string mobile_otp = "";
            string email_otp = "";
            if (db_otp.Rows.Count > 0)
            {
                mobile_otp = db_otp.Rows[0]["mobile_otp"].ToString();
                email_otp = db_otp.Rows[0]["email_otp"].ToString();

                if (mobile_otp == jsonReg.SelectToken("otp").ToString().Trim() && jsonReg.SelectToken("login_from").ToString().Trim() != string.Empty || email_otp == jsonReg.SelectToken("otp").ToString().Trim() && jsonReg.SelectToken("login_from").ToString().Trim() != string.Empty)
                {
                    if (jsonReg.SelectToken("login_from").ToString().Trim() == "Website")
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,first_name,last_name,profilePicUrl,email,mobile,country_code from user_details where Id='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string first_name = "";
                        string last_name = "";
                        string profile_pic_url = "";
                        string userid;
                        if (dt_Email.Rows.Count > 0)
                        {
                            string count = BasicFunction.Profile_Count_UserId(jsonReg.SelectToken("userid").ToString().Trim());

                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            first_name = dt_Email.Rows[0]["first_name"].ToString();
                            last_name = dt_Email.Rows[0]["last_name"].ToString();
                            profile_pic_url = dt_Email.Rows[0]["profilePicUrl"].ToString();

                            if (!string.IsNullOrEmpty(dt_Email.Rows[0]["profilePicUrl"].ToString()))
                            {
                                profile_pic_url = "";
                                profile_pic_url = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_Email.Rows[0]["profilePicUrl"].ToString();
                            }
                            else
                            {
                                profile_pic_url = "";
                            }

                            //
                            string authkey_web = RandomString(30); /*"efgh456$%^";*/
                            string authkey_mobile = "";
                            // store authkey in database against userid
                            BasicFunction.Insert_authkey(Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()), authkey_web, authkey_mobile);
                            //
                            sResponse = "{'status': 'true','message': 'success', 'result': { 'email': '" + dt_Email.Rows[0]["email"].ToString() + "','mobile': '" + dt_Email.Rows[0]["mobile"].ToString() + "','first_name':'" + first_name + "','last_name':'" + last_name + "','profile_pic_url':'" + profile_pic_url + "','country_code':'" + country_code + "','userid':'" + userid + "','authkey_web':'" + authkey_web + "','profile_completeness':'" + count + "'}}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                        else
                        {
                            sResponse = "{'status': 'false','message': 'Invalid user'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                        }
                    }
                    else
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,first_name,last_name,profilePicUrl,email,mobile,country_code from user_details where Id='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string first_name = "";
                        string last_name = "";
                        string profile_pic_url = "";
                        string userid;
                        if (dt_Email.Rows.Count > 0)
                        {
                            string count = BasicFunction.Profile_Count_UserId(jsonReg.SelectToken("userid").ToString().Trim());

                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            first_name = dt_Email.Rows[0]["first_name"].ToString();
                            last_name = dt_Email.Rows[0]["last_name"].ToString();
                            profile_pic_url = dt_Email.Rows[0]["profilePicUrl"].ToString();

                            if (!string.IsNullOrEmpty(dt_Email.Rows[0]["profilePicUrl"].ToString()))
                            {
                                profile_pic_url = "";
                                profile_pic_url = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_Email.Rows[0]["profilePicUrl"].ToString();
                            }
                            else
                            {
                                profile_pic_url = "";
                            }

                            //
                            string authkey_mobile = RandomString(30); /*"efgh456$%^";*/
                            string authkey_web = "";
                            // store authkey in database against userid
                            BasicFunction.Insert_authkey(Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()), authkey_web, authkey_mobile);
                            //
                            sResponse = "{'status': 'true','message': 'success', 'result': { 'email': '" + dt_Email.Rows[0]["email"].ToString() + "','mobile': '" + dt_Email.Rows[0]["mobile"].ToString() + "','first_name':'" + first_name + "','last_name':'" + last_name + "','profile_pic_url':'" + profile_pic_url + "','country_code':'" + country_code + "','userid':'" + userid + "','authkey_mobile':'" + authkey_mobile + "','profile_completeness':'" + count + "'}}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                        else
                        {
                            sResponse = "{'status': 'false','message': 'Invalid user'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                        }
                    }

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [WebMethod]
    public static string PreCheckLogin(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonEmail = JObject.Parse(replacedString);

        //JObject jsonEmail = JObject.Parse(decryptedString);
        string sResponse = "";


        //Maintain Log Fields

        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
        string host = HttpContext.Current.Request.Headers["Host"];
        //string RequestType = HttpContext.Current.Request.ServerVariables["request_method"].ToString();
        // string server_software = HttpContext.Current.Request.ServerVariables["server_software"].ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        var datetime = HttpContext.Current.Timestamp;
        DateTime request_date = Convert.ToDateTime(datetime.Date.ToString());
        BasicFunction.Logmaintain(ipAddress, userAgent, host, url, request_date);

        //

        try
        {
            string JSONString = string.Empty;
            DataTable dt_Email = new DataTable();

            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonEmail.SelectToken("userid").ToString().Trim() + "'");

            if (db_auth.Rows.Count > 0)
            {

                string authkey_mobile = "";
                string authkey_web = "";
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonEmail.SelectToken("login_from").ToString().Trim() == "Website")
                {

                    string l = jsonEmail.SelectToken("login_from").ToString().Trim();
                    string Id = jsonEmail.SelectToken("userid").ToString().Trim();
                    string auth_web = jsonEmail.SelectToken("authkey_web").ToString().Trim();



                    //// string authkey_web = "efgh456$%^";
                    //string authkey_web = string.Empty;
                    //authkey_web = HttpContext.Current.Request.Cookies["authkey_web"].Value;
                    if (auth_web == authkey_web)
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,first_name,last_name,profilePicUrl,email,mobile,country_code from user_details where Id='" + jsonEmail.SelectToken("userid").ToString().Trim() + "'");

                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string userid = "";
                        string first_name = "";
                        string last_name = "";

                        if (dt_Email.Rows.Count > 0)
                        {
                            //
                            string count = BasicFunction.Profile_Count(jsonEmail.SelectToken("userid").ToString().Trim());
                            //
                            //
                            string sProfPic = "";

                            if (!string.IsNullOrEmpty(dt_Email.Rows[0]["profilePicUrl"].ToString()))
                            {
                                sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_Email.Rows[0]["profilePicUrl"].ToString();
                            }
                            first_name = dt_Email.Rows[0]["first_name"].ToString();
                            last_name = dt_Email.Rows[0]["last_name"].ToString();
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            //
                            sResponse = "{'status': 'true','message': 'success','result': {'email':'" + email + "','mobile':'" + mobile + "','country_code':'" + country_code + "','userid':'" + userid + "','first_name':'" + first_name + "','last_name':'" + last_name + "','profile_pic_url':'" + sProfPic + "','profile_completeness':'" + count + "'}}";
                            //sResponse = sResponse.Replace("'", "\"");
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                            // return JSONString;
                        }
                        else
                        {
                            sResponse = "{'status':'false','message':'invalid'}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString));
                            //return JSONString;
                        }
                    }
                    else
                    {
                        sResponse = "{'status':'false','message':'invalid'}";
                        JObject jsonEmailobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString));
                        //return JSONString;  
                    }
                }
                else
                {
                    string l = jsonEmail.SelectToken("login_from").ToString().Trim();
                    string Id = jsonEmail.SelectToken("userid").ToString().Trim();
                    string auth_mobile = jsonEmail.SelectToken("authkey_mobile").ToString().Trim();
                    string device_code = jsonEmail.SelectToken("device_code").ToString().Trim();

                    //// string authkey_mobile = "abcd123!@#";
                    //string authkey_mobile = string.Empty;
                    //authkey_mobile = HttpContext.Current.Request.Cookies["authkey_mobile"].Value;

                    if (auth_mobile == authkey_mobile)
                    {
                        dt_Email = BasicFunction.GetDetailsByDatatable("select Id,first_name,last_name,profilePicUrl,email,mobile,country_code from user_details where Id='" + jsonEmail.SelectToken("userid").ToString().Trim() + /*"' and device_code='" + jsonEmail.SelectToken("device_code").ToString().Trim() + */"'");

                        string email = "";
                        string mobile = "";
                        string country_code = "";
                        string userid = "";
                        string first_name = "";
                        string last_name = "";

                        if (dt_Email.Rows.Count > 0)
                        { //
                            string count = BasicFunction.Profile_Count(jsonEmail.SelectToken("userid").ToString().Trim());
                            //
                            //
                            string sProfPic = "";

                            if (!string.IsNullOrEmpty(dt_Email.Rows[0]["profilePicUrl"].ToString()))
                            {
                                sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_Email.Rows[0]["profilePicUrl"].ToString();
                            }
                            first_name = dt_Email.Rows[0]["first_name"].ToString();
                            last_name = dt_Email.Rows[0]["last_name"].ToString();

                            //
                            email = dt_Email.Rows[0]["email"].ToString();
                            mobile = dt_Email.Rows[0]["mobile"].ToString();
                            country_code = dt_Email.Rows[0]["country_code"].ToString();
                            userid = dt_Email.Rows[0]["Id"].ToString();
                            //
                            sResponse = "{'status': 'true','message': 'success','result': {'email':'" + email + "','mobile':'" + mobile + "','country_code':'" + country_code + "','userid':'" + userid + "','first_name':'" + first_name + "','last_name':'" + last_name + "','profile-pic_url':'" + sProfPic + "','profile_completeness':'" + count + "'}}";
                            //sResponse = sResponse.Replace("'", "\"");
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                            // return JSONString;
                        }
                        else
                        {
                            sResponse = "{'status':'false','message':'invalid'}";
                            JObject jsonEmailobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString));
                            //return JSONString;
                        }

                    }
                    else
                    {
                        sResponse = "{'status':'false','message':'invalid'}";
                        JObject jsonEmailobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString));
                        //return JSONString;      
                    }


                }
            }
            else
            {
                sResponse = "{'status':'false','message':'invalid'}";
                JObject jsonEmailobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonEmailobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));
                //return JSONString;      
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string UpdateReg(string d)
    {
        string decryptedstring = d;

        decryptedstring = decryptedstring.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedstring = decryptedstring.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedstring);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(d);
        string sResponse = "";

        string JSONString = string.Empty;

        try
        {
            //Validation


            //


            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_UpdateReg", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;


            cmdInsert.Parameters.AddWithValue("@first_name", jsonReg.SelectToken("first_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@last_name", jsonReg.SelectToken("last_name").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@register_from", jsonReg.SelectToken("register_from").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@device_code", "code1");

            //Where condition parameter
            cmdInsert.Parameters.AddWithValue("@email", jsonReg.SelectToken("email").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@mobile", jsonReg.SelectToken("mobile").ToString().Trim());
            //

            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            //    JObject jsonMobileobj = JObject.Parse(sResponse);
            //    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            return sResponse;
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }


    }

    [WebMethod]
    public static string UpdateProfilePersonalDetails(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;

        //Maintain Log Fields

        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
        string host = HttpContext.Current.Request.Headers["Host"];
        //string RequestType = HttpContext.Current.Request.ServerVariables["request_method"].ToString();
        // string server_software = HttpContext.Current.Request.ServerVariables["server_software"].ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        var datetime = HttpContext.Current.Timestamp;
        DateTime request_date = Convert.ToDateTime(datetime.Date.ToString());
        BasicFunction.Logmaintain(ipAddress, userAgent, host, url, request_date);

        //

        try
        {

            //Validation

            //string authkey_web = string.Empty;
            //authkey_web = HttpContext.Current.Request.Cookies["authkey_web"].Value;
            //string authkey_mobile = string.Empty;
            //authkey_mobile = HttpContext.Current.Request.Cookies["authkey_web"].Value;

            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");

            if (db_auth.Rows.Count > 0)
            {
                string authkey_mobile = "";
                string authkey_web = "";
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                {
                    string sType = "";
                    string sPhotoFileName = "";
                    string PhotoImage = jsonReg.SelectToken("profile_pic_base64").ToString().Trim();

                    if (!string.IsNullOrEmpty(PhotoImage))
                    {
                        sType = "";
                        sType = PhotoImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PhotoImage = PhotoImage.Replace("data:image/jpeg;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:application/pdf;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PhotoImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType));
                                    sPhotoFileName = sPhotoFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPhotoFileName = "";

                            sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PhotoImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType), bytes);
                            sPhotoFileName = sPhotoFileName + sType;
                        }
                    }

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_UpdateProfilePersonalDetails", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@gender", jsonReg.SelectToken("gender").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@dob", jsonReg.SelectToken("dob").ToString().Trim());
                    //
                    // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                    //
                    cmdInsert.Parameters.AddWithValue("@hear_aboutus", jsonReg.SelectToken("hear_aboutus").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@interested_in", jsonReg.SelectToken("interested_in").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@nationality", jsonReg.SelectToken("nationality").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@register_from", jsonReg.SelectToken("register_from").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@device_code", jsonReg.SelectToken("device_code").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@first_name", jsonReg.SelectToken("first_name").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@last_name", jsonReg.SelectToken("last_name").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@nickname", jsonReg.SelectToken("nick_name").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@profile_pic_url", sPhotoFileName);
                    //cmdInsert.Parameters.AddWithValue("@password", jsonReg.SelectToken("password").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@interested_in_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());

                    // Where Condition parameters
                    cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));
                    //

                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {
                    string sType = "";
                    string sPhotoFileName = "";
                    string PhotoImage = jsonReg.SelectToken("profile_pic_base64").ToString().Trim();

                    if (!string.IsNullOrEmpty(PhotoImage))
                    {
                        sType = "";
                        sType = PhotoImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PhotoImage = PhotoImage.Replace("data:image/jpeg;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:application/pdf;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PhotoImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType));
                                    sPhotoFileName = sPhotoFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPhotoFileName = "";

                            sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PhotoImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType), bytes);
                            sPhotoFileName = sPhotoFileName + sType;
                        }
                    }

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_UpdateProfilePersonalDetails", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@gender", jsonReg.SelectToken("gender").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@dob", jsonReg.SelectToken("dob").ToString().Trim());
                    //
                    // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                    //
                    cmdInsert.Parameters.AddWithValue("@hear_aboutus", jsonReg.SelectToken("hear_aboutus").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@interested_in", jsonReg.SelectToken("interested_in").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@nationality", jsonReg.SelectToken("nationality").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@register_from", jsonReg.SelectToken("register_from").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@device_code", jsonReg.SelectToken("device_code").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@first_name", jsonReg.SelectToken("first_name").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@last_name", jsonReg.SelectToken("last_name").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@nickname", jsonReg.SelectToken("nick_name").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@profile_pic_url", sPhotoFileName);
                    //cmdInsert.Parameters.AddWithValue("@password", jsonReg.SelectToken("password").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@interested_in_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());

                    // Where Condition parameters
                    cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));
                    //

                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string UpdateMyAddress(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;

        //Maintain Log Fields

        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
        string host = HttpContext.Current.Request.Headers["Host"];
        //string RequestType = HttpContext.Current.Request.ServerVariables["request_method"].ToString();
        // string server_software = HttpContext.Current.Request.ServerVariables["server_software"].ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        var datetime = HttpContext.Current.Timestamp;
        DateTime request_date = Convert.ToDateTime(datetime.Date.ToString());
        BasicFunction.Logmaintain(ipAddress, userAgent, host, url, request_date);

        //
        try
        {

            //Validation

            //string authkey_web = string.Empty;
            //authkey_web = HttpContext.Current.Request.Cookies["authkey_web"].Value;
            //string authkey_mobile = string.Empty;
            //authkey_mobile = HttpContext.Current.Request.Cookies["authkey_web"].Value;

            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
            string authkey_mobile = "";
            string authkey_web = "";
            if (db_auth.Rows.Count > 0)
            {
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                {

                    if (jsonReg.SelectToken("address.action").ToString().Trim() == "update")
                    {
                        string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                        SqlConnection conn = new SqlConnection();
                        SqlCommand cmdInsert;

                        conn.ConnectionString = connStr;


                        conn.Open();
                        cmdInsert = new SqlCommand("sp_udpateMyAddress", conn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;


                        cmdInsert.Parameters.AddWithValue("@type", jsonReg.SelectToken("address.type").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@action", jsonReg.SelectToken("address.action").ToString().Trim());
                        //
                        // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                        //
                        cmdInsert.Parameters.AddWithValue("@your_name", jsonReg.SelectToken("address.your_name").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line1", jsonReg.SelectToken("address.add_line1").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line2", jsonReg.SelectToken("address.add_line2").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@pin_code", jsonReg.SelectToken("address.pin_code").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@country", jsonReg.SelectToken("address.country").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@state", jsonReg.SelectToken("address.state").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@city", jsonReg.SelectToken("address.city").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@location", jsonReg.SelectToken("address.location").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@gst_num", jsonReg.SelectToken("address.gst_num").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));

                        // Where Condition parameters
                        cmdInsert.Parameters.AddWithValue("@id", jsonReg.SelectToken("address.id").ToString().Trim());
                        //
                        // add parameters
                        SqlParameter outputParam = new SqlParameter();
                        outputParam.ParameterName = "@Idout";
                        outputParam.SqlDbType = SqlDbType.Int;
                        outputParam.Direction = ParameterDirection.Output;
                        cmdInsert.Parameters.Add(outputParam);
                        cmdInsert.ExecuteScalar();
                        String Id = outputParam.Value.ToString();
                        conn.Dispose();

                        //sResponse = "{'status': 'true','message': 'success','address_id':'" + Id + "'}";
                        //JObject jsonMobileobj = JObject.Parse(sResponse);
                        //JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                        //return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));


                        if (Id == "0")
                        {
                            sResponse = "{'status': 'false','message': 'UserId and Rowid Does not match.'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                        }
                        else
                        {
                            sResponse = "{'status': 'true','message': 'success','address_id':'" + Id + "'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }

                    }
                    else if (jsonReg.SelectToken("address.action").ToString().Trim() == "add")
                    {
                        string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                        SqlConnection conn = new SqlConnection();
                        SqlCommand cmdInsert;
                        conn.ConnectionString = connStr;

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }

                        conn.Open();
                        cmdInsert = new SqlCommand("sp_addMyAddress", conn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;


                        cmdInsert.Parameters.AddWithValue("@type", jsonReg.SelectToken("address.type").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@action", jsonReg.SelectToken("address.action").ToString().Trim());
                        //
                        // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                        //
                        cmdInsert.Parameters.AddWithValue("@Id", jsonReg.SelectToken("address.id").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@your_name", jsonReg.SelectToken("address.your_name").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line1", jsonReg.SelectToken("address.add_line1").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line2", jsonReg.SelectToken("address.add_line2").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@pin_code", jsonReg.SelectToken("address.pin_code").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@country", jsonReg.SelectToken("address.country").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@state", jsonReg.SelectToken("address.state").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@city", jsonReg.SelectToken("address.city").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@location", jsonReg.SelectToken("address.location").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@gst_num", jsonReg.SelectToken("address.gst_num").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));

                        // Where Condition parameters
                        //cmdInsert.Parameters.AddWithValue("@id",jsonReg.SelectToken("address.id").ToString().Trim());
                        //
                        // add parameters
                        SqlParameter outputParam = new SqlParameter();
                        outputParam.ParameterName = "@Idout";
                        outputParam.SqlDbType = SqlDbType.Int;
                        outputParam.Direction = ParameterDirection.Output;
                        cmdInsert.Parameters.Add(outputParam);
                        cmdInsert.ExecuteScalar();
                        String Id = outputParam.Value.ToString();
                        conn.Dispose();

                        //sResponse = "{'status': 'true','message': 'success','address_id':'" + Id + "'}";
                        //JObject jsonMobileobj = JObject.Parse(sResponse);
                        //JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                        //return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                        if (Id == "0")
                        {
                            sResponse = "{'status': 'false','message': 'Postal address already exists.chage type to update'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                        else
                        {
                            sResponse = "{'status': 'true','message': 'success','address_id':'" + Id + "'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                    }
                    else
                    {
                        sResponse = "{'status': 'false','message': 'Invalid user'}";
                        JObject jsonMobileobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                    }
                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {

                    if (jsonReg.SelectToken("address.action").ToString().Trim() == "update")
                    {
                        string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                        SqlConnection conn = new SqlConnection();
                        SqlCommand cmdInsert;
                        conn.ConnectionString = connStr;

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }

                        conn.Open();
                        cmdInsert = new SqlCommand("sp_udpateMyAddress", conn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;


                        cmdInsert.Parameters.AddWithValue("@type", jsonReg.SelectToken("address.type").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@action", jsonReg.SelectToken("address.action").ToString().Trim());
                        //
                        // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                        //
                        cmdInsert.Parameters.AddWithValue("@your_name", jsonReg.SelectToken("address.your_name").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line1", jsonReg.SelectToken("address.add_line1").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line2", jsonReg.SelectToken("address.add_line2").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@pin_code", jsonReg.SelectToken("address.pin_code").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@country", jsonReg.SelectToken("address.country").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@state", jsonReg.SelectToken("address.state").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@city", jsonReg.SelectToken("address.city").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@location", jsonReg.SelectToken("address.location").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@gst_num", jsonReg.SelectToken("address.gst_num").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));

                        // Where Condition parameters
                        cmdInsert.Parameters.AddWithValue("@id", jsonReg.SelectToken("address.id").ToString().Trim());
                        //
                        // add parameters
                        SqlParameter outputParam = new SqlParameter();
                        outputParam.ParameterName = "@Idout";
                        outputParam.SqlDbType = SqlDbType.Int;
                        outputParam.Direction = ParameterDirection.Output;
                        cmdInsert.Parameters.Add(outputParam);
                        cmdInsert.ExecuteScalar();
                        String Id = outputParam.Value.ToString();
                        conn.Dispose();

                        if (Id == "0")
                        {
                            sResponse = "{'status': 'false','message': 'UserId and Rowid Does not match.'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                        }
                        else
                        {
                            sResponse = "{'status': 'true','message': 'success','address_id':'" + Id + "'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }

                    }
                    else if (jsonReg.SelectToken("address.action").ToString().Trim() == "add")
                    {
                        string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                        SqlConnection conn = new SqlConnection();
                        SqlCommand cmdInsert;
                        conn.ConnectionString = connStr;

                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }

                        conn.Open();
                        cmdInsert = new SqlCommand("sp_addMyAddress", conn);
                        cmdInsert.CommandType = CommandType.StoredProcedure;


                        cmdInsert.Parameters.AddWithValue("@type", jsonReg.SelectToken("address.type").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@action", jsonReg.SelectToken("address.action").ToString().Trim());
                        //
                        // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                        //
                        cmdInsert.Parameters.AddWithValue("@Id", jsonReg.SelectToken("address.id").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@your_name", jsonReg.SelectToken("address.your_name").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line1", jsonReg.SelectToken("address.add_line1").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@add_line2", jsonReg.SelectToken("address.add_line2").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@pin_code", jsonReg.SelectToken("address.pin_code").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@country", jsonReg.SelectToken("address.country").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@state", jsonReg.SelectToken("address.state").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@city", jsonReg.SelectToken("address.city").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@location", jsonReg.SelectToken("address.location").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@gst_num", jsonReg.SelectToken("address.gst_num").ToString().Trim());
                        cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));

                        // Where Condition parameters
                        //cmdInsert.Parameters.AddWithValue("@id",jsonReg.SelectToken("address.id").ToString().Trim());
                        //
                        // add parameters
                        SqlParameter outputParam = new SqlParameter();
                        outputParam.ParameterName = "@Idout";
                        outputParam.SqlDbType = SqlDbType.Int;
                        outputParam.Direction = ParameterDirection.Output;
                        cmdInsert.Parameters.Add(outputParam);
                        cmdInsert.ExecuteScalar();
                        String Id = outputParam.Value.ToString();
                        conn.Dispose();

                        if (Id == "0")
                        {
                            sResponse = "{'status': 'false','message': 'Postal address already exists.chage type to update'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                        else
                        {
                            sResponse = "{'status': 'true','message': 'success','address_id':'" + Id + "'}";
                            JObject jsonMobileobj = JObject.Parse(sResponse);
                            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                        }
                    }
                    else
                    {
                        sResponse = "{'status': 'false','message': 'Invalid user'}";
                        JObject jsonMobileobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                    }
                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }


    }

    [WebMethod]

    public static string deleteMyAddress(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
            string authkey_mobile = "";
            string authkey_web = "";
            if (db_auth.Rows.Count > 0)
            {
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                {
                    int id = Convert.ToInt32(jsonReg.SelectToken("address_id").ToString().Trim());

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_deleteMyAddress", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@id", id);
                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {
                    int id = Convert.ToInt32(jsonReg.SelectToken("address_id").ToString().Trim());

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_deleteMyAddress", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@id", id);
                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }

                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }

            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }


    #region

    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static byte[] EncryptStringAES(string plainText)
    {
        //var keybytes = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");
        //var iv = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");

        var keybytes = Encoding.UTF8.GetBytes("codastagcoduruco");
        var iv = Encoding.UTF8.GetBytes("codastagcoduruco");

        var plainttext = plainText;
        var encriptedFromJavascript = EncryptStringToBytes(plainttext, keybytes, iv);
        return encriptedFromJavascript;
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
        {
            throw new ArgumentNullException("plainText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        byte[] encrypted;
        // Create a RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static string DecryptStringAES(string cipherText)
    {
        //var keybytes = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");
        //var iv = Encoding.UTF8.GetBytes("d9t65s8uy2dkw4eh");

        var keybytes = Encoding.UTF8.GetBytes("codastagcoduruco");
        var iv = Encoding.UTF8.GetBytes("codastagcoduruco");

        var encrypted = Convert.FromBase64String(cipherText);
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
        return string.Format(decriptedFromJavascript);
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
            try
            {
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {

                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();

                        }

                    }
                }
            }
            catch
            {
                plaintext = "keyError";
            }
        }

        return plaintext;
    }
    #endregion

    [WebMethod]
    public static string setDefaultMyAddress(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
            string authkey_mobile = "";
            string authkey_web = "";
            if (db_auth.Rows.Count > 0)
            {
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //
                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                {
                    int id = Convert.ToInt32(jsonReg.SelectToken("address_id").ToString().Trim());

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_setDefaultMyAddress", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@address_id", id);
                    cmdInsert.Parameters.AddWithValue("@userid", jsonReg.SelectToken("userid").ToString().Trim());


                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {
                    int id = Convert.ToInt32(jsonReg.SelectToken("address_id").ToString().Trim());

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_setDefaultMyAddress", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@address_id", id);
                    cmdInsert.Parameters.AddWithValue("@userid", jsonReg.SelectToken("userid").ToString().Trim());


                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string UpdateBankingDetails(string d)
    {
        string sType1 = "";

        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;

        //Maintain Log Fields

        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
        string host = HttpContext.Current.Request.Headers["Host"];
        //string RequestType = HttpContext.Current.Request.ServerVariables["request_method"].ToString();
        // string server_software = HttpContext.Current.Request.ServerVariables["server_software"].ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        var datetime = HttpContext.Current.Timestamp;
        DateTime request_date = Convert.ToDateTime(datetime.Date.ToString());
        BasicFunction.Logmaintain(ipAddress, userAgent, host, url, request_date);

        //
        try
        {

            //Validation

            //string authkey_web = string.Empty;
            //authkey_web = HttpContext.Current.Request.Cookies["authkey_web"].Value;
            //string authkey_mobile = string.Empty;
            //authkey_mobile = HttpContext.Current.Request.Cookies["authkey_web"].Value;

            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");

            if (db_auth.Rows.Count > 0)
            {
                string authkey_mobile = "";
                string authkey_web = "";
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                {
                    string sType = "";
                    string sPanFileName = "";
                    string sAdharFileName = "";
                    string sPassportFileName = "";
                    string sPhotoidFileName = "";
                    string PanImage = jsonReg.SelectToken("pan_card_base64").ToString().Trim();
                    string AdharImage = jsonReg.SelectToken("adhaar_card_base64").ToString().Trim();
                    string PassportImage = jsonReg.SelectToken("passport_base64").ToString().Trim();
                    string PhotoidImage = jsonReg.SelectToken("photoid_base64").ToString().Trim();


                    if (!string.IsNullOrEmpty(PanImage))
                    {
                        sType = "";
                        sType = PanImage;
                        sType1 = PanImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        else if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        else if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PanImage = PanImage.Replace("data:image/jpeg;base64,", "");
                        PanImage = PanImage.Replace("data:application/pdf;base64,", "");
                        PanImage = PanImage.Replace("data:image/png;base64,", "");

                        sType1 = sType;

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msPan = new MemoryStream(Convert.FromBase64String(PanImage)))
                            {
                                using (Bitmap bmpPan = new Bitmap(msPan))
                                {
                                    sPanFileName = "Pan_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpPan.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPanFileName + sType));
                                    sPanFileName = sPanFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPanFileName = "";

                            sPanFileName = "Pan_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PanImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPanFileName + sType), bytes);
                            sPanFileName = sPanFileName + sType;
                        }
                    }

                    if (!string.IsNullOrEmpty(AdharImage))
                    {
                        sType = "";
                        sType = AdharImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        AdharImage = AdharImage.Replace("data:image/jpeg;base64,", "");
                        AdharImage = AdharImage.Replace("data:application/pdf;base64,", "");
                        AdharImage = AdharImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(AdharImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sAdharFileName = "Adhaar_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sAdharFileName + sType));
                                    sAdharFileName = sAdharFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sAdharFileName = "";

                            sAdharFileName = "Adhaar_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(AdharImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sAdharFileName + sType), bytes);
                            sAdharFileName = sAdharFileName + sType;
                        }
                    }

                    if (!string.IsNullOrEmpty(PassportImage))
                    {
                        sType = "";
                        sType = PassportImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PassportImage = PassportImage.Replace("data:image/jpeg;base64,", "");
                        PassportImage = PassportImage.Replace("data:application/pdf;base64,", "");
                        PassportImage = PassportImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PassportImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPassportFileName = "Passport_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPassportFileName + sType));
                                    sPassportFileName = sPassportFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPassportFileName = "";

                            sPassportFileName = "Passport_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PassportImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPassportFileName + sType), bytes);
                            sPassportFileName = sPassportFileName + sType;
                        }
                    }

                    if (!string.IsNullOrEmpty(PhotoidImage))
                    {
                        sType = "";
                        sType = PhotoidImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PhotoidImage = PhotoidImage.Replace("data:image/jpeg;base64,", "");
                        PhotoidImage = PhotoidImage.Replace("data:application/pdf;base64,", "");
                        PhotoidImage = PhotoidImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PhotoidImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPhotoidFileName = "Photoid_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoidFileName + sType));
                                    sPhotoidFileName = sPhotoidFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPhotoidFileName = "";

                            sPhotoidFileName = "Photoid_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PhotoidImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoidFileName + sType), bytes);
                            sPhotoidFileName = sPhotoidFileName + sType;
                        }
                    }

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_UpdateBankingDetails", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@bank_name", jsonReg.SelectToken("bank_name").ToString().Trim());
                    //
                    // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                    //
                    cmdInsert.Parameters.AddWithValue("@account_num", jsonReg.SelectToken("account_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@ifsc_code", jsonReg.SelectToken("ifsc_code").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@swift_code", jsonReg.SelectToken("swift_code").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@pan_card_num", jsonReg.SelectToken("pan_card_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@adhaar_card_num", jsonReg.SelectToken("adhaar_card_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@passport_num", jsonReg.SelectToken("passport_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@photoid_num", jsonReg.SelectToken("photoid_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@term_condition", jsonReg.SelectToken("term_condition").ToString().Trim());

                    cmdInsert.Parameters.AddWithValue("@passport_url", sPassportFileName);
                    cmdInsert.Parameters.AddWithValue("@pan_card_url", sPanFileName);
                    cmdInsert.Parameters.AddWithValue("@adhaar_card_url", sAdharFileName);
                    cmdInsert.Parameters.AddWithValue("@photoid_url", sPhotoidFileName);

                    // Where Condition parameters
                    cmdInsert.Parameters.AddWithValue("@userid", jsonReg.SelectToken("userid").ToString().Trim());

                    //

                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {
                    string sType = "";
                    string sPanFileName = "";
                    string sAdharFileName = "";
                    string sPassportFileName = "";
                    string sPhotoidFileName = "";
                    string PanImage = jsonReg.SelectToken("pan_card_base64").ToString().Trim();
                    string AdharImage = jsonReg.SelectToken("adhaar_card_base64").ToString().Trim();
                    string PassportImage = jsonReg.SelectToken("passport_base64").ToString().Trim();
                    string PhotoidImage = jsonReg.SelectToken("photoid_base64").ToString().Trim();


                    if (!string.IsNullOrEmpty(PanImage))
                    {
                        sType = "";
                        sType = PanImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PanImage = PanImage.Replace("data:image/jpeg;base64,", "");
                        PanImage = PanImage.Replace("data:application/pdf;base64,", "");
                        PanImage = PanImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PanImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPanFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPanFileName + sType));
                                    sPanFileName = sPanFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPanFileName = "";

                            sPanFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PanImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPanFileName + sType), bytes);
                            sPanFileName = sPanFileName + sType;
                        }
                    }

                    if (!string.IsNullOrEmpty(AdharImage))
                    {
                        sType = "";
                        sType = AdharImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        AdharImage = AdharImage.Replace("data:image/jpeg;base64,", "");
                        AdharImage = AdharImage.Replace("data:application/pdf;base64,", "");
                        AdharImage = AdharImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PanImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sAdharFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sAdharFileName + sType));
                                    sAdharFileName = sAdharFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sAdharFileName = "";

                            sAdharFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(AdharImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sAdharFileName + sType), bytes);
                            sAdharFileName = sAdharFileName + sType;
                        }
                    }

                    if (!string.IsNullOrEmpty(PassportImage))
                    {
                        sType = "";
                        sType = PassportImage;

                        if (sType.Contains("jpeg") || sType.Contains("jpg"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("pdf"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("png"))
                        {
                            sType = ".png";
                        }

                        PassportImage = PassportImage.Replace("data:image/jpeg;base64,", "");
                        PassportImage = PassportImage.Replace("data:application/pdf;base64,", "");
                        PassportImage = PassportImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PassportImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPassportFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPassportFileName + sType));
                                    sPassportFileName = sPassportFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPassportFileName = "";

                            sPassportFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PassportImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPassportFileName + sType), bytes);
                            sPassportFileName = sPassportFileName + sType;
                        }
                    }

                    if (!string.IsNullOrEmpty(PhotoidImage))
                    {
                        sType = "";
                        sType = PhotoidImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PhotoidImage = PhotoidImage.Replace("data:image/jpeg;base64,", "");
                        PhotoidImage = PhotoidImage.Replace("data:application/pdf;base64,", "");
                        PhotoidImage = PhotoidImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PanImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPhotoidFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoidFileName + sType));
                                    sPhotoidFileName = sPhotoidFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPhotoidFileName = "";

                            sPhotoidFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PhotoidImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoidFileName + sType), bytes);
                            sPhotoidFileName = sPhotoidFileName + sType;
                        }
                    }

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_UpdateBankingDetails", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@bank_name", jsonReg.SelectToken("bank_name").ToString().Trim());
                    //
                    // cmdInsert.Parameters.AddWithValue("@interested_inhttps://docs.google.com/document/u/0/?authuser=0&usp=docs_web_bidding", jsonReg.SelectToken("interested_in_bidding").ToString().Trim());
                    //
                    cmdInsert.Parameters.AddWithValue("@account_num", jsonReg.SelectToken("account_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@ifsc_code", jsonReg.SelectToken("ifsc_code").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@swift_code", jsonReg.SelectToken("swift_code").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@pan_card_num", jsonReg.SelectToken("pan_card_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@adhaar_card_num", jsonReg.SelectToken("adhaar_card_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@passport_num", jsonReg.SelectToken("passport_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@photoid_num", jsonReg.SelectToken("photoid_num").ToString().Trim());
                    cmdInsert.Parameters.AddWithValue("@term_condition", jsonReg.SelectToken("term_condition").ToString().Trim());

                    cmdInsert.Parameters.AddWithValue("@passport_url", sPassportFileName);
                    cmdInsert.Parameters.AddWithValue("@pan_card_url", sPanFileName);
                    cmdInsert.Parameters.AddWithValue("@adhaar_card_url", sAdharFileName);
                    cmdInsert.Parameters.AddWithValue("@photoid_url", sPhotoidFileName);

                    // Where Condition parameters
                    cmdInsert.Parameters.AddWithValue("@userid", jsonReg.SelectToken("userid").ToString().Trim());

                    //

                    cmdInsert.ExecuteNonQuery();

                    sResponse = "{'status': 'true','message': 'success'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {

            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string GetUserAllDetails(string d)
    {

        webservice_Default wd = new webservice_Default();
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {

            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
            string authkey_mobile = "";
            string authkey_web = "";
            if (db_auth.Rows.Count > 0)
            {
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                //jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web)
                {

                    List<string> address = new List<string>();

                    DataTable dt_add = new DataTable();
                    dt_add = BasicFunction.GetDetailsByDatatable("select * from user_address_details where is_deleted='0' and UserDetailsId='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
                    if (dt_add.Rows.Count > 0)
                    {
                        wd.isbillingaddress = dt_add.Rows[0]["isbillingaddress"].ToString();
                        for (int i = 0; i < dt_add.Rows.Count; i++)
                        {
                            string postal_add = "{'type':'" + dt_add.Rows[i]["type"].ToString() + "','id':'" + dt_add.Rows[i]["id"].ToString() + "','your_name':'" + dt_add.Rows[i]["your_name"].ToString() + "','add_line1':'" + dt_add.Rows[i]["billing_add_line1"].ToString() + "','add_line2':'" + dt_add.Rows[i]["billing_add_line2"].ToString() + "','pin_code':'" + dt_add.Rows[i]["billing_pin_code"].ToString() + "','country':'" + dt_add.Rows[i]["billing_country"].ToString() + "','state':'" + dt_add.Rows[i]["billing_state"].ToString() + "','city':'" + dt_add.Rows[i]["billing_city"].ToString() + "','location':'" + dt_add.Rows[i]["billing_location"].ToString() + "','gst_num':'" + dt_add.Rows[i]["gst_num"].ToString() + "','default':'" + dt_add.Rows[i]["is_active"].ToString() + "'}";
                            address.Add(postal_add);
                        }

                    }
                    else
                    {
                        string postal_add = "{}";
                        address.Add(postal_add);
                    }

                    DataTable dt_basic = new DataTable();
                    dt_basic = BasicFunction.GetDetailsByDatatable("select * from user_details where Id='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
                    string basic_and_banking = "";

                    string count = BasicFunction.Profile_Count(jsonReg.SelectToken("userid").ToString().Trim());

                    if (dt_basic.Rows.Count > 0)
                    {
                        string sProfPic = "";

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["profilePicUrl"].ToString()))
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["profilePicUrl"].ToString();
                        }
                        else
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/ProfileImage.png";
                        }

                        string sDate = "";

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["dob"].ToString()))
                        {
                            sDate = Convert.ToDateTime(dt_basic.Rows[0]["dob"].ToString()).ToString("yyyy-MM-dd");
                        }

                        if (sDate == "1900-01-01")
                        {
                            sDate = "";
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["passportURL"].ToString()))
                        {
                            dt_basic.Rows[0]["passportURL"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["passportURL"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["photoIdURL"].ToString()))
                        {
                            dt_basic.Rows[0]["photoIdURL"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["photoIdURL"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["aadhar_card"].ToString()))
                        {
                            dt_basic.Rows[0]["aadhar_card"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["aadhar_card"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["pan_card"].ToString()))
                        {
                            dt_basic.Rows[0]["pan_card"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["pan_card"].ToString().Trim();
                        }

                        if (dt_add.Rows.Count == 1)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "]}}";
                        }
                        else if (dt_add.Rows.Count == 2)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "," + address[1] + "]}}";
                        }
                        else if (dt_add.Rows.Count == 3)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "," + address[1] + "," + address[2] + "]}}";
                        }
                        else if (dt_add.Rows.Count == 4)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "," + address[1] + "," + address[2] + "," + address[3] + "]}}";
                        }
                        else
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[]}}";
                        }
                    }

                    sResponse = "{'status': 'true','message': 'success','result':" + basic_and_banking + "}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));


                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {

                    List<string> address = new List<string>();

                    DataTable dt_add = new DataTable();
                    dt_add = BasicFunction.GetDetailsByDatatable("select * from user_address_details where is_deleted='0' and UserDetailsId='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
                    if (dt_add.Rows.Count > 0)
                    {
                        wd.isbillingaddress = dt_add.Rows[0]["isbillingaddress"].ToString();
                        for (int i = 0; i < dt_add.Rows.Count; i++)
                        {
                            string postal_add = "{'type':'" + dt_add.Rows[i]["type"].ToString() + "','id':'" + dt_add.Rows[i]["id"].ToString() + "','your_name':'" + dt_add.Rows[i]["your_name"].ToString() + "','add_line1':'" + dt_add.Rows[i]["billing_add_line1"].ToString() + "','add_line2':'" + dt_add.Rows[i]["billing_add_line2"].ToString() + "','pin_code':'" + dt_add.Rows[i]["billing_pin_code"].ToString() + "','country':'" + dt_add.Rows[i]["billing_country"].ToString() + "','state':'" + dt_add.Rows[i]["billing_state"].ToString() + "','city':'" + dt_add.Rows[i]["billing_city"].ToString() + "','location':'" + dt_add.Rows[i]["billing_location"].ToString() + "','gst_num':'" + dt_add.Rows[i]["gst_num"].ToString() + "','default':'" + dt_add.Rows[i]["is_active"].ToString() + "'}";
                            address.Add(postal_add);
                        }

                    }
                    else
                    {
                        string postal_add = "{}";
                        address.Add(postal_add);
                    }

                    DataTable dt_basic = new DataTable();
                    dt_basic = BasicFunction.GetDetailsByDatatable("select * from user_details where Id='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");
                    string basic_and_banking = "";

                    string count = BasicFunction.Profile_Count(jsonReg.SelectToken("userid").ToString().Trim());

                    if (dt_basic.Rows.Count > 0)
                    {
                        string sProfPic = "";

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["profilePicUrl"].ToString()))
                        {
                            sProfPic = "";
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["profilePicUrl"].ToString();
                        }
                        else
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/ProfileImage.png";
                        }

                        string sDate = "";

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["dob"].ToString()))
                        {
                            sDate = Convert.ToDateTime(dt_basic.Rows[0]["dob"].ToString()).ToString("yyyy-MM-dd");
                        }

                        if (sDate == "1900-01-01")
                        {
                            sDate = "";
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["passportURL"].ToString()))
                        {
                            dt_basic.Rows[0]["passportURL"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["passportURL"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["photoIdURL"].ToString()))
                        {
                            dt_basic.Rows[0]["photoIdURL"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["photoIdURL"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["aadhar_card"].ToString()))
                        {
                            dt_basic.Rows[0]["aadhar_card"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["aadhar_card"].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["pan_card"].ToString()))
                        {
                            dt_basic.Rows[0]["pan_card"] = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["pan_card"].ToString().Trim();
                        }

                        if (dt_add.Rows.Count == 1)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "]}}";
                        }
                        else if (dt_add.Rows.Count == 2)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "," + address[1] + "]}}";
                        }
                        else if (dt_add.Rows.Count == 3)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "," + address[1] + "," + address[2] + "]}}";
                        }
                        else if (dt_add.Rows.Count == 4)
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[" + address[0] + "," + address[1] + "," + address[2] + "," + address[3] + "]}}";
                        }
                        else
                        {
                            basic_and_banking = "{'profile':{'basicDetails':{'email':'" + dt_basic.Rows[0]["email"].ToString() + "','country_code':'" + dt_basic.Rows[0]["country_code"].ToString() + "','mobile':'" + dt_basic.Rows[0]["mobile"].ToString() + "','userid':'" + dt_basic.Rows[0]["Id"].ToString() + "','first_name':'" + dt_basic.Rows[0]["first_name"].ToString() + "','last_name':'" + dt_basic.Rows[0]["last_name"].ToString() + "','nick_name':'" + dt_basic.Rows[0]["nick_name"].ToString() + "','profile_pic_url':'" + sProfPic + "','gender':'" + dt_basic.Rows[0]["gender"].ToString() + "','dob':'" + sDate + "','interested_in_bidding':'" + dt_basic.Rows[0]["interested_in_bidding"].ToString() + "','hear_aboutus':'" + dt_basic.Rows[0]["hear_aboutus"].ToString() + "','interested_in':'" + dt_basic.Rows[0]["interested_in"].ToString() + "','nationality':'" + dt_basic.Rows[0]["nationality"].ToString() + "'},'bankDetails':{'bank_name':'" + dt_basic.Rows[0]["bank_name"].ToString() + "','account_num':'" + dt_basic.Rows[0]["account_num"].ToString() + "','ifsc_code':'" + dt_basic.Rows[0]["ifsc_code"].ToString() + "','swift_code':'" + dt_basic.Rows[0]["swift_code"].ToString() + "','pan_card_num':'" + dt_basic.Rows[0]["pan_card_num"].ToString() + "','pan_card_url':'" + dt_basic.Rows[0]["pan_card"].ToString() + "','adhaar_card_num':'" + dt_basic.Rows[0]["adhaar_card_num"].ToString() + "','adhaar_card_url':'" + dt_basic.Rows[0]["aadhar_card"].ToString() + "','passport_num':'" + dt_basic.Rows[0]["passport_num"].ToString() + "','passport_url':'" + dt_basic.Rows[0]["passportURL"].ToString() + "','photoid_num':'" + dt_basic.Rows[0]["photoid_card_num"].ToString() + "','photoid_url':'" + dt_basic.Rows[0]["photoIdURL"].ToString() + "','term_condition':'" + dt_basic.Rows[0]["term_condition"].ToString() + "'},'isbillingaddress':'" + wd.isbillingaddress + "','profile_completeness':'" + count + "','address':[]}}";
                        }
                    }

                    sResponse = "{'status': 'true','message': 'success','result':" + basic_and_banking + "}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));


                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string ChangePassword(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());

            if (result)
            {
                DataTable dt_add = new DataTable();
                int id = Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim());
                string new_password = jsonReg.SelectToken("new_password").ToString().Trim();
                dt_add = BasicFunction.GetDetailsByDatatable("select password from user_details where Id='" + id + "'");
                string pass = string.Empty;

                if (dt_add.Rows.Count > 0)
                {
                    pass = dt_add.Rows[0]["password"].ToString();

                    if (pass == jsonReg.SelectToken("current_password").ToString().Trim() && !string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(new_password))
                    {
                        BasicFunction.change_password(id, new_password);

                        sResponse = "{'status': 'true','message': 'success'}";
                        JObject jsonMobileobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                    }
                    else
                    {
                        sResponse = "{'status': 'false','message': 'Old Password does not match.'}";
                        JObject jsonMobileobj = JObject.Parse(sResponse);
                        JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                    }
                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user Authentication.'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {

                sResponse = "{'status': 'false','message': 'Invalid user3'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string UpdateProfilePic(string d)
    {
        DataTable dt_basic;

        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;

        //Maintain Log Fields

        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
        string host = HttpContext.Current.Request.Headers["Host"];
        //string RequestType = HttpContext.Current.Request.ServerVariables["request_method"].ToString();
        // string server_software = HttpContext.Current.Request.ServerVariables["server_software"].ToString();
        string url = HttpContext.Current.Request.Url.ToString();
        var datetime = HttpContext.Current.Timestamp;
        DateTime request_date = Convert.ToDateTime(datetime.Date.ToString());
        BasicFunction.Logmaintain(ipAddress, userAgent, host, url, request_date);

        //

        try
        {

            //Validation

            //string authkey_web = string.Empty;
            //authkey_web = HttpContext.Current.Request.Cookies["authkey_web"].Value;
            //string authkey_mobile = string.Empty;
            //authkey_mobile = HttpContext.Current.Request.Cookies["authkey_web"].Value;

            // Get Auth_key from table
            DataTable db_auth = new DataTable();
            db_auth = BasicFunction.GetDetailsByDatatable("select authkey_mobile,authkey_web from tbl_authkey where userid='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");

            if (db_auth.Rows.Count > 0)
            {
                string authkey_mobile = "";
                string authkey_web = "";
                authkey_mobile = db_auth.Rows[0]["authkey_mobile"].ToString();
                authkey_web = db_auth.Rows[0]["authkey_web"].ToString();
                //

                if (jsonReg.SelectToken("authkey_web").ToString().Trim() == authkey_web && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_web").ToString().Trim()))
                {
                    string sType = "";
                    string sPhotoFileName = "";
                    string PhotoImage = jsonReg.SelectToken("profile_pic_base64").ToString().Trim();

                    if (!string.IsNullOrEmpty(PhotoImage))
                    {
                        sType = "";
                        sType = PhotoImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PhotoImage = PhotoImage.Replace("data:image/jpeg;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:application/pdf;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PhotoImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType));
                                    sPhotoFileName = sPhotoFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPhotoFileName = "";

                            sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PhotoImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType), bytes);
                            sPhotoFileName = sPhotoFileName + sType;
                        }
                    }

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_UpdateProfilePic", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;


                    cmdInsert.Parameters.AddWithValue("@profile_pic_url", sPhotoFileName);

                    // Where Condition parameters
                    cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));
                    //

                    cmdInsert.ExecuteNonQuery();

                    dt_basic = new DataTable();
                    dt_basic = BasicFunction.GetDetailsByDatatable("select * from user_details where Id='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");

                    string sProfPic = "";
                    if (dt_basic.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["profilePicUrl"].ToString()))
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["profilePicUrl"].ToString();
                        }
                        else
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/ProfileImage.png";
                        }
                    }
                    else
                    {
                        sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/ProfileImage.png";
                    }

                    sResponse = "{'status': 'true','message': 'success','profile_pic_url':'" + sProfPic + "'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else if (jsonReg.SelectToken("authkey_mobile").ToString().Trim() == authkey_mobile && !string.IsNullOrEmpty(jsonReg.SelectToken("authkey_mobile").ToString().Trim()))
                {
                    string sType = "";
                    string sPhotoFileName = "";
                    string PhotoImage = jsonReg.SelectToken("profile_pic_base64").ToString().Trim();

                    if (!string.IsNullOrEmpty(PhotoImage))
                    {
                        sType = "";
                        sType = PhotoImage;

                        if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                        {
                            sType = ".jpg";
                        }
                        if (sType.Contains("data:application/pdf;base64"))
                        {
                            sType = ".pdf";
                        }
                        if (sType.Contains("data:image/png;base64"))
                        {
                            sType = ".png";
                        }

                        PhotoImage = PhotoImage.Replace("data:image/jpeg;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:application/pdf;base64,", "");
                        PhotoImage = PhotoImage.Replace("data:image/png;base64,", "");

                        if (sType == ".jpg" || sType == ".png")
                        {
                            using (MemoryStream msAdhaar = new MemoryStream(Convert.FromBase64String(PhotoImage)))
                            {
                                using (Bitmap bmpAdhaar = new Bitmap(msAdhaar))
                                {
                                    sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                                    bmpAdhaar.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType));
                                    sPhotoFileName = sPhotoFileName + sType;
                                }
                            }
                        }
                        else
                        {
                            sPhotoFileName = "";

                            sPhotoFileName = Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                            byte[] bytes = Convert.FromBase64String(PhotoImage);
                            File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sPhotoFileName + sType), bytes);
                            sPhotoFileName = sPhotoFileName + sType;
                        }
                    }

                    string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                    SqlConnection conn = new SqlConnection();
                    SqlCommand cmdInsert;
                    conn.ConnectionString = connStr;

                    conn.Open();
                    cmdInsert = new SqlCommand("sp_UpdateProfilePic", conn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;

                    cmdInsert.Parameters.AddWithValue("@profile_pic_url", sPhotoFileName);

                    // Where Condition parameters
                    cmdInsert.Parameters.AddWithValue("@userid", Convert.ToInt32(jsonReg.SelectToken("userid").ToString().Trim()));
                    //

                    cmdInsert.ExecuteNonQuery();

                    dt_basic = new DataTable();
                    dt_basic = BasicFunction.GetDetailsByDatatable("select * from user_details where Id='" + jsonReg.SelectToken("userid").ToString().Trim() + "'");

                    string sProfPic = "";
                    if (dt_basic.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt_basic.Rows[0]["profilePicUrl"].ToString()))
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/" + dt_basic.Rows[0]["profilePicUrl"].ToString();
                        }
                        else
                        {
                            sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/ProfileImage.png";
                        }
                    }
                    else
                    {
                        sProfPic = "http://astaguru.bcwebwise.com/AstaGuru_api/Uploads/ProfileImage.png";
                    }

                    sResponse = "{'status': 'true','message': 'success','profile_pic_url':'" + sProfPic + "'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }


    [WebMethod]
    public static string UpcomingAuction(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string Status = jsonReg.SelectToken("AuctionStatus").ToString().Trim();
            Status = Status.Replace("UpComing", "UpComming");
            Status = Status.Replace("Live", "Current");

            if (result)
            {
                sResponse = "{'status': 'true','message': 'Valid User'}";

                if (!string.IsNullOrEmpty(BasicFunction.GetUpcomingAuctionDetails(Status)))
                {
                    sResponse = "{'status': 'true','message': 'Valid User'," + BasicFunction.GetUpcomingAuctionDetails(Status) + "}";
                }

                sResponse = sResponse.Replace("UpComing", "UpComming");
                sResponse = sResponse.Replace("Current", "Live");

                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);

                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                //return JSONString;

            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'," + BasicFunction.GetUpcomingAuctionDetails(Status) + "}";

                sResponse = sResponse.Replace("UpComing", "UpComming");
                sResponse = sResponse.Replace("Current", "Live");

                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                //return JSONString;
            }
        }
        catch (Exception ex)
        {

            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string AddRemoveLotToWishList(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            if (result)
            {
                string IsLike = jsonReg.SelectToken("Like").ToString().Trim();
                if (IsLike == "true")
                {
                    IsLike = "1";
                }
                else
                {
                    IsLike = "0";
                }
                string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

                SqlConnection conn = new SqlConnection();
                SqlCommand cmdInsert;
                conn.ConnectionString = connStr;

                conn.Open();
                cmdInsert = new SqlCommand("sp_UpdateLotToWishList", conn);
                cmdInsert.CommandType = CommandType.StoredProcedure;


                cmdInsert.Parameters.AddWithValue("@LotId", jsonReg.SelectToken("LotId").ToString().Trim());
                cmdInsert.Parameters.AddWithValue("@LotLike", IsLike);

                // Where Condition parameters
                cmdInsert.Parameters.AddWithValue("@userid", jsonReg.SelectToken("userid").ToString().Trim());

                //

                cmdInsert.ExecuteNonQuery();


                sResponse = "{'status': 'true','message': 'Success'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);


                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
        }
        catch (Exception ex)
        {

            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string ShowInterestInAuction(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            //bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            //if (result)
            //{
            bool result1 = BasicFunction.IntrestInAuction(jsonReg);
            if (result1)
            {
                sResponse = "{'status': 'true','message': 'success'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user3'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
            }
            //}
            //else
            //{
            //sResponse = "{'status': 'false','message': 'Invalid user3'}";
            //JObject jsonMobileobj = JObject.Parse(sResponse);
            //JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            //}
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));

        }
    }

    [WebMethod]
    public static string AuctionClosingSchedule(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string id = jsonReg.SelectToken("AuctionId").ToString().Trim();
            string Result = BasicFunction.GetSchedule(id);
            if (Result != string.Empty)
            {
                if (result)
                {
                    sResponse = "{'status': 'true','message': 'success','result':{" + Result + "}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user','result':{" + Result + "}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }

        }

        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));

        }
    }

    [WebMethod]
    public static string UpcomingAuctionInformation(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            if (result)
            {
                sResponse = "{'status': 'true','message': 'Valid User'," + BasicFunction.UpcomingAuctionInformation(jsonReg.SelectToken("AuctionId").ToString()) + "}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);

                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                //return JSONString;

            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'," + BasicFunction.UpcomingAuctionInformation(jsonReg.SelectToken("AuctionId").ToString()) + "}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                //return JSONString;
            }
        }
        catch (Exception ex)
        {

            return Convert.ToBase64String(EncryptStringAES(ex.InnerException.ToString()));
        }
    }

    [WebMethod]
    public static string UpcomingLots(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            string userid = jsonReg.SelectToken("userid").ToString().Trim();
            //bool result = BasicFunction.veryfy_authkey(userid, jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string id = jsonReg.SelectToken("AuctionId").ToString().Trim();
            if (string.IsNullOrEmpty(userid))
            {
                userid = "0";
            }
            if (string.IsNullOrEmpty(id))
            {
                id = "0";
            }
            string Status = "";
            DataTable db_mail = new DataTable();
            db_mail = BasicFunction.GetDetailsByDatatableCRM("select status from AuctionListMaster where AuctionId='" + id + "'");
            //
            if (db_mail.Rows.Count > 0)
            {
                Status = db_mail.Rows[0]["status"].ToString();
            }
            string Result = BasicFunction.Upcoming_Lots(id, userid,Status);
            Result = Result.Replace("UpComming", "UpComing");

            if (Result != string.Empty)
            {
                if (userid != "0")
                {
                    sResponse = "{'status': 'true','message': 'success','result':{'AuctionId':'" + id + "','AuctionStatus':'" + Status.Replace("UpComing", "UpComming") + "','lots':[" + Result + "]}}";
                    sResponse = sResponse.Replace("Current", "Live");
                    sResponse = sResponse.Replace("LiveBid", "CurrentBid");


                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user','result':{'AuctionId':'" + id + "','AuctionStatus':'" + Status.Replace("UpComing", "UpComming") + "','lots':[" + Result + "]}}";
                    sResponse = sResponse.Replace("Current", "Live");
                    sResponse = sResponse.Replace("LiveBid", "CurrentBid");

                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
            }
            else
            {
                //sResponse = "{'status': 'false','message': 'Invalid user'}";
                //JObject jsonMobileobj = JObject.Parse(sResponse);
                //JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                //return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                if (userid != "0")
                {
                    sResponse = "{'status': 'true','message': 'success','result':{}}";
                    sResponse = sResponse.Replace("Current", "Live");
                    sResponse = sResponse.Replace("LiveBid", "CurrentBid");

                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user','result':{}}";
                    sResponse = sResponse.Replace("Current", "Live");
                    sResponse = sResponse.Replace("LiveBid", "CurrentBid");

                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
            }

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }


    [WebMethod]
    public static string GetProxyBidAmount(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string id = jsonReg.SelectToken("userid").ToString().Trim();


            if (result)
            {
                DataTable db_mail = new DataTable();
                //string str = "select Lot_Bid_EstimateFrom,Lot_Bid_EstimateTo from  ManageLotMaster  where Lot_ID='" + jsonReg.SelectToken("LotId").ToString().Trim() + "'";
                db_mail = BasicFunction.GetDetailsByDatatableCRM("select ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo,AM.DollarRate,IC.Value from  ManageLotMaster ML join AuctionListMaster AM on ML.Lot_AuctionId = AM.AuctionId join InventoryCategories IC on IC.LotNo= ML.Lot_ID where ML.Lot_ID='" + jsonReg.SelectToken("LotId").ToString().Trim() + "'");

                if (db_mail.Rows.Count > 0)
                {
                    int EstimateFrom = Convert.ToInt32(db_mail.Rows[0]["Lot_Bid_EstimateFrom"].ToString().Trim());
                    int OpeningBid = Convert.ToInt32(db_mail.Rows[0]["Value"].ToString().Trim());

                    double USD_Price = Convert.ToDouble(db_mail.Rows[0]["DollarRate"].ToString().Trim());
                    double First = OpeningBid + (OpeningBid * 0.1);
                    double Second = First + (First * 0.1);
                    double Third = Second + (Second * 0.1);
                    double Fourth = Third + (Third * 0.1);

                    sResponse = "{'status': 'true','message': 'success','result':{'BidIncrementPercentage':'10','NextValidBid':[{'INR':'" + First + "','USD':'" + Math.Round(First / USD_Price, 2).ToString() + "'},{'INR':'" + Second + "','USD':'" + Math.Round(Second / USD_Price, 2).ToString() + "'},{'INR':'" + Third + "','USD':'" + Math.Round(Third / USD_Price, 2).ToString() + "'},{'INR':'" + Fourth + "','USD':'" + Math.Round(Fourth / USD_Price, 2).ToString() + "'}]}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Bid Amount is not set against Lot_no=" + jsonReg.SelectToken("Lot_id").ToString().Trim() + " . Please contact to admin'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }

            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }


        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string InsertProxyBidAmount(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            bool result = BasicFunction.veryfy_authkey(jsonReg.SelectToken("userid").ToString().Trim(), jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string userid = jsonReg.SelectToken("userid").ToString().Trim();
            string LotId = jsonReg.SelectToken("LotId").ToString().Trim();
            string ProxyBidAmount = jsonReg.SelectToken("ProxyBidAmount").ToString().Trim();
            string LiveBidAmount = jsonReg.SelectToken("LiveBidAmount").ToString().Trim();
            string CurrencyType = jsonReg.SelectToken("CurrencyType").ToString().Trim();
            DataTable db_mail = new DataTable();
            //string str = "select Lot_Bid_EstimateFrom,Lot_Bid_EstimateTo from  ManageLotMaster  where Lot_ID='" + jsonReg.SelectToken("LotId").ToString().Trim() + "'";
            db_mail = BasicFunction.GetDetailsByDatatableCRM("select AM.DollarRate from  ManageLotMaster ML join AuctionListMaster AM on ML.Lot_AuctionId = AM.AuctionId where ML.Lot_ID='" + jsonReg.SelectToken("LotId").ToString().Trim() + "'");
            double DollarRate = 60;
            if (db_mail.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(db_mail.Rows[0]["DollarRate"].ToString()))
                {
                    DollarRate = Convert.ToDouble(db_mail.Rows[0]["DollarRate"].ToString());
                }
            }

            if (CurrencyType == "USD")
            {
                double ProxyBid = Convert.ToDouble(ProxyBidAmount) * DollarRate;
                ProxyBidAmount = ProxyBid.ToString();

                double LiveBid = Convert.ToDouble(LiveBidAmount) * DollarRate;
                LiveBidAmount = LiveBid.ToString();
            }
            if (result)
            {
                string Result = BasicFunction.InsertBidAmpount(userid, LotId, ProxyBidAmount, LiveBidAmount);

                //if (Result == "InsufisuntBalance")
                //{
                //    sResponse = "{'status': 'true','message': 'valid user','bidStatus':'InsufisuntBalance'}";
                //    JObject jsonMobileobj = JObject.Parse(sResponse);
                //    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                //    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                //}
                if (Result == "InsufisuntBalance")
                {
                    sResponse = "{'status': 'true','message': 'valid user','bidStatus':'false','bidMessage' : 'You have exceeded your bid limit. Please contact our office for support.'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else if (Result == "success")
                {
                    sResponse = "{'status': 'true','message': 'success','bidStatus':'true','bidMessage' :'Your Proxy Bid has been submitted successfully'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else
                {
                    sResponse = "{'status': 'true','message': 'valid user','bidStatus':'true','bidMessage' :'Proxy Bid value must be higher by at least 10% of Current Bid value'}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetLotDetails(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
       // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            string userid = jsonReg.SelectToken("userid").ToString().Trim();
            //bool result = BasicFunction.veryfy_authkey(userid, jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string id = jsonReg.SelectToken("LotId").ToString().Trim();
            // string LotNumber = jsonReg.SelectToken("LotNumber").ToString().Trim();
            string LotNumber = "1";
            if (string.IsNullOrEmpty(userid))
            {
                userid = "0";
            }

            string Result = BasicFunction.LotDetail_Lots(id, userid, LotNumber);
            Result = Result.Replace("UpComing", "UpComming");

            //if (result)
            //{
            if (Result != string.Empty)
            {
                if (userid != "0")
                {
                    sResponse = "{'status': 'true','message': 'success','result':{'lots':[" + Result + "]}}";
                    sResponse = sResponse.Replace("Current", "Live");
                    sResponse = sResponse.Replace("LiveBid", "CurrentBid");
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user','result':{'AuctionId':'" + id + "','lots':[" + Result + "]}}";
                    sResponse = sResponse.Replace("Current", "Live");
                    sResponse = sResponse.Replace("LiveBid", "CurrentBid");
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                sResponse = sResponse.Replace("Current", "Live");
                sResponse = sResponse.Replace("LiveBid", "CurrentBid");
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
            //}
            //else
            //{
            //    if (Result != string.Empty)
            //    {
            //        if (userid != "0")
            //        {
            //            sResponse = "{'status': 'false','message': 'Invalid user','result':{'lots':[" + Result + "]}}";
            //            JObject jsonMobileobj = JObject.Parse(sResponse);
            //            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            //        }
            //        else
            //        {
            //            sResponse = "{'status': 'false','message': 'Invalid user','result':{'AuctionId':'" + id + "','lots':[" + Result + "]}}";
            //            JObject jsonMobileobj = JObject.Parse(sResponse);
            //            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            //        }
            //    }
            //    else
            //    {
            //        sResponse = "{'status': 'false','message': 'Invalid user'}";
            //        JObject jsonMobileobj = JObject.Parse(sResponse);
            //        JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            //        return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            //    }
            //}

        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }

    [WebMethod]
    public static string GetSimilarLots(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            List<string> LotList = new List<string>();
            string userid = jsonReg.SelectToken("userid").ToString().Trim();
            //bool result = BasicFunction.veryfy_authkey(userid, jsonReg.SelectToken("authkey_web").ToString().Trim(), jsonReg.SelectToken("authkey_mobile").ToString().Trim());
            string id = jsonReg.SelectToken("LotId").ToString().Trim();
            if (string.IsNullOrEmpty(userid))
            {
                userid = "0";
            }

            DataTable db_ArtistLot = new DataTable();
            db_ArtistLot = BasicFunction.GetDetailsByDatatableCRM("select CM.CategoryName,IC.InventoryID,IC.Value,IC.ItemImagesURL,IC.[3DImageURL],IC.CategoryID from CategoryMaster CM join InventoryCategories IC on CM.CategoryID=IC.CategoryID where LotNo='" + id + "'");
            // string OpeningBid = db_ArtistLot.Rows[0]["Value"].ToString();

            DataTable db_BidLot = new DataTable();
            db_BidLot = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.status,AL.auctiontitle as 'title',ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId where ML.Lot_ID='" + id + "'");

            if (db_ArtistLot.Rows.Count > 0)
            {
                string CategoryName = db_ArtistLot.Rows[0]["CategoryName"].ToString();
                string InventoryId = db_ArtistLot.Rows[0]["InventoryID"].ToString();
                DataTable db_Lot = new DataTable();
                db_Lot = BasicFunction.GetDetailsByDatatableCRM("select " + CategoryName + "ID as DepartmentID,ArtistID,MediumID,Year,SizeID,Length,Breadth,Height from " + CategoryName + "  where InventoryID='" + InventoryId + "'");
                if (db_Lot.Rows.Count > 0)
                {
                    string ArtistId = db_Lot.Rows[0]["ArtistID"].ToString();
                    DataTable db_Lots = new DataTable();
                    db_Lots = BasicFunction.GetDetailsByDatatableCRM("select " + CategoryName + "ID as DepartmentID,InventoryID,ArtistID,MediumID,Year,SizeID,Length,Breadth,Height from " + CategoryName + "  where ArtistID='" + ArtistId + "'");
                    if (db_Lots.Rows.Count > 0)
                    {
                        for (int i = 0; i < db_Lots.Rows.Count; i++)
                        {
                            DataTable db_Lotss = new DataTable();
                            db_Lotss = BasicFunction.GetDetailsByDatatableCRM("select LotNo from InventoryCategories where InventoryID='" + db_Lots.Rows[i]["InventoryID"].ToString() + "'");
                            if (db_Lotss.Rows.Count > 0)
                            {
                                for (int j = 0; j < db_Lotss.Rows.Count; j++)
                                {
                                    string LotNo = db_Lotss.Rows[j]["LotNo"].ToString();
                                    LotList.Add(LotNo);
                                }
                            }
                            //else
                            //{
                            //    LotList.Add("0");
                            //}
                        }
                    }

                    //else
                    //{
                    //    LotList.Add("0");
                    //}
                }
                //else
                //{
                //    //LotList.Add("0");
                //}

            }
            if (db_BidLot.Rows.Count > 0)
            {
                string Lot_Bid_EstimateFrom = db_BidLot.Rows[0]["Lot_Bid_EstimateFrom"].ToString();
                DataTable db_BidLots = new DataTable();
                db_BidLots = BasicFunction.GetDetailsByDatatableCRM("select '' as LotURL1,AL.status,AL.auctiontitle as 'title',ML.Lot_ID,ML.Lot_Name,ML.Lot_Bid_EstimateFrom,ML.Lot_Bid_EstimateTo from AuctionListMaster AL join ManageLotMaster ML on AL.AuctionId = ML.Lot_AuctionId where ML.Lot_Bid_EstimateFrom='" + Lot_Bid_EstimateFrom + "'");
                if (db_BidLots.Rows.Count > 0)
                {
                    for (int i = 0; i < db_BidLots.Rows.Count; i++)
                    {
                        string LotNos = db_BidLots.Rows[i]["Lot_ID"].ToString();
                        LotList.Add(LotNos);
                    }
                }
                //else
                //{
                //    LotList.Add("0");
                //}
            }
            //else
            //{
            //    LotList.Add("0");
            //}

            for (int j = 0; j < LotList.Count; j++)
            {
                if (string.IsNullOrEmpty(LotList[j]))
                {
                    LotList.RemoveAt(j);
                }
            }

            LotList = LotList.Distinct().ToList();

            string Result = string.Empty;

            if (LotList.Count > 0)
            {
                Result = BasicFunction.SimilarUpcoming_Lots(LotList, userid);
                Result = Result.Replace("UpComing", "UpComming");
            }

            if (!string.IsNullOrEmpty(Result))
            {
                if (userid != "0")
                {
                    sResponse = "{'status': 'true','message': 'success','result':{'lots':[" + Result + "]}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
                else
                {
                    sResponse = "{'status': 'false','message': 'Invalid user','result':{'AuctionId':'" + id + "','lots':[" + Result + "]}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
            }
            else
            {
                sResponse = "{'status': 'false','message': 'Invalid user'}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string AdditionalCharges(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
       // JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            string id = jsonReg.SelectToken("LotId").ToString().Trim();
            DataTable db_AditionalCharge = new DataTable();
            db_AditionalCharge = BasicFunction.GetDetailsByDatatableCRM("select ML.Lot_ID,ML.Lot_AuctionId,ICD.LotNo,ICD.Gst,IC.Value,ICD.NonExportable,ICD.UsedGoodPercentage,ICD.IsusedGood,ICD.IsInternational,AL.auction_Intl_Custom_duty_per,AL.auction_TCS,AL.auction_Margin,AL.DollarRate from ManageLotMaster ML join InventoryCategoryDetails ICD on ML.Lot_ID = ICD.LotNo join InventoryCategories IC on IC.LotNo=ICD.LotNo join AuctionListMaster AL on AL.AuctionId=ML.Lot_AuctionId where IC.ID='" + id + "'");

            if (db_AditionalCharge.Rows.Count > 0)
            {
                if (db_AditionalCharge.Rows[0]["IsusedGood"].ToString() != "1")
                {
                    int DollarRate = Convert.ToInt32(db_AditionalCharge.Rows[0]["DollarRate"].ToString().Replace(".00", ""));
                    double GST = Convert.ToDouble(db_AditionalCharge.Rows[0]["Gst"].ToString().Replace(".00", ""));
                    string IsInternational = db_AditionalCharge.Rows[0]["IsInternational"].ToString();
                    double auction_Intl_Custom_duty_per = Convert.ToDouble(db_AditionalCharge.Rows[0]["auction_Intl_Custom_duty_per"].ToString());
                    double auction_TCS = Convert.ToDouble(db_AditionalCharge.Rows[0]["auction_TCS"].ToString());
                    double auction_Margin = Convert.ToDouble(db_AditionalCharge.Rows[0]["auction_Margin"].ToString());
                    int OpeningPrice = Convert.ToInt32(db_AditionalCharge.Rows[0]["Value"].ToString());

                    double MarginPrice = OpeningPrice - (OpeningPrice * (auction_Margin / 100));
                    double GSTPrice = OpeningPrice - (OpeningPrice * (GST / 100));
                    double TCSPrice = OpeningPrice - (OpeningPrice * (auction_TCS / 100));
                    double InternationalDutyPrice = 0;
                    double TotalPrice = OpeningPrice + MarginPrice + InternationalDutyPrice + GSTPrice + TCSPrice;
                    //var FinalOpeningPrice = Math.Round(OpeningPrice / DollarRate,0);
                    // var FinalMarginPrice= Math.Round(MarginPrice / DollarRate,0);

                    string AdditionalCharges = "'Price':{'INR':'" + OpeningPrice + "','USD':'" + Math.Round(Convert.ToDouble(OpeningPrice / DollarRate), 2) + "'},'Taxation':[{'Name':'" + auction_Margin + "% Margin','Value':{'INR':'" + MarginPrice + "','USD':'" + Math.Round(Convert.ToDouble(MarginPrice / DollarRate), 2) + "'}},{'Name':'GST On Lot(" + GST + ")','Value':{'INR':'" + GSTPrice + "','USD':'" + Math.Round(Convert.ToDouble(GSTPrice / DollarRate), 2) + "'}},{'Name':'TCS(" + auction_TCS + ")','Value':{'INR':'" + TCSPrice + "','USD':'" + Math.Round(Convert.ToDouble(TCSPrice / DollarRate), 2) + "'}}],'Total':{'INR':'" + TotalPrice + "','USD':'" + Math.Round(Convert.ToDouble(TotalPrice / DollarRate), 2) + "'},'Notes' : ['The GST on this lot is charged at 12 % on 20 % total Astaguru margin ( buyer + seller) in accordance with 32(5) of CGST Rule 2017.,Crating and shipping charged separately depending on delivery location.']";

                    if (IsInternational.ToLower() == "false".ToLower())
                    {
                        InternationalDutyPrice = OpeningPrice - (OpeningPrice * (auction_Intl_Custom_duty_per / 100));
                        GSTPrice = 0;
                        TotalPrice = OpeningPrice + MarginPrice + InternationalDutyPrice + GSTPrice + TCSPrice;

                        AdditionalCharges = "'Price':{'INR':'" + OpeningPrice + "','USD':'" + Math.Round(Convert.ToDouble(OpeningPrice / DollarRate), 2) + "'},'Taxation':[{'Name':'" + auction_Margin + "% Margin','Value':{'INR':'" + MarginPrice + "','USD':'" + Math.Round(Convert.ToDouble(MarginPrice / DollarRate), 2) + "'}},{'Name':'International Duty On Lot(" + InternationalDutyPrice + ")','Value':{'INR':'" + InternationalDutyPrice + "','USD':'" + Math.Round(Convert.ToDouble(InternationalDutyPrice / DollarRate), 2) + "'}},{'Name':'TCS(" + auction_TCS + ")','Value':{'INR':'" + TCSPrice + "','USD':'" + Math.Round(Convert.ToDouble(TCSPrice / DollarRate), 2) + "'}}],'Total':{'INR':'" + TotalPrice + "','USD':'" + Math.Round(Convert.ToDouble(TotalPrice / DollarRate), 2) + "'},'Notes' : ['The GST on this lot is charged at 12 % on 20 % total Astaguru margin ( buyer + seller) in accordance with 32(5) of CGST Rule 2017.,Crating and shipping charged separately depending on delivery location.']";

                    }
                    sResponse = "{'status': 'true','message': 'success','result':{" + AdditionalCharges + "}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
                }
                else
                {
                    int DollarRate = Convert.ToInt32(db_AditionalCharge.Rows[0]["DollarRate"].ToString().Replace(".00", ""));
                    double GST = Convert.ToDouble(db_AditionalCharge.Rows[0]["Gst"].ToString().Replace(".00", ""));
                    string IsInternational = db_AditionalCharge.Rows[0]["IsInternational"].ToString();
                    double auction_Intl_Custom_duty_per = Convert.ToDouble(db_AditionalCharge.Rows[0]["auction_Intl_Custom_duty_per"].ToString());
                    double auction_TCS = Convert.ToDouble(db_AditionalCharge.Rows[0]["auction_TCS"].ToString());
                    double auction_Margin = Convert.ToDouble(db_AditionalCharge.Rows[0]["auction_Margin"].ToString());
                    int OpeningPrice = Convert.ToInt32(db_AditionalCharge.Rows[0]["Value"].ToString());
                    int InitialPrice = 0;
                    int TaxableAmount = OpeningPrice - InitialPrice;

                    double MarginPrice = OpeningPrice - (TaxableAmount * (auction_Margin / 100));
                    double GSTPrice = OpeningPrice - (TaxableAmount * (GST / 100));
                    double TCSPrice = OpeningPrice - (TaxableAmount * (auction_TCS / 100));
                    double InternationalDutyPrice = 0;
                    double TotalPrice = OpeningPrice + MarginPrice + InternationalDutyPrice + GSTPrice + TCSPrice;
                    string AdditionalCharges = "'Price':{'INR':'" + OpeningPrice + "','USD':'" + Math.Round(Convert.ToDouble(OpeningPrice / DollarRate), 2) + "'},'Taxation':[{'Name':'" + auction_Margin + "% Margin','Value':{'INR':'" + MarginPrice + "','USD':'" + Math.Round(Convert.ToDouble(MarginPrice / DollarRate), 2) + "'}},{'Name':'GST On Lot(" + GST + ")','Value':{'INR':'" + GSTPrice + "','USD':'" + Math.Round(Convert.ToDouble(GSTPrice / DollarRate), 2) + "'}},{'Name':'TCS(" + auction_TCS + ")','Value':{'INR':'" + TCSPrice + "','USD':'" + Math.Round(Convert.ToDouble(TCSPrice / DollarRate), 2) + "'}}],'Total':{'INR':'" + TotalPrice + "','USD':'" + Math.Round(Convert.ToDouble(TotalPrice / DollarRate), 2) + "','Notes' : ['The GST on this lot is charged at 12 % on 20 % total Astaguru margin ( buyer + seller) in accordance with 32(5) of CGST Rule 2017.,Crating and shipping charged separately depending on delivery location.']";

                    if (IsInternational.ToLower() == "false".ToLower())
                    {
                        InternationalDutyPrice = OpeningPrice - (TaxableAmount * (auction_Intl_Custom_duty_per / 100));
                        GSTPrice = 0;
                        TotalPrice = OpeningPrice + MarginPrice + InternationalDutyPrice + GSTPrice + TCSPrice;
                        AdditionalCharges = "'Price':{'INR':'" + OpeningPrice + "','USD':'" + Math.Round(Convert.ToDouble(OpeningPrice / DollarRate), 2) + "'},'Taxation':[{'Name':'" + auction_Margin + "% Margin','Value':{'INR':'" + MarginPrice + "','USD':'" + Math.Round(Convert.ToDouble(MarginPrice / DollarRate), 2) + "'}},{'Name':'International Duty On Lot(" + InternationalDutyPrice + ")','Value':{'INR':'" + InternationalDutyPrice + "','USD':'" + Math.Round(Convert.ToDouble(InternationalDutyPrice / DollarRate), 2) + "'}},{'Name':'TCS(" + auction_TCS + ")','Value':{'INR':'" + TCSPrice + "','USD':'" + Math.Round(Convert.ToDouble(TCSPrice / DollarRate), 2) + "'}}],'Total':{'INR':'" + TotalPrice + "','USD':'" + Math.Round(Convert.ToDouble(TotalPrice / DollarRate), 2) + "','Notes' : ['The GST on this lot is charged at 12 % on 20 % total Astaguru margin ( buyer + seller) in accordance with 32(5) of CGST Rule 2017.,Crating and shipping charged separately depending on delivery location.']";
                    }
                    sResponse = "{'status': 'true','message': 'success','result':{" + AdditionalCharges + "}}";
                    JObject jsonMobileobj = JObject.Parse(sResponse);
                    JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                    return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

                }
            }
            else
            {
                sResponse = "{'status': 'true','message': 'success','result':{'message' : 'Record Not Found'}}";
                JObject jsonMobileobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonMobileobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));

            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));

        }
    }

    [WebMethod]
    public static string InsertReachUsForm(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_Insert_ReachUsForm", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;


            cmdInsert.Parameters.AddWithValue("@FullName", jsonReg.SelectToken("fullname").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@EmailId", jsonReg.SelectToken("emailid").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@PhoneNumber", jsonReg.SelectToken("phonenumber").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@Department", jsonReg.SelectToken("department").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@Query", jsonReg.SelectToken("query").ToString().Trim());

            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }


    [WebMethod]
    public static string InsertCareerForm(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";

        string JSONString = string.Empty;
        string sType = string.Empty;
        string sResume = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(jsonReg.SelectToken("resume").ToString().Trim()))
            {
                sResume = jsonReg.SelectToken("resume").ToString().Trim();
                sType = jsonReg.SelectToken("resume").ToString().Trim();

                if (sType.Contains("data:image/jpeg;base64") || sType.Contains("data:image/jpg;base64"))
                {
                    sType = ".jpg";
                }
                if (sType.Contains("data:application/pdf;base64"))
                {
                    sType = ".pdf";
                }
                if (sType.Contains("data:image/png;base64"))
                {
                    sType = ".png";
                }
                if (sType.Contains("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,"))
                {
                    sType = ".docx";
                }
                if (sType.Contains("data:application/msword;base64,"))
                {
                    sType = ".doc";
                }

                sResume = sResume.Replace("data:image/jpeg;base64,", "");
                sResume = sResume.Replace("data:application/pdf;base64,", "");
                sResume = sResume.Replace("data:image/png;base64,", "");
                sResume = sResume.Replace("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,", "");
                sResume = sResume.Replace("data:application/msword;base64,", "");

                if (sType == ".jpg" || sType == ".png")
                {
                    using (MemoryStream msPancard = new MemoryStream(Convert.FromBase64String(sResume)))
                    {
                        using (Bitmap bmpPancard = new Bitmap(msPancard))
                        {
                            sResume = "Resume_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));

                            bmpPancard.Save(HttpContext.Current.Request.MapPath("~/Uploads/" + sResume + sType));
                            sResume = sResume + sType;
                        }
                    }
                }
                else
                {
                    //sResume = "";
                    string sPDF = sResume;
                    sResume = "Resume_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyyhhmmss"));
                    byte[] bytes = Convert.FromBase64String(sPDF);
                    File.WriteAllBytes(HttpContext.Current.Request.MapPath("~/Uploads/" + sResume + sType), bytes);
                    sResume = sResume + sType;
                }
            }



            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_Insert_CareerForm", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;

            cmdInsert.Parameters.AddWithValue("@FullName", jsonReg.SelectToken("fullname").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@EmailId", jsonReg.SelectToken("emailid").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@JobTitle", jsonReg.SelectToken("jobtitle").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@Query", jsonReg.SelectToken("query").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@HowYouKnow", jsonReg.SelectToken("howyouknow").ToString().Trim());

            cmdInsert.Parameters.AddWithValue("@Resume", sResume);

            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string GetBidHistory(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonBid = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);
        string sResponse = "";
        string JSONString = string.Empty;

        try
        {
            DataTable db_BidLotsHistory = new DataTable();
            db_BidLotsHistory = BasicFunction.GetDetailsByDatatable("SELECT tbl_AuctionProxyBidInsert.LiveBidTiming,tbl_AuctionProxyBidInsert.LiveBidAmount,(select first_name + ' ' + last_name  from user_details where tbl_AuctionProxyBidInsert.UserId=user_details.Id) as FullName FROM tbl_AuctionProxyBidInsert where LotId='" + jsonBid.SelectToken("LotId").ToString().Trim() + "' order by Id desc");

            if (db_BidLotsHistory.Rows.Count > 0)
            {
                sResponse = sResponse + "{'status': 'true','message':'Valid User','result': {'BidHistory':[";

                for (int i = 0; i < db_BidLotsHistory.Rows.Count; i++)
                {
                    //if (!string.IsNullOrEmpty(db_BidLotsHistory.Rows[i]["LiveBidAmount"].ToString()))
                    //{
                    //    db_BidLotsHistory.Rows[i]["LiveBidAmount"] = String.Format("{0:0.00}", db_BidLotsHistory.Rows[i]["LiveBidAmount"].ToString());
                    //}

                    sResponse = sResponse + "{'Name':'" + db_BidLotsHistory.Rows[i]["FullName"].ToString() + "','DateTime':'" + db_BidLotsHistory.Rows[i]["LiveBidTiming"].ToString() + "','Amount':{'INR': '" + db_BidLotsHistory.Rows[i]["LiveBidAmount"].ToString() + "','USD': '" + db_BidLotsHistory.Rows[i]["LiveBidAmount"].ToString() + "'}}";

                    if (i + 1 != db_BidLotsHistory.Rows.Count)
                    {
                        sResponse = sResponse + ",";
                    }
                }
                sResponse = sResponse + "]}}";

                JObject jsonReachUsobj = JObject.Parse(sResponse);
                JSONString = JsonConvert.SerializeObject(jsonReachUsobj);
                return Convert.ToBase64String(EncryptStringAES(JSONString));

            }
            else
            {
                return Convert.ToBase64String(EncryptStringAES("No Record Found"));
            }
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.Message));
        }
    }

    [WebMethod]
    public static string InsertGetEstimateForm(string d)
    {
        string decryptedString = d;

        decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
        decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

        string replacedString = DecryptStringAES(decryptedString);
        replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

        JObject jsonReg = JObject.Parse(replacedString);
        //JObject jsonReg = JObject.Parse(decryptedString);

        string sResponse = "";

        string JSONString = string.Empty;
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["AstaguruConnection"].ToString();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmdInsert;
            conn.ConnectionString = connStr;

            conn.Open();
            cmdInsert = new SqlCommand("sp_InsertGetEstimateForm", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;


            cmdInsert.Parameters.AddWithValue("@fullname", jsonReg.SelectToken("fullname").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@phonenumber", jsonReg.SelectToken("phonenumber").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@emailid", jsonReg.SelectToken("emailid").ToString().Trim());
            cmdInsert.Parameters.AddWithValue("@query", jsonReg.SelectToken("query").ToString().Trim());


            cmdInsert.ExecuteNonQuery();

            sResponse = "{'status': 'true','message': 'success'}";
            JObject jsonMobileobj = JObject.Parse(sResponse);
            JSONString = JsonConvert.SerializeObject(jsonMobileobj);
            return Convert.ToBase64String(EncryptStringAES(JSONString.Replace("{", "curleyfront").Replace("}", "curleyback")));
        }
        catch (Exception ex)
        {
            return Convert.ToBase64String(EncryptStringAES(ex.StackTrace));
        }
    }
}
