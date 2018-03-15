<%@ WebHandler Language="C#" Class="Relate" %>

using System.Web;
using Budong.Common.Utils;

public class Relate : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int fromClientId = Parse.ToInt(context.Request.Params["fromClientId"]);
        string session3rd = context.Request.Params["session3rd"];
        string encryptedData = context.Request.Params["encryptedData"];
        string iv = context.Request.Params["iv"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("id") == 0)
        {
            result = ClientService.Relate(result.ToHash("data"), fromClientId, encryptedData, iv);
        }

        //  记录日志
        ClientService.Log(session3rd);

        //  返回结果
        context.Response.Write(result.ToJSON());
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}