<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.Manage.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>望客- 用户登录</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="js/lib/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="/js/lib/Common.js" type="text/javascript"></script>
    <script src="js/login.js" type="text/javascript"></script>
<!--[if IE 6]>
<script type="text/javascript" src="js/AC.js"></script>
<script type="text/javascript">
	DD_belatedPNG.fix('*')
</script>
<![endif]-->

</head>
<body>
<form id="form1" runat="server"> 
     <div class="login_bg"><img src="images/login_bg.jpg" id="bigpic"  border="0" /></div>
     <div class="login_con">
     <asp:TextBox id="username" runat="server" CssClass="login_user" value="请输入用户名" placeholder="请输入用户名"></asp:TextBox>
     <asp:TextBox id="password" runat="server" TextMode="Password" CssClass="login_pwd"  value="请输入密码" placeholder="请输入密码"></asp:TextBox>
     <asp:Button ID="btn_Save" runat="server" class="login_btn" onclick="ImageButton1_Click" />
</div>
<div class="login_footer">© <%--wangcl.com 沪ICP备14001048号-1--%><br />版权所有:<%--上海望客电子商务有限公司 021-64065280--%></div>
</form>
</body>
</html>
