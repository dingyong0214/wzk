/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-08-07 15:30:32
 * @version $Id$
 */
jQuery.easing.jswing = jQuery.easing.swing; jQuery.extend(jQuery.easing, { def: "easeOutQuad", swing: function (e, f, a, h, g) { return jQuery.easing[jQuery.easing.def](e, f, a, h, g) }, easeInQuad: function (e, f, a, h, g) { return h * (f /= g) * f + a }, easeOutQuad: function (e, f, a, h, g) { return -h * (f /= g) * (f - 2) + a }, easeInOutQuad: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f + a } return -h / 2 * ((--f) * (f - 2) - 1) + a }, easeInCubic: function (e, f, a, h, g) { return h * (f /= g) * f * f + a }, easeOutCubic: function (e, f, a, h, g) { return h * ((f = f / g - 1) * f * f + 1) + a }, easeInOutCubic: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f * f + a } return h / 2 * ((f -= 2) * f * f + 2) + a }, easeInQuart: function (e, f, a, h, g) { return h * (f /= g) * f * f * f + a }, easeOutQuart: function (e, f, a, h, g) { return -h * ((f = f / g - 1) * f * f * f - 1) + a }, easeInOutQuart: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f * f * f + a } return -h / 2 * ((f -= 2) * f * f * f - 2) + a }, easeInQuint: function (e, f, a, h, g) { return h * (f /= g) * f * f * f * f + a }, easeOutQuint: function (e, f, a, h, g) { return h * ((f = f / g - 1) * f * f * f * f + 1) + a }, easeInOutQuint: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f * f * f * f + a } return h / 2 * ((f -= 2) * f * f * f * f + 2) + a }, easeInSine: function (e, f, a, h, g) { return -h * Math.cos(f / g * (Math.PI / 2)) + h + a }, easeOutSine: function (e, f, a, h, g) { return h * Math.sin(f / g * (Math.PI / 2)) + a }, easeInOutSine: function (e, f, a, h, g) { return -h / 2 * (Math.cos(Math.PI * f / g) - 1) + a }, easeInExpo: function (e, f, a, h, g) { return (f == 0) ? a : h * Math.pow(2, 10 * (f / g - 1)) + a }, easeOutExpo: function (e, f, a, h, g) { return (f == g) ? a + h : h * (-Math.pow(2, -10 * f / g) + 1) + a }, easeInOutExpo: function (e, f, a, h, g) { if (f == 0) { return a } if (f == g) { return a + h } if ((f /= g / 2) < 1) { return h / 2 * Math.pow(2, 10 * (f - 1)) + a } return h / 2 * (-Math.pow(2, -10 * --f) + 2) + a }, easeInCirc: function (e, f, a, h, g) { return -h * (Math.sqrt(1 - (f /= g) * f) - 1) + a }, easeOutCirc: function (e, f, a, h, g) { return h * Math.sqrt(1 - (f = f / g - 1) * f) + a }, easeInOutCirc: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return -h / 2 * (Math.sqrt(1 - f * f) - 1) + a } return h / 2 * (Math.sqrt(1 - (f -= 2) * f) + 1) + a }, easeInElastic: function (f, h, e, l, k) { var i = 1.70158; var j = 0; var g = l; if (h == 0) { return e } if ((h /= k) == 1) { return e + l } if (!j) { j = k * 0.3 } if (g < Math.abs(l)) { g = l; var i = j / 4 } else { var i = j / (2 * Math.PI) * Math.asin(l / g) } return -(g * Math.pow(2, 10 * (h -= 1)) * Math.sin((h * k - i) * (2 * Math.PI) / j)) + e }, easeOutElastic: function (f, h, e, l, k) { var i = 1.70158; var j = 0; var g = l; if (h == 0) { return e } if ((h /= k) == 1) { return e + l } if (!j) { j = k * 0.3 } if (g < Math.abs(l)) { g = l; var i = j / 4 } else { var i = j / (2 * Math.PI) * Math.asin(l / g) } return g * Math.pow(2, -10 * h) * Math.sin((h * k - i) * (2 * Math.PI) / j) + l + e }, easeInOutElastic: function (f, h, e, l, k) { var i = 1.70158; var j = 0; var g = l; if (h == 0) { return e } if ((h /= k / 2) == 2) { return e + l } if (!j) { j = k * (0.3 * 1.5) } if (g < Math.abs(l)) { g = l; var i = j / 4 } else { var i = j / (2 * Math.PI) * Math.asin(l / g) } if (h < 1) { return -0.5 * (g * Math.pow(2, 10 * (h -= 1)) * Math.sin((h * k - i) * (2 * Math.PI) / j)) + e } return g * Math.pow(2, -10 * (h -= 1)) * Math.sin((h * k - i) * (2 * Math.PI) / j) * 0.5 + l + e }, easeInBack: function (e, f, a, i, h, g) { if (g == undefined) { g = 1.70158 } return i * (f /= h) * f * ((g + 1) * f - g) + a }, easeOutBack: function (e, f, a, i, h, g) { if (g == undefined) { g = 1.70158 } return i * ((f = f / h - 1) * f * ((g + 1) * f + g) + 1) + a }, easeInOutBack: function (e, f, a, i, h, g) { if (g == undefined) { g = 1.70158 } if ((f /= h / 2) < 1) { return i / 2 * (f * f * (((g *= (1.525)) + 1) * f - g)) + a } return i / 2 * ((f -= 2) * f * (((g *= (1.525)) + 1) * f + g) + 2) + a }, easeInBounce: function (e, f, a, h, g) { return h - jQuery.easing.easeOutBounce(e, g - f, 0, h, g) + a }, easeOutBounce: function (e, f, a, h, g) { if ((f /= g) < (1 / 2.75)) { return h * (7.5625 * f * f) + a } else { if (f < (2 / 2.75)) { return h * (7.5625 * (f -= (1.5 / 2.75)) * f + 0.75) + a } else { if (f < (2.5 / 2.75)) { return h * (7.5625 * (f -= (2.25 / 2.75)) * f + 0.9375) + a } else { return h * (7.5625 * (f -= (2.625 / 2.75)) * f + 0.984375) + a } } } }, easeInOutBounce: function (e, f, a, h, g) { if (f < g / 2) { return jQuery.easing.easeInBounce(e, f * 2, 0, h, g) * 0.5 + a } return jQuery.easing.easeOutBounce(e, f * 2 - g, 0, h, g) * 0.5 + h * 0.5 + a } });
// banner轮播
(function (jQuery) {
    jQuery.fn.picture_swing = function (options) {
        var defaults = {
            time: 5000,		//轮换秒数
            index: 1,			//默认第几张		
            speed: 800,		//切换时间
            dis: 1190,      //移动宽度
            splits: 1,
            swing_pic: ".banner_pic",//banner图片
            swing_list: ".banner_list",//banner图片列表
            pic_selected: "pic_selected",//给图片列表按钮添加class
            btn_left: ".banner_btn_l",//左按钮
            btn_right: ".banner_btn_r",//右按钮
            easing: "easeInOutExpo"  //easeInOutExpo,easeInOutElastic,easeOutBounce,"easeInOutBack"
        };
        var opts = jQuery.extend({}, defaults, options),
			_swing_pic = opts.swing_pic,
			_swing_list = opts.swing_list,
			_selected = opts.pic_selected,
			_index = opts.index,
			_time = opts.time,
			_speed = opts.speed,
			_dis = opts.dis,
			_easing = opts.easing,
			_splits = opts.splits,
			_this = jQuery(this),
			_btn_left = opts.btn_left,
			_btn_right = opts.btn_right,
			btn_left = _this.find(_btn_left),
			btn_right = _this.find(_btn_right),
			node_ul = _this.find(_swing_pic),
			node_li = node_ul.find("li"),
			node_list = jQuery(_swing_list),
			node_li_nav = node_list.find("li"),
			li_len = node_li.length;
        // list_width = node_list.css('width'),
        // margin_right = node_li_nav.css('margin-right');
        node_ul.css("width", _dis * li_len);
        // node_li_nav.css('width', (parseInt(list_width) - (li_len - 1) * parseInt(margin_right)) / li_len);
        // node_li_nav.eq(-1).css('margin-right', 0);
        var _start_left = node_ul.css("left");
        var _timer = setInterval(show, _time);
        init();
        function init() {
            btn_left.add(btn_right).mouseenter(function () {
                _timer = clearInterval(_timer);
                $(this).addClass('selected')
            }).mouseleave(function () {
                _timer = setInterval(show, _time);
                $(this).removeClass('selected')
            });
            btn_left.click(function (event) {
                left();
            });
            btn_right.click(function (event) {
                show();
            });
            node_ul.mouseenter(function () {
                _timer = clearInterval(_timer);
            }).mouseleave(function () {
                _timer = setInterval(show, _time);
            });
            // node_ul.parent().parent().mouseenter(function() {
            // 	node_list.addClass('selected');
            // }).mouseleave(function() {
            // 	node_list.removeClass('selected');
            // });
            node_li_nav.mouseover(function () {
                node_ul.stop(true, true);
                node_li_nav.eq(_index - 1).removeClass(_selected);
                _index = jQuery(this).index() + 1;
                node_li_nav.eq(_index - 1).addClass(_selected);
                _left = -_dis * (_index - 1);
                node_ul.animate({ "left": _left }, _speed, _easing);
                _timer = clearInterval(_timer);
            }).mouseout(function () {
                _timer = setInterval(show, _time);
            });
        }

        function show() {//right
            node_ul.stop(true, true);
            node_li_nav.eq(_index - 1).removeClass(_selected);
            _index++;
            if (_index > li_len) {
                node_ul.append(node_ul.find("li:lt(1)"));
                node_ul.css("left", parseInt(node_ul.css("left")) + _dis);
                node_li_nav.eq(0).addClass(_selected);
            }
            else {
                node_li_nav.eq(_index - 1).addClass(_selected);
            }
            var _left = parseInt(node_ul.css("left")) - _dis;
            node_ul.animate({ "left": _left }, _speed, _easing, function () {
                if (_index > li_len) {
                    node_ul.prepend(node_ul.find("li:gt(" + (li_len - _splits - 1) + ")"));
                    node_ul.css("left", 0);
                    _index = 1;
                }
            });
        }
        function left() {//left
            node_ul.stop(true, true);
            node_li_nav.eq(_index - 1).removeClass(_selected);
            _index--;
            if (_index <= 0) {
                node_ul.prepend(node_ul.find("li:gt(" + (li_len - _splits - 1) + ")"));
                node_ul.css("left", parseInt(node_ul.css("left")) - _dis);
                node_li_nav.eq(_index - 1).addClass(_selected);
            }
            else {
                node_li_nav.eq(_index - 1).addClass(_selected);
            }
            var _left = parseInt(node_ul.css("left")) + _dis;
            node_ul.animate({ "left": _left }, _speed, _easing, function () {
                if (_index <= 0) {
                    node_ul.append(node_ul.find("li:lt(1)"));
                    node_ul.css("left", -_dis * (li_len - 1));
                    _index = li_len;
                }
            });
        }
    }
})(jQuery);

// 固定在屏幕中
(function ($) {
    $.fn.fixedTop = function (options) {

        var cssfixedsupport = !-[1, ] && !window.XMLHttpRequest; //$.browser.msie && parseFloat($.browser.version) < 7, //判断是否ie6
        $window = $(window);

        return this.each(function () {

            var $this = $(this),
				fixedTime,
				def = {
				    cankao: null,//参考的对象 窗口改变 目标元素才可以跟着改变
				    is_right: false,//是否固定浮动在右边
				    startline: $this.offset().top, //出现固定位置的距离
				    top: 0, //固定位置的高度
				    left: $this.offset().left, //左边的距离
				    right: $this.offset().right,
				    z_index: 100
				},
				chazhi,//目标元素 与 参考元素 的offset.left差值
				opts = $.extend({}, def, options),
				controlTop = opts.top, //固定的高度
				init_css = { //初始化css属性
				    top: $this.css("top"),
				    right: $this.css("right"),
				    bottom: $this.css("bottom"),
				    left: $this.css("left"),
				    position: $this.css("position"),
				    zIndex: $this.css("zIndex")
				},
				clonehtml = $this.clone().css("visibility", "hidden"); //复制一遍被选中元素,复制后的元素不可见
            var cankao = $(opts.cankao);
            if (opts.cankao) {
                var init_cankao = cankao.offset().left;
                chazhi = opts.left - init_cankao;//目标元素 与 参考元素 的offset.left差值
            };

            if ($this.css("position") == "relative") {
                var addBox = $("<div></div>").css({
                    "position": "relative"
                }); //新建一个元素，作为后代元素的参考对象
                $this.wrapInner(addBox); //目标元素被新建元素包裹着，防止其他子元素以目标元素为参考的left、top属性失去定位标准
            }

            if (opts.startline == 0 && !cssfixedsupport) {//起始位置为0并且非ie6 直接固定位置，不用设置定时器
                if (opts.is_right) {
                    $this.after(clonehtml).css({
                        position: 'fixed',
                        top: controlTop,
                        right: opts.right,
                        zIndex: opts.z_index
                    });
                } else {
                    $this.after(clonehtml).css({
                        position: 'fixed',
                        top: controlTop,
                        left: opts.left,
                        zIndex: opts.z_index
                    });
                }
                if (opts.cankao) {
                    $window.resize(function (event) {
                        opts.left = cankao.offset().left + chazhi;
                        $this.after(clonehtml).css({
                            left: opts.left
                        });
                    });
                };
                return;
            };
            clearInterval(fixedTime);
            fixedTime = setInterval(function () {
                var shouldfixed = ($window.scrollTop() >= opts.startline) ? true : false;
                var controlTop_1 = cssfixedsupport ? $window.scrollTop() + controlTop : controlTop;

                if (shouldfixed) {
                    $this.after(clonehtml).css({
                        position: cssfixedsupport ? 'absolute' : 'fixed',
                        top: controlTop_1,
                        left: opts.left,
                        zIndex: opts.z_index
                    });
                } else {
                    clonehtml.remove();
                    $this.css(init_css);
                }

            }, 100);
            if (opts.cankao) {
                $window.resize(function (event) {
                    opts.left = cankao.offset().left + chazhi;
                });
            };

        });
    };

})(jQuery);

//  加入收藏 <a onclick="AddFavorite(window.location,document.title)">加入收藏</a>
function AddFavorite(sURL, sTitle) {
    try {
        window.external.addFavorite(sURL, sTitle);
    } catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        } catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}

/**
 * [countDown 倒计时]
 * @param  {[string]} selector [需要倒计时的选择器，如".count"]
 * @param  {[function]} callback    [结束后回调函数]
 */
var countDown = function (selector, callback) {
    if ($(selector).length === 0) return;
    var d1, d2, day, h, m, s, ms, d1ms, d2ms, d3ms, t, html = '',
		si, d_start, d_now, d_ms,
		dms = 1000 * 60 * 60 * 24,
		hms = 1000 * 60 * 60,
		mms = 1000 * 60,
		checkTime = function (i) {
		    if (i < 10) {
		        i = "0" + i;
		    }
		    return i;
		},
		counter = function () {
		    d_now = new Date();
		    ms = d_ms - (d_now.getTime() - d3ms); // 剩下毫秒数
		    html = '';
		    if (ms <= 0) {
		        day = 0;
		        h = 0;
		        m = 0;
		        s = 0;
		        ss = 0 % 1000;
		        clearInterval(t);
		        if (callback != null && callback != '') {
		            callback();
		        }
		    } else {
		        day = Math.floor(ms / dms);
		        h = Math.floor(ms % dms / hms);
		        m = Math.floor(ms % hms / mms);
		        s = Math.floor(ms % mms / 1000);
		        // var ss = Math.floor(ms % 1000);
		    }
		    si = day > 0 ? 1 : h > 0 ? 2 : m > 0 ? 3 : s > 0 ? 4 : 0;
		    switch (si) {
		        case 1:
		            html += '<span>' + checkTime(day) + '\u5929</span>'; //天
		        case 2:
		            html += '<span>' + checkTime(h) + '\u65f6</span>'; //时
		        case 3:
		            html += '<span>' + checkTime(m) + '\u5206</span>'; //分
		        case 4:
		            html += '<span>' + checkTime(s) + '\u79d2</span>'; //秒
		            break;
		        case 0:
		            html += '<span>' + '0' + '</span>\u79d2';
		    }
		    $(selector).html(html);

		};
    d_start = new Date();
    d3ms = d_start.getTime();

    // d1 = new Date(2015, 09, 23, 0, 0, 0);//结束时间 月份从0开始记得减去 1
    // d2 = new Date(2015, 09, 22, 0, 0, 0);//开始时间 月份从0开始记得减去 1
    // d1ms = d1.getTime();
    // d2ms = d2.getTime();
    d1ms = parseInt($(selector).attr("data-targettime")) || 0; //结束时间
    d2ms = parseInt($(selector).attr("data-servertime")) || 0; //开始时间

    d_ms = d1ms - d2ms; //开始结束 毫秒差

    counter();
    t = setInterval(function () {
        counter();
    }, 500);
};
var layerAlert = function (msg, v3_time) {
    layer.open({
        type: 1,
        title: "&nbsp;",
        skin: "",
        btn: ['确定'],
        time: v3_time || 5000,
        success: function (elem, index) {

        },
        content: '<div class="v3_layer_alert">' + msg + '</div>'
    });
    },
    layerErrorMsg = function (msg, time, callback) {
        layer.msg(msg, { icon: 2, time: time || 1000 }, callback);
    },
    layerSuccessMsg = function (msg, time, callback) {
        layer.msg(msg, { icon: 1, time: time || 1000 }, callback);
    },
	layerWarmAlert = function (msg, wtitle, v3_time) {
	    layer.open({
	        type: 1,
	        title: [wtitle, "" + layerOpt.titleColor],
	        skin: "",
	        btn: ['确定'],
	        time: v3_time || 5000,
	        success: function (elem, index) {

	        },
	        content: '<div class="v3_layer_alert">' + msg + '</div>'
	    });
	},
	layerConfirm = function (msg, yesCallback, noCallback) {
	    layer.open({
	        type: 1,
	        title: "&nbsp;",
	        skin: "",
	        btn: ['确定', '取消'],
	        success: function (elem, index) {

	        },
	        yes: function (index, elem) {
	            if (yesCallback != null && yesCallback != '') {
	                yesCallback(index, elem);
	            }
	            layer.close(index);
	        },
	        cancel: function (index) {
	            if (noCallback != null && noCallback != '') {
	                return noCallback(index);
	            }
	        },
	        content: '<div class="v3_layer_alert">' + msg + '</div>'
	    });
	},
	layerPage = function (opt) {
	    opt = opt || {};
	    var arr = ["btn", "yes", "cancel"],
			json = {
			    btn: opt.btn || ['确定', '取消'],
			    yes: function (index, elem) {
			        opt.yes && opt.yes(index, elem);
			        layer.close(index);
			    },
			    cancel: function (index) {
			        if (opt.cancel) {
			            return opt.cancel(index);
			        }
			    }
			},
			now_json = {
			    type: 1,
			    title: opt.title || false,
			    skin: opt.skin || "",
			    success: function (elem, index) {
			        opt.success && opt.success(elem, index);
			    },
			    area: [opt.width || '500px', opt.height || '400px'],
			    content: opt.html || ""
			};
	    for (var i = arr.length - 1; i >= 0; i--) {
	        opt[arr[i]] && (now_json[arr[i]] = json[arr[i]]);
	    };
	    layer.open(now_json);
	},
	layerLogin = function (callback) {
	    var html = '';
	    html += '<div class="loginMain" style="padding:30px 40px 5px;">';
	    html += '	<div class="bcover"></div>';
	    html += '	<div class="boxHead">';
	    html += '		<a class="boxLogo" href="###"><img src="../skin/images/v3_login2.png" /></a>';
	    html += '		<div class="fr">';
	    html += '			<a href="###"><i></i>商家认证</a>';
	    html += '			<a href="javascript:;" class="layer_ljzc"><i></i>立即注册</a>';
	    html += '		</div>';
	    html += '	</div>';
	    html += '	<div class="tips"><i></i>公共场所不建议自动登录，以防止账号丢失！</div>';
	    html += '	<form class="bContainer">';
	    html += '		<div class="loginBox">';
	    html += '			<div class="userName">';
	    html += '				<p><i></i></p>';
	    html += '				<input type="text" placeholder="请输入您的手机号码" />';
	    html += '			</div>';
	    html += '			<div class="passWord">';
	    html += '				<p><i></i></p>';
	    html += '				<input type="password"  placeholder="请输入您的登录密码" />';
	    html += '			</div>';
	    html += '		</div>';
	    html += '		<div class="keycode">';
	    html += '			<input type="text" /><img src="" alt="验证码" />';
	    html += '		</div>';
	    html += '		<div class="logSelect">';
	    html += '			<input type="hidden" id="choseLogin" value="0" /><!--用于记录是否选择自动登录,值为1表示已选择自动登录-->';
	    html += '			<div class="fl"><input type="checkbox" id="logSelect" /><label for="logSelect">自动登录</label></div>';
	    html += '			<a class="fr" href="###">忘记密码？</a>';
	    html += '		</div>';
	    html += '		<input type="submit" value="登   录" class="submit" />';
	    html += '	</form>';
	    html += '</div>';
	    // '<table class="v3_all_login">'+
	    // 	'<tr><td class="v3_left">账号：</td>'+
	    // 		'<td><input type="text"><b class="v3_fp_true"></b><b class="v3_fp_wrong">*</b></td></tr>'+
	    // 	'<tr><td class="v3_left">密码：</td>'+
	    // 		'<td><input type="password"><b class="v3_fp_true"></b><b class="v3_fp_wrong">*</b></td></tr>'+
	    // 	'<tr><td class="v3_left">验证码：</td>'+
	    // 		'<td><img src="skin/images/000.png" alt=""><b class="v3_fp_true"></b><b class="v3_fp_wrong">*</b></td></tr>'+
	    // '</table>'
	    layer.open({
	        type: 1,
	        title: "&nbsp;",
	        skin: "",
	        success: function (elem, index) {
	            $(elem).find('.submit').click(function (event) {
	                if (callback != null && callback != '') {
	                    callback(elem, index);
	                }
	                layer.close(index);
	            });
	            $(elem).find('.layer_ljzc').click(function (event) {
	                layerRegister(function (elem) { //elem 弹出层dom对象
	                    var inputs = $(elem).find("input"),
							name = inputs.eq(0).val(),
							password = inputs.eq(1).val();
	                    layerAlert("用户名：" + name + "密码：" + password);
	                });
	                layer.close(index);
	                return false;
	            });
	        },
	        // btn: ['确定','取消'],
	        // yes: function(index, elem) {
	        // 	if (yesCallback != null && yesCallback != '') {
	        // 		yesCallback(index, elem);
	        // 	}
	        // 	layer.close(index);
	        // },
	        // cancel: function(index){
	        // 	if (noCallback != null && noCallback != '') {
	        // 		return noCallback(index);
	        // 	}
	        //},
	        area: ['380px', '410px'],
	        content: html
	    });
	},
	layerRegister = function (callback) {
	    var html = '';
	    html += '<div class="v3_findpwd_main v3_register_box" style="border:0;">';
	    html += '	<table>';
	    html += '		<tr>';
	    html += '			<td class="v3_left"><i>*</i>用户名：</td>';
	    html += '			<td><div><input type="text" placeholder="请输入您的昵称"><i class="v3_fp_user_i"></i></div><b class="v3_fp_true"></b><b class="v3_fp_wrong">该用户名已被注册</b></td>';
	    html += '		</tr>';
	    html += '		<tr>';
	    html += '			<td class="v3_left"><i>*</i>请设置密码：</td>';
	    html += '			<td><div><input type="password" placeholder="请设置您的密码"><i class="v3_fp_icon"></i></div><b class="v3_fp_true"></b></td>';
	    html += '		</tr>';
	    html += '		<tr>';
	    html += '			<td class="v3_left"><i>*</i>请确认密码：</td>';
	    html += '			<td><div><input type="password" placeholder="请再次输入您的密码"><i class="v3_fp_icon"></i></div><b class="v3_fp_true"></b><b class="v3_fp_wrong">两次输入密码不一致，请重新输入</b></td>';
	    html += '		</tr>';
	    html += '		<tr>';
	    html += '			<td class="v3_left"><i>*</i>验证手机：</td>';
	    html += '			<td><div><input type="text" placeholder="请输入绑定的手机号码"><i class="v3_fp_msg_i"></i></div><b class="v3_fp_true"></b><b class="v3_fp_wrong">手机号码已注册，请重新输入</b></td>';
	    html += '		</tr>';
	    html += '		<tr>';
	    html += '			<td class="v3_left"><i>*</i>短信验证码：</td>';
	    html += '			<td><div class="v3_r_code"><input type="text" placeholder="请输入验证码"></div><a href="javascript:;" class="v3_fp_code_btn">获取短信验证码</a><b class="v3_fp_wrong">验证码错误，请重新输入</b></td>';
	    html += '		</tr>';
	    html += '	</table>';
	    html += '	<div class="v3_r_rule">';
	    html += '		<input type="checkbox" checked="checked"><span>我已阅读并同意</span><a href="#">《擦一擦用户协议》</a><br>';
	    html += '		<a href="#">商家认证申请&gt;&gt;</a>';
	    html += '	</div>';
	    html += '	<a href="#" class="v3_r_btn">注册</a>';
	    html += '</div>';
	    layer.open({
	        type: 1,
	        title: "&nbsp;",
	        skin: "",
	        success: function (elem, index) {
	            $(elem).find('.submit').click(function (event) {
	                if (callback != null && callback != '') {
	                    callback(elem, index);
	                }
	                layer.close(index);
	            });
	        },
	        area: ['744px', '545px'],
	        content: html
	    });
	};

// 页面布局写法
// <div class="count" data-targettime="1443153882000" data-servertime="1442900235282"></div>
// countDown(".count",function(){
// 	alert(123456);
// });

$(function () {
    //首页图片 懒加载
    // if($("#lazy_img").length){
    // 	$("img").lazyload({
    // 		data_attribute:"src",
    // 		threshold : 200,
    // 		placeholder:"skin/images/c2.png"
    // 	});
    // };
    //家私套餐
    $(".h_taocan li").hover(
		function () {
		    $(this).addClass('selected');
		}, function () {
		    $(this).removeClass('selected');
		});
    //收藏擦一擦
    $(".t_cayica").click(function (event) {
        AddFavorite(window.location, document.title);
        return false;
    });
    //点击搜索
    $(".search_btn").click(function () {
        var key = $(this).val();
        if (key == "") {
            alert("请输入搜索关键字！");
        } else {
            location.href = "/search-66-0-0-0-" + encodeURIComponent(key) + "-1-1.html";
        }
    });
    //广告 中国结、扫一扫  固定顶部
    $(".h_ad_left,.h_ad_right").fixedTop({
        cankao: ".v3_container",
        top: $(".h_ad_left").offset().top,
        startline: 0
    });
    //闲置列表 全部 个人 商家 tab栏 固定顶部
    $(".v3_sort_tab_wrap").fixedTop({
        cankao: ".v3_container"
    });
    //首页
    //顶部  订单管理、我的收藏
    $(".v3_top_main .t_right_m").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    //首页搜索选择
    $(".search_slt span").on('click', function (event) {
        $(this).addClass('selected').siblings().removeClass('selected');
    });
    //其他页面左侧导航栏
    // $(".v3_nav_all").hover(function() {
    // 	$(this).addClass('v3_nav_show');
    // }, function() {
    // 	$(this).removeClass('v3_nav_show');
    // });
    $(".v3_nav_all").mouseenter(function (event) {
        $(this).addClass('v3_show_nav');
        return false;
    });
    $(".v3_nav_all").mouseleave(function (event) {
        $(this).removeClass('v3_show_nav');
    });
    //导航栏
    $(".v3_side_nav li").hover(function () {
        if ($(this).next().length)
            $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    //区域
    $(".v3_quyu_w li").hover(function () {
        if ($(this).next().length == 0) {
            $(this).find('.v3_quyu_hide').css({
                "left": "auto",
                "right": '0'
            });
        };
        $(this).addClass('v3_show');
    }, function () {
        $(this).removeClass('v3_show');
    });
    //信誉商家
    $(".v3_merchant li").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    /**********主页banner图片切换***********/
    $(".v3_banner_m").picture_swing();

    /**********公告 安全***********/
    $(".v3_gonggao_nav a").click(function (event) {
        var i = $(this).index();
        $(this).addClass('selected').siblings().removeClass('selected');
        $(".v3_gonggao_list ul").eq(i).show().siblings().hide();
        return false;
    });

    // 滚动内容
    /**
	 * [scroll_list 内容滚动]
	 * @param  {[type]} elem_p [父标签选择器]
	 * @param  {[type]} elem_c [子元素 如 li]
	 * @param  {[type]} len    [超过多少个时滚动]
	 * @return {[type]}        [null]
	 */
    function scroll_list(elem_p, elem_c, len) {
        var h_new_scroll = $(elem_c),
			scroll_len = h_new_scroll.length;
        var scroll_ing = function () {
            var $ul = $(elem_p);
            var height = $ul.find("li").eq(0).innerHeight();
            $ul.animate({
                "marginTop": -height + "px"
            }, 600, function () {
                $ul.css({
                    "marginTop": 0
                }).find("li").eq(0).appendTo($ul);
            });
        };
        if (scroll_len > len) {
            var scroll_t;
            scroll_t = setInterval(scroll_ing, 3000);
            $(elem_p).hover(function () {
                clearInterval(scroll_t);
            }, function () {
                scroll_t = setInterval(scroll_ing, 3000);
            });
        }
    }
    scroll_list("#announcement", "#announcement li", 4); //公告
    scroll_list("#safety", "#safety li", 4);//安全
    scroll_list("#deal", "#deal li", 5);// 正在交易

    // 顶部固定
    var v3_top_wrap = $(".v3_top_wrap");
    if (v3_top_wrap.length > 0) {
        v3_top_wrap.fixedTop({
            startline: 0,
            top: 0
        });
    }

    // 右侧客服
    var v3_sidebar = $(".v3_sidebar");
    if (v3_sidebar.length > 0) {
        var sidebar_top = ($(window).height() - v3_sidebar.height()) / 2;
        v3_sidebar.fixedTop({
            is_right: true,
            startline: 0,
            top: sidebar_top
        });
    }

    $(".v3_kefu").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    //回到顶部
    $(".v3_to_top").click(function () {
        $('body,html').animate({
            scrollTop: 0
        });
        return false;
    });




    // 个人中心 导航栏效果
    $(".v3_m_nav_main li").click(function (event) {
        $(this).addClass('selected').siblings().removeClass('selected');
    });
    //个人中心 左侧 红色效果
    $(".v3_m_sub_nav p").click(function (event) {
        var _this = $(this);
        _this.parent().parent().find("p").removeClass('selected');
        _this.addClass('selected');
        _this.siblings('.v3_m_nav_h').addClass('selected');
    });

    //订单管理选择状态
    $(".v3_zht_w").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    $(".v3_zht_hide p").click(function (event) {
        var _this = $(this),
			text = _this.text();
        _this.parent().parent().find('.v3_zht_show p').text(text);
        _this.parent().parent().removeClass('selected');
    });

    var layerOpt = {
        titleColor: "color:#f00;"
    };

    //订单取消
    $(".v3_order_cancel").click(function (event) {
        layer.open({
            type: 1,
            title: ["<i></i>确定要取消订单吗，订单取消后不能恢复", "color:#f00;"],
            skin: "v3_order_c_wrap",
            area: ['390px', '275px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {
                $(elem).find('.v3_c_r_main').hover(function () {
                    $(this).addClass('selected');
                }, function () {
                    $(this).removeClass('selected');
                });
                $(elem).find('.v3_c_r_hide p').click(function (event) {
                    var _this = $(this),
						text = _this.text();
                    _this.parent().parent().find('.v3_c_r_show span').text(text);
                    _this.parent().parent().removeClass('selected');
                });
            },
            yes: function (index, elem) {
                var text = $(elem).find('.v3_c_r_show span').text();
                alert(text);
                layer.close(index);
            },
            content: $(".v3_cancel_reason_hide").html()
        });
        return false;
    });


    var revisedPrice = function (yesCallback) { //修改价格函数 参数是点击“确定”按钮要执行的函数回调
        layer.open({
            type: 1,
            title: ["<i></i>请您在与买家达成协议一致的情况下修改价格", "color:#f00;"],
            skin: "v3_r_p_wrap",
            area: ['735px', '300px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {
                var inputs = $(elem).find('.v3_r_p_input input');
                inputs.on('change keyup', function (event) {
                    var zongjia = parseFloat(inputs.eq(0).val()) * 100 || 0,
                        yunfei = parseFloat(inputs.eq(1).val()) * 100 || 0;
                    inputs.eq(2).val((zongjia + yunfei) / 100);
                });
            },
            yes: function (index, elem) {
                if (yesCallback != null && yesCallback != '') {
                    yesCallback(index, elem);
                }
                layer.close(index);
            },
            content: $(".v3_revised_price_hide").html()
        });
    },
		closeTrade = function (yesCallback) { //关闭交易函数 参数是点击“确定”按钮要执行的函数回调
		    layer.open({
		        type: 1,
		        title: ["<i></i>请您在与买家达成协议一致的情况下关闭交易", "color:#f00;"],
		        skin: "v3_c_t_wrap",
		        area: ['370px', '200px'],
		        btn: ['确定', '取消'],
		        success: function (elem, index) {

		        },
		        yes: function (index, elem) {
		            if (yesCallback != null && yesCallback != '') {
		                yesCallback(index, elem);
		            }
		            layer.close(index);
		        },
		        content: $(".v3_close_trade_hide").html()
		    });
		};

    //卖家个人中心 等待买家付款 修改价格
    $(".v3_order_xgjg").click(function (event) {
        revisedPrice(function (index, elem) {// elem 是弹出层dom对象
            var inputs = $(elem).find('.v3_r_p_input input');
            var zongjia = inputs.eq(0).val(),
				yunfei = inputs.eq(1).val(),
				shifu = inputs.eq(2).val();
            alert("总价：" + zongjia + "运费：" + yunfei + "实付：" + shifu);
        });
        return false;
    });

    //卖家个人中心 等待买家付款 关闭交易
    $(".v3_order_gbjy").click(function (event) {
        closeTrade(function (index, elem) {
            var selects = $(elem).find('select');
            alert(selects.val());
        });
        return false;
    });

    //卖家订单详情 修改价格
    $(".v3_o_m_xgjg").click(function (event) {
        revisedPrice(function (index, elem) {
            var inputs = $(elem).find('.v3_r_p_input input');
            var zongjia = inputs.eq(0).val(),
				yunfei = inputs.eq(1).val(),
				shifu = inputs.eq(2).val();
            alert("总价：" + zongjia + "运费：" + yunfei + "实付：" + shifu);
        });
        return false;
    });

    //卖家订单详情 关闭交易
    $(".v3_o_m_gbjy").click(function (event) {
        closeTrade(function (index, elem) {
            var selects = $(elem).find('select');
            alert(selects.val());
        });
        return false;
    });

    //发货弹出层函数
    var Deliver = function () {
        layer.open({
            type: 1,
            title: "&nbsp;",
            skin: "v3_fahuo_wrap",
            area: ['750px', '550px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {
                /*var all_li = $(elem).find('.v3_fahuo_m li');
				var kuaidi_list=$(elem).find(".v3_qtkd_list"),
					kdgs_input=$(elem).find('.v3_fahuo_kdgs input'),
					danhao = $(elem).find('.v3_fahuo_kddh input').eq(1),
					t;
				kdgs_input.on("keyup",function(event) {//快递公司输入框，改变内容显示其他快递公司列表
					var text=$(this).val();
					kuaidi_list.show();
				});
				kdgs_input.on('blur', function(event) {
					t=setTimeout(function(){
						kuaidi_list.hide();
					},2000);
				});
				kdgs_input.on('focus', function(event) {
					clearTimeout(t);
				});
				kuaidi_list.find('dd').on('click', function(event) {// 点击快递后 影藏列表
					var text=$(this).text();
					kdgs_input.val(text);
					kuaidi_list.hide();
					clearTimeout(t);
				});
				danhao.on('focus', function(event) {//快递单号 聚焦后 影藏列表
					kuaidi_list.hide();
					clearTimeout(t);
				});
				all_li.click(function(event) {
					if ($(this).index() === 9) {
						$(elem).find('.v3_fahuo_kdgs').css('display', 'block');
					} else {
						$(elem).find('.v3_fahuo_kdgs').css('display', 'none');
					}
					if ($(this).hasClass('v3_fahuo_shsm') && $(this).hasClass('selected')) {
						$(this).removeClass('selected');
						$(elem).find('.v3_fahuo_layer').css('display', 'none');
						return;
					} else if ($(this).hasClass('v3_fahuo_shsm')) {
						$(elem).find('.v3_fahuo_layer').css('display', 'block');
					}
					all_li.removeClass('selected');
					$(this).addClass('selected');
					return ;
				});*/
                var kdfh = $(elem).find("#kdfh"),
					kuaidi_list = $(elem).find(".v3_qtkd_list"),
					shsm = $(elem).find("#shsm");
                $(elem).find(".v3_fahuo_m ul label input").on("click", function () {
                    if (kdfh.is(':checked') == true) {
                        return true;
                    } else {
                        return false;
                    }
                });
                $(elem).find("#elseKuaidi").on("keyup", function (event) {//快递公司输入框，改变内容显示其他快递公司列表
                    var text = $(this).val();
                    if ($(this).val()) {
                        kuaidi_list.show();
                    }
                });
                kdfh.on("click", function () {
                    $(elem).find(".v3_fahuo_m").addClass("c");
                    $(elem).find("#shsmArea").removeClass('c');
                    $(elem).find(".tinput").removeAttr("readonly");
                });
                shsm.on("click", function () {
                    $(elem).find(".v3_fahuo_m").removeClass("c");
                    $(elem).find(".v3_fahuo_m ul label input").prop("checked", false);
                    $(elem).find("#shsmArea").addClass('c');
                    $(elem).find(".tinput").attr("readonly", "readonly").val("");
                })
                kuaidi_list.find('dd').on('click', function (event) {// 点击快递后 影藏列表
                    event.stopPropagation();
                    var text = $(this).text();
                    $(elem).find("#elseKuaidi").val(text);
                    kuaidi_list.hide();
                });
                $(elem).find("#elseKuaidi").on("click", function (event) {
                    $(this).prev().prop("checked", true);
                });
                $(elem).on("click", function () {
                    kuaidi_list.hide();
                });
            },
            yes: function (index, elem) {
                var danhao = $(elem).find('.v3_fahuo_kddh input');
                alert("快递公司" + danhao.eq(0).val() + "快递单号" + danhao.eq(1).val());
                layer.close(index);
            },
            content: $(".v3_fahuo_hide").html()
        });
        return false;
    }
    //发货
    $(".v3_fahuo_btn").click(function (event) {
        Deliver();
        return false;
    });




    //登录调用方式
    // layerLogin(function(elem){ //elem 弹出层dom对象
    // 	var inputs=$(elem).find("input"),
    // 		name=inputs.eq(0).val(),
    // 		password=inputs.eq(1).val();
    // 	layerAlert("用户名："+name+"密码："+password);
    // });

    //注册调用方式
    // layerRegister(function(elem){ //elem 弹出层dom对象
    // 	var inputs=$(elem).find("input"),
    // 		name=inputs.eq(0).val(),
    // 		password=inputs.eq(1).val();
    // 	layerAlert("用户名："+name+"密码："+password);
    // });


    //同意退款
    $(".v3_tytk_btn").click(function (event) {
        layerWarmAlert("同意退款后钱款将直接打回买家账户", "温馨提示：保证您同意退款时还没发货", 10000);
        return false;
    });

    //订单边框变颜色效果
    $(".v3_order_main li").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });

    //去掉订单布局线条
    $(".v3_order_main tr").each(function (index, el) {
        var td = $(this).find('td');
        switch (td.length) {
            case 3:
            case 7:
                td.eq(0).css('border-right', 'none');
                td.eq(1).css('border-right', 'none');
                break;
            case 4:
            case 8:
                td.eq(0).css('border-right', 'none');
                td.eq(1).css('border-right', 'none');
                td.eq(2).css('border-right', 'none');
                break;
        }
    });
    //批量收货
    $(".v3_plshouhuo").click(function (event) {
        var input = $(".v3_select_l:checked"),
			len = input.length;
        for (var i = 0; i < len; i++) {
            if (!input.eq(i).parents("li").find('.v3_qrsh_btn').length) {
                layerAlert("宝贝在发货以后才能确认收货，请重新选择");
                return false;
            }
        }
        if (len === 0) {
            layerAlert("请选择宝贝确认收货，可以选择多个");
            return false;
        }
        // 此处写收货代码
        // 
        // 此处写收货代码
        return false;
    });

    //批量发货
    $(".v3_plfahuo").click(function (event) {
        var input = $(".v3_select_l:checked"),
			len = input.length;
        if (len === 0) {
            layerAlert("请选择宝贝确认发货，可以选择多个");
            return false;
        };
        for (var i = 0; i < len; i++) {
            if (!input.eq(i).parents("li").find('.v3_fahuo_btn').length) {
                layerAlert("订单在付款以后才能发货，请重新选择");
                return false;
            }
        }
        // 这里需要判断是不是同一个人
        Deliver();
        return false;
    });

    //删除
    $(".v3_order_del_btn").click(function (event) {
        $(this).parent().parent().remove();
        return false;
    });

    //批量删除
    $(".v3_order_del").click(function (event) {
        var input = $(".v3_select_l:checked"),
			len = input.length;
        if (len === 0) {
            layerAlert("请选择要删除的宝贝");
            return false;
        }
        for (var i = 0; i < len; i++) {
            if (!input.eq(i).parent().parent().find('.v3_order_del_btn').length) {
                layerAlert("宝贝在关闭订单或订单成功以后才能被删除，请重新选择");
                return false;
            }
        }
        input.each(function (index, el) {
            $(this).parents("li").remove();
        });
        return false;
    });

    //全选
    $('.v3_select_all').click(function (event) {
        var all = $('.v3_select_l,.v3_select_all');
        if ($(this).get(0).checked) {//全部选择
            $.each(all, function () {
                this.checked = "checked";
            });
        } else {
            $.each(all, function () {//全部取消
                this.checked = false;
            });
        }
    });

    //全选后 点击其他 选项，去掉全选选中状态
    $('.v3_select_l').click(function (event) {
        var all_1 = $('.v3_select_all'),
			all_2 = $('.v3_select_l'),
			len_2 = all_2.length,
			i = 0;
        if ($(this).get(0).checked) {//选中
            $.each(all_2, function () {
                if (this.checked === true)
                    i++;
            });
            if (i === len_2) {
                $.each(all_1, function () {
                    this.checked = "checked";
                });
            }
        } else {
            $.each(all_1, function () {//取消选中
                this.checked = false;
            });
        }
    });

    // 订单搜索 placeholder 效果
    $.each($(".v3_m_s_main input"), function (index, val) {
        var txt = $(this).attr("placeholder");
        $(this).val(txt);
        if ($(this).hasClass('password')) {
            $(this).hide().siblings(".password1").show();
        };
    });
    $(".v3_m_s_main input").focus(function (event) {
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
    $(".v3_m_s_main input").blur(function (event) {
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

    //我收藏的宝贝
    //我收藏的店铺
    //我收藏的宝贝 二级导航
    $(".v3_c_subnav li").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    //二级导航选中效果
    $(".v3_c_subnav .v3_c_hide a").click(function (event) {
        $(this).parent().parent().find('a').removeClass('selected');
        $(this).addClass('selected');
    });
    //批量管理
    $(".v3_collect_plgl").click(function (event) {
        var parent = $(this).parent();
        parent.siblings().show();
        parent.hide();
        $(".v3_collect_main li").addClass('v3_c_caozuo');//宝贝层出现
        $(".v3_dianpu_list").addClass('v3_c_caozuo');//店铺层出现
        return false;
    });
    //取消管理
    $(".v3_collect_qxgl").click(function (event) {
        var parent = $(this).parent();
        parent.siblings().show();
        parent.hide();
        $(".v3_collect_main li").removeClass('v3_c_caozuo');//宝贝去掉操作层
        $(".v3_c_gl_layer").removeClass('selected');//宝贝去掉选中效果
        $(".v3_dianpu_list").removeClass('v3_c_caozuo');//店铺去掉操作层
        $(".v3_dp_layer").removeClass('selected');//店铺去掉选中效果
        $(".v3_collect_s_all").get(0).checked = false;
        return false;
    });

    //全选宝贝
    $(".v3_collect_s_all").click(function (event) {
        if (this.checked === true) {
            $(".v3_c_gl_layer").addClass('selected');//宝贝选中效果
            $(".v3_dp_layer").addClass('selected');//店铺选中效果
        } else {
            $(".v3_c_gl_layer").removeClass('selected');//去掉宝贝选中效果
            $(".v3_dp_layer").removeClass('selected');//去掉店铺选中效果
        }

    });

    //删除选中的宝贝 全选右边删除
    $(".v3_collect_del").click(function (event) {
        $.each($(".v3_c_gl_layer"), function (index, val) {//宝贝删除
            if ($(this).hasClass('selected')) {
                $(this).parent().parent().remove();
            }
        });
        $.each($(".v3_dp_layer"), function (index, val) {//店铺删除
            if ($(this).hasClass('selected')) {
                $(this).parent().remove();
            }
        });
        return false;
    });
    //逐个宝贝删除
    $(".v3_c_del").click(function (event) {
        $(this).parents("li").remove();
        return false;
    });

    //显示购物车，删除等按钮
    $(".v3_collect_img").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });

    //收藏的宝贝选中效果
    $(".v3_c_gl_layer").click(function (event) {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            $(this).addClass('selected');
        }
    });

    //收藏的店铺选中效果
    $(".v3_dp_layer").click(function (event) {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            $(this).addClass('selected');
        }
    });

    //宝贝管理
    //宝贝管理 下拉选择宝贝分类
    $(".v3_g_s_main").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });
    $(".v3_g_s_main").find('.v3_g_s_hide p').click(function (event) {
        var _this = $(this),
			text = _this.text();
        _this.parent().parent().find('.v3_g_s_show span').text(text);
        _this.parent().parent().removeClass('selected');
    });

    //仓库中的宝贝排序
    $(".v3_m_sort").click(function (event) {
        if ($(this).hasClass('v3_m_up')) {
            $(this).removeClass('v3_m_up');
        } else {
            $(this).addClass('v3_m_up');
        }
    });

    //宝贝管理全选
    $('.v3_g_select_all').click(function (event) {
        var all = $('.v3_g_select_l');
        if ($(this).get(0).checked) {//全部选择
            $.each(all, function () {
                this.checked = "checked";
            });
        } else {
            $.each(all, function () {//全部取消
                this.checked = false;
            });
        }
    });

    //全选后 点击其他 选项，去掉全选选中状态
    $('.v3_g_select_l').click(function (event) {
        var all_1 = $('.v3_g_select_all'),
			all_2 = $('.v3_g_select_l'),
			len_2 = all_2.length,
			i = 0;
        if ($(this).get(0).checked) {//选中
            $.each(all_2, function () {
                if (this.checked === true)
                    i++;
            });
            if (i === len_2) {
                $.each(all_1, function () {
                    this.checked = "checked";
                });
            }
        } else {
            $.each(all_1, function () {//取消选中
                this.checked = false;
            });
        }
    });

    //下架
    $(".v3_g_xiajia").click(function (event) {
        var input = $(".v3_g_select_l:checked"),
			len = input.length;
        if (len === 0) {
            layerAlert("请选择宝贝下架，可以选择多个");
            return false;
        }
        layerConfirm("您确定要将宝贝下架，下架的宝贝将从列表中删除",
			function () {
			    input.each(function (index, el) {
			        $(this).parent().parent().remove();
			    });
			    layerAlert("下架成功");
			}
		);
        return false;
    });

    // 批量删除已下架的宝贝
    $(".v3_g_del").click(function (event) {
        var input = $(".v3_g_select_l:checked"),
			len = input.length;
        if (len === 0) {
            layerAlert("请选择宝贝删除，可以选择多个");
            return false;
        };
        layerConfirm("您确定要删除宝贝，删除的宝贝将从列表中删除",
			function () {
			    input.each(function (index, el) {
			        $(this).parent().parent().remove();
			    });
			}
		);
        return false;
    });

    // 批量上架宝贝
    $(".v3_g_shangjia").click(function (event) {
        var input = $(".v3_g_select_l:checked"),
			len = input.length;
        if (len === 0) {
            layerAlert("请选择宝贝上架，可以选择多个");
            return false;
        };
        for (var i = len - 1; i >= 0; i--) {
            if (input.eq(i).parent().parent().find(".v3_g_gaoqing").length) {
                layerAlert("告罄的宝贝不能被上架，请重新选择");
                return false;
            }
        };
        layerAlert("宝贝上架成功");
        return false;
    });

    //逐个上架操作
    $(".v3_goods_sj").click(function (event) {
        if ($(this).hasClass('v3_g_gaoqing')) {
            layerAlert("告罄的宝贝不能被上架了");
            return false;
        };
        layerAlert("宝贝上架成功");
        return false;
    });

    //收货地址
    //设为默认地址
    $(".v3_set_moren").click(function (event) {
        var text1 = "当前默认",
			text2 = "设为默认",
			that = $(this),
			text = that.text(),
			tr_p = that.parent().parent().parent(),
			tr_p_s = tr_p.siblings();
        if (text == text1) {
            return false;
        }
        that.text(text1);
        tr_p.addClass('v3_a_moren');
        tr_p_s.removeClass("v3_a_moren");
        tr_p_s.find('.v3_set_moren').text(text2);
        layerAlert("设置成功！");
        return false;
    });

    //设为默认地址
    $(".v3_address_del").click(function (event) {
        var tr_p = $(this).parent().parent().parent();
        layerConfirm("确定要删除地址吗", function () {
            tr_p.remove();
        });
        return false;
    });

    //新增地址
    $(".v3_address_add_btn a").click(function (event) {
        layer.open({
            type: 1,
            title: "添加新地址",
            skin: "",
            area: ['450px', '340px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {

            },
            yes: function (index, elem) {
                var selects = $(elem).find('.v3_add_address select'),
					inputs = $(elem).find('.v3_add_address input'),
					sheng = selects.eq(0).val(),
					shi = selects.eq(1).val(),
					qu = selects.eq(2).val(),
					xxdz = inputs.eq(0).val(),
					yzbm = inputs.eq(1).val(),
					sjr = inputs.eq(2).val(),
					lxdh = inputs.eq(3).val();
                alert(sheng + shi + qu);
                layer.close(index);
            },
            content: $(".v3_address_hide").html()
        });
        return false;
    });

    //修改地址
    $(".v3_a_modify").click(function (event) {
        layer.open({
            type: 1,
            title: "修改地址",
            skin: "",
            area: ['450px', '340px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {

            },
            yes: function (index, elem) {
                var text = $(elem).find(".v3_address_xx").val();
                alert(text);
                layer.close(index);
            },
            content: $(".v3_address_hide").html()
        });
        return false;
    });

    //购物车列表
    //结算
    var account = function () {
        var danjia = $('.unit_price'),
			shuliang = $('.v3_shop_num input'),
			total_w = $(".v3_total_price"),
			account_num = $(".v3_total_num"),
			num = 0,
			total_price = 0;
        for (var i = danjia.length - 1; i >= 0; i--) {
            var input = danjia.eq(i).parents("tr").find('.v3_s_goods');
            if (input.length > 0 && input.get(0).checked) {
                total_price += parseFloat(danjia.eq(i).text()) * 100 * parseInt(shuliang.eq(i).val()) / 100;
                num += parseInt(shuliang.eq(i).val());
            }
        };
        total_w.text(total_price);
        account_num.text(num);
    };
    //购物车 物品数量加减效果
    $(".v3_shop_num span").click(function (event) {
        var $input = $(this).siblings('input'),
			num = $input.val(),
			i = parseInt(num),
			tr = $(this).parents("tr"),
			danjia = tr.find('.unit_price').text(),
			zongjia = tr.find('td').eq(4),
			price = 0;
        if ($(this).index() == 0) {
            i--;
            i = i <= 1 ? "1" : i;
            $input.val(i);
            price = parseFloat(danjia) * 100 * i / 100;
            zongjia.text(price);
        } else {
            i++;
            $input.val(i);
            price = parseFloat(danjia) * 100 * i / 100;
            zongjia.text(price);
        }

        //计算总价
        account();
    });
    //购物车 物品数量输入
    $(".v3_shop_num input").keydown(function (event) {//键盘只能输入数字
        var str = event.keyCode || event.which;
        if ((str <= 105 && str >= 96) || (str <= 57 && str >= 48) || str == 8) {
            return true;
        } else {
            event.returnValue = false;
            return false;
        }
    });
    $(".v3_shop_num input").on('input change', function (event) {
        event.preventDefault();
        var type = /^[0-9]*[1-9][0-9]*$/,//判断是不是数字
			re = new RegExp(type);
        if ($(this).val().match(re) == null) {
            $(this).val("1");
        }
        account();
    });

    //购物车 商店选项
    $('.v3_s_shangjia').click(function (event) {
        var that = $(this),
			shop_main = that.parents("li"),
			shop_wp_s = shop_main.find('.v3_s_goods'),
			all_sj = $(".v3_s_shangjia"),
			len2 = all_sj.length,
			i2 = 0,
			shop_s_all = $(".v3_s_all");

        if (that.get(0).checked) {
            $.each(shop_wp_s, function () {//全选该店铺下的所有商品
                this.checked = "checked";
                $(this).parent().parent().addClass('selected');//为商品添加背景色
            });
            //判断是不是选中所有店铺
            $.each(all_sj, function () {
                if (this.checked === true) {
                    i2++;
                }
            });
            if (i2 === len2) {
                $.each(shop_s_all, function () {
                    this.checked = "checked";
                });
            } else {
                $.each(shop_s_all, function () {
                    this.checked = false;
                });
            }
        } else {
            $.each(shop_wp_s, function () {//取消全选该店铺下的所有商品
                this.checked = false;
                $(this).parent().parent().removeClass('selected');//为商品去掉背景色
            });
            $.each(shop_s_all, function () {
                this.checked = false;
            });
        }
        //计算总价
        account();
    });

    //购物车 物品选项
    $('.v3_s_goods').click(function (event) {
        var table = $(this).parents("table"),
			goods_len = table.find('.v3_s_goods').length,
			goods_s_len = table.find('.v3_s_goods:checked').length,
			shop_shangjia = $(this).parents("li").find(".v3_s_shangjia"),
			shop_s_all = $(".v3_s_all");
        if (this.checked) {
            $(this).parent().parent().addClass('selected');//为商品添加背景色
            if (goods_len === goods_s_len) { //所有物品选中
                shop_shangjia.get(0).checked = "checked";
                var shangjia_len = $(".v3_s_shangjia").length,
					shangjia_s_len = $(".v3_s_shangjia:checked").length;
                if (shangjia_len === shangjia_s_len) { //所有商家选中
                    $.each(shop_s_all, function () {
                        this.checked = "checked";
                    });
                } else {
                    $.each(shop_s_all, function () {
                        this.checked = false;
                    });
                }
            }
        } else {
            $(this).parent().parent().removeClass('selected');//为商品去掉背景色
            shop_shangjia.get(0).checked = false;//取消店铺全选
            $.each(shop_s_all, function () {//取消所有全选
                this.checked = false;
            });
        }
        //计算总价
        account();
    });

    //购物车全选
    $('.v3_s_all').click(function (event) {
        var all = $('.v3_s_all,.v3_s_goods,.v3_s_shangjia');
        if ($(this).get(0).checked) {//全部选择
            $.each(all, function () {
                this.checked = "checked";
                if ($(this).hasClass('v3_s_goods')) { //为商品添加背景色
                    $(this).parent().parent().addClass('selected');
                }
            });
        } else {
            $.each(all, function () {//全部取消
                this.checked = false;
                if ($(this).hasClass('v3_s_goods')) {
                    $(this).parent().parent().removeClass('selected');//为商品去掉背景色
                }
            });
        }
        //计算总价
        account();
    });

    //逐个删除购物车 商品
    $(".v3_shop_del").click(function (event) {
        var tr = $(this).parents("tr"),
		 	li = $(this).parents("li"),
			tr_len = tr.parent().find('tr').length;
        layerConfirm("您确定删除该宝贝吗？", function () {
            tr.remove();
            //计算总价
            account();
            if (tr_len === 1) {
                li.remove();
            }
        });
        return false;
    });

    //逐个删除购物车 商品
    $(".v3_s_del_btn").click(function (event) {
        var all = $('.v3_s_goods');
        layerConfirm("您确定删除该宝贝吗？", function () {
            $.each(all, function () {
                if (this.checked) {
                    var tr = $(this).parents("tr"),
				 	li = $(this).parents("li"),
					tr_len = tr.parent().find('tr').length;
                    tr.remove();
                    if (tr_len === 1) {
                        li.remove();
                    }
                };
            });
            //计算总价
            account();
        });
        return false;
    });

    //发布闲置
    //物品分类
    $(".v3_fabu_fenlei").click(function (event) {
        $(this).children('.v3_fabu_fenlei_box').addClass('xianshi');
    });
    $(".v3_fabu_fenlei").mouseleave(function (event) {
        $(this).children('.v3_fabu_fenlei_box').removeClass('xianshi');
    });
    $(".v3_fabu_fenlei_li").hover(function (event) {
        $(this).addClass('selected');
    }, function (event) {
        $(this).removeClass('selected');
    });
    $(".v3_fabu_fenlei_li a").click(function (event) {
        $(".v3_fabu_fenlei span").text($(this).text());
        $(this).parents(".v3_fabu_fenlei_li").removeClass('selected');
        //根据选择赋值
        // $(".v3_fabu_fenlei_box").attr("data-v1", $(this).parents(".v3_fabu_fenlei_li").attr("data-v"));
        // $(".v3_fabu_fenlei_box").attr("data-v2", $(this).attr("data-v"));
        return false;
    });

    //未有地址 显示选择
    if (!$(".v3_fabu_yxdz").length) {
        $(".v3_fabu_wxdz").css('display', 'block');
    };
    // 修改地址
    $(".v3_fabu_yxdz a").click(function (event) {
        var p = $(this).parent();
        p.css('display', 'none');
        p.siblings().css('display', 'block');
    });

    //发布闲置 删除图片效果
    $(".v3_f_imglist").on({
        mouseenter: function (event) {
            event.preventDefault();
            $(this).addClass('selected');
        },
        mouseleave: function (event) {
            event.preventDefault();
            $(this).removeClass('selected');
        }
    }, ".v3_f_imgbox");
    //删除
    $(".v3_f_imglist").on('click', '.v3_f_imgbox div', function (event) {
        event.preventDefault();
        $(this).parent().parent().remove();
    });


    // 订单详情 买家
    // 删除地址
    $(".v3_receipt_area").on('click', '.v3_r_area_del', function (event) {
        var that_p = $(this).parent();
        layerConfirm("确定要删除地址吗", function () {
            that_p.siblings().empty().append('亲，您还没有收货地址哦');
            that_p.empty();
        });
        return false;
    });

    //修改地址
    $(".v3_receipt_area").on('click', '.v3_r_area_alter', function (event) {
        event.preventDefault();
        layer.open({
            type: 1,
            title: "修改地址",
            skin: "",
            area: ['450px', '340px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {

            },
            yes: function (index, elem) {
                var text = $(elem).find(".v3_address_xx").val();
                alert(text);
                layer.close(index);
            },
            content: $(".v3_address_hide").html()
        });
        return false;
    });
    //使用其他地址
    $(".v3_dizhi_other").click(function (event) {
        layer.open({
            type: 1,
            title: "其它收货地址",
            skin: "",
            area: ['650px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {
                $(elem).find('.v3_receipt_dizhi').click(function (event) {
                    $(this).find('input').get(0).checked = "checked";
                });
            },
            yes: function (index, elem) {
                var input = $(elem).find('input:checked'),
					html;
                if (input.length > 0) {
                    var receipt = $(".v3_receipt_area .v3_receipt_dizhi");
                    receipt.empty();
                    input.nextAll().prependTo(receipt);
                }
                layer.close(index);
            },
            content: $(".v3_all_address").html()
        });
        return false;
    });
    //新增地址
    $(".v3_dizhi_new").click(function (event) {
        layer.open({
            type: 1,
            title: "添加新地址",
            skin: "",
            area: ['450px', '340px'],
            btn: ['确定', '取消'],
            success: function (elem, index) {

            },
            yes: function (index, elem) {
                var selects = $(elem).find('.v3_add_address select'),
					inputs = $(elem).find('.v3_add_address input'),
					sheng = selects.eq(0).val(),
					shi = selects.eq(1).val(),
					qu = selects.eq(2).val(),
					xxdz = inputs.eq(0).val(),
					yzbm = inputs.eq(1).val(),
					sjr = inputs.eq(2).val(),
					lxdh = inputs.eq(3).val();
                alert(sheng + shi + qu);
                layer.close(index);
            },
            content: $(".v3_address_hide").html()
        });
        return false;
    });

    var order_account = function () {
        var li = $(".v3_orderinfo_main li"),
			total_w = $(".v3_orderinfo_total span"),
			account_num = $(".v3_orderinfo_total b"),
			num = 0,
			total_price = 0;
        li.each(function (index, el) {//逐个计算
            var that = $(this),
				danjia = that.find('.unit_price'),
				shuliang = that.find('.v3_orderinfo_num input'),
				youfei = parseFloat(that.find('.v3_orderinfo_fare').text()) * 100 / 100 || 0,
				xiaoji = that.find('.v3_orderinfo_xj'),
				coupon_on = that.find('.coupon_on'),
				coupon_slt = that.find('.coupon_slt'),
				coupon = parseFloat(coupon_slt.val()) || 0,
				price = 0;
            for (var i = danjia.length - 1; i >= 0; i--) {
                price += parseFloat(danjia.eq(i).text()) * 100 * parseInt(shuliang.eq(i).val()) / 100 + youfei;
                num += parseInt(shuliang.eq(i).val());
            };
            coupon_on.is(":checked") && (price -= coupon);
            total_price += price;
            xiaoji.text(price);//小计
        });
        total_w.text(total_price);//总计
        account_num.text(num);
    };
    //是否选择使用现金券
    $(".coupon_on").on('click', function (event) {
        order_account();
    });
    //现金券下拉选择改变
    $('.coupon_slt').on('change', function (event) {
        order_account();
    });
    //订单详情 物品数量加减效果
    $(".v3_orderinfo_num span").click(function (event) {
        var $input = $(this).siblings('input'),
			num = $input.val(),
			i = parseInt(num),
			tr = $(this).parents("tr"),
			danjia = tr.find('.unit_price').text(),
			zongjia = tr.find('td').eq(4),
			price = 0;
        if ($(this).index() == 0) {
            i--;
            i = i <= 1 ? "1" : i;
            $input.val(i);
            price = parseFloat(danjia) * 100 * i / 100;
            zongjia.text(price);
        } else {
            i++;
            $input.val(i);
            price = parseFloat(danjia) * 100 * i / 100;
            zongjia.text(price);
        }

        //计算总价
        order_account();
    });
    //订单详情 物品数量输入
    $(".v3_orderinfo_num input").keydown(function (event) {//键盘只能输入数字
        var str = event.keyCode || event.which;
        if ((str <= 105 && str >= 96) || (str <= 57 && str >= 48) || str == 8) {
            return true;
        } else {
            event.returnValue = false;
            return false;
        }
    });
    $(".v3_orderinfo_num input").on('input change', function (event) {
        event.preventDefault();
        var type = /^[0-9]*[1-9][0-9]*$/,//判断是不是数字
			re = new RegExp(type);
        if ($(this).val().match(re) == null) {
            $(this).val("1");
        }
        order_account();
    });

    //待付款倒计时 选择支付方式

    // (function() {
    // 	var count_down = $(".v3_o_pay_djs i");
    // 	if (count_down.length > 0) {
    // 		var hours = parseInt(count_down.eq(0).text()),
    // 			minutes = parseInt(count_down.eq(1).text()),
    // 			seconds = parseInt(count_down.eq(2).text()),
    // 			h,
    // 			min,
    // 			sec,
    // 			c_t;
    // 		c_t = setInterval(function() {
    // 			seconds--;
    // 			if (seconds < 0) {
    // 				seconds = 59;
    // 				minutes--;
    // 			};
    // 			if(minutes<0){
    // 				minutes=59;
    // 				hours--;
    // 			}
    // 			if (hours <= 0 && minutes <= 0 && seconds === 0) {
    // 				alert("系统自动取消订单");
    // 				clearInterval(c_t);
    // 			}
    // 			sec = checkTime(seconds);
    // 			min = checkTime(minutes);
    // 			h = checkTime(hours);
    // 			count_down.eq(0).text(h);
    // 			count_down.eq(1).text(min);
    // 			count_down.eq(2).text(sec);
    // 		}, 1000);
    // 	}
    // })();

    //订单详情 待付款 倒计时
    // (function() {
    // 	var count_down = $(".v3_o_djs i");
    // 	if (count_down.length > 0) {
    // 		var hours = parseInt(count_down.eq(0).text()),
    // 			minutes = parseInt(count_down.eq(1).text()),
    // 			seconds = parseInt(count_down.eq(2).text()),
    // 			h,
    // 			min,
    // 			sec,
    // 			c_t;
    // 		c_t = setInterval(function() {
    // 			seconds--;
    // 			if (seconds < 0) {
    // 				seconds = 59;
    // 				minutes--;
    // 			};
    // 			if(minutes<0){
    // 				minutes=59;
    // 				hours--;
    // 			}
    // 			if (hours <= 0 && minutes <= 0 && seconds === 0) {
    // 				alert("系统自动取消订单");
    // 				clearInterval(c_t);
    // 			}
    // 			sec = checkTime(seconds);
    // 			min = checkTime(minutes);
    // 			h = checkTime(hours);
    // 			count_down.eq(0).text(h);
    // 			count_down.eq(1).text(min);
    // 			count_down.eq(2).text(sec);
    // 		}, 1000);
    // 	}
    // })();

    // 确认收货倒计时
    // (function() {
    // 	var count_down = $(".v3_o_djs_day i");
    // 	if(count_down.length>0){
    // 		var arr = count_down,
    // 			day = parseInt(arr.eq(0).text()),
    // 			hours = parseInt(arr.eq(1).text()),
    // 			minutes = parseInt(arr.eq(2).text()),
    // 			seconds = parseInt(arr.eq(3).text()),
    // 			d,
    // 			h,
    // 			min,
    // 			sec,
    // 			c_t;
    // 		c_t = setInterval(function() {
    // 			seconds--;
    // 			if (seconds < 0) {
    // 				seconds = 59;
    // 				minutes--;
    // 			};
    // 			if(minutes<0){
    // 				minutes=59;
    // 				hours--;
    // 			}
    // 			if(hours<0){
    // 				hours=23;
    // 				day--;
    // 			}
    // 			if (day === 0 && hours === 0 && minutes === 0 && seconds === 0) {
    // 				alert("活动结束");
    // 				clearInterval(c_t);

    // 			}
    // 			sec = checkTime(seconds);
    // 			min = checkTime(minutes);
    // 			h = checkTime(hours);
    // 			d = checkTime(day);
    // 			count_down.eq(0).text(d);
    // 			count_down.eq(1).text(h);
    // 			count_down.eq(2).text(min);
    // 			count_down.eq(3).text(sec);
    // 		}, 1000);
    // 	};	
    // })();

    var checkTime = function (i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    },
		countDown_dhms = function (obj, callback) {
		    var count_down = obj;
		    if (count_down.length > 0) {
		        var arr = count_down,
					day = parseInt(arr.eq(0).text()),
					hours = parseInt(arr.eq(1).text()),
					minutes = parseInt(arr.eq(2).text()),
					seconds = parseInt(arr.eq(3).text()),
					d,
					h,
					min,
					sec,
					c_t;
		        c_t = setInterval(function () {
		            seconds--;
		            if (seconds < 0) {
		                seconds = 59;
		                minutes--;
		            };
		            if (minutes < 0) {
		                minutes = 59;
		                hours--;
		            }
		            if (hours < 0) {
		                hours = 23;
		                day--;
		            }
		            if (day === 0 && hours === 0 && minutes === 0 && seconds === 0) {
		                if (callback != null && callback != '') {
		                    callback();
		                }
		                clearInterval(c_t);
		            }
		            sec = checkTime(seconds);
		            min = checkTime(minutes);
		            h = checkTime(hours);
		            d = checkTime(day);
		            count_down.eq(0).text(d);
		            count_down.eq(1).text(h);
		            count_down.eq(2).text(min);
		            count_down.eq(3).text(sec);
		        }, 1000);
		    };
		},
		countDown_hms = function (obj, callback) {
		    var count_down = obj;
		    if (count_down.length > 0) {
		        var hours = parseInt(count_down.eq(0).text()),
					minutes = parseInt(count_down.eq(1).text()),
					seconds = parseInt(count_down.eq(2).text()),
					h,
					min,
					sec,
					c_t;
		        c_t = setInterval(function () {
		            seconds--;
		            if (seconds < 0) {
		                seconds = 59;
		                minutes--;
		            };
		            if (minutes < 0) {
		                minutes = 59;
		                hours--;
		            }
		            if (hours <= 0 && minutes <= 0 && seconds === 0) {
		                if (callback != null && callback != '') {
		                    callback();
		                }
		                clearInterval(c_t);
		            }
		            sec = checkTime(seconds);
		            min = checkTime(minutes);
		            h = checkTime(hours);
		            count_down.eq(0).text(h);
		            count_down.eq(1).text(min);
		            count_down.eq(2).text(sec);
		        }, 1000);
		    };
		};

    //待付款倒计时 选择支付方式
    countDown_hms($(".v3_o_pay_djs i"), function () {
        alert("系统自动取消订单");
    });

    //订单详情 待付款 倒计时
    countDown_hms($(".v3_o_djs i"), function () {
        alert("系统自动取消订单");
    });

    // 确认收货倒计时
    countDown_dhms($(".v3_o_djs_day i"), function () {
        alert("活动结束");
    });

    //卖家处理退款申请倒计时 
    countDown_dhms($(".v3_refund_djs_day i"), function () {
        alert("退款成功");
    });

    //订单详情 付款成功 提醒发货
    $(".v3_o_m_txfh").click(function (event) {
        layerAlert("提醒发货成功");
        return false;
    });



    //确认收货 支付密码
    (function () {
        /**
		 * 获得光标位置兼容IE/FF
		 * 
		 例：
		    var obj = document.getElementById("tx1");
		        var pos = getCaretPosition(obj);
		        alert("--"+pos);
		*/
        function getCaretPosition(obj) {
            var result = 0;
            if (obj.selectionStart) { //IE以外 
                result = obj.selectionStart;
            } else { //IE 
                try {
                    var rng;
                    if (obj.tagName == "textarea") { //TEXTAREA 
                        rng = event.srcElement.createTextRange();
                        rng.moveToPoint(event.x, event.y);
                    } else { //Text 
                        rng = document.selection.createRange();
                    }
                    rng.moveStart("character", -event.srcElement.value.length);
                    result = rng.text.length;
                } catch (e) {
                    // throw new Error(10, "asdasdasd")
                }
            }
            return result;
        }
        /**
		 * 设置光标位置兼容IE/FF
		 * @param tObj
		 * @param sPos
		 * 
		 例：
		  var obj =document.getElementById("tx1");  
		        var sPos = obj.value.length-1;  
		        setCaretPosition(obj, sPos);  
		 */
        function setCaretPosition(tObj, sPos) {
            sPos = sPos || 6;//因为360极速sPos一直为0，为了兼容,所以加上
            if (tObj && sPos) {
                if (tObj.setSelectionRange) {
                    setTimeout(function () {
                        tObj.setSelectionRange(sPos, sPos);
                        tObj.focus();
                    }, 0);
                } else if (tObj.createTextRange) {
                    var rng = tObj.createTextRange();
                    rng.move('character', sPos);
                    rng.select();
                }
            }
        }
        var pwd = $(".six_pwd_input"),
			pwd_span = $(".sixDigitPassword span"),
			pwd_i = $(".sixDigitPassword i"),
			pwd_b = $(".sixDigitPassword b"),
			c_pos = 0;
        var sixPwdCheck = function () {
            var len = pwd.val().length;
            if (len < 7) {
                pwd_span.addClass("active");
                if (len === 6) {
                    pwd_span.css("left", (len - 1) * 30);
                    pwd_i.removeClass('active');
                } else {
                    pwd_span.css("left", len * 30);
                    pwd_i.eq(len).addClass("active").siblings('i').removeClass('active');
                }
                pwd_b.removeClass('prev');
                for (var i = len - 1; i >= 0; i--) {
                    pwd_b.eq(i).addClass('prev');
                };
            }
        };
        pwd_i.click(function (event) {
            pwd.trigger('focus');
        });
        pwd.on("keydown input", function (event) {
            sixPwdCheck();
        });
        pwd.on("keyup", function (event) {
            sixPwdCheck();
            c_pos = getCaretPosition(this);
        });
        pwd.focus(function (event) {
            setCaretPosition(this, c_pos);
            sixPwdCheck();
        });
        pwd.blur(function (event) {
            pwd_span.removeClass('active');
            pwd_i.removeClass('active');
        });

    })();

    // 附近闲置
    $(".v3_fjxz_nav a").click(function (event) {
        var that = $(this),
		    i = that.parent().index();
        that.parent().parent().find('a').removeClass('selected');
        that.addClass('selected');
        $(".v3_fjxz_list").css('display', 'none').eq(i).css('display', 'block');
        return false;
    });


    //消息删除按钮
    $(".v3_news_del").click(function (event) {
        var parent_li = $(this).parent().parent();
        layerConfirm("您确定要删除该消息吗？", function () {
            parent_li.remove();
        });
    });




    //闲置物品 详情
    //收藏效果
    $(".v3_b_collect").click(function (event) {
        var b = $(this).find('b'),
		    i = $(this).find('i');
        if (b.hasClass('selected')) {
            b.removeClass('selected');
            i.text("收藏宝贝");
        } else {
            b.addClass('selected');
            i.text("已收藏");
        };
    });
    //数量加减效果
    $(".v3_buy_num span").click(function (event) {
        var $input = $(this).siblings('input'),
			num = $input.val(),
			i = parseInt(num);
        if ($(this).index() == 0) {
            i--;
            i = i <= 1 ? "1" : i;
            $input.val(i);
        } else {
            i++;
            $input.val(i);
        }
    });

    //加入购物车效果
    var add_cart_mian = $(".add_cart_mian");
    if (add_cart_mian.length) {
        var first_img = $(".botin img").eq(0).clone();
        add_cart_mian.append(first_img);
    };
    var add_cart = function (event) {
        var that = $(this),
			cart = $(".top_cart"),
			o = that.offset(),
		 	c_o = cart.offset(),
		 	scroll_top = $(document).scrollTop(),
		 	c_l = c_o.left,
		 	c_t = c_o.top - scroll_top,
		 	l = o.left,
		 	t = o.top;
        that.off("click");//禁用点击事件
        add_cart_mian.length && add_cart_mian.css({
            "left": l,
            "top": t - scroll_top,
            "width": "80px",
            "height": "80px",
            "display": "block"
        }).stop().animate({
            "left": c_l,
            "top": c_t,
            "width": "20px",
            "height": "20px"
        },
           1000, "easeOutExpo", function () {
               $(this).fadeOut('100');
           });
        setTimeout(function () {
            var shu = cart.siblings('i').eq(0);
            shu.text(parseInt(shu.text() || 0) + 1);
            that.click(add_cart);
        }, 1000);
        return false;
    }
    $(".v3_add_cart").click(add_cart);

    // 放大镜效果
    ! function () {
        if ($(".boxin").length === 0) {
            return;
        };
        var zoom_img = $(".boxin"), //要放大图片的外框
			img = zoom_img.find('img'), //要放大的图片
			zoom_main = $(".pic"), //放大区域
			zoom_big_img = zoom_main.find('div'), //被放大图片的外框
			img_big = zoom_big_img.find('img'), //被放大的图片
			zoom_layer = zoom_img.find('div'), //移动层
			z_w = zoom_img.width(), //宽
			z_h = zoom_img.height(), //高
			zoom_img_left = zoom_img.offset().left,
			zoom_img_top = zoom_img.offset().top;

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
        img.load(function () { //大图垂直水平居中
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

        $(".botin img").load(function () { //所有列表图片垂直水平居中
            img_v_c($(this));
        });
        window.onload = function () {
            reset_zoom(img, img_big); //大图垂直水平居中
            $.each($(".botin img"), function (index, val) { //所有列表图片垂直水平居中
                img_v_c($(this));
            });
        }

        var num = 0,
			buy_img_ul = $(".v3_buy_left .box ul"),
			buy_img_li = buy_img_ul.find('li'),
			W = buy_img_li.outerWidth(true);

        function fun_right() {
            if (num == buy_img_li.length - 5) {
                num = buy_img_li.length - 5;

            } else {
                num++;
                buy_img_ul.stop().animate({
                    "left": -num * W
                }, 300);
            }
        }

        function fun_left() {
            if (num == 0) {
                num = 0;
            } else {
                num--;
                buy_img_ul.stop().animate({
                    "left": -num * W
                }, 300);
            }
        }
        buy_img_li.mouseover(function (event) {
            $(this).addClass('current').siblings('li').removeClass('current')
            var src = $(this).children('img').attr('src');
            zoom_img.find("img").remove();
            zoom_img.append("<img src='" + src + "'/>");
            zoom_big_img.find("img").remove();
            zoom_big_img.append("<img src='" + src + "'/>");
            reset_zoom(zoom_img.find("img"), zoom_big_img.find("img"));
        });
        var li_length = buy_img_li.length * W;
        buy_img_ul.css('width', li_length);
        // 右点击
        $(".v3_buy_left .bot .right").click(function (event) {
            fun_right();
        });
        // 左点击
        $(".v3_buy_left .box .left").click(function (event) {
            fun_left();
        });
        //左键盘事件
        $(document).keydown(function (event) {
            if (event.keyCode == 39) {
                fun_right();
            } else if (event.keyCode == 37) {
                fun_left();
            }
        });

        if (buy_img_li.length < 5) {
            buy_img_ul.css('position', 'static');
        };

    }();


    ////闲置列表 =======李杨注释-2015-11-13 pc3.1无需这些======
    //$(".v3_sort_list li a").click(function (event) {
    //    var p = $(this).parent();
    //    p.addClass('selected').siblings().removeClass('selected');
    //    return false;
    //});

    ////全部、个人、商家 选择
    //$(".v3_sort_tab a").click(function (event) {
    //    var p = $(this).parent(),
    //		p_p = p.parent(),
    //		i = p.index();
    //    p_p.find('a').removeClass('selected');
    //    $(this).addClass('selected');
    //    $(".v3_xz_list").css('display', 'none').eq(i).css('display', 'block');
    //});

    ////默认排序 发布时间 价格
    //$(".v3_sort_paixu a").click(function (event) {
    //    $(this).addClass('selected').siblings().removeClass('selected');
    //    if ($(this).find(".arrow").hasClass('up')) {
    //        $(this).find(".arrow").removeClass("up").addClass("down");
    //    } else {
    //        $(this).find(".arrow").removeClass("down").addClass("up");
    //    }
    //    return false;
    //});

    //列表每排第四个 margin-right 为 0
    $(".v3_xz_list li").each(function (index, el) {
        if (($(this).index() + 1) % 4 === 0) {
            $(this).css('marginRight', 0);
        };
    });



});
