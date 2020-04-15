
//Check email existed
$(document).ready(function () {

    $("#email").blur(function () {
        $.ajax({
            type: "POST",
            url: "Login/CheckExitsEmail",
            data: { email: $(this).val() },
            dataType: "json",
            success: function (res) {
                if (res.status == true) {
                    $("#email_error").text("Email đã được sử dụng!!");
                    $("#submitRegister").attr("disabled");
                } else {
                    $("#submitRegister").removeAttr("disabled");
                    $("#email_error").text("");
                }
            }
        });
    });

    //validate form login
    $('#frmLogin').validate({
        rules: {
            loginPass: "required",
            loginEmail: {
                required: true,
                email: true
            }
        },

        messages: {
            loginPass: "Vui lòng nhập mật khẩu",
            loginEmail: {
                required: "Vui lòng nhập email",
                email: "Email không hợp lệ"
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });


    //validate form register
    $('#registerUser').validate({
        rules: {
            firstName: "required",
            email: {
                required: true,
                email: true
            },
            passWord: {
                required: true,
                minlength: 6,
                maxlength: 32
            },
            birthday: "required"
        },

        messages: {
            firstName: "Vui lòng nhập họ và tên",
            email: {
                required: "Vui lòng nhập email",
                email: "Email không hợp lệ"
            },
            passWord: {
                required: "Vui lòng nhập mật khẩu",
                minlength: "Mật khẩu phải từ 6 đến 32 ký tự",
                maxlenght: "Mật khẩu phải từ 6 đến 32 ký tự"
            },
            birthday: "Vui lòng nhập ngày sinh"
        },

        submitHandler: function (form) {
            form.submit();
        }
    });

    //validate form change pass
    $('#frmChangePass').validate({
        rules: {
            change_pass: {
                required: true,
                minlength: 6,
                maxlength: 32
            },
            Re_change_pass: {
                required: true,
                equalTo: "#change_pass"
            }
        },

        messages: {
            change_pass: {
                required: "Vui lòng nhập mật khẩu",
                minlength: "Mật khẩu phải từ 6 đến 32 ký tự",
                maxlength: "Mật khẩu phải từ 6 đến 32 ký tự",
            },
            Re_change_pass: {
                required: "Vui lòng nhập lại mật khẩu",
                equalTo: 'Mật khẩu nhập lại không trùng khớp'
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });

});


var ans = {
    init: function () {
        ans.regEvents();
    },
    regEvents: function () {

        //đăng nhập
        $("#UserLogin").off('click').on('click', function () {
            $.ajax({
                type: "POST",
                url: "Login/CheckLogin",
                data: {
                    email: $("#loginEmail").val(),
                    passWord: $("#loginPass").val()
                },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        PNotify.success({
                            title: 'THÔNG BÁO!!',
                            text: 'Đăng nhập thành công.'
                        });
                        window.location.href = "/";
                    } else {
                        PNotify.error({
                            title: 'ĐĂNG NHẬP KHÔNG THÀNH CÔNG!',
                            text: 'Email và mật khẩu không tồn tại hoặc chưa đăng ký.'
                        });
                    }
                }
            });
        });

        //Gửi mail thay đổi mk
        $("#btnSendMail").off('click').on('click', function () {
            $.ajax({
                type: "POST",
                url: "Login/ForgetPass",
                data: {
                    email: $("#confirmEmail").val()
                },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        PNotify.notice({
                            title: 'THÔNG BÁO!!',
                            text: 'Gửi e-mail thành công. Vui lòng truy cập đế tiến hành xác nhận tài khoản.'
                        });
                        window.location.href = "/";
                    }
                }
            });
        });

        // thay đổi mk
        $("#btnChangePass").off('click').on('click', function () {
            $.ajax({
                type: "POST",
                url: "Login/ChangePass",
                data: {
                    email: $("#ChangeEmail").val(),
                    pass: $("#change_pass").val()
                },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        PNotify.success({
                            title: 'THÔNG BÁO!!',
                            text: 'Bạn đã thay đổi mật khẩu thành công.'
                        });
                        window.location.href = "/";
                    }
                }
            });
        });

    }
}

ans.init();