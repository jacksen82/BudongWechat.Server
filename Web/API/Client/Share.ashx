<%@ WebHandler Language="C#" Class="Share" %>

using System.Web;
using Budong.Common.Utils;

public class Share : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int shareQuestionId = Parse.ToInt(context.Request.Params["shareQuestionId"]);
        string shareFrom = context.Request.Params["shareFrom"];
        string shareAction = context.Request.Params["shareAction"];
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = ClientService.Share(result.ToHash("data"), shareFrom, shareAction, shareQuestionId);
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