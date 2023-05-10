using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfRestfulService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IHttpService
    {

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //     UriTemplate = "mobileNumberValid?mobile={mobile}")]
        //Stream mobileNumberValid(string mobile);

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //     UriTemplate = "validateOTP?mobile={mobile}&otp={otp}")]
        //Stream validateOTP(string mobile, string otp);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,RequestFormat=WebMessageFormat.Json,BodyStyle=WebMessageBodyStyle.WrappedRequest,
             UriTemplate = "/AddAvatarInRoom")]
        string AddAvatarInRoom(string roomId, string avatarGLB, string username);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
           UriTemplate = "/GetUserList")]
        string GetUserList(string roomId);
    }
}