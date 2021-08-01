var CustomerTreeController = function () {
    this.initialize = function () {
        loadData();
    }

    function loadData(reload) {
        $.ajax({
            url: '/Admin/User/GetTreeAll',
            dataType: 'json',
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                if (reload) {
                    $('div#jstree').jstree(true).settings.core.data = response;
                    $('div#jstree').jstree(true).refresh();
                }
                else {
                    fillData(response);
                }

                be.stopLoading();

            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }

    var fillData = function (response) {
        $("div#jstree").jstree({
            plugins: ["table", "state", "types"],
            core: {
                themes: { responsive: false },
                check_callback: true,
                data: response
            },
            table: {
                columns: [
                    {
                        header: "Thành Viên",
                        format: function (v) {
                            if (v)
                                return v
                        }
                    },
                    {
                        header: "SĐT", value: "phoneNumber",
                        format: function (v) {
                            if (v)
                                return v;
                        }
                    },
                    {
                        header: "Email", value: "email",
                        format: function (v) {
                            if (v)
                                return v;
                        }
                    },
                    {
                        header: "Xác Nhận Email", value: "emailConfirmed",
                        format: function (v) {
                            if (v)
                                return '<span class="badge badge-success-light">YES</span>'

                            return '<span class="badge badge-danger-light">NO</span>'
                        },
                         width: 40
                    }
                ],
                resizable: true,
                draggable: true,
                width: "100%",
                height: "100%",
            },
            types: {
                default: { "icon": "fa fa-folder m--font-success" },
                file: { "icon": "fa fa-file  m--font-success" }
            },
            state: { "key": "demo2" }
        })
    }
}