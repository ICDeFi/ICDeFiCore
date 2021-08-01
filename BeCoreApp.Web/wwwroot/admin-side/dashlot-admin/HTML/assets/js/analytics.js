$(function(e){
  'use strict'
  
	/* Flot (#flotChart)  */
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
				show: false,
				min: 0,
				max: 65,
				position: "left",
				ticks: [
					[0, '600'],
					[50, '610'],
					[100, '620'],
					[150, '640'],
					[200, '660'],
					[250, '680'],
				],
				tickColor: 'rgb(142, 156, 173,0.1)',
				tickLength:0,
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
			fillColor: '#2205bf',
		},
			color: '#2205bf', 
		
		lines: {
			show: true, 
			fill: true,
			fillColor: { colors: [{ opacity: 0.09 }, { opacity: 0.5}] }
		},
		label: 'This year',
		stack: true,
		}], curvedLineOptions);
	}
	/* Flot (#flotChart) closed */
	
	
	/* Flot (#flotChart1)  */
	if ($("#flotChart1").length) {
		var d1 = [
					[0,56],
					[1,58],
					[2,60],
					[3,63],
					[4,64],
					[5,63],
					[6,64],
					[7,66],
					[8,66],
					[9,66],
					[10,66],
					[11,65],
					[12,68],
					[13,58],
					[14,58],
					[15,52],
					[16,54],
					[17,52],
					[18,51],
					[19,57],
					[20,58],
					[21,48],
					[22,55],
					[23,50],
					[24,45],
					[25,47],
					[26,52],
					[27,52],
					[28,51],
					[29,53],
					[30,52],
					[31,53],
					[32,57],
					[33,52],
					[34,48],
					[35,52],
					[36,53],
					[37,58],
					[38,55],
					[39,58],
					[40,61],
					[41,57],
					[42,60],
					[43,61],
					[44,57],
					[45,56],
					[46,58],
					[47,62],
					[48,58],
					[49,58],
					[50,56],
					[51,52],
					[52,52],
					[53,50],
					[54,55],
					[55,50],
					[56,52],
					[57,48],
					[58,47],
					[59,43],
					[60,38],
					[61,34],
					[62,31],
					[63,32],
					[64,36],
					[65,35],
					[66,32],
					[67,33],
					[68,30],
					[69,28],
					[70,28],
					[71,27],
					[72,27],
					[73,23],
					[74,24],
					[75,22],
					[76,22],
					[77,24],
					[78,27],
					[79,31],
					[80,27],
					[81,28],
					[82,24],
					[83,26],
					[84,26],
					[85,36],
					[86,35],
					[87,32],
					[88,32],
					[89,28],
					[90,24],
					[91,25],
					[92,21],
					[93,30],
					[94,32],
					[95,38],
					[96,42],
					[97,48],
					[98,42],
					[99,47],
					[100,50],
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
				show: false,
				min: 0,
				max: 70,
				position: "left",
				ticks: [
					[0, '600'],
					[50, '610'],
					[100, '620'],
					[150, '640'],
					[200, '660'],
					[250, '680'],
				],
				tickColor: 'rgb(142, 156, 173,0.1)',
				tickLength:0,
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
		$.plot($("#flotChart1"), [{
		data: d1,
		curvedLines: {
			apply: true ,
			tension: 1,
		},
		points: {
			show: false,
			fillColor: '#f57b4e',
		},
			color: '#f57b4e', 
		
		lines: {
			show: true, 
			fill: true,
			fillColor: { colors: [{ opacity: 0.1 }, { opacity: 0.5}] }
		},
		label: 'This year',
		stack: true,
		}], curvedLineOptions);
	}
	/* Flot (#flotChart1)  closed */
	
	
	/* Flot (#flotChart2)  */
	if ($("#flotChart2").length) {
		var d1 = [[0,29],
				[1,23],
				[2,27],
				[3,35],
				[4,35],
				[5,35],
				[6,32],
				[7,33],
				[8,25],
				[9,25],
				[10,27],
				[11,25],
				[12,30],
				[13,36],
				[14,36],
				[15,38],
				[16,40],
				[17,42],
				[18,43],
				[19,45],
				[20,40],
				[21,40],
				[22,43],
				[23,41],
				[24,48],
				[25,50],
				[26,56],
				[27,52],
				[28,41],
				[29,41],
				[30,43],
				[31,45],
				[32,52],
				[33,52],
				[34,45],
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
				show: false,
				min: 0,
				max: 70,
				position: "left",
				ticks: [
					[0, '600'],
					[50, '610'],
					[100, '620'],
					[150, '640']
				],
				tickColor: 'rgb(142, 156, 173,0.1)',
				tickLength:0,
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
			},
			legend: {
				noColumns: 4,
				container: $("#legendContainer"),
			}
		}
		$.plot($("#flotChart2"), [{
		data: d1,
		curvedLines: {
			apply: true ,
			tension: 1,
		},
		points: {
			show: false,
			fillColor: '#00b8d9',
		},
			color: '#00b8d9', 
		
		lines: {
			show: true, 
			fill: true,
			fillColor: { colors: [{ opacity: 0.1 }, { opacity: 0.5}] }
		},
		label: 'This year',
		stack: true,
		}], curvedLineOptions);
	}
	/* Flot (#flotChart2)  closed */
	
	
	/* Flot (#flotChart-1)  */
	var plot = $.plot('#flotChart-1', [{
		data: [
			[0,68],
			[1,60],
			[2,59],
			[3,67],
			[4,59],
			[5,60],
			[6,58],
			[7,58],
			[8,63],
			[9,62],
			[10,59],
			[11,60],
			[12,58],
			[13,64],
			[14,50],
			[15,55],
			[16,53],
			[17,59],
			[18,60],
			[19,48],
			[20,49],
			[21,56],
			[22,44],
			[23,48],
			[24,45],
			[25,49],
			[26,50],
			[27,48],
			[28,52],
			[29,56],
			[30,53],
			[31,50],
			[32,46],
			[33,41],
			[34,48],
			[35,49],
			[36,39],
			[37,43],
			[38,47],
			[39,45],
			[40,47],
			[41,49],
			[42,49],
			[43,44],
			[44,47],
			[45,49],
			[46,48],
			[47,47],
			[48,50],
			[49,45],
			[50,41],
			[51,41],
			[52,40],
			[53,35],
			[54,48],
			[55,48],
			[56,40],
			[57,42],
			[58,32],
			[59,47],
			[60,45],
			[61,49],
			[62,51],
			[63,51],
			[64,53],
			[65,49],
			[66,49],
			[67,53],
			[68,52],
			[69,53],
			[70,48],
			[71,53],
			[72,51],
			[73,54],
			[74,56],
			[75,57],
			[76,59],
			[77,56],
			[78,61],
			[79,60],
			[80,63],
			[81,66],
			[82,62],
			[83,66],
			[84,70],
			[85,68],
			[86,66],
			[87,65],
			[88,64],
			[89,64],
			[90,68],
			[91,67],
			[92,70],
			[93,67],
			[94,66],
			[95,65],
			[96,64],
			[97,62],
			[98,64],
			[99,64],
			[100,61],
			[101,59],
			[102,51],
			[103,55],
			[104,57],
			[105,61],
			[106,55],
			[107,60],
			[108,62],
			[109,60],
			[110,66],
			[111,65],
			[112,64],
			[113,67],
			[114,68],
			[115,71],
			[116,71],
			[117,74],
			[118,67],
			[119,65],
			[120,58],
			[121,54],
			[122,54],
			[123,57],
			[124,57],
			[125,61],
			[126,55],
			[127,57],
			[128,52],
			[129,54],
			[130,52],
			[131,59],
		],
		color: '#2205bf',
		lines: {
		fillColor: { colors: [{ opacity: 0.02 }, { opacity: 0.18}]}
		}
	},{
		data: [
			[0,16],
			[1,8],
			[2,12],
			[3,11],
			[4,27],
			[5,22],
			[6,28],
			[7,24],
			[8,23],
			[9,22],
			[10,18],
			[11,12],
			[12,9],
			[13,5],
			[14,9],
			[15,10],
			[16,13],
			[17,18],
			[18,13],
			[19,18],
			[20,13],
			[21,11],
			[22,13],
			[23,15],
			[24,12],
			[25,17],
			[26,21],
			[27,18],
			[28,14],
			[29,18],
			[30,14],
			[31,13],
			[32,14],
			[33,15],
			[34,16],
			[35,19],
			[36,14],
			[37,17],
			[38,13],
			[39,9],
			[40,11],
			[41,13],
			[42,15],
			[43,13],
			[44,11],
			[45,12],
			[46,8],
			[47,10],
			[48,8],
			[49,8],
			[50,6],
			[51,8],
			[52,17],
			[53,15],
			[54,12],
			[55,16],
			[56,16],
			[57,19],
			[58,19],
			[59,21],
			[60,20],
			[61,19],
			[62,16],
			[63,14],
			[64,22],
			[65,24],
			[66,23],
			[67,24],
			[68,22],
			[69,21],
			[70,23],
			[71,21],
			[72,25],
			[73,24],
			[74,21],
			[75,24],
			[76,26],
			[77,24],
			[78,25],
			[79,25],
			[80,23],
			[81,28],
			[82,27],
			[83,26],
			[84,21],
			[85,21],
			[86,20],
			[87,23],
			[88,24],
			[89,28],
			[90,20],
			[91,21],
			[92,25],
			[93,29],
			[94,28],
			[95,23],
			[96,22],
			[97,27],
			[98,23],
			[99,29],
			[100,29],
			[102,24],
			[103,25],
			[104,25],
			[105,28],
			[106,30],
			[107,32],
			[108,32],
			[109,34],
			[110,37],
			[111,41],
			[112,37],
			[113,38],
			[114,34],
			[115,36],
			[116,36],
			[117,46],
			[118,45],
			[119,42],
			[120,42],
			[121,38],
			[122,34],
			[123,35],
			[124,31],
			[125,40],
			[126,42],
			[127,48],
			[128,42],
			[129,38],
			[130,32],
			[131,37],
		],
		color: '#0ca7ec',
		lines: {
		fillColor: { colors: [{ opacity: 0 }, { opacity: 0.2 }]}
		}
	}], 
	{
		series: {
			stack: 0,
			shadowSize: 0,
			lines: {
				show: true,
				lineWidth: 1.7,
				fill: true,
				fillColor: {
					colors: [{
						opacity: 0
					}, {
						opacity: 0.2
					}]
				}
			}
		},
		grid: {
			borderWidth: 0,
			labelMargin: 5,
			hoverable: true
		},
    	yaxis: {
			show: true,
			min: 0,
			max: 110,
			ticks: [[0,''],[20,'20K'],[40,'40K'],[60,'60K'],[80,'80K'],[100,'100K']],
			tickColor: 'rgb(142, 156, 173,0.1)',
			color: '#8e9cad',
    		},
    	xaxis: {
            show: true,
			ticks: [
				[0, '08:00'],
				[20, '09:00'],
				[40, '10:00'],
				[60, '11:00'],
				[80, '12:00'],
				[100, '13:00'],
				[120, '14:00'],
				[140, '15:00']
			],
            color: '#8e9cad',
			tickColor: 'rgb(142, 156, 173,0.1)',
            ticks: [[0,'OCT 21'],[25,'OCT 22'],[50,'OCT 23'],[75,'OCT 24'],[100,'OCT 25'],[125,'OCT 26']],
          }
        });
	/* Flot (#flotChart-1) closed */
		
	/* Chartjs (#statistics) */
	var myCanvas = document.getElementById("statistics");
	myCanvas.height="300";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 300);
	gradientStroke1.addColorStop(0, '#2205bf');
	
	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 390);
	gradientStroke2.addColorStop(0, '#0ca7ec');

    var myChart = new Chart( myCanvas, {
		type: 'bar',
		data: {
			labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
			datasets: [{
				label: 'Page Load time',
				data: [15, 18, 12, 14, 10, 15, 7, 14],
				backgroundColor: gradientStroke1,
				hoverBackgroundColor: gradientStroke1,
				hoverBorderWidth: 2,
				hoverBorderColor: 'gradientStroke1'
			}, {

			    label: 'Avg time on Page',
				data: [10, 14, 10, 15, 9, 14, 15, 20],
				backgroundColor: gradientStroke2,
				hoverBackgroundColor: gradientStroke2,
				hoverBorderWidth: 2,
				hoverBorderColor: 'gradientStroke2'
			}
		  ]
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: '#000',
				bodyFontColor: '#000',
				backgroundColor: '#fff',
				cornerRadius: 3,
				intersect: false,

			},
			legend: {
				display: false,
				labels: {
					usePointStyle: true,
					fontFamily: 'Montserrat',
				},
			},
			scales: {
				xAxes: [{
					 barPercentage: 0.8,
					 barSpacing:3,
					 ticks: {
						display: false,

					 },
					display: true,
					gridLines: {
						display: true,
						color: 'rgba(255,255,255,0.03)',
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						labelString: 'Month',
						fontColor: 'rgba(255,255,255,0.5)'
					}
				}],
				yAxes: [{
					ticks: {
						display: false,

					 },
					display: true,
					gridLines: {
						display: true,
						color: 'rgba(255,255,255,0.03)',
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						labelString: 'sales',
						fontColor: 'rgba(255,255,255,0.5)'
					}
				}]
			},
			title: {
				display: false,
				text: 'Normal Legend'
			}
		}
	});
	/* Chartjs (#statistics) closed */
	
	/* sparkline_bar */
	$(".sparkline_bar").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
		type: 'bar',
		height:35,
		width:50,
		barWidth: 4,
		barSpacing: 2,
		colorMap: {
			'9': '#a1a1a1'
		},
		barColor: '#2205bf'
	});
	/* sparkline_bar end */
	
	/* sparkline_bar1*/
	$(".sparkline_bar1").sparkline([2, 4, 3, 4, 5, 4,5,3,4], {
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
	/* sparkline_bar1 end */
	
	/* Chartjs (#doChart) */
	var doughnut = document.getElementById("doChart");
	var myDoughnutChart = new Chart(doughnut, {
		type: 'doughnut',
		data: {
		labels:["Desktop","Tab", "Mobile"],
		datasets: [{
			label: "My First dataset",
			data: [4100,2500, 1800],
			backgroundColor: ['#2205bf','#f57b4e','#08bfe0'],
			borderColor: ['#2205bf','#f57b4e','#08bfe0']
		 }]
	   },
	  options: {
			maintainAspectRatio : false,
			cutoutPercentage:80,
			legend: {
				display: false
			},
		}
	});
	/* Chartjs (#doChart) closed */
	
	/* piechart */
	$('.piechart').easyPieChart({
		barColor: "#00B2CA",
		easing: 'easeOutBounce',
		onStep: function(from, to, percent) {
			$(this.el).find('.percent').text(Math.round(percent));
		}
	});
	/* piechart closed */
		
});
