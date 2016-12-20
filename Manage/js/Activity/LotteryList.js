var format = {
    rules: function (cellvalue, options, rowdata) {
        if (cellvalue == 1)
            return '每天';
        else if (cellvalue == 2)
            return '总共';
        else
            return '--';
    },
    prizeType: function (cellvalue, options, rowdata) {
        if (cellvalue == 1)
            return '实物';
        else if (cellvalue == 2)
            return '卡券';
        else
            return '--';
    },
}
var commonOper = {
    //参数初始化
    options: {
        url: "/data/Activity/LotteryList.ashx",
        addEditUrl: "/Activity/LotteryEdit.aspx",
        rowNum: 15,
        title: "抽奖活动",
        addEditDialogHeight: 600,
        addEditDialogWidth: 750,
        colNames: ['id', '活动名称', '开始日期', '结束日期', '活动规则', '活动次数', '状态', '创建日期', '操作'],
        colModel: [
         { name: 'id', index: 'id', width: 40, align: "center", sortable: false },
         { name: 'Name', index: 'Name', width: 80, align: "center", sortable: false },
         { name: 'Startdate', index: 'Startdate', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
         { name: 'Enddate', index: 'Enddate', width: 80, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
         { name: 'Rules', index: 'Rules', width: 60, align: "center", sortable: false, formatter: format.rules },
         { name: 'maxNum', index: 'maxNum', width: 60, align: "center", sortable: false },
         { name: 'Status', index: 'Status', width: 40, align: "center", sortable: false, formatter: commonJqGrid.defaultState },
         { name: 'Createtime', index: 'Createtime', width: 100, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
         { name: 'Operate', index: 'Operate', width: 100, align: "center", sortable: false },
        ],
        getSearchParameter: function () {
            var sortName = $("#txtSortName").val();
            var beginTime = $("#beginTime").val();
            var endTime = $("#endTime").val();
            return "&sortName=" + sortName + "&beginTime=" + beginTime + "&endTime=" + endTime;
        },
        gridContainerId: 'navgrid',

        //子表参数
        subGrid: true,
        subUrl: "/data/Activity/Lottery/LotteryPrize.ashx",
        subTitle: "抽奖奖品",
        subColNames: ['id', '奖品名称', '奖品图片', '奖品等级', '奖品价格', '奖品数量', '剩余奖品数量', '奖品位置', '状态', '奖品类型', '创建日期', '操作'],
        subColModel: [
         { name: 'id', index: 'id', width: 40, align: "center", sortable: false },
         { name: 'name', index: 'name', width: 80, align: "center", sortable: false },
         { name: 'PrizeImg', index: 'PrizeImg', width: 70, align: "center", sortable: false, formatter: commonJqGrid.defaultImg },
         { name: 'PrizeLevel', index: 'PrizeLevel', width: 60, align: "center", sortable: false },
         { name: 'price', index: 'price', width: 60, align: "center", sortable: false },
         { name: 'num', index: 'num', width: 40, align: "center", sortable: false },
         { name: 'NotReceiveTotal', index: 'NotReceiveTotal', width: 40, align: "center", sortable: false },
         { name: 'Position', index: 'Position', width: 40, align: "center", sortable: false },
         { name: 'Status', index: 'Status', width: 40, align: "center", sortable: false, formatter: commonJqGrid.defaultState },
         { name: 'PrizeType', index: 'PrizeType', width: 40, align: "center", sortable: false, formatter: format.prizeType },
         { name: 'Createtime', index: 'Createtime', width: 100, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
         { name: 'Operate', index: 'Operate', width: 120, align: "center", sortable: false },
        ],
    },
    //jqGrid初始化
    jqGridInit: function () {
        var gridContainer = $("#" + this.options.gridContainerId);
        gridContainer.jqGrid({
            url: this.options.url + '?action=getList',
            height: 'auto',
            datatype: "json",
            colNames: this.options.colNames,
            colModel: this.options.colModel,
            rowNum: this.options.rowNum,
            rowList: [10, 20, 30, 50],
            jsonReader: {
                root: "Data.rows",              //json中代表实际模型数据的入口
                page: "Data.pageIndex",         //json中代表当前页码的数据
                total: "Data.totalPage",        //json中代表页码总数的数据
                records: "Data.totalRecord",    //json中代表数据行总数的数据
                repeatitems: false,             //如果repeatitems为false，json 中数据可以乱序，并且允许数据空缺
                id: "id"                        //表示当在编辑数据模块中发送数据时，使用的id的名称.不设置默认为行索引
            },
            multiselect: true,
            pager: '#pagernav',
            mtype: "post",
            sortname: 'id',
            viewrecords: true,
            autowidth: true,
            subGrid: this.options.subGrid,
            subGridRowExpanded: function (subgrid_id, row_id) {
                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll' ></table><div id='" + pager_id + "' class='scroll'></div>");
                $("#" + subgrid_table_id).jqGrid({
                    url: commonOper.options.subUrl + "?action=getList&id=" + row_id,
                    height: "auto",
                    datatype: "json",
                    colNames: commonOper.options.subColNames,
                    colModel: commonOper.options.subColModel,
                    rowNum: 10,
                    rowList: [10, 20, 30],
                    jsonReader: {
                        root: "Data.rows",              //json中代表实际模型数据的入口
                        page: "Data.pageIndex",         //json中代表当前页码的数据
                        total: "Data.totalPage",        //json中代表页码总数的数据
                        records: "Data.totalRecord",    //json中代表数据行总数的数据
                        repeatitems: false,             //如果repeatitems为false，json 中数据可以乱序，并且允许数据空缺
                        id: "id"                        //表示当在编辑数据模块中发送数据时，使用的id的名称.不设置默认为行索引
                    },
                    pager: pager_id,
                    sortname: 'id',
                    autowidth: true,
                    gridComplete: function () {
                        //此事件发生在表格所有数据装入和进程完成后
                        //获取某列的值返回数组 
                        if (!paraVerify.verifyArr($("#" + subgrid_table_id).jqGrid('getCol', 'Operate', false))) {
                            return false;
                        }

                        var rowObj = $("#" + subgrid_table_id).jqGrid("getDataIDs");//返回当前grid里的id,类型[array]
                        for (var i = 0; i < rowObj.length; i++) {
                            var id = rowObj[i];
                            var operate = "";

                            operate += "<a href=\"javascript:;\" id=\"editrow_sub_" + id + "\" style=\"color:#ff6005\" class=\"hidden\" onclick=\"commonOper.subCreateJackpot(" + id + ",'" + subgrid_table_id + "')\">生成奖池</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                            operate += "<a href=\"javascript:;\" id=\"delrow_sub_" + id + "\" style=\"color:#ff6005\" class=\"hidden\" onclick=\"commonOper.subDelJackpot(" + id + ",'" + subgrid_table_id + "')\">删除奖池</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                            operate += "<a href=\"javascript:;\" id=\"editrow_sub_" + id + "\" style=\"color:#ff6005\" class=\"hidden\" onclick=\"commonOper.subEdit(" + row_id + "," + id + ",'" + subgrid_table_id + "')\">编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                            operate += "<a href=\"javascript:;\" id=\"delrow_sub_" + id + "\" style=\"color:#ff6005\" class=\"hidden\" onclick=\"commonOper.subDelSingle(" + row_id + "," + id + ",'" + subgrid_table_id + "')\">删除</a>";
                            $("#" + subgrid_table_id).jqGrid("setRowData", id, { Operate: operate });
                        }
                    },
                    loadComplete: function (data) {
                        //此事件发生在每个服务器请求后   data表示请求的数据集

                        //权限控制
                        manageDroit.checkPageStyle();
                    },
                });
                $("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false, search: false });
            },
            gridComplete: function () {
                //此事件发生在表格所有数据装入和进程完成后

                //获取某列的值返回数组 
                if (!paraVerify.verifyArr(gridContainer.jqGrid('getCol', 'Operate', false))) {
                    return false;
                }
                var rowObj = gridContainer.jqGrid("getDataIDs");//返回当前grid里的id,类型[array]
                for (var i = 0; i < rowObj.length; i++) {
                    var id = rowObj[i];
                    var operate = "<a href=\"javascript:;\" id=\"editrow_" + id + "\" style=\"color:#ff6005\"class=\"hidden\" onclick=\"commonOper.subAdd(" + id + ")\">添加奖品</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                    operate += "<a href=\"javascript:;\" id=\"editrow_" + id + "\" style=\"color:#ff6005\"class=\"hidden\" onclick=\"commonOper.edit(" + id + ")\">修改</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                    operate += "<a href=\"javascript:;\" id=\"delrow_" + id + "\" style=\"color:#ff6005\"class=\"hidden\" onclick=\"commonOper.delSingle(" + id + ")\">删除</a>";
                    gridContainer.jqGrid("setRowData", id, { Operate: operate });
                }
            },
            loadComplete: function (data) {
                //此事件发生在每个服务器请求后   data表示请求的数据集

                //权限控制
                manageDroit.checkPageStyle();
            },
        });
        gridContainer.jqGrid('navGrid', '#pagernav', { edit: false, add: false, del: false, search: false });
    },
    //新增
    add: function () {
        dialog.showDialog("add", this.options.addEditUrl, "新增" + this.options.title, this.options.addEditDialogHeight, this.options.addEditDialogWidth);
    },
    //编辑
    edit: function (id) {
        dialog.showDialog("edit_" + id, this.options.addEditUrl + "?id=" + id, "编辑" + this.options.title, this.options.addEditDialogHeight, this.options.addEditDialogWidth);
    },
    //删除单个记录
    delSingle: function (id) {
        if (!paraVerify.verifyNum(id)) {
            dialog.ShowTempMessage("请选择要删除的行");
            return false;
        }
        this.delData(id, this.options.url, function () { $("#" + commonOper.options.gridContainerId).trigger("reloadGrid"); });
    },
    //删除数据
    delData: function (arrId, url, callback) {
        if (!paraVerify.verifyStr(arrId)) {
            dialog.ShowTempMessage("参数错误");
            return false;
        }
        if (confirm("您确定要删除吗？")) {
            $.post(url, { action: 'delData', id: arrId, rand: Math.random() }, function (data) {
                if (data.Result == 1) {
                    dialog.ShowTempMessage("删除成功");
                    callback();
                }
                else {
                    dialog.ShowTempMessage(data.Message);
                }
            }, 'json');
        }
    },
    //搜索
    searchGridData: function () {
        $("#" + this.options.gridContainerId).jqGrid('setGridParam', { url: this.options.url + "?action=getList" + this.options.getSearchParameter(), page: 1 }).trigger("reloadGrid");
    },
    //添加奖品
    subAdd: function (sortid, subgrid_table_id) {
        dialog.showDialog("add", "Lottery/LotteryPrizeEdit.aspx?sortid=" + sortid + "&subgrid_table_id=" + subgrid_table_id, "添加奖品", 600, 750);
    },
    //编辑奖品
    subEdit: function (sortid, id, subgrid_table_id) {
        dialog.showDialog("edit_" + id, "Lottery/LotteryPrizeEdit.aspx?sortid=" + sortid + "&id=" + id + "&subgrid_table_id=" + subgrid_table_id, "编辑奖品", 600, 750);
    },
    //删除奖品
    subDelSingle: function (id, subgrid_table_id) {
        if (!paraVerify.verifyNum(id)) {
            dialog.ShowTempMessage("请选择要删除的行");
            return false;
        }
        this.delData(id, this.options.subUrl, function () { $("#" + subgrid_table_id).trigger("reloadGrid"); });
    },
    //创建奖池记录
    subCreateJackpot: function (id, subgrid_table_id) {
        if (!paraVerify.verifyNum(id)) {
            dialog.ShowTempMessage("参数错误");
            return false;
        }
        $.post(this.options.subUrl, { action: 'createJackpot', id: id, rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                dialog.ShowTempMessage("生成奖池成功");
                $("#" + subgrid_table_id).trigger("reloadGrid");
            }
            else {
                dialog.ShowTempMessage(data.Message);
            }
        }, 'json');
    },
    //删除奖池记录
    subDelJackpot: function (id, subgrid_table_id) {
        dialog.showDialog("del_" + id, "Lottery/LotteryJackpotDel.aspx?id=" + id + "&subgrid_table_id=" + subgrid_table_id, "删除奖池记录", 200, 400);
    },
}

$(function () {
    commonOper.jqGridInit();

    $("#addBtn").bind('click', function () {
        commonOper.add();
    });
    $("#submitButton").bind('click', function () {
        commonOper.searchGridData();
    });
});
