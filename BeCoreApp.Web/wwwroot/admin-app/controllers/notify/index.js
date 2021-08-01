var NotifyController = function () {
    this.initialize = function () {

        loadDetail();

        registerEvents();
        registerControls();
    }
    var dataNotify = null;
    function validateData() {

        dataNotify = {
            Id: +($('#hidId').val()),
            Name: $('#txtName').val(),
            Status: $('#ckStatus').prop('checked') === true ? 1 : 0,
            MildContent: CKEDITOR.instances.txtContent.getData()
        }

        var isValid = true;
        if (!dataNotify.Name)
            isValid = be.notify('Title is required!!!', "", 'error');

        if (!dataNotify.MildContent)
            isValid = be.notify('Content is required!!!', "", 'error');

        return isValid;
    }

    function registerControls() {
        CKEDITOR.replace('txtContent', {});


        $('#modal-add-edit').on('shown.bs.modal', function () {
            $(document).off('focusin.modal');
        });

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };
    }

    function registerEvents() {

        $('body').on('click', '#btnUpdate', function (e) { saveNotify(e) });
    }


    function saveNotify(e) {
        e.preventDefault();

        if (validateData()) {
            debugger;
            $.ajax({
                type: "POST",
                url: "/Admin/Notify/SaveEntity",
                data: { notifyVm: dataNotify },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Update Notification is success', "", 'success');
                    loadDetail();
                    be.stopLoading();
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        }
    }


    function loadDetail() {
        $.ajax({
            type: "GET",
            url: "/Admin/Notify/GetFirst",
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $('#hidId').val(response.Id);
                $('#txtName').val(response.Name);

                CKEDITOR.instances.txtContent.setData(response.MildContent);

                $('#ckStatus').prop('checked', response.Status == 1);

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }


}