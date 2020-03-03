<%@ WebHandler Language="C#" Class="AlipayDataDataserviceBillDownloadurlQueryService" %>

using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Web;
using System.Collections;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections.Generic;

public class AlipayDataDataserviceBillDownloadurlQueryService : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        DefaultAopClient client = new DefaultAopClient(config.gatewayUrl, config.app_id, config.private_key, "json", "1.0", config.sign_type, config.alipay_public_key, config.charset, false);
        AlipayDataDataserviceBillDownloadurlQueryModel model = ConvertToModel(context);
        
        AlipayDataDataserviceBillDownloadurlQueryRequest request = new AlipayDataDataserviceBillDownloadurlQueryRequest();
        // 设置同步回调地址
        request.SetReturnUrl("");
        // 设置异步通知接收地址
        request.SetNotifyUrl("");
        // 将业务model载入到request
        request.SetBizModel(model);
        
        AlipayDataDataserviceBillDownloadurlQueryResponse response = null;
        try{
            //因为是接口服务，使用exexcute方法获取到返回值
            response = client.Execute(request);
             context.Response.Write(response.Body);
        }
        catch (Exception exp){
            throw exp;
        }   
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    //将HttpContext反射到AlipayDataDataserviceBillDownloadurlQueryModel,只适用于普通类型,复杂类型开发者自行添加实现
    public static AlipayDataDataserviceBillDownloadurlQueryModel ConvertToModel(HttpContext context)
    {
        AlipayDataDataserviceBillDownloadurlQueryModel t = new AlipayDataDataserviceBillDownloadurlQueryModel();
        System.Reflection.PropertyInfo[] propertys = t.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo pi in propertys)
        {
            if (!pi.CanWrite)
                continue;
            object value = context.Request[pi.Name];
            if (value != null && value != DBNull.Value)
            {
                try
                {
                    if (value.ToString() != "")
                        pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);//这一步很重要，用于类型转换
                    else
                        pi.SetValue(t, value, null);
                }
                catch
                { }
            }
        }
        return t;
    }

}