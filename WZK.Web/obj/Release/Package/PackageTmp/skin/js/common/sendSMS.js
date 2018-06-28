$.fn.extend({
    sendMessage: function (phone, bussinessType, codeType, verCode, callback, success, error) {
        var obj = this;
        this.click(function () { 
            if (callback()) {
                obj.text('正在获取中...').attr('disabled', 'disabled');//禁用按钮
                $.post(
                    '/Common/GetPhoneCode',
                    {
                        Mobile: $(phone).val(),
                        BussinessType: bussinessType,
                        Channel: 0,
                        CodeType: codeType,
                        VerCode: $(verCode).val()
                    },
                    function (msg) {
                        if (msg.IsPass == true) {
                            obj.data('interval', 60);
                            obj.timeOut();
                            success();
                        }
                        else {
                            error(msg);
                            obj
                                .text('获取验证码')
                                .removeAttr('disabled');
                        }
                    });
            }
            return false; //防止按钮点击事件提交表单
        });
    },
    timeOut: function (interval) {
        var $obj = this;
        if (!$obj.data('interval')) {
            $obj.data('interval', interval);
        }
        var id = setInterval(function () {
            var interval = $obj.data('interval');
            interval--;
            if (interval > 0) {
                $obj.data('interval', interval);
                $obj.text('剩余' + interval + '秒');
            }
            else {
                window.clearInterval($obj.data('intervalID'));
                $obj.text('获取验证码').removeAttr('disabled').removeData('interval');
            }
        }, 1000);
        $obj.data('intervalID', id).attr('disabled', 'disabled'); //禁用按钮;
    }
});