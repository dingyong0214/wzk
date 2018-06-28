(function ($) {
    $.area = {
        change: function (x) {
            var $obj = $(this);
            $obj.nextAll("select").remove();
            var target = $obj.nextAll("input:eq(0)");
            $.area.getData($obj.val(), function (options) {
                //指定深度
                var depth = parseInt(target.attr("depth"));
                if (isNaN(depth))
                    depth = 3;
                var depthVal = parseInt($obj.val().substr((depth - 1) * 2, depth * 2));
                if (depthVal <= 0)
                    $obj.after("<select>" + options + "</select>");
                $obj.next().on('change', $.area.change);
            });
            target.val($obj.val());
            FillArea();
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
            $.getJSON('/Common/GetChildAreas?id=' + id, function (msg) {
                if (msg.length > 0) {
                    var option = new StringBuilder();
                    option.Append("<option value='-1'>--请选择--</option>");
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
    }

    //修改地址
    $.fn.modifyArea = function (isGetAreaName, disabled, depth) {
        var $obj = this;
        var county = $obj.val(),
            province = county.substr(0, 2) + '0000',//省
            city = county.substr(0, 4) + '00';//市 
        //获取所有省
        $.area.getData('A1560000', function (options) {
            $obj.before("<select name='Province' id='Province' " + (disabled ? "disabled='disabled'" : "") + ">" + options + "</select>");
            if (isGetAreaName) {
                FillArea();
            }
            if ($.trim(county).length > 0 && $.trim(county) != "-1") {
                //获取所有市
                $.area.getData(province, function (options) {
                    $obj.before("<select name='City' id='City' " + (disabled ? "disabled='disabled'" : "") + ">" + options + "</select>");
                    if (isGetAreaName) {
                        FillArea();
                    }
                    //获取所有县
                    if (typeof (depth) == "undefined" || depth == 3) {
                        $.area.getData(city, function (options) {
                            $obj.before("<select class='input-small2 valid' " + (disabled ? "disabled='disabled'" : "") + ">" + options + "</select>");
                            var $siblings = $obj.siblings("select");
                            $siblings.eq(0).val(province);
                            $siblings.eq(1).val(city);
                            $siblings.eq(2).val(county);
                            if (isGetAreaName) {
                                FillArea();
                            }
                            $siblings.on('change', $.area.change);
                        });
                    } else {
                        var $siblings = $obj.siblings("select");
                        $siblings.eq(0).val(province);
                        $siblings.eq(1).val(city);
                        $siblings.eq(2).val(county);
                    }
                });
            }
            else {
                $obj.siblings().on('change', $.area.change);
            }
        });
    }
})(jQuery);

function FillArea() {
    $("#Area").val
        (($("#Province").find("option:selected").text() == "--请选择--" ? "" : $("#Province").find("option:selected").text())
        + ($("#Province").next("select").find("option:selected").text() == "--请选择--" ? "" : $("#Province").next("select").find("option:selected").text())
        + ($("#Province").next("select").next("select").find("option:selected").text() == "--请选择--" ? "" : $("#Province").next("select").next("select").find("option:selected").text()));
}