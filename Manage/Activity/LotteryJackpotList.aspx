<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="LotteryJackpotList.aspx.cs" Inherits="Web.Manage.Activity.LotteryJackpotList" %>

<%@ Import Namespace="YYT.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        活动名称：<select id="selActivityName" class="select_able" name="selActivityName">
            <option value="0">请选择活动</option>
            <%foreach (Luck_Activity lotteryModel in actLotteryList)
                { %>
            <option value="<%=lotteryModel.Id %>"><%=lotteryModel.Name %></option>
            <% } %>
        </select>
        奖品名称：<select id="selPrizeName" class="select_able" name="selPrizeName">
            <option value="0">请选择活动</option>
        </select>
        中奖人id：<input type="text" id="txtout_id" class="txt_able" />
        状态：<select id="selStatus" class="select_able" name="selStatus">
            <option value="99">请选择</option>
            <option value="1">已分配</option>
            <option value="0">未分配</option>
        </select>
        <button id="submitButton" class="btn">查询</button>
    </div>
    <div class="title">查询结果</div>
    <table id="navgrid"></table>
    <div id="pagernav"></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
    <script type="text/javascript">
        $(function () {
            commonJqGrid.init({
                url: "/data/Activity/LotteryJackpotList.ashx",
                addEditUrl: "",
                title: "奖池记录",
                addEditDialogHeight: 600,
                addEditDialogWidth: 600,
                //gridRowOper: {
                //    edit: true,
                //    del: true,
                //},
                colNames: ['Id', '活动名称', '奖品名称', '状态', '创建日期', '中奖outId', '手机', '地址', '中奖时间', '操作'],
                colModel: [{ name: 'Id', index: 'Id', width: 55, align: "center", sortable: false },
                { name: 'activityName', index: 'activityName', width: 80, align: "center", sortable: false },
                { name: 'prizeName', index: 'prizeName', width: 50, align: "center", sortable: false },
                { name: 'Status', index: 'Status', width: 50, align: "center", sortable: false, formatter: commonJqGrid.defaultState },
                { name: 'createtime', index: 'createtime', width: 120, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                { name: 'out_id', index: 'out_id', width: 120, align: "center", sortable: false },
                { name: 'Mobile', index: 'Mobile', width: 80, align: "center", sortable: false },
                { name: 'addr', index: 'addr', width: 120, align: "center", sortable: false },
                { name: 'updatetime', index: 'updatetime', width: 120, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                { name: 'Operate', index: 'Operate', width: 80, align: "center", sortable: false, hidden: true },
                ],
                getSearchParameter: function () {
                    var activity_id = $("#selActivityName").val();
                    var award_id = $("#selPrizeName").val();
                    var selStatus = $("#selStatus").val();
                    var out_id = $("#txtout_id").val();
                    return '&activity_id=' + activity_id + '&award_id=' + award_id + '&out_id=' + out_id + '&status=' + selStatus + '';
                }
            });


            //改变事件
            $("#selActivityName").bind('change', function () {
                var activity_id = $("#selActivityName").val();
                if (activity_id == 0)
                    return false;

                var prizeName = [];
                prizeName.push('<option value="0">请选择活动</option>');
                $.post("/data/Activity/LotteryJackpotList.ashx", { action: 'getPrizeName', activity_id: activity_id, rand: Math.random() }, function (data) {
                    if (data.Result == 1) {
                        for (var i = 0; i < data.Data.rows.length; i++) {
                            var dr = data.Data.rows[i];
                            prizeName.push('<option value="' + dr.Id + '">' + dr.name + '</option>');
                        }
                        $("#selPrizeName").html(prizeName.join(''));
                    }
                    else {
                        dialog.ShowTempMessage(data.Message);
                    }
                }, 'json');
            });
        });
    </script>
</asp:Content>
