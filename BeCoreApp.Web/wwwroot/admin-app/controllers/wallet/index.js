var WalletController = function () {
    this.initialize = function () {
        loadWalletDeposits();
        registerEvents();
        registerControl();
    }
    function registerControl() {
        jQuery('#qrcodeTRXPublishKey').qrcode({
            text: $("#txtTRXPublishKey").val()
        });

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

    var registerEvents = function () {

        $('body').on('click', '#btnCopyTRXPublishKey', function (e) {
            copyTRXPublishKey();
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

        $('#btnWithdrawTRX').on('click', function (e) {
            e.preventDefault();
            withdrawTRX();
        });
    }

    function withdrawTRX() {

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
                url: "/Admin/Wallet/WithdrawTRX",
                dataType: "json",
                data: { modelJson: JSON.stringify(data) },
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {
                    if (response.Success) {
                        be.success('Withdraw TRX', response.Message, function () {
                            window.location.reload();
                        });
                    }
                    else {
                        be.error('Withdraw TRX', response.Message);
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

    function loadWalletDeposits() {
        $.ajax({
            type: 'GET',
            url: '/admin/Wallet/GetWalletDeposits',
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (response) {
                $('.walletTrxDeposit').html(be.formatCurrency(response.WalletTRX));
                $('#trxBalance').val(be.formatCurrency(response.WalletTRX));

                $('.walletDolpDeposit').html(be.formatCurrency(response.DOLPBalance));
                $('#dolpBalance').val(be.formatCurrency(response.DOLPBalance));
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
            }
        });
    }

    function copyTRXPublishKey() {
        var copyText = $("#txtTRXPublishKey");
        copyText.select();
        document.execCommand("copy");
    }
}