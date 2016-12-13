//后台权限
var manageDroit = {
    //权限对象
    createDroitObj: function () {
        return {
            add: false,
            edit: false,
            del: false,
            exports: false
        }
    },
    //获取权限根据Pid
    getDroit: function (opertDroit, pid) {
        var droitObj = this.createDroitObj();

        if (!paraVerify.verifyNum(pid) || !opertDroit || opertDroit.length == 0)
            return droitObj;

        opertDroit = eval(opertDroit);

        var arrDroit = [];
        for (var i = 0; i < opertDroit.length; i++) {
            var droit = opertDroit[i];
            if (droit.Pid == pid) {
                arrDroit.push(droit.Droit);
            }
        }
        if (!paraVerify.verifyArr(arrDroit))
            return droitObj;

        for (var i = 0; i < arrDroit.length; i++) {
            if (arrDroit[i] == "add")
                droitObj.add = true;
            else if (arrDroit[i] == "edit")
                droitObj.edit = true;
            else if (arrDroit[i] == "del")
                droitObj.del = true;
            else if (arrDroit[i] == "export")
                droitObj.exports = true;
        }
        return droitObj;
    },
    //根据Url获取列表id
    getListIdByUrl: function (opertDroit, url) {
        if (!opertDroit || opertDroit.length == 0)
            return 0;
        opertDroit = eval(opertDroit);
        for (var i = 0; i < opertDroit.length; i++) {
            var droit = opertDroit[i];
            if (paraVerify.verifyStr(droit.Url) && (url.indexOf(droit.Url) > -1 || droit.Url.indexOf(url) > -1)) {
                return droit.Id;
            }
        }
        return 0;
    },
    //验证权限
    checkAddDroit: function (droitObj, doritType) {
        if (!paraVerify.verifyObj(droitObj))
            return false;

        if (doritType == "add")
            return droitObj.add;
        else if (doritType == "edit")
            return droitObj.edit;
        else if (doritType == "del")
            return droitObj.del;
        else if (doritType == "export")
            return droitObj.exports;

        return false;
    },
    //封装权限页面样式
    checkPageStyle: function () {
        //如果是在列表页弹出窗口 所有权限不做验证
        if (window.name == "iframe_dialog") {
            if (paraVerify.verifyObj($("#addBtn")))
                $("#addBtn").removeClass("hidden");
            if (paraVerify.verifyObj($("[id^='editrow_']")))
                $("[id^='editrow_']").removeClass("hidden");
            if (paraVerify.verifyObj($("#delBtn")))
                $("#delBtn").removeClass("hidden");
            if (paraVerify.verifyObj($("[id^='delrow_']")))
                $("[id^='delrow_']").removeClass("hidden");
            if (paraVerify.verifyObj($("#addExprot")))
                $("#addExprot").removeClass("hidden");

            return;
        }
        
        if (!paraVerify.verifyObj($(window.top.frames["mainTop"])))
            return false;

        var operDroit = $(window.top.frames["mainTop"].document).find("#userOperDorit").html();
        var pid = commonFun.getQueryString("temid");
        ////根据url获取权限
        //var pathname = window.location.pathname;
        //var pid = this.getListIdByUrl(operDroit, pathname);
        var droitObj = this.getDroit(operDroit, pid);
        if (droitObj.add) {
            if (paraVerify.verifyObj($("#addBtn")))
                $("#addBtn").removeClass("hidden");
        }
        if (droitObj.edit) {
            if (paraVerify.verifyObj($("[id^='editrow_']")))
                $("[id^='editrow_']").removeClass("hidden");
        }
        if (droitObj.del) {
            if (paraVerify.verifyObj($("#delBtn")))
                $("#delBtn").removeClass("hidden");
            if (paraVerify.verifyObj($("[id^='delrow_']")))
                $("[id^='delrow_']").removeClass("hidden");
        }
        if (droitObj.exports) {
            if (paraVerify.verifyObj($("#addExprot")))
                $("#addExprot").removeClass("hidden");
        }
    },
}