$(document).ready(function () {
    $("input[name='FoodQuantity']").on("input", function () {
        var inputQuantity = this.value;
        var number = inputQuantity.replace(/\D/g,'');
        this.value = number;
        var quantityRegex = /^[0-9]$/;
        quantityRegex.test(number)

        var price = parseFloat($(this).closest("tr").find("td:eq(1)").text().replace(/,/g, ''));
        var quantity = parseFloat($(this).val());
        var newtotal = price * quantity;
        var totalprice = $(this).closest("tr").find("td:eq(3)");
        totalprice.text(newtotal.toLocaleString());

        var FoodID = $(this).closest("tr").find("input[name='FoodID']").val();
        updateCart(FoodID, quantity);
        updateTotal();
    });

});

function updateTotal() {
    var total = 0;
    $("table tbody tr").each(function () {
        var rowTotal = parseFloat($(this).find("td:eq(3)").text().replace(/,/g, '').trim());
        total += rowTotal;
    });
    $("#total-money-cart").html(total.toLocaleString() + '<sup>đ</sup>');
}

function updateCart(foodID, quantity) {
    $.ajax({
        url: '/Cart/UpdateCart',
        type: 'POST',
        data: { FoodID: foodID, FoodQuantity: quantity },
        error: function () {
            alert("Lỗi giỏ hàng");
        }
    });
}
