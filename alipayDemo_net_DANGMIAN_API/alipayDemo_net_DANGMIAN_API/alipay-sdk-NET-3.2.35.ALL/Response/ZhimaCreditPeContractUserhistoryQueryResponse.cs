using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Aop.Api.Domain;

namespace Aop.Api.Response
{
    /// <summary>
    /// ZhimaCreditPeContractUserhistoryQueryResponse.
    /// </summary>
    public class ZhimaCreditPeContractUserhistoryQueryResponse : AopResponse
    {
        /// <summary>
        /// 用户历史签约信息
        /// </summary>
        [XmlArray("contract_history")]
        [XmlArrayItem("contract_basic_info")]
        public List<ContractBasicInfo> ContractHistory { get; set; }
    }
}
