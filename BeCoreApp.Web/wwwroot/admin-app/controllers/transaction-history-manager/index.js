var TransactionHistoryManagerController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControl();
    }

    function registerControl() {
        $(".numberFormat").each(function () {

            var numberValue = parseFloat($(this).html().replace(/,/g, ''));
            if (!numberValue) {
                $(this).html(be.formatCurrency(0));
            }
            else {
                $(this).html(be.formatCurrency(numberValue));
            }
        });

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

        $('.numberFormat').on("keypress", function (e) {
            var keyCode = e.which ? e.which : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode == 46);
            if (ret)
                return true;
            else
                return false;
        });

        $(".numberFormat").focusout(function () {
            var numberValue = parseFloat($(this).val().replace(/,/g, ''));
            if (!numberValue) {
                $(this).val(be.formatCurrency(0));
            }
            else {
                $(this).val(be.formatCurrency(numberValue));
            }
        });

        $('body').on('click', '.btnApprove', function (e) {
            approveTransfer();
        });

        $('body').on('click', '.btnReject', function (e) {
            rejectTransfer();
        });

        $('body').on('click', '.btn-view', function (e) {
            loadDetail(e, $(this).data('id'))
        });
    }

    function approveTransfer() {
        $.ajax({
            type: "POST",
            url: "/Admin/TransactionHistoryManager/Approve",
            data: { Id: $('.hidId').val(), Note: $('.txtNote').val() },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $('#modal-view').modal('hide');

                if (response.Success) {
                    be.success('Approve Verify Transfer', response.Message, function () {
                        window.location.reload();
                    });
                }
                else {
                    be.error('Approve Verify Transfer', response.Message);
                }

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }

    function rejectTransfer() {
        $.ajax({
            type: "POST",
            url: "/Admin/TransactionHistoryManager/Reject",
            data: { Id: $('.hidId').val(), Note: $('.txtNote').val() },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $('#modal-view').modal('hide');

                if (response.Success) {
                    be.success('Approve Verify Transfer', response.Message, function () {
                        window.location.reload();
                    });
                }
                else {
                    be.error('Approve Verify Transfer', response.Message);
                }

                be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }

    function loadDetail(e, id) {
        e.preventDefault();
        debugger;
        $.ajax({
            type: "GET",
            url: "/Admin/TransactionHistoryManager/GetById",
            data: { id: id },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                debugger;
                $('.hidId').val(response.Id);
                $('.txtTransactionHash').val(response.TransactionHash);
                $('.txtAmount').val(be.formatCurrency(response.Amount));
                $('.boxImage').html('<img src="' + response.Image + '" width="auto" />')
                $('.txtNote').val(response.Note);
                $('.txtType').html(be.getTransactionType(response.Type));
                if (response.Type != 1) {
                    $('.btnApprove').hide();
                    $('.btnReject').hide();
                } else {
                    $('.btnApprove').show();
                    $('.btnReject').show();
                }

                be.stopLoading();

                $('#modal-view').modal('show');
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
            url: '/admin/transactionHistoryManager/GetAllPaging',
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
                        UserName: item.AppUserName,
                        Type: be.getTransactionType(item.Type),
                        TransactionHash: item.TransactionHash,
                        Amount: be.formatCurrency(item.Amount),
                        Note: item.Note,
                        Image: item.Image == null ? '<img src="/admin-side/images/user.png" width=200' : '<img src="' + item.Image + '" width=200 />',
                        CreatedDate: be.dateTimeFormatJson(item.CreatedDate),
                    });
                });

                $('#lbl-total-records').text(response.RowCount);

                $('#tbl-content').html(render);

                if (response.RowCount)
                    be.wrapPaging(response.RowCount, function () {
                        loadData();
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