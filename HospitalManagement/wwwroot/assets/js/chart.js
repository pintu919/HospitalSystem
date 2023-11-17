$(document).ready(function() {
	
	// Bar Chart

	var barChartData = {
		labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
		datasets: [{
			label: 'Dataset 1',
            backgroundColor: 'rgba(0, 102, 255, 0.8)',
            borderColor: 'rgba(0, 102, 255, 1)',
			borderWidth: 1,
			data: [35, 59, 80, 81, 56, 55, 40]
		}, {
			label: 'Dataset 2',
                backgroundColor: 'rgba(255, 173, 51, 0.8)',
                borderColor: 'rgba(255, 173, 51, 1)',
			borderWidth: 1,
			data: [28, 48, 40, 19, 86, 27, 90]
		}]
	};

	var ctx = document.getElementById('bargraph').getContext('2d');
	window.myBar = new Chart(ctx, {
		type: 'bar',
		data: barChartData,
		options: {
			responsive: true,
			legend: {
				display: false,
			}
		}
	});

	// Line Chart

	var lineChartData = {
		labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
		datasets: [{
			label: "My First dataset",
            backgroundColor: "rgba(0, 102, 255, 0.4)",
			data: [100, 70, 20, 100, 120, 50, 70, 50, 50, 100, 50, 90]
		}, {
		label: "My Second dataset",
                backgroundColor: "rgba(255, 173, 51, 0.4)",
		fill: true,
		data: [28, 48, 40, 19, 86, 27, 20, 90, 50, 20, 90, 20]
            },
            {
                label: "My third dataset",
                backgroundColor: "rgba(0, 221, 51, 0.4)",
                fill: true,
                data: [1, 95, 74, 37, 86, 27, 31, 90, 50, 20, 90, 200]
            }]
	};
	
	var linectx = document.getElementById('linegraph').getContext('2d');
	window.myLine = new Chart(linectx, {
		type: 'line',
		data: lineChartData,
		options: {
			responsive: true,
			legend: {
				display: false,
			},
			tooltips: {
				mode: 'index',
				intersect: false,
			}
		}
	});
	
	// Bar Chart 2
	
    barChart();
    
    $(window).resize(function(){
        barChart();
    });
    
    function barChart(){
        $('.bar-chart').find('.item-progress').each(function(){
            var itemProgress = $(this),
            itemProgressWidth = $(this).parent().width() * ($(this).data('percent') / 100);
            itemProgress.css('width', itemProgressWidth);
        });
    };
});