var ForgetController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {

        $('#btnForget').on('click', function (e) {
            e.preventDefault();
            validateForgetInfo();
        });

    }

    function validateEmail(email) {

        var filterEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        var result = filterEmail.test(email);

        return result;
    }

    function validateForgetInfo() {
        var data = {
            Email: $('#txtEmail').val()
        };

        var isValid = true;

        if (!data.Email) {
            isValid = be.notify('Please enter your Email', '', 'error');
        }
        else {
            data.Email = String(data.Email).toLowerCase();
            if (!validateEmail(data.Email)) {
                isValid = be.notify('Your Email invalid', '', 'error');
            }
        }

        if (isValid) {

            $.ajax({
                type: 'POST',
                data: { model: data },
                dataType: 'json',
                beforeSend: function () {
                    be.startLoading();
                },
                url: '/Admin/Account/ForgotPassword',
                success: function (response) {

                    if (response.Success) {
                        be.notify('Successful resend request, please check your email.', '', 'success');
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