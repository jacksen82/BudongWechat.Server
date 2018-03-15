using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端操作类
/// </summary>
public class ClientData
{
    /// <summary>
    /// 根据 ClientId 获取用户信息
    /// </summary>
    /// <param name="clientId">int 用户编号</param>
    /// <returns>Hash 用户信息</returns>
    public static Hash GetById(int clientId)
    {
        string sql = "SELECT * FROM tc_client WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId);
        }
    }
    /// <summary>
    /// 根据 OpenId 获取用户信息
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="openId">string 微信标识</param>
    /// <returns>Hash 用户信息</returns>
    public static Hash GetByOpenId(int appId, string openId)
    {
        string sql = "SELECT * FROM tc_client WHERE appId=@0 AND openId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, appId, openId);
        }
    }
    /// <summary>
    /// 根据三方会话标识获取用户信息
    /// </summary>
    /// <param name="session3rd">string 三方会话标识</param>
    /// <returns>Hash 用户信息</returns>
    public static Hash GetBySession3rd(string session3rd)
    {
        string sql = "SELECT * FROM tc_client WHERE EXISTS(SELECT * FROM tc_client_session WHERE appId=tc_client.appId AND openId=tc_client.openId AND session3rd=@0)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, session3rd);
        }
    }
    /// <summary>
    /// 创建新用户
    /// </summary>
    /// <param name="appId">int 应用编号</param>
    /// <param name="openId">string 微信标识</param>
    /// <param name="unionId">string 开放平台标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int appId, string openId, string unionId)
    {
        string sql = "INSERT INTO tc_client (appId,openId,unionId) VALUES(@0,@1,@2) ON DUPLICATE KEY UPDATE appId=@0 AND openId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, appId, openId, unionId);
        }
    }
    /// <summary>
    /// 更新用户资料
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="nick">string 昵称</param>
    /// <param name="gender">int 性别</param>
    /// <param name="avatarUrl">string 头像图片</param>
    /// <returns>int 受影响的行数</returns>
    public static int Update(int clientId, string nick, int gender, string avatarUrl)
    {
        string sql = "UPDATE tc_client SET nick=@1,gender=@2,avatarUrl=@3 WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, nick, gender, avatarUrl);
        }
    }
    /// <summary>
    /// 更新用户余额
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="balance">int 余额</param>
    /// <returns>int 受影响的行数</returns>
    public static int Update(int clientId, int balance)
    {
        string sql = "UPDATE tc_client SET balance=@1 WHERE id=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, balance);
        }
    }
}