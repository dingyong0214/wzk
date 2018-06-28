/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-10-29 11:38:32
 * @version $Id$
 */

var headerhtml = function() {
	var fun = function() {
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
		                  <a href="http://passport.cayiWZK.com/register/company">商家认证</a>

		              </div>
		              <div class="t_right_m">
		                  <div class="t_right_show">
		                      <a href="http://member.cayiWZK.com/Bought">订单管理</a> <i>&nbsp;</i>
		                  </div>
		                  <div class="t_right_hide">
		                      <p>
		                          <a href="http://member.cayiWZK.com/Bought">我是买家</a>
		                      </p>
		                      <p>
		                          <a href="http://member.cayiWZK.com/Sold">我是卖家</a>
		                      </p>
		                  </div>
		              </div>
		              <div class="t_right_m top_cart">
		                  <a href="http://cart.cayiWZK.com/"><span></span>购物车<i>0</i></a>
		              </div>
		              <div class="t_right_m">
		                  <div class="t_right_show">
		                      <a href="javascript:;">我的收藏</a> <i>&nbsp;</i>
		                  </div>
		                  <div class="t_right_hide">
		                      <p>
		                          <a href="http://member.cayiWZK.com/collection/index/1/0/">我收藏的宝贝</a>
		                      </p>
		                      <p>
		                          <a href="http://member.cayiWZK.com/collection/shop/0/1/0/0">我收藏的店铺</a>
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
		              <a href="http://www.cayiWZK.com/">
		                  <img style="float:right;" alt="擦一擦——有闲有趣的生活服务交易平台" src="http://imgs.cayiWZK.com/img/1.0.0/logo-logo.png" />

		              </a>
		          </h1>

		      </div>
		      <div class="v3_search_m">
		          <input type="text" id="searchText"><a href="javascript:;" id="search">搜索</a>
		      </div>
		      <div class="v3_gwch head_cart">
		          <a href="http://cart.cayiWZK.com/">我的购物车<span>0</span>件</a>
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
	}
	var lines = new String(fun);

	return lines.substring(lines.indexOf("/*") + 2, lines.lastIndexOf("*/"));
}
document.write(headerhtml());