using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Aop.Api.Response
{
    /// <summary>
    /// KoubeiMarketingCampaignBenefitSendResponse.
    /// </summary>
    public class KoubeiMarketingCampaignBenefitSendResponse : AopResponse
    {
        /// <summary>
        /// 领取的权益列表
        /// </summary>
        [XmlArray("benefit_ids")]
        [XmlArrayItem("string")]
        public List<string> BenefitIds { get; set; }
    }
}
