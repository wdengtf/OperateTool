<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="ChannelLogList.aspx.cs" Inherits="Manage.Channel.ChannelLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title>渠道日志</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        <%--渠道用户：<input type="text" id="txtChannelName" name="txtChannelName" class="txt_able" />
        状态：<select id="selStatus" class="select_able" name="selStatus">
            <option value="99">请选择</option>
            <option value="1">成功</option>
            <option value="0">失败</option>
        </select>
        类型：<select id="selType" class="select_able" name="selType">
            <option value="">请选择</option>
            <option value="OnTipMsg">提示</option>
            <option value="OnException">异常</option>
            <option value="OnFail">失败</option>
            <option value="OnBegin">开始</option>
            <option value="OnSuccess">成功</option>
            <option value="OnCompelete">完成</option>
        </select>
        Ip：<input type="text" id="txtIp" name="txtIp" class="txt_able" />
        日期：<input type="text" id="beginTime" onclick="WdatePicker()" name="beginTime" class="txt_able" />到&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="text" id="endTime" name="endTime" onclick="WdatePicker()" class="txt_able" />
        <button id="submitButton" class="btn">查询</button>--%>
    </div>

    <div class="title">查询结果</div>
    <table id="navgrid"></table>
    <div id="pagernav"></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
    <script type="text/javascript">
        $(function () {
            commonJqGrid.init({
                url: "/data/Channel/ChannelLogList.ashx",
                urlParm: "&status=" + $("#selStatus").val(),
                addEditUrl: "",
                title: "渠道日志",
                addEditDialogHeight: 600,
                addEditDialogWidth: 600,
                rowNum: 10,
                colNames: ['id', '昵称', 'openid', '状态', '类型', 'IP', '创建日期', '最后更新日期', '操作'],
                colModel: [{ name: 'id', index: 'id', width: 40, align: "center", sortable: false },
                 { name: 'channelName', index: 'channelName', width: 60, align: "center", sortable: false },
                 { name: 'interface', index: 'interface', width: 80, align: "center", sortable: false },
                 { name: 'status', index: 'status', width: 55, align: "center", sortable: false, hidden: true },
                 { name: 'failType', index: 'failType', width: 180, align: "center", sortable: false, hidden: true },
                 { name: 'ip', index: 'ip', width: 60, align: "center", sortable: false, hidden: true },
                 { name: 'Createtime', index: 'Createtime', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                 { name: 'Addtime', index: 'Addtime', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime, hidden: true },
                 { name: 'Operate', index: 'Operate', width: 80, align: "center", sortable: false, hidden: true },
                ],
                getSearchParameter: function () {
                    var channelName = $("#txtChannelName").val();
                    var status = $("#selStatus").val();
                    var failType = $("#selType").val();
                    var beginTime = $("#beginTime").val();
                    var endTime = $("#endTime").val();
                    var ip = $("#txtIp").val();
                    return "&channelName=" + channelName + "&status=" + status + "&beginTime=" + beginTime + "&endTime=" + endTime + "&failType=" + failType + "&ip=" + ip;
                }
            });
        });
    </script>
</asp:Content>
