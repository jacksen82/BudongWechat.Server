<%@ WebHandler Language="C#" Class="Activate" %>

using System.Web;
using Budong.Common.Utils;

public class Activate : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int toClientId = Parse.ToInt(context.Request.Params["toClientId"]);
        string openGId = context.Request.Params["openGId"];
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = GameService.Activate(result.ToHash("data"), toClientId, openGId);
        }

        //  记录日志
        LogService.Append(session3rd);

        //  返回结果
        context.Response.Write(result.ToJSON());
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}