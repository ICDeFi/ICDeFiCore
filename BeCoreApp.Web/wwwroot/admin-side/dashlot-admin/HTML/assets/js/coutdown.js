(function($) {
	"use strict";
	// ______________Countdown
	$('#count-down').countDown({
		targetDate: {
			'day': 15,
			'month': 10,
			'year': 2027,
			'hour': 0,
			'min': 0,
			'sec': 0
		},
		omitWeeks: true
	});
	
	$('.count-down').countdown100({
		// Set Endtime here
		// Endtime must be > current time
		endtimeYear: 0,
		endtimeMonth: 0,
		endtimeDate: 35,
		endtimeHours: 18,
		endtimeMinutes: 0,
		endtimeSeconds: 0,
		timeZone: ""
		// ex:  timeZone: "America/New_York", can be empty
		// go to " http://momentjs.com/timezone/ " to get timezone
	});
	
})(jQuery);