var MenuGroupController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControl()
    }

    class MenuGroupViewModel {
        constructor() {
            this.Id = +($('#hidId').val());
            this.Name = $('#txtName').val();
            this.RoleId = $('#RoleId').val();
            this.Status = $('#ckStatus').prop('checked') === true ? 1 : 0;
        }

        Validate() {
            var isValid = true;
            if (!this.Name)
                isValid = be.notify('Name is required!!!', "", 'error');

            if (!this.RoleId)
                isValid = be.notify('Role is required!!!', "", 'error');

            return isValid;
        }
    }

    function registerControl() {
        //$('#RoleId,#SearchRoleId').select2({
        //    placeholder: "Chọn vai trò",
        //    allowClear: true,
        //});
    }

    function registerEvents() {

        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData(true);
            }
        });

        $("#SearchRoleId").on('change', function (e) {
            e.preventDefault();
            loadData(true);
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

        $('#btnSave').on('click', function (e) { saveMenuGroup(e) });

        $('body').on('click', '.btn-delete', function (e) { deleteMenuGroup(e, this) });
    };


    function saveMenuGroup(e) {
        e.preventDefault();

        var menuGroupVm = new MenuGroupViewModel();
        if (menuGroupVm.Validate()) {
            $.ajax({
                type: "POST",
                url: "/Admin/MenuGroup/SaveEntity",
                data: { menuGroupVm },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    be.notify('Update menu group is success', '', 'success');

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
    };

    function deleteMenuGroup(e, element) {
        e.preventDefault();
        be.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/MenuGroup/Delete",
                data: { id: $(element).data('id') },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Delete menu group is success', "", 'success');
                    be.stopLoading();
                    loadData(true);
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    };

    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtName').val('');
        $('#ckStatus').prop('checked', true);
        $('#RoleId').val(null).trigger('change');
    };

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/MenuGroup/GetAllPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                roleId: $('#SearchRoleId').val(),
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
                        Role: item.RoleName,
                        Id: item.Id,
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
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    };

    function loadDetails(e, element) {
        e.preventDefault();
        $.ajax({
            type: "GET",
            url: "/Admin/MenuGroup/GetById",
            data: { id: $(element).data('id') },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $('#hidId').val(response.Id);
                $('#txtName').val(response.Name);
                $('#ckStatus').prop('checked', response.Status === 1);
                $('#RoleId').val(response.RoleId).trigger('change');
                $('#modal-add-edit').modal('show');

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    };
}