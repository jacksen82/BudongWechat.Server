﻿<%@ WebHandler Language="C#" Class="Next" %>

using System.Web;
using Budong.Common.Utils;

public class Next : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //  格式化参数
        string session3rd = context.Request.Params["session3rd"];

        //  定义返回结果
        Hash result = ClientService.Token(session3rd);

        if (result.ToInt("code") == 0)
        {
            result = GameService.Next(result.ToHash("data"));
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