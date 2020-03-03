<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <!--#include file="./base/common.aspx"-->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <title>开发者工具</title>
    <link href="./static/css/main.css" rel="stylesheet">
  </head>
  <body>
    <!--#include file="./base/head.aspx"-->
    <div class="container">
      <!-- body begin -->
      <div class="container-body">
        <!-- Tabs -->
        <div id="tabs">
          <!-- Nav tabs -->
          
        </div>

      </div>
    </div>
    <!-- footer begin -->
    <!--#include file="./base/foot.aspx"-->
  </body>
  <script src="./static/js/jquery-3.2.1.js"></script>
  <script src="./static/js/main.js"></script>
  <script src="./static/js/tabPanel.js"></script>
  <script type="text/javascript">
      var url = "demo/ApiInfo.ashx";
      $.post(url, function (json) {
          $('#tabs').tabPanel({
              data: json
          })
      });

  </script>
</html>
