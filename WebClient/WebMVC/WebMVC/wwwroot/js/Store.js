$(document).ready(function () {
    // click menu move
    $('.menu-store').click(function () {
        var MenuID = $(this).attr("MenuID");
        var MenuFood = $('.list-food-namemenu');
        var selectMenu = MenuFood.filter("[MenuID='" + MenuID + "']");
        $('html, body').animate({
            scrollTop: selectMenu.offset().top
        }, 1);
    });

    //notification close
    var status = true;
    var StatusID = $("#time-store").attr("statusID");
    if (StatusID == 0) {
        alert("Quán Đang Đóng Cửa");
        status = false;
    }

    var buttonsAdd = document.querySelectorAll("#default-store-add");
    buttonsAdd.forEach(function (button) {
        button.addEventListener("click", function (e) {
            if (!status) {
                e.preventDefault();
            }
        });
    });
});
