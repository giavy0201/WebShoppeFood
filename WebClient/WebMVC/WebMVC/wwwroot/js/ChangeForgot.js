//$(document).ready(function () {
//    let validatePass = false;
//    let validateConfirm = false;

//    // check password
//    var passwordInput = document.getElementById("new-password");
//    passwordInput.addEventListener("blur", function () {
//        var inputPassword = this.value;
//        var passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=.]).{6,}$/;
//        var errorPassword = document.getElementById("error-message-password");

//        if (inputPassword) {
//            if (!passwordRegex.test(inputPassword)) {
//                errorPassword.textContent = "Mật khẩu không hợp lệ gồm trên 6 kí, chữ hoa, kí tự ...";
//                errorPassword.style.display = "block";
//                validatePass = false;
//            } else {
//                errorPassword.style.display = 'none';
//                validatePass = true;
//            }
//        } else {
//            errorPassword.style.display = 'none';
//        }
//    });
//    //end check password

//    // check confirmpassword
//    var passwordConfirmInput = document.getElementById("confirm-password");
//    passwordConfirmInput.addEventListener("blur", function () {
//        var inputPassword = this.value;
//        var passwords = document.getElementById("new-password").value;
//        var errorConfirm = document.getElementById("error-message-confirmpassword");

//        if (inputPassword) {
//            if (inputPassword !== passwords) {
//                errorConfirm.textContent = "Mật khẩu xác nhận không khớp";
//                errorConfirm.style.display = "block";
//                validateConfirm = false;
//            } else {
//                errorConfirm.style.display = 'none';
//                validateConfirm = true;
//            }
//        } else {
//            errorConfirm.style.display = 'none';
//        }
//    });
//    //end check confirmpassword

//    var checkShowPass = document.getElementById("register-showpass");
//    checkShowPass.addEventListener("change", function () {
//        if (checkShowPass.checked) {
//            passwordInput.type = "text";
//            passwordConfirmInput.type = "text";
//        } else {
//            passwordInput.type = "password";
//            passwordConfirmInput.type = "password";
//        }
//    });

//    $("#changeforgot-form").submit(function (e) {
//        if (!validatePass || !validateConfirm) {
//            e.preventDefault();
//        }
//    });
//});
$(document).ready(function () {
    let validatePass = false;
    let validateConfirm = false;

    // Check password
    $("#new-password").blur(function () {
        var inputPassword = $(this).val();
        var passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=.]).{6,}$/;
        var errorPassword = $("#error-message-password");

        if (inputPassword) {
            if (!passwordRegex.test(inputPassword)) {
                errorPassword.text("Mật khẩu không hợp lệ gồm trên 6 kí tự, chữ hoa, kí tự đặc biệt ...");
                errorPassword.show();
                validatePass = false;
            } else {
                errorPassword.hide();
                validatePass = true;
            }
        } else {
            errorPassword.hide();
        }
    });

    // Check confirm password
    $("#confirm-password").blur(function () {
        var inputPassword = $(this).val();
        var passwords = $("#new-password").val();
        var errorConfirm = $("#error-message-confirmpassword");

        if (inputPassword) {
            if (inputPassword !== passwords) {
                errorConfirm.text("Mật khẩu xác nhận không khớp");
                errorConfirm.show();
                validateConfirm = false;
            } else {
                errorConfirm.hide();
                validateConfirm = true;
            }
        } else {
            errorConfirm.hide();
        }
    });

    // Toggle password visibility
    $("#register-showpass").change(function () {
        var passwordInput = $("#new-password");
        var passwordConfirmInput = $("#confirm-password");
        if ($(this).prop("checked")) {
            passwordInput.attr("type", "text");
            passwordConfirmInput.attr("type", "text");
        } else {
            passwordInput.attr("type", "password");
            passwordConfirmInput.attr("type", "password");
        }
    });

    // Validate form submission
    $("#changeforgot-form").submit(function (e) {
        if (!validatePass || !validateConfirm) {
            e.preventDefault();
        }
    });
});
