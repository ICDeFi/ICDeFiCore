$(function () {


	/* Banner fade-out */
	$("#skip").click(function() {
        $(".big-deal").fadeOut();
    })


	/* chart dropshadow */
	var draw = Chart.controllers.line.prototype.draw;
	Chart.controllers.lineShadow = Chart.controllers.line.extend({
	  draw: function () {
		draw.apply(this, arguments);
		var ctx = this.chart.chart.ctx;
		var _stroke = ctx.stroke;
		ctx.stroke = function () {
		  ctx.save();
		  ctx.shadowColor = 'transparant';
		  ctx.shadowBlur = 10;
		  ctx.shadowOffsetX = 8;
		  ctx.shadowOffsetY = 8;
		  _stroke.apply(this, arguments)
		  ctx.restore();
		}
	  }
	});

	/* Chart-js (#ecommerce-chart-1) */
	var myCanvas = document.getElementById("ecommerce-chart-1");
	myCanvas.height="295";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 200);
	gradientStroke1.addColorStop(0, '#2205bf');

	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 190);
	gradientStroke2.addColorStop(0, '#2205bf');
	var myChart = new Chart( myCanvas, {
		 type: 'lineShadow',
		lineDashType: "dash",
		data : {
			 labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
			 type: 'line',
			 datasets: [
			{
				label: "Online",
				data: [4, 2, 8, 4, 7, 4, 6, 4, 6, 3,14],
				backgroundColor:'transparent',
				borderColor: '#2205bf' ,
				pointBackgroundColor:'#fff',
				pointHoverBackgroundColor:gradientStroke1,
				pointBorderColor :gradientStroke1,
				pointHoverBorderColor :gradientStroke1,
				pointBorderWidth :0,
				pointRadius :0,
				pointHoverRadius :0,
				labelColor:gradientStroke1,
				borderWidth: 3,


			}, {
				label: "Offline",
				data: [7, 5, 12, 7, 12, 6, 10, 6, 11, 5,0,3,4],
				backgroundColor: 'transparent',
				borderColor: '#f58054 ',
				pointBackgroundColor:'#fff',
				pointHoverBackgroundColor:gradientStroke2,
				pointBorderColor :gradientStroke2,
				pointHoverBorderColor :gradientStroke2,
				pointBorderWidth :0,
				pointRadius :0,
				pointHoverRadius :0,
				borderWidth: 3,
				 borderDash: [10,5]

			}
		  ]
		},
		 options: {
			responsive: true,
			maintainAspectRatio: false,
			legend: {
			    labels: {
					fontColor: '#8492a6'
			    }
			},
			scales: {
				xAxes: [{
					display: true,
					gridLines: {
						display: true,
						color: 'rgb(142, 156, 173,0.1)',
						drawBorder: false
					},
					ticks: {
                            fontColor: '#8e9cad',
                            autoSkip: true,
                            maxTicksLimit: 9,
                            maxRotation: 0,
                            labelOffset: 10
                        },
					scaleLabel: {
						display: false,
						labelString: 'Month',
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					ticks: {
						fontColor: "#8e9cad",
					 },
					display: true,
					gridLines: {
						display: false,
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						labelString: 'sales',
						fontColor: 'transparent'
					}
				}]
			},
			title: {
				display: false,
				text: 'Normal Legend'
			},

			elements: {
				line: {
					borderWidth: 2
				},
				point: {
					radius: 0,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	})
	/* Chart-js (#ecommerce-chart-1) closed */

	/* Chart-js (#donutChart) */
	var doughnut = document.getElementById("donutChart");
	var myDoughnutChart = new Chart(doughnut, {
		type: 'doughnut',
		data: {
		labels:["Desktop","Tab", "Mobile"],
		datasets: [{
			label: "My First dataset",
			data: [4100,2500, 1800,1200],
			backgroundColor: ['rgb(34, 5, 191,0.8)','rgb(9, 176, 236,0.8)','rgb(249, 72, 89,0.9)','rgb(255, 171, 0,0.7)'],
			borderColor: ['rgb(34, 5, 191,0.2)','rgb(9, 176, 236,0.2)','rgb(249, 72, 89,0.4)','rgb(255, 171, 0,0.2)'],
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
	/* Chart-js (#donutChart) closed */

	/* echart (#echart3) */
	var chartdata = [{
		name: 'sales',
		type: 'bar',
		data: [10, 15, 9, 18],
		barWidth: 10,
	},  {
		name: 'growth',
		type: 'bar',
		data: [10, 14, 10, 15],
		barWidth: 10,
	}];
	var option3 = {
		grid: {
			top: '0',
			right: '20',
			bottom: '20',
			left: '32',
		},
		xAxis: {
			type: 'value',
			axisLine: {
				lineStyle: {
					color: 'transparant'
				}
			},
			axisLabel: {
				fontSize: 10,
				color: '#8e9cad'
			},
			splitLine: {
				lineStyle: {
					color: 'rgb(142, 156, 173,0.1)'
				}
			},
		},
		yAxis: {
			type: 'category',
			data: ['2017', '2018', '2019', '2020'],
			splitLine: {
				lineStyle: {
					color: 'transparant'
				}
			},
			axisLine: {
				lineStyle: {
					color: '#8e9cad'
				}
			},
			axisLabel: {
				fontSize: 10,
				color: '#8e9cad'
			},
		},
		series: chartdata,
		color: ['#2205bf', 'rgb(9, 176, 236,0.8)' ]
	};
	var chart3 = document.getElementById('echart3');
	var barChart3 = echarts.init(chart3);
	barChart3.setOption(option3);
	/* echart (#echart3) closed */

	/* Chart-js (#bar-chart) */
	var myCanvas = document.getElementById("bar-chart");
	myCanvas.height="202";
	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 120, 0, 180);
	gradientStroke1.addColorStop(0, 'rgb(10, 144, 251,0.9)');
	gradientStroke1.addColorStop(1, 'rgb(10, 144, 251,0.9)');

	var myChart = new Chart(myCanvas, {
		type: 'bar',
		data: {
			labels: ["2015", "2016", "2017", "2018", "2019"],
			datasets: [{
				label: 'Carrying Costs Of Inventory',
				data: [16, 14, 12, 14, 16, 15, 12, 14,18,10],
				backgroundColor: gradientStroke1,
				hoverBackgroundColor: gradientStroke1,
				hoverBorderWidth: 2,
				hoverBorderColor: 'gradientStroke1'
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
				},
			},
			scales: {
				xAxes: [{
					 barPercentage: 0.3,
					display: true,
					gridLines: {
						display: true,
						color: 'rgb(142, 156, 173,0.1)',
						drawBorder: false
					},
					ticks: {
                            fontColor: '#8e9cad',
                            autoSkip: true,
                            maxTicksLimit: 9,
                            maxRotation: 0,
                            labelOffset: 10
                        },
					scaleLabel: {
						display: false,
						labelString: 'Month',
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					ticks: {
						fontColor: "#8e9cad",
					 },
					display: true,
					gridLines: {
						display: false,
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						labelString: 'sales',
						fontColor: 'transparent'
					}
				}]
			},
			title: {
				display: false,
				text: 'Normal Legend'
			},

			elements: {
				line: {
					borderWidth: 2
				},
				point: {
					radius: 0,
					hitRadius: 10,
					hoverRadius: 4
				}
			}
		}
	});
	/* Chart-js (#bar-chart) closed */

});