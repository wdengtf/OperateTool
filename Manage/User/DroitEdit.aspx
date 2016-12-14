<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="DroitEdit.aspx.cs" Inherits="Web.Manage.User.DroitEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7">
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">标题：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="mb5 txt_able" Width="328px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">类型：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="ddlisMenu" runat="server" RepeatDirection="Horizontal" Width="253px">
                    <asp:ListItem Value="1">主菜单</asp:ListItem>
                    <asp:ListItem Value="2">次菜单</asp:ListItem>
                    <asp:ListItem Value="0" Selected>否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">状态：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal"
                    Width="120px">
                    <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">页面地址：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtUrl" runat="server" CssClass="mb5 txt_able" Width="286px" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">排序：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtRs_order" runat="server" CssClass="mb5 txt_able" Width="286px" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">父ID：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:DropDownList ID="Pid" CssClass="dorpdown_info" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="13%" height="25" align="right" bgcolor="#f7f7f7">权限：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rblDroit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="main">主栏目</asp:ListItem>
                    <asp:ListItem Value="view">列表</asp:ListItem>
                    <asp:ListItem Value="add">新增</asp:ListItem>
                    <asp:ListItem Value="edit">修改</asp:ListItem>
                    <asp:ListItem Value="del">删除</asp:ListItem>
                    <asp:ListItem Value="export">导出</asp:ListItem>
<%--                 <asp:ListItem Value="sel">浏览</asp:ListItem>
                    <asp:ListItem Value="pass">审核</asp:ListItem>
                    <asp:ListItem Value="lock">锁定</asp:ListItem>
                    <asp:ListItem Value="stock">出库</asp:ListItem>
    --%>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td bgcolor="#f7f7f7">&nbsp;</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:Button ID="Submit" runat="server" CssClass="btn_info" Text="提 交" OnClick="Submit_Click"></asp:Button></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
</asp:Content>
