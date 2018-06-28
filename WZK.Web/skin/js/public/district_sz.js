$.prototype.addOption = function (callback, name, val) {
    var thi = this[0],
        add = function (result) {
            $.each(result, function () { thi.options.add(new Option(this[name], this[val])) });
        };
    thi.options.length = 0;
    thi.options.add(new Option('--请选择--', ''));
    $.type(callback) == 'function' ? callback(add) : add(callback);
    return this;
};
(window.Zepto)&&($.fn.addOption = $.prototype.addOption);
//$.extend($.fn, $.prototype.addOption);
var district = {
cache: [{
'id': 440000,
'name': '广东省',
'city': [{
	'id': 440300,
	'name': '深圳市',
	'area': [{
			'id': 440303,
			'name': '罗湖区'
		}, {
			'id': 440304,
			'name': '福田区'
		}, {
			'id': 440305,
			'name': '南山区'
		}, {
			'id': 440306,
			'name': '宝安区'
		}, {
			'id': 440307,
			'name': '龙岗区'
		}, {
			'id': 440308,
			'name': '盐田区'
		}, {
			'id': 440309,
			'name': '光明新区'
		}, {
			'id': 440310,
			'name': '坪山新区'
		}, {
			'id': 440311,
			'name': '大鹏新区'
		}, {
			'id': 440312,
			'name': '龙华新区'
		}]
	}]
}],
    init: function (p, c, m) {
        $(p).addOption(district.cache, 'name', 'id').change(function () {
            var pSelectedIndex = $('option:selected', p).index();
            $(m).addOption([], 'name', 'id');
            $(c).addOption(pSelectedIndex > 0 ? district.cache[pSelectedIndex - 1].city : [], 'name', 'id').change(function () {
                pSelectedIndex = $('option:selected', p).index();
                var cSelectedIndex = this.selectedIndex;
                $(m).addOption(cSelectedIndex > 0 ? district.cache[pSelectedIndex - 1].city[cSelectedIndex - 1].area : [], 'name', 'id');
            });
        });
    },
    setvalue: function (p, c, m, pv, cv, mv) {
        this.init(p, c, m);
        $(p).val(pv);
        var pSelectedIndex = $('option:selected', p).index();
        $(c).addOption(pSelectedIndex > 0 ? district.cache[pSelectedIndex - 1].city : [], 'name', 'id').change(function () {
            pSelectedIndex = $('option:selected', p).index();
            var cSelectedIndex = this.selectedIndex;
            $(m).addOption(cSelectedIndex > 0 ? district.cache[pSelectedIndex - 1].city[cSelectedIndex - 1].area : [], 'name', 'id');
        });
        $(c).val(cv);
        var cSelectedIndex = $('option:selected', c).index();
        $(m).addOption(cSelectedIndex > 0 ? district.cache[pSelectedIndex - 1].city[cSelectedIndex - 1].area : [], 'name', 'id');
        $(m).val(mv);
    }
};

