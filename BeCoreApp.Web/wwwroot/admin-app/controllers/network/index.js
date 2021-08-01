var NetworkController = function () {
    this.initialize = function () {
        be.startLoading();
        loadData();
        be.stopLoading();
        registerEvents();
    }
    var refIndex = 1;
    function registerEvents() {

        $('.txt-search-keyword-1,.txt-search-keyword-2,.txt-search-keyword-3').keypress(function (e) {
            if (e.which === 13) {
                debugger;
                e.preventDefault();
                be.configs.pageSize = $("#ddl-show-page").val();
                be.configs.pageIndex = 1;
                loadData(true);
            }
        });
        $('.nav-link-network').click(function () {
            var indexNumber = $(this).attr('data-id');
            debugger;
            if (refIndex != indexNumber) {
                be.configs.pageSize = $("#ddl-show-page").val();
                be.configs.pageIndex = 1;
                refIndex = indexNumber;
                loadData(true);
            }
        });
        $("#ddl-show-page").on('change', function () {
            be.configs.pageSize = $(this).val();
            be.configs.pageIndex = 1;
            loadData(true);
        });

        document.getElementById("btnCopyReferlink").addEventListener("click", function () {
            copyToClipboard(document.getElementById("txtReferlink"));
        });
    };

    function copyToClipboard(elem) {
        // create hidden text element, if it doesn't already exist
        var targetId = "_hiddenCopyText_";
        var isInput = elem.tagName === "INPUT" || elem.tagName === "TEXTAREA";
        var origSelectionStart, origSelectionEnd;
        if (isInput) {
            // can just use the original source element for the selection and copy
            target = elem;
            origSelectionStart = elem.selectionStart;
            origSelectionEnd = elem.selectionEnd;
        } else {
            // must use a temporary form element for the selection and copy
            target = document.getElementById(targetId);
            if (!target) {
                var target = document.createElement("textarea");
                target.style.position = "absolute";
                target.style.left = "-9999px";
                target.style.top = "0";
                target.id = targetId;
                document.body.appendChild(target);
            }
            target.textContent = elem.textContent;
        }
        // select the content
        var currentFocus = document.activeElement;
        target.focus();
        target.setSelectionRange(0, target.value.length);

        // copy the selection
        var succeed;
        try {
            succeed = document.execCommand("copy");
        } catch (e) {
            succeed = false;
        }
        // restore original focus
        if (currentFocus && typeof currentFocus.focus === "function") {
            currentFocus.focus();
        }

        if (isInput) {
            // restore prior selection
            elem.setSelectionRange(origSelectionStart, origSelectionEnd);
        } else {
            // clear temporary content
            target.textContent = "";
        }
        return succeed;
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/network/GetAllPaging",
            data: {
                refIndex: refIndex,
                keyword: $('.txt-search-keyword-' + refIndex).val(),
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
                        Id: item.Id,
                        Email: item.Email,
                        PhoneNumber: item.PhoneNumber,
                        DateCreated: be.dateTimeFormatJson(item.DateCreated),
                        EmailConfirmed: be.getEmailConfirmed(item.EmailConfirmed)
                    });
                });
                $("#lbl-total-records").text(response.RowCount);

                $('#tbl-content-' + refIndex).html(render);

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
    };
}