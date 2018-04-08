<%@ WebHandler Language="C#" Class="Delete" %>

using System.Web;
using Budong.Common.Utils;

public class Delete : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int subjectId = Parse.ToInt(context.Request.Params["subjectId"]);
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("id") == 0)
        {
            result = ClientMissionSubjectService.Delete(result.ToHash("data"), subjectId);
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