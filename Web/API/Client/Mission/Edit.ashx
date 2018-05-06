<%@ WebHandler Language="C#" Class="Edit" %>

using System.Web;
using Budong.Common.Utils;

public class Edit : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int missionId = Parse.ToInt(context.Request.Params["missionId"]);
        string title = context.Request.Params["title"];
        string session3rd = context.Request.Params["session3rd"];
        HttpPostedFile logofile = context.Request.Files["logofile"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = ClientMissionService.Edit(result.ToHash("data"), missionId, title, logofile);
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