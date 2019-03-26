var ModuleViewerJs = window.ModuleViewerJs || {};
var ModuleViewJs = function () {
    var self = this;
    var CurrentPage = 1;
    var CurrentPageSize = 20;
    var CurrentModule = "";
    var defaultOption = {
        ModuleName: "",
        ActionUrl: "",
        targetInsertForm: "",
        inserFormUrl: "",
        detailViewUrl: "",
        detailViewContainerId: "",
        detailViewCallback: $.noop,
        targetUpdateInsert: ""
    };
    var settings = {};
    var Init = function (options) {
        self.settings = $.extend(defaultOption, options);
        self.CurrentModule = self.settings.ModuleName;
    }

    var setModule = function (module) {
        self.CurrentModule = module;
    }

    var getModuleName = function () {
        return self.CurrentModule;
    }

    var setPageSize = function (size) {
        self.CurrentPageSize = size;
    }
    

    var getPageSize = function () {
        return self.CurrentPageSize;
    }

    var setCurrentPage = function (page) {
        self.CurrentPage = page;
    }

    var getCurrentPage = function () {
        return self.CurrentPage;
    }



    var getSettings = function () {
        return self.settings;
    }

    var LoadDataInit = function () {
        var pageLoad = 1;
        var moduleLoad = self.CurrentModule || "";
        var pageSize = self.CurrentPageSize || 20;
        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
    }

    var LoadDataByPage = function (page) {
        var pageLoad = page;
        var moduleLoad = self.CurrentModule || "";
        var pageSize = self.CurrentPageSize || 20;
        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
    }

    var reloadData = function () {
        var pageLoad = self.CurrentPage || 1;
        var moduleLoad = self.CurrentModule || "";
        var pageSize = self.CurrentPageSize || 20;
        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
    }

    var LoadInsert = function (modalSelector) {
        var moduleLoad = self.CurrentModule || "";
        $("#" + self.settings.targetInsertForm).load(self.settings.inserFormUrl, { "module": moduleLoad }, function () {
            $("#ACTION").val("ADD");
            $(modalSelector).modal("show");
        });
    }

    var LoadDetail = function (id) {
        $("#" + self.settings.detailViewContainerId).load(self.settings.detailViewUrl, { "id": id }, function () {
            if (self.settings.detailViewCallback) self.settings.detailViewCallback(id);
        });
    }

    var LoadUpdate = function (modalSelector, id) {
        var moduleLoad = self.CurrentModule || "";
        $("#" + self.settings.targetInsertForm).load(self.settings.inserFormUrl, { "id": id, "module": moduleLoad }, function () {
            $("#ACTION").val("UPDATE");
            $(modalSelector).modal("show");
        });
    }

    var OnSuccessInsert = function () {
        var pageLoad = self.CurrentPage || 1;
        var moduleLoad = self.CurrentModule || "";
        var pageSize = self.CurrentPageSize || 20;
        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
        $.bootstrapGrowl('<h4>SUCCESS!</h4> <p>Thao tác thực hiện thành công</p>', {
            type: "success",
            delay: 1000,
            allow_dismiss: true,
            offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
            align: 'left', // ('left', 'right', or 'center')
        });
    }

    var OnSuccessUpdate = function (page) {
        var pageLoad = self.CurrentPage || 1;
        var moduleLoad = self.CurrentModule || "";
        var pageSize = self.CurrentPageSize || 20;
        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
        $.bootstrapGrowl('<h4>SUCCESS!</h4> <p>Thao tác thực hiện thành công</p>', {
            type: "success",
            delay: 1000,
            allow_dismiss: true,
            offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
            align: 'left', // ('left', 'right', or 'center')
        });
    }

    var Delete = function (id, callback) {
        if (confirm('Bạn chắc chắn muốn xoá bản ghi này ?')) {
            $.ajax({
                method: "POST",
                url: "/" + self.settings.ModuleName + "/delete", data: { "id": id }, success: function (result) {
                    if (result.success) {
                        var pageLoad = self.CurrentPage || 1;
                        var moduleLoad = self.CurrentModule || "";
                        var pageSize = self.CurrentPageSize || 20;
                        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
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

    var DeleteMulti = function (classCheckbox, attributeName) {
        var IdDelete = [];
        $("." + classCheckbox + ":checked").each(function () {
            IdDelete.push($(this).data(attributeName));
        });
        if (IdDelete.length === 0) {
            $.bootstrapGrowl('<h4>ERROR!</h4> <p>Vui lòng chọn các mục cần xoá</p>', {
                type: "danger",
                delay: 2500,
                allow_dismiss: true,
                offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
                align: 'left', // ('left', 'right', or 'center')
            });
            return;
        }
        if (confirm('Bạn chắc chắn muốn xoá những bản ghi này ?')) {
            
            var lstIdDelete = IdDelete.join();
            $.ajax({
                method: "POST",
                url: "/" + self.settings.ModuleName + "/deletemulti", data: { "lstIdDelete": lstIdDelete }, success: function (result) {
                    if (result.success) {
                        var pageLoad = self.CurrentPage || 1;
                        var moduleLoad = self.CurrentModule || "";
                        var pageSize = self.CurrentPageSize || 20;
                        $("#" + self.settings.targetUpdateInsert).load(self.settings.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
                        $.bootstrapGrowl('<h4>SUCCESS!</h4> <p>Thao tác thực hiện thành công</p>', {
                            type: "success",
                            delay: 1000,
                            allow_dismiss: true,
                            offset: { from: 'top', amount: 20 }, // 'top', or 'bottom'
                            align: 'left', // ('left', 'right', or 'center')
                        });
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

    var OnSuccessDelete = function (page) {
        var pageLoad = self.CurrentPage || 1;
        var moduleLoad = self.CurrentModule || "";
        var pageSize = self.CurrentPageSize || 20;
        $("#" + self.targetUpdateInsert).load(self.ActionUrl, { "page": pageLoad, "module": moduleLoad, "pageSize": pageSize });
    }

    return {
        CurrentModule: CurrentModule,
        CurrentPage: CurrentPage,
        CurrentPageSize: CurrentPageSize,
        settings: settings,
        Init: Init,
        setModule: setModule,
        setPageSize: setPageSize,
        LoadDataInit: LoadDataInit,
        LoadDataByPage: LoadDataByPage,
        reloadData: reloadData,
        LoadInsert: LoadInsert,
        LoadUpdate: LoadUpdate,
        LoadDetail: LoadDetail,
        OnSuccessInsert: OnSuccessInsert,
        OnSuccessUpdate: OnSuccessUpdate,
        Delete: Delete,
        DeleteMulti: DeleteMulti,
        getModuleName: getModuleName,
        getPageSize,
        setCurrentPage,
        getCurrentPage,
        getSettings
    }
}

