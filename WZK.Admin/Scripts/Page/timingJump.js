/// <reference path="../../jquery-1.11.1.min.js" />
$.fn.extend({
    timingJump: function (time, url, interval) {
        var $obj = $(this);
        if (!interval)
            interval = 1000;

        var i = setInterval(function () {
            $obj.html(time);
            time--;
            if (time <= 0) {
                window.clearInterval(i);
                location.href = url;
            }
        }, interval);
    }
});