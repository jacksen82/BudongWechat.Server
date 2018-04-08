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
        if (mp3file == null || mp3file.InputStream == null || mp3file.ContentLength == 0)
        {
            return new Hash((int)CodeType.SubjectMP3Empty, "音频为空");
        }
        if (!mp3file.FileName.EndsWith("mp3", StringComparison.CurrentCultureIgnoreCase))
        {
            return new Hash((int)CodeType.SubjectMP3Unvalid, "音频格式不对");
        }
        if (ClientMissionSubjectData.ExistsByTitle(missionId, title, 0))
        {
            return new Hash((int)CodeType.SubjectTitleExists, "题目重复");
        }
        int index = ClientMissionSubjectData.NewIndex(missionId);
        string mp3Url = "/Uploads/MP3File/" + mp3file.FileName;
        mp3file.SaveAs(HttpContext.Current.Server.MapPath(mp3Url));
        if (!File.Exists(HttpContext.Current.Server.MapPath(mp3Url)))
        {
            return new Hash((int)CodeType.SubjectTitleExists, "文件保存失败");
        }
        if (ClientMissionSubjectData.Add(client.ToInt("id"), missionId, title, tip, categoryId, index, mp3Url) > 0)
        {
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
        if (ClientMissionSubjectData.ExistsByTitle(missionId, title, subjectId))
        {
            return new Hash((int)CodeType.SubjectTitleExists, "题目重复");
        }
        if (ClientMissionSubjectData.Edit(subjectId, title, tip, categoryId) > 0)
        {
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
        Hash subject = ClientMissionSubjectData.GetById(subjectId);
        if (ClientMissionSubjectData.Delete(subjectId) > 0)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath(subject.ToString("mp3Url")));
            }
            catch
            {

            }
            ClientMissionSubjectData.Clear(subjectId);
            return new Hash((int)CodeType.OK, "成功");
        }
        return new Hash((int)CodeType.DataBaseUnknonw, "数据库操作失败");
    }
}