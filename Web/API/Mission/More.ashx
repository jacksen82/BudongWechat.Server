<%@ WebHandler Language="C#" Class="More" %>

using System.Web;
using Budong.Common.Utils;

public class More : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int pageId = Parse.ToInt(context.Request.Params["pageId"], 1);
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = MissionService.More(result.ToHash("data"), pageId, Settings.PAGE_SIZE);
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