// JavaScript Document
var ModuleViewerJs = function(){
	var instance = this;
	var pageSize = 20;
	var pageNumber = 1;
	var defaultOptions ={
		urlSearch : "urlSearch",
		dataContainer : "dataContainer",
        dataTmpl: "dataTmpl",
        pagingContainer: "#pagingContainer",
        pagingTmpl: "#pagingTmpl",
        numberOfCols: 2,
        pageSize: 20,
        pageNumber : 1

	};
	
	var settings = {};
	
	var Init = function(options){
		settings = $.extend(defaultOptions, options);
		if(settings.numberOfCols !== null || settings.numberOfCols > 0){
			$(settings.dataContainer).html("<tr><td style='background: #fff !important;text-align: center;color: #9d9d9d !important;line-height: 30px;padding: 20px 0 !important;border-bottom: none !important;' colspan='" + settings.numberOfCols + "'><div><i class='fa fa-2x fa-frown-o'></i></div><div>Không có dữ liệu</div></td></tr>");
		}
    };

    var renderPaging = function (data) {
        if (data.totalPage === 1) return;
        var pageData = {};
        pageData.currentPage = data.pageNumber;
        pageData.totalPage = data.totalPage;
        pageData.ListPage = [];
        for (i = data.firstItemOnPage; i <= data.lastItemOnPage; i++){
            pageData.ListPage.push({ pageItem : i });
        }
        $(settings.pagingContainer).html("");
        $(settings.pagingTmpl).tmpl(pageData).appendTo(settings.pagingContainer);
    };
		
	
    var search = function (query, callback) {		
        if (typeof query === "string") { queryData = query || ""; }
        else {
            queryData = query || { page: 1, pageSize: 20 };
            instance.pageNumber = queryData.page;
            instance.pageSize = queryData.pageSize;
        }	
		//SpinnerPlugin.activityStart("Đang tải dữ liệu...", { dimBackground: true });
        $.getJSON(settings.urlSearch, { query: queryData, page: pageNumber, pageSize: pageSize } , function( data ) {
			//alert(JSON.stringify(data));
			$(settings.dataContainer).html("");
			var rowData = (data.result);
			if(rowData !== null && rowData.length > 0) $(settings.dataTmpl).tmpl(rowData).appendTo(settings.dataContainer);
			else $(settings.dataContainer).html("<tr><td style='background: #fff !important;text-align: center;color: #9d9d9d !important;line-height: 30px;padding: 20px 0 !important;border-bottom: none !important;' colspan='" + settings.numberOfCols + "'><div><i class='fa fa-2x fa-frown-o'></i></div><div>Không có dữ liệu</div></td></tr>");
			$('.formatnumber').number( true, 0, ',', '.' );
            Initcolumn();
            renderPaging(data);
			if (callback && $.isFunction(callback)) {
                callback();
            }
			//SpinnerPlugin.activityStop();
		});
	};
	
	return {
		Init : Init,
		search : search		
	};
};