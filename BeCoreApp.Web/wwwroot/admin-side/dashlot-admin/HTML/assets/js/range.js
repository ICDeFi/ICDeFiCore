	 $("#range_01").ionRangeSlider();

    $("#range_02").ionRangeSlider({
        min: 1000,
        max: 10000,
        from: 1500
    });

    $("#range_03").ionRangeSlider({
        type: "double",
        grid: true,
        min: 1,
        max: 1500,
        from: 500,
        to: 1000,
        prefix: "$"
    });
	
	$("#range_04").ionRangeSlider({
		 type: "double",
        grid: true,
        min: -500,
        max: 2000,
        from: -800,
        to: 900
	});

    $("#range_05").ionRangeSlider({
        type: "double",
        grid: true,
        min: -2000,
        max: 2000,
        from: -600,
        to: 800,
        step: 300
    });

    $("#range_06").ionRangeSlider({
        grid: true,
        from: 2,
        values: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"]
    });

    $("#range_07").ionRangeSlider({
        grid: true,
        min: 10000,
        max: 10000000,
        from: 200000,
        step: 10000,
        prettify_enabled: true
    });

    $("#range_08").ionRangeSlider({
        min: 1000,
        max: 2000,
        from: 1200,
        disable: true
    });
	$("#range_27").ionRangeSlider({
	  type: "double",
	  min: 1000000,
	  max: 2000000,
	  grid: true,
	  force_edges: true
	});
	$("#range").ionRangeSlider({
	  hide_min_max: true,
	  keyboard: true,
	  min: 0,
	  max: 5000,
	  from: 1000,
	  to: 4000,
	  type: 'double',
	  step: 1,
	  prefix: "$",
	  grid: true
	});
	$("#range_25").ionRangeSlider({
	  type: "double",
	  min: 1000000,
	  max: 2000000,
	  grid: true
	});
	$("#range_26").ionRangeSlider({
	  type: "double",
	  min: 0,
	  max: 10000,
	  step: 500,
	  grid: true,
	  grid_snap: true
	});
	$("#range_31").ionRangeSlider({
	  type: "double",
	  min: 0,
	  max: 100,
	  from: 30,
	  to: 70,
	  from_fixed: true
	});
	$(".range_min_max").ionRangeSlider({
	  type: "double",
	  min: 0,
	  max: 100,
	  from: 30,
	  to: 70,
	  max_interval: 50
	});
	$("#range_17").ionRangeSlider({
		type: "double",
		min: 0,
		max: 5000,
		from: 1200,
		to: 2800,
		hide_min_max: true,
		hide_from_to: false,
		grid: false
	});
			
			