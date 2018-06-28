(function ($) {
    $.extend({
        ms_DatePicker: function (options) {
            var defaults = {
                YearSelector: "#sel_year",
                MonthSelector: "#sel_month",
                DaySelector: "#sel_day",
                FirstValue: 0,
                YearFirstText: "年",
                MonthFirstText: "月",
                DayFirstText: "日", 
                YearSelecedValue: 0,
                MonthSelecedValue: 0,
                DaySelecedValue: 0
            };
            var opts = $.extend({}, defaults, options);
            $YearSelector = $(opts.YearSelector),
            $MonthSelector = $(opts.MonthSelector),
            $DaySelector = $(opts.DaySelector),
            YearFirstText = opts.YearFirstText,
            MonthFirstText = opts.MonthFirstText,
            DayFirstText = opts.DayFirstText,
            FirstValue = opts.FirstValue,
            YearSelecedValue = opts.YearSelecedValue,
            MonthSelecedValue = opts.MonthSelecedValue,
            DaySelecedValue = opts.DaySelecedValue;


            // 绑定初始值
            var yearStr = "<option value=\"" + FirstValue + "\">" + YearFirstText + "</option>";
            var monthStr = "<option value=\"" + FirstValue + "\">" + MonthFirstText + "</option>";
            var dayStr = "<option value=\"" + FirstValue + "\">" + DayFirstText + "</option>";
            $YearSelector.html(yearStr);
            $MonthSelector.html(monthStr);
            $DaySelector.html(dayStr);

            // 绑定年
            var yearNow = new Date().getFullYear(); 
            for (var i = yearNow; i >= 1900; i--) {
                var sed = YearSelecedValue == i ? "selected=selected" : "";
                yearStr = "<option value=\"" + i + "\" " + sed + ">" + i + "</option>";
                $YearSelector.append(yearStr);
            }

            // 绑定月 
            for (var i = 1; i <= 12; i++) {
                var sed = MonthSelecedValue == i ? "selected=selected" : "";
                monthStr = "<option value=\"" + i + "\" " + sed + ">" + i + "</option>";
                $MonthSelector.append(monthStr);
            }

            // 绑定日
            function BuildDay() {
                InitializeDayStr();
                if ($YearSelector.val() == 0 || $MonthSelector.val() == 0) {
                    $DaySelector.html(dayStr);
                } else {
                    $DaySelector.html(dayStr);
                    var year = parseInt($YearSelector.val());
                    var month = parseInt($MonthSelector.val());
                    var dayCount = 0;
                    switch (month) {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            dayCount = 31;
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            dayCount = 30;
                            break;
                        case 2:
                            dayCount = 28;
                            if ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0)) {
                                dayCount = 29;
                            }
                            break;
                        default:
                            break;
                    }
                     
                    for (var i = 1; i <= dayCount; i++) {
                        var sed = DaySelecedValue == i ? "selected=selected" : "";
                        dayStr = "<option value=\"" + i + "\" " + sed + ">" + i + "</option>";
                        $DaySelector.append(dayStr);
                    }
                }
            }
            function InitializeDayStr() {
                dayStr = "<option value=\"" + FirstValue + "\">" + DayFirstText + "</option>";
            }
            $MonthSelector.change(function () {
                BuildDay();
            });
            $YearSelector.change(function () {
                BuildDay();
            });
            if ($DaySelector.attr("rel") != "") {
                BuildDay();
            }
        } // End ms_DatePicker
    });
})(jQuery);