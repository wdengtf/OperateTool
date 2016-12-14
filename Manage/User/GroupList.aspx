<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="Web.Manage.User.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title>用户组</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
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
                url: "/data/User/group.ashx",
                addEditUrl: "/User/GroupEdit.aspx",
                title: "组",
                addEditDialogHeight: 600,
                addEditDialogWidth: 600,
                gridRowOper: {
                    edit: true,
                    del: true,
                },
                colNames: ['Id', '用户组','创建日期', '操作'],
                colModel: [{ name: 'id', index: 'id', width: 55, align: "center", sortable: false },
                { name: 'Title', index: 'Title', width: 80, align: "center", sortable: false },
                { name: 'Createtime', index: 'Createtime', width: 50, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                { name: 'Operate', index: 'Operate', width: 80, align: "center", sortable: false },
                ],
            });
        });
    </script>
</asp:Content>
