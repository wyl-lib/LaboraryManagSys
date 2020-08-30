# DEMO仅供参考，实际开发中需要结合具体业务场景修改使用
#
# 运行环境:VS2010SP1及以上
# demo使用前必读

# 运行demo步骤如下(Visual Studio直接打开网站即可):
1、修改配置文件src/resources/Alipay-Config.properties中的参数
2、在项目上右键，在浏览器中查看; 


├── alipay-sdk-NET{XXX}       # 支付宝开放平台sdk
├── App_Code
│   ├── ${}config.cs          # 配置文件的读取类，用于读取配置文件以及配置文件更新等操作
│   ├── ApiInfoModle.cs       # 接口信息的实体类，用于存储接口各项信息
│   └── ApiParamModel.cs      # 接口参数信息的实体类，用于存储接口参数各项信息
├── Bin
│   ├── AopSdk.dll
│   └── AopSdk.pdb
├── Default.aspx              # demo展示页
├── Default.aspx.cs
├── Notify_url.aspx
├── Notify_url.aspx.cs
├── Return_url.aspx
├── Return_url.aspx.cs
├── Web.Debug.config
├── Web.config
├── base
│   ├── common.aspx           # 通用静态资源应用页
│   ├── common.aspx.cs
│   ├── foot.aspx             # 通用尾部
│   ├── foot.aspx.cs
│   ├── head.aspx             # 通用头部
│   └── head.aspx.cs
├── demo
│   ├── {接口名}Service.ashx   # 各    各接口请求的controller，文件名中{接口名}是接口首字母大写的形式，用于接收前端的请求参数，进行与支付宝的对接
│   └── ApiInfo.ashx          # 入口信息文件，里面包含前端显示的接口的接口信息
├── readme.txt
└── static
    ├── css
    │   ├── head.css
    │   ├── index.css
    │   └── main.css
    ├── images
    └── js
        ├── bootstrap
        │   ├── css
        │   ├── fonts
        │   └── js
        ├── jquery-3.2.1.js
        ├── main.js           # demo展示页js
        └── tabPanel.js

