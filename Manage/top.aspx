<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="top.aspx.cs" Inherits="Web.Manage.top" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/top.css" rel="stylesheet" type="text/css" />
    <script src="js/lib/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/lib/vue.min.js" type="text/javascript"></script>
    <script src="/js/lib/Common.js" type="text/javascript"></script>
</head>
<body>
    <div class="top_bg">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="170"><span class="top_logo"></span></td>
                <td class="top_center" v-on:click="top_center($event);">
                    <template v-if="topMenu!=null && topMenu!=[]">
                        <a v-for="menu in topMenu" href="left.aspx?pid={{menu.id}}" class="{{$index == 0 ? 'top_center_actived':''}}" target="leftFrame">{{menu.Title}}</a>
                    </template>
                </td>
                <td class="top_right">
                    <a href="javascript:;"><%=manageUserModel.UserName %> 你好！</a>
                    <a href="javascript:;"><span class="color_ff6005">密码修改</span></a>
                    <a target="mainFrame" href="exit.aspx"><span class="color_ff6005">退出登录</span></a>
                </td>
            </tr>
        </table>
    </div>
    <div id="userOperDorit"><%=manageDroitListStr %></div>
    <script src="js/top.js" type="text/javascript"></script>
</body>
</html>
