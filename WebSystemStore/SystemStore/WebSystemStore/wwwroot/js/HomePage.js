$(document).ready(function () {
    timenow();
    selectOrder();
    selectListOrderToday();
    totalMoneyToday();
    function timenow() {
        var today = new Date();
        var options = { timeZone: 'Asia/Ho_Chi_Minh', hour12: false };
        var formattedTime = today.toLocaleString('vi-VN', options);
        $('#time-now').html(formattedTime);
        $('#time-now-responsive').html(formattedTime);
        setTimeout(timenow, 1000);
    }

    function selectOrder() {
        var storeid = $("#store-id-generic").val();
        $.ajax({
            url: "/Service/GetListCartOrderToday?StoreID=" + storeid,
            type: "Post",
            contentType: "application/json",
            success: function (data) {
                $("#cart-order-xll").html(data + " Đơn");
                $("#cart-order-mobile").html(data + " Đơn");
            },
            error: function () {
                $("#cart-order-xll").html("0 Đơn");
                $("#cart-order-mobile").html("0 Đơn");
            }
        });
    }

    function selectListOrderToday() {
        var storeid = $("#store-id-generic").val();
        $.ajax({
            url: "/Service/GetListCartToday?StoreID=" + storeid,
            type: "Get",
            contentType: "application/json",
            success: function (data) {
                $("#cart-order-today-xxl").html(data + " Đơn");
                $("#cart-order-today-mobile").html(data + " Đơn");
            },
            error: function () {
                $("#cart-order-today-xxl").html("0 Đơn");
                $("#cart-order-today-mobile").html("0 Đơn");
            }
        });
    }

    function totalMoneyToday() {
        var storeid = $("#store-id-generic").val();
        $.ajax({
            url: "/Service/GetTotalMoneyToDay?StoreID=" + storeid,
            type: "Get",
            contentType: "application/json",
            success: function (data) {
                var formattedData = parseFloat(data).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                $("#total-cart-today-xxl").html(formattedData);
                $("#total-cart-today-mobile").html(formattedData);
            },
            error: function () {
                $("#total-cart-today-xxl").html("0 <sup>đ</sup>");
                $("#total-cart-today-mobile").html("0 <sup>đ</sup>");
            }
        });
    }
});