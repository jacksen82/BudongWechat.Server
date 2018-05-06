using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端金币操作类
/// </summary>
public class ClientCoinData
{
    /// <summary>
    /// 获取最后一条记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="avenue">AvenueType 途径</param>
    /// <returns>Hash 返回详情</returns>
    public static Hash Last(int clientId, AvenueType avenue)
    {
        string sql = "SELECT * FROM tc_client_coin WHERE clientId=@0 AND avenue=@1 ORDER BY createTime DESC LIMIT 1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId, (int)avenue);
        }
    }
    /// <summary>
    /// 获取金币账户明细
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="pageId">int 页码</param>
    /// <param name="pageSize">int 页尺寸</param>
    /// <returns>Hash 返回结果集合</returns>
    public static Hash List(int clientId, int pageId, int pageSize)
    {
        string sql = "SELECT * FROM tc_client_coin WHERE clientId=@0 ORDER BY createTime DESC";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHashCollectionByPageId(sql, pageId, pageSize, clientId);
        }
    }
    /// <summary>
    /// 插入金币记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="avenue">AvenueType 途径</param>
    /// <param name="amount">int 金额</param>
    /// <param name="balance">int 余额</param>
    /// <param name="text">string 描述</param>
    /// <returns>int 受影响的行数</returns>
    public static int Create(int clientId, AvenueType avenue, int amount, int balance, string text)
    {
        string sql = "INSERT INTO tc_client_coin (clientId,avenue,amount,balance,text) VALUES(@0,@1,@2,@3,@4)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, (int)avenue, amount, balance,text);
        }
    }
}