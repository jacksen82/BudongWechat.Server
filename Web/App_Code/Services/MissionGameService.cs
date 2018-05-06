﻿using System;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// MissionGameService 的摘要说明
/// </summary>
public class MissionGameService
{
    /// <summary>
    /// 获取答案提示
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Tip(Hash client, int subjectId)
    {
        Hash subject = MissionSubjectData.GetById(subjectId);
        ClientCoinService.Change(client, AvenueType.GameTip, -30, "查看答案提示");
        return new Hash((int)CodeType.OK, "成功");
    }
    /// <summary>
    /// 跳过题目
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <param name="secondCount">int 耗时，单位：秒</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Skip(Hash client, int subjectId, int secondCount)
    {
        Hash subject = MissionSubjectData.GetById(subjectId);
        ClientCoinService.Change(client, AvenueType.GameSkip, -30, "跳过题目");
        MissionSubjectClientData.Create(client.ToInt("id"), subject.ToInt("missionId"), subjectId, 10, secondCount);
        MissionClientData.Update(client.ToInt("id"), subject.ToInt("missionId"));
        return new Hash((int)CodeType.OK, "成功");
    }
    /// <summary>
    /// 重听题目音频
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Replay(Hash client, int subjectId)
    {
        Hash subject = MissionSubjectData.GetById(subjectId);
        ClientCoinService.Change(client, AvenueType.GameReplay, -10, "重听题目音频");
        return new Hash((int)CodeType.OK, "成功");
    }
    /// <summary>
    /// 获取题目信息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <param name="fromClientId">int 来源客户端编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Help(Hash client, int missionId, int subjectId, int fromClientId)
    {
        Hash mission = MissionData.GetById(missionId);
        Hash subject = MissionSubjectData.GetById(subjectId);
        mission["subject"] = subject;
        mission["author"] = ClientData.GetById(mission.ToInt("clientId"));
        mission["fromClient"] = ClientData.GetById(fromClientId);
        return new Hash((int)CodeType.OK, "成功", mission);
    }
    /// <summary>
    /// 答题成功
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <param name="secondCount">int 耗时，单位：秒</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Answer(Hash client, int subjectId, int secondCount)
    {
        Hash subject = MissionSubjectData.GetById(subjectId);
        MissionSubjectClientData.Create(client.ToInt("id"), subject.ToInt("missionId"), subjectId, 100, secondCount);
        MissionClientData.Update(client.ToInt("id"), subject.ToInt("missionId"));
        return new Hash((int)CodeType.OK, "成功");
    }
    /// <summary>
    /// 完成关卡
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Complete(Hash client, int missionId)
    {
        Hash data = new Hash();
        Hash mission = MissionClientData.GetByIdAndClientId(missionId, client.ToInt("id"));
        MissionClientData.First(client.ToInt("id"), missionId);
        if (mission.ToInt("clientId") != client.ToInt("id") && 
            mission.ToInt("subjectIndex") >= mission.ToInt("subjectCount") && 
            mission.ToInt("first") == 0)
        {
            data["coins"] = mission.ToInt("score") * 10;
            ClientCoinService.Change(client, AvenueType.GameSuccess, mission.ToInt("score") * 10, "闯关成功奖励");
        }
        else
        {
            data["coins"] = 0;
        }
        return new Hash((int)CodeType.OK, "成功", data);
    }
    /// <summary>
    /// 开始关卡
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Start(Hash client, int missionId)
    {
        Hash mission = MissionData.GetById(missionId);
        Hash missionClient = MissionClientData.GetByIdAndClientId(missionId, client.ToInt("id"));
        if (missionClient.ToInt("missionClientId") == 0) 
        {
            MissionClientData.Create(client.ToInt("id"), missionId);
        }
        if (missionClient.ToInt("subjectIndex") >= mission.ToInt("subjectCount"))
        {
            MissionClientData.Clear(client.ToInt("id"), missionId);
            MissionClientData.Update(client.ToInt("id"), missionId);
        }
        MissionData.Update(missionId);
        return MissionService.Detail(client, missionId);
    }
    /// <summary>
    /// 关卡排行榜
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Rank(Hash client, int missionId)
    {
        Hash mission = MissionClientData.GetByIdAndClientId(missionId, client.ToInt("id"));
        HashCollection playersTop10 = MissionClientData.RankTop10(client.ToInt("id"), missionId).ToHashCollection("data");
        HashCollection playersInFriend= MissionClientData.RankInFriend(client.ToInt("id"), missionId).ToHashCollection("data");

        mission["author"] = ClientData.GetById(mission.ToInt("clientId"));
        mission["players"] = playersTop10;
        mission["playersTop10"] = playersTop10;
        mission["playersInFriend"] = playersInFriend;
        return new Hash((int)CodeType.OK, "成功", mission);
    }
}