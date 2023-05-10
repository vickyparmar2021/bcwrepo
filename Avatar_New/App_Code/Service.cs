using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;


namespace WcfRestfulService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class HttpService : IHttpService
    {
        string connStr = ConfigurationManager.ConnectionStrings["AvatarConnection"].ToString();

        public string AddAvatarInRoom(string roomId, string avatarGLB, string username)
        {
            string sResponse = "";
            string sRoomId = string.Empty;

            if (!string.IsNullOrEmpty(roomId.Trim()))
            {
                sRoomId = roomId.Trim();
            }
            else
            {
                sRoomId = BasicFunction.RandomNumber();
            }

            try
            {

                SqlConnection conn = new SqlConnection();
                SqlCommand cmdInsert;
                conn.ConnectionString = connStr;

                conn.Open();
                cmdInsert = new SqlCommand("sp_InsertAvatarInRoom", conn);
                cmdInsert.CommandType = CommandType.StoredProcedure;


                cmdInsert.Parameters.AddWithValue("@roomId", sRoomId.Trim());
                cmdInsert.Parameters.AddWithValue("@avatarGLB", avatarGLB.Trim());
                cmdInsert.Parameters.AddWithValue("@username", username.Trim());

                cmdInsert.ExecuteNonQuery();

                sResponse = "{'roomId': '" + sRoomId.Trim() + "'}";

                return sResponse.Replace("'", "\""); ;

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string GetUserList(string roomId)
        {
            string decryptedString = roomId.Trim();

            decryptedString = decryptedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");
            //decryptedString = decryptedString.Replace("d:", "").Replace("}", "").Replace("'", "").Replace("{", "").Trim();

            string replacedString = decryptedString;
            replacedString = replacedString.Replace("curleyfront", "{").Replace("curleyback", "}").Replace("d:", "");

            //JObject jsonReg = JObject.Parse(replacedString);
            string sResponse = "";

            string JSONString = string.Empty;

            string sRoomId = string.Empty;

            string strQuery = string.Empty;

            string sConcat = string.Empty;

            DataTable dTable = new DataTable();

            sRoomId = roomId.Trim();

            try
            {
                strQuery = "select UserName,AvatarGLB from tblAvatarDetails where RoomId='" + sRoomId + "'";

                dTable = BasicFunction.GetDetailsByDatatable(strQuery);

                if (dTable.Rows.Count > 0)
                {
                    sConcat = "[";

                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        sConcat = sConcat + "{'username': '" + dTable.Rows[i]["UserName"].ToString() + "','avatarGLB': '" + dTable.Rows[i]["avatarGLB"].ToString() + "' }";

                        if (i + 1 != dTable.Rows.Count)
                        {
                            sConcat = sConcat + ",";
                        }
                    }


                    sConcat = sConcat + "]";

                    JSONString = sConcat;
                    return JSONString.Replace("'", "\"");
                }
                else
                {
                    return "No Records Found for RoomId : " + sRoomId;
                }


            }
            catch (Exception ex)
            {
                return ex.Message.ToString(); ;
            }
        }
    }
}