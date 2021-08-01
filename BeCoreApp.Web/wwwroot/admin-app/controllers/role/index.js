var RoleController = function () {

    this.initialize = function () {
        loadData();
        registerEvents();
    }

    class AppRoleViewModel {
        constructor() {
            this.Id = $('#hidIdM').val();
            this.Name = $('#txtName').val();
            this.Description = $('#txtDescription').val();
        }

        Validate() {
            var isValid = true;
            if (!this.Name) {
                be.notify('Name is required!!!', "", 'error');
                isValid = false;
            }

            if (!this.Description) {
                be.notify('Description is required!!!', "", 'error');
                isValid = false;
            }
            return isValid;
        }
    }

    function registerEvents() {

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which == 13) {
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

        $('#btnSave').on('click', function (e) { saveRole(e) });

        $('body').on('click', '.btn-delete', function (e) { deleteRole(e, this) });
    };

    function deleteRole(e, element) {
        e.preventDefault();
        be.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Role/Delete",
                data: { id: $(element).data('id') },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Delete is success', "", 'success');
                    be.stopLoading();
                    loadData(true);
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function saveRole(e) {
        e.preventDefault();

        var roleVm = new AppRoleViewModel();
        if (roleVm.Validate()) {
            $.ajax({
                type: "POST",
                url: "/Admin/Role/SaveEntity",
                data: { roleVm },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Update is successs', "", 'success');

                    $('#modal-add-edit').modal('hide');

                    resetFormMaintainance();

                    be.stopLoading();

                    loadData(true);
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                },
            });
        }
    }

    function loadDetails(e, element) {
        e.preventDefault();
        $.ajax({
            type: "GET",
            url: "/Admin/Role/GetById",
            data: { id: $(element).data('id') },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtName').val(data.Name);
                $('#txtDescription').val(data.Description);
                $('#modal-add-edit').modal('show');
                be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    };

    function resetFormMaintainance() {
        $('#hidIdM').val('');
        $('#txtName').val('');
        $('#txtDescription').val('');
    };

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/role/GetAllPaging",
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
                        Name: item.Name,
                        Id: item.Id,
                        Status: be.getStatus(item.Status),
                        Description: item.Description
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
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    };
}