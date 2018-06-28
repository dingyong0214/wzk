/*分页异步化*/
$(function () {
    //分页
    $(document).on('click', '.page>a:not(.go)', function () {
        getPageData($(this));
        return false;
    });
    //GO 
    $(document).on('click', '.page>a.go', function () {
        var index = $.trim($(this).parent().find("input").val());
        if (isNaN(index)) {
            index = index.replace(/\D/ig, ""); 
            return false;
        }
        //超出最大的页数
        if (index > parseInt($(this).parent().data("max"))) { 
            return false;
        }

        getPageData($(this), index || 1);
        return false;
    });
    //获取数据
    function getPageData($this, index) {
         
        var container = $this.parents("#page").data("itemarea");
        
        if ($this.text() != "Go") {
            $.get($this.attr("href"), function (data) {
                $("#" + container).html(data);
                $("#hdIndex").val($("#page .current").text() == "" ? "1" : $("#page .current").text());
            });
        } else {
            var url = $("#page").data('action').replace(/[/]\d+$/, '/' + index).replace(/[/]\d+[?]/, '/' + index + '?');
            $.get(url, function (data) {
                $("#" + container).html(data);
                $("#hdIndex").val($("#page .current").text() == "" ? "1" : $("#page .current").text());
            });
        }
    }

    /*限制大小*/
    $(".limitSize").each(function () {
        $(this).bind("change paste", function () {
            if ($(this).attr("max") && parseFloat(this.value) > parseFloat($(this).attr("max")))
                this.value = $(this).attr("max");
            if ($(this).attr("min") && parseFloat(this.value) < parseFloat($(this).attr("min"))) {
                this.value = $(this).attr("min");
            }
        }).css("ime-mode", "disabled")
    });

    /*绑定只能输入数字*/
    $(".digital").each(function (event) {
        $(this).bind("keyup paste change", function () {  //keyup事件处理 
            if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40)
                return;
            this.value = this.value.replace(/\D|^0/g, '');
        }).css("ime-mode", "disabled");  //CSS设置输入法不可用
    });
    /*绑定只能输入float*/
    $(".float").each(function (event) {
        $(this).bind("paste keyup change", function () {
            if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40)
                return;
            this.value = this.value.replace(/[^0-9.]/g, '');
        }).css("ime-mode", "disabled"); //CSS设置输入法不可用  
    });
});