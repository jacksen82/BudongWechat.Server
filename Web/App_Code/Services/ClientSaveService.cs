using System;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// 客户端复活卡业务操作类
/// </summary>
public class ClientSaveService
{
    /// <summary>
    /// 获取激活复活卡记录
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash List(Hash token)
    {
        return new Hash((int)CodeType.OK, "成功", ClientSaveData.List(token.ToInt("clientId")));
    }

    /// <summary>
    /// 赠送复活卡
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <param name="fromClientId">int 邀请人客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Save(Hash token, int fromClientId, string openGId)
    {
        if (token.ToInt("clientId") == fromClientId)
        {
            return new Hash((int)CodeType.ClientRelateInvalid, "不能为自己激活复活卡");
        }
        Hash data = ClientData.GetByClientId(fromClientId);
        if (ClientSaveData.Save(token.ToInt("clientId"), fromClientId, openGId) > 0)
        {
            ClientData.Save(fromClientId);

            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
}