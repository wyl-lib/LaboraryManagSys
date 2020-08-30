using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// MybankCreditCreditriskConsultQueryResponse.
    /// </summary>
    public class MybankCreditCreditriskConsultQueryResponse : AopResponse
    {
        /// <summary>
        /// 可贷额度，单位元
        /// </summary>
        [XmlElement("available_amt")]
        public string AvailableAmt { get; set; }

        /// <summary>
        /// 咨询结果，根据具体的场景约定不同的值。  授信前准入场景，返回1表示准入，0表示不准入
        /// </summary>
        [XmlElement("consult_result_code")]
        public string ConsultResultCode { get; set; }

        /// <summary>
        /// 咨询结果的描述信息
        /// </summary>
        [XmlElement("consult_result_msg")]
        public string ConsultResultMsg { get; set; }
    }
}
