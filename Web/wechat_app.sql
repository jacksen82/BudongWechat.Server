# Host: localhost  (Version: 5.5.53)
# Date: 2018-03-26 19:45:21
# Generator: MySQL-Front 5.3  (Build 4.234)

/*!40101 SET NAMES utf8 */;

#
# Structure for table "ta_app"
#

DROP TABLE IF EXISTS `ta_app`;
CREATE TABLE `ta_app` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `appKey` varchar(50) NOT NULL DEFAULT '' COMMENT '应用 KEY',
  `appSecret` varchar(50) NOT NULL DEFAULT '' COMMENT '应用秘钥',
  `accessToken` varchar(250) DEFAULT NULL COMMENT '验证票',
  `accessTokenExpireIn` timestamp NULL DEFAULT NULL COMMENT '验证票过期时间',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` timestamp NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='微信小程序应用表';

#
# Data for table "ta_app"
#

INSERT INTO `ta_app` VALUES (1,'wx9f47a0436c96fa8b','f4256317185b08031eb249f9ef573184','7_6YV8kuFBA6RHioagTquNGF_F0UqpUWNtiKWNB307tgp3NmjK9sF5R80XIcmjff1fjYFbx3n9AWILh697U4pWjWQDHZmvsnSp5cDtAaIHcFLZ_ahFitC9oCPBeLo8eAyVzYI473gapmUV1_eZVXGcAAAQSB','2018-03-06 19:31:46','2018-02-28 11:50:39',NULL),(2,'wx6ffd425d3c8d2e9e','cea5ac5e62de82a3434449cc3e10e1b2','7_REcrQPW968Yfye7T_6TChyQqvxrO7IMXok2SfY6TX_ZCKzbJJmfupudpHqzykXcjf2ZzOt373ewTbQnpcxfiMVeZDN0cvOBJgK0nYP5kzuk76kzj6dTTWK463LiDHZaK2-GsAqP9wOWmUjWAVGQiABAPAK','2018-03-06 12:52:37','2018-03-06 09:49:17',NULL);

#
# Structure for table "ta_app_qrcode"
#

DROP TABLE IF EXISTS `ta_app_qrcode`;
CREATE TABLE `ta_app_qrcode` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `appId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '应用编号',
  `scene` varchar(255) DEFAULT NULL COMMENT '存储变量',
  `page` varchar(255) DEFAULT NULL COMMENT '启动页面',
  `qrcodeUrl` varchar(255) DEFAULT NULL COMMENT '二维码地址',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='二维码表';

#
# Data for table "ta_app_qrcode"
#

INSERT INTO `ta_app_qrcode` VALUES (1,1,'123456','','/Uploads/QrCode/qrcode_180306173507357.png','2018-03-06 17:35:08'),(2,1,'123456','','/Uploads/QrCode/qrcode_18030618060565.png','2018-03-06 18:06:07'),(3,1,'123456','','/Uploads/QrCode/qrcode_180306180634634.png','2018-03-06 18:06:42'),(4,1,'123456','','/Uploads/QrCode/qrcode_180306180639639.png','2018-03-06 18:06:43');

#
# Structure for table "tc_client"
#

DROP TABLE IF EXISTS `tc_client`;
CREATE TABLE `tc_client` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `appId` int(11) NOT NULL COMMENT '应用编号',
  `openId` varchar(50) NOT NULL COMMENT '用户标识',
  `unionId` varchar(50) DEFAULT NULL COMMENT '开放平台标识',
  `nick` varchar(50) DEFAULT NULL COMMENT '昵称',
  `gender` int(11) DEFAULT NULL COMMENT '性别 (1-男,2-女)',
  `birthYear` int(11) DEFAULT NULL COMMENT '出生年份',
  `avatarUrl` varchar(255) DEFAULT NULL COMMENT '头像图片',
  `coins` int(11) unsigned DEFAULT NULL COMMENT '金币数量',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` timestamp NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `appId_openId` (`appId`,`openId`) COMMENT '应用编号+客户端标识',
  KEY `createTime` (`createTime`),
  KEY `updateTime` (`updateTime`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='客户端表';

#
# Data for table "tc_client"
#

INSERT INTO `tc_client` VALUES (1,1,'obpDS5GoqnyGNGUmejRgbZo8eEcw','',NULL,NULL,NULL,NULL,NULL,'2018-02-28 13:32:30',NULL);

#
# Structure for table "tc_client_coin"
#

DROP TABLE IF EXISTS `tc_client_coin`;
CREATE TABLE `tc_client_coin` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '客户端编号',
  `avenue` int(11) unsigned DEFAULT NULL COMMENT '获得途径',
  `amount` int(11) DEFAULT NULL COMMENT '金额',
  `balance` int(11) DEFAULT '0' COMMENT '账户余额',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `clientId` (`clientId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='客户端金币记录明细表';

#
# Data for table "tc_client_coin"
#


#
# Structure for table "tc_client_friend"
#

DROP TABLE IF EXISTS `tc_client_friend`;
CREATE TABLE `tc_client_friend` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) NOT NULL COMMENT '客户端编号',
  `friendClientId` int(11) NOT NULL COMMENT '关联客户端编号',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发生时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `clientId_relateClientId` (`clientId`,`friendClientId`) COMMENT '客户端+好友客户端'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='客户端好友关系表';

#
# Data for table "tc_client_friend"
#


#
# Structure for table "tc_client_group"
#

DROP TABLE IF EXISTS `tc_client_group`;
CREATE TABLE `tc_client_group` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) NOT NULL COMMENT '客户端标识',
  `openGId` varchar(32) NOT NULL COMMENT '群标识',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发生时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `clientId_openGId` (`clientId`,`openGId`) COMMENT '客户端+群标识'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='客户端群关系表';

#
# Data for table "tc_client_group"
#


#
# Structure for table "tc_client_log"
#

DROP TABLE IF EXISTS `tc_client_log`;
CREATE TABLE `tc_client_log` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) NOT NULL COMMENT '客户端编号',
  `session3rd` varchar(255) DEFAULT NULL COMMENT '三方标识',
  `scene` int(11) DEFAULT '0' COMMENT '来源',
  `url` varchar(255) DEFAULT NULL COMMENT '请求地址',
  `query` varchar(255) DEFAULT NULL COMMENT '请求参数',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发生时间',
  PRIMARY KEY (`id`),
  KEY `session3rd` (`session3rd`) COMMENT '三方标识',
  KEY `scene` (`scene`) COMMENT '小程序启动场景',
  KEY `createTime` (`createTime`) COMMENT '发生时间',
  KEY `clientId` (`clientId`) COMMENT '客户端编号'
) ENGINE=InnoDB AUTO_INCREMENT=149 DEFAULT CHARSET=utf8 COMMENT='客户端日志表';

#
# Data for table "tc_client_log"
#

INSERT INTO `tc_client_log` VALUES (12,0,'f35c7c21a54a4bdab3063bc0bff9a041',0,'/api/client/token.ashx','appId=1\r\nsession3rd=f35c7c21a54a4bdab3063bc0bff9a041\r\n','2018-02-28 14:41:07'),(13,0,'f35c7c21a54a4bdab3063bc0bff9a041',0,'/api/client/token.ashx','appId=1\r\nsession3rd=f35c7c21a54a4bdab3063bc0bff9a041\r\n','2018-02-28 14:45:21'),(14,0,'f35c7c21a54a4bdab3063bc0bff9a041',0,'/api/client/token.ashx','appId=1&session3rd=f35c7c21a54a4bdab3063bc0bff9a041&','2018-02-28 14:47:19'),(15,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&','2018-02-28 14:47:49'),(16,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 14:48:05'),(18,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 16:53:34'),(20,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 17:23:33'),(22,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/relate.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&fromClientId=1&encryptedData=undefined&iv=undefined&','2018-02-28 17:28:00'),(23,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 17:29:41'),(24,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/relate.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&fromClientId=1&encryptedData=&iv=&','2018-02-28 17:35:40'),(25,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 17:36:39'),(26,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:34:28'),(27,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=Joum8zZU1hOOfErJCpi51tFmDQnHEvhCRWHJa6/ix9zZliQCRfIkI4QBPzWQG1GLoRURHQwYgZQYS8mjlLR32RK7kCUNjfSmOab79InV55UNH+lu6XnEkDet6CNKrQDIwwA23W4N2enoIzSyxc9hoQ==&iv=ofc5eGWZ3/MpZ03KPPkDLw==&','2018-02-28 19:34:31'),(28,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:37:20'),(29,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=Z2SllpDpHfvpiosqzLUnSo2c0S8Kdnz14+i0FE66Vd5UQyczMyAFPtBOnVwQw7ePJj1pmo0ojKGA0fl5QqOwT64xymGMj3NtfylQSvcEzQ/9fEOUG/vez67ewZE9wjvCqdFeTx2XcOK1uhJaM0ewNw==&iv=T2B9BkMGpWcXOIekEsFCzQ==&','2018-02-28 19:38:17'),(30,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:39:12'),(31,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=/meh85ayMgQQDstiOUtPERYP7wD/rbvvdiAM5RsNRdMqK79jyi1O4vCW8SUpiL2qUDcNv2PEO5O/RptU8+vbT0Lk+P2ISJMTRrgjIyyKKLVvi02NlaCbhEk+lvRRV8A9dxvExEPqC3LXH9we4gOfjA==&iv=TE15bZ945W1pIO9gifCyEw==&','2018-02-28 19:39:15'),(32,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:40:10'),(33,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=7WAtWuVMQEotxTVhRT04zb0DprmCvqM8/Ge8doYpfc3cPOxGT9dcUpwZXKx9fRFq+afUzt0XyPDYARGUOEkuyuS+ga7xY8GP+eZqTpshpe6499+YVGoUpvh6Rbrp4SfJrVFX6szXG0Iy7lRoFGQ/Mw==&iv=UOHOtHdwFRo+qt5PNua4/g==&','2018-02-28 19:40:13'),(34,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:40:33'),(35,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=kggrFAs7JfPqiyEB6QD/hYyov0jECDQmB6JlzOiAGD6JhYZji1DglV8Qze7KC7lLKGEpYWXFbAXTXZxtIr11AH8Ide1jIWnmoeAQQUU18GzC2D+sZzpcMjH6z1GLiXr85o3tfs3d3SYzj3YeBgZong==&iv=8U/r4b/ew7kpurdUt3+WdA==&','2018-02-28 19:40:39'),(36,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:41:41'),(37,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=TCkRxvz8hYtS8Qy/L2G391sj7azfDp5Hb0ASB1ZZs/HRSz1qDkWObRb1UYCinc+lYk9EL7u9zXm9wzvuZBHpwEeM2fGkzmW5+NJjhzC5NobUxD6hMRBlgf3OA2NvEm+9VnIaUk3zsKTaOUswNMxz3A==&iv=zV9qYM3GW3LVoHW41jVdGw==&','2018-02-28 19:41:50'),(38,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:44:07'),(39,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=B58li240iL7dcEt90MXfzTaoA/7Asq99v46Svba1evw6eEEGeDo+PssTKuVimFOGENm5ZGAlloUNtPvL6y8FYQuEkSZxyCw1UHZ7SwI7mKylDeO1qSuSF8ffIi71YGRlc2gztkNcqYS8GtcStFfi6Q==&iv=AlM2HrxeORDYNsX2b7NFfQ==&','2018-02-28 19:44:09'),(40,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-02-28 19:45:29'),(41,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=bncvulnWKucJtc7C0mqX8hmPn+iYqSHGuk5EBdHEnZ0zltLK1QrNJnPtbTE36tGaFDYz2qlizM682Onxcf+nEtjcU7j8BEXJgZmZogP9uIBRXWMABMSeVEOkI1heNQc72CPo0q/R+05klkak8NNMuw==&iv=yE/j71nZ/ihcum4QppSTwQ==&','2018-02-28 19:45:34'),(42,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-03-01 09:20:03'),(43,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=TmKOlMnG/9P33dfA1jwXUiFk9w0bMtDZZSAeZ66R5gWEtsZTIEMrCFlwJkJUudgI8vMbvhHmSbYB2NDWx4hfGclwdv3lfLWlrclVe88aMFn/YGjzM/1QToQjk1OEhaCM1ikB23irHREA5xfLBpnCCA==&iv=H9pUebkBf0wpmle73XG8mw==&','2018-03-01 09:20:12'),(44,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',1001,'/api/client/token.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&scene=1001&','2018-03-01 09:37:36'),(45,0,'undefined',0,'/api/client/share.ashx','appId=1&session3rd=undefined&encryptedData=6jNy0UNWqt6TwjjSDvMu9vyFo4WJACYXmfYurX4n0pmsbTuavuq4HQN3MHfRP5rpJNWKAc6QT+5XTh9pPDuGQrePejSdxXx/TN4wL1jab95ThTjgUlL3Df1t5Gz0xOIkveO/IACRvoPmyhkag3LnLQ==&iv=9WhcXRoaHQZrLikHeL9dgQ==&','2018-03-01 09:38:01'),(46,0,'bd94fbfa01ae4f2c9e09eee97339a7e2',0,'/api/client/share.ashx','appId=1&session3rd=bd94fbfa01ae4f2c9e09eee97339a7e2&encryptedData=7ELSnYKP4CHyvl2o1R/OOwLMrOVObmv0vM9xZJEQevyom/bjWtCVZbUdgsecsy2FshljP9HAWFakx8of+mf4cRQ1QVc9NFkQuYPH490e5YMkyeShWqglqOMbc+Cm26SRkUfClCMPEarZ+1zVkxXf6Q==&iv=AMWi+qU8vkunXh6wy9ud+w==&','2018-03-01 09:48:52'),(47,0,'cabca4d8ec7748d6bda06d9fc643f4e2',0,'/api/client/share.ashx','appId=1&session3rd=cabca4d8ec7748d6bda06d9fc643f4e2&encryptedData=lDV7nWHHUvLW1K4RCr8rYacY4qK9GkfyXtdPmewxZswT8shNM+8wa5/S97KQXzGq1Ts41hgaudPrpY2X2EqnKYaiY7jvORlBr8z6VFethPjLUJAVCL1uBLjkYwpKly3DM7IkXo5nGF2TnMG2hsTzqQ==&iv=URodVFLrGKC69Wy7mrCt3g==&','2018-03-01 09:49:51'),(48,0,'cabca4d8ec7748d6bda06d9fc643f4e2',1001,'/api/client/token.ashx','appId=1&session3rd=cabca4d8ec7748d6bda06d9fc643f4e2&scene=1001&','2018-03-01 09:52:16'),(49,0,'cabca4d8ec7748d6bda06d9fc643f4e2',0,'/api/client/share.ashx','appId=1&session3rd=cabca4d8ec7748d6bda06d9fc643f4e2&encryptedData=LqKDBo8YT7jRzkkB5SVFBclZ+1FpvYM7j7CyYx3pzrfgYf4Ldq6PDqk5YrmRoT54oA3W4SbKUWkmQX4jIuPJ+kBTKP8kg1YBCWoM7iIDe5V/dotPaJ1RYTXq8F9+FUx8BpkAZP0k4R7iKgegn/GDfg==&iv=Mkf7Kr3OphiJ3DvQE7zrNg==&','2018-03-01 09:54:15'),(50,0,'cabca4d8ec7748d6bda06d9fc643f4e2',0,'/api/client/share.ashx','appId=1&session3rd=cabca4d8ec7748d6bda06d9fc643f4e2&encryptedData=0Yh2UhUMeCHsQhGZAM6DopyXCQYABLFRGoSCfwkeA9OTSbekxrmisT04kkJrmThqbQyRwr9KiT1wEBw50/2581K4fzxuugzjoojw5ky29rTVKRsIfzbairn2toT+H7VDhjMeDaPkQMTrHsu2raYKyA==&iv=A6ye1DoUV5LT7vXW7Pg3eA==&','2018-03-01 09:54:30'),(51,0,'cabca4d8ec7748d6bda06d9fc643f4e2',0,'/api/client/share.ashx','appId=1&session3rd=cabca4d8ec7748d6bda06d9fc643f4e2&encryptedData=R5X0JQkmdrb3oxpFNZRynsbj2vjcC5z6srgkuklIpY+2qSzJZ+Izm7HX05fmR8nQaiDqQpGpWNhyg9K+tm3/sV7s/CYcQ7vfYNUZdXBfAsjb1WVnhITs3dvxMBJkmv4bUiUbnOctkvxf334oGWQQRg==&iv=Ms+gfBQbcMsnCjlESv56qQ==&','2018-03-01 09:54:40'),(52,0,'cabca4d8ec7748d6bda06d9fc643f4e2',1001,'/api/client/token.ashx','appId=1&session3rd=cabca4d8ec7748d6bda06d9fc643f4e2&scene=1001&','2018-03-01 10:01:47'),(53,0,'2c8bff693d3a49cfbb3363c89bb7adcf',1001,'/api/client/token.ashx','appId=1&session3rd=2c8bff693d3a49cfbb3363c89bb7adcf&scene=1001&','2018-03-01 10:09:53'),(54,0,'9e21311d78f341138c2d1bb2687fe952',1001,'/api/client/token.ashx','appId=1&session3rd=9e21311d78f341138c2d1bb2687fe952&scene=1001&','2018-03-01 10:21:36'),(55,0,'a70c2cb8434e4c9b94254e3a028d4f90',1001,'/api/client/token.ashx','appId=1&session3rd=a70c2cb8434e4c9b94254e3a028d4f90&scene=1001&','2018-03-01 10:22:04'),(56,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:23:29'),(57,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:35:16'),(58,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:35:16'),(59,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:35:27'),(60,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:35:28'),(61,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:35:50'),(62,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:35:51'),(63,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:35:56'),(64,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:35:56'),(65,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:36:56'),(66,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:36:58'),(67,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:37:56'),(68,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:37:57'),(69,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:38:16'),(70,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:38:16'),(71,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',0,'/api/client/relate.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&fromClientId=0&encryptedData=&iv=&','2018-03-01 10:38:30'),(72,0,'2d94b2fa8c3d41048eb6dbf4c6cd9fd7',1001,'/api/client/token.ashx','appId=1&session3rd=2d94b2fa8c3d41048eb6dbf4c6cd9fd7&scene=1001&','2018-03-01 10:38:32'),(73,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-01 10:46:32'),(74,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-01 10:48:58'),(75,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/share.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&encryptedData=pIMVUs9mDEqD5rngqsn7wPQyilzMYk3fljyM2ncWV5u0MB8w7DszKxtN0oMXsUHktFmJPUOZ9wbrzWCPAqpsF2SvKq1qK7ajEiD4vf0fuoxGCftnNyNFbk5hu5lyEb5QktDYiwrssLCKIuEIIz6BtQ==&iv=n42ABLD4/r0Pn199iAiIXA==&','2018-03-01 10:49:03'),(76,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-01 13:36:46'),(77,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/share.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&encryptedData=w3qPlCrSXDU3WFHxo8RdZ9Qi1YPLjgYXhD6IyEaV1yJDiWaNhK88qEnc0Net39vKaYgD8jkAxQhXOfb8KpPcbvlO6wACMwQR41rMKCVLkJjYV/vMR8hbaHwcubKawjtxtBUAx9kxst61egCbU26NPQ==&iv=cOtWRoe+jELJzQIwFPYbiA==&','2018-03-01 13:39:03'),(78,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-01 16:32:52'),(79,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-05 11:37:58'),(80,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 11:38:32'),(81,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 11:41:36'),(82,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 11:42:24'),(83,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 11:42:43'),(84,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 11:44:32'),(85,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 11:45:29'),(86,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 12:26:26'),(87,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 14:18:15'),(88,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 14:20:00'),(89,0,NULL,0,'/API/Client/QrCode.ashx','appid=1&','2018-03-05 14:20:27'),(90,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:26:21'),(91,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:30:01'),(92,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:33:02'),(93,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:33:58'),(94,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:35:26'),(95,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:38:53'),(96,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:47:53'),(97,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 09:49:42'),(98,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 10:00:29'),(99,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 10:01:18'),(100,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 10:03:11'),(101,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 10:52:23'),(102,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 10:53:37'),(103,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 11:50:27'),(104,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 11:50:59'),(105,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 11:54:26'),(106,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:33:54'),(107,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:35:12'),(108,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:35:43'),(109,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:36:16'),(110,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:36:48'),(111,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:37:13'),(112,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:38:08'),(113,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:38:26'),(114,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:38:39'),(115,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:39:46'),(116,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:40:13'),(117,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:40:29'),(118,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:40:52'),(119,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:42:19'),(120,0,NULL,0,'/API/Client/QrCode.ashx','','2018-03-06 12:43:24'),(121,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 14:47:28'),(122,0,NULL,0,'/API/Client/Send.ashx','','2018-03-06 14:49:27'),(123,0,'obpDS5GoqnyGNGUmejRgbZo8eEcw',0,'/API/Client/Send.ashx','session3rd=obpDS5GoqnyGNGUmejRgbZo8eEcw&','2018-03-06 14:49:44'),(124,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/API/Client/Send.ashx','session3rd=28e27949ef9a443b9e86ec32de4d850a&','2018-03-06 14:50:24'),(125,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 14:57:35'),(126,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/API/Client/Send.ashx','session3rd=28e27949ef9a443b9e86ec32de4d850a&','2018-03-06 14:58:40'),(127,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/API/Client/Send.ashx','session3rd=28e27949ef9a443b9e86ec32de4d850a&','2018-03-06 15:28:56'),(128,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/API/Client/Send.ashx','session3rd=28e27949ef9a443b9e86ec32de4d850a&','2018-03-06 15:31:32'),(129,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 15:41:45'),(130,0,'undefined',0,'/api/client/qrcode.ashx','appId=1&session3rd=undefined&scene=asdf&','2018-03-06 17:17:18'),(131,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 17:31:45'),(132,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 17:31:46'),(133,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 17:34:13'),(134,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 17:34:14'),(135,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 17:35:07'),(136,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 17:35:08'),(137,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 17:36:57'),(138,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 17:37:01'),(139,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 17:46:43'),(140,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 17:46:44'),(141,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 17:49:55'),(142,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 18:06:05'),(143,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 18:06:07'),(144,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 18:06:35'),(145,0,'28e27949ef9a443b9e86ec32de4d850a',0,'/api/client/qrcode.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=asdf&','2018-03-06 18:06:42'),(146,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 18:08:15'),(147,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 18:28:33'),(148,0,'28e27949ef9a443b9e86ec32de4d850a',1001,'/api/client/token.ashx','appId=1&session3rd=28e27949ef9a443b9e86ec32de4d850a&scene=1001&','2018-03-06 19:08:30');

#
# Structure for table "tc_client_session"
#

DROP TABLE IF EXISTS `tc_client_session`;
CREATE TABLE `tc_client_session` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `appId` varchar(50) NOT NULL COMMENT '应用标识',
  `openId` varchar(50) NOT NULL COMMENT '用户标识',
  `sessionKey` varchar(50) NOT NULL COMMENT '会话标识',
  `session3rd` varchar(50) NOT NULL COMMENT '三方标识',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `session3rd` (`session3rd`) COMMENT '唯一三方标识',
  UNIQUE KEY `appId_openId_sessionKey` (`appId`,`openId`,`sessionKey`) COMMENT '应用+标识组合+会话标识'
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8 COMMENT='客户端三方会话标识表';

#
# Data for table "tc_client_session"
#

INSERT INTO `tc_client_session` VALUES (3,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','K5UFPXrZc8HLTHTK+kgOJw==','c682cdc18517461b888271ee20ca3142','2018-02-28 13:37:12'),(9,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','K6xyM6w2dydeZY0bpLc3XA==','52d1de16f3d34d4c8a122298d5cd4375','2018-02-28 14:03:38'),(10,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','nZaOGeaM1MmTHRLMmaxchQ==','fde7b7e9d1a14b8aa1e1389ff84f29a0','2018-02-28 14:04:24'),(11,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','c8bi2d6dcVyEOCo6lcHRgA==','1d4a4389ea4a4a678d342b4073d72f87','2018-02-28 14:06:20'),(12,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','vkB7fTxsvYpBiYEp5yOswg==','f35c7c21a54a4bdab3063bc0bff9a041','2018-02-28 14:36:13'),(13,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','3QlJA+wDvcMTUMsx/so0aA==','bd94fbfa01ae4f2c9e09eee97339a7e2','2018-02-28 14:47:37'),(14,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','oVSvrX/qHHrmL3RmfZGjgg==','cabca4d8ec7748d6bda06d9fc643f4e2','2018-03-01 09:49:45'),(15,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','Pz6X2tsl9xG07buYkk/qNA==','2c8bff693d3a49cfbb3363c89bb7adcf','2018-03-01 10:09:28'),(16,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','hpXb9WX+qKjaXyAlyafUbQ==','444d5f6662c047b8a2237ac7dd69299a','2018-03-01 10:13:13'),(17,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','b6ASZ4ppmYLkAa9O24PrPg==','9e21311d78f341138c2d1bb2687fe952','2018-03-01 10:19:57'),(18,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','3q6fiTTwWGzZXS9hV0iXXA==','a70c2cb8434e4c9b94254e3a028d4f90','2018-03-01 10:21:48'),(19,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','haQsN5TjqWxHdp+rE7Z/GQ==','2d94b2fa8c3d41048eb6dbf4c6cd9fd7','2018-03-01 10:22:10'),(20,'1','obpDS5GoqnyGNGUmejRgbZo8eEcw','P85iE9nhw8ZanAv3oDQK1A==','28e27949ef9a443b9e86ec32de4d850a','2018-03-01 10:46:30');

#
# Structure for table "tc_client_share"
#

DROP TABLE IF EXISTS `tc_client_share`;
CREATE TABLE `tc_client_share` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '客户端编号',
  `openGId` varchar(50) DEFAULT NULL COMMENT '群标识',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发生时间',
  PRIMARY KEY (`id`),
  KEY `clientId` (`clientId`) COMMENT '客户端编号',
  KEY `createTime` (`createTime`) COMMENT '创建时间',
  KEY `openGId` (`openGId`) COMMENT '群标识'
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8 COMMENT='客户端分享记录表';

#
# Data for table "tc_client_share"
#

INSERT INTO `tc_client_share` VALUES (1,1,'','2018-02-28 19:34:31'),(2,1,'','2018-02-28 19:38:17'),(3,1,'','2018-02-28 19:39:15'),(4,1,'','2018-02-28 19:40:13'),(5,1,'','2018-02-28 19:40:39'),(6,1,'','2018-02-28 19:41:50'),(7,1,'','2018-02-28 19:44:09'),(8,1,'','2018-02-28 19:45:34'),(9,1,'','2018-03-01 09:20:12'),(10,0,'','2018-03-01 09:38:01'),(11,1,'','2018-03-01 09:48:52'),(12,1,'tGbpDS5NmW3OeqnhghVP9K3Ea5y30','2018-03-01 09:49:51'),(13,1,'tGbpDS5FNCu7_c1y_kxM4KgN2X2-Q','2018-03-01 09:54:15'),(14,1,'tGbpDS5I_cNIpc99-1qMQwkmGxx5s','2018-03-01 09:54:30'),(15,1,'tGbpDS5FNCu7_c1y_kxM4KgN2X2-Q','2018-03-01 09:54:40'),(16,1,'tGbpDS5MusZLITLhH5rur-UgxweJo','2018-03-01 10:49:03'),(17,1,'tGbpDS5FNCu7_c1y_kxM4KgN2X2-Q','2018-03-01 13:39:03');

#
# Structure for table "tm_mission"
#

DROP TABLE IF EXISTS `tm_mission`;
CREATE TABLE `tm_mission` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '客户端编号',
  `title` varchar(255) NOT NULL DEFAULT '' COMMENT '关卡标题',
  `grade` int(11) unsigned DEFAULT NULL COMMENT '精选关卡',
  `subjectCount` varchar(255) DEFAULT NULL COMMENT '题目数量',
  `playerCount` int(11) unsigned DEFAULT NULL COMMENT '玩家数量',
  `winnerCount` int(11) unsigned DEFAULT NULL COMMENT '获胜玩家数量',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` timestamp NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`),
  KEY `clientId` (`clientId`) COMMENT '客户端标识'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='游戏关卡表';

#
# Data for table "tm_mission"
#


#
# Structure for table "tm_mission_client"
#

DROP TABLE IF EXISTS `tm_mission_client`;
CREATE TABLE `tm_mission_client` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '客户端编号',
  `missionId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '关卡编号',
  `score` int(11) unsigned DEFAULT NULL COMMENT '答题数量',
  `seconds` int(11) unsigned DEFAULT NULL COMMENT '用时，单位秒',
  `createTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` timestamp NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`),
  KEY `clientId` (`clientId`) COMMENT '客户端编号',
  KEY `missionId` (`missionId`) COMMENT '关卡编号'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='关卡客户端参与表';

#
# Data for table "tm_mission_client"
#


#
# Structure for table "tm_mission_client_subject"
#

DROP TABLE IF EXISTS `tm_mission_client_subject`;
CREATE TABLE `tm_mission_client_subject` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `clientId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '客户端编号',
  `missionId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '关卡编号',
  `subjectId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '题目编号',
  `seconds` int(11) unsigned DEFAULT NULL COMMENT '用时，单位秒',
  `result` int(11) unsigned DEFAULT NULL COMMENT '答题结果',
  PRIMARY KEY (`id`),
  KEY `clientId` (`clientId`) COMMENT '客户端编号',
  KEY `missionId` (`missionId`) COMMENT '关卡编号',
  KEY `subjectId` (`subjectId`) COMMENT '题目编号'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='关卡客户端参与题目表';

#
# Data for table "tm_mission_client_subject"
#


#
# Structure for table "tm_mission_subject"
#

DROP TABLE IF EXISTS `tm_mission_subject`;
CREATE TABLE `tm_mission_subject` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `missionId` int(11) unsigned NOT NULL DEFAULT '0' COMMENT '关卡编号',
  `name` varchar(255) DEFAULT NULL COMMENT '题目答案',
  `tip` varchar(255) DEFAULT NULL COMMENT '题目提示',
  `type` int(11) unsigned DEFAULT NULL COMMENT '题目类型',
  `index` int(11) unsigned DEFAULT NULL COMMENT '排序位置',
  `voiceUrl` varchar(255) DEFAULT '' COMMENT '音频文件路径',
  PRIMARY KEY (`id`),
  KEY `missionId` (`missionId`) COMMENT '关卡编号'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='游戏关卡题目表';

#
# Data for table "tm_mission_subject"
#

