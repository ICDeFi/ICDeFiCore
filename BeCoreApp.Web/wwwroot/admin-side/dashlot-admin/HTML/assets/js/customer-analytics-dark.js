$(function(e){
  'use strict'
  
	/* sparkline_bar31 */
	$(".sparkline_bar").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		type: 'bar',
		height:35,
		width:50,
		barWidth: 4,
		barSpacing: 2,
		colorMap: {
			'9': '#a1a1a1'
		},
		barColor: '#007bff '
	});
	/* sparkline_bar31 end */
	
	/* sparkline_bar31 */
	$(".sparkline_bar31").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		type: 'bar',
		height:35,
		width:50,
		barWidth:4,
		barSpacing: 2,
		colorMap: {
			'9': '#a1a1a1'
		},
		barColor: '#f88960',
	});
	/* sparkline_bar31 end */
		
    /* sparkline_bar1 */
	$(".sparkline_bar1").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		 type: 'bar',
			barWidth: 8,
			height: 30,
			barColor: '#007bff ',
			chartRangeMax: 7
	});
	/* sparkline_bar1 end */
	
	/* sparkline_bar2 */
	$(".sparkline_bar2").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		 type: 'bar',
			barWidth: 8,
			height: 30,
			barColor: '#f57b4e',
			chartRangeMax: 7
	});
	/* sparkline_bar2 end */

	/* sparkline_bar3 */
	$(".sparkline_bar3").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		 type: 'bar',
			barWidth: 8,
			height: 30,
			barColor: '#09b0ec',
			chartRangeMax: 7
	});
	/* sparkline_bar3 end */
	
	/* sparkline_bar4 */
	$(".sparkline_bar4").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		 type: 'bar',
			barWidth: 8,
			height: 30,
			barColor: '#36b37e',
			chartRangeMax: 7
	});
	/* sparkline_bar4 end */
	
	
	
	if ($("#flotChart").length) {
		var d1 = [
				[0,45],
				[1,43],
				[2,47],
				[3,45],
				[4,45],
				[5,45],
				[6,42],
				[7,43],
				[8,45],
				[9,45],
				[10,47],
				[11,42],
				[12,40],
				[13,41],
				[14,38],
				[15,42],
				[16,36],
				[17,42],
				[18,38],
				[19,35],
				[20,35],
				[21,35],
				[22,33],
				[23,31],
				[24,28],
				[25,30],
				[26,26],
				[27,22],
				[28,31],
				[29,31],
				[30,33],
				[31,35],
				[32,42],
				[33,42],
				[34,35],
				[35,38],
				[36,38],
				[37,30],
				[38,31],
				[39,32],
				[40,38],
				[41,42],
				[42,37],
				[43,42],
				[44,44],
				[45,44],
				[46,42],
				[47,39],
				[48,37],
				[49,40],
				[50,42],
				[51,37],
				[52,40],
				[53,42],
				[54,37],
				[55,32],
				[56,36],
				[57,37],
				[58,38],
				[59,42],
				[60,40],
				[61,40],
				[62,43],
				[63,43],
				[64,41],
				[65,40],
				[66,43],
				[67,46],
				[68,47],
				[69,42],
				[70,39],
				[71,41],
				[72,45],
				[73,47],
				[74,51],
				[75,45],
				[76,50],
				[77,52],
				[78,50],
				[79,56],
				[80,55],
				[81,54],
				[82,57],
				[83,58],
				[84,61],
				[85,61],
				[86,64],
				[87,57],
				[88,55],
				[89,48],
				[90,44],
				[91,44],
				[92,47],
				[93,47],
				[94,51],
				[95,45],
				[96,47],
				[97,42],
				[98,44],
				[99,42],
				[100,49],
			];
		var curvedLineOptions = {
			series: {
				curvedLines: {
						active: true,
				},
				shadowSize: 0,
				lines: {
						show: true,
						lineWidth: 2,
						fill: false
				},
			},
			
			grid: {
				borderWidth: 0,
				labelMargin: 0
			},
			yaxis: {
				show: true,
				min: 0,
				max: 69,
				position: "left",
				ticks: [
					[0, '0'],
					[20, '25'],
					[40, '50'],
					[60, '75'],
					[70, '100'],
					[90, '120'],
					
					
				],
				tickColor: 'rgb(142, 156, 173,0.1)',
				color: '#8e9cad',
			},
			xaxis: {
				show: true,
				position: "bottom",
				ticks: [
						[0, 'Jan'],
						[20, 'Feb'],
						[40, 'Mar'],
						[60, 'Apr'],
						[80, 'May'],
						[100, 'Jun']
				],
				tickColor: 'rgb(142, 156, 173,0.1)',
				color: '#8e9cad',
			},
			legend: {
				noColumns: 4,
				container: $("#legendContainer"),
			}
		}
		$.plot($("#flotChart"), [{
		data: d1,
		curvedLines: {
			apply: true ,
			tension: 1,
		},
		points: {
			show: false,
			fillColor: 'transparent',
		},
			color: '#007bff ', 
		
		lines: {
			show: true, 
			fill: false,
			fillColor: { colors: [{ opacity: 0.09 }, { opacity: 0.5}] }
		},
		label: 'This year',
		stack: true,
		}], curvedLineOptions);
	}
	
	/*-----canvasDoughnut-----*/
	 var doughnut = document.getElementById("donutChart");
		var myDoughnutChart = new Chart(doughnut, {
			type: 'doughnut',
			data: {
			labels:["Very Satisfied", "Satisfied","Neutral", "Unsatisfied"],
			datasets: [{
				label: "My First dataset",
				data: [40,25, 20,15],
				backgroundColor: ['rgb(34, 5, 191,0.8)','rgb(255, 102, 0,0.7)','rgb(9, 176, 236,0.8)','rgb(54, 179, 126,0.8)'],
			 }]
		   },
		  options: {
				maintainAspectRatio : false,
				cutoutPercentage:60,
				legend: {
					display: false
				},
			},
			elements: {
				line: {
					borderWidth: 1
				},
				point: {
					radius: 4,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		});
	
});
	
