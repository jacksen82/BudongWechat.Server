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
        //  如果 session3rd 为空，则跳出
        if (Genre.IsNull(session3rd))
        {
            return new Hash((int)CodeType.Session3rdRequired, "session3rd 为空");
        }

        //  查询客户端编号是否存在
        int clientId = ClientData.GetClientIdBySession3rd(session3rd);

        //  如果客户端存在
        if (clientId > 0)
        {
            //  获取用户信息
            Hash data = ClientData.GetByClientId(clientId);
            Hash setting = SettingData.Detail();

            //  附加 APP 配置信息
            foreach (string key in setting.Keys)
            {
                if (key.StartsWith("page"))
                {
                    data[key] = setting[key];
                }
            }

            //  返回成功结果
            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.ClientNotExists, "session3rd 无效");
    }
    /// <summary>
    /// 登录方法
    /// </summary>
    /// <param name="code">string 微信登录凭证</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Login(string code)
    {
        //  如果 code 为空，则跳出
        if (Genre.IsNull(code))
        {
            return new Hash((int)CodeType.CodeRequired, "code 为空");
        }

        //  用 code 换取微信客户端信息
        Hash setting = SettingData.Detail();
        Hash wechatSession = API.Code2Session(setting.ToString("appKey"), setting.ToString("appSecret"), code);
        if (wechatSession.ToInt("errcode") != 0)
        {
            return new Hash((int)CodeType.CodeInvalid, wechatSession.ToString("errmsg"));
        }

        //  初始化新客户端信息
        int clientId = ClientData.GetClientIdForCreate();
        string openId = wechatSession.ToString("openid");
        string sessionKey = wechatSession.ToString("session_key");

        //  创建新客户端
        if (ClientData.Create(clientId, openId, sessionKey, Guid.NewGuid().ToString().Replace("-", "")) > 0)
        {
            //  重新获取客户端编号
            clientId = ClientData.GetClientIdByOpenId(openId);

            //  查询客户端信息
            Hash data = ClientData.GetByClientId(clientId);

            //  附加 APP 配置信息
            foreach (string key in setting.Keys)
            {
                if (key.StartsWith("page"))
                {
                    data[key] = setting[key];
                }
            }

            //  返回成功结果
            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 设置客户端资料
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <param name="nick">string 昵称</param>
    /// <param name="gender">int 性别</param>
    /// <param name="avatarUrl">string 头像</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash SetUserInfo(Hash token, string nick, int gender, string avatarUrl)
    {
        if (ClientData.SetUserInfo(token.ToInt("clientId"), nick, gender, avatarUrl) > 0)
        {
            //  获取用户信息
            Hash data = ClientData.GetByClientId(token.ToInt("clientId"));

            //  返回成功结果
            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 分享方法
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <param name="shareFrom">string 分享来源</param>
    /// <param name="shareAction">string 分享目的</param>
    /// <param name="shareQuestionId">int 分享题目编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Share(Hash token, string shareFrom, string shareAction, int shareQuestionId)
    {
        if (ClientShareData.Create(token.ToInt("clientId"), shareFrom, shareAction, shareQuestionId) > 0)
        {
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 建立客户端关系
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <param name="fromClientId">int 邀请人客户端编号</param>
    /// <param name="shareQuestionId">int 分享题目编号</param>
    /// <param name="scene">int 来源场景</param>
    /// <param name="encryptedData">string 群标识加密信息</param>
    /// <param name="iv">string 群标识加密向量</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Relation(Hash token, int fromClientId, int shareQuestionId, int scene, string encryptedData, string iv)
    {
        //  如果邀请人不存在，则跳出
        if (fromClientId == 0)
        {
            return new Hash((int)CodeType.ClientRelateRequired, "邀请人不存在");
        }

        //  初始化参数集合
        Hash data = new Hash();
        Hash shareTicket = new Hash();
        Hash fromClient = ClientData.GetByClientId(fromClientId);

        //  获取群标识
        if (!Genre.IsNull(encryptedData) && !Genre.IsNull(iv) && !token.IsNull("sessionKey"))
        {
            shareTicket = API.GetEncryptedData(encryptedData, token.ToString("sessionKey"), iv);
        }

        if (shareTicket.IsNull("openGId"))
        {
            //  如果不是来自群会话
            if (token.ToInt("clientId") != fromClientId)
            {
                ClientRelateData.Create(token.ToInt("clientId"), fromClientId, RelateType.FromFriend);
                ClientRelateData.Create(fromClientId, token.ToInt("clientId"), RelateType.FromFriend);
            }
        }
        else
        {
            //  如果来自群会话
            ClientGroupData.Create(token.ToInt("clientId"), shareTicket.ToString("openGId"));
            ClientGroupData.Renovate(shareTicket.ToString("openGId"));
        }

        //  初始化题目
        if (shareQuestionId > 0)
        {
            Hash question = ClientQuestionData.GetByClientIdAndQuestionId(token.ToInt("clientId"), shareQuestionId);
            if (question.ToInt("id") == 0)
            {
                ClientQuestionData.Ready(token.ToInt("clientId"), shareQuestionId);
            }
        }

        //  拼装结果信息
        data["openGId"] = shareTicket.ToString("openGId");
        data["fromClient"] = fromClient;
        return new Hash((int)CodeType.OK, "成功", data);
    }
    /// <summary>
    /// 获取激活复活卡记录
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Lives(Hash token)
    {
        return new Hash((int)CodeType.OK, "成功", ClientLiveData.List(token.ToInt("clientId")));
    }
}