using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// ZhimaMerchantOrderCreditModifyResponse.
    /// </summary>
    public class ZhimaMerchantOrderCreditModifyResponse : AopResponse
    {
        /// <summary>
        /// 被修改订单的外部订单号
        /// </summary>
        [XmlElement("out_order_no")]
        public string OutOrderNo { get; set; }

        /// <summary>
        /// 被修改订单的新逾期时间
        /// </summary>
        [XmlElement("overdue_time")]
        public string OverdueTime { get; set; }

        /// <summary>
        /// 被修改的订单的芝麻订单号
        /// </summary>
        [XmlElement("zm_order_no")]
        public string ZmOrderNo { get; set; }
    }
}
