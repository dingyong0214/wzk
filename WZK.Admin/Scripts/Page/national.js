/// <reference path="../../jquery-1.11.1.min.js" />
var national = [
           "汉族", "壮族", "满族", "回族", "苗族", "维吾尔族", "土家族", "彝族", "蒙古族", "藏族", "布依族", "侗族", "瑶族", "朝鲜族", "白族", "哈尼族",
           "哈萨克族", "黎族", "傣族", "畲族", "傈僳族", "仡佬族", "东乡族", "高山族", "拉祜族", "水族", "佤族", "纳西族", "羌族", "土族", "仫佬族", "锡伯族",
           "柯尔克孜族", "达斡尔族", "景颇族", "毛南族", "撒拉族", "布朗族", "塔吉克族", "阿昌族", "普米族", "鄂温克族", "怒族", "京族", "基诺族", "德昂族", "保安族",
           "俄罗斯族", "裕固族", "乌孜别克族", "门巴族", "鄂伦春族", "独龙族", "塔塔尔族", "赫哲族", "珞巴族"
];

$.fn.extend({
    select: function (name) {
        this.get(0).options.length = 0;
        for (var i = 0; i < national.length; i++) {
            var option = new Option(national[i], national[i]);
            if (name == national[i]) {
                option.selected = true;
            }
            this.get(0).appendChild(option);
        }
    }
});

function getNationHtml(id,isSel) {
    var option = new StringBuilder();
    if (isSel) {
        option.Append("<option>--请选择--</option>");
    }
    $(national).each(function (index, item) {
        option.AppendFormat("<option value='{0}'>{0}</option>", item);
    });
    return ("<select name='" + id + "' id='" + id + "'>" + option.ToString() + "</select>");
}