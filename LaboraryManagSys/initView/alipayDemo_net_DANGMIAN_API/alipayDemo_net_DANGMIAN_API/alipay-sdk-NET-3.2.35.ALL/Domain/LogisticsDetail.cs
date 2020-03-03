using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// LogisticsDetail Data Structure.
    /// </summary>
    [Serializable]
    public class LogisticsDetail : AopObject
    {
        /// <summary>
        /// 物流类型, 参考com.alipay.tradecore.service.enums.TransportTypeEnum下的值， POST 平邮 EXPRESS 其他快递 VIRTUAL 虚拟物品 EMS EMS DIRECT 无需物流 VIRTUAL_10 虚拟物品(10天超时） VIRTUAL_30 虚拟物品(30天超时）
        /// </summary>
        [XmlElement("logistics_type")]
        public string LogisticsType { get; set; }
    }
}
