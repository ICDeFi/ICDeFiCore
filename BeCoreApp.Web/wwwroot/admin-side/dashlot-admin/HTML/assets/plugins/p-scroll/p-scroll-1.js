(function($) {
	"use strict";
	
	//P-scrolling
	const ps = new PerfectScrollbar('.sidebar-right', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	const ps1 = new PerfectScrollbar('.toggle-sidebar', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	const ps2 = new PerfectScrollbar('.drop-scroll', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	const ps3 = new PerfectScrollbar('.drop-notify', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	const ps4 = new PerfectScrollbar('.drop-cart', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	
	
})(jQuery);