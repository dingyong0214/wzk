/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-10-29 11:38:32
 * @version $Id$
 */
var cssjshtml = function() {
	var fun = function() {
		/*
		    <meta name="author" content="擦一擦www.cayiWZK.com">
		    <meta name="renderer" content="webkit">
		    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		    <link rel="stylesheet" type="text/css" href="http://res.cayiWZK.com/css/public/1.0.0/header_footer.css">
		    <link rel="stylesheet" type="text/css" href="http://res.cayiWZK.com/css/public/1.0.0/index.css">
		    <link rel="stylesheet" type="text/css" href="http://res.cayiWZK.com/css/public/1.0.0/base.css">
		    <link rel="stylesheet" type="text/css" href="http://res.cayiWZK.com/css/public/1.0.0/content.css">
		    <script src="http://res.cayiWZK.com/js/public/1.0.0/jquery.min.js"></script>
		    <script src="http://res.cayiWZK.com/js/public/1.0.0/jquery.lazyload.js"></script>
		    <script type="text/javascript" src="http://res.cayiWZK.com/js/public/1.0.0/common.js"></script>
		    <script src="http://res.cayiWZK.com/js/public/1.0.0/jquery.form.js"></script>
		    <script type="text/javascript" src="http://res.cayiWZK.com/js/layer/1.0.0/layer.js"></script>
		    <script type="text/javascript" src="http://res.cayiWZK.com/js/m/1.0.0/base.js"></script>

		    <script type="text/javascript" src="http://res.cayiWZK.com/js/public/1.0.0/district.js"></script>


		    <script type="text/javascript" src="http://res.cayiWZK.com/js/ueditor/1.0.0/ueditor.config.js"></script>
		    <script type="text/javascript" src="http://res.cayiWZK.com/js/ueditor/1.0.0/ueditor.all.min.js"></script>
		    <script>window.cDomain = { "CayicaMainSite": "http://www.cayiWZK.com", "CayicaMemberSite": "http://member.cayiWZK.com", "CayicaImageSite": "http://imgs.cayiWZK.com", "CayicaResSite": "http://res.cayiWZK.com", "CayicaMobileSite": "http://m.cayiWZK.t", "CayicaWeixinSite": "http://wx.cayiWZK.t", "CayicaPassportSite": "http://passport.cayiWZK.com", "CayicaCartSite": "http://cart.cayiWZK.com", "CookieDomain": ".cayiWZK.t" };</script>
		*/
	}
	var lines = new String(fun);
	return lines.substring(lines.indexOf("/*") + 2, lines.lastIndexOf("*/"));
}
document.write(cssjshtml());