$(function () {
    $("#subft").bind('click', function () {
        ajax.submitForm();
    })
    wechat.createSharePage(wx);
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
        $.post("/Data/Lottery.ashx", { action: 'memberBindLottery', rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                //绑定抽奖信息成功回调 1：NEWA美容仪，2：NEWA传用LIFT凝胶 3：NEWA专属圣诞礼盒 4：200优惠券
                return data.Data;
            }
            else {
                alert(data.Message);
            }
        }, 'json');
    },
    //提交表单
    submitForm: function () {
        var name = $("#ftsinp1").val();
        var mobile = $("#ftsinp2").val();
        var addr = $("#ftsinp3").val();

        if (paraVerify.verifyStr(name) && paraVerify.verifyStr(mobile) && paraVerify.verifyStr(addr)) {
            $.post("/Data/Member.ashx", { action: 'updateMember', name: name, mobile: mobile, addr: addr, rand: Math.random() }, function (data) {
                if (data.Result == 1) {
                    //alert("信息提交成功");
                }
                else {
                    paraVerify.ShowTempMessage(data.Message);
                    //alert(data.Message);
                }
            }, 'json');
        }
        else {
            //alert("请填写完整");
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
    },
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
    }
}


//微信信息
var wechat = {
    //判断是否为微信浏览器
    wxBrowers: function () {
        var ua = navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            return true;
        } else {
            return false;
        }
    },
    //获取微信配置参数
    wxConfig: function (wx, callback) {
        var url = location.href;
        $.post("/Data/Wechat.ashx",
           {
               action: "wxConfig",
               url: url,
               sign: "",
               rand: Math.random()
           },
           function (data) {
               if (data.Result == 1) {
                   var obj = data.Data;
                   if ($.type(obj) == "object" || $.type(obj) == "array") {
                       wx.config({
                           debug: false,
                           appId: obj.appid,
                           timestamp: obj.timestamp,
                           nonceStr: obj.nonceStr,
                           signature: obj.signature,
                           url: url,
                           jsApiList: [
                                'checkJsApi',
                                'onMenuShareTimeline',
                                'onMenuShareAppMessage',
                                'onMenuShareQQ',
                                'onMenuShareWeibo',
                                'getLocation'
                           ]
                       });

                       callback();
                   }
               }
               else {
                   return false;
               }
           }, 'json');
    },
    //微信分享内容设置
    wxShareContact: function (wx, link, imgUrl, title, desc, Circle_desc) {
        wx.ready(function () {
            //分享给朋友
            wx.onMenuShareAppMessage({
                title: title,
                desc: desc,
                link: link,
                imgUrl: imgUrl,
                success: function () {
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                }
            });
            //分享给朋友圈
            wx.onMenuShareTimeline({
                title: Circle_desc,
                desc: Circle_desc,
                link: link,
                imgUrl: imgUrl,
                success: function () {
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
        });
    },
    //当含有参数时，新的参数中的属性覆盖到原有的属性，当没有时，则使用默认的[图片路径，标题，发到朋友_详情，发到朋友圈_详情]
    createSharePage: function (wx, options) {
        var defaults = {
            imgUrl: "http://newacampaign.csais.me/static/img/shareimg.png",
            title:  "NEWA美容仪邀你一起“生蛋快乐”",
            desc: "生蛋有惊喜哦",
            Circle_desc: this.desc
        };
        if (typeof options === 'undefined') {
            options = defaults;
        } else {
            options = $.extend({}, defaults, options);
        }
        var link = location.href.split('#')[0];

        var wxShare = this.wxShareContact(wx, link, options.imgUrl, options.title, options.desc, options.Circle_desc);
        this.wxConfig(wx, wxShare);
    },
}