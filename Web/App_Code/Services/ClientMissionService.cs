using System;
using Budong.Common.Utils;

/// <summary>
/// MissionService 的摘要说明
/// </summary>
public class ClientMissionService
{
    /// <summary>
    /// 获取关卡详细信息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Detail(Hash client, int missionId)
    {
        Hash mission = ClientMissionData.GetById(missionId);
        mission["subjects"] = ClientMissionSubjectData.List(missionId).ToHashCollection("data");
        return new Hash((int)CodeType.OK, "成功", mission);
    }
    /// <summary>
    /// 创建新关卡
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="title">string 关卡标题</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Create(Hash client, string title)
    {
        if (ClientMissionData.ExistsByTitle(title, 0))
        {
            return new Hash((int)CodeType.MissionTitleExists, "关卡重名");
        }
        if (ClientMissionData.Create(client.ToInt("id"), title) > 0)
        {
            Hash mission = ClientMissionData.GetByTitle(client.ToInt("id"), title);
            return new Hash((int)CodeType.OK, "成功", mission);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 修改关卡名称
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="title">string 关卡标题</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Edit(Hash client, int missionId, string title)
    {
        if (ClientMissionData.ExistsByTitle(title, missionId))
        {
            return new Hash((int)CodeType.MissionTitleExists, "关卡重名");
        }
        if (ClientMissionData.Edit(missionId, title) > 0)
        {
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 更新关卡信息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Update(Hash client, int missionId)
    {
        int subjectCount = ClientMissionData.GetSubjectCount(missionId);
        string tags = String.Empty;
        object[] categoryIds = ClientMissionData.GetCategoryIds(missionId);
        foreach (object categoryId in categoryIds)
        {
            tags += (String.IsNullOrEmpty(tags) ? "" : ",") + Filter.categoryIdToName(Parse.ToInt(categoryId));
        }
        if (ClientMissionData.Update(missionId, tags, subjectCount) > 0)
        {
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 删除关卡
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Delete(Hash client, int missionId)
    {
        if (ClientMissionData.Delete(missionId) > 0)
        {
            HashCollection subjects = ClientMissionSubjectData.List(missionId).ToHashCollection("data");
            foreach (Hash subject in subjects)
            {
                ClientMissionSubjectService.Delete(client, subject.ToInt("id"));
            }
            ClientMissionData.Clear(missionId);
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 获取用户关卡列表
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash List(Hash client)
    {
        Hash missions = ClientMissionData.List(client.ToInt("id"));
        return new Hash((int)CodeType.OK, "成功", missions);
    }
}