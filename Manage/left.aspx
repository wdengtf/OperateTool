<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="left.aspx.cs" Inherits="Web.Manage.left" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body {
            height: 100%;
        }
    </style>
    <script src="js/lib/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/lib/vue.min.js" type="text/javascript"></script>
    <script src="/js/lib/Common.js" type="text/javascript"></script>
</head>
<body>
    <div class="toggle">
        <dl v-if="leftMenu!=null && leftMenu!=[]">
            <dd v-for="menu in leftMenu" class="{{$index == 0 ? 'fist':''}}">
                <a v-on:click="toggle($event);" data="{{menu.Url}}?temid={{menu.id}}"  href="javascript:parent.add_menu('{{menu.Title}}','{{menu.Url}}?temid={{menu.id}}');" target="mainFrame">{{menu.Title}}</a>
            </dd>
        </dl>
        <div style="height: 32px; line-height: 32px; color: #535353; position: fixed; bottom: 0; width: 100%; text-align: center; border: 1px solid #d2d2d2;">最近打开的标签</div>
    </div>
    <script type="text/javascript">
        var pid = "<%=pid %>";
    </script>
    <script type="text/javascript" src="js/left.js"></script>
</body>
</html>
