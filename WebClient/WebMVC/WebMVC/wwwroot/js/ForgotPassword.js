
$(document).ready(function () {
    let validateGmail = false;
    //check gmail
    var loginInput = document.getElementById("email-forgot");
    loginInput.addEventListener("blur", function () {
        var email = this.value;
        var gmailRegex = /^[a-zA-Z0-9._%+-]+@[\w-]+\.\w{2,}$/;
        var errorEmail = document.getElementById("error-message-email");

        if (email) {
            if (gmailRegex.test(email)) {
                $.ajax({
                    url: "/Service/CheckGmail/?email=" + email,
                    type: "GET",
                    contentType: "application/json",
                    success: function (data) {
                        if (data != 0) {
                            errorEmail.textContent = "Email chưa được đăng kí";
                            errorEmail.style.display = "block";
                            validateGmail = false;
                        } else {
                            errorEmail.style.display = 'none';
                            validateGmail = true;
                        }
                    },
                });
            } else {
                errorEmail.textContent = "Email Không Hợp Lệ";
                errorEmail.style.display = "block";
                validateGmail = false;
            }
        } else {
            errorEmail.style.display = 'none';
        }
    });
    //end check gmail

    $("#forgotpass-form").submit(function (e) {
        if (!validateGmail) {
            e.preventDefault();
        } else {
            e.preventDefault();
            var email = $("#email-forgot").val();
            $.ajax({
                url: "/Customer/SendCodeForgot/?Email=" + email,
                type: "GET",
                contentType: "application/json",
                success: function (data) {
                    if (data === 0) {
                        alert("Xem lại gmail");
                    } else {
                        alert("Đã gửi link về email " + email);
                    }
                },
                error: function () {
                    alert("error");
                }
            });
        }
    });
}); 