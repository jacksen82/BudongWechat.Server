using System;
using System.IO;
using System.Web;
using Budong.Common.Utils;

/// <summary>
/// ClientMissionSubjectService 的摘要说明
/// </summary>
public class ClientMissionSubjectService
{
    /// <summary>
    /// 获取题目详细信息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Detail(Hash client, int subjectId)
    {
        Hash subject = MissionSubjectData.GetById(subjectId);
        return new Hash((int)CodeType.OK, "成功", subject);
    }
    /// <summary>
    /// 添加新题目
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="title">string 题目答案</param>
    /// <param name="tip">string 答案提示</param>
    /// <param name="categoryId">int 题目分类</param>
    /// <param name="mp3file">HttpPostedFile 音频文件</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Add(Hash client, int missionId, string title, string tip, int categoryId, HttpPostedFile mp3file)
    {
        title = Parse.ToString(title).Trim();
        tip = Parse.ToString(tip).Trim();
        if (mp3file == null || mp3file.InputStream == null || mp3file.ContentLength == 0)
        {
            return new Hash((int)CodeType.SubjectMP3Empty, "音频为空");
        }
        if (!mp3file.FileName.EndsWith("mp3", StringComparison.CurrentCultureIgnoreCase))
        {
            return new Hash((int)CodeType.SubjectMP3Unvalid, "音频格式不对");
        }
        if (MissionSubjectData.ExistsByTitle(missionId, title, 0))
        {
            return new Hash((int)CodeType.SubjectTitleExists, "题目重复");
        }
        int index = MissionSubjectData.NewIndex(missionId);
        string mp3Url = "/Uploads/MP3File/audio_" + missionId + "_" + Files.NewFileName(".mp3");
        mp3file.SaveAs(HttpContext.Current.Server.MapPath(mp3Url));
        if (!File.Exists(HttpContext.Current.Server.MapPath(mp3Url)))
        {
            return new Hash((int)CodeType.SubjectTitleExists, "文件保存失败");
        }
        if (MissionSubjectData.Add(client.ToInt("id"), missionId, title, tip, categoryId, Filter.categoryIdToName(categoryId), index, mp3Url) > 0)
        {
            MissionData.Update(missionId); //  更新关卡标签及题目数量
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 修改题目信息
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="missionId">int 关卡编号</param>
    /// <param name="subjectId">int 题目编号</param>
    /// <param name="title">string 题目答案</param>
    /// <param name="tip">string 答案提示</param>
    /// <param name="categoryId">int 题目分类</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Edit(Hash client, int missionId, int subjectId, string title, string tip, int categoryId)
    {
        title = Parse.ToString(title).Trim();
        tip = Parse.ToString(tip).Trim();
        if (MissionSubjectData.ExistsByTitle(missionId, title, subjectId))
        {
            return new Hash((int)CodeType.SubjectTitleExists, "题目重复");
        }
        if (MissionSubjectData.Edit(subjectId, title, tip, categoryId, Filter.categoryIdToName(categoryId)) > 0)
        {
            MissionData.Update(missionId); //  更新关卡标签及题目数量
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
    /// <summary>
    /// 删除题目
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Delete(Hash client, int subjectId)
    {
        Hash subject = MissionSubjectData.GetById(subjectId);
        if (MissionSubjectData.Delete(subjectId) > 0)
        {
            Files.Delete(subject.ToString("mp3Url"));
            MissionData.Update(subject.ToInt("missionId")); //  更新关卡标签及题目数量
            MissionSubjectData.Clear(subjectId);  //  清理题目痕迹
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
}