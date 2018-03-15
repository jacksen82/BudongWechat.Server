using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端群关系操作类
/// </summary>
public class ClientGroupData
{
    /// <summary>
    /// 根据客户端编号群标识获取群关系信息
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>Hash 详细信息</returns>
    public static Hash GetByClientIdAndOpenId(int clientId, string openGId)
    {
        string sql = "SELECT id FROM tc_client_group WHERE clientId=@0 AND openGId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId, openGId);
        }
    }
    /// <summary>
    /// 添加客户端群关系
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, string openGId)
    {
        string sql = "INSERT INTO tc_client_group (clientId, openGId) VALUES(@0,@1) ON DUPLICATE KEY UPDATE clientId=@0 AND openGId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, openGId);
        }
    }
}