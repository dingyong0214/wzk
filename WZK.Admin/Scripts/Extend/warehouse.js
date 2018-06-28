$(function ()
{
    $("select[name='WarehouseId']").change(function () {
        var $this = $(this);
        $this.parents('tr').find("#WarehousePartitionId").html("<option value=''>请选择</option>");

        var $val = $(this).val();
        if ($val != '') {
            $.getJSON('/stock/GetPartition', { warehouseId: $val }, function (data) {
                if (data != "") {
                    var html = "<option value=''>请选择</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].Value + "'>" + data[i].Text + "</option>"
                    }
                    $this.parents('tr').find("#WarehousePartitionId").html(html);
                }
            });
        }
    });

    $("#DdlWarehouseId").change(function () {
        $("#DdlWarehousePartitionId").html("<option value=''>请选择</option>");
        var $val = $(this).val();
        if ($val != '') {
            $.getJSON('/stock/GetPartition', { warehouseId: $val }, function (data) {
                if (data != "") {
                    var html = "<option value=''>请选择</option>";
                    for (var i = 0; i < data.length; i++) {
                        html += "<option value='" + data[i].Value + "'>" + data[i].Text + "</option>"
                    }
                    $("#DdlWarehousePartitionId").html(html);
                }
            });
        }
    });
});