using System;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// APP 业务操作类
/// </summary>
public class APPService
{
    /// <summary>
    /// 根据 appId 获取 accessToken 信息
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash GetAccessToken(int appId)
    {
        Hash app = APPData.GetById(appId);
        if (app.ToInt("id") > 0)
        {
            if (app.IsNull("accessToken") || app.ToDateTime("accessTokenExpireIn") < DateTime.Now)
            {
                Hash accessToken = API.GetAccessToken(app.ToString("appKey"), app.ToString("appSecret"));
                if (accessToken.ToInt("errcode") == 0)
                {
                    APPData.SetAccessToken(app.ToInt("id"), accessToken.ToString("access_token"), DateTime.Now.AddSeconds(accessToken.ToInt("expires_in")));
                    return new Hash((int)CodeType.OK, "成功", APPData.GetById(appId));
                }
            }
            return new Hash((int)CodeType.OK, "成功", app);
        }
        return new Hash((int)CodeType.WechatAPPNotExists, "app 不存在");
    }
}