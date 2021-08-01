var functionController = function () {

    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        $('#btnSave').on('click', function (e) { ValidateData(); });
    }

    function ValidateData() {
        var data = {
            Id: $('#txtId').val(),
            Name: $('#txtName').val(),
            ParentId: $('#txtParentId').val(),
            IconCss: $('#txtIconCss').val(),
            Url: $('#txtUrl').val(),
            ActionControl: $('#txtActionControl').val(),
            Action: $("#txtAction").val(),
            SortOrder: $("#txtSortOrder").val(),
            Status: $('#ckStatus').prop('checked') == true ? 1 : 0,
            IsFrontEnd: $('#ckIsFrontEnd').prop('checked') == true ? 1 : 0
        };

        var isValid = true;
        if (!data.Id) {
            be.notify('Root ID is required!!!', "", 'error');
            isValid = false;
        }
        if (!data.Name) {
            be.notify('Name is required!!!', "", 'error');
            isValid = false;
        }

        if (!data.ParentId) {
            be.notify('Parent ID is required!!!', "", 'error');
            isValid = false;
        }
        else {
            data.ParentId = data.ParentId == "#" ? null : data.ParentId;
        }

        if (!data.Url) {
            be.notify('Url is required!!!', "", 'error');
            isValid = false;
        }

        if (isValid) {
            if (data.Action == 0) {
                checkIsExistId(data.Id, function (response) {
                    if (response) {
                        be.notify('Root ID already exists in the system!!!', "", 'error');
                    }
                    else {
                        createData(data);
                    }
                });
            }
            else {
                createData(data);
            }
        }
    }

    function checkIsExistId(id, cbfn) {
        $.ajax({
            type: "POST",
            url: "/Admin/Function/CheckIsExistId",
            data: { Id: id },
            dataType: "json",
            success: function (response) {
                if (cbfn != null) {
                    cbfn(response);
                }
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
            },
        });
    }

    function createData(data) {
        $.ajax({
            type: "POST",
            url: "/Admin/Function/SaveEntity",
            data: { functionVm: data },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {

                be.notify('Add function is success!!!', "", 'success');
                $('#modal-add-edit').modal('hide');

                be.stopLoading();

                loadData(true);
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            },
        });
    }

    function removeData(id, text) {

        be.confirm('Do you want remove ' + text + '?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Function/Delete",
                data: { id: id },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    be.notify('delete is success!!!!', '', 'success');
                    be.stopLoading();

                    loadData(true);
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                },
            });
        });
    }

    function updateParentId(data) {
        $.ajax({
            type: "POST",
            url: "/Admin/Function/UpdateParentId",
            data: { id: data.node.id, parentId: data.parent },
            dataType: "json",
            beforeSend: function () { },
            success: function (response) {
                be.notify('Update Parent ID is success!!!!', '', 'success');

                loadData(true);
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            },
        });
    }

    function resetFormMaintainance() {
        $('#txtId').val('');
        $("#txtId").attr('readonly', false);
        $('#txtName').val('');
        $('#txtParentName').val('');
        $('#txtParentId').val('');
        $('#txtIconCss').val('');
        $('#txtUrl').val('#');
        $('#txtActionControl').val('');
        $('#ckStatus').prop('checked', true);
        $('#ckIsFrontEnd').prop('checked', false);
    }

    function loadData(reload) {
        $.ajax({
            url: '/Admin/Function/GetAll',
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (response) {
                response.sort(function (a, b) {
                    return a.data.sortOrder - b.data.sortOrder;
                });

                if (reload) {
                    $('div#jstree').jstree(true).settings.core.data = response;
                    $('div#jstree').jstree(true).refresh();
                }
                else {
                    fillData(response);
                }
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            },
        });
    }

    var fillData = function (response) {
        $("div#jstree").jstree({
            plugins: ["table", "contextmenu", "dnd", "state", "types"],
            core: {
                themes: { responsive: false },
                check_callback: true,
                data: response
            },
            table: {
                columns: [
                    {
                        header: "Name",
                        format: function (v) {
                            if (v) {
                                return '<span>' + v + '</span >'
                            }
                        }
                    },
                    {
                        header: "Url", value: "url",
                        format: function (v) {
                            if (v) {
                                return '<span>' + v + '</span >'
                            }
                        }
                    },
                    {
                        header: "Action", value: "actionControl",
                        format: function (v) {
                            if (v) {
                                return '<span>' + v + '</span >'
                            }
                        }
                    },
                    {
                        header: "FrontEnd", value: "isFrontEnd",
                        format: function (v) {
                            if (v) {
                                return '<span class="badge badge-primary-light">Yes</span>'
                            }
                            return '<span class="badge badge-secondary-light">No</span>'
                        },
                    },
                    {
                        header: "Status", value: "status",
                        format: function (v) {
                            if (v)
                                return '<span class="badge badge-success-light">Kích hoạt</span>'

                            return '<span class="badge badge-secondary-light">Vô hiệu</span>'
                        },
                        width: 40
                    }
                ],
                resizable: true,
                draggable: true,
                //contextmenu: true,
                width: "100%",
                height: "100%",
            },
            types: {
                default: { "icon": "fa fa-folder m--font-success" },
                file: { "icon": "fa fa-file  m--font-success" }
            },
            state: { "key": "demo2" },
            contextmenu: {
                items: contextmenuHandle
            }
        })
            .bind("move_node.jstree", (e, data) => {
                if (data.parent === data.old_parent) {

                }
                else {
                    updateParentId(data);
                }
            })
    }

    var contextmenuHandle = function ($node) {
        return {
            "Create": {
                "separator_before": false,
                "separator_after": false,
                "label": "Tạo Mới",
                "action": function (obj) {

                    resetFormMaintainance();
                    var modalShow = $('#modal-add-edit');
                    modalShow.find("#txtAction").val(0);
                    modalShow.find("#txtSortOrder").val(1);
                    modalShow.find("#txtParentName").val($node.text);
                    modalShow.find("#txtParentId").val($node.id);
                    modalShow.modal("show");
                }
            },
            "Rename": {
                "separator_before": false,
                "separator_after": false,
                "label": "Cập Nhật",
                "action": function (obj) {
                    resetFormMaintainance();

                    var treeRoot = $("div#jstree").jstree(true);
                    var modalShow = $('#modal-add-edit');
                    modalShow.find("#txtAction").val(1);
                    modalShow.find("#txtSortOrder").val($node.data.sortOrder);
                    modalShow.find("#txtId").val($node.id);
                    modalShow.find("#txtId").attr('readonly', true);
                    modalShow.find("#txtName").val($node.text);
                    modalShow.find("#txtIconCss").val($node.data.iconCss);
                    modalShow.find("#txtUrl").val($node.data.url);
                    modalShow.find("#txtActionControl").val($node.data.actionControl);
                    modalShow.find("#txtParentName").val(treeRoot.get_node($node.parent).text);
                    modalShow.find("#txtParentId").val($node.parent);
                    modalShow.find("#ckStatus").prop('checked', $node.data.status === 1 ? true : false);
                    modalShow.find("#ckIsFrontEnd").prop('checked', $node.data.isFrontEnd === 1 ? true : false);
                    modalShow.modal("show");
                }
            },
            "Remove": {
                "separator_before": false,
                "separator_after": false,
                "label": "Xóa",
                "action": function (obj) {
                    removeData($node.id, $node.text);
                }
            }
        };
    }
}