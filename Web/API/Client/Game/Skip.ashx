<%@ WebHandler Language="C#" Class="Skip" %>

using System.Web;
using Budong.Common.Utils;

public class Skip : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int questionId = Parse.ToInt(context.Request.Params["questionId"]);
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = GameService.Skip(result.ToHash("data"), questionId);
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