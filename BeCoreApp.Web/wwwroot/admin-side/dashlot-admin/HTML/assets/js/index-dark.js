
$(function(e){
  'use strict'

	/* Chartjs (#linechart) */
	var myCanvas = document.getElementById("line-chart");
	myCanvas.height="280";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 500);
	gradientStroke1.addColorStop(0, 'rgb(2, 69, 218,0.7)');

	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 390);
	gradientStroke2.addColorStop(0, 'rgb(250, 113, 59,0.8)');

    var myChart = new Chart( myCanvas, {
		type: 'line',
		data: {
			labels: ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"],
			datasets: [{
				label: 'Last month',
				data: [3, 5, 7, 4, 6, 3, 5, 3, 5, 3,4,14],
				backgroundColor: gradientStroke1,
				borderWidth: 2,
				hoverBackgroundColor: gradientStroke1,
				hoverBorderWidth: 0,
				borderColor: gradientStroke1,
				hoverBorderColor: 'gradientStroke1',
				lineTension: .3,
				pointBorderWidth: 0,
				pointHoverRadius: 4,
				pointHoverBorderColor: "#fff",
				pointHoverBorderWidth: 0,
				pointRadius: 0,
				pointHitRadius: 0,
			}, {

			    label: 'This Month',
				data: [7, 6, 10, 6, 8, 5, 9, 5, 10, 4],
				backgroundColor: gradientStroke2,
				borderWidth: 2,
				hoverBackgroundColor: gradientStroke2,
				hoverBorderWidth: 0,
				borderColor: gradientStroke2,
				hoverBorderColor: 'gradientStroke2',
				lineTension: .3,
				pointBorderWidth: 0,
				pointHoverRadius: 4,
				pointHoverBorderColor: "#fff",
				pointHoverBorderWidth: 0,
				pointRadius: 0,
				pointHitRadius: 0,


			}
		  ]
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: 'rgba(225,225,225,0.9)',
				bodyFontColor: 'rgba(225,225,225,0.9)',
				backgroundColor: 'rgba(0,0,0,0.5)',
				cornerRadius: 3,
				intersect: false,
			},
			legend: {
				display: true,
				labels: {
					usePointStyle: true,
					fontColor: "#8492a6",
				},
			},
			scales: {
				xAxes: [{
					ticks: {
						fontColor: "#8492a6",
					 },
					display: true,
					gridLines: {
						display: false,
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						fontColor: '#8492a6'
					}
				}],
				yAxes: [{
					ticks: {
						fontColor: "#8492a6",
					 },
					display: true,
					gridLines: {
						display: false,
						drawBorder: false
					},
					scaleLabel: {
						display: false,
						fontColor: '#8492a6'
					}
				}]
			},
			title: {
				display: false,
				text: 'Normal Legend'
			}
		}
	});
	/* Chartjs (#linechart) closed */

	/* Chartjs (#earning) */
	var ctx = document.getElementById("earning");
    var myChart = new Chart( ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul','Aug','Sep','Oct'],
            type: 'line',
            datasets: [ {
                data: [320, 220, 400, 190, 860, 270, 900, 450, 600, 380],
                backgroundColor: 'transparent',
                borderColor: '#007bff',
				borderWidth:4,
				pointBorderWidth :0,
				pointRadius :0,
				pointHoverRadius :0,
				pointBackgroundColor:'#fff',
            }, ]
        },
        options: {

            maintainAspectRatio: true,
            legend: {
                display: false
            },
            responsive: true,
            tooltips: {
                mode: 'index',
                titleFontSize: 12,
                titleFontColor: '#000',
                bodyFontColor: '#000',
                backgroundColor: '#fff',
                cornerRadius: 0,
                intersect: false,
            },
            scales: {
                xAxes: [ {
                    gridLines: {
                        color: 'transparent',
                        zeroLineColor: 'transparent'
                    },
                    ticks: {
                        fontSize: 2,
                        fontColor: 'transparent'
                    }
                } ],
                yAxes: [ {
                    display:false,
                    ticks: {
                        display: false,
                    }
                } ]
            },
            title: {
                display: false,
            },
            elements: {
                line: {
                    borderWidth: 3
                },
                point: {
                    radius: 0,
                    hitRadius: 10,
                    hoverRadius: 4
                }
            }
        }
    });
	/* Chartjs (#earning) */

	/* Chartjs (#leads) */
	var myCanvas = document.getElementById("leads");

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 500);
	gradientStroke1.addColorStop(0, '#007bff');

	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 390);
	gradientStroke2.addColorStop(0, 'rgb(250, 113, 59,0.8)');

	var gradientStroke3 = myCanvasContext.createLinearGradient(0, 0, 0, 390);
	gradientStroke3.addColorStop(0, 'rgb(236, 232, 245,0.1)');


    var myChart = new Chart( myCanvas, {
		type: 'bar',
		data: {
			labels: ["0", "1", "2", "3", "4", "5" ,"6","7","8"],
			datasets: [{
				label: 'Open leads',
				data: [3, 3, 7, 4, 6, 3, 5, 3, 5, 3,4,14],
				backgroundColor: gradientStroke1,
				borderWidth: 1,
				hoverBackgroundColor: gradientStroke1,
				hoverBorderWidth: 0,
				borderColor: gradientStroke1,
				hoverBorderColor: 'gradientStroke1',
				lineTension: .3,
				pointBorderWidth: 0,
				pointHoverRadius: 4,
				pointHoverBorderColor: "#fff",
				pointHoverBorderWidth: 0,
				pointRadius: 0,
				pointHitRadius: 0,
			}, {

			    label: 'Close Leads',
				data: [7, 6, 10, 6, 8, 10, 9, 5, 10, 4],
				backgroundColor: gradientStroke2,
				borderWidth: 1,
				hoverBackgroundColor: gradientStroke2,
				hoverBorderWidth: 0,
				borderColor: gradientStroke2,
				hoverBorderColor: 'gradientStroke2',
				lineTension: .3,
				pointBorderWidth: 0,
				pointHoverRadius: 4,
				pointHoverBorderColor: "#fff",
				pointHoverBorderWidth: 0,
				pointRadius: 0,
				pointHitRadius: 0,
			},
			{

			    label: 'Total Leads',
				data: [12, 12, 12, 12, 12, 12, 12, 12, 12, 12],
				backgroundColor: gradientStroke3,
				borderWidth: 1,
				hoverBackgroundColor: gradientStroke3,
				hoverBorderWidth: 0,
				borderColor: gradientStroke3,
				hoverBorderColor: 'gradientStroke3',
				lineTension: .3,
				pointBorderWidth: 0,
				pointHoverRadius: 4,
				pointHoverBorderColor: "#fff",
				pointHoverBorderWidth: 0,
				pointRadius: 0,
				pointHitRadius: 0,
			}
		  ]
		},
		options: {

			responsive: true,
			maintainAspectRatio: false,
			layout: {
				padding: {
					left: 0,
					right: 0,
					top: 0,
					bottom: 0
				}
			},
			scales: {
				yAxes: [{
					display: false,
					gridLines: {
						display: true,
						color: "#eaf0f7",
					},
					ticks: {
						beginAtZero: true,
						fontColor: "#8492a6"
					},
				}],
				xAxes: [{
                    barPercentage: 0.2,
					barValueSpacing :3,
					barDatasetSpacing : 3,
					stacked: true,
					ticks: {
						beginAtZero: true,
						fontColor: "#8492a6"
					},
					gridLines: {
						color: "rgb(234, 240, 247,0.1)",
						display: false
					},

				}]
			},
			legend: {
				display: false
			},
			elements: {
				point: {
					radius: 0
				}
			}
		}
	});
	/* Chartjs (#leads) closed */

	/* Chartjs (#areachart1) */
	var ctx = document.getElementById("areaChart1");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17'],
			type: 'line',
			datasets: [{
				data: [45, 23, 32, 67, 49, 72, 52, 55, 46, 54, 32, 74, 88, 36, 36, 32, 48, 54,110],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#007bff',
				borderWidth: '2.5',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				 enabled: false
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
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
		}
	});
	/* Chartjs (#areachart1) closed */

	/* Chartjs (#areachart2) */
	var ctx = document.getElementById("areaChart2");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17'],
			type: 'line',
			datasets: [{
				data: [28, 56, 36, 32, 48, 54, 37,58, 66, 53, 21, 24, 14, 45, 0, 32, 67, 49, 52, 55, 46, 54,130],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#f88a61',
				borderWidth: '2.5',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				 enabled: false
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
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
		}
	});
	/* Chartjs (#areachart2) closed */
	
	/* Chartjs (#areachart3) */
	var ctx = document.getElementById("areaChart3");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17','Date 18', 'Date 19'],
			type: 'line',
			datasets: [{
				data: [58, 78, 55, 41,31, 45, 54, 51, 32, 48, 88, 66, 36, 32, 48, 24, 14, 45, 0, 32, 67, 49, 54, 87,130],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#09b0ec',
				borderWidth: '2.5',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				 enabled: false
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
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
		}
	});
	/* Chartjs (#areachart3) closed */

	/* Chartjs (#areachart4) */
	var ctx = document.getElementById("areaChart4");
	var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ['Date 1', 'Date 2', 'Date 3', 'Date 4', 'Date 5', 'Date 6', 'Date 7', 'Date 8', 'Date 9', 'Date 10', 'Date 11', 'Date 12', 'Date 13', 'Date 14', 'Date 15', 'Date 16', 'Date 17'],
			type: 'line',
			datasets: [{
				data: [28, 56, 36, 52, 48, 24, 14, 45, 80, 32, 45, 54, 51, 52, 48, 54, 67, 49, 58, 78,54,120],
				label: 'Admissions',
				backgroundColor: 'transparent',
				borderColor: '#1dab2d',
				borderWidth: '3',
				pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
			}]
		},
		options: {
			maintainAspectRatio: false,
			legend: {
				display: false
			},
			responsive: true,
			tooltips: {
				 enabled: false
			},
			scales: {
				xAxes: [{
					gridLines: {
						color: 'transparent',
						zeroLineColor: 'transparent'
					},
					ticks: {
						fontSize: 2,
						fontColor: 'transparent'
					}
				}],
				yAxes: [{
					display: false,
					ticks: {
						display: false,
					}
				}]
			},
			title: {
				display: false,
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
		}
	});
	/* Chartjs (#areachart4 closed) */
	
	/* On-going project scroll  */
	const ps3 = new PerfectScrollbar('.m-scroll', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	
	/* Activity scroll */
	const ps4 = new PerfectScrollbar('.activity-scroll', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});

	/* Banner-fade-out */
	$("#skip").click(function() {
        $(".banner").fadeOut();
      })
});