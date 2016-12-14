<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="DroitList.aspx.cs" Inherits="Web.Manage.User.DroitList" %>

<%@ Import Namespace="YYT.Model" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title>用户权限</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        名称：<input type="text" id="txtTitle" name="txtTitle" class="txt_able" />
        主菜单：<select id="menuId" name="menuId" class="select_able">
            <option value="0">请选择菜单</option>
            <%foreach (HT_Menu admin_MenuModel in admin_MenuList)
                { %>
            <option value="<%=admin_MenuModel.id %>"><%=admin_MenuModel.Title %></option>
            <% } %>
        </select>
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
                url: "/data/User/droit.ashx",
                addEditUrl: "/User/DroitEdit.aspx",
                title: "权限",
                addEditDialogHeight: 600,
                addEditDialogWidth: 600,
                gridRowOper: {
                    edit: true,
                    del: true,
                },
                colNames: ['Id', '名称', '权限', '菜单', '链接', '状态', '排序', '创建日期', '操作'],
                colModel: [{ name: 'id', index: 'id', width: 55, align: "center", sortable: false },
                    { name: 'Title', index: 'Title', width: 80, align: "center", sortable: false },
                    { name: 'Droit', index: 'Droit', width: 50, align: "center", sortable: false },
                    { name: 'isMenu', index: 'isMenu', width: 50, align: "center", sortable: false, hidden: true },
                    { name: 'Url', index: 'Url', width: 120, align: "center", sortable: false },
                    { name: 'status', index: 'status', width: 50, align: "center", sortable: false, formatter: commonJqGrid.defaultState },
                    { name: 'SortId', index: 'SortId', width: 50, align: "center", sortable: false },
                    { name: 'createtime', index: 'createtime', width: 50, align: "center", sortable: false, formatter: commonJqGrid.formatTime },
                    { name: 'Operate', index: 'Operate', width: 80, align: "center", sortable: false },
                ],

                getSearchParameter: function () {
                    var title = $("#txtTitle").val();
                    var menuid = $("#menuId").val();
                    return '&title=' + title + '&menuid=' + menuid;
                }
            });
        });
    </script>
</asp:Content>
