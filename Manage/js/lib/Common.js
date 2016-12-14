//窗口操作
var dialog = {
    //统一弹框
    showDialog: function (id, url, title, height, width) {
        if ($("#" + id).length > 0) {
            dialog.ShowTempMessage("弹窗已存在");
            return false;
        }

        var $this = "<div id='" + id + "'></div>";// style='overflow:hidden;'
        jQuery($this).append(jQuery("<iframe frameborder='0' width='100%' height='100%' name='iframe_dialog' id='frm_" + id + "'  ></iframe>")).dialog({
            show: "scale",
            autoOpen: true,
            modal: true,
            height: height,
            width: width,
            resizable: true,
            title: title,
            close: function () {
                //关闭弹框
                dialog.closeDialog(id);
            },
            open: function (event, ui) {
                //打开
                dialog.AdjustDialogButton(this);
                var iframOpen = setInterval(function () {
                    $("#frm_" + id).attr("src", url);
                    window.clearInterval(iframOpen);
                }, 1000);
            },
            position: {
                my: 'center top+1%',
                at: 'center top',
                of: 'body'
            }
        });
    },
    //跳转弹框按钮
    AdjustDialogButton: function (oThis) {
        var dw = $(oThis.parentElement).outerWidth();
        var bw = $(oThis.parentElement).find(".ui-dialog-buttonset").outerWidth();
        $(oThis.parentElement).find(".ui-dialog-buttonset").css("float", "none");
        $(oThis.parentElement).find(".ui-dialog-buttonset").css("width", (bw + 3) + "px");
        $(oThis.parentElement).find(".ui-dialog-buttonset").css("position", "relative");
        $(oThis.parentElement).find(".ui-dialog-buttonset").css("left", parseInt((dw - bw + 1) / 2) + "px");
        $($(oThis.parentElement).find(".ui-dialog-buttonset").find(".ui-button-text")[0]).css("background-color", "#79c4cb");
        $($(oThis.parentElement).find(".ui-dialog-buttonset").find(".ui-button-text")[0]).css("color", "#fff");
    },
    //统一弹出层提示
    ShowTempMessage: function (msg, delaySeconds, option) {
        if (top && top.Dialog && top.Dialog.tip) {
            top.Dialog.tip(msg, delaySeconds);
            return;
        }
        if (msg == "") return;
        var Tip = $('<span>' + msg + '</span>'),
            move = 30;
        option = {
            position: 'absolute',
            padding: '5px 10px',
            color: '#fff',
            left: '50%',
            top: '50%',
            opacity: 0,
            "line-height": "20px",
            'z-index': 99999,
            'background-color': '#333',
            'margin-top': -Tip.outerHeight() / 2,
            'margin-left': -Tip.outerWidth() / 2
        }
        Tip.appendTo(document.body).css(option);
        if (Tip.width() > 300) {
            Tip.css({ width: 300 });
            Tip.css({ 'margin-left': -Tip.outerWidth() / 2 });
        }
        Tip.addClass("tip");

        var showTipTimer = setTimeout(function () {
            var top = Tip.offset().top;
            Tip.css({
                top: top + move / 2
            });
            Tip.animate({
                top: top - move / 2,
                opacity: 1
            }, function () {
                setTimeout(function () {
                    var top = Tip.offset().top;
                    Tip.animate({
                        top: top - move,
                        opacity: 0
                    }, function () {
                        Tip.remove();
                    });
                }, delaySeconds || 1000);
            });
        });
        return Tip;
    },
    //关闭弹出框
    closeDialog: function (id) {
        if ($("#" + id).length > 0) {
            $("#" + id).remove();
        }
    },
    //关闭弹出框并弹出提示信息
    closeDialogAlertMsg: function (id, title) {
        dialog.ShowTempMessage(title);
        dialog.closeDialog(id);
    },
    //关闭弹出框并弹出提示信息并刷新
    closeDialogAlertMsgRefer: function (id, title) {
        dialog.ShowTempMessage(title);
        dialog.closeDialog(id);
        setTimeout(function () { location.reload() }, 1000);
    },
    //关闭弹出框并弹出提示信息并JQGrid刷新
    closeDialogAlertMsgReferJqGrid: function (id, title, jqGridId) {
        if (!paraVerify.verifyStr(jqGridId))
            jqGridId = "navgrid";

        dialog.ShowTempMessage(title);
        dialog.closeDialog(id);
        $("#" + jqGridId).trigger("reloadGrid");
    }
}

//参数验证
var paraVerify = {
    //参数过滤
    filterParm: function (str) {
        if (str == "" || typeof (str) == "undefined" || !str)
            return "";
        str = str.toString();
        str = str.replace("<", "");
        str = str.replace(">", "");
        str = str.replace(";", "");
        str = str.replace("'", "");
        str = str.replace("/", "");
        return str;
    },
    //验证是否为数字
    verifyNum: function (str) {
        if (isNaN(str))
            return false;
        if (str < 1) return false;
        return true;
    },
    //验证字符串
    verifyStr: function (str) {
        str = paraVerify.filterParm(str);
        if (str == "" || typeof (str) == "undefined" || !str)
            return false;
        return true;
    },
    //验证数组
    verifyArr: function (arr) {
        if (arr && arr.length > 0)
            return true;
        else
            return false;
    },
    //验证对象
    verifyObj: function (obj) {
        if (obj && obj.length > 0)
            return true;
        else
            return false;
    },
    //验证Json
    verifyJson: function (obj) {
        if (typeof (obj) == "object" &&
          Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length) {
            return true;
        }
        else
            return false;
    },
    //验证邮箱
    verifyEmail: function (str) {
        var reg = /(^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$)/;
        if (!reg.test(str))
            return false;
        return true;
    },
    //验证是否为手机
    verifyMobile: function (str) {
        var reg = /(^0*(13|14|15|17|18)\d{9}$)/;
        if (!reg.test(str))
            return false;
        return true;
    }
}

//公共通用函数
var commonFun = {
    //图片前缀
    imgBaseUrl: function () { return 'http://img.wangcl.com/'; },
    //返回图片路径
    returnImgUrl: function (str) {
        if (!paraVerify.verifyStr(str))
            return "";

        if (str.indexOf("http://") > -1)
            return str;
        else
            return commonFun.imgBaseUrl() + str;
    },
    //获取url参数
    getQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return (r[2]); return "";
    },
    //转日期
    formatDate: function (val) {
        var re = /-?\d+/;
        var m = re.exec(val);
        var d = new Date(parseInt(m[0]));
        // 按【2012-02-13】的格式返回日期
        return d.format("yyyy-MM-dd");
    },
    //转时间
    formatTime: function (val) {
        var re = /-?\d+/;
        var m = re.exec(val);
        var d = new Date(parseInt(m[0]));
        // 按【2012-02-13 09:09:09】的格式返回日期
        return d.format("yyyy-MM-dd hh:mm:ss");
    },
    timeTransDate: function (val) {
        if (!paraVerify.verifyStr(val))
            return val;
        if (val.indexOf('T') > 0)
            return val.split('T')[0];
        else
            return val.split(' ')[0];
    },
}