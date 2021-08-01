var BlogCategoryController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
        registerControl();
    }

    function ValidateData() {
        var data = {
            Id: parseInt($('#hideId').val()),
            Name: $('#txtName').val(),
            Description: CKEDITOR.instances.txtDescription.getData(),
            Type: $('#Type').val(),
            ParentId: parseInt($('#txtParentId').val()),
            FunctionId: $('#ddlFunctionId').combotree('getValue'),
            IconCss: $('#txtIconCss').val(),
            Url: $('#txtUrl').val(),
            SortOrder: $("#txtSortOrder").val(),
            SeoPageTitle: $('#txtSeoPageTitle').val(),
            SeoKeywords: $('#txtSeoKeywords').val(),
            SeoDescription: $('#txtSeoDescription').val(),
            IsMain: $('#ckIsMain').prop('checked') == true ? 1 : 0,
            Status: $('#ckStatus').prop('checked') == true ? 1 : 0
        };

        var isValid = true;

        if (!data.Name)
            isValid = be.notify('Title is required!!!', "", 'error');

        if (!data.Type)
            isValid = be.notify('Type is required!!!', "", 'error');

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
            url: "/Admin/BlogCategory/SaveEntity",
            data: { blogCategoryVm: data },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {

                be.notify('Add category is success!!!', "", 'success');

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
                url: "/Admin/BlogCategory/Delete",
                data: { id: id },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function (response) {

                    be.notify('Remove category is success!!!!', '', 'success');

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
            url: "/Admin/BlogCategory/UpdateParentId",
            data: { id: data.node.id, parentId: data.parent == "#" ? null : data.parent },
            dataType: "json",
            beforeSend: function () { },
            success: function (response) {
                be.notify('Update parent id is success!!!!', '', 'success');

                loadData(true);
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            },
        });
    }
    function registerControl() {
        CKEDITOR.replace('txtDescription', {});

        $('#modal-add-edit').on('shown.bs.modal', function () {
            $(document).off('focusin.modal');
        });

        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };

        $('#Type').select2({
            placeholder: "choose type",
            allowClear: true,
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
            url: "/Admin/BlogCategory/GetAllFunction",
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
        $('#Type').val(null).trigger('change');
        $('#txtParentId').val(0);
        $('#txtIconCss').val('');
        $('#txtUrl').val('#');
        $('#txtSeoPageTitle').val('');
        $('#txtSeoKeywords').val('');
        $('#txtSeoDescription').val('');
        $('#ckIsMain').prop('checked', false);
        $('#ckStatus').prop('checked', true);
    }

    function loadData(reload) {
        $.ajax({
            url: '/Admin/BlogCategory/GetAll',
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
                            if (v)
                                return '<span>' + v + '</span >'
                        }
                    },
                    //{
                    //    header: "Description", value: "description",
                    //    format: function (v) {
                    //        if (v)
                    //            return '<span>' + v + '</span >'
                    //    }
                    //},
                    {
                        header: "Url", value: "url",
                        format: function (v) {
                            if (v)
                                return '<span>' + v + '</span >'
                        }
                    },
                    {
                        header: "Type", value: "typeName",
                        format: function (v) {
                            if (v)
                                return '<span>' + v + '</span >'
                        }
                    },
                    {
                        header: "Function", value: "functionName",
                        format: function (v) {
                            if (v)
                                return '<span>' + v + '</span >'
                        }
                    },
                    {
                        header: "Sort Order", value: "sortOrder",
                        format: function (v) {
                            if (v)
                                return '<span class="badge badge-primary-light">' + v + '</span >'
                        }
                    },
                    {
                        header: "Status", value: "status",
                        format: function (v) {
                            if (v)
                                return '<span class="badge badge-success-light">Active</span>'

                            return '<span class="badge badge-secondary-light">InActive</span>'
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
                "label": "Create New",
                "action": function (obj) {

                    resetFormMaintainance();
                    initTreeDdlFunction();
                    CKEDITOR.instances.txtDescription.setData('');
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

                    CKEDITOR.instances.txtDescription.setData($node.data.description);

                    var modalShow = $('#modal-add-edit');
                    modalShow.find('#Type').val($node.data.type).trigger('change');
                    modalShow.find("#txtSortOrder").val($node.data.sortOrder);
                    modalShow.find("#hideId").val($node.id);
                    modalShow.find("#txtName").val($node.text);
                    modalShow.find("#txtIconCss").val($node.data.iconCss);
                    modalShow.find("#txtUrl").val($node.data.url);
                    modalShow.find("#txtParentName").val(treeRoot.get_node($node.parent).text);
                    modalShow.find("#txtParentId").val($node.parent);
                    modalShow.find('#txtSeoPageTitle').val($node.data.seoPageTitle);
                    modalShow.find('#txtSeoKeywords').val($node.data.seoKeywords);
                    modalShow.find('#txtSeoDescription').val($node.data.seoDescription);
                    modalShow.find("#ckIsMain").prop('checked', $node.data.isMain === 1 ? true : false);
                    modalShow.find("#ckStatus").prop('checked', $node.data.status === 1 ? true : false);
                    modalShow.modal("show");
                }
            },
            "Remove": {
                "separator_before": false,
                "separator_after": false,
                "label": "Remove",
                "action": function (obj) {
                    removeData($node.id, $node.text);
                }
            }
        };
    }
}