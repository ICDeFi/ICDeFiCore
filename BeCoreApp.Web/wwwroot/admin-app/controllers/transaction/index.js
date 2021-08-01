var TransactionController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControl();
    }

    function registerControl() {

        $(".numberFormat").each(function () {
            var numberValue = parseFloat($(this).val().replace(/,/g, ''));
            if (!numberValue) {
                $(this).val(be.formatCurrency(0));
            }
            else {
                $(this).val(be.formatCurrency(numberValue));
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

        $('.ddlPlatform').on("change", function () {
            var platformId = $(this).val();
            displayUXByPlatform(platformId);
        });

        $('#btnWithdraw').on('click', function (e) {
            e.preventDefault();
            withdraw();
        });
    }

    function withdraw() {

        var data = {
            AddressReceiving: $('#txtAddressReceiving').val(),
            Amount: parseFloat($('#txtAmount').val().replace(/,/g, '')),
        };

        var isValid = true;

        if (data.Amount <= 0) {
            isValid = be.notify('Amount is required.', '', 'error');
        }

        if (!data.AddressReceiving) {
            isValid = be.notify('Address receiving is required.', '', 'error');
        }

        if (isValid) {
            $.ajax({
                type: "POST",
                url: "/Admin/Transaction/Withdraw",
                dataType: "json",
                data: { modelJson: JSON.stringify(data) },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    if (response.Success) {
                        be.success('Withdraw TickTok (TT)', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Withdraw TickTok (TT)', response.Message);
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

    function swap() {

        var data = {
            Amount: parseFloat($('#txtAmount').val().replace(/,/g, '')),
        };

        var isValid = true;

        if (data.Amount <= 0) {
            isValid = be.notify('Amount is required.', '', 'error');
        }

        if (isValid) {
            $.ajax({
                type: "POST",
                url: "/Admin/Transaction/Swap",
                dataType: "json",
                data: { modelJson: JSON.stringify(data) },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    if (response.Success) {
                        be.success('Swap To TickTok (TT)', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Swap To TickTok (TT)', response.Message);
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

    function buy() {

        var data = {
            Amount: parseFloat($('#txtAmount').val().replace(/,/g, '')),
        };

        var isValid = true;

        if (data.Amount <= 0) {
            isValid = be.notify('Amount is required.', '', 'error');
        }

        if (isValid) {
            $.ajax({
                type: "POST",
                url: "/Admin/Transaction/Buy",
                dataType: "json",
                data: { modelJson: JSON.stringify(data) },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    if (response.Success) {
                        be.success('Buy TickTok (TT)', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Buy TickTok (TT)', response.Message);
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

    function getTRXPaymentAmount(element) {
        debugger;

        var data = {
            Amount: parseFloat(element.replace(/,/g, '')),
        };

        if (data.Amount) {
            $.ajax({
                type: "POST",
                url: "/Admin/Transaction/GetTRXPaymentAmount",
                dataType: "json",
                data: { modelJson: JSON.stringify(data) },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    debugger;
                    if (response.Success) {
                        $("#txtTTReceiving").val(be.formatCurrency(response.Data));
                    }
                    else {
                        be.notify(response.Message, "", 'error');
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

    function displayUXByPlatform(platformId) {
        $.ajax({
            type: "GET",
            url: "/Admin/transaction/DisplayUXByPlatform",
            dataType: "html",
            data: { platformId: platformId },
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                $(".box-platform").html(response);

                $('#btnWithdraw').on('click', function (e) {
                    e.preventDefault();
                    withdraw();
                });

                $('#btnSwap').on('click', function (e) {
                    e.preventDefault();
                    swap();
                });

                $('#btnBuy').on('click', function (e) {
                    e.preventDefault();
                    buy();
                });

                $('.txtTRXAmount').on("keyup", function (e) {
                    getTRXPaymentAmount($(this).val());
                });

                $(".numberFormat").each(function () {
                    var numberValue = parseFloat($(this).val().replace(/,/g, ''));
                    if (!numberValue) {
                        $(this).val(be.formatCurrency(0));
                    }
                    else {
                        $(this).val(be.formatCurrency(numberValue));
                    }
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

                be.stopLoading();
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
            url: '/admin/transaction/GetAllPaging',
            dataType: 'json',
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        UserName: item.AppUserName,
                        Type: item.TypeName,
                        Transaction: item.TransactionHas,
                        AddressReceiving: item.AddressTo,
                        Amount: be.formatCurrency(item.Amount),
                        DateCreated: be.dateTimeFormatJson(item.DateCreated),
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