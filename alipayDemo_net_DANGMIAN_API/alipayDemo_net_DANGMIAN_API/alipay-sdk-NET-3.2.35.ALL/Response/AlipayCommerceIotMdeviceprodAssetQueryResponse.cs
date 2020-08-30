using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// AlipayCommerceIotMdeviceprodAssetQueryResponse.
    /// </summary>
    public class AlipayCommerceIotMdeviceprodAssetQueryResponse : AopResponse
    {
        /// <summary>
        /// 物料模板的图片url
        /// </summary>
        [XmlElement("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 物料模板的名称
        /// </summary>
        [XmlElement("item_name")]
        public string ItemName { get; set; }
    }
}
