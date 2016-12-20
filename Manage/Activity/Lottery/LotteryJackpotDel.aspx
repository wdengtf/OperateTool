<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="LotteryJackpotDel.aspx.cs" Inherits="Web.Manage.Activity.Lottery.LotteryJackpotDel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7" width="100%">
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" width="100px" class="w123">奖品数量：</td>
            <td align="left" bgcolor="#ffffff">
                <%=prizeNum %>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" width="100px" class="w123">可删除奖品数量：</td>
            <td align="left" bgcolor="#ffffff">
                <%=delMaxNum %>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" width="100px" class="w123">删除奖品数量：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtNum" runat="server" Text="1" CssClass="txt_able" Width="180px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td bgcolor="#f7f7f7" class="w123">&nbsp;</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:Button ID="Submit" runat="server" Text="保 存" CssClass="btn" OnClick="Submit_Click"></asp:Button></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
</asp:Content>
