/**
 * 
 * @authors Your Name (you@example.org)
 * @date    2015-04-30 08:26:22
 * @version $Id$
 */
$(function(){
	//类型、区域、分类
	$('.buy_right a').click(function(event) {
		$(this).parent().addClass('selected').siblings().removeClass('selected');
		if($(this).parent().index()==0){
			$(this).parent().parent().siblings('.b_erji').hide();
		}else{
			$(this).parent().parent().siblings('.b_erji').show();
		}
		var typeName = $(this).parent().parent().attr("id");
		switch (typeName) {
		    case "infoType"://类型
		        pageParamUrl("infoType", $(this).attr("data-value"));
		        break;
		    case "areaId"://区域
		        if ($(this).attr("data-value") == '-1') {
		            $("#hidbusiId").val(-1);
		            pageParamUrl("areaId", $(this).attr("data-value"));
		        } else {
		            //$("#hidareaId").val($(this).attr("data-value"));
		            $("#hidbusiId").val(-1);
		           pageParamUrl("areaId", $(this).attr("data-value"));
		        }
		        break;
		    case "busiId"://商圈
		        pageParamUrl("busiId", $(this).attr("data-value"));
		        break;
		    case "firstLevelType"://物品一级分类ID
		        if ($(this).attr("data-value") == '-1') {
		            $("#hidsecondLevelType").val(-1);
		            pageParamUrl("firstLevelType", $(this).attr("data-value"));
		        } else {
		            //  $("#hidfirstLevelType").val($(this).attr("data-value"));
		            $("#hidsecondLevelType").val("-1");
		            pageParamUrl("firstLevelType", $(this).attr("data-value"));
		        }
		        break;
		    case "secondLevelType"://物品二级分类ID
		        pageParamUrl("secondLevelType", $(this).attr("data-value"));
		        break;
		    case "priceType"://价格
		        pageParamUrl("priceType", $(this).attr("data-value"));
		        break;
		    default:

		}
		return false;
		return false;
	});
	$(".b_erji a").click(function(event) {
		$(this).addClass('selected').siblings().removeClass('selected');
		return false;
	});

	//排序
	$('.buy_sort_l span').click(function(event) {
	    $(this).addClass('selected').siblings().removeClass('selected');
	    var value = $(this).attr("data-value");
	    pageParamUrl("sort", value);
	});

	//平铺、大图中 li 右边框样式
	$('#buy_list li').each(function() {
		 if (($(this).index()+1)%4==0) {
		 	$(this).css({
		 		"border-right": 'none'
		 	});
		 };
	});

	//平铺、大图
	$('.buy_sort_r span').click(function(event) {
		var i=$(this).index();
		$(this).addClass('selected').siblings().removeClass('selected');
		if (i>0) {
			$('#buy_list').addClass('buy_list1').removeClass('buy_list');
		}else{
			$('#buy_list').addClass('buy_list').removeClass('buy_list1');
		};

		pageParamUrl("arrange", i);
	});


	//会员中心----基本资料
	//编辑资料
	$(".m_info a").click(function(event) {
		$(".m_bianji").show();
		$(".m_wancheng").hide();
		$(".modify_btn").show();
		return false;
	});
	$(".m_btn_cancel").click(function(event) {
		$(".m_bianji").hide();
		$(".m_wancheng").show();
		$(".modify_btn").hide();
		return false;
	});
	$(".m_btn_save").click(function(event) {
		var $bianji=$(".m_bianji"),
			$wancheng=$(".m_wancheng"),
			txt='',
			txt1='';
		$.each($bianji.eq(2).find("span"), function(index, val) {
			if(index<=1){
			 txt+=($(this).text()+'-');
			}else{
				txt+=($(this).text());
			}
		});
		$.each($bianji.eq(3).find("span"), function(index, val) {
			if(index<=2){
			 txt1+=($(this).text()+'-');
			}else{
				txt1+=($(this).text());
			}
		});
		var txt1List = txt1.split('-');
		txt1 = '';
		if ($("#ProvinceDiv").is(":visible"))
		{
		    txt1 += txt1List[0];
		}
		if ($("#CityDiv").is(":visible")) {
		    txt1 +=("-"+txt1List[1]);
		}
		if ($("#AreaDiv").is(":visible")) {
		    txt1 += ("-" + txt1List[2]);
		}
		if ($("#BusiDiv").is(":visible")) {
		    txt1 += ("-" + txt1List[3]);
		}
		var name=$bianji.eq(0).find("input").val(),
			sex=$bianji.eq(1).find("input:checked").val(),
			birth=txt,
			weizhi=txt1,
			jutiweizhi=$bianji.eq(4).find("input").val(),
			qianming=$bianji.eq(5).find("input").val();
		$wancheng.eq(0).text(name);
		$wancheng.eq(1).text(sex);
		$wancheng.eq(2).text(birth);
		$wancheng.eq(3).text(weizhi);
		$wancheng.eq(4).text(jutiweizhi);
		$wancheng.eq(5).text(qianming);
	
		return false;
	});

	//出生年月
	$(".info_birth").click(function(event) {
		$(this).children('.info_birth_box').addClass('xianshi');
	});
	$(".info_birth").mouseleave(function(event) {
		$(this).children('.info_birth_box').removeClass('xianshi');
	});
	$(".info_birth_box li").click(function(event) {
		event.stopPropagation();
		$(this).parent().removeClass('xianshi');
		$(this).parents(".info_birth").find('span').text($(this).text());
		return false;
	});

	//位置选择
	$(".info_weizhi").click(function(event) {
		$(this).children('.info_weizhi_box').addClass('xianshi');
	});
	$(".info_weizhi").mouseleave(function(event) {
		$(this).children('.info_weizhi_box').removeClass('xianshi');
	});
	$(".info_weizhi_box li").click(function(event) {
		event.stopPropagation();
		$(this).parent().removeClass('xianshi');
		$(this).parents(".info_weizhi").find('span').text($(this).text());
		return false;
	});


});
