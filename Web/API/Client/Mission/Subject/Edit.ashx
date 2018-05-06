<%@ WebHandler Language="C#" Class="Edit" %>

using System.Web;
using Budong.Common.Utils;

public class Edit : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int missionId = Parse.ToInt(context.Request.Params["missionId"]);
        int subjectId = Parse.ToInt(context.Request.Params["subjectId"]);
        int categoryId = Parse.ToInt(context.Request.Params["categoryId"]);
        string title = context.Request.Params["title"];
        string tip = context.Request.Params["tip"];
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = ClientMissionSubjectService.Edit(result.ToHash("data"), missionId, subjectId, title, tip, categoryId);
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