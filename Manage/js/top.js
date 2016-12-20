$(function () {
    vueTop.getTopMenu();
    vueTop.getUserNameList();

    //定时执行
    topInterval = setInterval(function () {
        var $this = $(".top_center a:eq(0)");
        if ($this) {
            //添加一个对象 做点击
            $this.append("<p></p>").find("p").click();
            $this.find("p").remove();

            //window.parent.leftFrame.location.href = $this.attr("href");
            window.clearInterval(topInterval);
        }
    }, 200);
})

var vueTop = new Vue({
    el: '.top_bg',
    data: {
        topMenu: '',
        accountList: '',
        selected: $(".top_right a:eq(0)").attr("data-userid"),
    },
    methods: {
        getTopMenu: function () {
            topAjax.getTopMenu();
        },
        getUserNameList: function () {
            topAjax.getUserNameList();
        },
        changeUserDroit: function () {
            topAjax.changeUserDroit();
        },
        top_center: function (event) {
            if (event) {
                $(event.target).addClass("top_center_actived").siblings().removeClass("top_center_actived");
            }
        },
    }
});

var topAjax = {
    getTopMenu: function () {
        $.post("data/menus.ashx", { action: "top", rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                vueTop.$data.topMenu = data.Data;
            }
            else if (data.Message == "未登陆") {
                window.top.location.href = 'index.aspx ';
            }
            else {
                dialog.ShowTempMessage(data.Message, 3000);
            }
        }, 'json');
    },
    getUserNameList: function () {
        $.post("data/menus.ashx", { action: "getUserNameList", rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                vueTop.$data.accountList = data.Data;
            }
            else if (data.Message == "未登陆") {
                window.top.location.href = 'index.aspx ';
            }
            else {
                dialog.ShowTempMessage(data.Message, 3000);
            }
        }, 'json');
    },
    changeUserDroit: function () {
        var userid = $("#userList").val();
        var currUserid = $(".top_right a:eq(0)").attr("data-userid");
        if (!paraVerify.verifyNum(userid) || !paraVerify.verifyNum(currUserid)) {
            dialog.ShowTempMessage("参数错误");
            return false;
        }
        $.post("data/menus.ashx", { action: "changeUserDroit", userid: userid, currUserid: currUserid, rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                top.location.reload();
            }
            else if (data.Message == "未登陆") {
                window.top.location.href = 'index.aspx ';
            }
            else {
                dialog.ShowTempMessage(data.Message, 3000);
            }
        }, 'json');
    },
}