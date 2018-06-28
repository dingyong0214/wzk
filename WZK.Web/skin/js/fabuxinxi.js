/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-04-30 09:49:44
 * @version $Id$
 */
$(function() {
	// //类型、区域、分类
	// $('.buy_right a').click(function(event) {
	// 	$(this).parent().addClass('selected').siblings().removeClass('selected');
	// 	return false;
	// });
	

	//与上一元素替换位置
    $(".hw").delegate('li .b .left','click',function (event) {
		var at = $(this).parent().parent().prev("li").children('img').attr('src');
		var this_at = $(this).parent().siblings('img').attr('src');
		$(this).parent().siblings('img').attr('src', at);
		$(this).parent().parent().prev("li").children('img').attr('src', this_at)
	});
	//与下一元素替换位置
    $(".hw").delegate('li .b .right', 'click', function (event) {
		var at = $(this).parent().parent().next("li").children('img').attr('src');
		var this_at = $(this).parent().siblings('img').attr('src');
		$(this).parent().siblings('img').attr('src', at);
		$(this).parent().parent().next("li").children('img').attr('src', this_at)
	});
	//删除
    $(".hw").delegate('li .b span', 'click', function (event) {
		$(this).parent().parent().remove();
	});


    //物品分类
	$(".fabu_fenlei").click(function(event) {
		$(this).children('.fabu_fenlei_box').addClass('xianshi');
	});
	$(".fabu_fenlei").mouseleave(function(event) {
		$(this).children('.fabu_fenlei_box').removeClass('xianshi');
	});
	$(".fabu_fenlei_li").hover(function(event) {
		$(this).addClass('selected');
	}, function(event) {
		$(this).removeClass('selected');
	});
	$(".fabu_fenlei_li a").click(function(event) {
		$(".fabu_fenlei span").text($(this).text());
		$(this).parents(".fabu_fenlei_li").removeClass('selected');
	    //根据选择赋值
		$(".fabu_fenlei_box").attr("data-v1", $(this).parents(".fabu_fenlei_li").attr("data-v"));
		$(".fabu_fenlei_box").attr("data-v2", $(this).attr("data-v"));
		return false;
	});

	//其他地址
	$(".fabu_dizhi_b").delegate('.fabu_dizhi', 'mouseover', function(event) {
		$(this).find('div').show();
	});
	$(".fabu_dizhi_b").delegate('.fabu_dizhi', 'mouseout', function(event) {
		$(this).find('div').hide();
	});
	
	$(".fabu_dizhi_o .dizhi_1").click(function(event) {
		$(".fabu_dizhi_b").css({
		    "max-height": "none"
	    });
		$(this).hide().siblings().show();
		return false;
	});
	//设置默认
	$(".fabu_dizhi_b").delegate('.fabu_shezhi', 'click', function(event) {
		var p=$(this).parent().parent();
		p.find('input').trigger('click');
		p.addClass('selected').siblings().removeClass('selected');
		//p.prependTo(p.parent());
		//return false;
	});
	//修改地址
	$(".fabu_dizhi_b").delegate('.fabu_xiugai', 'click', function(event) {
		//var p=$(this).parent().parent();
		//p.find('input').trigger('click');
		//p.addClass('selected').siblings().removeClass('selected');
		//p.prependTo(p.parent());
		//return false;
	});
	//新增地址弹出
	$(".dizhi_2").click(function(event) {
		var h=$("body").height(),
			w=$("body").width(),
			w_h=$(window).height(),
			s_top=$(document).scrollTop();
			m_h=$(".add_weizhi_b").height();
			$(".add_weizhi_b").css({
				"top": (w_h-m_h)/2+s_top+"px",
				"display":"block"
			});
			$(".fabu_layer").css({
				"height": h,
				"width": w,
				"display":"block"
			});
	});
	$(".add_weizhi_top a,.fabu_layer,.fabu_btn_cancel").click(function(event) {
		$(".add_weizhi_b").hide();
		$(".fabu_layer").hide();
		return false;
	});

	//字数统计
	$(".input_main").keyup(function (event) {
        var txt = $(this).val(), len = txt.length;
        if (len > 600) {
            $(this).val(txt.substring(0, 600));
            $("#has_word").text(600);
            $("#leave_word").text(0);
        } else {
            var s = 600 - len;
            $("#has_word").text(len);
            $("#leave_word").text(s);
        }
    });
	var t;
	t = setInterval(function() {
		var li = $(".fabu_step");
		if ($(".hw li").length > 1) {
				li.eq(0).addClass('fabu_last prev').removeClass('present');
				li.eq(1).addClass('prev').removeClass('present');
				li.eq(2).addClass('present');
				return;
			}
		if ($(".fabu_title input").val().length > 0) {
			
			li.eq(0).addClass('prev').removeClass('present fabu_last');
			li.eq(1).addClass('present').removeClass('prev');
			li.eq(2).removeClass('present');
			return;
		}
		li.eq(0).addClass('present').removeClass('fabu_last prev');
		li.eq(1).removeClass('present prev');
		li.eq(2).removeClass('present');

	}, 1000);

});



//字数统计
	function wordCount() {
		var txt = $(".input_main").val(),
			len = txt.length;
		var s = 600 - len;
		$("#has_word").text(len);
		$("#leave_word").text(s);
		if (len > 600) {
			$(".input_main").val(txt.substring(0, 600));
			$("#has_word").text(600);
			$("#leave_word").text(0);
		} else {
			var s = 600 - len;
			$("#has_word").text(len);
			$("#leave_word").text(s);
		}
	}