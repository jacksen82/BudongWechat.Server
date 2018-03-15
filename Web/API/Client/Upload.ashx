<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;

public class Upload : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string lines = String.Empty;
        foreach (string key in context.Request.Params.AllKeys)
        {
            lines += key + "=" + context.Request.Params[key] + "\r\n";
        }
        foreach (string key in context.Request.Files.AllKeys)
        {
            lines += key + "=" + context.Request.Files[key] + "\r\n";
        }
        System.IO.File.AppendAllText("E:\\xxx.txt", lines);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}