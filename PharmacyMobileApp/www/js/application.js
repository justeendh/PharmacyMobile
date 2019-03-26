// JavaScript Document
var titleHeight = 0;

var getUrlParameter = function getUrlParameter(sParam) {
	var sPageURL = decodeURIComponent(window.location.search.substring(1)),
		sURLVariables = sPageURL.split('&'),
		sParameterName,
		i;

	for (i = 0; i < sURLVariables.length; i++) {
		sParameterName = sURLVariables[i].split('=');

		if (sParameterName[0] === sParam) {
			return sParameterName[1] === undefined ? true : sParameterName[1];
		}
	}
};

function CheckLogin()
{
	var storage = window.localStorage;
//	var IsLogin = storage.getItem("IsLogin"); // Pass a key name to get its value.	
//	if(IsLogin === null || IsLogin === "false" || !IsLogin)
//	{
//		window.location = "login.html";
//		return;
//	}
}


function Logout()
{
	navigator.notification.confirm(
		"Bạn chắc chắn muốn đăng xuất ?",  // message
		function(buttonIndex){
			if(buttonIndex === 1){
				var storage = window.localStorage;
				storage.removeItem("IsLogin");
				window.location = "startup.html";
				return;
			}
		},         // callback
		'Xác nhận đăng xuất',            // title
		['Đăng xuất','Không']                  // buttonName
	);
}


function ExitApp(){
	navigator.notification.confirm(
		"Thoát ứng dụng ?",  // message
		function(buttonIndex){
			if(buttonIndex === 1){
				//var storage = window.localStorage;
				//storage.removeItem("IsLogin");
				navigator.app.exitApp();
			}
		},         // callback
		'Xác nhận thoát',            // title
		['Thoát','Không']                  // buttonName
	);
}


function Initcolumn(){
	$(".tb-data thead th div").each(function(){
		$(".tb-data thead th div").each(function(){
			var thctn = $(this).closest( "th" );
			$(this).css("left", thctn.position().left);
		});
	});
}



$(function(){
	
	$('body').on('click', '.logoutButton', function () {
		 Logout();
	});
	
	$('body').on('click', '.plusInput', function () {
		var target = $(this).attr("targetinput");
		var maxVal = $(this).attr("max") || "";
		var valueInput = numeral($(target).val()).value();
		if(maxVal !== "" && valueInput === numeral(maxVal).value()) return;
		else $(target).val(valueInput+1);
	});
	
	$('body').on('click', '.minusInput', function () {
		var target = $(this).attr("targetinput");
		var minVal = $(this).attr("min") || "";
		var valueInput = numeral($(target).val()).value();
		if(minVal !== "" && valueInput === numeral(minVal).value()) return;
		else $(target).val(valueInput-1);
	});
	
	Initcolumn();

	$('.tb-container').scroll(function(){
		$(".tb-data thead th div").each(function(){
			Initcolumn();
		});
	});
});

var responsiveSet = function(){
	var heightHeader = $(".navbar-header").first().outerHeight() || 0;
	var heightBottomBar = $(".bottombar").first().outerHeight() || 0;
	var heightThead = $(".table thead tr th div").first().outerHeight() || 0;
	//alert(heightThead);
	//alert(heightHeader);alert(heightBottomBar);alert(($(window).innerHeight()));
    $(".list-section").height(($(window).innerHeight() - (heightHeader + titleHeight + heightThead + heightBottomBar + 10)));
	$(".tb-section").height(($(window).innerHeight()-(heightHeader+titleHeight+heightThead+heightBottomBar+ 10)));
	$(".tb-container").height(($(window).innerHeight()-(heightHeader+titleHeight+heightThead+heightBottomBar+ 10)));
	$(".body-section").height(($(window).innerHeight()-(heightHeader+titleHeight+heightThead+heightBottomBar+ 10)));
};
	
$(window).load(function(){
	responsiveSet();
});

$(window).resize(function(){
	responsiveSet();
	Initcolumn();
});

var applicationJS = function(options){
	
	var defaultSettings = {
		backbuttonCallback : ExitApp,
		initCallback : function(){
			var storage = window.localStorage;
			var IsLogin = storage.getItem("IsLogin") || "false"; // Pass a key name to get its value.	
			if(!IsLogin || IsLogin === "false")
			{
				window.location = "startup.html";
				return;
			}			
			
			var heightBottomBar = $(".bottombar").first().height();
			$('.bottombar .bottombar-nav li a').ellipsis();
		},
		orientationchangeCallback : $.noop,
		resizeCallback : $.noop,
		titleHeight : 0
	}	
	
	var settings = $.extend(defaultSettings, options);
	titleHeight = settings.titleHeight;
	
	document.addEventListener('deviceready', function() {
				
		if (window.cordova && StatusBar)
		{
			StatusBar.backgroundColorByHexString(StatusBarBgColor);
		}		
		

		if (settings.callback && $.isFunction(settings.callback)) {
			settings.initCallback();
		}

	}, false);


	//back button confirm exit			
	document.addEventListener('backbutton', function(e){
		if (settings.backbuttonCallback && $.isFunction(settings.backbuttonCallback)) {
			settings.backbuttonCallback();
		}
	}, false);

	/////////////////////
	// Listen for orientation changes
	window.addEventListener("orientationchange", function() {
		if (settings.orientationchangeCallback && $.isFunction(settings.orientationchangeCallback)) {
			settings.orientationchangeCallback();
		}
	}, false);

	// Listen for resize changes
	window.addEventListener("resize", function() {
		if (settings.resizeCallback && $.isFunction(settings.resizeCallback)) {
			settings.resizeCallback();
		}
	}, false);
}