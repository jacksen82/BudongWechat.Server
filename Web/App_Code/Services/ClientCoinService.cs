using System;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// ClientCoinService 的摘要说明
/// </summary>
public class ClientCoinService
{
    /// <summary>
    /// 用户金币账户操作
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="avenue">AvenueType 来源场景</param>
    /// <param name="amount">int 金额</param>
    /// <param name="text">string 描述</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Change(Hash client, AvenueType avenue, int amount, string text)
    {
        if (amount != 0)
        {
            if (ClientCoinData.Create(client.ToInt("id"), avenue, amount, amount + client.ToInt("balance"), text) > 0)
            {
                if (ClientData.UpdateBalance(client.ToInt("id"), amount + client.ToInt("balance")) > 0)
                {
                    return new Hash((int)CodeType.OK, "成功");
                }
            }
            return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
        }
        return new Hash((int)CodeType.CoinZero, "金额不能为零");
    }
    /// <summary>
    /// 获取用户金币账户明细
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="pageId">int 页码</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash List(Hash client, int pageId)
    {
        Hash coins = ClientCoinData.List(client.ToInt("id"), pageId);
        return new Hash((int)CodeType.OK, "成功", coins);
    }
}