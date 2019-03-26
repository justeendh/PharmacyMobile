var currentPagetLoad = 1;
var instance = new ModuleViewJs();
instance.Init({
    ModuleName: "contact",
    ActionUrl: "/contact/Loadbypage",
    targetUpdateInsert: "contacts-data-container",
    targetInsertForm: "formDetailContact",
    detailViewUrl: "/contact/detail",
    detailViewContainerId: "formDetailContact",
    detailViewCallback: function (id) {
        $("#detailContactModal").modal("show");
    }
});

instance.MarkRead = function (id, callback) {
    if (confirm('Bạn chắc chắn muốn đánh dấu đã xem bản ghi này ?')) {
        $.ajax({
            method: "POST",
            url: "/" + instance.getModuleName() + "/markread", data: { "id": id }, success: function (result) {
                if (result.success) {
                    var pageLoad = instance.getCurrentPage() || 1;
                    var moduleLoad = instance.getModuleName() || "";
                    var pageSize = instance.getPageSize() || 20;
                    var setting = instance.getSettings();
                    $("#" + setting.targetUpdateInsert).load(setting.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
                    $.bootstrapGrowl('<h4>SUCCESS!</h4> <p>Thao tác thực hiện thành công</p>', {
                        type: "success",
                        delay: 1000,
                        allow_dismiss: true,
                        offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
                        align: 'left', // ('left', 'right', or 'center')
                    });
                    if (callback) callback();
                }
                else {
                    var msg = result.msg || "Đã có lỗi xảy ra ! Vui lòng kiểm tra và thử lại";
                    $.bootstrapGrowl('<h4>ERROR!</h4> <p>' + msg + '</p>', {
                        type: "danger",
                        delay: 2500,
                        allow_dismiss: true,
                        offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
                        align: 'left', // ('left', 'right', or 'center')
                    });
                }
            }
        });
    }
}

var OnSuccessDetail = function () {
    instance.LoadDataByPage(currentPagetLoad);
    ellipsisCallback();
    $("#detailContactModal").modal("hide");
};