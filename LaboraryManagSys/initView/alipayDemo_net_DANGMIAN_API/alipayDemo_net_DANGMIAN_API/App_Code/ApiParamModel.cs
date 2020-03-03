using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ApiParamModel
{
    /**
     * 基础类型
     */
    public static int TYPE_BASETYPE = 0;
    /**
     * 复杂类型
     */
    public static int TYPE_COMPLEXTYPE = 1;
    /** API参数模型列表*/
    private List<ApiParamModel> childs = new List<ApiParamModel>();

    /** 是否列表类型 */
    private bool isListType;
    /** 是否必须,非空 */
    private int           isMust;

    /** 类型 */
    private int baseType;
    
    private String description;


    private String                   enName;
    private String                   fullParamName;
    private String                   title;
    private String                   desc;

    public List<ApiParamModel> getChilds() {
        return this.childs;
    }

    public void setChilds(List<ApiParamModel> childs) {
        this.childs = childs;
    }


    public String getFullParamName() {
        return this.fullParamName;
    }

    public void setFullParamName(String fullParamName) {
        this.fullParamName = fullParamName;
    }

    public String getTitle() {
        return this.title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDesc() {
        return this.desc;
    }

    public void setDesc(String desc) {
        this.desc = desc;
    }

    public bool getIsListType()
    {
        return this.isListType;
    }

    public void setIsListType(bool listType)
    {
        isListType = listType;
    }

    public int getIsMust() {
        return this.isMust;
    }

    public void setIsMust(int isMust) {
        this.isMust = isMust;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getEnName() {
        return this.enName;
    }

    public void setEnName(String enName) {
        this.enName = enName;
    }

    public int getBaseType() {
        return this.baseType;
    }

    public void setBaseType(int baseType) {
        this.baseType = baseType;
    }
}
