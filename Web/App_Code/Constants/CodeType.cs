﻿
/// <summary>
/// 错误码枚举
/// </summary>
public enum CodeType : int
{
    /// <summary>
    /// 成功
    /// </summary>
    OK = 0,

    /// <summary>
    /// 用户标识不能为空
    /// </summary>
    OpenIdRequired = 1001,
    /// <summary>
    /// 用户标识不合法
    /// </summary>
    OpenIdIllegal = 1002,
    /// <summary>
    /// 用户标识已存在
    /// </summary>
    OpenIdExists = 1003,
    /// <summary>
    /// 用户标识不存在
    /// </summary>
    OpenIdNotExists = 1004,
    /// <summary>
    /// Code 不能为空
    /// </summary>
    CodeRequired = 1011,
    /// <summary>
    /// Code 不合法
    /// </summary>
    CodeInvalid = 1012,
    /// <summary>
    /// SessionKey 不能为空 
    /// </summary>
    SessionKeyRequired = 1021,
    /// <summary>
    /// SessionCode 不存在
    /// </summary>
    Session3rdRequired = 1022,
    /// <summary>
    /// SessionCode 不存在
    /// </summary>
    Session3rdNotExists = 1023,
    /// <summary>
    /// SessionCode 已过期
    /// </summary>
    Session3rdExpire = 1024,
    /// <summary>
    /// 微信小程序接口错误
    /// </summary>
    WechatDecrypt = 2001,
    /// <summary>
    /// 微信小程序不存在
    /// </summary>
    WechatAPPNotExists = 2011,
    /// <summary>
    /// 微信小程序 AccessToken 无效
    /// </summary>
    WechatAccessTokenInvalid = 2012,
    /// <summary>
    /// 微信小程序接口错误
    /// </summary>
    WechatAPIError = 2021,
    /// <summary>
    /// 客户端不存在
    /// </summary>
    ClientRequired = 2021,
    /// <summary>
    /// 客户端不存在
    /// </summary>
    ClientNotExists = 2022,
    /// <summary>
    /// 客户端不活跃
    /// </summary>
    ClientInaction = 2031,
    /// <summary>
    /// 今日已签到
    /// </summary>
    ClientSignInExists = 2041,
    /// <summary>
    /// 账户变动金额不能为零
    /// </summary>
    CoinZero = 2101,

    /// <summary>
    /// 关卡重名
    /// </summary>
    MissionTitleExists = 5001,
    /// <summary>
    /// 关卡封面格式不对
    /// </summary>
    MissionLogoUnvalid = 5002,

    /// <summary>
    /// 题目重复
    /// </summary>
    SubjectTitleExists = 5101,
    /// <summary>
    /// 音频文件为空
    /// </summary>
    SubjectMP3Empty = 5111,
    /// <summary>
    /// 音频格式不对
    /// </summary>
    SubjectMP3Unvalid = 5112,


    /// <summary>
    /// 数据库未知错误
    /// </summary>
    DataBaseUnknonw = 9899,

    /// <summary>
    /// 未知错误
    /// </summary>
    Unknown = 9999
}