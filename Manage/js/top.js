$(function () {
    vueTop.getTopMenu();

    //定时执行
    topInterval = setInterval(function () {
        var $this=$(".top_center a:eq(0)");
        if ($this) {
            //添加一个对象 做点击
            $this.append("<p></p>").find("p").click();
            $this.find("p").remove();

            //window.parent.leftFrame.location.href = $this.attr("href");
            window.clearInterval(topInterval);
        }
    }, 500);
})

var vueTop = new Vue({
    el: '.top_center',
    data: {
        topMenu: '',
    },
    methods: {
        getTopMenu: function () {
            topAjax.getTopMenu();
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
    }
}