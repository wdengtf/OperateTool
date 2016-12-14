/**
 * Created by 1314 on 14-11-4.
 */

$(function(){
    cover();
    $(window).resize(function(){ //浏览器窗口变化
        cover();
    });
});
function cover(){
    var win_width = $(window).width();
    var win_height = $(window).height();
    $("#bigpic").attr({width:win_width, height:win_height});
}

$(document).ready(function()    {
    $(".login_user").focusin(function() {
        if($(this).val() =="请输入用户名"){
            $(this).val("");
        }
    });
    $(".login_user").focusout(function() {
        if($(this).val() ==""){
            $(this).val("请输入用户名");
        }
    });

    $(".login_pwd").focusin(function() {
        if($(this).val() =="请输入密码"){
            $(this).val("");
        }
    });
    $(".login_pwd").focusout(function() {
        if($(this).val() ==""){
            $(this).val("请输入密码");
        }
    });
});

