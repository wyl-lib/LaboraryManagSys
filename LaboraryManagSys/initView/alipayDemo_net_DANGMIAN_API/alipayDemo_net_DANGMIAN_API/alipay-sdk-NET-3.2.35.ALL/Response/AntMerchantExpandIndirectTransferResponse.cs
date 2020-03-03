using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// AntMerchantExpandIndirectTransferResponse.
    /// </summary>
    public class AntMerchantExpandIndirectTransferResponse : AopResponse
    {
        /// <summary>
        /// 转移成功返回true，失败返回false
        /// </summary>
        [XmlElement("transfer_result")]
        public bool TransferResult { get; set; }
    }
}
