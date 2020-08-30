using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// KoubeiCateringOrderInfoSyncResponse.
    /// </summary>
    public class KoubeiCateringOrderInfoSyncResponse : AopResponse
    {
        /// <summary>
        /// 是否需要重试，true-需要，false-不需要
        /// </summary>
        [XmlElement("retry")]
        public bool Retry { get; set; }
    }
}
