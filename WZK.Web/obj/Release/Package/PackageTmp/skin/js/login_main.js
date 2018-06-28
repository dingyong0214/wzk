/**
 *
 * @authors Your Name (you@example.org)
 * @date    2015-04-28 08:18:54
 * @version $Id$
 */

$(function () {
    // 登录注册页面开始

    /**
     * [is_that_day 判断是否指定日期]
     * @param {[string]} riqi      [设置日期，格式"2015-6-15"]
     */
    var is_that_day = function (riqi) {
        var arr = riqi.split("-");
        var date1 = new Date(arr[0],parseInt(arr[1])-1,arr[2],'','',''),
            today1 = new Date(),
            year1 = today1.getFullYear(),
            month1 = today1.getMonth(),
            day1 = today1.getDate(),
            date2 = new Date(year1,month1,day1,'','','');
        if (date1 - date2 == 0) {
            return true;
        }
        return false;
    };
    var jieri = ['2015-6-19', '2015-6-20', '2015-6-21', '2015-6-22'],
        bg_src = ["../skin/images/20150620.jpg", "../skin/images/20150620.jpg", "../skin/images/20150621.jpg", "../skin/images/20150620.jpg"];
    for (var i = 0, len = jieri.length; i < len; i++) {
        if (is_that_day(jieri[i])) {
            $(".l_bg img").attr('src', bg_src[i]);
            break;
        }
    };
    if (i >= len) {
        $(".l_bg img").attr('src', "../skin/images/bg1.jpg");
    };
    //图片背景
    function bg_img() {
        var height = $(window).height() - 35;//减去顶部部分
        var width = $(window).width();
        if (height < 600) height = 600;
        $(".l_bg,.l_bg img,.l_r_main").css({
            "height": height
        });
        var img_width = $(".l_bg img").css("width");
        var left_img = (width - parseInt(img_width)) / 2;
        $(".l_bg img").css({
            "left": left_img
        });
    }
    bg_img();
    $(window).load(function () {
        bg_img();
    });
    $(window).resize(function (event) {
        bg_img();
    });

    //登录注册选项卡
    $(".l_r_box ul a").click(function (event) {
        var i = $(this).parent().index();
        $(this).parent().addClass('selected').siblings().removeClass('selected');
        $(".l_tab").eq(i).addClass('selected').siblings().removeClass('selected');
        if (i == 0) {
            $(".l_logo span").text("用户登录");
            document.getElementById('txtNumber').focus();//added by jimmy,2015-5-11
        } else {
            $(".l_logo span").text("用户注册");
            document.getElementById('txtReNumber').focus();//added by jimmy,2015-5-11
        }
        return false;
    });
    // 验证码倒计时
    //var code_time = 60,
	//	yanzhengma_t;
    //$(".login_code").click(function (event) {
    //    var $this = $(this);
    //    if (code_time == 60) {
    //        code_time--;
    //        $this.text(code_time + "秒");
    //        yanzhengma_t = setInterval(function () {
    //            code_time--;
    //            $this.text(code_time + "秒");
    //            if (code_time <= 0) {
    //                $this.text("发送验证码");
    //                code_time = 60;
    //                clearInterval(yanzhengma_t);
    //            };
    //        }, 1000);

    //        //这里是处理主函数
    //        //alert(123);
    //    }
    //    return false;
    //});
    //登录注册 input placeholder效果
    $.each($(".l_tab input"), function (index, val) {
        var txt = $(this).attr("placeholder");
        $(this).val(txt);
        if ($(this).hasClass('password')) {
            $(this).hide().siblings(".password1").show();
        };
    });
    $(".l_tab input").focus(function (event) {
        var $this = $(this),
			txt = $this.val(),
			text = $this.attr("placeholder");
        if (txt == text)
            $this.val("");
        $this.addClass('input_focus');
        if ($(this).hasClass('password1')) {
            $(this).hide().siblings(".password").show().trigger('focus');
        };
    });
    $(".l_tab input").blur(function (event) {
        var $this = $(this),
			txt = $this.val(),
			text = $this.attr("placeholder");
        if (txt == "" || txt == text) {
            $this.removeClass('input_focus').val(text);
            if ($(this).hasClass('password')) {
                $(this).hide().siblings(".password1").show();
            };
        };
    });
    // 登录注册页面结束

    // 重置密码开始
    // 验证码倒计时
    //var code_time = 60,
	//	yanzhengma_t;
    //$(".yanzhengma a,.phone_number .number .number_xz button").click(function (event) {
    //    var $this = $(this);
    //    if (code_time == 60) {
    //        code_time--;
    //        $this.text(code_time + "秒");
    //        yanzhengma_t = setInterval(function () {
    //            code_time--;
    //            $this.text(code_time + "秒");
    //            if (code_time <= 0) {
    //                $this.text("点击获取短信");
    //                code_time = 60;
    //                clearInterval(yanzhengma_t);
    //            };
    //        }, 1000);

    //        //这里是处理主函数
    //        //alert(123);
    //    }
    //    return false;
    //});
    //$(".yanzhengma a,.phone_number .number .number_xz button").click(function (event) {
    //    var $this = $(this);
    //    if (code_time == 60) {
    //        code_time--;
    //        $this.text(code_time + "秒");
    //        yanzhengma_t = setInterval(function () {
    //            code_time--;
    //            $this.text(code_time + "秒");
    //            if (code_time <= 0) {
    //                $this.text("点击获取短信");
    //                code_time = 60;
    //                clearInterval(yanzhengma_t);
    //            };
    //        }, 1000);

    //        //这里是处理主函数
    //        //alert(123);
    //    }
    //    return false;
    //});
    //重置密码 input placeholder效果
    $.each($(".reset_list input"), function (index, val) {
        var txt = $(this).attr("placeholder");
        $(this).val(txt);
        if ($(this).hasClass('password')) {
            $(this).hide().siblings(".password1").show();
        };
    });
    $(".reset_list input").focus(function (event) {
        var $this = $(this),
			txt = $this.val(),
			text = $this.attr("placeholder");
        if (txt == text)
            $this.val("");
        $this.addClass('input_focus');
        if ($(this).hasClass('password1')) {
            $(this).hide().siblings(".password").show().trigger('focus');
        };
    });
    $(".reset_list input").blur(function (event) {
        var $this = $(this),
			txt = $this.val(),
			text = $this.attr("placeholder");
        if (txt == "" || txt == text) {
            $this.removeClass('input_focus').val(text);
            if ($(this).hasClass('password')) {
                $(this).hide().siblings(".password1").show();
            };
        };
    });
    //重置密码结束

    //用户协议弹出
    $(".agreement_btn").click(function (event) {
        var h = $("body").height(),
			w = $("body").width(),
			w_h = $(window).height(),
			s_top = $(document).scrollTop();
        m_h = $(".agreement_box").height();
        $(".agreement_box").css({
            "top": (w_h - m_h) / 2 + s_top + "px",
            "display": "block"
        });
        $(".agreement_layer").css({
            "height": h,
            "width": w,
            "display": "block"
        });
    });
    $(".agreement_top a,.agreement_layer").click(function (event) {
        $(".agreement_box").hide();
        $(".agreement_layer").hide();
        return false;
    });

    var $url = location.href;
    if ($url.indexOf("#register") >= 0) {
        $(".l_r_box ul a").eq(1).trigger('click');
    }
});