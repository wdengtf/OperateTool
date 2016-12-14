<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="GroupEdit.aspx.cs" Inherits="Web.Manage.User.GroupEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7">
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">用户组：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="mb5 txt_able"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7">权限：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:Literal ID="sDroit" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td bgcolor="#f7f7f7">&nbsp;</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:Button ID="Submit" runat="server" Text="提 交" CssClass="btn_info" OnClick="Submit_Click"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
</asp:Content>
