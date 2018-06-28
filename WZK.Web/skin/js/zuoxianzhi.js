$(document).ready(function () {
    // 全部闲置物品分类图标切换效果
    var img_icon_1 = ['/skin/images/q01.png', '/skin/images/q02.png', '/skin/images/q03.png', '/skin/images/q04.png', '/skin/images/q05.png', '/skin/images/q06.png', '/skin/images/q07.png', '/skin/images/q08.png']
    var img_icon_2 = ['/skin/images/q1.png', '/skin/images/q2.png', '/skin/images/q3.png', '/skin/images/q4.png', '/skin/images/q5.png', '/skin/images/q6.png', '/skin/images/q7.png', '/skin/images/q8.png']
    $(".banner_content_l .banner_content_l_nav li:last()").css('border-bottom', '0');
    $(".banner_content_l .banner_content_l_nav li").mouseenter(function () {
        var that = $(this)
        that.children('a').children('img').attr('src', img_icon_1[that.index()]);
        that.children('a').css({
            'color': '#57C957',
            'text-decoration': 'underline'
        });
        $(".banner_content_l_left").show();
        $(".banner_content_l_left li").eq($(this).index()).show().siblings('li').hide();
    });
    $(".banner_content_l .banner_content_l_nav li").mouseleave(function (event) {
        var that = $(this);
        that.children('a').children('img').attr('src', img_icon_2[that.index()]);
        that.children('a').css({
            'color': '#575757',
            'text-decoration': 'none'
        });
    });
    $(".banner_content_l").mouseleave(function (event) {
        $(".banner_content_l_left").hide();
    });
    
    $(".nav_in dt").mouseenter(function (event) {
        
        $(".banner_content_l_outher").show();
        $(this).children().children('.arr').addClass('current');
    });
    $(".nav_in dt").mouseleave(function (event) {

        $(".banner_content_l_outher").hide();
        $(".nav_in dt .arr").removeClass("current");
       
    });
    $(".nav_in dd").eq(0).css('margin-left', 200+'px');
   
});