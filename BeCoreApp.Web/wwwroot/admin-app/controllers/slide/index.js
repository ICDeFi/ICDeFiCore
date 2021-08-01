var SlideController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }

    var dataModel = null;
    function validateData() {

        dataModel = {
            Id: +($('#hidId').val()),
            Name: $('#txtName').val(),
            Image: $('#txtImage').val(),
            Url: $('#txtUrl').val(),
            Description: $('#txtDescription').val(),
            Status: $('#ckStatus').prop('checked') === true ? 1 : 0,
            HotFlag: $('#ckHot').prop('checked'),
        }
        debugger;

        var isValid = true;
        if (!dataModel.Name)
            isValid = be.notify('Tên không được bỏ trống!!!', "", 'error');

        if (!dataModel.Image)
            isValid = be.notify('Ảnh bìa không được bỏ trống!!!', "", 'error');

        if (!dataModel.Url)
            isValid = be.notify('Url không được bỏ trống!!!', "", 'error');

        if (!dataModel.Description)
            isValid = be.notify('Tóm tắt không được bỏ trống!!!', "", 'error');

        return isValid;
    }


    function registerEvents() {
        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData(true);
            }
        });

        $('body').on('change', "#ddl-show-page", function () {
            be.configs.pageSize = $(this).val();
            be.configs.pageIndex = 1;
            loadData(true);
        });

        $('body').on('click', "#btn-create", function (e) {
            e.preventDefault();
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });

        $('body').on('click', '#btnSelectImg', function () {
            $('#fileInputImage').click();
        });

        $("body").on('change', '#fileInputImage', function () {
            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }
            $.ajax({
                type: "POST",
                url: "/Admin/Upload/UploadImage",
                contentType: false,
                processData: false,
                data: data,
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (path) {
                    $('#txtImage').val(path);
                    be.stopLoading();
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });

        $('body').on('click', '.btn-edit', function (e) { loadDetails($(this).data('id')) });

        $('body').on('click', '#btnSave', function (e) { saveData(e) });

        $('body').on('click', '.btn-delete', function (e) { deleteData(e, this) });
    }


    function saveData(e) {
        e.preventDefault();

        if (validateData()) {
            $.ajax({
                type: "POST",
                url: "/Admin/slide/SaveEntity",
                data: { model: dataModel },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Cập nhật slide thành công', "", 'success');

                    $('#modal-add-edit').modal('hide');

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

    function deleteData(e, element) {
        e.preventDefault();
        be.confirm('Bạn chắc chắn muốn xóa?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/slide/Delete",
                data: { id: $(element).data('id') },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Xóa slide thành công', "", 'success');
                    be.stopLoading();
                    loadData();
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function loadDetails(id) {
        $.ajax({
            type: "GET",
            url: "/Admin/slide/GetById",
            data: { id: id },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {

                $('#hidId').val(response.Id);
                $('#txtName').val(response.Name);
                $('#txtImage').val(response.Image);
                $('#txtUrl').val(response.Url);
                $('#txtDescription').val(response.Description);
                $('#ckStatus').prop('checked', response.Status == 1);
                $('#ckHot').prop('checked', response.HotFlag);

                be.stopLoading();

                $('#modal-add-edit').modal('show');
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
        $('#txtImage').val('');
        $('#txtUrl').val('');
        $('#txtDescription').val('');
        $('#ckStatus').prop('checked', true);
        $('#ckHot').prop('checked', false);
    }

    function loadData(isPageChanged) {

        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: be.configs.pageIndex,
                pageSize: be.configs.pageSize
            },
            url: '/admin/slide/GetAllPaging',
            dataType: 'json',
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";

                $.each(response.Results, function (i, item) {

                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Description: item.Description,
                        Url: item.Url,
                        Image: item.Image == null ? '<img src="/admin-side/images/user.png" width=100' : '<img src="' + item.Image + '" width=100 />',
                        HotFlag: be.getStatus(item.HotFlag),
                        Status: be.getStatus(item.Status)
                    });
                });

                $('#lbl-total-records').text(response.RowCount);

                $('#tbl-content').html(render);

                if (response.RowCount)
                    be.wrapPaging(response.RowCount, function () {
                        loadData()
                    }, isPageChanged);

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }
}