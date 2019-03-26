var FormJS = function(options){
	"use strict";
	
	var defaultOptions = {
		urlAction : "",
		formMethod : "POST",
		formId : "",
		inputClass : ".input-search",
		preSubmit : $.noop,
		submitCallback : $.noop
	}
	
	var settings = $.extend(defaultOptions, options);
	
	var submitForm = function(){
		var dataSubmit = {};
		
		$(formId + " > " + inputClass).each(function(){
			dataSubmit[$(this.attr("md"))] = $(this).val();
		});
		
		if(settings.preSubmit && $.isFunction(settings.preSubmit)) dataSubmit = settings.preSubmit(dataSubmit);
		$.ajax(
			{
			  method: settings.formMethod,
			  url: settings.urlAction,
			  data: dataSubmit
			}
		)
		.done(function() {			
			if(settings.submitCallback && $.isFunction(settings.submitCallback)) settings.submitCallback(true);
		})
		.fail(function() {
			if(settings.submitCallback && $.isFunction(settings.submitCallback)) settings.submitCallback(false);
		})
		.always(function() {

		});
	}
	
	return {
		submitForm : submitForm
	}
	
};
