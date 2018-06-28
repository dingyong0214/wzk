/*分页异步化*/
$(function () {
    //分页
    $(document).on('click', '#page a', function () {
        getPageData($(this));
        return false;
    });
    //获取数据
    function getPageData($this) {
            $.get($this.attr("href"), function (data) {
                $("#container").html(data);
            });
    }
});