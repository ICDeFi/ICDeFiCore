var TransactionHistoryController = function () {
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

        $('body').on('click', '#btnSelectImg', function () {
            $('#fileInputImage').click();
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

        $('body').on('click', "#btn-create", function (e) {
            e.preventDefault();
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });

        $('body').on('click', '#btnSave', function (e) {
            validateVerifyTransfer();
        });

        $('body').on('click', '.btn-view', function (e) {
            loadDetail(e, $(this).data('id'))
        });
    }

    function validateVerifyTransfer() {
        var data = {
            TransactionHash: $('#txtTransactionHash').val(),
            Amount: parseFloat($('#txtAmount').val().replace(/,/g, '')),
            Image: $('#txtImage').val(),
        };

        var isValid = true;
        if (!data.TransactionHash) {
            isValid = be.notify('Transaction Hash is required', '', 'error');
        }

        if (data.Amount <= 0) {
            isValid = be.notify('Amount is required!', '', 'error');
        }

        if (data.Image <= 0) {
            isValid = be.notify('Image is required!', '', 'error');
        }

        if (isValid) {

            $.ajax({
                type: "POST",
                url: "/Admin/TransactionHistory/SaveVerify",
                data: { modelJson: JSON.stringify(data) },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    $('#modal-add-edit').modal('hide');

                    if (response.Success) {
                        be.success('Create Verify Transfer', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Create Verify Transfer', response.Message);
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

    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtTransactionHash').val('');
        $('#txtAmount').val(be.formatCurrency(0));
        $('#txtImage').val('');
    }

    function loadDetail(e, id) {
        e.preventDefault();
        debugger;
        $.ajax({
            type: "GET",
            url: "/Admin/TransactionHistory/GetById",
            data: { id: id },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                debugger;
                $('.txtTransactionHash').val(response.TransactionHash);
                $('.txtAmount').val(be.formatCurrency(response.Amount));
                $('.boxImage').html('<img src="' + response.Image + '" width="auto" />')
                $('.txtNote').html(response.Note);
                $('.txtType').html(be.getTransactionType(response.Type));
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
            url: '/admin/transactionHistory/GetAllPaging',
            dataType: 'json',
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id:item.Id,
                        UserName: item.AppUserName,
                        Type: be.getTransactionType(item.Type),
                        TransactionHash: item.TransactionHash,
                        Amount: be.formatCurrency(item.Amount),
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