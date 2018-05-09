using System;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// MissionService 的摘要说明
/// </summary>
public class MissionService
{
    /// <summary>
    /// 获取关卡列表
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="pageId">int 页码</param>
    /// <param name="pageSize">int 页尺寸</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash List(Hash client, int pageId, int pageSize)
    {
        Hash missions = MissionData.List(client.ToInt("id"), pageId, pageSize);
        return new Hash((int)CodeType.OK, "成功", missions);
    }
    /// <summary>
    /// 获取待审核关卡列表
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="pageId">int 页码</param>
    /// <param name="pageSize">int 页尺寸</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash More(Hash client, int pageId, int pageSize)
    {
        Hash missions = MissionData.More(client.ToInt("id"), pageId, pageSize);
        return new Hash((int)CodeType.OK, "成功", missions);
    }
    /// <summary>
    /// 获取关卡详细信息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Detail(Hash client, int missionId)
    {
        Hash mission = MissionClientData.GetByIdAndClientId(missionId, client.ToInt("id"));
        mission["author"] = ClientData.GetById(mission.ToInt("clientId"));
        mission["subjects"] = MissionSubjectData.List(missionId).ToHashCollection("data");
        return new Hash((int)CodeType.OK, "成功", mission);
    }
}