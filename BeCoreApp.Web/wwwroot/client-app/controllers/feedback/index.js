
var FeedbackController = function () {
    this.initialize = function () {
        registerEvents();
    }
    var dataModel = null;

    function validateData() {
        dataModel = {
            FullName: $('#txtFullName').val(),
            Title: $('#txtTitle').val(),
            Email: $('#txtEmail').val(),
            Phone: $('#txtPhone').val(),
            Message: $('#txtMessage').val(),
        }

        var isvalid = true;
        if (!dataModel.FullName)
            isvalid = be.notify('Your Name is required!!!', '', 'error');

        if (!dataModel.Title)
            isvalid = be.notify('Your Subject is required!!!', '', 'error');

        if (!dataModel.Email) {
            isvalid = be.notify('Your Email is required!!!', '', 'error');
        }
        else {
            dataModel.Email = String(dataModel.Email).toLowerCase();
            if (!validateEmail(dataModel.Email)) {
                isvalid = be.notify('Your Email is not properly formatted Email!!!', '', 'error');
            }
        }

        if (!dataModel.Phone) {
            isvalid = be.notify('Your Phone is required!!!', '', 'error');
        }
        else {
            if (!validatePhone(dataModel.Phone)) {
                isvalid = be.notify('Your Phone is not properly formatted!!!', '', 'error');
            }
        }

        if (!dataModel.Message)
            isvalid = be.notify('Your Message is required!!!', "", 'error');

        return isvalid;
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

    function registerEvents() {
        $('body').on('click', '#btnSendFeedback', function (e) {
            saveData(e);
        });
    }

    function resetFormMaintainance() {
        $('#txtFullName').val('');
        $('#txtTitle').val('');
        $('#txtEmail').val('');
        $('#txtPhone').val('');
        $('#txtMessage').val('');
    }

    function saveData(e) {
        e.preventDefault();

        if (validateData()) {
            $.ajax({
                type: "POST",
                url: "/Contact/SaveEntity",
                data: { model: dataModel },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    debugger;
                    if (response) {
                        be.success("Submit request successfully", "We will get back to you as soon as possible.")
                        resetFormMaintainance();
                    }
                    else {
                        be.error("Request failed", "Please review the information entered")
                    }

                    be.stopLoading();
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        }
    }
}