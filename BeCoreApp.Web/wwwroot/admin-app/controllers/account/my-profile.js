var MyProfileController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {

        $('#btnMyProfile').on('click', function (e) {
            e.preventDefault();
            validateMyProfileInfo();
        });
    }

    function validatePhone(phone) {

        var filterPhone = /^[0-9-+]+$/;

        var result = filterPhone.test(phone);

        return result;
    }


    function validateMyProfileInfo() {
        var data = {
            FullName: $('#txtFullName').val(),
            PhoneNumber: $('#txtPhoneNumber').val(),
        };

        var isValid = true;

        if (!data.FullName) {
            isValid = be.notify('FullName is required', '', 'error');
        }

        if (!data.PhoneNumber) {
            isValid = be.notify('Phone Number is required', '', 'error');
        }
        else {
            if (!validatePhone(data.PhoneNumber)) {
                isValid = be.notify('Invalid Phone Number', '', 'error');
            }
        }

        if (isValid) {

            $.ajax({
                type: 'POST',
                data: { profileVm: data },
                url: '/admin/profile/index',
                dataType: 'json',
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    if (response.Success) {
                        be.success('Update profile', response.Message);
                    }
                    else {
                        be.error('Update profile', response.Message);
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