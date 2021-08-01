var MenuItemController = function () {
    this.initialize = function (menuGroupId) {
        MenuGroupId = parseInt(menuGroupId);
        loadData();
        registerEvents();
    }
    var MenuGroupId;
    function ValidateData() {
        var data = {
            Id: parseInt($('#hideId').val()),
            Name: $('#txtName').val(),
            ParentId: parseInt($('#txtParentId').val()),
            FunctionId: $('#ddlFunctionId').combotree('getValue'),
            MenuGroupId: MenuGroupId,
            IconCss: $('#txtIconCss').val(),
            Url: $('#txtUrl').val(),
            SortOrder: $("#txtSortOrder").val(),
            Status: $('#ckStatus').prop('checked') == true ? 1 : 0,
        };

        var isValid = true;

        if (!data.Name)
            isValid = be.notify('Name is required!!!', "", 'error');

        if (!data.MenuGroupId)
            isValid = be.notify('Menu group is required!!!', "", 'error');

        if (!data.FunctionId)
            isValid = be.notify('Function is required!!!', "", 'error');

        if (isNaN(data.ParentId))
            data.ParentId = null;

        if (!data.Url)
            isValid = be.notify('Url is required!!!', "", 'error');

        if (isValid)
            createData(data);
    }


    function createData(data) {
        $.ajax({
            type: "POST",
            url: "/Admin/MenuItem/SaveEntity",
            data: { menuItemVm: data },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {

                be.notify('Add menu item is success!!!', "", 'success');
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

        be.confirm('Do you want delete ' + text + '?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/MenuItem/Delete",
                data: { id: id },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    be.notify('Delete is success!!!!', '', 'success');
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
            url: "/Admin/MenuItem/UpdateParentId",
            data: { id: data.node.id, parentId: data.parent == "#" ? null : data.parent },
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

    function registerEvents() {
        $('#btnSave').on('click', function (e) {
            ValidateData();
        });

        $('#btnCreate').on('click', function (e) {
            resetFormMaintainance();
            initTreeDdlFunction();

            var modalShow = $('#modal-add-edit');
            modalShow.find("#txtSortOrder").val(1);
            modalShow.find("#txtParentName").val("");
            modalShow.find("#txtParentId").val("#");
            modalShow.modal("show");
        });
    };

    function initTreeDdlFunction(selectedId) {
        $.ajax({
            url: "/Admin/MenuItem/GetAllFunction",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                response.sort(function (a, b) {
                    return a.data.sortOrder - b.data.sortOrder;
                });

                $('#ddlFunctionId').combotree({
                    data: response,
                    onSelect: function ($node) {
                        $("#txtUrl").val($node.data.url);
                    }
                });
                if (selectedId != undefined) {
                    $('#ddlFunctionId').combotree('setValue', selectedId);
                }
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
            }
        });
    }

    function resetFormMaintainance() {
        $('#hideId').val(0);
        $('#txtName').val('');
        $('#txtParentName').val('');
        $('#txtParentId').val(0);
        $('#txtIconCss').val('');
        $('#txtUrl').val('/');
        $('#ckStatus').prop('checked', true);
    }

    function loadData(reload) {
        $.ajax({
            url: '/Admin/MenuItem/GetAll',
            data: { menuGroupId: MenuGroupId },
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
            }
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
                        header: "Function", value: "functionName",
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
                        header: "Sort Order", value: "sortOrder",
                        format: function (v) {
                            if (v) {
                                return '<span class="badge badge-primary-light">' + v + '</span >'
                            }
                        }
                    },
                    {
                        header: "Icon Css", value: "iconCss",
                        format: function (v) {
                            if (v) {
                                return '<span class="badge badge-info-light">' + v + '</span >'
                            }
                        }
                    },
                    {
                        header: "Status", value: "status",
                        format: function (v) {
                            if (v)
                                return '<span class="badge badge-success-light">Yes</span>'

                            return '<span class="badge badge-secondary-light">No</span>'
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
                if (data.parent === data.old_parent) { }
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
                "label": "Create",
                "action": function (obj) {

                    resetFormMaintainance();
                    initTreeDdlFunction();

                    var modalShow = $('#modal-add-edit');
                    modalShow.find("#txtSortOrder").val(1);
                    modalShow.find("#txtParentName").val($node.text);
                    modalShow.find("#txtParentId").val($node.id);
                    modalShow.modal("show");
                }
            },
            "Rename": {
                "separator_before": false,
                "separator_after": false,
                "label": "Update",
                "action": function (obj) {
                    var treeRoot = $("div#jstree").jstree(true);

                    resetFormMaintainance();
                    initTreeDdlFunction($node.data.functionId);

                    var modalShow = $('#modal-add-edit');
                    modalShow.find("#txtSortOrder").val($node.data.sortOrder);
                    modalShow.find("#hideId").val($node.id);
                    modalShow.find("#txtName").val($node.text);
                    modalShow.find("#txtIconCss").val($node.data.iconCss);
                    modalShow.find("#txtUrl").val($node.data.url);
                    modalShow.find("#txtParentName").val(treeRoot.get_node($node.parent).text);
                    modalShow.find("#txtParentId").val($node.parent);
                    modalShow.find("#ckStatus").prop('checked', $node.data.status === 1 ? true : false);
                    modalShow.modal("show");

                }
            },
            "Remove": {
                "separator_before": false,
                "separator_after": false,
                "label": "Delete",
                "action": function (obj) {
                    removeData($node.id, $node.text);
                }
            }
        };
    }
}