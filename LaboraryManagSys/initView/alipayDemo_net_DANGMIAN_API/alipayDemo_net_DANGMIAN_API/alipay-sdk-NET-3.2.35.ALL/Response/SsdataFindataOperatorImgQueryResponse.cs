using System;
using System.Xml.Serialization;
using Aop.Api.Domain;

namespace Aop.Api.Response
{
    /// <summary>
    /// SsdataFindataOperatorImgQueryResponse.
    /// </summary>
    public class SsdataFindataOperatorImgQueryResponse : AopResponse
    {
        /// <summary>
        /// 系统业务流水号
        /// </summary>
        [XmlElement("biz_no")]
        public string BizNo { get; set; }

        /// <summary>
        /// 图片验证码信息
        /// </summary>
        [XmlElement("captcha")]
        public CaptchaInfo Captcha { get; set; }

        /// <summary>
        /// 商户系统的业务流水号
        /// </summary>
        [XmlElement("org_biz_no")]
        public string OrgBizNo { get; set; }
    }
}
