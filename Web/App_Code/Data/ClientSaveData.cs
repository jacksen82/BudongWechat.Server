using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// ClientSavedData 的摘要说明
/// </summary>
public class ClientSaveData
{
    /// <summary>
    /// 根据复活卡激活列表
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 激活列表</returns>
    public static Hash List(int clientId)
    {
        string sql = "SELECT tcs.*,tc.avatarUrl,tc.nick,tc.gender FROM tc_client_save tcs LEFT JOIN tc_client tc ON tcs.clientId=tc.clientId WHERE tcs.saveClientId=@0 ORDER BY tcs.createTime DESC ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollection(sql, clientId);
        }
    }
    /// <summary>
    /// 发放复活卡
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="saveClientId">int 激活客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>int 受影响的行数</returns>
    public static int Save(int clientId, int saveClientId, string openGId)
    {
        string sql = "INSERT INTO tc_client_save (clientId,saveClientId,openGId) VALUES(@0,@1,@2)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, saveClientId, openGId);
        }
    }
}