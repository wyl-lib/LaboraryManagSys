using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// AlipayEcoMycarParkingAgreementQueryResponse.
    /// </summary>
    public class AlipayEcoMycarParkingAgreementQueryResponse : AopResponse
    {
        /// <summary>
        /// 车牌代扣状态，0：为支持代扣，1：为不支持代扣
        /// </summary>
        [XmlElement("agreement_status")]
        public string AgreementStatus { get; set; }
    }
}
