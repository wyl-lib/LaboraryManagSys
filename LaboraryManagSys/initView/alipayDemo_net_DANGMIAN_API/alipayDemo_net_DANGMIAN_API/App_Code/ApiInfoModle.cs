using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aop.Api;
using System.Text.RegularExpressions;

[Serializable]
public class ApiInfoModel
{
    /**
     * 接口请求
     */
    public static int INVOKE_TYPE_REQUEST = 1;
    /**
     * 页面跳转
     */
    public static int INVOKE_TYPE_REDIRECT = 2;
    
    private String apiName;
    private String apiZhName;
    private String apiNameFirstLower;
    /**
     * 字段说明
     * 1:接口请求
     * 2：页面跳转
     */
    private int invokeType;

    private List<ApiParamModel> apiInParam;
    private List<ApiParamModel> apiOutParam;


    public int getInvokeType()
    {
        return this.invokeType;
    }

    public void setInvokeType(int invokeType)
    {
        this.invokeType = invokeType;
    }

    public String getApiName() {
        return this.apiName;
    }

    public String getApiNameFirstLower()
    {
        return this.apiNameFirstLower;
    }
    

    public void setApiName(String apiName) {
        this.apiName = apiName;
        this.apiNameFirstLower = UpperFirst(apiName).Replace(".", "");
        
    }

    public List<ApiParamModel> getApiInParam() {
        return this.apiInParam;
    }

    public void setApiInParam(List<ApiParamModel> apiInParam) {
        this.apiInParam = apiInParam;
    }

    public List<ApiParamModel> getApiOutParam() {
        return this.apiOutParam;
    }

    public void setApiOutParam(List<ApiParamModel> apiOutParam) {
        this.apiOutParam = apiOutParam;
    }

    public String getApiZhName() {
        return this.apiZhName;
    }

    public void setApiZhName(String apiZhName) {
        this.apiZhName = apiZhName;
    }

    public static string UpperFirst(string s)
    {
        return Regex.Replace(s, @"\b[a-z]\w+", delegate(Match match)
        {
            string v = match.ToString();
            return char.ToUpper(v[0]) + v.Substring(1);
        });
    }

}
