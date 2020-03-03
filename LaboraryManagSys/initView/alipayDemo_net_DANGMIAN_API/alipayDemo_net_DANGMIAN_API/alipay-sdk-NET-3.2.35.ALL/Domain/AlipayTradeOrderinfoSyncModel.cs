using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayTradeOrderinfoSyncModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayTradeOrderinfoSyncModel : AopObject
    {
        /// <summary>
        /// 交易信息同步对应的业务类型，具体值与支付宝约定；信用授权场景下传CREDIT_AUTH
        /// </summary>
        [XmlElement("biz_type")]
        public string BizType { get; set; }

        /// <summary>
        /// 商户传入同步信息，具体值要和支付宝约定；用于芝麻信用租车、单次授权等信息同步场景，格式为json格式
        /// </summary>
        [XmlElement("order_biz_info")]
        public string OrderBizInfo { get; set; }

        /// <summary>
        /// 标识一笔交易多次请求，同一笔交易多次信息同步时需要保证唯一
        /// </summary>
        [XmlElement("out_request_no")]
        public string OutRequestNo { get; set; }

        /// <summary>
        /// 支付宝交易号，和商户订单号不能同时为空
        /// </summary>
        [XmlElement("trade_no")]
        public string TradeNo { get; set; }
    }
}
