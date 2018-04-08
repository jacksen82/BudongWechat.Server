<%@ WebHandler Language="C#" Class="UpdateBirthyear" %>

using System.Web;
using Budong.Common.Utils;

public class UpdateBirthyear : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        int birthyear = Parse.ToInt(context.Request.Params["birthyear"]);
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("id") == 0)
        {
            result = ClientService.UpdateBirthyear(result.ToHash("data"), birthyear);
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