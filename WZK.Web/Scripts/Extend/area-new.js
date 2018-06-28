(function ($) {
    $.area = {
        change: function () {
            var $obj = $(this),
                $parent = $obj.parent();
            var index = -1;
            if ($parent.find("select").length > 0)
                index = $parent.find("select").index($obj);
            var selectId = "";
            if (index == 0)
                selectId = "City";
            else if (index == 1)
                selectId = "Area";
            else
                selectId = "Town";
          
            var grade = $parent.data('grade') || 0; //grade=区域层级 ,0无限制 1省 2省市 3省市县 4省市县镇 
            
            $obj.nextAll("select").remove();
            if (grade == 0 || index < (grade - 1)) {
                $.area.getData($obj.val(), function (options) {
                    $obj.after("<select name='" + selectId + "' id='" + selectId + "'>" + options + "</select>");
                    $obj.next().on('change', $.area.change);
                });
            }
            $("#" + $parent.data('areaid')).val($obj.val()).focusout();
        },
        //初始化
        initialize: function (area, value) {
            this.getData(area.val(), function (options) {
                area.html("<select>" + options + "</select>");
            });
            area.on('change', this.change);

        },
        //获取地区信息
        getData: function (id, callback) {
            $.getJSON('/Common/GetAreaList?parentCode=' + id, function (msg) {
                if (msg.length > 0) {
                    var option = new StringBuilder();
                    option.Append("<option value='-1' selected='selected'>--请选择--</option>");
                    $(msg).each(function () {
                        option.AppendFormat("<option value='{0}'>{1}</option>", this.id, this.name);
                    });
                    callback(option.ToString());
                }
            });
        },
        //获取地区名
        getAreasName: function (id, callback) {
            if ($.trim(id).length > 0) {
                $.getJSON('/Common/GetAreasName?id=' + id, function (msg) {
                    callback(msg.name);
                });
            }
        },
        //获取省市区街道
        getAreasCode: function (id, callback) {
            if ($.trim(id).length > 0) {
                $.getJSON('/Common/GetAreasCode?childCode=' + id, function (msg) {
                    callback(msg);
                });
            }
        },
    }
    //修改地址 
    $.fn.modifyArea = function (isavail) {
        var $obj = this;
        var town = 0,//镇
            county = 0,//区
            province = 0,//省
            city = 0,//市 
            grade = $obj.parent().data('grade') || 4; //等级 1.省 2.省市 3.省市区 4.省市区镇
        var disabled = isavail == 'False' ? "disabled=disabled" : "";
        var $siblings = $obj.siblings(),
            $input = $obj.parent().find("input");

        //获取所有省
        $.area.getData('100000', function (options) {
            $obj.before("<select name='Province' id='Province' " + disabled + ">" + options + "</select>");
            $siblings = $obj.siblings();
            if ($.trim($obj.val()).length > 0 && $.trim($obj.val()) != "0") {
                $.area.getAreasCode($obj.val(), function (areaview) {
                    province = areaview.Province
                    city = areaview.City;
                    county = areaview.Area;
                    town = areaview.Town; 
                   
                    $siblings.eq(0).val(province);
                    if (grade > 1) {
                        //获取所有市
                        $.area.getData(province, function (options) {
                            $obj.before("<select name='City' id='City' " + disabled + ">" + options + "</select>");
                            $siblings = $obj.siblings();
                            $siblings.eq(1).val(city);
                            if (grade > 2) {
                                //获取所有县
                                $.area.getData(city, function (options) {
                                    $obj.before("<select name='Area' id='Area' " + disabled + ">" + options + "</select>");
                                    $siblings = $obj.siblings();
                                    $siblings.eq(2).val(county);
                                    if (grade > 3) {
                                        //获取所有街道
                                        $.area.getData(county, function (options) {
                                            $obj.before("<select name='Area' id='Town' " + disabled + ">" + options + "</select>");
                                            $siblings = $obj.siblings();
                                            $siblings.eq(3).val(town);
                                            $input.val(town).focusout();
                                            $siblings.on('change', $.area.change);
                                        }); 
                                    } else {
                                        $input.val(county).focusout();
                                        $siblings.on('change', $.area.change);
                                    } 
                                });
                            } else {
                                $input.val(city).focusout();
                                $siblings.on('change', $.area.change);
                            }
                        }); 
                    } else {
                        $input.val(province).focusout();
                        $siblings.on('change', $.area.change);
                    } 
                });
            } else {
                $siblings.on('change', $.area.change);
            }
        });
    }
})(jQuery);
