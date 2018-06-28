$(function () {
    /*注销、退出系统*/
    $("#logout").on('click', function () {
        $.getJSON('/Passport/logOut', {}, function (data) {
            location.replace(data.url);
        });
    });
    //全局ajax事件
    var layerLoad;
    $(document).ajaxSend(function () {
        layerLoad = layer.load(2, { time: 60 * 1000 });
    });
    $(document).ajaxComplete(function () {
        layer.close(layerLoad);
    });

    $("#txtStarTime").focus(function () {
        WdatePicker({ onpicked: function () { txtEndTime.focus(); }, maxDate: '#F{$dp.$D(\'txtEndTime\')||\'%y-%M-%d\'}' });
    });
    $("#txtEndTime").focus(function () {
        WdatePicker({ minDate: '#F{$dp.$D(\'txtStarTime\')}', maxDate: '%y-%M-%d' });
    });

    $(document).on('click', '.img-open', function () {
        var $SRC = $(this).attr('src');
        layer.open({
            type: 1,
            title: false,
            closeBtn: 0,
            maxWidth: "430",
            area: 'auto',
            skin: 'layui-layer-nobg', //没有背景色
            shadeClose: true,
            content: "<img alt=\"\" src=\"" + $SRC + "\" />"
        });
    }); 
});
//图片上传
function uploadImg(inputFile, callback, action,type) {
    if (!action || action.length == 0) {
        action = "UploadImg";
    }
    var data = new FormData();
    var file = $("#" + inputFile)[0].files[0];
    if (file.size <= 1024 * 1024 * 4) {
        data.append('upload_file', file);
        data.append('type', type);
        $.ajax({
            url: '/Common/' + action,
            type: 'POST',
            data: data,
            contentType: false,
            processData: false,
            success: function (data) { callback(data); }
        });
    } else {
        layer.msg("上传图片不能超过4M", { icon: 7 });
    }
}
//图片上传
function uploadMuchImg(inputFile, callback, action, type,length) {
    if (!action || action.length == 0) {
        action = "UploadMuchImg";
    }
    var data = new FormData();
    var file = $("#" + inputFile)[0].files;
    if (file.length + length > 9) {
        layer.msg("最多只能上传9张图片", { icon: 7 });
        return;
    }
    var totalSize = 0;
    for (var i=0, item;item=file[i++];){
        data.append('upload_file', item);
        totalSize += item.size;
    }
    if (totalSize <= 1024 * 1024 * 2 * 9) {
        //data.append('upload_file', file);
        data.append('type', type);
        $.ajax({
            url: '/Common/' + action,
            type: 'POST',
            data: data,
            contentType: false,
            processData: false,
            success: function (data) { callback(data); }
        });
    } else {
        layer.msg("上传图片不能超过18M", {icon:7});
    }
}

function ShowModelStateErrorMsg(obj, msg) {
    $('span[data-valmsg-for="' + obj + '"]').removeClass('field-validation-valid').addClass('field-validation-error').html('<span for="' + obj + '" class="">' + msg + '</span>');
}
function HideModelStateErrorMsg(obj) {
    $('span[data-valmsg-for="' + obj + '"]').addClass('field-validation-valid').removeClass('field-validation-error').html('');
}
function HideAllModelStateErrorMsg() {
    $('.field-validation-error').addClass('field-validation-valid').removeClass('field-validation-error').html('');
}
//处理数字保留2位小数，强制保留2位小数不够补上.00 
function toDecimal2(x) {
    x = $.trim(x);
    var f = parseFloat( x);
    if (isNaN(f)) {
        return "0.00";
    }
    var f = Math.round(x * 100) / 100;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 2) {
        s += '0';
    }
    return s;
}
String.prototype.isMobile = function () {
    return /^(?:13\d|14\d|15\d|17\d|18\d)\d{5}(\d{3}|\*{3})$/.test(this);
};
jQuery.validator.addMethod('notEqualTo', function (value, element, param) {
    //意思是表单值为空时也能通过验证
    //但，如果表单有值，就必须满足||后面的条件，否则返回false
    return this.optional(element) || value != $(param).val();
});

//第一个参数是jquery验证扩展方法名
//第二个参数与rule.ValidationParameters["other"]中的key对应
//option是指ModelClientValidationRule对象实例
jQuery.validator.unobtrusive.adapters.add('notequalto', ['other'], function (options) {
    options.rules['notEqualTo'] = '#' + options.params.other;
    if (options.message) {
        options.messages['notEqualTo'] = options.message;
    }
});