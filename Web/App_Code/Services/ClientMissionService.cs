using System;
using System.IO;
using System.Web;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

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
        Hash mission = MissionData.GetById(missionId);
        mission["subjects"] = MissionSubjectData.List(missionId).ToHashCollection("data");
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
        title = Parse.ToString(title).Trim();
        if (MissionData.ExistsByTitle(title, 0))
        {
            return new Hash((int)CodeType.MissionTitleExists, "关卡重名");
        }
        if (MissionData.Create(client.ToInt("id"), title, client.ToString("avatarUrl")) > 0)
        {
            Hash mission = MissionData.GetByTitle(client.ToInt("id"), title);
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
    /// <param name="logofile">HttpPostedFile 封面文件</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Edit(Hash client, int missionId, string title, HttpPostedFile logofile)
    {
        Hash mission = MissionData.GetById(missionId);

        title = Parse.ToString(title).Trim();
        if (MissionData.ExistsByTitle(title, missionId))
        {
            return new Hash((int)CodeType.MissionTitleExists, "关卡重名");
        }
        if (logofile != null && logofile.ContentLength > 0)
        {
            if (!logofile.FileName.EndsWith("jpg", StringComparison.CurrentCultureIgnoreCase) && 
                !logofile.FileName.EndsWith("jpeg", StringComparison.CurrentCultureIgnoreCase) && 
                !logofile.FileName.EndsWith("png", StringComparison.CurrentCultureIgnoreCase))
            {
                return new Hash((int)CodeType.MissionLogoUnvalid, "封面格式不对");
            }
            string logoUrl = "/Uploads/LogoFile/logo_" + missionId + "_" + Files.NewFileName(".jpg");
            logofile.SaveAs(HttpContext.Current.Server.MapPath(logoUrl));
            if (!File.Exists(HttpContext.Current.Server.MapPath(logoUrl)))
            {
                return new Hash((int)CodeType.SubjectTitleExists, "文件保存失败");
            }
            if (!mission.IsNull("logoUrl") && !mission.ToString("logoUrl").StartsWith("https", StringComparison.InvariantCultureIgnoreCase))
            {
                Files.Delete(mission.ToString("logoUrl"));
            }
            mission["logoUrl"] = logoUrl;
        }
        if (MissionData.Edit(missionId, title, mission.ToString("logoUrl")) > 0)
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
        if (MissionData.Update(missionId) > 0)
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
        Hash mission = MissionData.GetById(missionId);
        if (MissionData.Delete(missionId) > 0)
        {
            if (!mission.IsNull("logoUrl") && !mission.ToString("logoUrl").StartsWith("https", StringComparison.InvariantCultureIgnoreCase))
            {
                Files.Delete(mission.ToString("logoUrl"));
            }
            HashCollection subjects = MissionSubjectData.List(missionId).ToHashCollection("data");
            foreach (Hash subject in subjects)
            {
                ClientMissionSubjectService.Delete(client, subject.ToInt("id"));
            }
            MissionData.Clear(missionId);
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
        Hash missions = MissionData.All(client.ToInt("id"));
        return new Hash((int)CodeType.OK, "成功", missions);
    }
}