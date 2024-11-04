//$(document).ready(function () {
//    let validateGmail = false;
//    let validatePass = false;
//    let validatePhone = true;
//    let validateConfirm = false;
//    //check gmail
//    var loginInput = document.getElementById("register-email");
//    loginInput.addEventListener("blur", function () {
//        var email = this.value;
//        var gmailRegex = /^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$/;
//        var errorEmail = document.getElementById("error-message-email");
//        if (email) {
//            if (gmailRegex.test(email)) {
//                $.ajax({
//                    url: "/Service/CheckGmail/?email=" + email,
//                    type: "GET",
//                    contentType: "application/json",
//                    success: function (data) {
//                        if (data === 0) {
//                            errorEmail.textContent = "Email đã tồn tại";
//                            errorEmail.style.display = "block";
//                            validateGmail = false;
//                        } else {
//                            validateGmail = true;
//                            errorEmail.style.display = 'none';
//                        }
//                    },
//                });
//            } else {
//                errorEmail.textContent = "Email Không Hợp Lệ";
//                errorEmail.style.display = "block";
//                validateGmail = false;
//            }
//        } else {
//            errorEmail.style.display = 'none';
//        }
//    });
//    //end check gmail
//    // check password
//    var passwordInput = document.getElementById("register-password");
//    passwordInput.addEventListener("blur", function () {
//        var inputPassword = this.value;
//        var passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=.]).{6,}$/;
//        var errorPassword = document.getElementById("error-message-password");
//        if (inputPassword) {
//            if (!passwordRegex.test(inputPassword)) {
//                errorPassword.textContent = "Mật khẩu không hợp lệ gồm trên 6 kí tự, chữ hoa, kí tự đặc biệt ...";
//                errorPassword.style.display = "block";
//                validatePass = false;
//            } else {
//                validatePass = true;
//                errorPassword.style.display = 'none';
//            }
//        }else {
//            errorPassword.style.display = 'none';
//        }
//    });
//    //end check password
//    // check confirmpassword
//    var passwordConfirmInput = document.getElementById("register-confirmpassword");
//    passwordConfirmInput.addEventListener("blur", function () {
//        var inputPassword = this.value;
//        var passwords = document.getElementById("register-password").value;
//        var errorPassword = document.getElementById("error-message-confirmpassword");
//        if (inputPassword) {
//            if (inputPassword !== passwords) {
//                errorPassword.textContent = "Mật khẩu xác nhận không khớp";
//                errorPassword.style.display = "block";
//                validateConfirm = false;
//            } else {
//                validateConfirm = true;
//                errorPassword.style.display = 'none';
//            }
//        } else {
//            errorPassword.style.display = 'none';
//        }
//    });
//    //end check confirmpassword
//    //check show pass
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
//    //end check show pass
//    // check phone
//    var phoneInput = document.getElementById("register-phone");
//    var errorPhone = document.getElementById("error-message-phone");
//    phoneInput.addEventListener("blur", function () {
//        var inputPhone = this.value;
//        if (inputPhone === "") {
//            validatePhone = true;
//            errorPhone.style.display = 'none';
//        }
//    });
//    phoneInput.addEventListener("input", function () {
//        var inputPhone = this.value;
//        var numberphone = inputPhone.replace(/\D/g, '');
//        this.value = numberphone;
//        var phoneRegex = /^[0-9]{10}$/;
//        if (numberphone.length !== 10 || !phoneRegex.test(numberphone)) {
//            errorPhone.textContent = "Số điện thoại phải là 10 số";
//            errorPhone.style.display = "block";
//            validatePhone = false;
//        } else {
//            validatePhone = true;
//            errorPhone.style.display = 'none';
//        }
//    });
//    //end check phone
//    // check name
//    var lastnameInput = document.getElementById("register-lastname");
//    var fristnameInput = document.getElementById("register-firstname");
//    var errorLastname = document.getElementById("error-message-lastname");
//    var errorFirstname = document.getElementById("error-message-firstname");
//    var textRegex = /^[a-zA-Z\s]+$/;
//    lastnameInput.addEventListener("input", function () {
//        var inputLastname = this.value;
//        var lastName = inputLastname.replace(/[^a-zA-Z\s]/g, '');
//        this.value = lastName;
//        if (!textRegex.test(lastName)) {
//            errorLastname.textContent = "Chỉ Được Nhập Chữ";
//            errorLastname.style.display = "block";
//        } else {
//            errorLastname.style.display = 'none';
//        }
//    });
//    fristnameInput.addEventListener("input", function () {
//        var inputFirstname = this.value;
//        var firstName = inputFirstname.replace(/[^a-zA-Z\s]/g, '');
//        this.value = firstName;
//        if (!textRegex.test(firstName)) {
//            errorFirstname.textContent = "Chỉ Được Nhập Chữ";
//            errorFirstname.style.display = "block";
//        } else {
//            errorFirstname.style.display = 'none';
//        }
//    });
//    //end check name
//    // load list option address
//    var citis = document.getElementById("register-city");
//    var districts = document.getElementById("register-district");
//    var wards = document.getElementById("register-ward");
//    var Parameter = {
//        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
//        method: "GET",
//        responseType: "application/json",
//    };
//    var promise = axios(Parameter);
//    promise.then(function (result) {
//        renderCity(result.data);
//    });
//    function renderCity(data) {
//        for (const x of data) {
//            citis.options[citis.options.length] = new Option(x.Name, x.Id);
//        }
//        citis.onchange = function () {
//            districts.length = 1;
//            wards.length = 1;
//            if (this.value != "") {
//                const result = data.filter(n => n.Id === this.value);
//                for (const k of result[0].Districts) {
//                    districts.options[districts.options.length] = new Option(k.Name, k.Id);
//                }
//            }
//        };
//        districts.onchange = function () {
//            wards.length = 1;
//            const dataCity = data.filter((n) => n.Id === citis.value);
//            if (this.value != "") {
//                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.value)[0].Wards;
//                for (const w of dataWards) {
//                    wards.options[wards.options.length] = new Option(w.Name, w.Id);
//                }
//            }
//        };
//    }
//    //end load list option address
//    //register
//    $("#registerForm").submit(function (e) {
//        if (!validateGmail || !validatePass || !validateConfirm || !validatePhone) {
//            e.preventDefault();
//        }
//        else {
//            e.preventDefault();
//            var locations = $("#register-location").val() + " , " + $("#register-ward option:selected").text() + " , "
//                + $("#register-district option:selected").text() + " , " + $("#register-city option:selected").text();
//            var obj = {
//                FirstName: $("#register-firstname").val(),
//                LastName: $("#register-lastname").val(),
//                Email: $("#register-email").val(),
//                Password: $("#register-password").val(),
//                ConfirmPassword: $("#register-confirmpassword").val(),
//                Birthday: $("#register-birthday").val(),
//                Gender: $("input[name='gender-option']:checked").val(),
//                Location: locations,
//                PhoneNumber: $("#register-phone").val(),
//                Image: "user.jpg",
//            }
//            //$.ajax({
//            //    url: "http://10.100.0.105:8088/Customers/SignUp",
//            //    type: "POST",
//            //    data: JSON.stringify(obj),
//            //    contentType: "application/json",
//            //    success: function () {
//            //        alert("Đăng kí thành công");
//            //        window.location.href = "/Customer/Login";
//            //    },
//            //    error: function () {
//            //        alert("Đăng kí thất bại , kiểm tra lại thông tin đăng kí.");
//            //    }
//            //});
//            $.ajax({
//                url: "https://localhost:7152/Customers/SignUp",
//                type: "POST",
//                data: JSON.stringify(obj),
//                // contentType: "application/json", // Loại bỏ dòng này
//                success: function () {
//                    alert("Đăng kí thành công");
//                    window.location.href = "/Customer/Login";
//                },
//                error: function () {
//                    alert("Đăng kí thất bại, vui lòng kiểm tra lại thông tin đăng kí.");
//                }
//            });
//        }
//    });
//});
$(document).ready(function() {
    let validateGmail = false;
    let validatePass = false;
    let validatePhone = true;
    let validateConfirm = false;

    // Check email
    var loginInput = document.getElementById("register-email");
    loginInput.addEventListener("blur", function() {
        var email = this.value;
        var gmailRegex = /^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$/;
        var errorEmail = document.getElementById("error-message-email");

        if (email) {
            if (gmailRegex.test(email)) {
                $.ajax({
                    url: "/Service/CheckGmail/?email=" + email,
                    type: "GET",
                    contentType: "application/json",
                    success: function(data) {
                        if (data === 0) {
                            errorEmail.textContent = "Email đã tồn tại";
                            errorEmail.style.display = "block";
                            validateGmail = false;
                        } else {
                            validateGmail = true;
                            errorEmail.style.display = 'none';
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

    // Check password
    var passwordInput = document.getElementById("register-password");
    passwordInput.addEventListener("blur", function() {
        var inputPassword = this.value;
        var passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=.]).{6,}$/;
        var errorPassword = document.getElementById("error-message-password");

        if (inputPassword) {
            if (!passwordRegex.test(inputPassword)) {
                errorPassword.textContent = "Mật khẩu không hợp lệ gồm trên 6 kí tự, chữ hoa, kí tự đặc biệt ...";
                errorPassword.style.display = "block";
                validatePass = false;
            } else {
                validatePass = true;
                errorPassword.style.display = 'none';
            }
        } else {
            errorPassword.style.display = 'none';
        }
    });

    // Check confirm password
    var passwordConfirmInput = document.getElementById("register-confirmpassword");
    passwordConfirmInput.addEventListener("blur", function() {
        var inputPassword = this.value;
        var passwords = document.getElementById("register-password").value;
        var errorPassword = document.getElementById("error-message-confirmpassword");

        if (inputPassword) {
            if (inputPassword !== passwords) {
                errorPassword.textContent = "Mật khẩu xác nhận không khớp";
                errorPassword.style.display = "block";
                validateConfirm = false;
            } else {
                validateConfirm = true;
                errorPassword.style.display = 'none';
            }
        } else {
            errorPassword.style.display = 'none';
        }
    });

    // Show password
    var checkShowPass = document.getElementById("register-showpass");
    checkShowPass.addEventListener("change", function() {
        var passwordInput = document.getElementById("register-password");
        var passwordConfirmInput = document.getElementById("register-confirmpassword");

        if (checkShowPass.checked) {
            passwordInput.type = "text";
            passwordConfirmInput.type = "text";
        } else {
            passwordInput.type = "password";
            passwordConfirmInput.type = "password";
        }
    });

    // Check phone
    var phoneInput = document.getElementById("register-phone");
    var errorPhone = document.getElementById("error-message-phone");
    phoneInput.addEventListener("blur", function() {
        var inputPhone = this.value;
        if (inputPhone === "") {
            validatePhone = true;
            errorPhone.style.display = 'none';
        }
    });
    phoneInput.addEventListener("input", function() {
        var inputPhone = this.value;
        var numberphone = inputPhone.replace(/\D/g, '');
        this.value = numberphone;

        var phoneRegex = /^[0-9]{10}$/;
        if (numberphone.length !== 10 || !phoneRegex.test(numberphone)) {
            errorPhone.textContent = "Số điện thoại phải là 10 số";
            errorPhone.style.display = "block";
            validatePhone = false;
        } else {
            validatePhone = true;
            errorPhone.style.display = 'none';
        }
    });

    // Load list option address
    var citis = document.getElementById("register-city");
    var districts = document.getElementById("register-district");
    var wards = document.getElementById("register-ward");
    var Parameter = {
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        method: "GET",
        responseType: "application/json",
    };
    var promise = axios(Parameter);
    promise.then(function(result) {
        renderCity(result.data);
    });

    function renderCity(data) {
        for (const x of data) {
            citis.options[citis.options.length] = new Option(x.Name, x.Id);
        }
        citis.onchange = function() {
            districts.length = 1;
            wards.length = 1;
            if (this.value != "") {
                const result = data.filter(n => n.Id === this.value);

                for (const k of result[0].Districts) {
                    districts.options[districts.options.length] = new Option(k.Name, k.Id);
                }
            }
        };
        districts.onchange = function() {
            wards.length = 1;
            const dataCity = data.filter((n) => n.Id === citis.value);
            if (this.value != "") {
                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.value)[0].Wards;

                for (const w of dataWards) {
                    wards.options[wards.options.length] = new Option(w.Name, w.Id);
                }
            }
        };
    }

    // Register form submit
    $("#registerForm").submit(function(e) {
        if (!validateGmail || !validatePass || !validateConfirm || !validatePhone) {
            e.preventDefault();
        } else {
            e.preventDefault();
            var locations = $("#register-location").val() + " , " + $("#register-ward option:selected").text() + " , "
                + $("#register-district option:selected").text() + " , " + $("#register-city option:selected").text();
            var obj = {
                FirstName: $("#register-firstname").val(),
                LastName: $("#register-lastname").val(),
                Email: $("#register-email").val(),
                Password: $("#register-password").val(),
                ConfirmPassword: $("#register-confirmpassword").val(),
                Birthday: $("#register-birthday").val(),
                Gender: $("input[name='gender-option']:checked").val(),
                Location: locations,
                PhoneNumber: $("#register-phone").val(),
                Image: "user.jpg",
            };

            $.ajax({
                url: "https://localhost:7152/Customers/SignUp",
                type: "POST",
                data: JSON.stringify(obj),
                success: function() {
                    alert("Đăng kí thành công");
                    window.location.href = "/Customer/Login";
                },
                error: function() {
                    alert("Đăng kí thất bại, vui lòng kiểm tra lại thông tin đăng kí.");
                }
            });
        }
    });
});
