using System;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// 游戏业务操作类
/// </summary>
public class GameService
{
    /// <summary>
    /// 使用复活卡
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Revive(Hash token)
    {
        //  如果状态不为中止，则跳出
        if (token.ToInt("status") != 200)
        {
            return new Hash((int)CodeType.OK, "成功", ClientData.GetByClientId(token.ToInt("clientId")));
        }

        //  如果没有可用的复活卡，则跳出
        if (token.ToInt("lives") == 0)
        {
            return new Hash((int)CodeType.ClientHaveNotCard, "没有可用复活卡");
        }

        //  数据库操作
        if (ClientData.Revive(token.ToInt("clientId")) > 0)
        {
            //  获取用户游戏进度信息
            Hash data = ClientQuestionData.GetPositionByClientId(token.ToInt("clientId"));

            //  返回成功结果
            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Restart(Hash token)
    {
        //  清空用户答题记录及得分
        ClientData.Restart(token.ToInt("clientId"));
        ClientQuestionData.Clear(token.ToInt("clientId"));

        //  获取用户游戏进度信息
        Hash data = ClientQuestionData.GetPositionByClientId(token.ToInt("clientId"));

        //  返回成功结果
        return new Hash((int)CodeType.OK, "成功", data);
    }
    /// <summary>
    /// 开始游戏获取题目
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Next(Hash token)
    {
        //  获取下一个题目
        Hash question = ClientQuestionData.Next(token.ToInt("clientId"));
        Hash data = ClientQuestionData.GetPositionByClientId(token.ToInt("clientId"));

        //  准备开始答题
        if (question.ToInt("questionId") > 0)
        {
            ClientQuestionData.Ready(token.ToInt("clientId"), question.ToInt("questionId"));
            data = ClientQuestionData.GetPositionByClientId(token.ToInt("clientId"));
            data["question"] = question;
        }

        //  返回结果
        return new Hash((int)CodeType.OK, "成功", data);
    }
    /// <summary>
    /// 答题结果
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <param name="questionId">int 题目编号</param>
    /// <param name="result">int 答题结果</param>
    /// <returns>Hash 结果信息</returns>
    public static Hash Answer(Hash token, int questionId, int result)
    {
        //  记录答题结果
        if (ClientQuestionData.Answer(token.ToInt("clientId"), questionId, result) > 0)
        {
            //  更新用户进度和得分
            ClientData.Score(token.ToInt("clientId"), result);

            //  获取用户进度信息
            Hash data = ClientQuestionData.GetPositionByClientId(token.ToInt("clientId"));

            //  返回结果
            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 赠送复活卡
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <param name="toClientId">int 求助客户端编号</param>
    /// <param name="openGId">string 群标识</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Save(Hash token, int toClientId, string openGId)
    {
        //  不能复活自己
        if (token.ToInt("clientId") == toClientId)
        {
            return new Hash((int)CodeType.ClientRelateInvalid, "不能为自己激活复活卡");
        }

        //  获取用户信息
        if (ClientSaveData.Save(token.ToInt("clientId"), toClientId, openGId) > 0)
        {
            //  更新用户复活卡
            ClientData.Activate(toClientId);

            //  获取用户信息
            Hash data = ClientQuestionData.GetPositionByClientId(token.ToInt("clientId"));

            //  返回结果
            return new Hash((int)CodeType.OK, "成功", data);
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 获取排行榜信息
    /// </summary>
    /// <param name="token">Hash 客户端信息</param>
    /// <returns>Hash 排行榜</returns>
    public static Hash Rank(Hash token)
    {
        Hash data = new Hash();
        HashCollection all = ClientData.GetRank(token.ToInt("clientId")).ToHashCollection("data");
        HashCollection global = new HashCollection();
        HashCollection friend = new HashCollection();
        for (int i = 0; i < all.Count; i++)
        {
            if (i < 10)
            {
                global.Add(all[i]);
            }
            if (all[i].ToInt("relateType") > 0 || all[i].ToInt("clientId") == token.ToInt("clientId"))
            {
                friend.Add(all[i]);
            }
        }
        data["global"] = global;
        data["friend"] = friend;
        return new Hash((int)CodeType.OK, "成功", data);
    }
}