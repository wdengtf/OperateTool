<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="ChannelUserList.aspx.cs" Inherits="Manage.Channel.ChannelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title>渠道用户</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        用户名：<input type="text" id="txtName" name="txtName" class="txt_able" />
        状态：<select id="selStatus" class="select_able" name="selStatus">
            <option value="99">请选择</option>
            <option value="1">正常</option>
            <option value="0">锁定</option>
        </select>
        结束日期：<input type="text" id="beginTime" onclick="WdatePicker()" name="beginTime" class="txt_able" />到&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="text" id="endTime" name="endTime" onclick="WdatePicker()" class="txt_able" />
        <button id="submitButton" class="btn">查询</button>
        <button id="addBtn" class="btn hidden">新增</button>
    </div>
    <div class="title">查询结果</div>
    <table id="navgrid"></table>
    <div id="pagernav"></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
    <script type="text/javascript">
        $(function () {
            commonJqGrid.init({
                url: "/data/Channel/ChannelUserList.ashx",
                urlParm: "&status=" + $("#selStatus").val(),
                addEditUrl: "/Channel/ChannelUserEdit.aspx",
                title: "渠道用户",
                gridRowOper: {
                    add: true,
                    edit: true,
                    del: true
                },
                colNames: ['id', '用户名', '用户密钥', '状态', '开始日期', '结束日期','限制IP', '创建日期', '操作'],
                colModel: [{ name: 'id', index: 'id', width: 40, align: "center", sortable: false },
                 { name: 'user_name', index: 'user_name', width: 60, align: "center", sortable: false },
                 { name: 'user_key', index: 'user_key', width: 100, align: "center", sortable: false },
                 { name: 'Status', index: 'Status', width: 50, align: "center", sortable: false, formatter: commonJqGrid.defaultState },
                 { name: 'start_time', index: 'start_time', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                 { name: 'end_time', index: 'end_time', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                 { name: 'validate_ip', index: 'validate_ip', width: 50, align: "center", sortable: false, formatter: commonJqGrid.defaultYesOrNo },
                 { name: 'Createtime', index: 'Createtime', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                 { name: 'Operate', index: 'Operate', width: 80, align: "center", sortable: false },
                ],
                getSearchParameter: function () {
                    var name = $("#txtName").val();
                    var status = $("#selStatus").val();
                    var beginTime = $("#beginTime").val();
                    var endTime = $("#endTime").val();
                    return "&name=" + name + "&status=" + status + "&beginTime=" + beginTime + "&endTime=" + endTime;
                }
            });
        });
    </script>
</asp:Content>
