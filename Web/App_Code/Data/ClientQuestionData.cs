using System;
using Budong.Common.Data;
using Budong.Common.Utils;

/// <summary>
/// 客户端题目操作类
/// </summary>
public class ClientQuestionData
{
    /// <summary>
    /// 获取客户端回答指定题目的信息
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="questionId">int 题目编号</param>
    /// <returns>Hash 答题信息</returns>
    public static Hash GetByClientIdAndQuestionId(int clientId, int questionId)
    {
        string sql = "SELECT * FROM tc_client_question WHERE clientId=@0 AND questionId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId, questionId);
        }
    }
    /// <summary>
    /// 获取客户端进度信息
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 进度信息</returns>
    public static Hash GetPositionByClientId(int clientId)
    {
        string sql = "SELECT tc.*, " +
            "   (SELECT COUNT(*) FROM tc_client WHERE score>tc.score) AS rankIndex, " +
            "   ((SELECT COUNT(*) FROM tc_client WHERE score<=tc.score AND clientId<>tc.clientId) * 100 / (SELECT COUNT(*) FROM tc_client)) AS rankPosition, " +
            "   (SELECT COUNT(*) FROM tc_client_question WHERE clientId=@0 AND result=1) AS questionCorrect, " +
            "   (SELECT COUNT(*) FROM tc_client_question WHERE clientId=@0) AS questionAnswered, " +
            "   (SELECT COUNT(*) FROM tq_question) AS questionAmount " +
            "FROM tc_client tc WHERE clientId=@0 ";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.GetHash(sql, clientId);
        }
    }
    /// <summary>
    /// 获取下一个题目
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>Hash 题目集合</returns>
    public static Hash Next(int clientId)
    {
        string sql = "SELECT td.*,IFNULL(tcd.result, -1) AS result " +
            "FROM tq_question td LEFT JOIN tc_client_question tcd ON td.questionId=tcd.questionId AND clientId=@0 " +
            "WHERE IFNULL(tcd.result, -1) IN(-1, 0, 2) " + 
            "ORDER BY IFNULL(tcd.result, -1) DESC, td.degree ASC ";
        using (MySqlADO ado = new MySqlADO())
        {
            HashCollection items = ado.GetHashCollection(sql, clientId).ToHashCollection("data");
            if (items.Count > 0)
            {
                int degree = items[0].ToInt("degree");
                int maxDegree = 1;
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ToInt("degree") > degree)
                    {
                        maxDegree = i;
                        break;
                    }
                }
                if (items[0].ToInt("result") == 0 || items[0].ToInt("result") == 2)
                {
                    return items[0];
                }
                Random random = new Random();
                return items[random.Next(0, maxDegree)];
            }
            return new Hash();
        }
    }
    /// <summary>
    /// 准备答题
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="questionId">int 题目编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Ready(int clientId, int questionId)
    {
        string sql = "INSERT INTO tc_client_question (clientId,questionId,result) VALUES(@0,@1,0) " +
            "ON DUPLICATE KEY UPDATE clientId=VALUES(clientId),questionId=VALUES(questionId),result=VALUES(result)";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, questionId);
        }
    }
    /// <summary>
    /// 答题结果
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <param name="questionId">int 题目编号</param>
    /// <param name="result">ResultType 答题结果</param>
    /// <returns>int 受影响的行数</returns>
    public static int Answer(int clientId, int questionId, ResultType result)
    {
        string sql = "UPDATE tc_client_question SET result=@2 WHERE clientId=@0 AND questionId=@1";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId, questionId, (int)result);
        }
    }
    /// <summary>
    /// 清空记录
    /// </summary>
    /// <param name="clientId">int 客户端编号</param>
    /// <returns>int 受影响的行数</returns>
    public static int Clear(int clientId)
    {
        string sql = "DELETE FROM tc_client_question WHERE clientId=@0";
        using (MySqlADO ado = new MySqlADO())
        {
            return ado.NonQuery(sql, clientId);
        }
    }
}