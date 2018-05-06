using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 小程序会话状态操作类
/// </summary>
public class ClientSessionData
{
    /// <summary>
    /// 根据三方会话标识获取会话信息
    /// </summary>
    /// <param name="session3rd">string 三方会话标识</param>
    /// <returns>Hash 操作结果</returns>
    public static Hash GetBySession3rd(string session3rd)
    {
        string sql = "SELECT * FROM tc_client_session WHERE session3rd=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, session3rd);
        }
    }
    /// <summary>
    /// 根据应用编号及 OpenId 获取最后一个有效的 SessionKey
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="openId">string 微信标识</param>
    /// <returns>Hash 操作结果</returns>
    public static Hash GetLastByOpenId(int appId, string openId)
    {
        string sql = "SELECT * FROM tc_client_session WHERE appId=@0 AND openId=@1 ORDER BY createTime DESC LIMIT 1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, appId, openId);
        }
    }
    /// <summary>
    /// 获取会员信息
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="openId">string 微信标识</param>
    /// <param name="sessionKey">string 会话密钥</param>
    /// <returns>Hash 操作结果</returns>
    public static Hash GetByOpenIdAndSessionKey(int appId, string openId, string sessionKey)
    {
        string sql = "SELECT * FROM tc_client_session WHERE appId=@0 AND openId=@1 AND sessionKey=@2";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, appId, openId, sessionKey);
        }
    }
    /// <summary>
    /// 生成新的会话标识
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="openId">string 微信标识</param>
    /// <param name="sessionKey">string 会话密钥</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int appId, string openId, string sessionKey)
    {
        string sql = "INSERT INTO tc_client_session (appId,openId,sessionKey,session3rd) VALUES(@0,@1,@2,@3) ON DUPLICATE KEY UPDATE appId=VALUES(appId), openId=VALUES(openId) AND sessionKey=VALUES(sessionKey)";
        string session3rd = Guid.NewGuid().ToString().Replace("-", "");
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, appId, openId, sessionKey, session3rd);
        }
    }
}