/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-10-29 11:38:32
 * @version $Id$
 */
var cssjshtml = function() {
	var fun = function() {
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
	}
	var lines = new String(fun);
	return lines.substring(lines.indexOf("/*") + 2, lines.lastIndexOf("*/"));
}
document.write(cssjshtml());