using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// SsdataFindataReportQueryResponse.
    /// </summary>
    public class SsdataFindataReportQueryResponse : AopResponse
    {
        /// <summary>
        /// 查询的报告
        /// </summary>
        [XmlElement("biz_content")]
        public string BizContent { get; set; }

        /// <summary>
        /// 系统业务流水号
        /// </summary>
        [XmlElement("biz_no")]
        public string BizNo { get; set; }

        /// <summary>
        /// 商户系统的业务流水号
        /// </summary>
        [XmlElement("org_biz_no")]
        public string OrgBizNo { get; set; }
    }
}
