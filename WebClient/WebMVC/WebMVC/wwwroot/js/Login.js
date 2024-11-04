$(document).ready(function () {
    let validateGmail = false;
    //check gmail
    var loginInput = document.getElementById("login-name");
    loginInput.addEventListener("blur", function () {
        var email = this.value;
        var gmailRegex = /^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$/;
        var errorEmail = document.getElementById("error-message-email");

        if (email) {
            if (!gmailRegex.test(email)) {
                errorEmail.textContent = "Email Không Hợp Lệ";
                errorEmail.style.display = "block";
                validateGmail = false;
            } else {
                errorEmail.style.display = 'none';
                validateGmail = true;
            }
        } else {
            errorEmail.style.display = 'none';
        }
    });
    //end check gmail

    $("#form-login").submit(function (e) {
        if (!validateGmail) {
            e.preventDefault();
            return;
        }
    });
});