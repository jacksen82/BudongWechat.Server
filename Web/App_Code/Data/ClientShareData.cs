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
    /// <param name="shareQuestionId">int 分享题目编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, string shareFrom, string shareAction, int shareQuestionId)
    {
        string sql = "INSERT INTO tc_client_share (clientId, shareFrom, shareAction, shareQuestionId) VALUES(@0, @1, @2, @3)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, shareFrom, shareAction, shareQuestionId);
        }
    }
}