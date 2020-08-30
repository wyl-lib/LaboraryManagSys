using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Aop.Api.Domain;

namespace Aop.Api.Response
{
    /// <summary>
    /// SsdataFindataOperatorChannelQueryResponse.
    /// </summary>
    public class SsdataFindataOperatorChannelQueryResponse : AopResponse
    {
        /// <summary>
        /// 运营商渠道信息，其中item_status枚举：ENABLE（可用），DISABLE(不可用)。
        /// </summary>
        [XmlArray("channels")]
        [XmlArrayItem("operator_channel_info")]
        public List<OperatorChannelInfo> Channels { get; set; }
    }
}
