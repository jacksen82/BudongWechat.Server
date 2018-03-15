using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端金币操作类
/// </summary>
public class ClientCoinData
{
    /// <summary>
    /// 插入分享记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="avenue">AvenueType 途径</param>
    /// <param name="amount">int 金额</param>
    /// <param name="balance">int 余额</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, AvenueType avenue, int amount, int balance)
    {
        string sql = "INSERT INTO tc_client_coin (clientId,avenue,amount,balance) VALUES(@0,@1,@2,@3)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, (int)avenue, amount, balance);
        }
    }
}