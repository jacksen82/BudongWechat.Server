using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端好友关系操作类
/// </summary>
public class ClientFriendData
{
    /// <summary>
    /// 根据客户端编号获取好友关系信息
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="fromClientId">int 好友客户端编号</param>
    /// <returns>Hash 详细信息</returns>
    public static Hash GetByClientIdAndFromClientId(int clientId, int fromClientId)
    {
        string sql = "SELECT id FROM tc_client_friend WHERE clientId=@0 AND friendClientId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId, fromClientId);
        }
    }
    /// <summary>
    /// 添加客户端好友关系
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="fromClientId">int 好友客户端编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, int fromClientId)
    {
        string sql = "INSERT INTO tc_client_friend (clientId, friendClientId) VALUES(@0,@1) ON DUPLICATE KEY UPDATE clientId=VALUES(clientId), friendClientId=VALUES(friendClientId)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, fromClientId);
        }
    }
}