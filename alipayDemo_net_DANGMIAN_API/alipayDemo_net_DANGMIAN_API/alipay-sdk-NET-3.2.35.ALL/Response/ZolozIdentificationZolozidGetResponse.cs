using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// ZolozIdentificationZolozidGetResponse.
    /// </summary>
    public class ZolozIdentificationZolozidGetResponse : AopResponse
    {
        /// <summary>
        /// zcif结果
        /// </summary>
        [XmlElement("result_info")]
        public string ResultInfo { get; set; }
    }
}
