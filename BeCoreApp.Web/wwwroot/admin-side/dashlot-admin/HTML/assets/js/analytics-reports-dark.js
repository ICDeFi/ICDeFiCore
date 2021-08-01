$(function(e){
  'use strict'
  
	/* Chartjs (#bar-chart-horizontal) */
	new Chart(document.getElementById("bar-chart-horizontal"), {
    type: 'horizontalBar',
	data: {
		labels: ["Organic", "Direct", "Campagion" ],
		datasets: [
				{
					label: "Traffic Source",
					backgroundColor: ["rgba(0, 123, 255,0.7)", "rgba(255, 102, 0,0.7)","rgba(9, 176, 236,0.7)"],
					data: [5478,2267,934,],
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
					 barPercentage: 0.2,
					 barSpacing:3,
					ticks: {
						fontColor: "#8e9cad",
					 },
					display: true,
					gridLines: {
						display: true,
						color: 'rgb(142, 156, 173,0.1)',
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						labelString: 'Month',
						fontColor: '#8492a6 '
					}
				}],
				yAxes: [{
					 barPercentage: 1,
					 barSpacing:2,
					ticks: {
						fontColor: "#8e9cad",
					 },
					display: true,
					gridLines: {
						display: true,
						color: 'rgb(142, 156, 173,0.1)',
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						labelString: 'sales',
						fontColor: '#8492a6 '
					}
				}]
			},
			title: {
				display: false,
				text: 'Normal Legend'
			}
		}
	});
	/* Chartjs (#bar-chart-horizontal) closed */

	/* Chartjs (#site-executive) */
	var myCanvas = document.getElementById("site-executive");
	myCanvas.height="295";
	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 200);
	gradientStroke1.addColorStop(0, '#007bff');

	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 190);
	gradientStroke2.addColorStop(0, 'rgb(255, 148, 76,0.4)');
	var myChart = new Chart( myCanvas, {
		type: 'line',
		lineDashType: "dash",
		data : {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
			
			 datasets: [
			{
				label: "Domestic",
				data: [2,7,3,5,4,5,2,8,4,6,5,2,8,4,7,2,4,6,4,8,4,],
				backgroundColor:'transparent',
				borderColor: '#007bff' ,
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
				label: "International",
				data: [5,9,5,9,5,9,7,3,5,2,5,3,9,6,5,9,7,3,5,2,7,10],
				backgroundColor: 'transparent',
				borderColor: 'rgb(255, 148, 76,0.9)',
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
			tooltips: {
				show: true,
				showContent: true,
				alwaysShowContent: true,
				triggerOn: 'mousemove',
				trigger: 'axis',
				axisPointer:
				{
					label: {
						show: false,
						color: '#8492a6 ',
					},
				},
				gridLines: {
					display: true,
					color: 'rgb(142, 156, 173,0.1)',
					drawBorder: false
				},
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
	/* Chartjs (#site-executive) closed */
	
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
		barColor: '#007bff'
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


});


