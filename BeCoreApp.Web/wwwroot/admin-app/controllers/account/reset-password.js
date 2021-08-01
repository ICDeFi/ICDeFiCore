var ResetPasswordController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {

        $('#btnResetPassword').on('click', function (e) {
            e.preventDefault();
            validateResetPasswordInfo();
        });
    }

    function validateEmail(email) {

        var filterEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        var result = filterEmail.test(email);

        return result;
    }

    function validateResetPasswordInfo() {
        var data = {
            Code: $('#hdCode').val(),
            Password: $('#txtPassword').val(),
            ConfirmPassword: $('#txtConfirmPassword').val(),
            Email: $('#txtEmail').val()
        };

        var isValid = true;
        if (!data.Code) {
            isValid = be.notify('Your code invalid', '', 'error');
        }

        if (!data.Password) {
            isValid = be.notify('Please enter your new password', '', 'error');
        }

        if (data.Password != data.ConfirmPassword) {
            isValid = be.notify('New password and confirm password not matched', '', 'error');
        }

        if (!data.Email) {
            isValid = be.notify('Please enter your Email', '', 'error');
        }
        else {
            data.Email = String(data.Email).toLowerCase();
            if (!validateEmail(data.Email)) {
                isValid = be.notify('Your Email invalid', '', 'error');
            }
        }
        debugger;

        if (isValid) {

            $.ajax({
                type: 'POST',
                data: { model: data },
                url: '/Admin/Account/ResetPassword',
                dataType: 'json',
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    debugger;

                    if (response.Success) {
                        window.location.href = '/Admin/Account/ResetPasswordConfirmation';
                    }
                    else {
                        be.notify(response.Message, '', 'error');
                    }

                    be.stopLoading();
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                },
            });

        }
    }
}