using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端操作类
/// </summary>
public class ClientData
{
    /// <summary>
    /// 生成一个新的客户端编号
    /// </summary>
    /// <returns>int 新客户端编号</returns>
    public static int GetClientIdForCreate()
    {
        string sql = "SELECT MAX(clientId) FROM tc_client";
        using (MySqlADO ado = new MySqlADO())
        {
            return Parse.ToInt(ado.GetValue(sql), 1000000000) + 1;
        }
    }
    /// <summary>
    /// 根据三方会话标识获取客户端编号
    /// </summary>
    /// <param name="session3rd">string 三方会话标识</param>
    /// <returns>int 客户端编号</returns>
    public static int GetClientIdBySession3rd(string session3rd)
    {
        string sql = "SELECT clientId FROM tc_client WHERE session3rd=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, session3rd);
        }
    }
    /// <summary>
    /// 根据 OpenId 获取客户端编号
    /// </summary>
    /// <param name="openId">string 微信标识</param>
    /// <returns>int 客户端编号</returns>
    public static int GetClientIdByOpenId(string openId)
    {
        string sql = "SELECT clientId FROM tc_client WHERE openId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetInt(sql, openId);
        }
    }
    /// <summary>
    /// 根据 ClientId 获取用户信息
    /// </summary>
    /// <param name="clientId">int 用户编号</param>
    /// <returns>Hash 用户信息</returns>
    public static Hash GetByClientId(int clientId)
    {
        string sql = "SELECT * FROM tc_client WHERE clientId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId);
        }
    }
    /// <summary>
    /// 根据客户端获取排行榜
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 排行榜</returns>
    public static Hash GetRank(int clientId)
    {
        string sql = "SELECT *,tcr.relateType,tcr.openGId " +
            "FROM tc_client tc LEFT JOIN tc_client_relate tcr ON tc.clientId=tcr.toClientId AND tcr.fromClientId=@0 " +
            "WHERE tc.actived=1 ORDER BY tc.score DESC, tc.updateTime DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
        }
    }
    /// <summary>
    /// 创建新用户
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="openId">string 微信标识</param>
    /// <param name="sessionKey">string 微信会话标识</param>
    /// <param name="session3rd">string 三方会话标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, string openId, string sessionKey, string session3rd)
    {
        string sql = "INSERT INTO tc_client (clientId,openId,sessionKey,session3rd) " +
            "VALUES(@0,@1,@2,@3) " +
            "ON DUPLICATE KEY UPDATE openId=VALUES(openId),sessionKey=VALUES(sessionKey),session3rd=VALUES(session3rd),updateTime=Now()";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, openId, sessionKey, session3rd);
        }
    }
    /// <summary>
    /// 设置客户端资料
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="nick">string 昵称</param>
    /// <param name="gender">int 性别</param>
    /// <param name="avatarUrl">string 头像图片</param>
    /// <returns>int 受影响的行数</returns>
    public static int SetUserInfo(int clientId, string nick, int gender, string avatarUrl)
    {
        string sql = "UPDATE tc_client SET nick=@1,gender=@2,avatarUrl=@3,lives=IF(IFNULL(actived,0)=0,2,lives),actived=1 WHERE clientId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, nick, gender, avatarUrl);
        }
    }
    /// <summary>
    /// 重新开始
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Restart(int clientId)
    {
        string sql = "UPDATE tc_client SET score=0,status=0 WHERE clientId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId);
        }
    }
    /// <summary>
    /// 复活继续
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="isSkip">bool 是否跳过</param>
    /// <returns>int 受影响的行数</returns>
    public static int Revive(int clientId, bool isSkip)
    {
        string sql = "UPDATE tc_client SET lives=lives-@1,status=0 WHERE clientId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, (isSkip ? 1: 0) );
        }
    }
    /// <summary>
    /// 激活复活卡
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Activate(int clientId)
    {
        string sql = "UPDATE tc_client SET lives=lives+1 WHERE clientId=@0 ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId);
        }
    }
    /// <summary>
    /// 更新得分
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="questionId">int 题目</param>
    /// <param name="degree">int 难度等级</param>
    /// <param name="result">ResultType 答题结果 [ 1 - 正确, 2 - 错误, 3 - 跳过 ]</param>
    /// <returns>int 受影响的行数</returns>
    public static int Score(int clientId, int questionId, int degree, ResultType result)
    {
        string sql = "UPDATE tc_client SET status=status WHERE clientId=@0";
        if (result == ResultType.Correct)
        {
            sql = "UPDATE tc_client SET score=score+@1 WHERE clientId=@0";
        }
        if (result == ResultType.Incorrect)
        {
            sql = "UPDATE tc_client SET status=200 WHERE clientId=@0";
        }
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, degree);
        }
    }
}