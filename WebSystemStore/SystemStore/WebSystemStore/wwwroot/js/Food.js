$(document).ready(function () {
    $('.edit-food').click(function () {
        var foodID = $(this).attr('data-food-id');
        var storeID = $(this).attr('data-store-id');
        var modalupdate = $('#modal-update-food');
        var obj = {
            FoodID: foodID
        };
        $.ajax({
            url: "/Food/DetailFood",
            type: "GET",
            contentType: "application/json",
            data: obj,
            success: function (data) {
                modalupdate.find('#id-food-input').val(foodID);
                modalupdate.find("#name-food-update-input").val(data.name);
                var imageSrc = "/image/Store-" + storeID + "/" + data.image;
                modalupdate.find("#image-food-update").attr('src', imageSrc);
                modalupdate.find("#price-food-update-input").val(data.price);
                modalupdate.find("#discount-food-update-input").val(data.discount);
                modalupdate.find("#description-food-update-input").val(data.description);
                modalupdate.find("#menu-food-update-input").val(data.menuID);
                modalupdate.find('#status-food-input').val(data.status);
                modalupdate.modal('show');
            }
        });
    });

    $('.delete-food').click(function () {
        var foodID = $(this).attr('data-food-id');
        var modalupdate = $('#modal-delete-food');
        modalupdate.find('#id-food-delete').val(foodID);
        modalupdate.modal('show');
    });

    $('.set-status-product').click(function () {
        var foodID = $(this).attr('data-food-id');
        $.ajax({
            url: "/Food/SetStatusproduct?FoodId=" + foodID,
            contentType: "application/json",
            success: function (data) {
                if (data.isSuccess === false) {
                    $("#toastAddToCart").find(".toast-body").html(data.message);
                    $("#toastAddToCart").attr('style', 'background-color:red')
                    $("#toastAddToCart").toast("show");
                } else if (data.isSuccess === true) {
                    $("#toastAddToCart").find(".toast-body").html(data.message);
                    $("#toastAddToCart").attr('style', 'background-color:green')
                    $("#toastAddToCart").toast("show");
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                }
            }
        });
    });
});

$("#form-update-food").submit(function (e) {
    e.preventDefault();
    var formData = new FormData();
    var modalupdate = $('#modal-update-food');
    formData.append('formFile', modalupdate.find('#img-food-update-input')[0].files[0]);
    formData.append('foodid', modalupdate.find("#id-food-input").val());
    formData.append('name', modalupdate.find("#name-food-update-input").val());
    formData.append('img', modalupdate.find("#img-food-update-input").val().replace(/C:\\fakepath\\/i, ''));
    formData.append('price', modalupdate.find("#price-food-update-input").val());
    formData.append('discount', modalupdate.find("#discount-food-update-input").val());
    formData.append('description', modalupdate.find("#description-food-update-input").val() ?? "");
    formData.append('menuid', modalupdate.find("#menu-food-update-input").val() ?? "");
    formData.append('status', modalupdate.find("#status-food-input").val());
    $.ajax({
        url: "/Food/UpdateFood",
        type: "POST",
        contentType: "application/json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.isSuccess === false) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-update-food').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:red')
                $("#toastAddToCart").toast("show");
            } else if (data.isSuccess === true) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-update-food').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:green')
                $("#toastAddToCart").toast("show");
                setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        }
    });
});

$("#form-delete-food").submit(function (e) {
    e.preventDefault();
    var foodid = $("#id-food-delete").val();
    $.ajax({
        url: "/Food/DeleteFood?FoodId="+foodid,
        type: "DELETE",
        contentType: "application/json",
        success: function (data) {
            if (data.isSuccess === false) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-delete-food').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:red')
                $("#toastAddToCart").toast("show");
            } else if (data.isSuccess === true) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-delete-food').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:green')
                $("#toastAddToCart").toast("show");
                setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        }
    });
});

// seach product
$(document).ready(function () {
    // // slider call
    var maxitems = $("#slider").attr('max-item-product');
    var max_value = $('#max-price').val();
    var min_value = $('#min-price').val();
    var valuemax = parseInt(maxitems) + 1000;
    $('#slider').slider({
        range: true,
        min: 0,
        max: valuemax,
        step: 1000,
        values: [min_value, max_value],
        slide: function (event, ui) {
            $("#min-price").val(ui.values[0]);
            $("#max-price").val(ui.values[1]);
            var min = ui.values[0].toLocaleString('it-IT');
            var max = ui.values[1].toLocaleString('it-IT')
            $('.ui-slider-handle:eq(0) .price-range-min').html(min);
            $('.ui-slider-handle:eq(1) .price-range-max').html(max);
            $('.price-range-both').html('<i>' + ui.values[0].toLocaleString('it-IT') + '-</i>' + ui.values[1].toLocaleString('it-IT'));
        }
    });
    $('.ui-slider-handle:eq(0)').append('<span class="price-range-min value-min">' + $('#slider').slider('values', 0).toLocaleString('it-IT') + '</span>');
    $('.ui-slider-handle:eq(1)').append('<span class="price-range-max value-max">' + $('#slider').slider('values', 1).toLocaleString('it-IT') + '</span>');

});