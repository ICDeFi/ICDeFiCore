var SupportManageController = function () {
    this.initialize = function () {

        be.startLoading();
        loadData();
        be.stopLoading();

        registerEvents();
        registerControls();
    }
    var dataSupport = null;
    function validateData() {

        dataSupport = {
            Id: +($('#hidId').val()),
            ResponseContent: $('#txtResponseContent').val()
        }

        var isValid = true;

        if (!dataSupport.ResponseContent)
            isValid = be.notify('Anwser is required!!!', "", 'error');

        return isValid;
    }

    function registerControls() {
        $('#modal-edit').on('shown.bs.modal', function () {
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

        $('body').on('click', '.btn-edit', function (e) { loadDetails(e, $(this).data('id')) });

        $('body').on('click', '#btnSave', function (e) { saveSupport(e) });
    }


    function saveSupport(e) {
        e.preventDefault();

        if (validateData()) {
            debugger;
            $.ajax({
                type: "POST",
                url: "/Admin/SupportManage/SaveEntity",
                data: { supportVm: dataSupport },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Send support is success.', "", 'success');
                    $('#modal-edit').modal('hide');
                    resetFormMaintainance();
                    be.stopLoading();
                    loadData(true);
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        }
    }

    function loadDetails(e, id) {
        e.preventDefault();

        $.ajax({
            type: "GET",
            url: "/Admin/SupportManage/GetById",
            data: { id: id },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $('#hidId').val(response.Id);
                $('#txtName').val(response.Name);
                $('#txtRequestContent').val(response.RequestContent);
                $('#txtResponseContent').val(response.ResponseContent);
                $('#badgeType').html(be.getSupportType(response.Type));
                be.stopLoading();
                $('#modal-edit').modal('show');
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }


    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtName').val('');
        $('#txtRequestContent').val('');
        $('#txtResponseContent').val('');
    }
    function loadData(isPageChanged) {

        $.ajax({
            type: 'GET',
            data: {
                page: be.configs.pageIndex,
                pageSize: be.configs.pageSize
            },
            url: '/admin/SupportManage/GetAllPaging',
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                $.each(response.Results, function (i, item) {

                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        UserName: item.AppUserName,
                        RequestContent: item.RequestContent,
                        ResponseContent: item.ResponseContent,
                        Type: be.getSupportType(item.Type)
                    });
                });

                $('#lbl-total-records').text(response.RowCount);

                $('#tbl-content').html(render);

                if (response.RowCount)
                    be.wrapPaging(response.RowCount, function () { loadData() }, isPageChanged);
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
            }
        });
    }
}