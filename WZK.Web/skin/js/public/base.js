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
            _isFadeIn: false,
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
			$parent = _this.parent(),
			btn_left = $parent.find(_btn_left),
			btn_right = $parent.find(_btn_right),
			node_ul = _this.find(_swing_pic),
			node_li = node_ul.find("li"),
			node_list = jQuery(_swing_list),
			node_li_nav = node_list.find("li"),
			li_len = node_li.length,
			bgColor = '',
			nav_html = "";
        for (var i = 0; i < li_len; i++) {
            nav_html += "<li></li>";
        };
        node_list.empty().html(nav_html);
        node_li_nav = node_list.find("li");
        node_li_nav.eq(_index - 1).addClass(_selected);
        // list_width = node_list.css('width'),
        // margin_right = node_li_nav.css('margin-right');
        opts._isFadeIn || node_ul.css("width", _dis * li_len);
        // node_li_nav.css('width', (parseInt(list_width) - (li_len - 1) * parseInt(margin_right)) / li_len);
        // node_li_nav.eq(-1).css('margin-right', 0);
        var _start_left = node_ul.css("left");
        var _timer = setInterval(show, _time);
        init();
        function init() {
            if (opts._isFadeIn) {
                _index = 0;  // 把第几页设置为默认下标。 方便操作。	
                node_li.css({ "position": 'absolute' });
                node_li.eq(_index).siblings().css("display", "none");
            }
            btn_left.add(btn_right).mouseover(function () {
                _timer = clearInterval(_timer);
                $(this).addClass('selected')
            }).mouseout(function () {
                _timer = setInterval(show, _time);
                $(this).removeClass('selected')
            }).hide();
            btn_left.click(function (event) {
                left();
            });
            btn_right.click(function (event) {
                show();
            });
            node_ul.mouseover(function () {
                _timer = clearInterval(_timer);
            }).mouseout(function () {
                _timer = setInterval(show, _time);
            });
            $parent.hover(function () {
                btn_left.add(btn_right).show();
            }, function () {
                btn_left.add(btn_right).hide();
            });
            // node_ul.parent().parent().mouseenter(function() {
            // 	node_list.addClass('selected');
            // }).mouseleave(function() {
            // 	node_list.removeClass('selected');
            // });

            if (opts._isFadeIn) {
                node_li_nav.click(function () {
                    var _thisIndex = jQuery(this).index();
                    _index = _thisIndex;
                    node_li.stop(true, true).eq(_thisIndex).fadeIn(1000).siblings().fadeOut(1000);
                    node_li_nav.eq(_thisIndex).addClass(_selected).siblings().removeClass(_selected);
                    _timer = clearInterval(_timer);
                });
            } else {
                node_li_nav.mouseover(function () {
                    var thisIndex = $(this).index();
                    _index = thisIndex + 1;
                    node_li_nav.eq(thisIndex).addClass(_selected).siblings().removeClass(_selected);
                    _left = -_dis * (thisIndex);
                    node_ul.stop(true, true).animate({ "left": _left }, _speed, _easing);
                    _timer = clearInterval(_timer);
                }).mouseout(function () {
                    _timer = setInterval(show, _time);
                });
            }
        }

        function show() {//right
            if (opts._isFadeIn) {
                if (_index >= node_li.length - 1) {
                    _index = 0;
                } else {
                    _index++;
                }
                node_li.stop(true, true).eq(_index).fadeIn(1000).siblings().fadeOut(1000);
                node_li_nav.eq(_index).addClass(_selected).siblings().removeClass(_selected);
            } else {
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
        }
        function left() {//left
            if (opts._isFadeIn) {
                if (_index <= 0) {
                    _index = node_li.length - 1;
                } else {
                    _index--;
                }
                node_li.stop(true, true).eq(_index).fadeIn(1000).siblings().fadeOut(1000);
                node_li_nav.eq(_index).addClass(_selected).siblings().removeClass(_selected);
            } else {
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

var buildEvent = function (obj, e) {
    var that = this;
    that.list = {};
    $(obj).on(e, '[data-' + e + ']', function (event) {
        var d = $(this).data(e);
        if (that.list[d].call(this, event) === false) {
            return false;
        };
    });
};

//用于表单验证
(function () {
    // 策略对象
    var strategys = {
        required: function (value, errorMsg) {//是否必填
            if (value === '') {
                return errorMsg;
            }
        },
        // 限制最小长度
        minLength: function (value, length, errorMsg) {
            if (value.length < length) {
                return errorMsg;
            }
        },
        // 限制最大长度
        maxLength: function (value, length, errorMsg) {
            if (value.length > length) {
                return errorMsg;
            }
        },
        // 手机号码格式
        mobile: function (value, errorMsg) {
            if (!/^0?(13[0-9]|15[0-9]|17[0-9]|18[0-9]|14[0-9])[0-9]{8}$/.test(value)) {//   /^0?(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$/
                return errorMsg;
            }
        },
        //密码验证
        password: function (value, errorMsg) {
            if (!/^[\w\\\[\]\:\^\-~!@#$%&*()-+={}|;'",.\/<>?]{6,16}$/.test(value)) {
                return errorMsg;
            }
        },
        //重复密码验证
        equal: function (value, selector, errorMsg) {
            if (value !== $.trim($(selector).val())) {
                return errorMsg;
            }
        },
        //验证邮箱
        email: function (value, errorMsg) {
            if (!/^[\w.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}$/.test(value)) {
                return errorMsg;
            }
        }
    };
    var Validator = function () {
        this.cache = []; // 保存校验规则
    };
    Validator.prototype.add = function (dom, rules, errfunc) {//添加验证
        var self = this;

        for (var i = 0, rule; rule = rules[i++];) {
            (function (rule) {
                var strategy_arr = rule.strategy;
                var errorMsg = rule.errorMsg;

                self.cache.push({
                    dom: dom,//验证的dom对象
                    errfunc: errfunc || function () { },//错误后执行的方法
                    checkfunc: function () {
                        var strategyAry = strategy_arr.split(":"),
							strategy = strategyAry.shift(),
							value = dom.value;
                        value = typeof value == "string" ? value.trim ? value.trim() : value.replace(/^\s+|\s+$/gm, "") : "";
                        strategyAry.unshift(value);
                        strategyAry.push(errorMsg);
                        return strategys[strategy].apply(dom, strategyAry);//执行验证函数 strategyAry 第一个参数为dom.value 最后一个参数为错误信息
                    }
                });
            })(rule);
        }
    };
    Validator.prototype.start = function () {//开始验证
        for (var i = 0, j; j = this.cache[i++];) {
            var msg = j.checkfunc(); // 开始校验 并取得校验后的返回信息
            if (msg) {
                return j.errfunc(j.dom, msg), msg;
            }
        }
    };
    Validator.prototype.newFlag = function () {
        var g = "";
        var i = 32;
        while (i--) {
            g += Math.floor(Math.random() * 16.0).toString(16);
        }
        return g;
    }
    Validator.prototype.addRulesByAttr = function (elem, errfunc) {
        var self = this;
        var elem = elem,
			errfunc = errfunc || function () { },
			validate = elem.getAttribute("data-validate"),
			msg = elem.getAttribute("data-msg"),
			strategys = validate && validate.split("|") || [],
			errorMsgs = msg && msg.split("|") || [],
			rules = [];
        for (var i = 0; i < strategys.length; i++) { //遍历增加校验规则
            rules.push({
                strategy: strategys[i],
                errorMsg: errorMsgs[i]
            });
        };
        self.add(elem, rules, errfunc);
    };
    window.Validator = Validator;
})();

//函数节流  fn运行的方法，delay时间内连续调用函数，后一个调用会把前一个调用的等待处理掉，但每隔runDelay时间之久会至少执行一次
function throttle(fn, delay, runDelay) {
    var timer = null;
    var t_start;
    return function () {
        var context = this,
			args = arguments,
			t_cur = new Date();
        timer & clearTimeout(timer);
        if (!t_start) {
            t_start = t_cur;
        }
        if (t_cur - t_start >= runDelay) {
            fn.apply(context, args);
            t_start = t_cur;
        } else {
            timer = setTimeout(function () {
                fn.apply(context, args);
            }, delay);
        }
    }
}

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
// var layerOpt={
// 	titleColor:"color:#f00;"
// };
var layerAlert = function (msg, v3_time, callback) {//msg消息；v3_time关闭时间，可不填，表示不关闭；callback关闭后回调；v3_time和callback没有先后之分
    if (typeof (v3_time) == "function") {
        callback = [v3_time, v3_time = callback][0];
    };
    layer.open({
        type: 1,
        title: 0,
        closeBtn: 0,
        skin: "",
        btn: ['确定'],
        time: v3_time || 0,
        success: function (elem, index) {
        },
        end: function () {
            callback && callback();
        },
        content: '<div class="v3_layer_alert">' + msg + '</div>'
    });
     },
     layerErrorMsg = function (msg, time) {
         layer.msg(msg, { icon: 2, time: time || 1000 });
     },
    layerSuccessMsg = function (msg, time) {
        layer.msg(msg, { icon: 1, time: time || 1000 });
    },
	layerMsg = function (msg, v3_time, callback) {//msg消息；v3_time关闭时间，默认三秒即3000；callback关闭后回调；v3_time和callback没有先后之分
	    if (typeof (v3_time) == "function") {
	        callback = [v3_time, v3_time = callback][0];
	    };
	    layer.open({
	        type: 1,
	        title: 0,
	        closeBtn: 0,
	        skin: "layer_msg",
	        //btn: ['确定'],
	        time: v3_time || 3000,
	        success: function (elem, index) {
	        },
	        end: function () {
	            callback && callback();
	        },
	        content: msg
	    });
	},
	layerWarmAlert = function (msg, wtitle, v3_time) {
	    layer.open({
	        type: 1,
	        title: [wtitle, "" + "color:#f00;"],
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
	            var doc = document,
					activeElem = doc.activeElement || {},
					tagN = activeElem.tagName || '';
	            if (tagN.toLowerCase() == 'a') {
	                activeElem.blur();//防止a标签点击后聚焦，按确定键后触发click事件
	            }
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
			        opt.yes && opt.yes(index, elem) !== false && layer.close(index);
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
			    shade: opt.shade,
			    skin: opt.skin || "",
			    success: function (elem, index) {
			        var doc = document,
						activeElem = doc.activeElement || {},
						tagN = activeElem.tagName || '';
			        if (tagN.toLowerCase() == 'a') {
			            activeElem.blur();//防止a标签点击后聚焦，按确定键后触发click事件
			        }
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

var cayica = window.cayica || {};
cayiWZK.layerMsg = function (msg, time, callback) { //msg消息；time关闭时间(单位为秒)，默认三秒；callback关闭后回调；time和callback没有先后之分
    if (typeof time == "function") {
        callback = [time, time = callback][0];
    };
    layer.open({
        type: 1,
        title: 0,
        closeBtn: 0,
        skin: "layer_msg",
        shift: 5,//动画类型
        //btn: ['确定'],
        time: time * 1000 || 1000,
        success: function (elem, index) { },
        end: function () {
            callback && callback();
        },
        content: msg
    });
},
	cayiWZK.layerConfirm = function (msg, yesCallback, noCallback) { //msg消息；yesCallback确定按钮回调；noCallback取消按钮回调，返回false时不关闭弹出层
	    var isobj = Object.prototype.toString.call(msg) == '[object Object]',
			opt = {},
			btn;
	    if (isobj) {
	        opt = msg;
	        msg = opt.msg;
	        yesCallback = opt.yes;
	        noCallback = opt.cancel;
	        btn = opt.btn;
	    }
	    layer.open({
	        type: 1,
	        title: "&nbsp;",
	        skin: "",
	        shift: 5, //动画类型
	        btn: btn || ['确定', '取消'],
	        success: function (elem, index) {
	            var doc = document,
					activeElem = doc.activeElement || {},
					tagN = activeElem.tagName || '';
	            if (tagN.toLowerCase() == 'a') {
	                activeElem.blur(); //防止a标签点击后聚焦，按确定键后触发click事件
	            }
	        },
	        yes: function (index, elem) {
	            yesCallback && yesCallback(index, elem);
	            layer.close(index);
	        },
	        cancel: function (index) {
	            return noCallback && noCallback(index);
	        },
	        content: '<div class="v3_layer_alert">' + msg + '</div>'
	    });
	},
	cayiWZK.layerAlert = function (msg, time, callback) { //msg消息；time关闭时间(单位为秒)，可不填，表示不关闭；callback关闭后回调；time和callback没有先后之分
	    if (typeof (time) == "function") {
	        callback = [time, time = callback][0];
	    };
	    layer.open({
	        type: 1,
	        title: 0,
	        shift: 5, //动画类型
	        closeBtn: 0,
	        skin: "",
	        btn: ['确定'],
	        time: time * 1000 || 0,
	        success: function (elem, index) { },
	        end: function () {
	            callback && callback();
	        },
	        content: '<div class="v3_layer_alert">' + msg + '</div>'
	    });
	},
	cayiWZK.layerPage = function (opt) {
	    opt = opt || {};
	    var arr = ["btn", "yes", "cancel"],
			json = {
			    btn: opt.btn || ['确定', '取消'],
			    yes: function (index, elem) {
			        opt.yes && opt.yes(index, elem) !== false && layer.close(index);
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
			    shade: opt.shade,
			    closeBtn: opt.closeBtn,//1 2 0不显示
			    skin: opt.skin || "",
			    shift: 5, //动画类型
			    success: function (elem, index) {
			        var doc = document,
						activeElem = doc.activeElement || {},
						tagN = activeElem.tagName || '';
			        if (tagN.toLowerCase() == 'a') {
			            activeElem.blur(); //防止a标签点击后聚焦，按确定键后触发click事件
			        }
			        $(elem).on('click', '.main_layer_box .close', function (event) {//关闭弹出层
			            layer.close(index);
			        });
			        opt.success && opt.success(elem, index);
			    },
			    area: [opt.width || 'auto', opt.height || 'auto'],
			    content: opt.html || ""
			};
	    for (var i = arr.length - 1; i >= 0; i--) {
	        opt[arr[i]] && (now_json[arr[i]] = json[arr[i]]);
	    };
	    layer.open(now_json);
	};

// 页面布局写法
// <div class="count" data-targettime="1443153882000" data-servertime="1442900235282"></div>
// countDown(".count",function(){
// 	alert(123456);
// });

//懒加载方法
cayiWZK.lazy = function (opt) {
    opt = opt || {};
    var json = {
        data_attribute: "src",
        threshold: 0,
        event: "scroll",
        failure_limit: 0,
        container: window,
        placeholder: opt.placeholder || $("#product_lazy").val() || !1
    };
    if (!$.fn.lazyload) {
        return;
    }
    switch (opt.index) {
        default:
        case 1: //首页产品图片 懒加载
            //if ($("#product_lazy").length) {
                $(opt.className || ".lazy_img").lazyload($.extend(json, opt));
            //};
            break;
        case 2: //用户头像
            //if ($("#user_lazy").length) {
                json.placeholder = opt.placeholder || $("#user_lazy").val();
                $(opt.className || ".lazy_photo").lazyload($.extend(json, opt));
            //};
            break;
    }
};

$(function () {
    //公共 首页
    //顶部  城市定位 我的擦一擦 手机擦一擦
    $(".v3_top_main .t_right_m,.v3_top_main .t_left_m").hover(function () {
        $(this).addClass('selected');
    }, function () {
        $(this).removeClass('selected');
    });

    // 滚动内容 擦哥头条
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
    scroll_list(".newsList ul", ".newsList li", 5); //擦哥头条
    // scroll_list("#announcement","#announcement li",4); //公告
    // scroll_list("#safety","#safety li",4);//安全
    // scroll_list("#deal","#deal li",5);// 正在交易

    // 顶部固定
    var v3_top_wrap = $(".v3_top_wrap");
    if (v3_top_wrap.length > 0) {
        v3_top_wrap.fixedTop({
            startline: 0,
            top: 0,
            z_index: 150
        });
    }

    //首页图片 懒加载
    // if($("#lazy_img").length){
    // 	$("img").lazyload({
    // 		data_attribute:"src",
    // 		threshold : 200,
    // 		placeholder:"skin/images/c2.png"
    // 	});
    // };

    //收藏擦一擦
    // $(".t_cayica").click(function(event) {
    // 	AddFavorite(window.location,document.title);
    // 	return false;
    // });
    //点击搜索
    // $(".search_btn").click(function () {
    // 	var key = $(this).val();
    // 	if (key == "") {
    // 		alert("请输入搜索关键字！");
    // 	} else {
    // 		location.href = "/search-66-0-0-0-"+encodeURIComponent(key)+"-1-1.html";
    // 	}
    // });

    //闲置列表 全部 个人 商家 tab栏 固定顶部
    // $(".v3_sort_tab_wrap").fixedTop({
    // 	cankao:".v3_container"
    // });

    //首页搜索选择
    // $(".search_slt span").on('click', function(event) {
    // 	$(this).addClass('selected').siblings().removeClass('selected');
    // });
    //其他页面左侧导航栏
    // $(".v3_nav_all").hover(function() {
    // 	$(this).addClass('v3_nav_show');
    // }, function() {
    // 	$(this).removeClass('v3_nav_show');
    // });
    // $(".v3_nav_all").mouseenter(function(event) {
    // 	$(this).addClass('v3_show_nav');
    // 	return false;
    // });
    // $(".v3_nav_all").mouseleave(function(event) {
    // 	$(this).removeClass('v3_show_nav');
    // });
    //导航栏
    // $(".v3_side_nav li").hover(function() {
    // 	if($(this).next().length)
    // 		$(this).addClass('selected');
    // }, function() {
    // 	$(this).removeClass('selected');
    // });
    //区域
    // $(".v3_quyu_w li").hover(function() {
    // 	if ($(this).next().length == 0) {
    // 		$(this).find('.v3_quyu_hide').css({
    // 			"left": "auto",
    // 			"right": '0'
    // 		});
    // 	};
    // 	$(this).addClass('v3_show');
    // }, function() {
    // 	$(this).removeClass('v3_show');
    // });
    //信誉商家
    // $(".v3_merchant li").hover(function() {
    // 	$(this).addClass('selected');
    // }, function() {
    // 	$(this).removeClass('selected');
    // });

    /**********公告 安全***********/
    // $(".v3_gonggao_nav a").click(function(event) {
    // 	var i=$(this).index();
    // 	$(this).addClass('selected').siblings().removeClass('selected');
    // 	$(".v3_gonggao_list ul").eq(i).show().siblings().hide();
    // 	return false;
    // });

    // 右侧客服
    // var v3_sidebar = $(".v3_sidebar");
    // if (v3_sidebar.length > 0) {
    // 	var sidebar_top=($(window).height()-v3_sidebar.height())/2;
    // 	v3_sidebar.fixedTop({
    // 		is_right: true,
    // 		startline: 0,
    // 		top: sidebar_top
    // 	});
    // }

    // $(".v3_kefu").hover(function() {
    // 	$(this).addClass('selected');
    // }, function() {
    // 	$(this).removeClass('selected');
    // });
    //回到顶部
    // $(".v3_to_top").click(function() {
    // 	$('body,html').animate({
    // 		scrollTop: 0
    // 	});
    // 	return false;
    // });




    // 个人中心 导航栏效果
    // $(".v3_m_nav_main li").click(function(event) {
    // 	$(this).addClass('selected').siblings().removeClass('selected');
    // });
    //个人中心 左侧 红色效果
    // $(".v3_m_sub_nav p").click(function(event) {
    // 	var _this=$(this);
    // 	_this.parent().parent().find("p").removeClass('selected');
    // 	_this.addClass('selected');
    // 	_this.siblings('.v3_m_nav_h').addClass('selected');
    // });

    // var revisedPrice = function(yesCallback) { //修改价格函数 参数是点击“确定”按钮要执行的函数回调
    // 		layer.open({
    // 			type: 1,
    // 			title: ["<i></i>请您在与买家达成协议一致的情况下修改价格", "color:#f00;"],
    // 			skin: "v3_r_p_wrap",
    // 			area: ['735px', '300px'],
    // 			btn: ['确定', '取消'],
    // 			success: function(elem, index) {
    // 				var inputs = $(elem).find('.v3_r_p_input input');
    // 				inputs.on('change keyup', function(event) {
    // 					var zongjia = parseFloat(inputs.eq(0).val()) * 100 || 0,
    // 						yunfei = parseFloat(inputs.eq(1).val()) * 100 || 0;
    // 					inputs.eq(2).val((zongjia + yunfei) / 100);
    // 				});
    // 			},
    // 			yes: function(index, elem) {
    // 				if (yesCallback != null && yesCallback != '') {
    // 					yesCallback(index, elem);
    // 				}
    // 				layer.close(index);
    // 			},
    // 			content: $(".v3_revised_price_hide").html()
    // 		});
    // 	},
    // 	closeTrade=function(yesCallback) { //关闭交易函数 参数是点击“确定”按钮要执行的函数回调
    // 		layer.open({
    // 			type: 1,
    // 			title: ["<i></i>请您在与买家达成协议一致的情况下关闭交易", "color:#f00;"],
    // 			skin: "v3_c_t_wrap",
    // 			area: ['370px', '200px'],
    // 			btn: ['确定', '取消'],
    // 			success: function(elem, index) {

    // 			},
    // 			yes: function(index, elem) {
    // 				if (yesCallback != null && yesCallback != '') {
    // 					yesCallback(index, elem);
    // 				}
    // 				layer.close(index);
    // 			},
    // 			content: $(".v3_close_trade_hide").html()
    // 		});
    // 	};

    //卖家个人中心 等待买家付款 修改价格
    // $(".v3_order_xgjg").click(function(event) {
    // 	revisedPrice(function(index,elem){// elem 是弹出层dom对象
    // 		var inputs=$(elem).find('.v3_r_p_input input');
    // 		var zongjia=inputs.eq(0).val(),
    // 			yunfei=inputs.eq(1).val(),
    // 			shifu=inputs.eq(2).val();
    // 		alert("总价："+zongjia+"运费："+yunfei+"实付："+shifu);
    // 	});
    // 	return false;
    // });

    //卖家个人中心 等待买家付款 关闭交易
    // $(".v3_order_gbjy").click(function(event) {
    // 	closeTrade(function(index,elem){
    // 		var selects=$(elem).find('select');
    // 		alert(selects.val());
    // 	});
    // 	return false;
    // });

    //卖家订单详情 修改价格
    // $(".v3_o_m_xgjg").click(function(event) {
    // 	revisedPrice(function(index,elem){
    // 		var inputs=$(elem).find('.v3_r_p_input input');
    // 		var zongjia=inputs.eq(0).val(),
    // 			yunfei=inputs.eq(1).val(),
    // 			shifu=inputs.eq(2).val();
    // 		alert("总价："+zongjia+"运费："+yunfei+"实付："+shifu);
    // 	});
    // 	return false;
    // });

    //卖家订单详情 关闭交易
    // $(".v3_o_m_gbjy").click(function(event) {
    // 	closeTrade(function(index,elem){
    // 		var selects=$(elem).find('select');
    // 		alert(selects.val());
    // 	});
    // 	return false;
    // });

    //发货弹出层函数
    // var Deliver = function() {
    // 	layer.open({
    // 		type: 1,
    // 		title: "&nbsp;",
    // 		skin: "v3_fahuo_wrap",
    // 		area: ['750px', '550px'],
    // 		btn: ['确定', '取消'],
    // 		success: function(elem, index) {
    // 			// var all_li = $(elem).find('.v3_fahuo_m li');
    // 			// var kuaidi_list=$(elem).find(".v3_qtkd_list"),
    // 			// 	kdgs_input=$(elem).find('.v3_fahuo_kdgs input'),
    // 			// 	danhao = $(elem).find('.v3_fahuo_kddh input').eq(1),
    // 			// 	t;
    // 			// kdgs_input.on("keyup",function(event) {//快递公司输入框，改变内容显示其他快递公司列表
    // 			// 	var text=$(this).val();
    // 			// 	kuaidi_list.show();
    // 			// });
    // 			// kdgs_input.on('blur', function(event) {
    // 			// 	t=setTimeout(function(){
    // 			// 		kuaidi_list.hide();
    // 			// 	},2000);
    // 			// });
    // 			// kdgs_input.on('focus', function(event) {
    // 			// 	clearTimeout(t);
    // 			// });
    // 			// kuaidi_list.find('dd').on('click', function(event) {// 点击快递后 影藏列表
    // 			// 	var text=$(this).text();
    // 			// 	kdgs_input.val(text);
    // 			// 	kuaidi_list.hide();
    // 			// 	clearTimeout(t);
    // 			// });
    // 			// danhao.on('focus', function(event) {//快递单号 聚焦后 影藏列表
    // 			// 	kuaidi_list.hide();
    // 			// 	clearTimeout(t);
    // 			// });
    // 			// all_li.click(function(event) {
    // 			// 	if ($(this).index() === 9) {
    // 			// 		$(elem).find('.v3_fahuo_kdgs').css('display', 'block');
    // 			// 	} else {
    // 			// 		$(elem).find('.v3_fahuo_kdgs').css('display', 'none');
    // 			// 	}
    // 			// 	if ($(this).hasClass('v3_fahuo_shsm') && $(this).hasClass('selected')) {
    // 			// 		$(this).removeClass('selected');
    // 			// 		$(elem).find('.v3_fahuo_layer').css('display', 'none');
    // 			// 		return;
    // 			// 	} else if ($(this).hasClass('v3_fahuo_shsm')) {
    // 			// 		$(elem).find('.v3_fahuo_layer').css('display', 'block');
    // 			// 	}
    // 			// 	all_li.removeClass('selected');
    // 			// 	$(this).addClass('selected');
    // 			// 	return ;
    // 			// });
    // 			var kdfh = $(elem).find("#kdfh"),
    // 				kuaidi_list=$(elem).find(".v3_qtkd_list"),
    // 				shsm = $(elem).find("#shsm");
    // 			$(elem).find(".v3_fahuo_m ul label input").on("click",function(){
    // 				if(kdfh.is(':checked') == true){
    // 					return true;
    // 				}else{
    // 					return false;
    // 				}
    // 			});
    // 			$(elem).find("#elseKuaidi").on("keyup",function(event) {//快递公司输入框，改变内容显示其他快递公司列表
    // 				var text=$(this).val();
    // 				if($(this).val()){
    // 					kuaidi_list.show();
    // 				}
    // 			});
    // 			kdfh.on("click",function(){
    // 				$(elem).find(".v3_fahuo_m").addClass("c");
    // 				$(elem).find("#shsmArea").removeClass('c');
    // 				$(elem).find(".tinput").removeAttr("readonly");
    // 			});
    // 			shsm.on("click",function(){
    // 				$(elem).find(".v3_fahuo_m").removeClass("c");
    // 				$(elem).find(".v3_fahuo_m ul label input").prop("checked",false);
    // 				$(elem).find("#shsmArea").addClass('c');
    // 				$(elem).find(".tinput").attr("readonly","readonly").val("");
    // 			})
    // 			kuaidi_list.find('dd').on('click', function(event) {// 点击快递后 影藏列表
    // 				event.stopPropagation();
    // 				var text=$(this).text();
    // 				$(elem).find("#elseKuaidi").val(text);
    // 				kuaidi_list.hide();
    // 			});
    // 			$(elem).find("#elseKuaidi").on("click",function(event){
    // 				$(this).prev().prop("checked",true);
    // 			});
    // 			$(elem).on("click",function(){
    // 				kuaidi_list.hide();
    // 			});
    // 		},
    // 		yes: function(index, elem) {
    // 			var danhao = $(elem).find('.v3_fahuo_kddh input');
    // 			alert("快递公司" + danhao.eq(0).val()+"快递单号" + danhao.eq(1).val());
    // 			layer.close(index);
    // 		},
    // 		content: $(".v3_fahuo_hide").html()
    // 	});
    // 	return false;
    // }
    //发货
    // $(".v3_fahuo_btn").click(function(event) {
    // 	Deliver();
    // 	return false;
    // });




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
    // $(".v3_tytk_btn").click(function(event) {
    // 	layerWarmAlert("同意退款后钱款将直接打回买家账户","温馨提示：保证您同意退款时还没发货",10000);
    // 	return false;
    // });

    //批量收货
    // $(".v3_plshouhuo").click(function(event) {
    // 	var input = $(".v3_select_l:checked"),
    // 		len = input.length;
    // 	for (var i = 0; i < len; i++) {
    // 		if (!input.eq(i).parents("li").find('.v3_qrsh_btn').length) {
    // 			layerAlert("宝贝在发货以后才能确认收货，请重新选择");
    // 			return false;
    // 		}
    // 	}
    // 	if (len === 0) {
    // 		layerAlert("请选择宝贝确认收货，可以选择多个");
    // 		return false;
    // 	}
    // 	// 此处写收货代码
    // 	// 
    // 	// 此处写收货代码
    // 	return false;
    // });

    //批量发货
    // $(".v3_plfahuo").click(function(event) {
    // 	var input = $(".v3_select_l:checked"),
    // 		len = input.length;
    // 	if (len === 0) {
    // 		layerAlert("请选择宝贝确认发货，可以选择多个");
    // 		return false;
    // 	};
    // 	for (var i = 0; i < len; i++) {
    // 		if (!input.eq(i).parents("li").find('.v3_fahuo_btn').length) {
    // 			layerAlert("订单在付款以后才能发货，请重新选择");
    // 			return false;
    // 		}
    // 	}
    // 	// 这里需要判断是不是同一个人
    // 	Deliver();
    // 	return false;
    // });

    //删除
    // $(".v3_order_del_btn").click(function(event) {
    // 	$(this).parent().parent().remove();
    // 	return false;
    // });

    //批量删除
    // $(".v3_order_del").click(function(event) {
    // 	var input = $(".v3_select_l:checked"),
    // 		len = input.length;
    // 	if(len===0){
    // 		layerAlert("请选择要删除的宝贝");
    // 		return false;
    // 	}
    // 	for (var i = 0; i < len; i++) {
    // 		if(!input.eq(i).parent().parent().find('.v3_order_del_btn').length){
    // 			layerAlert("宝贝在关闭订单或订单成功以后才能被删除，请重新选择");
    // 			return false;
    // 		}
    // 	}
    // 	input.each(function(index, el) {
    // 		$(this).parents("li").remove();
    // 	});
    // 	return false;
    // });

    //全选
    // $('.v3_select_all').click(function(event) {
    // 	var all=$('.v3_select_l,.v3_select_all');
    // 	if($(this).get(0).checked){//全部选择
    // 		$.each(all, function() {
    // 			this.checked = "checked";
    // 		});
    // 	}else{
    // 		$.each(all, function() {//全部取消
    // 			this.checked = false;
    // 		});
    // 	}
    // });

    //全选后 点击其他 选项，去掉全选选中状态
    // $('.v3_select_l').click(function(event) {
    // 	var all_1=$('.v3_select_all'),
    // 		all_2=$('.v3_select_l'),
    // 		len_2=all_2.length,
    // 		i=0;
    // 	if($(this).get(0).checked){//选中
    // 		$.each(all_2, function() {
    // 			if (this.checked === true)
    // 			i++;
    // 		});
    // 		if (i===len_2) {
    // 			$.each(all_1, function() {
    // 				this.checked = "checked";
    // 			});
    // 		}
    // 	}else{
    // 		$.each(all_1, function() {//取消选中
    // 			this.checked = false;
    // 		});
    // 	}
    // });

    // 订单搜索 placeholder 效果
    // $.each($(".v3_m_s_main input"), function(index, val) {
    // 	var txt = $(this).attr("placeholder");
    // 	$(this).val(txt);
    // 	if ($(this).hasClass('password')) {
    // 		$(this).hide().siblings(".password1").show();
    // 	};
    // });
    // $(".v3_m_s_main input").focus(function(event) {
    // 	var $this = $(this),
    // 		txt = $this.val(),
    // 		text = $this.attr("placeholder");
    // 	if (txt == text)
    // 		$this.val("");
    // 	$this.addClass('input_focus');
    // 	if ($(this).hasClass('password1')) {
    // 		$(this).hide().siblings(".password").show().trigger('focus');
    // 	};
    // });
    // $(".v3_m_s_main input").blur(function(event) {
    // 	var $this = $(this),
    // 		txt = $this.val(),
    // 		text = $this.attr("placeholder");
    // 	if (txt == "" || txt == text) {
    // 		$this.removeClass('input_focus').val(text);
    // 		if ($(this).hasClass('password')) {
    // 			$(this).hide().siblings(".password1").show();
    // 		};
    // 	};
    // });

    //我收藏的宝贝
    //我收藏的店铺
    //我收藏的宝贝 二级导航
    // $(".v3_c_subnav li").hover(function() {
    // 	$(this).addClass('selected');
    // }, function() {
    // 	$(this).removeClass('selected');
    // });
    //二级导航选中效果
    // $(".v3_c_subnav .v3_c_hide a").click(function(event) {
    // 	$(this).parent().parent().find('a').removeClass('selected');
    // 	$(this).addClass('selected');
    // });
    //批量管理
    // $(".v3_collect_plgl").click(function(event) {
    // 	var parent=$(this).parent();
    // 	parent.siblings().show();
    // 	parent.hide();
    // 	$(".v3_collect_main li").addClass('v3_c_caozuo');//宝贝层出现
    // 	$(".v3_dianpu_list").addClass('v3_c_caozuo');//店铺层出现
    // 	return false;
    // });
    //取消管理
    // $(".v3_collect_qxgl").click(function(event) {
    // 	var parent=$(this).parent();
    // 	parent.siblings().show();
    // 	parent.hide();
    // 	$(".v3_collect_main li").removeClass('v3_c_caozuo');//宝贝去掉操作层
    // 	$(".v3_c_gl_layer").removeClass('selected');//宝贝去掉选中效果
    // 	$(".v3_dianpu_list").removeClass('v3_c_caozuo');//店铺去掉操作层
    // 	$(".v3_dp_layer").removeClass('selected');//店铺去掉选中效果
    // 	$(".v3_collect_s_all").get(0).checked=false;
    // 	return false;
    // });

    //全选宝贝
    // $(".v3_collect_s_all").click(function(event) {
    // 	if (this.checked === true){
    // 		$(".v3_c_gl_layer").addClass('selected');//宝贝选中效果
    // 		$(".v3_dp_layer").addClass('selected');//店铺选中效果
    // 	}else{
    // 		$(".v3_c_gl_layer").removeClass('selected');//去掉宝贝选中效果
    // 		$(".v3_dp_layer").removeClass('selected');//去掉店铺选中效果
    // 	}

    // });

    //删除选中的宝贝 全选右边删除
    // $(".v3_collect_del").click(function(event) {
    // 	$.each($(".v3_c_gl_layer"), function(index, val) {//宝贝删除
    // 		if($(this).hasClass('selected')){
    // 			$(this).parent().parent().remove();
    // 		}
    // 	});
    // 	$.each($(".v3_dp_layer"), function(index, val) {//店铺删除
    // 		if($(this).hasClass('selected')){
    // 			$(this).parent().remove();
    // 		}
    // 	});
    // 	return false;
    // });
    //逐个宝贝删除
    // $(".v3_c_del").click(function(event) {
    // 	$(this).parents("li").remove();
    // 	return false;
    // });

    //显示购物车，删除等按钮
    // $(".v3_collect_img").hover(function() {
    // 	$(this).addClass('selected');
    // }, function() {
    // 	$(this).removeClass('selected');
    // });

    //收藏的宝贝选中效果
    // $(".v3_c_gl_layer").click(function(event) {
    // 	if($(this).hasClass('selected')){
    // 		$(this).removeClass('selected');
    // 	}else{
    // 		$(this).addClass('selected');
    // 	}
    // });

    //收藏的店铺选中效果
    // $(".v3_dp_layer").click(function(event) {
    // 	if($(this).hasClass('selected')){
    // 		$(this).removeClass('selected');
    // 	}else{
    // 		$(this).addClass('selected');
    // 	}
    // });

    //宝贝管理
    //宝贝管理 下拉选择宝贝分类
    // $(".v3_g_s_main").hover(function() {
    // 	$(this).addClass('selected');
    // }, function() {
    // 	$(this).removeClass('selected');
    // });
    // $(".v3_g_s_main").find('.v3_g_s_hide p').click(function(event) {
    // 	var _this = $(this),
    // 		text = _this.text();
    // 	_this.parent().parent().find('.v3_g_s_show span').text(text);
    // 	_this.parent().parent().removeClass('selected');
    // });

    //仓库中的宝贝排序
    // $(".v3_m_sort").click(function(event) {
    // 	if($(this).hasClass('v3_m_up')){
    // 		$(this).removeClass('v3_m_up');
    // 	}else{
    // 		$(this).addClass('v3_m_up');
    // 	}
    // });

    //宝贝管理全选
    // $('.v3_g_select_all').click(function(event) {
    // 	var all=$('.v3_g_select_l');
    // 	if($(this).get(0).checked){//全部选择
    // 		$.each(all, function() {
    // 			this.checked = "checked";
    // 		});
    // 	}else{
    // 		$.each(all, function() {//全部取消
    // 			this.checked = false;
    // 		});
    // 	}
    // });

    //全选后 点击其他 选项，去掉全选选中状态
    // $('.v3_g_select_l').click(function(event) {
    // 	var all_1=$('.v3_g_select_all'),
    // 		all_2=$('.v3_g_select_l'),
    // 		len_2=all_2.length,
    // 		i=0;
    // 	if($(this).get(0).checked){//选中
    // 		$.each(all_2, function() {
    // 			if (this.checked === true)
    // 			i++;
    // 		});
    // 		if (i===len_2) {
    // 			$.each(all_1, function() {
    // 				this.checked = "checked";
    // 			});
    // 		}
    // 	}else{
    // 		$.each(all_1, function() {//取消选中
    // 			this.checked = false;
    // 		});
    // 	}
    // });

    //下架
    // $(".v3_g_xiajia").click(function(event) {
    // 	var input = $(".v3_g_select_l:checked"),
    // 		len = input.length;
    // 	if (len === 0) {
    // 		layerAlert("请选择宝贝下架，可以选择多个");
    // 		return false;
    // 	}
    // 	layerConfirm("您确定要将宝贝下架，下架的宝贝将从列表中删除",
    // 		function(){
    // 			input.each(function(index, el) {
    // 				$(this).parent().parent().remove();
    // 			});
    // 			layerAlert("下架成功");
    // 		}
    // 	);
    // 	return false;
    // });

    // 批量删除已下架的宝贝
    // $(".v3_g_del").click(function(event) {
    // 	var input = $(".v3_g_select_l:checked"),
    // 		len = input.length;
    // 	if(len===0){
    // 		layerAlert("请选择宝贝删除，可以选择多个");
    // 		return false;
    // 	};
    // 	layerConfirm("您确定要删除宝贝，删除的宝贝将从列表中删除",
    // 		function(){
    // 			input.each(function(index, el) {
    // 				$(this).parent().parent().remove();
    // 			});
    // 		}
    // 	);
    // 	return false;
    // });

    // 批量上架宝贝
    // $(".v3_g_shangjia").click(function(event) {
    // 	var input = $(".v3_g_select_l:checked"),
    // 		len = input.length;
    // 	if(len===0){
    // 		layerAlert("请选择宝贝上架，可以选择多个");
    // 		return false;
    // 	};
    // 	for (var i = len - 1; i >= 0; i--) {
    // 		if(input.eq(i).parent().parent().find(".v3_g_gaoqing").length){
    // 			layerAlert("告罄的宝贝不能被上架，请重新选择");
    // 			return false;
    // 		}
    // 	};
    // 	layerAlert("宝贝上架成功");
    // 	return false;
    // });

    //逐个上架操作
    // $(".v3_goods_sj").click(function(event) {
    // 	if($(this).hasClass('v3_g_gaoqing')){
    // 		layerAlert("告罄的宝贝不能被上架了");
    // 		return false;
    // 	};
    // 	layerAlert("宝贝上架成功");
    // 	return false;
    // });

    //收货地址
    //设为默认地址
    // $(".v3_set_moren").click(function(event) {
    // 	var text1="当前默认",
    // 		text2="设为默认",
    // 		that=$(this),
    // 		text=that.text(),
    // 		tr_p=that.parent().parent().parent(),
    // 		tr_p_s=tr_p.siblings();
    // 	if(text==text1){
    // 		return false;
    // 	}
    // 	that.text(text1);
    // 	tr_p.addClass('v3_a_moren');
    // 	tr_p_s.removeClass("v3_a_moren");
    // 	tr_p_s.find('.v3_set_moren').text(text2);
    // 	layerAlert("设置成功！");
    // 	return false;
    // });

    //设为默认地址
    // $(".v3_address_del").click(function(event) {
    // 	var tr_p=$(this).parent().parent().parent();
    // 	layerConfirm("确定要删除地址吗",function(){
    // 		tr_p.remove();
    // 	});
    // 	return false;
    // });

    //新增地址
    // $(".v3_address_add_btn a").click(function(event) {
    // 	layer.open({
    // 		type: 1,
    // 		title: "添加新地址",
    // 		skin: "",
    // 		area: ['450px', '340px'],
    // 		btn: ['确定', '取消'],
    // 		success: function(elem, index) {

    // 		},
    // 		yes: function(index, elem) {
    // 			var selects=$(elem).find('.v3_add_address select'),
    // 				inputs=$(elem).find('.v3_add_address input'),
    // 				sheng=selects.eq(0).val(),
    // 				shi=selects.eq(1).val(),
    // 				qu=selects.eq(2).val(),
    // 				xxdz=inputs.eq(0).val(),
    // 				yzbm=inputs.eq(1).val(),
    // 				sjr=inputs.eq(2).val(),
    // 				lxdh=inputs.eq(3).val();
    // 			alert(sheng+shi+qu);
    // 			layer.close(index);
    // 		},
    // 		content: $(".v3_address_hide").html()
    // 	});
    // 	return false;
    // });

    //修改地址
    // $(".v3_a_modify").click(function(event) {
    // 	layer.open({
    // 		type: 1,
    // 		title: "修改地址",
    // 		skin: "",
    // 		area: ['450px', '340px'],
    // 		btn: ['确定', '取消'],
    // 		success: function(elem, index) {

    // 		},
    // 		yes: function(index, elem) {
    // 			var text=$(elem).find(".v3_address_xx").val();
    // 			alert(text);
    // 			layer.close(index);
    // 		},
    // 		content: $(".v3_address_hide").html()
    // 	});
    // 	return false;
    // });

    //发布闲置
    //物品分类
    // $(".v3_fabu_fenlei").click(function(event) {
    // 	$(this).children('.v3_fabu_fenlei_box').addClass('xianshi');
    // });
    // $(".v3_fabu_fenlei").mouseleave(function(event) {
    // 	$(this).children('.v3_fabu_fenlei_box').removeClass('xianshi');
    // });
    // $(".v3_fabu_fenlei_li").hover(function(event) {
    // 	$(this).addClass('selected');
    // }, function(event) {
    // 	$(this).removeClass('selected');
    // });
    // $(".v3_fabu_fenlei_li a").click(function(event) {
    // 	$(".v3_fabu_fenlei span").text($(this).text());
    // 	$(this).parents(".v3_fabu_fenlei_li").removeClass('selected');
    // 	//根据选择赋值
    // 	// $(".v3_fabu_fenlei_box").attr("data-v1", $(this).parents(".v3_fabu_fenlei_li").attr("data-v"));
    // 	// $(".v3_fabu_fenlei_box").attr("data-v2", $(this).attr("data-v"));
    // 	return false;
    // });

    //未有地址 显示选择
    // if(!$(".v3_fabu_yxdz").length){
    // 	$(".v3_fabu_wxdz").css('display', 'block');
    // };
    // 修改地址
    // $(".v3_fabu_yxdz a").click(function(event) {
    // 	var p=$(this).parent();
    // 	p.css('display', 'none');
    // 	p.siblings().css('display', 'block');
    // });

    //发布闲置 删除图片效果
    // $(".v3_f_imglist").on({
    // 	mouseenter:function(event) {
    // 		event.preventDefault();
    // 		$(this).addClass('selected');
    // 	},
    // 	mouseleave:function(event) {
    // 		event.preventDefault();
    // 		$(this).removeClass('selected');
    // 	}
    // },".v3_f_imgbox");
    //删除
    // $(".v3_f_imglist").on('click', '.v3_f_imgbox div', function(event) {
    // 	event.preventDefault();
    // 	$(this).parent().parent().remove();
    // });


    // 订单详情 买家
    // 删除地址
    // $(".v3_receipt_area").on('click', '.v3_r_area_del',function(event) {
    // 	var that_p=$(this).parent();
    // 	layerConfirm("确定要删除地址吗",function(){
    // 		that_p.siblings().empty().append('亲，您还没有收货地址哦');
    // 		that_p.empty();
    // 	});
    // 	return false;
    // });

    //修改地址
    // $(".v3_receipt_area").on('click', '.v3_r_area_alter', function(event) {
    // 	event.preventDefault();
    // 	layer.open({
    // 		type: 1,
    // 		title: "修改地址",
    // 		skin: "",
    // 		area: ['450px', '340px'],
    // 		btn: ['确定', '取消'],
    // 		success: function(elem, index) {

    // 		},
    // 		yes: function(index, elem) {
    // 			var text=$(elem).find(".v3_address_xx").val();
    // 			alert(text);
    // 			layer.close(index);
    // 		},
    // 		content: $(".v3_address_hide").html()
    // 	});
    // 	return false;
    // });
    //使用其他地址
    // $(".v3_dizhi_other").click(function(event) {
    // 	layer.open({
    // 		type: 1,
    // 		title: "其它收货地址",
    // 		skin: "",
    // 		area: ['650px'],
    // 		btn: ['确定', '取消'],
    // 		success: function(elem, index) {
    // 			$(elem).find('.v3_receipt_dizhi').click(function(event) {
    // 				$(this).find('input').get(0).checked = "checked";
    // 			});
    // 		},
    // 		yes: function(index, elem) {
    // 			var input=$(elem).find('input:checked'),
    // 				html;
    // 			if(input.length>0){
    // 				var receipt=$(".v3_receipt_area .v3_receipt_dizhi");
    // 				receipt.empty();
    // 				input.nextAll().prependTo(receipt);
    // 			}
    // 			layer.close(index);
    // 		},
    // 		content: $(".v3_all_address").html()
    // 	});
    // 	return false;
    // });
    //新增地址
    // $(".v3_dizhi_new").click(function(event) {
    // 	layer.open({
    // 		type: 1,
    // 		title: "添加新地址",
    // 		skin: "",
    // 		area: ['450px', '340px'],
    // 		btn: ['确定', '取消'],
    // 		success: function(elem, index) {

    // 		},
    // 		yes: function(index, elem) {
    // 			var selects=$(elem).find('.v3_add_address select'),
    // 				inputs=$(elem).find('.v3_add_address input'),
    // 				sheng=selects.eq(0).val(),
    // 				shi=selects.eq(1).val(),
    // 				qu=selects.eq(2).val(),
    // 				xxdz=inputs.eq(0).val(),
    // 				yzbm=inputs.eq(1).val(),
    // 				sjr=inputs.eq(2).val(),
    // 				lxdh=inputs.eq(3).val();
    // 			alert(sheng+shi+qu);
    // 			layer.close(index);
    // 		},
    // 		content: $(".v3_address_hide").html()
    // 	});
    // 	return false;
    // });

    // var order_account = function() {
    // 	var li = $(".v3_orderinfo_main li"),
    // 		total_w = $(".v3_orderinfo_total span"),
    // 		account_num = $(".v3_orderinfo_total b"),
    // 		num = 0,
    // 		total_price = 0;
    // 	li.each(function(index, el) {//逐个计算
    // 		var that = $(this),
    // 			danjia = that.find('.unit_price'),
    // 			shuliang = that.find('.v3_orderinfo_num input'),
    // 			youfei = parseFloat(that.find('.v3_orderinfo_fare').text())*100/100 || 0,
    // 			xiaoji = that.find('.v3_orderinfo_xj'),
    // 			coupon_on = that.find('.coupon_on'),
    // 			coupon_slt = that.find('.coupon_slt'),
    // 			coupon = parseFloat(coupon_slt.val())||0,
    // 			price = 0;
    // 		for (var i = danjia.length - 1; i >= 0; i--) {
    // 			price += parseFloat(danjia.eq(i).text()) * 100 * parseInt(shuliang.eq(i).val()) / 100+youfei;
    // 			num += parseInt(shuliang.eq(i).val());
    // 		};
    // 		coupon_on.is(":checked")&&(price-=coupon);
    // 		total_price += price;
    // 		xiaoji.text(price);//小计
    // 	});
    // 	total_w.text(total_price);//总计
    // 	account_num.text(num);
    // };
    //是否选择使用现金券
    // $(".coupon_on").on('click', function(event) {
    // 	order_account();
    // });
    //现金券下拉选择改变
    // $('.coupon_slt').on('change', function(event) {
    // 	order_account();
    // });
    //订单详情 物品数量加减效果
    // $(".v3_orderinfo_num span").click(function(event) {
    // 	var $input=$(this).siblings('input'),
    // 		num=$input.val(),
    // 		i=parseInt(num),
    // 		tr=$(this).parents("tr"),
    // 		danjia=tr.find('.unit_price').text(),
    // 		zongjia=tr.find('td').eq(4),
    // 		price=0;
    // 	if($(this).index()==0){
    // 		i--;
    // 		i=i<=1?"1":i;
    // 		$input.val(i);
    // 		price=parseFloat(danjia)*100*i/100;
    // 		zongjia.text(price);
    // 	}else{
    // 		i++;
    // 		$input.val(i);
    // 		price=parseFloat(danjia)*100*i/100;
    // 		zongjia.text(price);
    // 	}

    // 	//计算总价
    // 	order_account();
    // });
    //订单详情 物品数量输入
    // $(".v3_orderinfo_num input").keydown(function(event) {//键盘只能输入数字
    // 	var str = event.keyCode||event.which;
    // 	if ((str <= 105 && str >= 96)||(str <= 57 && str >= 48)||str == 8) {
    // 		return true;
    // 	}else{
    // 		event.returnValue = false;
    // 		return false;
    // 	}
    // });
    // $(".v3_orderinfo_num input").on('input change', function(event) {
    // 	event.preventDefault();
    // 	var type = /^[0-9]*[1-9][0-9]*$/,//判断是不是数字
    // 		re = new RegExp(type);
    // 	if ($(this).val().match(re) == null) {
    // 		$(this).val("1");
    // 	}
    // 	order_account();
    // });

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
    // countDown_hms($(".v3_o_pay_djs i"),function(){
    // 	alert("系统自动取消订单");
    // });

    //订单详情 待付款 倒计时
    countDown_hms($(".v3_o_djs i"), function () {
        alert("系统自动取消订单");
    });

    // 确认收货倒计时
    countDown_dhms($(".v3_o_djs_day i"), function () {
        alert("活动结束");
    });

    //卖家处理退款申请倒计时 
    // countDown_dhms($(".v3_refund_djs_day i"),function(){
    // 	alert("退款成功");
    // });

    //订单详情 付款成功 提醒发货
    // $(".v3_o_m_txfh").click(function(event) {
    // 	layerAlert("提醒发货成功");
    // 	return false;
    // });



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
    // $(".v3_fjxz_nav a").click(function(event) {
    // 	var that=$(this),
    // 		i=that.parent().index();
    // 	that.parent().parent().find('a').removeClass('selected');
    // 	that.addClass('selected');
    // 	$(".v3_fjxz_list").css('display', 'none').eq(i).css('display', 'block');
    // 	return false;
    // });


    //消息删除按钮
    // $(".v3_news_del").click(function(event) {
    // 	var parent_li = $(this).parent().parent();
    // 	layerConfirm("您确定要删除该消息吗？", function() {
    // 		parent_li.remove();
    // 	});
    // });




    //闲置物品 详情
    //收藏效果
    // $(".v3_b_collect").click(function(event) {
    // 	var b=$(this).find('b'),
    // 		i=$(this).find('i');
    // 	if (b.hasClass('selected')) {
    // 		b.removeClass('selected');
    // 		i.text("收藏宝贝");
    // 	}else{
    // 		b.addClass('selected');
    // 		i.text("已收藏");
    // 	};
    // });
    //数量加减效果
    // $(".v3_buy_num span").click(function(event) {
    // 	var $input=$(this).siblings('input'),
    // 		num=$input.val(),
    // 		i=parseInt(num);
    // 	if($(this).index()==0){
    // 		i--;
    // 		i=i<=1?"1":i;
    // 		$input.val(i);
    // 	}else{
    // 		i++;
    // 		$input.val(i);
    // 	}
    // });

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
    // $(".v3_xz_list li").each(function(index, el) {
    // 	if (($(this).index()+1)%4===0) {
    // 		$(this).css('marginRight', 0);
    // 	};
    // });




    //回车数据提交失去焦点
    $(".nonpoint,input[type='submit'],input[type='button']").on('keypress', function (e) {
        var target = e.target || e.srcElement;
        if (e.keyCode == 13) {
            target.blur();
        }
    })

    //头部导航栏效果。
    var headnav = {
        contains: $("#hnavCont"),
        list: $("#hnavCont").find(".havItem"),
        timer: 'navTimer',
        Htime: 200,
        contIndex: '',
        navShow: function () {
            //this.contains.find(".havItem").eq(this.contIndex).show().siblings().hide();
            this.contains.stop(true).animate({ "height": "200px" }).addClass('hasbb');
            $(window).trigger('scroll');
        },
        navHide: function () {
            this.contains.stop(true).animate({ "height": "0" }).removeClass('hasbb');
        }
    };
    // $(".header_nav a").on("mouseenter",function(){
    // 	headnav.contIndex  = $(this).parent().index();
    // 	headnav.navShow();
    // 	clearInterval(headnav.timer);
    // }).on("mouseleave",function(){
    // 	headnav.timer = setTimeout(function(){
    // 		headnav.navHide();
    // 	},headnav.Htime);
    // });
    // $("#hnavCont").on("mouseenter",function(){
    // 	headnav.navShow();
    // 	clearInterval(headnav.timer);
    // }).on("mouseleave",function(){
    // 	headnav.timer = setTimeout(function(){
    // 		headnav.navHide();
    // 	},headnav.Htime);
    // });
    $(".header_nav a").on("mouseenter", function () {
        clearTimeout(headnav.timer);//清除掉关闭的
        headnav.timer = setTimeout(function () {//设置定时显示
            headnav.navShow();
        }, headnav.Htime);
        headnav.contIndex = $(this).parent().index();
        headnav.list.eq(headnav.contIndex).find('li').length ? headnav.list.eq(headnav.contIndex).show().siblings().hide() : (clearTimeout(headnav.timer), headnav.navHide());


    }).on("mouseleave", function () {
        clearTimeout(headnav.timer);//清除掉显示
        headnav.timer = setTimeout(function () {//设置定时关闭
            headnav.navHide();
        }, headnav.Htime);
    });
    $("#hnavCont").on("mouseenter", function () {
        clearTimeout(headnav.timer);//清除掉关闭的
        //已经显示了不用设置定时显示
    }).on("mouseleave", function () {
        headnav.timer = setTimeout(function () {//设置定时关闭
            headnav.navHide();
        }, headnav.Htime);
    });

    //潮流搭配滑动效果 web4.0 新品推荐
    function combo(opt) {
        this.currSize = 0;   //当前滚动页面
        this.rollsize = 0;   //设置滚动次数
        this.timer = 'combo_timer'; //设置定时器
        this.contains = opt.contains;
        this.con_lis = this.contains.find("li");
        this.signWidth = this.con_lis.eq(0).outerWidth(true);//+parseInt(this.con_lis.eq(0).css("marginRight"));
        this.baseTime = opt.baseTime || null;
        this.baseSize = opt.baseSize || 4;
        this.color = opt.color || ["#f2b6b6", "#fadb8b", "#8bd2fa", "#8bfaca"];
        this.rbtn = opt.rbtn;
        this.lbtn = opt.lbtn;
        //设定ul的基础宽度。
        this.contains.css("width", this.con_lis.length * this.signWidth);
        for (var i = 0, len = this.con_lis.length; i < len; i++) {
            var b = i % this.baseSize;
            this.con_lis.eq(i).css({ "border-top-color": this.color[b] });  //设置li的borderTop颜色。
        }
        this.rollsize = Math.ceil(this.con_lis.length / this.baseSize);
        this.setCSS();
        if (this.rollsize > 1 && this.baseTime) {
            this.run(this);
        }
        var self = this;
        this.lbtn.on("click", function () {
            self.lScroll();
            $(window).trigger('scroll');
        });
        this.rbtn.on("click", function () {
            self.rScroll();
            $(window).trigger('scroll');
        });
    }
    combo.prototype = {
        lScroll: function () {  //左击按钮事件
            if (this.currSize === 0) { return; } //如果是第一页则不需要滚动
            clearInterval(this.timer);
            this.currSize--;
            var arrive = -this.currSize * this.baseSize * this.signWidth;  //滚动后要到的marginLeft值
            this.contains.css("marginLeft", arrive);
            this.setCSS();
            if (this.baseTime) {
                this.run();
            }
        },
        rScroll: function () {  //右击按钮事件
            console.log(this.rollsize)
            if (this.rollsize === 0 || this.currSize === this.rollsize - 1) { return; }  //如果是第最后一页则不需要滚动
            clearInterval(this.timer);
            this.currSize++;
            var arrive = this.currSize * this.baseSize * this.signWidth;  //滚动后要到的marginLeft值
            this.contains.css("marginLeft", -arrive);
            this.setCSS();
            if (this.baseTime) {
                this.run();
            }
        },
        setCSS: function () {  //设置按钮样式。
            var lbtn = this.lbtn, rbtn = this.rbtn;
            if (this.currSize === 0) { lbtn.removeClass('canRoll'); } else { lbtn.addClass('canRoll') }
            if (this.rollsize === 0 || this.currSize === this.rollsize - 1) { rbtn.removeClass('canRoll'); } else { rbtn.addClass('canRoll') }
        },
        run: function (_self) {  //设置定时器，定时滚动
            var _self = _self || this;
            _self.timer = setInterval(function () {
                if (_self.currSize === _self.rollsize - 1) {
                    _self.currSize = 0;
                    _self.contains.css("marginLeft", 0);
                    _self.setCSS();
                } else {
                    _self.rScroll();
                }
            }, _self.baseTime);
        }
    };
    // if($("#pro_combo").length>0){
    // 	var comboCp = new combo({
    // 		contains : $("#pro_combo ul"),
    // 		baseTime : 5000,
    // 		baseSize : 4,
    // 		lbtn     : $("#combo_lbtn"),
    // 		rbtn     : $("#combo_rbtn")
    // 	});
    // }

    if ($(".v4_pro_tuijian").length > 0) {
        var comboCp = new combo({
            contains: $(".v4_pro_tuijian ul"),
            baseTime: 0,//为0时 不滚动
            baseSize: 5,
            color: ["#fbad2c", "#f6ff00", "#ff0000", "#2b8e00", "#0066ff"],
            lbtn: $("#combo_lbtn"),
            rbtn: $("#combo_rbtn")
        });
    }

    //web3.1文档类
    if ($("#docLink").length > 0) {
        $("#docLink ul").on("click", "li,dd", function () {
            $(this).addClass('selected').siblings().removeClass('selected');
        })

    }

    // if($("#hnavCont").length>0){
    // 	var contLen = $("#hnavCont .havItem").length,
    // 		arr = [];
    // 	for(var ci = 0; ci<contLen; ci++){
    // 		arr[ci] = new combo({
    // 			contains : $("#hnavCont .havItem").eq(ci).find("ul").eq(0),
    // 			baseSize : 7,
    // 			lbtn	 : $("#hnavCont .havItem"+ci+"_lbtn"),
    // 			rbtn     : $("#hnavCont .havItem"+ci+"_rbtn")
    // 		});
    // 	}
    // }

    //回到顶部
    var $go_to_top = $(".go_to_top");
    if ($go_to_top.length) {
        var _window = $(window),
			obj_w = $go_to_top.width();
        function change_right() {
            var w_w = _window.width(),
				right = (w_w - 1190) / 2 - obj_w - 5;
            $go_to_top.css('right', right);
        }
        change_right();
        // var sidebar_top=$(window).height()-$go_to_top.height();
        // $go_to_top.fixedTop({
        // 	is_right: true,
        // 	startline: 0,
        // 	top: sidebar_top
        // });
        _window.scroll(function () {
            if (_window.scrollTop() > 200) {
                $go_to_top.fadeIn(500);
            } else {
                $go_to_top.fadeOut(500);
            }
        }).resize(function (event) {
            change_right();
        });;
        $go_to_top.click(function () {
            $('body,html').animate({
                scrollTop: 0
            });
            return false;
        });
    }

    //购物车显示效果
    (function () {
        var $myCartWrap = $(".myCartWrap");
        if (!$myCartWrap.length) return;
        var //$myCartWrap=$(".myCartWrap"),
			hTime = 200,
			timer;

        $myCartWrap.on("mouseenter", function () {
            clearTimeout(timer);//清除掉关闭的
            timer = setTimeout(function () {//设置定时显示
                $myCartWrap.addClass('dropdown');
            }, hTime);
        }).on("mouseleave", function () {
            clearTimeout(timer);//清除掉显示
            timer = setTimeout(function () {//设置定时关闭
                $myCartWrap.removeClass('dropdown');
            }, hTime);
        });
    })();

    //所有 导航栏显示效果
    (function () {
        var $sub_nav_box = $(".sub_nav_box");
        if (!$sub_nav_box.length) return;
        var $btn_nav = $(".header_nav h3"),
			hTime = 200,
			timer;

        $btn_nav.on("mouseenter", function () {
            clearTimeout(timer);//清除掉关闭的
            timer = setTimeout(function () {//设置定时显示
                $sub_nav_box.addClass('show');
            }, hTime);
        }).on("mouseleave", function () {
            clearTimeout(timer);//清除掉显示
            timer = setTimeout(function () {//设置定时关闭
                $sub_nav_box.removeClass('show');
            }, hTime);
        });
        $sub_nav_box.on("mouseenter", function () {
            clearTimeout(timer);//清除掉关闭的
            //已经显示了不用设置定时显示
        }).on("mouseleave", function () {
            timer = setTimeout(function () {//设置定时关闭
                $sub_nav_box.removeClass('show');
            }, hTime);
        });

    })();

    //所有 三级导航栏显示效果
    (function () {
        var $ul = $(".l_banner_nav ul");
        if (!$ul.length) return;
        var //$li=$(".myCartWrap"),
			hTime = 50,
			timer;

        $ul.on("mouseenter", "li", function () {
            var self = this;
            clearTimeout(timer); //清除掉关闭的
            timer = setTimeout(function () { //设置定时显示
                $(self).addClass('selected').siblings().removeClass('selected');
            }, hTime);
        }).on("mouseleave", "li", function () {
            var self = this;
            clearTimeout(timer); //清除掉显示
            timer = setTimeout(function () { //设置定时关闭
                $(self).siblings().addBack().removeClass('selected');
            }, hTime);
        });
    })();

    //二级导航 baner图左边的产品展开宽度
    // (function(){
    // 	var nav_child = $(".nav_child"),
    // 		rowWidth = 160;  //每一个产品的宽度
    // 	nav_child.each(function(){
    // 		var ddLen = $(this).find("dd").length,
    // 			ddrow = Math.ceil(ddLen/4);
    // 		if(ddrow>5){ ddrow=5}
    // 		console.log(rowWidth,ddrow)
    // 		$(this).css("width",rowWidth*ddrow+40+"px");
    // 	});
    // })();

    //会员中心 两栏等高布局
    (function () {
        var $userNav = $(".userNav");
        if (!$userNav.length) return;
        var $userMain = $(".userMain");
        $userMain.css('min-height', $userNav.height() - 52);//52=20(左边m-bottom)+30(右边p-bottom)+2(右边border)
    })();

    //全局ajax事件
    var layerLoad;
    cayiWZK.loading = !0;
    $(document).ajaxSend(function () {
        layerLoad = cayiWZK.loading && layer.load(2, { time: 60 * 1000 });
    });
    $(document).ajaxComplete(function () {
        layer.close(layerLoad);
    });

    //右侧栏 优惠券展示效果
	(function(){
		var $toolbar_open_btn = $(".toolbar_open_btn"),
			$toolbar_main = $(".toolbar_main"),
			$g_coupon_close = $(".g_coupon_close");
		$toolbar_open_btn.click(function(event) {//打开或关闭
			if($(this).hasClass('selected')){
				$toolbar_main.removeClass('toolbar_open');
				$(this).removeClass('selected');
			}else{
				$toolbar_main.addClass('toolbar_open');
				$(this).addClass('selected');
			}
		});
		$g_coupon_close.click(function(event) {//关闭
			$toolbar_main.removeClass('toolbar_open');
			$toolbar_open_btn.removeClass('selected');
		});
		$(".coupon").click(function(event) {
			event.stopPropagation();
			event.preventDefault();
			$toolbar_open_btn.trigger('click');
		});
		$(document).click(function(event) {//关闭
			$g_coupon_close.trigger('click');
		});
		$toolbar_main.click(function(event) {//阻止事件冒泡
			event.stopPropagation();
			event.preventDefault();
		});
	})();

    //图片懒加载
	cayiWZK.lazy({
	    className:".lazy_img"
	});

});




