using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// MybankCreditLoanapplyArrangementCreateResponse.
    /// </summary>
    public class MybankCreditLoanapplyArrangementCreateResponse : AopResponse
    {
        /// <summary>
        /// 合约编号
        /// </summary>
        [XmlElement("ar_no")]
        public string ArNo { get; set; }
    }
}
