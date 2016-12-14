<%@ Page Title="" Language="C#" MasterPageFile="~/Master/ListMaster.Master" AutoEventWireup="true" CodeBehind="LotteryList.aspx.cs" Inherits="Web.Manage.Activity.LotteryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadTop" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadOtherCss" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadOtherJS" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="server">
    <div class="title">查询条件</div>
    <div>
        活动名称：<input type="text" id="txtSortName" name="txtSortName" class="txt_able" />
        开始日期：<input type="text" id="beginTime" onclick="WdatePicker()" name="beginTime" class="txt_able" />到&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="text" id="endTime" name="endTime" onclick="WdatePicker()" class="txt_able" />
        <button id="submitButton" class="btn">查询</button>
        <button id="addBtn" class="btn hidden">新增</button>
    </div>
    <div class="title">查询结果</div>
    <table id="navgrid"></table>
    <div id="pagernav"></div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FootOtherJS" runat="server">
    <script type="text/javascript" src="../js/Activity/LotteryList.js"></script>
</asp:Content>
