( function ( $ ) {
    "use strict";

    //Team chart
    var ctx = document.getElementById( "chartjs1" );
    var myChart = new Chart( ctx, {
        type: 'line',
        data: {
            labels: [ "2010", "2011", "2012", "2013", "2014", "2015", "2016" ],
            type: 'line',
            defaultFontFamily: 'Montserrat',
            datasets: [ {
                data: [ 0, 7, 3, 5, 2, 10, 7 ],
                label: "Expense",
                backgroundColor: 'rgba(33, 91, 220,.6)',
                borderColor: 'rgba(33, 91, 220,0.6)',
                borderWidth: 3.5,
                pointStyle: 'circle',
                pointRadius: 5,
                pointBorderColor: 'transparent',
                pointBackgroundColor: 'rgba(33, 91, 220,0.9)',
                    }, ]
        },
        options: {
            responsive: true,
			maintainAspectRatio: false,
            tooltips: {
                mode: 'index',
                titleFontSize: 12,
                titleFontColor: '#8492a6',
                bodyFontColor: '#8492a6',
                backgroundColor: '#fff',
                cornerRadius: 3,
                intersect: false,
            },
            legend: {
                display: false,
                position: 'top',
                labels: {
                    usePointStyle: true,
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
            }
        }
    } );


    //Sales chart
    var ctx = document.getElementById( "chartjs2" );
    var myChart = new Chart( ctx, {
        type: 'line',
        data: {
            labels: [ "2010", "2011", "2012", "2013", "2014", "2015", "2016" ],
            type: 'line',
            defaultFontFamily: 'Montserrat',
            datasets: [ {
                label: "Foods",
                data: [ 0, 30, 10, 120, 50, 63, 10 ],
                backgroundColor: 'transparent',
                borderColor: 'rgba(33, 91, 220)',
                borderWidth: 3,
                pointStyle: 'circle',
                pointRadius: 5,
                pointBorderColor: 'transparent',
                pointBackgroundColor: 'rgba(33, 91, 220)',
                    }, {
                label: "Electronics",
                data: [ 0, 50, 40, 80, 40, 79, 120 ],
                backgroundColor: 'transparent',
                borderColor: '#fa713b',
                borderWidth: 3,
                pointStyle: 'circle',
                pointRadius: 5,
                pointBorderColor: 'transparent',
                pointBackgroundColor: '#fa713b',
                    } ]
        },
        options: {
            responsive: true,
			maintainAspectRatio: false,
            tooltips: {
                mode: 'index',
                titleFontSize: 12,
                titleFontColor: '#8492a6  ',
                bodyFontColor: '#8492a6  ',
                backgroundColor: '#fff',
                titleFontFamily: 'Montserrat',
                bodyFontFamily: 'Montserrat',
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
            }
        }
    } );


    //line chart
    var ctx = document.getElementById( "chartjs3" );
    var myChart = new Chart( ctx, {
        type: 'line',
        data: {
            labels: [ "January", "February", "March", "April", "May", "June", "July" ],
            datasets: [
                {
                    label: "My First dataset",
                    borderColor: "rgba(33, 91, 220,.9)",
                    borderWidth: "1",
                    backgroundColor: "rgba(33, 91, 220,.7)",
                    data: [ 22, 44, 67, 43, 76, 45, 12 ]
                            },
                {
                    label: "My Second dataset",
                    borderColor: "#fa713b",
                    borderWidth: "1",
                    backgroundColor: "#fa713b",
                    pointHighlightStroke: "#fa713b",
                    data: [ 16, 32, 18, 26, 42, 33, 44 ]
                            }
                        ]
        },
        options: {
			legend: {
			    labels: {
					fontColor: '#8492a6'
			    }
			},
            responsive: true,
			maintainAspectRatio: false,
            tooltips: {
                mode: 'index',
                intersect: false
            },
            hover: {
                mode: 'nearest',
                intersect: true
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
						fontColor: '#8e9cad'
					}
				}]
			},
        },
			
       
    } );


    //bar chart
    var ctx = document.getElementById( "chartjs4" );
    var myChart = new Chart( ctx, {
        type: 'bar',
        data: {
            labels: [ "January", "February", "March", "April", "May", "June", "July" ],
            datasets: [
			{
				label: "My First dataset",
				data: [ 65, 59, 80, 81, 56, 55, 40 ],
				borderColor: "rgba(33, 91, 220, 0.9)",
				borderWidth: "0",
				backgroundColor: "rgba(33, 91, 220, 0.7)"
						},
			{
				label: "My Second dataset",
				data: [ 28, 48, 40, 19, 86, 27, 90 ],
				borderColor: "#fa713b",
				borderWidth: "0",
				backgroundColor: "#fa713b "
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
        }
    } );

    //radar chart
    var ctx = document.getElementById( "chartjs5" );
    var myChart = new Chart( ctx, {
        type: 'radar',
        data: {
            labels: [ [ "Eating", "Dinner" ], [ "Drinking", "Water" ], "Sleeping", [ "Designing", "Graphics" ], "Coding", "Cycling", "Running" ],
            datasets: [
                {
                    label: "My First dataset",
                    data: [ 65, 59, 66, 45, 56, 55, 40 ],
                    borderColor: "rgba(33, 91, 220, 0.6)",
                    borderWidth: "1",
                    backgroundColor: "rgba(33, 91, 220, 0.4)"
                            },
                {
                    label: "My Second dataset",
                    data: [ 28, 12, 40, 19, 63, 27, 87 ],
                    borderColor: '#fa713b',
                    borderWidth: "1",
                    backgroundColor: "#fa713b"
                            }
                        ]
        },
        options: {
			responsive: true,
			maintainAspectRatio: false,
            legend: {
                position: 'top'
            },
              scale: {
				 gridLines: {
					color: 'rgb(142, 156, 173,0.2)',
					display: true
				},
                ticks: {
					fontColor: "#8e9cad",
				},
            }
        }
    } );


    //pie chart
    var ctx = document.getElementById( "chartjs6" );
    var myChart = new Chart( ctx, {
        type: 'pie',
        data: {
            datasets: [ {
                data: [ 45, 25, 20, 10 ],
                backgroundColor: [
                                    "#215bdc",
                                    "#fa713b",
                                    "#00b8d9",
                                    "#36b37e"
                                ],
                hoverBackgroundColor: [

                                    "#215bdc",
                                    "#fa713b",
                                    "#00b8d9",
                                    "#36b37e"
                                ]

                            } ],
            labels: [
				"Server 1",
				"Server 2",
				"Server 3",
				"Server 4",
			],
			 labels: {
				fontColor: '#8492a6'
			},
				
						
        },
        options: {
            responsive: true,
			maintainAspectRatio: false,
        }
    } );

    //doughut chart
    var ctx = document.getElementById( "chartjs7" );
    var myChart = new Chart( ctx, {
        type: 'doughnut',
        data: {
            datasets: [ {
                data: [ 45, 25, 20, 10 ],
                backgroundColor: [
                                    "#215bdc",
                                    "#fa713b",
                                    "#ffb209",
                                    "#36b37e"
                                ],
                hoverBackgroundColor: [
                                    "#215bdc",
                                    "#fa713b",
                                    "#ffb209",
                                    "#36b37e"
                                ]

                            } ],
            labels: [
                            "Server 1",
                            "Server 2",
                            "Server 3",
                            "Server 4",
                        ]
        },
        options: {
            responsive: true,
			maintainAspectRatio: false,
        }
    } );

    //polar chart
    var ctx = document.getElementById( "chartjs8" );
    var myChart = new Chart( ctx, {
        type: 'polarArea',
        data: {
            datasets: [ {
                data: [ 15, 18, 9, 6, 19 ],
                backgroundColor: [
                                    "#215bdc",
                                    "#fa713b",
                                    "#ffb209",
                                    "#36b37e",
									"#21c44c"
                                ]

                            } ],
            labels: [
                            "Data1",
                            "Data2",
                            "Data3",
                            "Data4",
                            "Data5"
                        ]
        },
        options: {
            responsive: true,
			maintainAspectRatio: false,
			scale: {
				 gridLines: {
					color: 'rgb(142, 156, 173,0.2)',
					display: true
				},
                ticks: {
					fontColor: "#8e9cad",
				},
            }
        }
    } );

    // single bar chart
    var ctx = document.getElementById( "chartjs9" );
    var myChart = new Chart( ctx, {
        type: 'bar',
        data: {
            labels: [ "Sun", "Mon", "Tu", "Wed", "Th", "Fri", "Sat" ],
            datasets: [
                {
                    label: "My First dataset",
                    data: [ 60, 55, 75, 81, 56, 55,70 ],
                    borderColor: "rgba(33, 91, 220, 0.9)",
                    borderWidth: "0",
                    backgroundColor: "rgba(33, 91, 220, 0.8)"
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
					barPercentage: 0.2,
					gridLines: {
						display: true,
						color: 'rgb(142, 156, 173,0.1)',
						drawBorder: false
					},
					ticks: {
                            fontColor: '#8e9cad',
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
        }
    } );

	/* chartjs (#sales-statistics) */
	var myCanvas = document.getElementById("chartjs10");
	myCanvas.height="300";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 360);
	gradientStroke1.addColorStop(0, '#215bdc');
	gradientStroke1.addColorStop(1, '#215bdc');

	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 360);
	gradientStroke2.addColorStop(0, '#fa713b');
	gradientStroke2.addColorStop(1, '#fa713b');

    var myChart = new Chart( myCanvas, {
		type: 'bar',
		data: {
			labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
			datasets: [{
				label: 'Total Sales',
				data: [15, 13, 12, 14, 10, 15, 7, 14],
				backgroundColor: gradientStroke1,
				hoverBackgroundColor: gradientStroke1,
				hoverBorderWidth: 2,
				hoverBorderColor: 'gradientStroke1'
			},{
			label: 'Total Profits',
				data: [10, 14, 10, 15, 9, 13, 15, 18],
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
					display: true,
					barPercentage: 0.2,
					gridLines: {
						display: true,
						color: 'rgb(142, 156, 173,0.1)',
						drawBorder: false
					},
					ticks: {
                            fontColor: '#8e9cad',
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
			}
		}
	});
	/* chartjs (#sales-statistics) closed */

	/* chartjs (#sales) */
	var myCanvas = document.getElementById("chartjs11");
	myCanvas.height="300";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke = myCanvasContext.createLinearGradient(0, 0, 0, 380);
	gradientStroke.addColorStop(0, 'rgba(33, 91, 220,0.6)');
	gradientStroke.addColorStop(1, 'rgba(33, 91, 220,0.6)');
    var myChart = new Chart( myCanvas, {
		type: 'line',
		data: {
            labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
            type: 'line',
            datasets: [ {
				label: 'Return-On-Assets',
				data: [0, 50, 10, 100, 20, 130, 100, 140, 50],
				backgroundColor: gradientStroke,
				borderColor: 'rgba(33, 91, 220)',
				pointBackgroundColor:'#fff',
				pointHoverBackgroundColor:gradientStroke,
				pointBorderColor :'#215bdc',
				pointHoverBorderColor :gradientStroke,
				pointBorderWidth :2,
				pointRadius :6,
				pointHoverRadius :2,
				borderWidth: 2
            }, ]
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
					usePointStyle: false,
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
						display: true,
						labelString: 'Month',
						fontColor: '#8e9cad'
					}
				}],
				yAxes: [{
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
						display: true,
						labelString: 'sales',
						fontColor: '#8e9cad'
					}
				}]
			},
			title: {
				display: false,
				text: 'Normal Legend'
			}
		}
	});
	/* chartjs (#sales) closed */

	/* Chartjs (#total-customers) */
	var myCanvas = document.getElementById("chartjs12");
	myCanvas.height="233";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 380);
	gradientStroke1.addColorStop(0, '#fa713b');
	gradientStroke1.addColorStop(1, '#fa713b');

	var myChart = new Chart(myCanvas, {
		type: 'bar',
		data: {
			labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun" ,"Aug", "Sep"],
			datasets: [{
				label: 'Revenue',
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
			}
		}
	});
	/* Chartjs (#total-customers) closed */
	/* Chartjs (#total-coversations) */
	var ctx = document.getElementById('chartjs13').getContext('2d');
	var gradientStroke = myCanvasContext.createLinearGradient(0, 0, 0, 300);
	gradientStroke.addColorStop(0, '#215bdc');
	gradientStroke.addColorStop(1, '#215bdc');
    var myChart = new Chart(ctx, {
		type: 'line',
		data: {
			labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
			datasets: [{
				label: "Total-coversations",
				borderColor: gradientStroke,
				borderWidth: 2,
				backgroundColor: 'transparent',
				data: [0, 50, 0, 100, 50, 130, 100, 140]
			}]
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
	/* Chart-js (#site-executive) */
	var myCanvas = document.getElementById("chartjs14");
	myCanvas.height = "290";
	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 200);
	gradientStroke1.addColorStop(0, 'transparent');
	gradientStroke1.addColorStop(1, 'transparent');
	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 190);
	gradientStroke2.addColorStop(0, 'transparent');
	gradientStroke2.addColorStop(1, 'transparent');
	var myChart = new Chart(myCanvas, {
		type: 'line',
		data: {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
			type: 'line',
			datasets: [{
				label: "Users",
				data: [2, 7, 3, 9, 4, 5, 2, 8, 4, 6, 5, 2, 8, 4, 7, 2, 4, 6, 4, 8, 4, ],
				backgroundColor: gradientStroke1,
				borderColor: '#215bdc',
				pointBackgroundColor: '#fff',
				pointHoverBackgroundColor: gradientStroke1,
				pointBorderColor: '#215bdc',
				pointHoverBorderColor: gradientStroke1,
				pointBorderWidth: 2,
				pointRadius: 4,
				pointHoverRadius: 4,
				labelColor: gradientStroke1,
				borderWidth: 2,
			}, {
				label: "Page-views",
				data: [5, 3, 9, 6, 5, 9, 7, 3, 5, 2, 5, 3, 9, 6, 5, 9, 7, 3, 5, 2, 7, 10],
				backgroundColor: gradientStroke2,
				borderColor: '#fa713b',
				pointBackgroundColor: '#fff',
				pointHoverBackgroundColor: gradientStroke2,
				pointBorderColor: '#fa713b',
				pointHoverBorderColor: gradientStroke2,
				pointBorderWidth: 2,
				pointRadius: 4,
				pointHoverRadius: 4,
				borderWidth: 2,
			}]
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
				axisPointer: {
					label: {
						show: false,
					},
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
	var myCanvas = document.getElementById("chartjs15");
	myCanvas.height = "343";
	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 240);
	gradientStroke1.addColorStop(0, '#215bdc');
	gradientStroke1.addColorStop(1, '#215bdc');
	var gradientStroke2 = myCanvasContext.createLinearGradient(0, 0, 0, 280);
	gradientStroke2.addColorStop(0, '#fa713b');
	gradientStroke2.addColorStop(1, '#fa713b');
	var myChart = new Chart(myCanvas, {
		type: 'line',
		data: {
			labels: ["Jan", "feb", "Mar", "Apr", "May", "Jun", "Jul"],
			type: 'line',
			datasets: [{
				label: 'Social',
				data: [0, 70, 75, 120, 94, 141, 162],
				backgroundColor: 'transparent',
				borderColor: gradientStroke1,
				pointBackgroundColor: '#fff',
				pointHoverBackgroundColor: gradientStroke1,
				pointBorderColor: gradientStroke1,
				pointHoverBorderColor: gradientStroke1,
				pointBorderWidth: 4,
				pointRadius: 2,
				pointHoverRadius: 2,
				borderWidth: 2
			}, {
				label: "Paid Ads",
				data: [0, 50, 40, 80, 40, 79, 120],
				backgroundColor: 'transparent',
				borderColor: gradientStroke2,
				pointBackgroundColor: '#fff',
				pointHoverBackgroundColor: gradientStroke2,
				pointBorderColor: gradientStroke2,
				pointHoverBorderColor: gradientStroke2,
				pointBorderWidth: 4,
				pointRadius: 2,
				pointHoverRadius: 2,
				borderWidth: 2
			}]
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			tooltips: {
				mode: 'index',
				titleFontSize: 12,
				titleFontColor: 'rgba(0,0,0,0.5)',
				bodyFontColor: 'rgba(0,0,0,0.5)',
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
			}
		}
	});
	/* Chartjs (#total-income) */
	var myCanvas = document.getElementById("chartjs16");
	myCanvas.height="300";

	var myCanvasContext = myCanvas.getContext("2d");
	var gradientStroke1 = myCanvasContext.createLinearGradient(0, 0, 0, 280);
	gradientStroke1.addColorStop(0, '#215bdc');
	gradientStroke1.addColorStop(1, '#215bdc');

    var myChart = new Chart( myCanvas, {
        type: 'bar',
        data: {
            labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
            type: 'line',
            datasets: [ {
				label: 'Total-incomeincome',
				data: [24, 18, 28, 21, 32, 28,30,30,10,40,20],
				backgroundColor: 'rgb(33, 91, 220, 0.2)',
				borderColor: gradientStroke1,
				pointBackgroundColor:'#fff',
				pointHoverBackgroundColor:gradientStroke1,
				pointBorderColor :gradientStroke1,
				pointHoverBorderColor :gradientStroke1,
				pointBorderWidth :2,
				pointRadius :2,
				pointHoverRadius :2,
				borderWidth: 2
            }, ]
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
} )( jQuery );