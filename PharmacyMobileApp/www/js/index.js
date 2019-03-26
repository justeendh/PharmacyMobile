/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
function caclSize()
{
    var heightBottomBar = $(".bottombar").first().height();
    $('.tb-container').height($(window).height()- (85) - heightBottomBar);
}

var app = {
    // Application Constructor
    initialize: function() {
        document.addEventListener('deviceready', this.onDeviceReady.bind(this), false);
        // Listen for orientation changes
        window.addEventListener("orientationchange", function() {
        	caclSize();
        }, false);

        // Listen for resize changes
        window.addEventListener("resize", function() {
            caclSize();
        }, false);
    },

    // deviceready Event Handler
    //
    // Bind any cordova events here. Common events are:
    // 'pause', 'resume', etc.
    onDeviceReady: function() {
		document.addEventListener('backbutton', function(e){
			if (confirm("Thoát ứng dụng ?"))
			{
				var storage = window.localStorage;
				storage.removeItem("IsLogin");
				navigator.app.exitApp();
			}
			else
			{
				
			}
		}, false);
		if (window.cordova && StatusBar)
		{
			StatusBar.backgroundColorByHexString('#1f723d');
		}
		caclSize();
    }
};

function getDateIfDate(d) {
    var m = d.match(/\/Date\((\d+)\)\//);
    return m ? (new Date(+m[1])).toLocaleDateString('en-US', {month: '2-digit', day: '2-digit', year: 'numeric'}) : d;
}

app.initialize();

$(document).ready(function(){
	$('.bottombar .bottombar-nav li a').ellipsis();
    caclSize();
    $(".add-active-click").on("click", function(){
        $(".add-active-click").removeClass("active");
        $(this).addClass("active");
    });

});
