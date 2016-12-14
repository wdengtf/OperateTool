<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="Web.Manage.Member.MemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
    <title>会员列表</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        手机号：<input type="text" id="txtMobile" name="txtMobile" class="txt_able" />
        outId：<input type="text" id="txtOutid" name="txtOutid" class="txt_able" />
        注册日期：<input type="text" id="beginTime" onclick="WdatePicker()" name="beginTime" class="txt_able" />到&nbsp;&nbsp;&nbsp;&nbsp;
    <input type="text" id="endTime" name="endTime" onclick="WdatePicker()" class="txt_able" />
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
                url: "/data/Member/memberList.ashx",
                addEditUrl: "",
                title: "会员",
                addEditDialogHeight: 600,
                addEditDialogWidth: 600,
                rowNum: 10,
                colNames: ['id', '图像', '手机号', '平台类型', '平台id', '昵称', 'Email', '地址', '创建日期', '最后更新日期'],
                colModel: [{ name: 'Id', index: 'Id', width: 40, align: "center", sortable: false },
                 { name: 'Headurl', index: 'Headurl', width: 60, align: "center", sortable: false, formatter: imgFormatter },
                 { name: 'Mobile', index: 'Mobile', width: 80, align: "center", sortable: false },
                 { name: 'data_type', index: 'data_type', width: 55, align: "center", sortable: false },
                 { name: 'out_id', index: 'out_id', width: 100, align: "center", sortable: false },
                 { name: 'nickname', index: 'nickname', width: 60, align: "center", sortable: false },
                 { name: 'email', index: 'email', width: 80, align: "center", sortable: false },
                 { name: 'addr', index: 'addr', width: 120, align: "center", sortable: false },
                 { name: 'Create_time', index: 'Create_time', width: 80, align: "center", sortable: false },
                 { name: 'Updatetime', index: 'Updatetime', width: 80, align: "center", sortable: false },
                ],
                getSearchParameter: function () {
                    var mobile = $("#txtMobile").val();
                    var outid = $("#txtOutid").val();
                    var beginTime = $("#beginTime").val();
                    var endTime = $("#endTime").val();
                    return "&mobile=" + mobile + "&beginTime=" + beginTime + "&endTime=" + endTime + "&outid=" + outid;
                }
            });
            //图片格式化
            function imgFormatter(cellvalue, options, rowdata) {
                if (paraVerify.verifyStr(cellvalue))
                    return '<img src="' + commonFun.returnImgUrl(cellvalue) + '" style="width:60px;height:60px;" />';
                else
                    return '<img src="../../images/logined_default.png" style="width:60px;height:60px;" />';
            }
        });
    </script>
</asp:Content>
