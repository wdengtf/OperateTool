<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="LotteryDataEdit.aspx.cs" Inherits="Manage.Activity.LotteryDataEdit" %>

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
                <asp:DropDownList ID="ddlActivity" class="select_able"  runat="server">
                </asp:DropDownList>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品名称：</td>
            <td align="left" bgcolor="#ffffff">
                <select id="selActivieyPrize" class="select_able" name="selActivieyPrize">
                    <option value="0">请选择活动</option>
                </select>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">用户类型：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:DropDownList ID="ddlDataType" runat="server">
                    <asp:ListItem Value="WX">微信</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">姓名</td>
            <td align="left" bgcolor="#ffffff">
               <asp:TextBox ID="txtRealName" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">手机：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtMobile" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">地址</td>
            <td align="left" bgcolor="#ffffff">
               <asp:TextBox ID="txtAddress" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">OpenId：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtOutId" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">昵称</td>
            <td align="left" bgcolor="#ffffff">
               <asp:TextBox ID="txtNickName" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">IP：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtIP" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25"  align="right" bgcolor="#f7f7f7" class="w">更新日期：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtUpdateTime" onfocus="WdatePicker({dateFmt:'yyyy-M-d H:mm:ss'})" runat="server" CssClass="txt_able"></asp:TextBox>
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
    <script type="text/javascript">
        $(function () {
            $("#Content_ddlActivity").bind('change', function () {
                var activity_id = $(this).val();
                if (activity_id == 0)
                    return false;

                var prizeName = [];
                prizeName.push('<option value="0">请选择活动</option>');
                $.post("/data/Activity/LotteryJackpotList.ashx", { action: 'getPrizeName', activity_id: activity_id, rand: Math.random() }, function (data) {
                    if (data.Result == 1) {
                        for (var i = 0; i < data.Data.rows.length; i++) {
                            var dr = data.Data.rows[i];
                            prizeName.push('<option value="' + dr.id + '">' + dr.name + '</option>');
                        }
                        $("#selActivieyPrize").html(prizeName.join(''));
                    }
                    else {
                        dialog.ShowTempMessage(data.Message);
                    }
                }, 'json');
            });
        })
    </script>
</asp:Content>
