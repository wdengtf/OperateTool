<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.Api.Api.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>API接口测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="right">
                接口名称
            </td>
            <td align="left">
                <asp:DropDownList ID="ddl_interface" runat="server">
                    <asp:ListItem Value="0">======查询接口======</asp:ListItem>
                    <asp:ListItem Value="WxServerAuth">微信服务号授权</asp:ListItem>
                    <asp:ListItem Value="1">======操作接口======</asp:ListItem>
                    <asp:ListItem Value="AddressAdd">添加地址</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <strong>系统参数</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                授权的用户名：
            </td>
            <td align="left">
                <asp:TextBox ID="txt_UserName" Text="admin" runat="server" Width="280px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                密钥：
            </td>
            <td align="left">
                <asp:TextBox ID="txt_Key" Text="e1f84e2dda1147fcb31967bd0b734dd8" runat="server" Width="280px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <strong>应用参数</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                参数：
            </td>
            <td align="left">
               <asp:TextBox ID="txt_Parm" runat="server" TextMode="MultiLine"  Height="100px" Width="700px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right">
            </td>
            <td align="left">
                <asp:Button ID="btn_Save" Text="提交测试" runat="server" OnClick="btn_Save_Click" />
            </td>
        </tr>
        <tr>
            <td align="right">
                生成后Json串：
            </td>
            <td align="left">
                <asp:TextBox ID="txt_ResultJson" TextMode="MultiLine" runat="server" Height="270px" Width="700px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <strong>返回结果</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                结果：
            </td>
            <td align="left">
                <asp:TextBox ID="txt_Result" TextMode="MultiLine" runat="server" Height="270px" Width="700px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
