using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端分享操作类
/// </summary>
public class ClientShareData
{
    /// <summary>
    /// 分享记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="shareFrom">string 分享来源</param>
    /// <param name="shareAction">string 分享目的</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, string shareFrom, string shareAction)
    {
        string sql = "INSERT INTO tc_client_share (clientId, shareFrom, shareAction) VALUES(@0, @1, @2)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, shareFrom);
        }
    }
}