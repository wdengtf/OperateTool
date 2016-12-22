<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="AccountEdit.aspx.cs" Inherits="Web.Manage.User.AccountEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7">
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">用户：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="mb5 txt_able" Width="200px" Height="23px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">密码：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="password" runat="server" Width="100px" TextMode="Password" CssClass="mb5 txt_able" Height="20px"></asp:TextBox>
                <input type="checkbox" name="CHK" id="CHK" runat="server" />修改密码请选中
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">用户组：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:DropDownList ID="ddlGroup" CssClass="dorpdown_info" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">状态：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal"
                    Height="16px" Width="132px">
                    <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">创建日期：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtCreatetime" runat="server" onclick="WdatePicker()" CssClass="mb5 txt_able" Width="200px" Height="23px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td bgcolor="#f7f7f7">&nbsp;</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:Button ID="Submit" runat="server" CssClass="btn_info" Text="保 存" OnClick="Submit_Click"></asp:Button></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
</asp:Content>
