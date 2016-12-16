﻿$(function () {
    //function aaa() {
    //    console.log("aaa");
    //}
    //ajax.getLotteryActivity(aaa);
    //ajax.memberBindLottery(aaa);
});

var ajax = {
    //加载抽奖活动 作用判断活动时候可以进行
    getLotteryActivity: function (callback) {
        $.post("/Data/Lottery.ashx", { action: 'getLotteryActivity', rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                //活动加载成功回调
                callback();
            }
            else {
                alert(data.Message);
            }
        }, 'json');
    },
    //绑定抽奖信息 点击生蛋调此方法
    memberBindLottery: function () {
        $.post("http://newacampaign.csais.me/Data/Lottery.ashx", { action: 'memberBindLottery', rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                //绑定抽奖信息成功回调 1：NEWA美容仪，2：NEWA传用LIFT凝胶 3：NEWA传用LIFT凝胶 4：200优惠券
                return data.Data;
            }
            else {
                alert(data.Message);
            }
        }, 'json');
    },
    //提交表单
    submitForm: function () {
        var name = $(".fts .ftsinp1").val();
        var mobile = $(".fts .ftsinp2").val();
        var addr = $(".fts .ftsinp3").val();

        if (paraVerify.verifyStr(name) && paraVerify.verifyStr(mobile) && paraVerify.verifyStr(addr)) {
            $.post("/Data/Member.ashx", { action: 'updateMember', name: name, mobile: mobile, addr: addr, rand: Math.random() }, function (data) {
                if (data.Result == 1) {
                    alert("信息提交成功");
                }
                else {
                    alert(data.Message);
                }
            }, 'json');
        }
        else {
            alert("请填写完整");
        }
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