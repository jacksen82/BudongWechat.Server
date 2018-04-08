<%@ WebHandler Language="C#" Class="Add" %>

using System.Web;
using Budong.Common.Utils;

public class Add : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int missionId = Parse.ToInt(context.Request.Params["missionId"]);
        int categoryId = Parse.ToInt(context.Request.Params["categoryId"]);
        string title = context.Request.Params["title"];
        string tip = context.Request.Params["tip"];
        string session3rd = context.Request.Params["session3rd"];
        HttpPostedFile mp3file = context.Request.Files["mp3file"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("id") == 0)
        {
            result = ClientMissionSubjectService.Add(result.ToHash("data"), missionId, title, tip, categoryId, mp3file);
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