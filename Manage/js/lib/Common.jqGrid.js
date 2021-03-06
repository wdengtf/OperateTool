﻿var commonJqGrid = {
    //参数初始化
    options: {
        gridContainerId: 'navgrid',//填充列表id
        jsonReaderId: 'id',//jsonReader Id字段[当在编辑数据模块中发送数据时，使用的colModel的名称.不设置默认为行索引]
        title: "",//栏目名称
        url: "", //列表Url
        urlParm: '',//列表参数
        colNames: [],//列表标题
        colModel: [],//列表值
        rowNum: 15,//列表显示行数
        multiselect: false,//显示多选框
        getSearchParameter: function () { }, //查询参数
        //列表操作权限
        gridRowOper: {
            add: false,
            edit: false,
            del: false,
            exports: false
        },
        gridCompleteCallBack: null,//列表在行上面的操作回调
        addEditUrl: "",//新增编辑操作Url
        addEditDialogHeight: 600,//新增编辑操作弹层高度
        addEditDialogWidth: 700,//新增编辑操作弹层宽度

        //子表参数
        subGrid: false,//显示子列表
        subJsonReaderId: 'id',//jsonReader Id字段[用户操作时候获取id]
        subTitle: "",//栏目名称
        subUrl: "",//Url
        subColNames: [],//标题
        subColModel: [],//值
        subAddEditUrl: "",//新增编辑操作Url
        subaddEditDialogHeight: 600,//高度
        subaddEditDialogWidth:700,//宽度
        //操作权限
        subGridRowOper: {
            add: false,
            edit: false,
            del: false,
            exports: false
        },
        subGridCompleteCallBack: null,//操作回调
    },
    init: function (optionsObject) {
        var _this = this;
        _this.options.url = optionsObject.url;
        _this.options.addEditUrl = optionsObject.addEditUrl;
        _this.options.title = optionsObject.title;
        _this.options.colNames = optionsObject.colNames;
        _this.options.colModel = optionsObject.colModel;
        _this.options.getSearchParameter = optionsObject.getSearchParameter;

        if (optionsObject.rowNum) _this.options.rowNum = optionsObject.rowNum;
        if (optionsObject.multiselect) _this.options.multiselect = optionsObject.multiselect;
        if (optionsObject.urlParm) _this.options.urlParm = optionsObject.urlParm;
        if (optionsObject.addEditDialogHeight) _this.options.addEditDialogHeight = optionsObject.addEditDialogHeight;
        if (optionsObject.addEditDialogWidth) _this.options.addEditDialogWidth = optionsObject.addEditDialogWidth;
        if (optionsObject.gridContainerId) _this.options.gridContainerId = optionsObject.gridContainerId;
        if (optionsObject.gridCompleteCallBack) _this.options.gridCompleteCallBack = optionsObject.gridCompleteCallBack;
        if (paraVerify.verifyJson(optionsObject.gridRowOper)) _this.options.gridRowOper = optionsObject.gridRowOper;
        if (optionsObject.jsonReaderId) _this.options.jsonReaderId = optionsObject.jsonReaderId;

        if (optionsObject.subGrid) _this.options.subGrid = optionsObject.subGrid;
        if (_this.options.subGrid) {
            if (optionsObject.subColNames) _this.options.subColNames = optionsObject.subColNames;
            if (optionsObject.subColModel) _this.options.subColModel = optionsObject.subColModel;
            if (optionsObject.subUrl) _this.options.subUrl = optionsObject.subUrl;
            if (optionsObject.subAddEditUrl) _this.options.subAddEditUrl = optionsObject.subAddEditUrl;
            if (optionsObject.subTitle) _this.options.subTitle = optionsObject.subTitle;
            if (optionsObject.subaddEditDialogHeight) _this.options.subaddEditDialogHeight = optionsObject.subaddEditDialogHeight;
            if (optionsObject.subaddEditDialogWidth) _this.options.subaddEditDialogWidth = optionsObject.subaddEditDialogWidth;
            if (optionsObject.subGridCompleteCallBack) _this.options.subGridCompleteCallBack = optionsObject.subGridCompleteCallBack;
            if (paraVerify.verifyJson(optionsObject.subGridRowOper)) _this.options.subGridRowOper = optionsObject.subGridRowOper;
            if (optionsObject.subJsonReaderId) _this.options.subJsonReaderId = optionsObject.subJsonReaderId;
        }

        _this.jqGridInit();

        //查询
        if (paraVerify.verifyObj($("#submitButton"))) {
            $("#submitButton").bind('click', function () {
                _this.searchGridData();
            });
        }
        //添加
        if (paraVerify.verifyObj($("#addBtn"))) {
            $("#addBtn").bind('click', function () {
                _this.add();
            });
        }
        //删除
        if (paraVerify.verifyObj($("#delBtn"))) {
            $("#delBtn").bind('click', function () {
                _this.delMultiple();
            });
        }

    },
    //jqGrid初始化
    jqGridInit: function () {
        var gridContainer = $("#" + this.options.gridContainerId);
        gridContainer.jqGrid({
            url: this.options.url + '?action=getList' + this.options.urlParm,
            height: 'auto',
            datatype: "json",
            colNames: this.options.colNames,
            colModel: this.options.colModel,
            rowNum: this.options.rowNum,
            rowList: [10, 15, 20, 30, 50],
            jsonReader: {
                root: "Data.rows",              //json中代表实际模型数据的入口
                page: "Data.pageIndex",         //json中代表当前页码的数据
                total: "Data.totalPage",        //json中代表页码总数的数据
                records: "Data.totalRecord",    //json中代表数据行总数的数据
                repeatitems: false,             //如果repeatitems为false，json 中数据可以乱序，并且允许数据空缺
                id: this.options.jsonReaderId   //表示当在编辑数据模块中发送数据时，使用的id的名称.不设置默认为行索引
            },
            multiselect: this.options.multiselect,
            pager: '#pagernav',
            mtype: "post",
            sortname: this.options.jsonReaderId,
            viewrecords: true,
            autowidth: true,
            subGrid: this.options.subGrid,
            subGridRowExpanded: function (subgrid_id, row_id) {
                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll' ></table><div id='" + pager_id + "' class='scroll'></div>");
                $("#" + subgrid_table_id).jqGrid({
                    url: commonJqGrid.options.subUrl + "?action=getList&id=" + row_id,
                    height: "auto",
                    datatype: "json",
                    colNames: commonJqGrid.options.subColNames,
                    colModel: commonJqGrid.options.subColModel,
                    rowNum: 20,
                    rowList: [10, 20, 30],
                    jsonReader: {
                        root: "Data.rows",              //json中代表实际模型数据的入口
                        page: "Data.pageIndex",         //json中代表当前页码的数据
                        total: "Data.totalPage",        //json中代表页码总数的数据
                        records: "Data.totalRecord",    //json中代表数据行总数的数据
                        repeatitems: false,             //如果repeatitems为false，json 中数据可以乱序，并且允许数据空缺
                        id: commonJqGrid.options.subJsonReaderId                      //表示当在编辑数据模块中发送数据时，使用的id的名称.不设置默认为行索引
                    },
                    pager: pager_id,
                    sortname: commonJqGrid.options.subJsonReaderId,
                    autowidth: true,
                    gridComplete: function () {
                        // 如果自定义装载完成方法不为null, 执行回调
                        if (commonJqGrid.options.subGridCompleteCallBack != null) {
                            commonJqGrid.options.subGridCompleteCallBack(subgrid_table_id, row_id);
                            return;
                        }

                        //此事件发生在表格所有数据装入和进程完成后
                        //获取某列的值返回数组 
                        if (!paraVerify.verifyArr($("#" + subgrid_table_id).jqGrid('getCol', 'Operate', false))) {
                            return false;
                        }

                        if (!paraVerify.verifyJson(commonJqGrid.options.subGridRowOper)) {
                            return false;
                        }

                        var rowObj = $("#" + subgrid_table_id).jqGrid("getDataIDs");//返回当前grid里的id,类型[array]
                        for (var i = 0; i < rowObj.length; i++) {
                            var id = rowObj[i];

                            var operate = "";
                            if (commonJqGrid.options.subGridRowOper.edit)
                                operate += commonJqGrid.subEditGrid(id, subgrid_table_id);
                            if (commonJqGrid.options.subGridRowOper.del)
                                operate += commonJqGrid.subDelGrid(id, subgrid_table_id);
                            $("#" + subgrid_table_id).jqGrid("setRowData", id, { Operate: operate });
                        }
                    },
                    loadComplete: function (data) {
                        //此事件发生在每个服务器请求后   data表示请求的数据集

                        //权限控制
                        //manageDroit.checkPageStyle();
                    },
                });
                jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false, search: false });
            },
            gridComplete: function () {
                // 如果自定义装载完成方法不为null, 执行回调
                if (commonJqGrid.options.gridCompleteCallBack != null) {
                    commonJqGrid.options.gridCompleteCallBack(commonJqGrid.options.gridContainerId);
                    return;
                }
                //此事件发生在表格所有数据装入和进程完成后

                //获取某列的值返回数组 
                if (!paraVerify.verifyArr(gridContainer.jqGrid('getCol', 'Operate', false))) {
                    return false;
                }

                if (!paraVerify.verifyJson(commonJqGrid.options.gridRowOper)) {
                    return false;
                }
                var rowObj = gridContainer.jqGrid("getDataIDs");//返回当前grid里的id,类型[array]
                for (var i = 0; i < rowObj.length; i++) {
                    var id = rowObj[i];
                    var operate = "";
                    if (commonJqGrid.options.gridRowOper.edit)
                        operate += commonJqGrid.editGrid(id);
                    if (commonJqGrid.options.gridRowOper.del)
                        operate += commonJqGrid.delGrid(id);
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
        this.delData(id, this.options.url);
    },
    //删除多个记录
    delMultiple: function () {
        //可以多选时，返回选中行的ID 
        var arrId = $("#" + this.options.gridContainerId).jqGrid('getGridParam', 'selarrrow');
        if (!paraVerify.verifyArr(arrId)) {
            dialog.ShowTempMessage("请选择要删除的行");
            return false;
        }
        this.delData(arrId.join(","), this.options.url);
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
                    if (callback)
                        callback();
                    else
                        $("#" + commonJqGrid.options.gridContainerId).trigger("reloadGrid");
                }
                else {
                    dialog.ShowTempMessage(data.Message);
                }
            }, 'json');
        }
    },
    //编辑样式
    editGrid: function (id) {
        return "<a href=\"javascript:;\" id=\"editrow_" + id + "\" style=\"color:#ff6005\" class=\"hidden\" onclick=\"commonJqGrid.edit(" + id + ")\">编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;";
    },
    //删除样式
    delGrid: function (id) {
        return "<a href=\"javascript:;\" id=\"delrow_" + id + "\" style=\"color:#ff6005\"class=\"hidden\" onclick=\"commonJqGrid.delSingle(" + id + ")\">删除</a>&nbsp;&nbsp;&nbsp;&nbsp;"
    },
    //子表编辑
    subEdit: function (id, subgrid_table_id) {
        dialog.showDialog("edit_" + id, this.options.subAddEditUrl + "?id=" + id + "&subgrid_table_id=" + subgrid_table_id, "编辑" + this.options.subTitle, this.options.subaddEditDialogHeight, this.options.subaddEditDialogWidth);
    },
    //子表删除
    subDelSingle: function (id, subgrid_table_id) {
        if (!paraVerify.verifyNum(id)) {
            dialog.ShowTempMessage("请选择要删除的行");
            return false;
        }
        this.delData(id, this.options.subUrl, function () { $("#" + subgrid_table_id).trigger("reloadGrid"); });
    },
    //子表编辑样式
    subEditGrid: function (id, subgrid_table_id) {
        return "<a href=\"javascript:;\" id=\"editrow_sub_" + id + "\" style=\"color:#ff6005\"  onclick=\"commonJqGrid.subEdit(" + id + ",'" + subgrid_table_id + "')\">编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;";
    },
    //子表删除样式
    subDelGrid: function (id, subgrid_table_id) {
        return "<a href=\"javascript:;\" id=\"delrow_sub_" + id + "\" style=\"color:#ff6005\" onclick=\"commonJqGrid.subDelSingle(" + id + ",'" + subgrid_table_id + "')\">删除</a>&nbsp;&nbsp;&nbsp;&nbsp;"
    },
    //搜索
    searchGridData: function () {
        $("#" + this.options.gridContainerId).jqGrid('setGridParam', { url: this.options.url + "?action=getList" + this.options.getSearchParameter(), page: 1 }).trigger("reloadGrid");
    },
    //默认状态
    defaultState: function (cellvalue, options, rowdata) {
        var value = "--";
        switch (cellvalue) {
            case 0:
                value = '锁定';
                break;
            case 1:
                value = '正常';
                break;
        }
        return value;
    },
    //默认是否
    defaultYesOrNo: function (cellvalue, options, rowdata) {
        var value = "--";
        switch (cellvalue) {
            case 0:
                value = '否';
                break;
            case 1:
                value = '是';
                break;
        }
        return value;
    },
    //默认图片
    defaultImg: function (cellvalue, options, rowdata) {
        if (paraVerify.verifyStr(cellvalue))
            return '<img src="' + commonFun.returnImgUrl(cellvalue) + '" style="width:60px;height:60px;" />';
        else
            return '<img src="/images/logined_default.png" style="width:60px;height:60px;" />';
    },
    //时间转日期
    timeTransDate: function (cellvalue, options, rowdata) {
        return commonFun.timeTransDate(cellvalue);
    },
    //时间格式化
    formatTime: function (cellvalue, options, rowdata) {
        return commonFun.formatTime(cellvalue);
    },
}