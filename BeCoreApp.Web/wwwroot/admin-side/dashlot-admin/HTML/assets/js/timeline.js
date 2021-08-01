(function($) {
	"use strict";
	
	timeline(document.querySelectorAll('.timeline-vertical-demo'), {
		forceVerticalMode: 1000,
		mode: 'vertical',
		verticalStartPosition: 'left',
		visibleItems: 4
	});
	timeline(document.querySelectorAll('.timeline-horizontal'), {
		forceVerticalMode: 1000,
		mode: 'horizontal',
		verticalStartPosition: 'left',
		visibleItems: 2
	});
	
})(jQuery);