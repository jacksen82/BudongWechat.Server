<%@ WebHandler Language="C#" Class="Relation" %>

using System.Web;
using Budong.Common.Utils;

public class Relation : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int scene = Parse.ToInt(context.Request.Params["scene"]);
        int fromClientId = Parse.ToInt(context.Request.Params["fromClientId"]);
        int shareQuestionId = Parse.ToInt(context.Request.Params["shareQuestionId"]);
        string encryptedData = context.Request.Params["encryptedData"];
        string iv = context.Request.Params["iv"];
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = ClientService.Relation(result.ToHash("data"), fromClientId, shareQuestionId, scene, encryptedData, iv);
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