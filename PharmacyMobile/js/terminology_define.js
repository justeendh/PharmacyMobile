
var Terminologies = {
	dics : {

	},
	importDictionary : function(lang, dic) {
		this.dics[lang] = dic;
		//alert(JSON.stringify(this.dics[lang]));
	},
	getByKey : function(key) {
		try {
			var keyUpper = key.toLowerCase();
			//console.log(keyUpper);
			return this.dics[WorkContextJs.lang][keyUpper];
		} catch (ex) {
			console.log('getByKey.ex', ex.message);
		}
		return key;
	},
	getBySubKey : function(lang, subKey) {
		var keyUpper = (lang + subKey).toLowerCase();
		return this.dics[WorkContextJs.lang][keyUpper];
	},
	term: function(key){
		try{
			return this.getByKey(key);
		}catch(ex){
			console.log('term.ex', ex.message);
		}
		return key;
	},
	translateUI : function(blok) {
		try {
			blok = $.extend(blok, { translAttr: 'transl', container : '' });
			var translAttr = blok.translAttr || 'transl';
			var container = blok.container || '';
			$(container + ' [' + translAttr + ']').each(function(index, item) {
                var key = $(item).attr(translAttr);
                var targetattr = $(item).attr("targetattr") || "";
				var translValue = Terminologies.getByKey(key);
				//console.log('key' + index, key);
                //console.log('translValue' + index, translValue);
                if (targetattr !== "") $(item).attr(targetattr, translValue);
				else $(item).html(translValue);
			});
		} catch (ex) {
			console.log('translateUI.ex', ex.message);
		}

	}
}