(function($) {
	"use strict";
	
	//P-scrolling
	const ps5 = new PerfectScrollbar('.drop-scroll', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	const ps6 = new PerfectScrollbar('.drop-notify', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	const ps4 = new PerfectScrollbar('.drop-cart', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	
})(jQuery);