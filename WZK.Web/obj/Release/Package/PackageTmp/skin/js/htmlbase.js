/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-10-29 11:38:32
 * @version $Id$
 */
var basehtml={
	create:function(func){
		var lines = new String(func);
		return lines.substring(lines.indexOf("/*") + 2, lines.lastIndexOf("*/"));
	},
	cssjs:function(){
		/*
			<meta name="author" content="擦一擦www.cayiVote.com">
			<meta name="renderer" content="webkit">
			<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
			<link rel="stylesheet" type="text/css" href="http://res.cayiVote.com/css/public/1.0.0/header_footer.css">
			<link rel="stylesheet" type="text/css" href="http://res.cayiVote.com/css/public/1.0.0/index.css">
			<link rel="stylesheet" type="text/css" href="http://res.cayiVote.com/css/public/1.0.0/base.css">
			<link rel="stylesheet" type="text/css" href="http://res.cayiVote.com/css/public/1.0.0/content.css">
			<script src="http://res.cayiVote.com/js/public/1.0.0/jquery.min.js"></script>
			<script src="http://res.cayiVote.com/js/public/1.0.0/jquery.lazyload.js"></script>
			<script type="text/javascript" src="http://res.cayiVote.com/js/public/1.0.0/common.js"></script>
			<script src="http://res.cayiVote.com/js/public/1.0.0/jquery.form.js"></script>
			<script type="text/javascript" src="http://res.cayiVote.com/js/layer/1.0.0/layer.js"></script>
			<script type="text/javascript" src="http://res.cayiVote.com/js/m/1.0.0/base.js"></script>

			<script type="text/javascript" src="http://res.cayiVote.com/js/public/1.0.0/district.js"></script>


			<script type="text/javascript" src="http://res.cayiVote.com/js/ueditor/1.0.0/ueditor.config.js"></script>
			<script type="text/javascript" src="http://res.cayiVote.com/js/ueditor/1.0.0/ueditor.all.min.js"></script>
			<script>window.cDomain = { "CayicaMainSite": "http://www.cayiVote.com", "CayicaMemberSite": "http://member.cayiVote.com", "CayicaImageSite": "http://imgs.cayiVote.com", "CayicaResSite": "http://res.cayiVote.com", "CayicaMobileSite": "http://m.cayiVote.t", "CayicaWeixinSite": "http://wx.cayiVote.t", "CayicaPassportSite": "http://passport.cayiVote.com", "CayicaCartSite": "http://cart.cayiVote.com", "CookieDomain": ".cayiVote.t" };</script>
		*/
	},
	header:function(){
		/*
		  <div class="v3_top_wrap">
		      <div class="v3_top_main">
		          <div class="t_left">
		              <a href="javascript:;" class="t_cayica">
		                  <span></span>
		                  收藏擦一擦
		              </a>
		              <span class="login_info">
		                  <a href="javascript:;" class="v3_t_color">请登录</a>
		                  <a href="javascript:;">免费注册</a>
		              </span>

		          </div>
		          <div class="t_right">
		              <div class="t_right_m">
		                  <a href="http://passport.cayiVote.com/register/company">商家认证</a>

		              </div>
		              <div class="t_right_m">
		                  <div class="t_right_show">
		                      <a href="http://member.cayiVote.com/Bought">订单管理</a> <i>&nbsp;</i>
		                  </div>
		                  <div class="t_right_hide">
		                      <p>
		                          <a href="http://member.cayiVote.com/Bought">我是买家</a>
		                      </p>
		                      <p>
		                          <a href="http://member.cayiVote.com/Sold">我是卖家</a>
		                      </p>
		                  </div>
		              </div>
		              <div class="t_right_m top_cart">
		                  <a href="http://cart.cayiVote.com/"><span></span>购物车<i>12</i></a>
		              </div>
		              <div class="t_right_m">
		                  <div class="t_right_show">
		                      <a href="javascript:;">我的收藏</a> <i>&nbsp;</i>
		                  </div>
		                  <div class="t_right_hide">
		                      <p>
		                          <a href="http://member.cayiVote.com/collection/index/1/0/">我收藏的宝贝</a>
		                      </p>
		                      <p>
		                          <a href="http://member.cayiVote.com/collection/shop/0/1/0/0">我收藏的店铺</a>
		                      </p>
		                  </div>
		              </div>
		              <div class="t_right_m">
		                  <a href="javascript:;">消息</a>
		              </div>
		          </div>
		      </div>
		  </div>
		  <script>login.State();</script>
		  <div class="v3_search_w">
		      <div class="v3_logo">
		          <h1 title="擦一擦——有闲有趣的生活服务交易平台">
		              <a href="http://www.cayiVote.com/">
		                  <img style="float:right;" alt="擦一擦——有闲有趣的生活服务交易平台" src="http://imgs.cayiVote.com/img/1.0.0/logo-logo.png" />

		              </a>
		          </h1>

		      </div>
		      <div class="v3_search_m">
		          <input type="text" id="searchText"><a href="javascript:;" id="search">搜索</a>
		      </div>
		      <div class="v3_gwch head_cart">
		          <a href="http://cart.cayiVote.com/">我的购物车<span>33</span>件</a>
		      </div>
		  </div>
		  <div class="v3_nav_w">
		      <div class="v3_nav">
		          <div class="v3_nav_all">
		              <a href="javascript:;" class="v3_nav_t">全部分类</a>
		              <div class="v3_side_nav">
		                  <ul>
		                      <li><div class="v3_nav_show"><span><a href="/search-46-0-0-0-1-1-1.html">家私</a></span><a href="/search-48-0-0-0-1-1-1.html">床</a><a href="/search-49-0-0-0-1-1-1.html">床垫</a><i></i></div><div class="v3_nav_hide"><a href="/search-50-0-0-0-1-1-1.html">沙发</a><a href="/search-51-0-0-0-1-1-1.html">鞋架</a><a href="/search-52-0-0-0-1-1-1.html">电视柜</a><a href="/search-53-0-0-0-1-1-1.html">衣柜</a><a href="/search-58-0-0-0-1-1-1.html">电脑桌</a><a href="/search-59-0-0-0-1-1-1.html">餐桌</a><a href="/search-60-0-0-0-1-1-1.html">椅子</a><a href="/search-69-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-57-0-0-0-1-1-1.html">家电</a></span><a href="/search-61-0-0-0-1-1-1.html">洗衣机</a><a href="/search-62-0-0-0-1-1-1.html">冰箱</a><i></i></div><div class="v3_nav_hide"><a href="/search-63-0-0-0-1-1-1.html">空调</a><a href="/search-64-0-0-0-1-1-1.html">电视机</a><a href="/search-65-0-0-0-1-1-1.html">电饭煲</a><a href="/search-66-0-0-0-1-1-1.html">电磁炉</a><a href="/search-67-0-0-0-1-1-1.html">电水壶</a><a href="/search-68-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-1-0-0-0-1-1-1.html">3C数码</a></span><a href="/search-7-0-0-0-1-1-1.html">电脑</a><a href="/search-8-0-0-0-1-1-1.html">相机</a><i></i></div><div class="v3_nav_hide"><a href="/search-9-0-0-0-1-1-1.html">平板电脑</a><a href="/search-10-0-0-0-1-1-1.html">影音娱乐</a><a href="/search-11-0-0-0-1-1-1.html">数码配件</a><a href="/search-12-0-0-0-1-1-1.html">手机</a><a href="/search-13-0-0-0-1-1-1.html">笔记本</a><a href="/search-14-0-0-0-1-1-1.html">手机配件</a><a href="/search-15-0-0-0-1-1-1.html">智能设备</a><a href="/search-70-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-6-0-0-0-1-1-1.html">服装鞋包</a></span><a href="/search-36-0-0-0-1-1-1.html">女装</a><a href="/search-37-0-0-0-1-1-1.html">男装</a><i></i></div><div class="v3_nav_hide"><a href="/search-38-0-0-0-1-1-1.html">内衣</a><a href="/search-39-0-0-0-1-1-1.html">女鞋</a><a href="/search-40-0-0-0-1-1-1.html">男鞋</a><a href="/search-41-0-0-0-1-1-1.html">女包</a><a href="/search-42-0-0-0-1-1-1.html">男包</a><a href="/search-43-0-0-0-1-1-1.html">户外装备</a><a href="/search-44-0-0-0-1-1-1.html">配饰</a><a href="/search-45-0-0-0-1-1-1.html">护肤</a><a href="/search-75-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-2-0-0-0-1-1-1.html">家具日用</a></span><a href="/search-16-0-0-0-1-1-1.html">床上用品</a><a href="/search-17-0-0-0-1-1-1.html">日用品</a><i></i></div><div class="v3_nav_hide"><a href="/search-18-0-0-0-1-1-1.html">室内装饰</a><a href="/search-19-0-0-0-1-1-1.html">健身器材</a><a href="/search-20-0-0-0-1-1-1.html">宠物生活</a><a href="/search-71-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-5-0-0-0-1-1-1.html">母婴玩具</a></span><a href="/search-30-0-0-0-1-1-1.html">童装</a><a href="/search-31-0-0-0-1-1-1.html">玩具乐器</a><i></i></div><div class="v3_nav_hide"><a href="/search-32-0-0-0-1-1-1.html">喂养用品</a><a href="/search-33-0-0-0-1-1-1.html">童鞋</a><a href="/search-34-0-0-0-1-1-1.html">孕妇专区</a><a href="/search-35-0-0-0-1-1-1.html">洗护用品</a><a href="/search-74-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-4-0-0-0-1-1-1.html">办公用品</a></span><a href="/search-27-0-0-0-1-1-1.html">办公设备</a><a href="/search-28-0-0-0-1-1-1.html">文具用品</a><i></i></div><div class="v3_nav_hide"><a href="/search-29-0-0-0-1-1-1.html">办公耗材</a><a href="/search-73-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-3-0-0-0-1-1-1.html">书籍文体</a></span><a href="/search-21-0-0-0-1-1-1.html">生活</a><a href="/search-22-0-0-0-1-1-1.html">少儿动漫</a><i></i></div><div class="v3_nav_hide"><a href="/search-23-0-0-0-1-1-1.html">经管励志</a><a href="/search-24-0-0-0-1-1-1.html">图书</a><a href="/search-25-0-0-0-1-1-1.html">人文社科</a><a href="/search-26-0-0-0-1-1-1.html">少儿教育</a><a href="/search-72-0-0-0-1-1-1.html">其他</a></div></li>
		                      <li><div class="v3_nav_show"><span><a href="/search-78-0-0-0-1-1-1.html">其他</a></span><a href="/search-80-0-0-0-1-1-1.html">其他</a><i></i></div></li>
		                  </ul>
		              </div>
		          </div>
		          <ul class="v3_line_nav">
		              <li><a href="/">首页</a></li>
		              <li><a href="/publish.html">发闲置</a></li>
		              <li><a href="/list/near">附近闲置</a></li>
		              <!--<li><a href="javascript:;">论坛</a></li>-->
		          </ul>
		      </div>
		  </div>
		*/
	},
	footer:function(){
		/*
		    <footer>
		        <div class="v3_youqin_w">
		            <div class="v3_youqin_m">
		                <div class="v3_y_left">
		                    <ul>
		                        <li class="v3_first"><a href="javascript:;">关于我们</a></li>
		                        <li><a href="/html/AboutSite.html">网站简介</a></li>
		                        <li><a href="/html/Contact.html">联系我们</a></li>
		                        <li><a href="/html/Rules.html">规则中心</a></li>
		                    </ul>
		                    <ul>
		                        <li class="v3_first"><a href="javascript:;">新手入门</a></li>
		                        <li><a href="/html/Question.html">常见问题</a></li>
		                        <li><a href="/html/Safety.html">账户管理</a></li>
		                    </ul>
		                    <ul>
		                        <li class="v3_first"><a href="javascript:;">交易指南</a></li>
		                        <li><a href="/html/Process.html#c1">我是买家</a></li>
		                        <li><a href="/html/Process.html#c2">我是卖家</a></li>
		                        <li><a href="/html/Process.html#c3">我是商家</a></li>
		                    </ul>
		                    <ul>
		                        <li class="v3_first"><a href="javascript:;">友情链接</a></li>
		                        <li><a href="http://www.yitask.com" target="_blank">阿拉神灯</a></li>
		                        <li><a href="http://www.clubo.cn" target="_blank">克拉博</a></li>
		                        <li><a href="http://www.zczj.com" target="_blank">众筹之家</a></li>
		                    </ul>
		                </div>
		                <div class="v3_y_right">
		                    <div class="v3_erweima">&nbsp;</div>
		                    <p>擦一擦服务号</p>
		                </div>
		            </div>
		        </div>
		        <div class="v3_bottom_w">
		            <div class="v3_bottom_m">
		                <div class="v3_partner">
		                    <span>合作伙伴</span>
		                    <a href="javascript:;" class="v3_p_wx"><i></i>微信</a>
		                    <a href="javascript:;" class="v3_p_zfb"><i></i>支付宝</a>
		                    <a href="javascript:;" class="v3_p_zx"><i></i>中信银行</a>
		                </div>
		                <div class="v3_copy">
		                    深圳塔斯克电子商务有限公司技术支持
		                </div>
		            </div>
		        </div>
		    </footer>
		    <input type="hidden" id="lazy_img" name="name" value="http://imgs.cayiVote.com/img/1.0.0/c2.png" />
		    <script>
			    $(function () {
			        //搜索
			        $("#search").click(function () {
			            window.location.href = "/List/ProductList?" + "keys=" + removeHTMLTag($("#searchText").val());
			        })
			        //回车搜索
			        $('#searchText').bind('keypress', function (event) {
			            if (event.keyCode == "13") {
			                window.location.href = "/List/ProductList?" + "keys=" + removeHTMLTag($("#searchText").val());
			            }
			        });
			    })
			    function removeHTMLTag(str) {
			        str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
			        str = str.replace(/[ | ]* /g, ' '); //去除行尾空白
			        //str = str.replace(/ [\s| | ]* /g,' '); //去除多余空行
			        str = str.replace(/ /ig, '');//去掉
			        return str;
			    }
			</script>
		 */
	},
	cssjshtml:function(){
		return this.create(this.cssjs);
	},
	headerhtml:function(){
		return this.create(this.header);
	},
	footerhtml:function(){
		return this.create(this.footer);
	}

};
