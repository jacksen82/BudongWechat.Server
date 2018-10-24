<%@ WebHandler Language="C#" Class="Saved" %>

using System.Web;
using Budong.Common.Utils;

public class Saved : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int fromClientId = Parse.ToInt(context.Request.Params["fromClientId"]);
        string openGId = context.Request.Params["openGId"];
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = ClientSaveService.Save(result.ToHash("data"), fromClientId, openGId);
        }

        //  记录日志
        ClientLogService.Append(session3rd);

        //  返回结果
        context.Response.Write(result.ToJSON());
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}