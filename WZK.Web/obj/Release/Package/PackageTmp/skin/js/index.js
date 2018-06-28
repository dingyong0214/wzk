$(document).ready(function() {
    // 轮播图效果
    $(window).resize(function(event) {
        var ulWidth = $(".banner ul li").length * $('.banner_in').width()
        var liWidth = $('.banner_in').width()
        var arrLeft = $(".banner .arr").width() / 2
        $(".banner .arr").css('margin-left', -arrLeft);
        $(".banner ul").css('width', ulWidth);
        $(".banner ul li").css('width', liWidth);
       

       
        //var banner_widht = $(".banner ul li a img").width()
    });
    // 全部闲置物品分类图标切换效果
    var img_icon_1 = ['/skin/images/q01.png', '/skin/images/q02.png', '/skin/images/q03.png', '/skin/images/q04.png', '/skin/images/q05.png', '/skin/images/q06.png', '/skin/images/q07.png', '/skin/images/q08.png']
    var img_icon_2 = ['/skin/images/q1.png', '/skin/images/q2.png', '/skin/images/q3.png', '/skin/images/q4.png', '/skin/images/q5.png', '/skin/images/q6.png', '/skin/images/q7.png', '/skin/images/q8.png']



    $(".banner_content_l .banner_content_l_nav li:last()").css('border-bottom', '0');
    $(".banner_content_l .banner_content_l_nav li").mouseenter(function() {

        var that = $(this)

        that.children('a').children('img').attr('src', img_icon_1[that.index()]);
        that.children('a').css({
            'color': '#57C957',
            'text-decoration': 'underline'
        });
        $(".banner_content_l_left").show();
        $(".banner_content_l_left li").eq($(this).index()).show().siblings('li').hide();

    });
    $(".banner_content_l .banner_content_l_nav li").mouseleave(function(event) {

        var that = $(this);

        that.children('a').children('img').attr('src', img_icon_2[that.index()]);
        that.children('a').css({
            'color': '#575757',
            'text-decoration': 'none'
        });
    });
    //默认加载物品分类图标,added by jimmy,2015-5-14
    $(".banner_content_l .banner_content_l_nav li").each(function (event) {

        var that = $(this);
        that.children('a').children('img').attr('src', img_icon_2[that.index()]);
        that.children('a').css({
            'color': '#575757',
            'text-decoration': 'none'
        });
    });
    $(".banner_content_l").mouseleave(function(event) {
        $(".banner_content_l_left").hide();
    });

    // 内容模块颜色切换效果
    var more = ['#0098FF', '#E66084', '#7663A6', '#43B543', '#F2C51E', '#EE7500']
    $(".content_follow2_bottom_qh .more").each(function(index, el) {
        $(this).css({
            'border-color': more[index],
            'color': more[index]
        });
        $(this).hover(function() {
            $(this).css({
                'background-color': more[index],
                'color': '#fff'
            });
        }, function() {
            $(this).css({
                'background-color': '#fff',
                'color': more[index]
            });
        });
    });
    $(".content_follow2_bottom_qh .title span").each(function(index, el) {
        $(this).css({
            'border-color': more[index]
        });

    });
    $(".banner ul li:first").clone().appendTo($(".banner ul"));

    var banner_length = $(".banner ul li").length - 1;
    for (var i = 0; i < banner_length; i++) {
        $(".banner .arr").append('<li></li>')
    };
    $(".banner .arr li").eq(0).addClass('current2');
    var ulWidth = $(".banner ul li").length * $(window).width()
    var liWidth = $('.banner_in').width()
    var arrLeft = $(".banner .arr").width() / 2
    $(".banner .arr").css('margin-left', -arrLeft);
    $(".banner ul").css('width', ulWidth);
    $(".banner ul").css('left', 0);
    $(".banner ul li").css('width', liWidth);
    var dd = $(".banner ul li").length - 2
    var ee = $(".banner ul li").length - 1
    var key = 0
    var speed = 400;
    var speed2 = 5000;
    var timer = null
    $(".banner  .left").click(function(event) {
        key--
        if (key < 0) {
            key = dd
            $(".banner ul").css('left', -ee * $('.banner_in').width());
        }
        $(".banner ul").stop().animate({
            "left": -key * $('.banner_in').width()
        }, speed)
        var cc = key
        if (cc > 0) {
            cc = dd
        };
        $(".banner .arr li").eq(cc).addClass('current2').siblings('li').removeClass('current2')
    });
    $(".banner  .right").click(function(event) {
        fun()
    });
    timer = setInterval(fun, speed2)

    function fun() {
        key++
        if (key > $(".banner ul li").length - 1) {
            key = 1

            $(".banner ul").css('left', 0);
        }
        $(".banner ul").stop().animate({
            "left": -key * $('.banner_in').width()
        }, speed)
        var cc = key
        if (cc > dd) {
            cc = 0
        };
        $(".banner .arr li").eq(cc).addClass('current2').siblings('li').removeClass('current2')
    }
    $(".banner .arr li").click(function(event) {
        var bb = -$(this).index() * $('.banner_in').width()
        $(this).addClass('current2').siblings('li').removeClass('current2')
        $(".banner ul").stop().animate({
            "left": bb
        }, speed)
    });
    $(".banner").hover(function() {
        clearInterval(timer);
        timer = null;
    }, function() {
        clearInterval(timer);
        timer = setInterval(fun, speed2);
    });
    // banner图左右滑动
    $(".banner").hover(function() {
        $(".banner .right, .banner .left").show()
    }, function() {
        $(".banner .right, .banner .left").hide()

    });
    //搜索关键词效果
    $("#searchKeyword a").click(function(event) {
        $(this).addClass('current').siblings().removeClass('current')
    });
    // 导航栏切换效果
    $(".nav_in dd").click(function(event) {
        $(this).addClass('current1').siblings('dd').removeClass('current1')
    });
    //搜索切换
    $(".search_in_top a").click(function(event) {
        $(this).addClass('current').siblings('a').removeClass('current')
    });
    // 收藏按钮下拉效果
    $(".content_follow2_bottom_right li").hover(function() {
        $(this).children('.love').stop().animate({
            'top': 130
        }, 300);
    }, function() {
        $(this).children('.love').stop().animate({
            'top': -50
        }, 300);
    });
    //$(".content_follow2_bottom_right li .love").click(function(event) {
    //    if ($(this).children('span').css('background-position') == '-289px -57px') {
    //        $(this).children('span').css('background-position', '-305px -57px')
    //    } else {
    //        $(this).children('span').css('background-position', '-289px -57px')
    //    }

    //});
    $(".nav_in dd").eq(0).css('margin-left', 200 + 'px');



});