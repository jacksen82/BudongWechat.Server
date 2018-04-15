<%@ WebHandler Language="C#" Class="Skip" %>

using System.Web;
using Budong.Common.Utils;

public class Skip : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int subjectId = Parse.ToInt(context.Request.Params["subjectId"]);
        int secondCount = Parse.ToInt(context.Request.Params["secondCount"]);
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("id") == 0)
        {
            result = MissionGameService.Skip(result.ToHash("data"), subjectId, secondCount);
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