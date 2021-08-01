var FeedbackController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
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

        $('body').on('click', '#btnCancel', function (e) {
            loadData();
        });
        $('body').on('click', '.btn-edit', function (e) {
            loadDetails($(this).data('id'))
        });
        $('body').on('click', '.btn-change-type', function (e) {
            changeType(e, this)
        });
        $('body').on('click', '.btn-delete', function (e) {
            deleteData(e, this)
        });
    }

    function changeType(e, element) {
        e.preventDefault();
        be.confirm('Request status update?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/feedback/ChangeType",
                data: { id: $(element).data('id') },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Request status update successful', "", 'success');
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

    function deleteData(e, element) {
        e.preventDefault();
        be.confirm('You definitely want to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/feedback/Delete",
                data: { id: $(element).data('id') },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Delete request successfully', "", 'success');
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
            url: "/Admin/feedback/GetById",
            data: { id: id },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {

                $('#txtTitle').val(response.Title);
                $('#txtFullName').val(response.FullName);
                $('#txtPhone').val(response.Phone);
                $('#txtEmail').val(response.Email);
                $('#txtMessage').val(response.Message);

                be.stopLoading();

                $('#modal-add-edit').modal('show');
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }


    function loadData(isPageChanged) {

        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: be.configs.pageIndex,
                pageSize: be.configs.pageSize
            },
            url: '/admin/feedback/GetAllPaging',
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
                        Title: item.Title,
                        FullName: item.FullName,
                        Phone: item.Phone,
                        Type: be.getType(item.Type),
                        Email: item.Email,
                        ModifiedBy: item.ModifiedBy,
                        ModifiedDate: be.dateTimeFormatJson(item.DateModified),
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