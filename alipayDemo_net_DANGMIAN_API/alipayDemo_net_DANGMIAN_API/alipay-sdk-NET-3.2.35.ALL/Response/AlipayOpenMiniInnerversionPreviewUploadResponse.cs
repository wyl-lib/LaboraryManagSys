using System;
using System.Xml.Serialization;

namespace Aop.Api.Response
{
    /// <summary>
    /// AlipayOpenMiniInnerversionPreviewUploadResponse.
    /// </summary>
    public class AlipayOpenMiniInnerversionPreviewUploadResponse : AopResponse
    {
        /// <summary>
        /// 构建好的包地址
        /// </summary>
        [XmlElement("build_package_url")]
        public string BuildPackageUrl { get; set; }
    }
}
