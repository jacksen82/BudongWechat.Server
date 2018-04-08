using System;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// 客户端业务操作类
/// </summary>
public class ClientService
{
    /// <summary>
    /// 获取 Token 方法
    /// </summary>
    /// <param name="session3rd">string 三方标识</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Token(string session3rd)
    {
        if (!Genre.IsNull(session3rd))
        {
            Hash client = ClientData.GetBySession3rd(session3rd);
            Hash clientSession = ClientSessionData.GetBySession3rd(session3rd);
            if (clientSession.ToInt("id") > 0)
            {
                if (client.ToInt("id") > 0)
                {
                    client.Add("session3rd", session3rd);
                    client.Add("sessionKey", clientSession.ToString("sessionKey"));
                    return new Hash((int)CodeType.OK, "成功", client);
                }
                return new Hash((int)CodeType.ClientNotExists, "client 不存在");
            }
            return new Hash((int)CodeType.Session3rdNotExists, "session3rd 不存在");
        }
        return new Hash((int)CodeType.Session3rdRequired, "session3rd 为空");
    }
    /// <summary>
    /// 登录方法
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="code">string 微信登录凭证</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Login(int appId, string code)
    {
        if (!Genre.IsNull(code))
        {
            Hash app = APPData.GetById(appId);
            if (app.ToInt("id") > 0)
            {
                Hash wechatSession = API.Code2Session(app.ToString("appKey"), app.ToString("appSecret"), code);
                if (wechatSession.ToInt("errcode") == 0)
                {
                    string openId = wechatSession.ToString("openid");
                    string unionId = wechatSession.ToString("unionid");
                    string sessionKey = wechatSession.ToString("session_key");

                    Hash client = ClientData.GetByOpenId(appId, openId);
                    Hash clientSession = ClientSessionData.GetByOpenIdAndSessionKey(appId, openId, sessionKey);
                    if (client.ToInt("id") == 0)
                    {
                        ClientData.Create(appId, openId, unionId);
                        client = ClientData.GetByOpenId(appId, openId);
                    }
                    if (clientSession.ToInt("id") == 0)
                    {
                        ClientSessionData.Create(appId, openId, sessionKey);
                        clientSession = ClientSessionData.GetByOpenIdAndSessionKey(appId, openId, sessionKey);
                    }

                    client.Add("session3rd", clientSession.ToString("session3rd"));
                    client.Add("sessionKey", clientSession.ToString("sessionKey"));

                    return new Hash((int)CodeType.OK, "成功", client);
                }
                return new Hash((int)CodeType.CodeInvalid, wechatSession.ToString("errmsg"));
            }
            return new Hash((int)CodeType.WechatAPPNotExists, "app 不存在");
        }
        return new Hash((int)CodeType.CodeRequired, "code 为空");
    }
    /// <summary>
    /// 更新用户资料
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="nick">string 昵称</param>
    /// <param name="gender">int 性别</param>
    /// <param name="avatarUrl">string 头像</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash UpdateProflie(Hash client, string nick, int gender, string avatarUrl)
    {
        if (ClientData.UpdateProfile(client.ToInt("id"), nick, gender, avatarUrl) > 0)
        {
            client = ClientData.GetById(client.ToInt("id"));
            return new Hash((int)CodeType.OK, "成功", client);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 更新出生年代
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="birthyear">int 出生年代</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash UpdateBirthyear(Hash client, int birthyear)
    {
        if (ClientData.UpdateBirthyear(client.ToInt("id"), birthyear) > 0)
        {
            client = ClientData.GetById(client.ToInt("id"));
            return new Hash((int)CodeType.OK, "成功", client);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 建立客户端关系
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="fromClientId">int 客户端好友编号</param>
    /// <param name="encryptedData">string 群标识加密信息</param>
    /// <param name="iv">string 群标识加密向量</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Relate(Hash client, int fromClientId, string encryptedData, string iv)
    {
        if (fromClientId > 0 && fromClientId != client.ToInt("id"))
        {
            ClientFriendData.Create(client.ToInt("id"), fromClientId);
            ClientFriendData.Create(fromClientId, client.ToInt("id"));
        }
        if (!Genre.IsNull(encryptedData) && !Genre.IsNull(iv) && !client.IsNull("sessionKey"))
        {
            Hash shareTicket = API.GetEncryptedData(encryptedData, client.ToString("sessionKey"), iv);
            if (!shareTicket.IsNull("openGId"))
            {
                ClientGroupData.Create(client.ToInt("id"), shareTicket.ToString("openGId"));
            }
            return new Hash((int)CodeType.Unknown, "成功",shareTicket);
        }
        return new Hash((int)CodeType.OK, "成功");
    }
    /// <summary>
    /// 客户端分享
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="encryptedData">string 群标识加密信息</param>
    /// <param name="iv">string 群标识加密向量</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Share(Hash client, string encryptedData, string iv)
    {
        if (!Genre.IsNull(encryptedData) && !Genre.IsNull(iv) && !client.IsNull("sessionKey"))
        {
            Hash shareTicket = API.GetEncryptedData(encryptedData, client.ToString("sessionKey"), iv);

            ClientShareData.Create(client.ToInt("id"), shareTicket.ToString("openGId"));
            if (!shareTicket.IsNull("openGId"))
            {
                if (ClientGroupData.Create(client.ToInt("id"), shareTicket.ToString("openGId")) > 0)
                {
                    ClientCoinService.Change(client, AvenueType.ShareToGroup, 100, "分享到群奖励");
                }
            }
        }
        return new Hash((int)CodeType.OK, "成功");
    }
    /// <summary>
    /// 发送客服消息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="content">string 消息内容</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Send(Hash client, string content)
    {
        Hash accessToken = APPService.GetAccessToken(client.ToInt("appId"));
        if (accessToken.ToInt("code") == 0)
        {
            if (client.ToDateTime("updateTime").AddHours(24) > DateTime.Now)
            {
                Hash query = new Hash();
                Hash queryText = new Hash();
                queryText["content"] = content;
                query["touser"] = client.ToString("openId");
                query["msgtype"] = "text";
                query["text"] = queryText;
                Hash result = API.Send(accessToken.ToHash("data").ToString("accessToken"), query.ToJSON());
                if (result.ToInt("errcode") == 0)
                {
                    return new Hash((int)CodeType.OK, "成功");
                }
                return new Hash((int)CodeType.WechatAPIError, result.ToString("errmsg"));
            }
            return new Hash((int)CodeType.ClientInaction, "客户端不活跃");
        }
        return new Hash((int)CodeType.WechatAccessTokenInvalid, "accessToken 无效");
    }
    /// <summary>
    /// 记录客户端日志
    /// </summary>
    public static void Log(string session3rd)
    {
        int scene = Parse.ToInt(HttpContext.Current.Request.Params["scene"]);
        string url = HttpContext.Current.Request.Url.AbsolutePath;
        string query = String.Empty;
        foreach (string key in HttpContext.Current.Request.Form.AllKeys)
        {
            query += key + "=" + HttpContext.Current.Request.Form[key] + "&";
        }
        foreach (string key in HttpContext.Current.Request.QueryString.AllKeys)
        {
            query += key + "=" + HttpContext.Current.Request.QueryString[key] + "&";
        }
        if (Parse.ToString(HttpContext.Current.Application[session3rd]) != url + query)
        {
            HttpContext.Current.Application[session3rd] = url + query;
            ClientLogData.Create(session3rd, scene, url, query);
        }
    }   
}