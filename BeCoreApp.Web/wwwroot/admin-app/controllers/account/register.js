var registerController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {

        $('#btnRegister').on('click', function (e) {
            e.preventDefault();
            validateRegisterInfo();
        });
    }

    function validatePhone(phone) {

        var filterPhone = /^[0-9-+]+$/;

        var result = filterPhone.test(phone);

        return result;
    }

    function validateEmail(email) {

        var filterEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        var result = filterEmail.test(email);

        return result;
    }

    function validateRegisterInfo() {
        var data = {
            UserName: $('#txtUserName').val(),
            ReferralId: $('#txtReferralId').val(),
            FullName: $('#txtFullName').val(),
            Password: $('#txtPassword').val(),
            ConfirmPassword: $('#txtConfirmPassword').val(),
            Email: $('#txtEmail').val(),
            PhoneNumber: $('#txtPhoneNumber').val(),
            Agree: $('#cbAgree').prop('checked'),
        };

        var isValid = true;

        if (!data.FullName) {
            isValid = be.notify('FullName is required!!!', '', 'error');
        }

        if (!data.ReferralId) {
            isValid = be.notify('Referral is required!!!', '', 'error');
        }

        if (!data.UserName) {
            isValid = be.notify('UserName is required!!!', '', 'error');
        }

        if (!data.Password) {
            isValid = be.notify('Password is required!!!', '', 'error');
        }

        if (data.Password != data.ConfirmPassword) {
            isValid = be.notify('Password did not match!!!', '', 'error');
        }

        if (!data.PhoneNumber) {
            isValid = be.notify('Phone Number is required!!!', '', 'error');
        }
        else {
            if (!validatePhone(data.PhoneNumber)) {
                isValid = be.notify('Phone Number incorrect format!!!', '', 'error');
            }
        }

        if (!data.Email) {
            isValid = be.notify('Email is required!!!', '', 'error');
        }
        else {
            data.Email = String(data.Email).toLowerCase();
            if (!validateEmail(data.Email)) {
                isValid = be.notify('Email incorrect format!!!', '', 'error');
            }
        }

        if (!data.Agree) {
            isValid = be.notify('Please confirm "I agree to the terms and conditions"', '', 'error');
        }

        if (isValid) {

            $.ajax({
                type: 'POST',
                data: { registerVm: data },
                url: '/admin/account/register',
                dataType: 'json',
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    if (response.Success) {
                        be.success('Register is success', response.Message, function () {
                            window.location.href = '/admin/account/login';
                        });
                    }
                    else {
                        be.error('Register is failed', response.Message);
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