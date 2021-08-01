var UserController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControl();
    }

    var dataModel = null;
    function validateData() {
        dataModel = {
            Id: $('#hidId').val(),
            RoleId: $('#RoleId').val(),
            Status: $('#ckStatus').prop('checked') === true ? 1 : 0,
            FullName: $('#txtFullName').val(),
            UserName: $('#txtUserName').val(),
            Password: $('#txtPassword').val(),
            ConfirmPassword: $('#txtConfirmPassword').val(),
            Email: $('#txtEmail').val(),
            PhoneNumber: $('#txtPhoneNumber').val(),
            Roles: []
        }
        debugger;
        var isValid = true;
        if (!dataModel.FullName)
            isValid = be.notify('FullName is required!!!', "", 'error');

        if (!dataModel.UserName)
            isValid = be.notify('UserName is required!!!', "", 'error');

        if (!dataModel.Email)
            isValid = be.notify('Email is required!!!', "", 'error');

        if (!dataModel.Password)
            isValid = be.notify('Password is required!!!', "", 'error');

        if (dataModel.ConfirmPassword != dataModel.Password)
            isValid = be.notify('Passwords and password confirmation are not the same!!!', "", 'error');

        if (!dataModel.RoleId) {
            isValid = be.notify('Role is required!!!', "", 'error');
        }
        else {
            dataModel.Roles.push(dataModel.RoleId);
        }
        debugger;
        return isValid;
    }

    function registerControl() {
        $('#RoleId,#SearchRoleId').select2({
            placeholder: "Choose role",
            allowClear: true,
        });
    }

    function registerEvents() {

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData(true);
            }
        });

        $("#ddl-show-page").on('change', function () {
            be.configs.pageSize = $(this).val();
            be.configs.pageIndex = 1;
            loadData(true);
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) { loadDetails(e, this) });

        $('#btnSave').on('click', function (e) { saveData(e) });

        $('body').on('click', '.btn-delete', function (e) { deleteUser(e, this) });
    };

    function saveData(e) {
        e.preventDefault();
        if (validateData()) {
            $.ajax({
                type: "POST",
                url: "/Admin/User/SaveEntity",
                data: { userVm: dataModel },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    debugger;
                    be.notify('Update member is success', '', 'success');

                    $('#modal-add-edit').modal('hide');

                    resetFormMaintainance();

                    be.stopLoading();

                    loadData(true);
                },
                error: function (message) {
                    be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        }
    }

    function deleteUser(e, element) {
        e.preventDefault();
        be.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/User/Delete",
                data: { id: $(element).data('id') },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Delete member is success', '', 'success');
                    be.stopLoading();
                    loadData(true);
                },
                error: function (message) {
                    be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function disableFieldEdit(disabled) {
        $('#txtUserName').prop('disabled', disabled);
        $('#txtPassword').prop('disabled', disabled);
        $('#txtConfirmPassword').prop('disabled', disabled);
    }

    function resetFormMaintainance() {
        disableFieldEdit(false);
        $('#hidId').val('');
        $('#RoleId').val(null).trigger('change');
        $('#txtFullName').val('');
        $('#txtUserName').val('');
        $('#txtPassword').val('');
        $('#txtConfirmPassword').val('');
        $('#txtEmail').val('');
        $('#txtPhoneNumber').val('');
        $('#ckStatus').prop('checked', true);
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/user/GetAllPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: be.configs.pageIndex,
                pageSize: be.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {

                var template = $('#table-template').html();
                var render = "";

                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        FullName: item.FullName,
                        Id: item.Id,
                        UserName: item.UserName,
                        DateCreated: be.dateTimeFormatJson(item.DateCreated),
                        Status: be.getStatus(item.Status)
                    });
                });

                $("#lbl-total-records").text(response.RowCount);

                $('#tbl-content').html(render);

                if (response.RowCount) {
                    be.wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);
                }

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    };

    function loadDetails(e, element) {
        e.preventDefault();
        $.ajax({
            type: "GET",
            url: "/Admin/User/GetById",
            data: { id: $(element).data('id') },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $('#hidId').val(response.Id);
                $('#txtFullName').val(response.FullName);
                $('#txtUserName').val(response.UserName);
                $('#txtEmail').val(response.Email);
                $('#txtPhoneNumber').val(response.PhoneNumber);
                $('#ckStatus').prop('checked', response.Status === 1);
                $('#txtPassword').val(response.Password);
                $('#txtConfirmPassword').val(response.Password);
                disableFieldEdit(true);
                $('#RoleId').val(response.Roles[0]).trigger('change');
                $('#modal-add-edit').modal('show');

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    };
}