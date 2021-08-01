var be = {
    configs: {
        pageSize: 10,
        pageUserSize: 150,
        pageIndex: 1
    },
    notify: function (message, title, type) {

        if (type.search("success") >= 0) {
            $.growl.notice({ message: message });
            return true;
        }
        else {
            $.growl.error({ message: message });
            return false;
        }
    },
    confirm: function (message, okCallback) {
        swal({
            title: "Xác Nhận",
            text: message,
            type: "warning",
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
        }, function (result) {

            debugger;
            if (result) {
                okCallback()
            }
        });
    },
    error: function (title, message) {
        swal(title, message, "error");
    },
    success: function (title, message) {
        swal(title, message, "success");
    },
    dateFormatJson: function (datetime) {
        if (datetime == null || datetime == '')
            return '';
        var newdate = new Date(datetime);
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        var hh = newdate.getHours();
        var mm = newdate.getMinutes();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        return day + "/" + month + "/" + year;
    },
    dateTimeFormatJson: function (datetime) {
        if (datetime == null || datetime == '')
            return '';
        var newdate = new Date(datetime);
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        var hh = newdate.getHours();
        var mm = newdate.getMinutes();
        var ss = newdate.getSeconds();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        if (ss < 10)
            ss = "0" + ss;
        return day + "/" + month + "/" + year + " " + hh + ":" + mm + ":" + ss;
    },
    startLoading: function () {
        $("#preloader").fadeIn("slow");
    },
    stopLoading: function () {
        $("#preloader").fadeOut("slow");
    },
    getStatus: function (status) {
        if (status == 1)
            return '<span class="badge badge-primary-light">Kích Hoạt</span>';
        else
            return '<span class="badge badge-secondary-light">Khóa</span>';
    },
    getType: function (type) {
        if (type == 1)
            return '<span class="badge badge-primary-light">Mới</span>';
        else if (type == 2)
            return '<span class="badge badge-secondary-light">Đã Xem</span>';
        else
            return '<span class="badge badge-success-light">Đã Phản Hồi</span>';
    },
    formatNumber: function (number, precision) {
        if (!isFinite(number)) {
            return number.toString();
        }

        var a = number.toFixed(precision).split('.');
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, '$&,');
        return a.join('.');
    },
    formatCurrency: function (num) {
        num = num.toString().replace(/\$|\,/g, '');
        if (isNaN(num))
            num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        cents = num % 100;
        num = Math.floor(num / 100).toString();
        if (cents < 10)
            cents = "0" + cents;
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                num.substring(num.length - (4 * i + 3));
        return (((sign) ? '' : '-') + num + '.' + cents);
    },
    unflattern: function (arr) {
        var map = {};
        var roots = [];
        for (var i = 0; i < arr.length; i += 1) {
            var node = arr[i];
            node.children = [];
            map[node.id] = i; // use map to look-up the parents
            if (node.parentId !== null) {
                arr[map[node.parentId]].children.push(node);
            } else {
                roots.push(node);
            }
        }
        return roots;
    },
    wrapUserPaging: function (recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / be.configs.pageUserSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: '<<',
            prev: '<',
            next: '>',
            last: '>>',
            onPageClick: function (event, p) {
                //if (be.configs.pageIndex !== p) {
                be.configs.pageIndex = p;
                callBack();
                //}
            }
        });
    },
    wrapPaging: function (recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / be.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: '<<',
            prev: '<',
            next: '>',
            last: '>>',
            onPageClick: function (event, p) {
                //if (be.configs.pageIndex !== p) {
                be.configs.pageIndex = p;
                callBack();
                //}
            }
        });
    }
}

$(document).ajaxSend(function (e, xhr, options) {
    if (options.type.toUpperCase() == "POST" || options.type.toUpperCase() == "PUT") {
        var token = $('form').find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});