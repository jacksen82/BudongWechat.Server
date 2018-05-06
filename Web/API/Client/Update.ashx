<%@ WebHandler Language="C#" Class="Update" %>

using System.Web;
using Budong.Common.Utils;

public class Update : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int gender = Parse.ToInt(context.Request.Params["gender"]);
        int birthyear = Parse.ToInt(context.Request.Params["birthyear"]);
        string nick = context.Request.Params["nick"];
        string avatarUrl = context.Request.Params["avatarUrl"];
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = ClientService.Update(result.ToHash("data"), nick, gender, avatarUrl, birthyear);
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