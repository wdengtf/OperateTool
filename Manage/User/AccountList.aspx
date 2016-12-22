<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="Web.Manage.User.AccountList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title>用户账户</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        名称：<input type="text" id="txtUsername" name="txtTitle" class="txt_able" />
        <button id="submitButton" class="btn">查询</button>
        <button id="addBtn" class="btn hidden">新增</button>
        <button id="delBtn" class="btn hidden">删除</button>
    </div>

    <div class="title">查询结果</div>
    <table id="navgrid"></table>
    <div id="pagernav"></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
    <script type="text/javascript">
        $(function () {
            commonJqGrid.init({
                url: "/data/User/account.ashx",
                addEditUrl: "/User/AccountEdit.aspx",
                title: "账户",
                addEditDialogHeight: 600,
                addEditDialogWidth: 600,
                multiselect: true,
                gridRowOper: {
                    edit: true,
                    del: true,
                },
                colNames: ['Id', '账户名', '权限组', '状态', '创建日期', '操作'],
                colModel: [{ name: 'id', index: 'id', width: 55, align: "center", sortable: false },
                { name: 'username', index: 'username', width: 80, align: "center", sortable: false },
                { name: 'groupid', index: 'groupid', width: 50, align: "center", sortable: false },
                { name: 'status', index: 'status', width: 50, align: "center", sortable: false, formatter: commonJqGrid.defaultState },
                { name: 'createtime', index: 'createtime', width: 120, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                { name: 'Operate', index: 'Operate', width: 80, align: "center", sortable: false },
                ],
                getSearchParameter: function () {
                    var userName = $("#txtUsername").val();
                    return '&userName=' + userName;
                }
            });
        });
    </script>
</asp:Content>
