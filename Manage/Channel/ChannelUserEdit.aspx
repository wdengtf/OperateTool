<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="ChannelUserEdit.aspx.cs" Inherits="Manage.Channel.ChannelUserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7">
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">用户名：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="mb5 txt_able" Width="100px" Height="23px"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7">渠道：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:DropDownList ID="ddlChannelSort" runat="server" CssClass="dorpdown_info"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">密钥：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtUserKey" runat="server" Width="380px" CssClass="mb5 txt_able" Height="23px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">状态：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rblStatus" runat="server" CssClass="dorpdown_info" RepeatDirection="Horizontal" Height="16px" Width="132px">
                    <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7">限定IP：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rblLimitIp" runat="server" CssClass="dorpdown_info" RepeatDirection="Horizontal" Height="16px" Width="132px">
                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">开始日期：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtStartDate" runat="server" Width="200px" onclick="WdatePicker()" CssClass="mb5 txt_able" Height="23px"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7">结束日期：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtEndDate" runat="server" Width="200px" onclick="WdatePicker()" CssClass="mb5 txt_able" Height="23px"></asp:TextBox>
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
