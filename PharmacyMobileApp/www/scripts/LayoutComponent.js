
Vue.component('paginate', VuejsPaginate);

Vue.filter('currency', function (val) {
    return accounting.formatNumber(val, 0, '.')
});
// Define a new component called button-counter
Vue.component('app-header', {
    data: function () {
        return {
            
        }
    },
    template:
    `
        <nav class="navbar navbar-default navbar-fixed-top">
			<div class="container-fluid">
			<!-- Brand and toggle get grouped for better mobile display -->
			<div class="navbar-header">
				<img alt="Brand" src="img/logo.png" style="height: 37px;margin-top: 5px;margin-left: 5px;">
			</div>
			</div><!-- /.container-fluid -->
		</nav>
    `
});

Vue.component('bottom-menu', {
    data: function () {
        return {
            
        }
    },
    template:
    `
        <nav class="bottombar">
	        <div class="bottombar-header">
		        <ul class="bottombar-nav">
			        <li><a class="add-active-click" href="dashboard.html"><i class="fa fa-2x fa-home"></i></a></li>
			        <li><a class="add-active-click" href="donhang.html"><i class="fa fa-2x fa-shopping-cart"></i></a></li>
			        <li><a class="add-active-click" href="mathang.html"><i class="fa fa-2x fa-list-alt"></i></a></li>
			        <li class="hidden"><a class="add-active-click" href="consult.html"><i class="fa fa-2x fa-bell-o"></i></a></li>
			        <li>
				        <a class="add-active-click" href="javascript:void(0);" data-toggle="dropdown">
					        <i class="fa fa-2x fa-ellipsis-h"></i>
				        </a>
				        <ul class="dropdown-menu" aria-labelledby="dLabel" style="left: auto; right: 5px; top: auto; bottom: 100%; margin-bottom: 5px; color: #f00;">
					        <li class="dropdown-li"><a href="dashboard.html"><i class="fa fa-home"></i> Trang chủ</a></li> 
					        <li role="separator" class="dropdown-li divider"></li>
					        <li class="dropdown-li">
						        <a href="javascript:void(0);" class="logoutButton">
							        <i class="fa fa-sign-out"></i> Đăng xuất
						        </a>
					        </li>
					        <li class="dropdown-li">
						        <a href="javascript:void(0);" onClick="ExitApp();">
							        <i class="fa fa-power-off"></i> Thoát
						        </a>
					        </li>
				        </ul>
			        </li>
		        </ul>
	        </div>
        </nav>
    `
});