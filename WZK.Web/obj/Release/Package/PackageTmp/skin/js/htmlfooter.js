/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-10-29 11:38:32
 * @version $Id$
 */

var footerhtml = function() {
	var fun = function() {
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
	}
	var lines = new String(fun);

	return lines.substring(lines.indexOf("/*") + 2, lines.lastIndexOf("*/"));
}
document.write(footerhtml());