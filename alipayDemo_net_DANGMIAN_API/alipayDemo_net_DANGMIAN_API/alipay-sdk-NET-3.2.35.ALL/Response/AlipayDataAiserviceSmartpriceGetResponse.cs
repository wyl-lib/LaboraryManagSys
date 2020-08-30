using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// AlipayDataAiserviceSmartpriceGetResponse.
    /// </summary>
    public class AlipayDataAiserviceSmartpriceGetResponse : AopResponse
    {
        /// <summary>
        /// 用户购买hellobike月卡的推荐价格，单位为分。
        /// </summary>
        [XmlElement("promo_price_cent")]
        public long PromoPriceCent { get; set; }
    }
}
