$(function () {
    leftAjax.getLeftMenu(pid);
})


var vueLeftMenu = new Vue({
    el: '.toggle',
    data: {
        leftMenu: '',
    },
    methods: {
        getLeftMenu: function () {
            leftAjax.getLeftMenu();
        },
        toggle: function (event) {
            if (event) {
                $(event.target).parent().parent().find("a").removeClass("dd_actived");
                $(event.target).addClass("dd_actived");
                parent.add_menu('' + $(event.target).html() + '', '' + $(event.target).attr("data") + '');
            }
        },
    }
});

var leftAjax = {
    getLeftMenu: function (pid) {
        if (!paraVerify.verifyNum(pid)) {
            return false;
        }

        $.post("data/menus.ashx", { action: "leftmenu", pid: pid, rand: Math.random() }, function (data) {
            if (data.Result == 1) {
                vueLeftMenu.$data.leftMenu = data.Data;
            }
            else if (data.Message == "未登陆") {
                window.top.location.href = 'index.aspx ';
            }
            else {
                dialog.ShowTempMessage(data.Message, 3000);
            }
        }, 'json');
    }
}