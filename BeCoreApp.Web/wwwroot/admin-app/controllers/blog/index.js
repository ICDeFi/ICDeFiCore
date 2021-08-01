var BlogController = function () {
    this.initialize = function () {
        initTreeDdlBlogCategory('#ddlSearchBlogCategoryId');

        be.startLoading();
        loadData();
        be.stopLoading();

        registerEvents();
        registerControls();
    }
    var dataBlog = null;
    function validateData() {

        dataBlog = {
            Id: +($('#hidId').val()),
            Name: $('#txtName').val(),
            Image: $('#txtImage').val(),
            Tags: $('#ddlTags').val(),
            Description: $('#txtDescription').val(),
            SeoPageTitle: $('#txtSeoPageTitle').val(),
            SeoKeywords: $('#txtSeoKeywords').val(),
            SeoDescription: $('#txtSeoDescription').val(),
            BlogCategoryId: $('#ddlBlogCategoryId').combotree('getValue'),
            Status: $('#ckStatus').prop('checked') === true ? 1 : 0,
            HotFlag: $('#ckHot').prop('checked'),
            HomeFlag: $('#ckShowHome').prop('checked'),
            MildContent: CKEDITOR.instances.txtContent.getData()
        }

        var isValid = true;
        if (!dataBlog.Name)
            isValid = be.notify('Tên không được bỏ trống!!!', "", 'error');

        if (!dataBlog.Image)
            isValid = be.notify('Ảnh bìa không được bỏ trống!!!', "", 'error');

        if (!dataBlog.Description)
            isValid = be.notify('Tóm tắt không được bỏ trống!!!', "", 'error');

        dataBlog.Tags = dataBlog.Tags.filter(function (x) { return x != "" });
        if (dataBlog.Tags.length == 0)
            isValid = be.notify('Tags không được bỏ trống!!!', "", 'error');

        if (!dataBlog.MildContent)
            isValid = be.notify('Tổng quan không được bỏ trống!!!', "", 'error');

        if (!dataBlog.BlogCategoryId)
            isValid = be.notify('Danh mục không được bỏ trống!!!', "", 'error');

        return isValid;
    }

    function registerControls() {
        CKEDITOR.replace('txtContent', {});


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


        $('#ddlTags').select2({
            placeholder: "Tags...",
            allowClear: true,
            tags: true,
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

        $('body').on('click', "#btn-create", function (e) {
            e.preventDefault();
            resetFormMaintainance();
            initTreeDdlBlogCategory('#ddlBlogCategoryId');
            $('#modal-add-edit').modal('show');
        });

        $('body').on('click', '#btnSelectImg', function () {
            $('#fileInputImage').click();
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

        $('body').on('click', '.btn-edit', function (e) { loadDetails(e, $(this).data('id')) });

        $('body').on('click', '#btnSave', function (e) { saveBlog(e) });

        $('body').on('click', '.btn-delete', function (e) { deleteBlog(e, this) });
    }


    function saveBlog(e) {
        e.preventDefault();

        if (validateData()) {
            debugger;
            $.ajax({
                type: "POST",
                url: "/Admin/Blog/SaveEntity",
                data: { blogVm: dataBlog },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Update news is success', "", 'success');
                    $('#modal-add-edit').modal('hide');
                    resetFormMaintainance();
                    be.stopLoading();
                    loadData(true);
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        }
    }

    function deleteBlog(e, element) {
        e.preventDefault();
        be.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Blog/Delete",
                data: { id: $(element).data('id') },
                dataType: "json",
                beforeSend: function () {
                    be.startLoading();
                },
                success: function () {
                    be.notify('Remove news is success', "", 'success');
                    be.stopLoading();
                    loadData();
                },
                error: function (message) {
                    be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                    be.stopLoading();
                }
            });
        });
    }

    function loadDetails(e, id) {
        e.preventDefault();

        $.ajax({
            type: "GET",
            url: "/Admin/Blog/GetById",
            data: { id: id },
            dataType: "json",
            beforeSend: function () {
                be.startLoading();
            },
            success: function (response) {
                var tagArray = response.BlogTags;
                $('#ddlTags').val(tagArray.map(function (x) { return x.TagId })).trigger('change');
                $('#hidId').val(response.Id);
                $('#txtName').val(response.Name);
                $('#txtImage').val(response.Image);
                $('#txtDescription').val(response.Description);
                $('#txtSeoPageTitle').val(response.SeoPageTitle);
                $('#txtSeoKeywords').val(response.SeoKeywords);
                $('#txtSeoDescription').val(response.SeoDescription);

                CKEDITOR.instances.txtContent.setData(response.MildContent);

                $('#ckStatus').prop('checked', response.Status == 1);
                $('#ckHot').prop('checked', response.HotFlag);
                $('#ckShowHome').prop('checked', response.HomeFlag);

                initTreeDdlBlogCategory('#ddlBlogCategoryId', response.BlogCategoryId);

                be.stopLoading();

                $('#modal-add-edit').modal('show');
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                be.stopLoading();
            }
        });
    }

    function initTreeDdlBlogCategory(selector, selectedId) {
        $.ajax({
            url: "/Admin/Blog/GetAllBlogCategory",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {

                response.sort(function (a, b) {
                    return a.data.sortOrder - b.data.sortOrder;
                });

                if (selector == "#ddlBlogCategoryId") {
                    $(selector).combotree({
                        data: response
                    });
                }
                else {
                    $(selector).combotree({
                        data: response,
                        onSelect: function ($node) {
                            nodeBlogCategoryId = $node.id;
                            loadData(true);
                        }
                    });
                }

                if (selectedId != undefined)
                    $(selector).combotree('setValue', selectedId);
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
            }
        });
    }

    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtName').val('');
        $('#txtImage').val('');
        $('#ddlTags').val(null).trigger('change');
        $('#txtDescription').val('');
        $('#txtSeoPageTitle').val('');
        $('#txtSeoKeywords').val('');
        $('#txtSeoDescription').val('');
        $('#ckStatus').prop('checked', true);
        $('#ckHot').prop('checked', false);
        $('#ckShowHome').prop('checked', false);
        CKEDITOR.instances.txtContent.setData('');
    }
    var nodeBlogCategoryId = 0;
    function loadData(isPageChanged) {

        $.ajax({
            type: 'GET',
            data: {
                blogCategoryId: nodeBlogCategoryId,
                keyword: $('#txt-search-keyword').val(),
                page: be.configs.pageIndex,
                pageSize: be.configs.pageSize
            },
            url: '/admin/Blog/GetAllPaging',
            dataType: 'json',
            beforeSend: function () {
                //be.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                $.each(response.Results, function (i, item) {
                    let listTags = "";
                    $.each(item.BlogTags, function () {
                        listTags += '<span class="m-badge m-badge--warning m-badge--wide m--margin-bottom-5 m--margin-right-5">' + this.TagId + '</span>'
                    })
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Tags: listTags,
                        BlogCategoryName: item.BlogCategoryName,
                        Image: item.Image == null ? '<img src="/admin-side/images/user.png" width=100' : '<img src="' + item.Image + '" width=100 />',
                        Status: be.getStatus(item.Status)
                    });
                });

                $('#lbl-total-records').text(response.RowCount);

                $('#tbl-content').html(render);

                if (response.RowCount)
                    be.wrapPaging(response.RowCount, function () { loadData() }, isPageChanged);

                //be.stopLoading();
            },
            error: function (message) {
                be.notify(`${message.responseText}`, `Status code: ${message.status}`, 'error');
                //be.stopLoading();
            }
        });
    }
}