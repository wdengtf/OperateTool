<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="LotteryEdit.aspx.cs" Inherits="Web.Manage.Activity.LotteryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7">
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">活动名称：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_Name" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">状态：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">活动规则：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rblRules" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">每天一次</asp:ListItem>
                    <asp:ListItem Value="2">总共一次</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w"></td>
            <td align="left" bgcolor="#ffffff">
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">开始日期：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_startdate" onfocus="WdatePicker()" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="2
                            5"
                align="right" bgcolor="#f7f7f7" class="w">结束日期：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_enddate" onfocus="WdatePicker()" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td bgcolor="#f7f7f7">&nbsp;</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:Button ID="Submit" runat="server" Text="保 存" CssClass="btn_info" OnClick="Submit_Click"></asp:Button></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
</asp:Content>
