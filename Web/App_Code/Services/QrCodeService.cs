using System;
using Budong.Common.Utils;
using Budong.Common.Third.Wechat;

/// <summary>
/// 二维码业务操作类
/// </summary>
public class QrCodeService
{
    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="client">Hash 客户端信息</param>
    /// <param name="page">string 页面地址</param>
    /// <param name="scene">string 参数集合</param>
    /// <returns>Hash 返回结果</returns>
    public static Hash Create(Hash client, string page, string scene)
    {
        Hash accessToken = APPService.GetAccessToken(Common.APP_ID);
        if (accessToken.ToInt("code") == 0)
        {
            byte[] buffer;
            string unionId = Guid.NewGuid().ToString();
            string qrcodeUrl = "/Uploads/QrCode/qrcode_" + Files.NewFileName(".png");
            Hash query = new Hash();
            query["scene"] = scene;
            query["page"] = page;
            query["width"] = "600";
            Hash result = API.GetQrCode(accessToken.ToHash("data").ToString("accessToken"), query.ToJSON(), out buffer);
            if (result.ToInt("errcode") == 0)
            {
                Files.SaveAllBytes(Files.MapPath(qrcodeUrl), buffer);
                APPQrCodeData.Create(Common.APP_ID, unionId, scene, page, qrcodeUrl);
                return new Hash((int)CodeType.OK, "成功", APPQrCodeData.GetByUnionId(unionId));
            }
            return new Hash((int)CodeType.WechatAPIError, result.ToString("errmsg"));
        }
        return new Hash((int)CodeType.WechatAccessTokenInvalid,"accessToken 无效");
    }
}
