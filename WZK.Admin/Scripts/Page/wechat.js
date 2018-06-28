$(function () {
    //选择微信内容
    $("#choiceContent").click(function () {
        $.get("/WeChat/ContentList", { isList: false }, function (view) {
            layer.open({
                type: 1,
                title: "选择内容",
                skin: 'layui-layer-rim', //加上边框
                area: ['1024px', '610px'], //宽高
                content: view,
                btn: ['选择', '取消'],
                yes: function () {
                    var id = $("#view input[name='choice']:checked").val(); 
                    if (id != undefined) {
                        $.get("/WeChat/ContentDetail", { id: id, isModify: false }, function (view) {
                            $("#menucontent").after(view);
                            $("#menucontent").hide();
                        });
                        layer.closeAll();
                    } else {
                        layer.msg("请选择内容", { time: 1000, icon: 2 });
                    }
                },
                cancel: function () {
                    layer.closeAll();
                }
            });
        });
    });
});