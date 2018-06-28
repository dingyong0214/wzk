$(document).ready(function () {
    // 个人主页导航栏切换效果
    $(".nav_list_1 li").click(function (event) {
        $(this).children('a').addClass('current1');
        $(this).siblings('li').children('a').removeClass('current1');
    });

    // 下一页效果
    $(".lest_last .num").click(function (event) {
        $(this).addClass('current2').siblings('.num').removeClass('current2');
        $(this).children('a').addClass('current3');
        $(this).siblings('.num').children('a').removeClass('current3')
    });

    // 放大镜效果
    function fun_right() {
        if (num == $(".detail_list_left .bot ul li").length - 4) {
            num = $(".detail_list_left .bot ul li").length - 4

        } else {
            num++
            $(".detail_list_left .box ul").animate({
                "left": -num * W
            }, 300)
        }
    }

    function fun_left() {
        if (num == 0) {
            num = 0
        } else {
            num--
            $(".detail_list_left .box ul").animate({
                "left": -num * W
            }, 300)
        }
    }
    var zoom_img = $(".boxin"), //要放大图片的外框
		img = zoom_img.find('img'), //要放大的图片
		zoom_main = $(".pic"), //放大区域
		zoom_big_img = zoom_main.find('div'), //被放大图片的外框
		img_big = zoom_big_img.find('img'), //被放大的图片
		zoom_layer = zoom_img.find('div'), //移动层
		z_w = zoom_img.width(), //宽
		z_h = zoom_img.height(), //高
		zoom_img_left = zoom_img.offset() != undefined ? zoom_img.offset().left : 0,//modifiedy by jimmy,2015-5-14
		zoom_img_top = zoom_img.offset() != undefined ? zoom_img.offset().top : 0;//modifiedy by jimmy,2015-5-14

var beishu = 3; //放大倍数

var l_w = z_w / beishu, //层的宽
    l_h = z_h / beishu, //层的高
    x_min = l_w / 2, //鼠标x轴移动的最小距离
    x_max = z_w - x_min, //鼠标x轴移动的最大距离
    y_min = l_h / 2, //鼠标y轴移动的最小距离
    y_max = z_h - y_min; //鼠标y轴移动的最大距离

zoom_layer.css({ //设置层的大小
    "width": l_w,
    "height": l_h
});
zoom_main.css({ //放大区域和要放大图片的外框大小相等
    "width": z_w,
    "height": z_h
});
zoom_big_img.css({ //被放大图片外框的大小
    "width": z_w * beishu,
    "height": z_h * beishu
});

function img_v_c(img) { //图片垂直水平居中
    var img_width = img.width(), //实际的宽度
        img_height = img.height(), //实际的高度
        img_bili = img_width / img_height, //图片的比例
        w = img.parent().width(),
        h = img.parent().height(),
        bili = w / h; //外框宽高比例
    if (img_bili > bili) //图片宽高比例比父级元素宽高比例大，则图片由宽度限制
    {
        if (img_width > w) {
            img.css({
                "width": w,
                "height": "auto"
            });
        }
    } else { //图片宽高比例比父级元素宽高比例小，则图片由高度限制
        if (img_height > h) {
            img.css({
                "width": "auto",
                "height": h
            });
        }
    }
    var now_w = img.width(),
        now_h = img.height();
    img.css({
        "top": "50%",
        "left": "50%",
        "margin-top": -now_h / 2 + "px",
        "margin-left": -now_w / 2 + "px"
    });
}

function reset_zoom(img, img_big) { //重置放大区域
    img_v_c(img);
    img_big.css({ //被放大图片的大小
        "width": img.width() * beishu,
        "height": img.height() * beishu
    });
    img_v_c(img_big);
}
img.load(function () {//大图垂直水平居中
    reset_zoom(img, img_big);
});

zoom_img.bind('mouseover', function (event) {
    zoom_layer.show();
    zoom_main.show();
});
zoom_img.bind('mouseleave', function (event) {
    zoom_layer.hide();
    zoom_main.hide();
});
zoom_img.bind('mousemove', function (event) {
    event.preventDefault();
    var x = (event.pageX || event.offsetX) - zoom_img_left,
        y = (event.pageY || event.offsetY) - zoom_img_top;
    var left, top;
    if (x >= x_min && x <= x_max) {
        left = x - x_min;
    } else if (x > x_max) {
        left = z_w - l_w;
    } else {
        left = 0;
    }
    if (y >= y_min && y <= y_max) {
        top = y - y_min;
    } else if (y > y_max) {
        top = z_h - l_h;
    } else {
        top = 0;
    }
    zoom_layer.css({
        "top": top,
        "left": left
    });
    zoom_big_img.css({
        "top": -top * beishu,
        "left": -left * beishu
    });
});
$(".botin img").load(function () {//所有列表图片垂直水平居中
    img_v_c($(this));
});
window.onload = function () {
    reset_zoom(img, img_big);//大图垂直水平居中
    $.each($(".botin img"), function (index, val) {//所有列表图片垂直水平居中
        img_v_c($(this));
    });
}
var num = 0
var W = $(".detail_list_left .box ul li").outerWidth(true);
$(".detail_list_left .bot ul li").mouseover(function (event) {
    $(this).addClass('current').siblings('li').removeClass('current')
    var src = $(this).children('img').attr('src');
    zoom_img.find("img").remove();
    zoom_img.append("<img src='" + src + "'/>");
    zoom_big_img.find("img").remove();
    zoom_big_img.append("<img src='" + src + "'/>");
    reset_zoom(zoom_img.find("img"), zoom_big_img.find("img"));
});
var li_length = $(".detail_list_left .bot ul li").length * $(".detail_list_left .bot ul li").outerWidth(true);
$(".detail_list_left .bot ul ").css('width', li_length);
// 右点击
$(".detail_list_left .bot .right").click(function (event) {
    fun_right()
});
// 左点击
$(".detail_list_left .box .left").click(function (event) {
    fun_left()
});
//左键盘事件
$(document).keydown(function (event) {
    if (event.keyCode == 39) {
        fun_right()
    } else if (event.keyCode == 37) {
        fun_left()
    }
});

if ($(".detail_list_left .bot ul li").length < 4) {
    $(".detail_list_left .bot ul").css('position', 'static');
};
    
// 录音播放效果
var yes = false
$(".list_my_left .audio").click(function (event) {
    if (yes == true) {
        $(this).children('audio').get(0).pause()
        $(this).css('background-position', 'bottom');
        yes = false
    } else {
        $(this).children('audio').get(0).play()
        $(this).css('background-position', 'top');
        yes = true
    }
});

// 会员中心-我的发布左侧切换效果
$(".list_my_left ul li").each(function (index, el) {
    $(this).click(function (event) {
        $(this).addClass('current4').siblings('li').removeClass('current4');
        $(this).children('a').addClass('current5')
        $(this).siblings().children('a').removeClass('current5');
        $(".list_my_right .title li").eq(index).addClass('current6').siblings('li').removeClass('current6');
        $(".list_my_right .title li").eq(index).children('a').addClass('current6');
        $(".list_my_right .title li").eq(index).siblings('li').children('a').removeClass('current6');
        $(".list_content").eq(index).show().siblings('.list_content').hide();
    });
});


// 会员中心-我的发布右侧切换效果
$(".list_my_right .title li").each(function (index, el) {
    $(this).click(function (event) {
        $(this).addClass('current6').siblings('li').removeClass('current6');
        $(this).children('a').addClass('current6');
        $(this).siblings('li').children('a').removeClass('current6');
        $(".list_my_left ul li").eq(index).addClass('current4').siblings('li').removeClass('current4');
        $(".list_my_left ul li").eq(index).children('a').addClass('current5')
        $(".list_my_left ul li").eq(index).siblings().children('a').removeClass('current5');
        $(".list_content").eq(index).show().siblings('.list_content').hide();
    });
});

// 会员中心-我的发布右侧切换效果
//$(".list_my_right .title li").each(function(index, el) {
//	$(this).click(function(event) {
//		$(this).addClass('current6').siblings('li').removeClass('current6');
//		$(this).children('a').addClass('current6');
//		$(this).siblings('li').children('a').removeClass('current6');
//		$(".list_my_left ul li").eq(index).addClass('current4').siblings('li').removeClass('current4');
//		$(".list_my_left ul li").eq(index).children('a').addClass('current5')
//		$(".list_my_left ul li").eq(index).siblings().children('a').removeClass('current5');
//		$(".list_content").eq(index).show().siblings('.list_content').hide();
//	});
//});


$(".list_my_right .list_content li").hover(function () {
    $(this).css('border-color', '#57C957');
    $(this).children('.all').show();
}, function () {
    $(this).css('border-color', '#E5E5E5');
    $(this).children('.all').hide();
});
// 会员中心-我的收藏取消收藏效果
$(".list_my_right .list_content1 li .all a").click(function (event) {
    $(this).parent().parent().remove();
});
// 会员中心-我的收藏和会员中心-我的发布头像更换效果
$(".list_my_left .tx").hover(function () {
    $(this).children('a').show();
}, function () {
    $(this).children('a').hide();
});
// 会员中心-消息页面时间切换
$(".list_my_right1  .news dt .time").click(function (event) {
    if ($(this).html() == '时间&nbsp;↑') {
        $(this).html('时间&nbsp;↓')
    } else {
        $(this).html('时间&nbsp;↑')
    }
    return false;
});
// 会员中心-消息收起
$(".list_my_right1 .news dd .theme .nr .sq").click(function (e) {
    $(this).parent('.nr').hide();
    e.stopPropagation()
});
// 会员中心-消息展开
$(".list_my_right1 .news dd .theme").click(function (event) {
    if ($(this).children('.nr').css('display') == 'block') {
        $(this).children('.nr').hide();
    } else {
        $(this).children('.nr').show();
    }
});
// 会员中心-删除消息
$(".list_my_right1 .news dd .operate").click(function (event) {
    //$(this).parent().parent().remove();
});
// 会员中心-去除文字加粗效果
$(".list_my_right1 .news dd .theme_in").click(function (event) {
    $(this).addClass('current8');
});

var aa = true
$('.list_my_right1 .news dt .qx').click(function (event) {

    if (aa == true) {
        $('.list_my_right1 .news dd input').each(function (index, el) {
            $(this).get(0).checked = true;
        });
        $('.list_my_right1 .news dt .qx span').text('取消全选')
        aa = false

    } else {
        $('.list_my_right1 .news dd input').each(function (index, el) {
            $(this).get(0).checked = false;
        });
        $('.list_my_right1 .news dt .qx span').text('全选')
        aa = true
    }
});

//位置选择
$(".fabu_weizhi").click(function (event) {
    $(this).children('.fabu_weizhi_box').addClass('xianshi');
});
$(".fabu_weizhi").mouseleave(function (event) {
    $(this).children('.fabu_weizhi_box').removeClass('xianshi');
});
$(".fabu_weizhi_box").delegate("li", "click", function (event) {
    event.stopPropagation();
    $(this).parent().removeClass('xianshi');
    $(this).parents(".fabu_weizhi").find('span').text($(this).text());

    //当城市为深圳时显示商圈
    //var txt = $(this).parents(".fabu_list_r").find('span').eq(1).text();
    //if (txt == "深圳市" || txt == "深圳") {
    //    $(this).parents(".fabu_list_r").find(".shangquan").show();
    //} else {
    //    $(this).parents(".fabu_list_r").find(".shangquan").hide();
    //}
    //根据选择赋值
    $(this).parent().attr("data-v", $(this).attr("data-v"));
    $(this).parent().attr("data-n", $(this).text());
    AddressList($(this).parent().attr("id"),"","", $(this).attr("data-v"));



    return false;
});



//根据父类加载子类下拉（位置）
var AddressList = function (Type,zID,zName, Pid) {
    var ObjectBox;
    if (Type == "Province") {
        ObjectBox = $("#City");
        $("#CityDiv").hide();
        $("#AreaDiv").hide();
        $("#BusiDiv").hide();
    }
    if (Type == "City") {
        ObjectBox = $("#Area");
        $("#AreaDiv").hide();
        $("#BusiDiv").hide();
    }
    if (Type == "Area") {    
        ObjectBox = $("#Busi");
        $("#BusiDiv").show();
    }
    if (Type == "Province" || Type == "City" || Type == "Area") {
        var MsgStr = new Object();
        MsgStr.AddressType = Type;
        MsgStr.ParentId = parseInt(Pid);
        var PostJson = JSON.parse(JSON.stringify(MsgStr));
        $.post("/Information/GetAddressList", PostJson, function (obj) {
            var AddressList = JSON.parse(obj);
            if (AddressList.length > 0) {
                var Li_html = '';
                for (var i = 0; i < AddressList.length; i++) {
                    Li_html += '<li data-v="' + AddressList[i]["MenuId"] + '">' + AddressList[i]["MenuName"] + '</li>';
                }
                ObjectBox.html(Li_html);
                if (Type == "Province") {
                    if(zID!=""&&zName!="")
                    {
                        $("#City").attr("data-n", zName);
                        $("#City").attr("data-v", zID);
                        $("#City").parents(".fabu_weizhi").find('span').text(zName);
                    }
                    else
                    {
                        $("#City").attr("data-n", $("#City li").eq(0).html());
                        $("#City").attr("data-v", $("#City li").eq(0).attr("data-v"));
                        $("#City").parents(".fabu_weizhi").find('span').text($("#City li").eq(0).html());
                        $("#CityDiv").show();
                        $("#AreaDiv").hide();
                        $("#BusiDiv").hide();
                    } 
                       
                }
                else if (Type == "City") {
                    if (zID != "" && zName != "")
                    {
                        $("#Area").attr("data-n", zName);
                        $("#Area").attr("data-v", zID);
                        $("#Area").parents(".fabu_weizhi").find('span').text(zName);
                    }
                    else {
                        $("#Area").attr("data-n", $("#Area li").eq(0).html());
                        $("#Area").attr("data-v", $("#Area li").eq(0).attr("data-v"));
                        $("#Area").parents(".fabu_weizhi").find('span').text($("#Area li").eq(0).html());
                        $("#AreaDiv").show();
                        $("#BusiDiv").hide();
                    }
                       
                }
                else if (Type == "Area") {
                        
                    if (zID != "" && zName != "")
                    {
                        $("#Busi").attr("data-n", zName);
                        $("#Busi").attr("data-v", zID);
                        $("#Busi").parents(".fabu_weizhi").find('span').text(zName);
                    }
                    else
                    {
                        $("#BusiDiv").show();
                        $("#Busi").attr("data-n", $("#Busi li").eq(0).html());
                        $("#Busi").attr("data-v", $("#Busi li").eq(0).attr("data-v"));
                        $("#Busi").parents(".fabu_weizhi").find('span').text($("#Busi li").eq(0).html());
                    }
       
                }
                else
                {
                }

            }
            else { ObjectBox.html(""); }
        });
    }


    return false;
}
// 地址管理页面鼠标移入效果-背景以及内元素显示
$(".add_management dd").hover(function () {
    $(this).addClass('current10');
    $(this).children('.acquiesce_add_1 ').show();
    $(this).siblings("dd").children('.acquiesce_add_1 ').hide();
}, function () {
    $(this).children('.acquiesce_add_1 ').hide();
    $(this).removeClass('current10');

});
// 地址管理删除地址
$(".add_management .close").click(function (event) {
    var $this = $(this);
    var MsgStr = new Object();
    MsgStr.Type = 0;
    MsgStr.AddressId = parseInt($(this).parent().attr("data-v"));
    layer.confirm('确定是否删除？', {
        btn: ['是', '否'], //按钮
        shade: false //不显示遮罩
    }, function () {
        var PostJson = JSON.parse(JSON.stringify(MsgStr));
        $.post("/Member/PostAddressEdit", PostJson, function (obj) {
            if (obj == "ok") {
                $this.parent('dd').remove();
                layer.msg("删除成功！", { time: 2000 });
            }
            else {
                alert(obj);
                layer.msg("删除失败！", { time: 2000 });
            }
        });
    }, function () {
    });
});
// 地址管理设置默认地址
$(".acquiesce_add_1").click(function (event) {
    var $this = $(this);
    var MsgStr = new Object();
    MsgStr.Type = 1;
    MsgStr.AddressId = parseInt($(this).parent().attr("data-v"));
    var PostJson = JSON.parse(JSON.stringify(MsgStr));
    $.post("/Member/PostAddressEdit", PostJson, function (obj) {
        if (obj == "ok") {
            $this.text('默认地址');
            $this.parent('dd').siblings('dd').children('.acquiesce_add_1').text('设为默认');
            $this.parent().addClass('current9');
            $this.parent().siblings('dd').removeClass('current9');
            $this.siblings('.yes').show();
            $this.parent('dd').siblings('dd').children('.yes').hide();
        }
        else { alert(obj); }
    });
});
// 地址管理设置默认地址
$(".add_management dt").hover(function () {
    $(this).addClass('current11');
}, function () {
    $(this).removeClass('current11');
});
//新增地址弹出
$(".add_management dt").click(function (event) {
    //重置弹出层数据
    $("#AddressId").attr("data-v", "0");
    $("#AddressId").attr("data-is", "False");
    $("#A_Name").val("");
    $("#A_Address").val("");
    $("#A_Mobile").val("");
    $("#A_Section").val("");
    $("#A_Phone").val("");
    $("#Province").attr("data-v", "0");
    $("#Province").attr("data-n", "");
    $("#Province").parents(".fabu_weizhi").find('span').text("--省--");
    $("#City").attr("data-v", "0");
    $("#City").attr("data-n", "");
    $("#City").parents(".fabu_weizhi").find('span').text("--市--");
    $("#Area").attr("data-v", "0");
    $("#Area").attr("data-n", "");
    $("#Area").parents(".fabu_weizhi").find('span').text("--区--");
    $("#Busi").attr("data-v", "0");
    $("#Busi").attr("data-n", "");
    $("#Busi").parents(".fabu_weizhi").find('span').text("--商圈--");


    OpenWeiZhi();//打开弹出层
});
//修改地址
$(".add_management").delegate("dd .alter", "click", function (event) {
    var $this = $(this).parent();

    if ($this.find("h3 span").attr("data-v") != "0")
    {
        AddressList("Province", $this.find("h3 i").attr("data-v"), $this.find("h3 i").text(), $this.find("h3 span").attr("data-v"));
        $("#CityDiv").show();
         
    }
            
       
    if ($this.find("h3 i").attr("data-v") != "0")
    {
        AddressList("City", $this.find(".other span").attr("data-v"), $this.find(".other span").text(), $this.find("h3 i").attr("data-v"));
        $("#AreaDiv").show();
                
    }
    else
    {
        $("#AreaDiv").hide();

    }
      
        
    if ($this.find(".other span").attr("data-v") != "0")
    {
        AddressList("Area", $this.attr("data-b"), $this.attr("data-bn"), $this.find(".other span").attr("data-v"));
        $("#BusiDiv").show();
    }
    else
    {
        $("#AreaDiv").hide();

    }
        


    //绑定省、市、区、商圈
    //if ($this.find("h3 span").attr("data-v") != "0") {
    //    AddressList("Province", $this.find("h3 i").attr("data-v"), $this.find("h3 i").text(), $this.find("h3 span").attr("data-v"));
    //}

    //if ($this.find("h3 i").attr("data-v") != "0") {
    //    AddressList("City", $this.find(".other span").attr("data-v"), $this.find(".other span").text(), $this.find("h3 i").attr("data-v"));
    //}
    //if ($this.find(".other span").attr("data-v") != "0") {
    //    AddressList("Area", $this.attr("data-b"), $this.attr("data-bn"), $this.find(".other span").attr("data-v"));
    //}
    //else {
    //    $("#AreaDiv").hide();

    //}
    //if ($this.attr("data-b") == "0") {
    //    $("#BusiDiv").hide();
    //}

    $("#AddressId").attr("data-v", $this.attr("data-v"));
    $("#AddressId").attr("data-is", $this.attr("data-is"));
    $("#A_Name").val($this.find("h3 s").text());
    $("#A_Address").val($this.find(".other i").text());
    $("#A_Mobile").val($this.find(".other s").text());
    $("#A_Section").val($this.find(".other i").attr("data-v"));
    $("#A_Phone").val($this.find(".other s").attr("data-v"));
    $("#Province").attr("data-v", $this.find("h3 span").attr("data-v"));
    $("#Province").attr("data-n", $this.find("h3 span").text());
    $("#Province").parents(".fabu_weizhi").find('span').text($this.find("h3 span").text());
    //var CityName = $this.find("h3 i").text();
    //$("#City").attr("data-n", CityName);
    //$("#City").attr("data-v", $this.find("h3 i").attr("data-v"));
    //$("#City").parents(".fabu_weizhi").find('span').text(CityName);
    ////if (CityName == "深圳市" || CityName == "深圳") {
    ////    $("#Busi").parents(".shangquan").show();
    ////} else {
    ////    $("#Busi").parents(".shangquan").hide();
    ////}
    //$("#Area").attr("data-v", $this.find(".other span").attr("data-v"));
    //$("#Area").attr("data-n", $this.find(".other span").text());
    //$("#Area").parents(".fabu_weizhi").find('span').text($this.find(".other span").text());
    //$("#Busi").attr("data-v", $this.attr("data-b"));
    //$("#Busi").attr("data-n", $this.attr("data-bn"));
    //$("#Busi").parents(".fabu_weizhi").find('span').text($this.attr("data-bn"));

    OpenWeiZhi();
        
});
//打开地址弹出层
var OpenWeiZhi = function () {
    var h = $("body").height(),
        w = $("body").width(),
        w_h = $(window).height(),
        s_top = $(document).scrollTop();
    m_h = $(".add_weizhi_b").height();
    $(".add_weizhi_b").css({
        "top": (w_h - m_h) / 2 + s_top + "px",
        "display": "block"
    });
    $(".fabu_layer").css({
        "height": h,
        "width": w,
        "display": "block"
    });
}
//关闭地址弹出层
$(".add_weizhi_top a,.fabu_layer,.fabu_btn_cancel").click(function (event) {
    $(".add_weizhi_b").hide();
    $(".fabu_layer").hide();
    return false;
});
$(".search_nav_in ul li").click(function (event) {
    $(this).children('a').addClass('current12')
    $(this).siblings('li').children('a').removeClass('current12')
});






$(".list_my_right .list_content li").each(function (index, el) {
    var bb = $(this).children('h3').text();
    var aa = $(this).children('h3').text().length;
    if (aa >= 9) {
        var num1 = $(this).children('h3').text().substr(0, 30);
        $(this).children('h3').text(num1 + '...');

    };
});
//详情页收藏点击效果
//$("#coll_Id").click(function(event) {
//    if ($(this).children('span').attr('class')=='l2') 
//       {
//           $(this).children('span').removeClass('l2').addClass('l2_1');
//       }
//      else{
//           $(this).children('span').removeClass('l2_1').addClass('l2');

//      } 
//}); 




});

//判断密码的强弱
//判断输入密码的类型  
function CharMode(iN) {
    if (iN >= 48 && iN <= 57) //数字  
        return 1;
    if (iN >= 65 && iN <= 90) //大写  
        return 2;
    if (iN >= 97 && iN <= 122) //小写  
        return 4;
    else
        return 8;
}
//bitTotal函数  
//计算密码模式  
function bitTotal(num) {
    modes = 0;
    for (i = 0; i < 4; i++) {
        if (num & 1) modes++;
        num >>>= 1;
    }
    return modes;
}
//返回强度级别  
function checkStrong(sPW) {
    if (sPW.length <= 4)
        return 0; //密码太短  
    Modes = 0;
    for (i = 0; i < sPW.length; i++) {
        //密码模式  
        Modes |= CharMode(sPW.charCodeAt(i));
    }
    return bitTotal(Modes);
}

//显示颜色  
function pwStrength(pwd) {
    O_color = "#FFCC99";
    L_color = "#FF6600";
    M_color = "#FF6600";
    H_color = "#FF6600";
    if (pwd == null || pwd == '') {
        Lcolor = Mcolor = Hcolor = O_color;
    } else {
        S_level = checkStrong(pwd);
        switch (S_level) {
            case 0:
                Lcolor = Mcolor = Hcolor = O_color;
            case 1:
                Lcolor = L_color;
                Mcolor = Hcolor = O_color;
                break;
            case 2:
                Lcolor = Mcolor = M_color;
                Hcolor = O_color;
                break;
            default:
                Lcolor = Mcolor = Hcolor = H_color;
        }
    }
    document.getElementById("strength_L").style.background = Lcolor;
    document.getElementById("strength_M").style.background = Mcolor;
    document.getElementById("strength_H").style.background = Hcolor;
    return;
}