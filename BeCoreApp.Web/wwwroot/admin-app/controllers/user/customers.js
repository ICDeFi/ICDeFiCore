var CustomerController = function () {
    this.initialize = function () {
        be.startLoading();
        loadData();
        be.stopLoading();
        registerEvents();
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

        $('body').on('click', '.btn-delete', function (e) {
            deleteCustomer(e, this);
        });

        $('body').on('click', '#btnRunLuckyDay', function (e) {
            runLuckyDay(e);
        });

        $('body').on('click', '#btnRunLucky5Day', function (e) {
            runLucky5Day(e);
        });
    };

    function runLuckyDay(e) {

        e.preventDefault();

        be.confirm('Bạn muốn quét mỗi ngày?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/User/RunLuckyDay",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    be.stopLoading();

                    if (response.Success) {
                        be.success('Quét Mỗi Ngày', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Quét Mỗi Ngày', response.Message, function () {
                            window.location.reload();
                        });
                    }
                },
                error: function (message) {
                    be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function runLucky5Day(e) {

        e.preventDefault();

        be.confirm('Bạn muốn quét 5 ngày?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/User/RunLucky5Day",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    be.stopLoading();

                    if (response.Success) {
                        be.success('Quét 5 Ngày', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Quét 5 Ngày', response.Message, function () {
                            window.location.reload();
                        });
                    }
                },
                error: function (message) {
                    be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function deleteCustomer(e, element) {
        e.preventDefault();
        be.confirm('You want to delete this member?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/User/DeleteCustomer",
                data: { id: $(element).data('id') },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    debugger;
                    if (response.Success) {
                        be.notify(response.Message, '', 'success');

                        loadData(true);
                    }
                    else {
                        be.notify(response.Message, '', 'error');
                    }

                    be.stopLoading();
                },
                error: function (message) {
                    be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/user/GetAllCustomerPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: be.configs.pageIndex,
                pageSize: be.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                //be.startLoading();
            },
            success: function (response) {

                var template = $('#table-template').html();
                var render = "";

                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        FullName: item.FullName,
                        UserName: item.UserName,
                        Id: item.Id,
                        Email: item.Email,
                        PhoneNumber: item.PhoneNumber,
                        EmailConfirmed: be.getEmailConfirmed(item.EmailConfirmed),
                    });
                });

                $("#lbl-total-records").text(response.RowCount);

                $('#tbl-content').html(render);

                if (response.RowCount) {
                    be.wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);
                }

                //be.stopLoading();
            },
            error: function (message) {
                be.notify(`jqXHR.responseText: ${message.responseText}`, `Status code: ${message.status}`, 'error');
                //be.stopLoading();
            }
        });
    }
}