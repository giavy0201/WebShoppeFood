$(document).ready(function () {
    $('.update-store').click(function () {
        var storeID = $(this).attr('data-store-id');
        var modalupdate = $('#modal-update-infostore');
        var obj = {
            StoreID: storeID
        };
        $.ajax({
            url: "/Store/DetailStore",
            type: "GET",
            contentType: "application/json",
            data: obj,
            success: function (data) {
                modalupdate.find('#id-store-input').val(storeID);
                modalupdate.find("#name-store-update-input").val(data.name);
                var imageSrc = "/image/Store-" + storeID + "/" + data.image;
                modalupdate.find("#image").attr('src', imageSrc);
                modalupdate.find("#timeopen-store-update-input").val(data.timeOpen);
                modalupdate.find("#timeclose-store-update-input").val(data.timeClose);
                modalupdate.find("#preferential-store-update-input").val(data.preferential);
                modalupdate.find("#address-store-update-input").val(data.address);
                modalupdate.find('#content-store-update-input').val(data.contentID);
                modalupdate.find('#status-store-input').val(data.status);
                GetCity(data.wardID);
                modalupdate.modal('show');
            }
        });
    });

    //fill data-city-district-ward
    function GetCity(WardID) {
        $.ajax({
            url: "/Store/ListLocationID",
            type: "GET",
            data: { WardID: WardID },
            success: function (data) {
                $("#city-store-input").val(data.cityID);
                GetDistrict(data.districtID, data.wardID);
            },
        });

    }
    function GetDistrict(DistrictID, WardID) {
        var cityId = $("#city-store-input").val();
        if (cityId) {
            $.ajax({
                url: "/Store/ListDistrictByCity",
                type: "GET",
                data: { CityID: cityId },
                success: function (data) {
                    var items = '<option value="">Chọn Quận/Huyện</option>';
                    $(data).each(function () {
                        items += "<option value=" + this.id + ">" + this.name + "</option>";
                    });
                    $('#district-store-input').html(items);
                    $("#district-store-input").val(DistrictID)
                    GetWard(WardID);
                },
            });
        }
    };
    function GetWard(WardID) {
        var districtId = $("#district-store-input").val();
        if (districtId) {
            $.ajax({
                url: "/Store/ListWardByDistrict",
                type: "GET",
                data: { DistrictID: districtId },
                success: function (data) {
                    var items = '<option value="">Chọn Phường</option>';
                    $(data).each(function () {
                        items += "<option value=" + this.id + ">" + this.name + "</option>";
                    });
                    $('#ward-store-input').html(items);
                    $("#ward-store-input").val(WardID)
                }
            });
        } 
    };
    //select list city - district - ward
    $("#city-store-input").on("change", function () {
        var cityId = $(this).val();
        if (cityId) {
            $.ajax({
                url: "/Store/ListDistrictByCity",
                type: "GET",
                data: { CityID: cityId },
                success: function (data) {
                    var items = '<option value="">Chọn Quận/Huyện</option>';
                    $(data).each(function () {
                        items += "<option value=" + this.id + ">" + this.name + "</option>";
                    });
                    $('#district-store-input').html(items);
                },
            });
        } else {
            $('#district-store-input').empty();
            $('#ward-store-input').empty();
        }
    });

    $("#district-store-input").on("change", function () {
        var districtId = $(this).val();
        if (districtId) {
            $.ajax({
                url: "/Store/ListWardByDistrict",
                type: "GET",
                data: { DistrictID: districtId },
                success: function (data) {
                    var items = '<option value="">Chọn Phường</option>';
                    $(data).each(function () {
                        items += "<option value=" + this.id + ">" + this.name + "</option>";
                    });
                    $('#ward-store-input').html(items);
                }
            });
        } else {
            $('#ward-store-input').empty();
        }
    });
});

$("#form-update-infostore").submit(function (e) {
    e.preventDefault();
    var formData = new FormData();
    var modalupdate = $('#modal-update-infostore');
    formData.append('formFile', modalupdate.find('#img-store-update-input')[0].files[0]);
    formData.append('storeID', modalupdate.find("#id-store-input").val());
    formData.append('name', modalupdate.find("#name-store-update-input").val());
    formData.append('img', modalupdate.find("#img-store-update-input").val().replace(/C:\\fakepath\\/i, ''));
    formData.append('timeOpen', modalupdate.find("#timeopen-store-update-input").val());
    formData.append('timeClose', modalupdate.find("#timeclose-store-update-input").val());
    formData.append('preferential', modalupdate.find("#preferential-store-update-input").val() ?? "");
    formData.append('location', modalupdate.find("#address-store-update-input").val());
    formData.append('wardID', modalupdate.find("#ward-store-input").val());
    formData.append('contentID', modalupdate.find("#content-store-update-input").val());
    formData.append('status', modalupdate.find("#status-store-input").val());
    $.ajax({
        url: "/Store/UpdateStore",
        type: "POST",
        contentType: "application/json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (value) {
            if (value.isSuccess === false) {
                if (value.data == null) {
                    $("#toastAddToCart").find(".toast-body").html(value.message);
                    $('#modal-update-infostore').modal("hide");
                    $("#toastAddToCart").attr('style', 'background-color:red')
                    $("#toastAddToCart").toast("show");
                } else {
                    $("#toastAddToCart").find(".toast-body").html(value.data);
                    $('#modal-update-infostore').modal("hide");
                    $("#toastAddToCart").attr('style', 'background-color:red')
                    $("#toastAddToCart").toast("show");
                }
            } else if (value.isSuccess === true) {
                $("#toastAddToCart").find(".toast-body").html(value.message);
                $('#modal-update-infostore').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:green')
                $("#toastAddToCart").toast("show");
                setTimeout(function () {
                    location.reload();
                }, 2000);
            }
        }
    });
});