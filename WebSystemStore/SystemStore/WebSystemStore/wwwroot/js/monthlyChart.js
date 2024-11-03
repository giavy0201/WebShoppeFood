//$$(document).ready(function () {
//    // Gửi yêu cầu khi trang được tải
//    fetchMonthlyOrdersData();

//    // Sự kiện onchange cho dropdown list
//    $('#select-month').change(function () {
//        fetchMonthlyOrdersData();
//    });

//    // Hàm để gửi yêu cầu lấy dữ liệu đơn hàng hàng tháng
//    function fetchMonthlyOrdersData() {
//        var selectedMonth = $('#select-month').val(); // Lấy giá trị tháng đã chọn từ dropdown list

//        // Gửi yêu cầu GET đến endpoint /Cart/monthly với tháng đã chọn
//        fetch('https://localhost:7152/Cart/monthly?month=' + selectedMonth)
//            .then(response => {
//                if (!response.ok) {
//                    throw new Error('Failed to fetch monthly orders data');
//                }
//                return response.json();
//            })
//            .then(data => {
//                // Xử lý dữ liệu khi yêu cầu thành công
//                console.log('Monthly orders data:', data);
//                // Ví dụ: vẽ biểu đồ từ dữ liệu ở đây
//                drawChart(data);
//            })
//            .catch(error => {
//                // Xử lý lỗi khi yêu cầu thất bại
//                console.error('Error fetching monthly orders:', error);
//                // Hiển thị thông báo lỗi cho người dùng
//                $('#error-message').text('Failed to fetch monthly orders data.');
//            });
//    }
$(document).ready(function () {
    // Xử lý khi người dùng click chọn tháng
    $('.month-selector').click(function () {
        var selectedMonth = $(this).data('month');

        // Gửi yêu cầu GET đến action GetMonthlyChart trong HomeController
        $.ajax({
            url: 'https://localhost:7152/Cart/monthly?month=',
            type: 'GET',
            data: { month: selectedMonth },
            contentType: 'application/json',
            success: function (data) {
                // Xử lý dữ liệu khi yêu cầu thành công
                console.log('Monthly orders data:', data);
                // Vẽ biểu đồ từ dữ liệu ở đây
                drawChart(data);
            },
            error: function (xhr, status, error) {
                // Xử lý lỗi khi yêu cầu thất bại
                console.error('Error fetching monthly orders:', error);
            }
        });
    });

    // Hàm để vẽ biểu đồ từ dữ liệu đơn hàng hàng tháng
    function drawChart(data) {
        // Biểu đồ cột
        var columnChart = new Chart($('#column-chart'), {
            type: 'bar',
            data: {
                labels: data.map(function (item) { return item.Month; }),
                datasets: [{
                    label: 'Số lượng đơn hàng',
                    data: data.map(function (item) { return item.OrderCount; }),
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });

        // Biểu đồ tròn
        var pieChart = new Chart($('#pie-chart'), {
            type: 'doughnut',
            data: {
                labels: data.map(function (item) { return item.Month; }),
                datasets: [{
                    data: data.map(function (item) { return item.OrderCount; }),
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.5)',
                        'rgba(54, 162, 235, 0.5)',
                        'rgba(255, 206, 86, 0.5)',
                        'rgba(75, 192, 192, 0.5)',
                        'rgba(153, 102, 255, 0.5)',
                        'rgba(255, 159, 64, 0.5)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                legend: {
                    position: 'bottom'
                }
            }
        });
    }
});
