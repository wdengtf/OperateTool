<%@ Page Title="" Language="C#" MasterPageFile="~/Master/EditMaster.Master" AutoEventWireup="true" CodeBehind="LotteryPrizeEdit.aspx.cs" Inherits="Web.Manage.Activity.Lottery.LotteryPrizeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <table width="100%" border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7">
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品名称：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_Name" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">抽奖活动：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:DropDownList ID="ddl_activie_name" CssClass="dorpdown_info" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品级别：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_level" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品价格：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_price" Text="0" Width="80px" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品类型：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:DropDownList ID="prizeType" CssClass="dorpdown_info" runat="server">
                    <asp:ListItem Value="1">实物</asp:ListItem>
                    <asp:ListItem Value="2">卡券</asp:ListItem>
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品数量：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_num" Text="0" Width="80px" runat="server" CssClass="txt_able"></asp:TextBox>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品显示位置：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txtPosition" Text="0" Width="80px" ReadOnly="true" runat="server" CssClass="txt_able" BackColor="#CCCCCC"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品位置图片：</td>
            <td align="left" colspan="3" bgcolor="#ffffff">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtPostionImg" runat="server" MaxLength="255" CssClass="txt_able" Style="width: 220px;"></asp:TextBox>
                        </td>
                        <td>
                            <input type="button" name="btnPic2" value="上 传" onclick="return ajaxUpload('txtPostionImg', 'PicFile2', 'loading2', 'td_show_ch2');" id="btnPic2" class="mb5" /></td>
                        <td>
                            <input name="PicFile2" type="file" id="PicFile2" class="mb5" style="width: 100px; height: 25px" size="3" /></td>
                        <td>
                            <img id="loading2" src="/images/loading.gif" style="display: none;" alt="" /></td>
                        <td id="td_show_ch2"></td>
                        <td id="showimg1"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品链接地址：</td>
            <td align="left" colspan="3" bgcolor="#ffffff">
                <asp:TextBox ID="txt_Url" runat="server" CssClass="txt_able" Width="480px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品图片：</td>
            <td align="left" colspan="3" bgcolor="#ffffff">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="txt_winImg" runat="server" MaxLength="255" CssClass="txt_able" Style="width: 220px;"></asp:TextBox>
                        </td>
                        <td>
                            <input type="button" name="btnPic1" value="上 传" onclick="return ajaxUpload('txt_winImg', 'PicFile1', 'loading1 ', 'td_show_ch1');" id="btnPic1" class="mb5" /></td>
                        <td>
                            <input name="PicFile1" type="file" id="PicFile1" class="mb5" style="width: 100px; height: 25px" size="3" /></td>
                        <td>
                            <img id="loading1" src="/images/loading.gif" style="display: none;" alt="" /></td>
                        <td id="td_show_ch1"></td>
                        <td id="showimg"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">奖品简介：</td>
            <td colspan="3" align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_Introduction" TextMode="MultiLine" runat="server"
                    CssClass="txt_able" Width="500px" Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">状态：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:RadioButtonList ID="rbl_status" CssClass="dorpdown_info" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td height="25" align="right" bgcolor="#f7f7f7" class="w">添加日期：</td>
            <td align="left" bgcolor="#ffffff">
                <asp:TextBox ID="txt_createtime" onfocus="WdatePicker()" runat="server" CssClass="txt_able"></asp:TextBox>
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
