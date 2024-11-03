$("#form-create-menu").submit(function (e) {
    e.preventDefault();
    var formData = {
        menuName: $("#name-menu-input").val(),
    };
    $.ajax({
        url: "/Menu/CreateMenu",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function (data) {
            if (data.isSuccess === false) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-create-menu').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:red')
                $("#toastAddToCart").toast("show");
            } else if (data.isSuccess === true) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-create-menu').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:green')
                $("#toastAddToCart").toast("show");
                setTimeout(function () {
                    window.location.href = "/Menu/ListMenu";
                }, 2000);
            }
        }
    });
});
$(document).ready(function () {
    $('.edit-menu').click(function () {
        var menuID = $(this).attr('data-menu-id');
        var modalupdate = $('#modal-update-menu');
        var obj = {
            MenuID: menuID
        };
        $.ajax({
            url: "/Menu/DetailMenu",
            type: "GET",
            contentType: "application/json",
            data: obj,
            success: function (data) {
                modalupdate.find('#id-menu-input').val(menuID);
                modalupdate.find("#name-menu-update-input").val(data.name);
                modalupdate.find('#status-menu-input').val(data.status);
                modalupdate.modal('show');
            }
        });
    });

    $('.delete-menu').click(function () {
        var menuID = $(this).attr('data-menu-id');
        var modalupdate = $('#modal-delete-menu');
        modalupdate.find('#id-menu-delete').val(menuID);
        modalupdate.modal('show');
    });
});

$("#form-update-menu").submit(function (e) {
    e.preventDefault();
    var formData = {
        menuID: $("#id-menu-input").val(),
        menuName: $("#name-menu-update-input").val(),
        status: $("#status-menu-input").val()
    };
    $.ajax({
        url: "/Menu/UpdateMenu",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function (data) {
            if (data.isSuccess === false) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-update-menu').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:red')
                $("#toastAddToCart").toast("show");
            } else if (data.isSuccess === true) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-update-menu').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:green')
                $("#toastAddToCart").toast("show");
                setTimeout(function () {
                    window.location.href = "/Menu/ListMenu";
                }, 2000);
            }
        }
    });
});

$("#form-delete-menu").submit(function (e) {
    e.preventDefault();
    var menuID = $("#id-menu-delete").val();
    $.ajax({
        url: "/Menu/DeleteMenu?MenuID=" + menuID,
        type: "DELETE",
        contentType: "application/json",
        success: function (data) {
            if (data.isSuccess === false) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-delete-menu').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:red')
                $("#toastAddToCart").toast("show");
            } else if (data.isSuccess === true) {
                $("#toastAddToCart").find(".toast-body").html(data.message);
                $('#modal-delete-menu').modal("hide");
                $("#toastAddToCart").attr('style', 'background-color:green')
                $("#toastAddToCart").toast("show");
                setTimeout(function () {
                    window.location.href = "/Menu/ListMenu";
                }, 2000);
            }
        }
    });
});